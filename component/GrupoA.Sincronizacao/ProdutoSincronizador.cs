using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using GrupoaA.Log;

namespace GrupoA.Sincronizacao
{
    public class ProdutoSincronizador
    {
        #region [ Proprerties ]

        List<Produto> produtoBOList = null;
        string remoteHost = Convert.ToString(ConfigurationManager.AppSettings["RemoteHost"]);
        string remoteUser = Convert.ToString(ConfigurationManager.AppSettings["RemoteUser"]);
        string remotePass = Convert.ToString(ConfigurationManager.AppSettings["RemotePass"]);

        string folderRemotePath = Convert.ToString(ConfigurationManager.AppSettings["SincProdutoFolderRemotePath"]);
        string folderLocalPath = Convert.ToString(ConfigurationManager.AppSettings["SincProdutoFolderLocalPath"]);

        private LogHelper objLogHelper = new LogHelper("SincronizadorProduto");
        private bool _flagTesteLocal = false;

        #endregion

        #region [ Methods ]

        /// <summary>
        /// 
        /// </summary>
        /// 
        public void SincronizarProduto()
        {
            objLogHelper.WriteOnFile(Environment.NewLine + "P1", "SincronizarProduto - Início Sincronização", "");
            try
            {
                if (_flagTesteLocal)
                {
                    this.ProcessarArquivo("C:\\inetpub\\wwwroot\\Artmed\\trunk\\sincronizador\\produtos5.csv");
                }
                else
                {
                    var fileList = this.FTPGetFileList();

                    if (fileList != null && fileList.Count > 0)
                    {
                        foreach (var item in fileList)
                        {
                            string fileLocalPath = this.SincronizarFTP(item.Name);
                            if (!String.IsNullOrEmpty(fileLocalPath))
                            {
                                this.ProcessarArquivo(fileLocalPath);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objLogHelper.WriteOnFile("P3", "Erro no processamento", ex.ToString());
            }

            objLogHelper.WriteOnFile("P2", "SincronizarProduto - Término Sincronização", "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileLocalPath"></param>
        public void ProcessarArquivo(string fileLocalPath)
        {
            objLogHelper.WriteOnFile(Environment.NewLine + "PA1", "ProcessarArquivo - Início Sincronização", "Arquivo: " + fileLocalPath);

            try
            {
                //string fileLocalPath = this.SincronizarFTP();

                if (!String.IsNullOrEmpty(fileLocalPath))
                {
                    produtoBOList = new List<Produto>();

                    produtoBOList = this.ProcessarCSV(fileLocalPath);

                    if (produtoBOList != null && produtoBOList.Any())
                    {
                        this.ProcessarProduto(produtoBOList);

                        this.ArquivarCSV(fileLocalPath);
                    }
                }
            }
            catch (Exception ex)
            {
                objLogHelper.WriteOnFile("PA3", "ProcessarArquivo - Erro no processamento", ex.ToString());
            }

            objLogHelper.WriteOnFile("PA2", "ProcessarArquivo - Término Sincronização", "");
        }

        #region [ FTP ]


        private List<FTPLineResult> FTPGetFileList()
        {
            List<FTPLineResult> listFilesAndFolders = null;
            string fileLocalPath = string.Empty;
            FtpWebRequest reqFTP;
            FtpWebResponse response = null;
            StreamReader reader = null;
            string requestUri = String.Concat(remoteHost, folderRemotePath);

            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(requestUri));
            reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            reqFTP.EnableSsl = false;
            reqFTP.UseBinary = true;
            reqFTP.Credentials = new NetworkCredential(remoteUser, remotePass);

            try
            {
                response = (FtpWebResponse)reqFTP.GetResponse();

                reader = new StreamReader(response.GetResponseStream());
                //read file/directory names into arraylist
                string strFtpLine = reader.ReadLine();

                listFilesAndFolders = new List<FTPLineResult>();
                while (strFtpLine != null)
                {
                    try
                    {
                        var ftpLine = new FTPLineParser().Parse(strFtpLine);
                        listFilesAndFolders.Add(ftpLine);
                    }
                    catch { }
                    finally
                    {
                        strFtpLine = reader.ReadLine();
                    }
                }
            }
            catch (WebException ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            if (listFilesAndFolders != null && listFilesAndFolders.Count > 0)
            {
                listFilesAndFolders = (from obj in listFilesAndFolders
                                       where obj.IsDirectory == false
                                       orderby obj.DateTime
                                       select obj).ToList();
            }

            return listFilesAndFolders;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string SincronizarFTP(string fileName)
        {
            objLogHelper.WriteOnFile("S1", "SincronizarFTP - Início", "Arquivo: " + fileName);

            string fileLocalPath = string.Empty;
            FtpWebRequest reqFTP;
            bool hasFile = true;

            try
            {
                fileName = Regex.Replace(fileName.ToString(), @"\s.*$", "").Trim();

                string requestUri = String.Concat(remoteHost, folderRemotePath, fileName);
                FileStream outputStream = null;

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(requestUri));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.EnableSsl = false;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(remoteUser, remotePass);

                try
                {
                    FtpWebResponse response = null;

                    try
                    {
                        response = (FtpWebResponse)reqFTP.GetResponse();
                    }
                    catch (WebException ex)
                    {
                        response = (FtpWebResponse)ex.Response;
                        if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                        {
                            hasFile = false;//Does not exist
                        }
                        else
                        {
                            throw ex;
                        }
                    }

                    if (hasFile)
                    {
                        Stream ftpStream = response.GetResponseStream();
                        long cl = response.ContentLength;
                        int bufferSize = 2048;
                        int readCount;
                        byte[] buffer = new byte[bufferSize];

                        readCount = ftpStream.Read(buffer, 0, bufferSize);

                        outputStream = new FileStream(String.Concat(folderLocalPath, fileName), FileMode.Create);
                        while (readCount > 0)
                        {
                            outputStream.Write(buffer, 0, readCount);
                            readCount = ftpStream.Read(buffer, 0, bufferSize);
                        }

                        ftpStream.Close();
                        response.Close();

                        reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(requestUri));
                        reqFTP.Method = WebRequestMethods.Ftp.Rename;
                        reqFTP.RenameTo = String.Concat("../processados/", fileName.Replace(".csv", String.Concat(".csvOLD_", DateTime.Now.ToString("yyyyMMddhhmmssFFFF"))));
                        reqFTP.EnableSsl = false;
                        reqFTP.UseBinary = true;
                        reqFTP.Credentials = new NetworkCredential(remoteUser, remotePass);
                        response = (FtpWebResponse)reqFTP.GetResponse();
                        response.Close();
                    }
                    else
                    {
                        objLogHelper.WriteOnFile("S2", "SincronizarFTP - Sem arquivo para sincronizar");
                    }
                }
                catch (Exception ex)
                {
                    objLogHelper.WriteOnFile("S3", "SincronizarFTP - Erro", ex.ToString());
                }
                finally
                {
                    if (outputStream != null)
                    {
                        outputStream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                objLogHelper.WriteOnFile("S4", "SincronizarFTP- Erro", ex.ToString());
            }

            if (this.ArquivoExiste(folderLocalPath, fileName))
            {
                fileLocalPath = String.Concat(folderLocalPath, fileName);
            }

            objLogHelper.WriteOnFile("S5", "SincronizarFTP - Término");

            return fileLocalPath;
        }

        /// <summary>
        /// Move o CSV para uma pasta de backup local e renomeia a extensão, e apaga o arquivo no FTP
        /// </summary>
        /// <param name="fileLocalPath"></param>
        public void ArquivarCSV(string fileLocalPath)
        {
            string folderLocalPathToStore = ConfigurationManager.AppSettings["SincProdutoFolderLocalPathToStore"].ToString();
            string fileName = ConfigurationManager.AppSettings["SincProdutoFileName"].ToString();

            try
            {
                objLogHelper.WriteOnFile("A1", "ArquivarCSV", "Início");

                if (!Directory.Exists(folderLocalPathToStore))
                {
                    Directory.CreateDirectory(folderLocalPathToStore);
                }

                if (Directory.Exists(folderLocalPathToStore))
                {
                    File.Move(fileLocalPath, String.Concat(folderLocalPathToStore, fileName.Replace(".csv", String.Concat(".csvOLD_", DateTime.Now.ToString("yyyyMMddhhmmssFFFF")))));
                    objLogHelper.WriteOnFile("A2", "ArquivarCSV - Arquivo " + fileLocalPath + " Arquivado");
                }
                else
                {
                    objLogHelper.WriteOnFile("A3", "ArquivarCSV - Arquivo Não Encontrado");
                }
            }
            catch (Exception ex)
            {
                objLogHelper.WriteOnFile("A4", "ArquivarCSV - Erro", ex.ToString());
            }

            objLogHelper.WriteOnFile("A5", "ArquivarCSV", "Término");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="arquivo"></param>
        /// <returns></returns>
        public bool ArquivoExiste(string filePath, string arquivo)
        {
            try
            {
                string pathFile = string.Concat(filePath, arquivo);

                FileInfo info = new FileInfo(pathFile);

                if (info.Exists && info.Length > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region [ Processar CSV]

        /// <summary>
        /// Lê o CSV e devolve uma Lista de Produto
        /// </summary>
        /// <param name="produtoBOList"></param>
        private List<Produto> ProcessarCSV(string pathCSV)
        {
            List<Produto> produtoBOList = new List<Produto>();
            DataTable dataTableCSV = new DataTable();

            if (!File.Exists(pathCSV))
            {
                return null;
            }

            int countProduto = 0;
            string line;

            System.IO.StreamReader file = new System.IO.StreamReader(pathCSV, System.Text.Encoding.Default); // Read the file and display it line by line.

            while ((line = file.ReadLine()) != null)
            {
                try
                {
                    if (line != null && !line.ToUpper().Contains("CODIGO"))
                    {
                        Produto produtoBO = this.ProcessarLinhaCSV(line);

                        produtoBOList.Add(produtoBO);

                        countProduto++;
                    }
                }
                catch (Exception err) { }
            }

            file.Close();

            return produtoBOList;
        }

        /// <summary>
        /// Lê a linha do CSV e devolve um Produto
        /// </summary>
        /// <param name="linhaCSV"></param>
        /// <returns></returns>
        private Produto ProcessarLinhaCSV(string linhaCSV)
        {
            string[] colunaCSV = Convert.ToString(linhaCSV).Split(';');

            Produto produtoBO = new Produto();

            if (colunaCSV != null && colunaCSV.Any())
            {
                if (colunaCSV[2] != null && !String.IsNullOrEmpty(colunaCSV[2].ToString().Trim())) // Verifica tipo Produto
                {
                    switch (colunaCSV[2].ToString().Trim())
                    {
                        case "ME": // Titulo Impresso
                            produtoBO = this.ProcessarME(linhaCSV);
                            break;
                        case "EB": // Titulo Eletronico
                            produtoBO = this.ProcessarTituloEletronico(linhaCSV);
                            break;
                        case "MC": // Capitulo Impresso
                            produtoBO = this.ProcessarCapituloImpresso(linhaCSV);
                            break;
                        case "EC": // Capitulo Eletronico
                            produtoBO = this.ProcessarCapituloEletronico(linhaCSV);
                            break;
                        //case "RE": // Revista Edição
                        //    produtoBO = this.ProcessarRevistaEdicao(linhaCSV);
                        //    break;
                        //case "RA": // Revista Assinatura
                        //    produtoBO = this.ProcessarRevistaAssinatura(linhaCSV);
                        //    break;
                        case "EA": // Titulo Eletronico Aluguel
                            produtoBO = this.ProcessaTituloAluguel(linhaCSV);
                            break;
                        default:
                            break;
                    }
                }
            }

            return produtoBO;
        }

        /// <summary>
        /// Divide os produtos do tipo ME, em TituloImpresso, Revista Assinatura  Revista Ediação
        /// </summary>
        /// <param name="linhaCSV"></param>
        /// <returns></returns>
        private Produto ProcessarME(string linhaCSV)
        {
            Produto produtoBO = new Produto();

            string[] colunaCSV = Convert.ToString(linhaCSV).Split(';');

            if (colunaCSV[5] != null && !String.IsNullOrEmpty(colunaCSV[5].ToString().Trim()))
            {
                switch (colunaCSV[5].ToString().Trim())
                {
                    case "44.01.00": // Assinatura BMJ
                    case "55.01.00": // Assinatura Patio Infantil
                    case "55.02.00": // Assinatura Patio Pedagogica
                    case "55.03.00": // Assinatura Patio Ensino Medio
                        produtoBO = this.ProcessarRevistaAssinatura(linhaCSV);
                        break;
                    case "44.00.00": // Periódicos BMJ
                    case "44.01.01": // Edição BMJ
                    case "55.00.00": // Periódicos Patio Infantil
                    case "55.01.01": // Edição Patio Infantil
                    case "55.02.01": // Edição Patio Pedagogica
                    case "55.03.01": // Edição Patio Ensino Medio
                        produtoBO = this.ProcessarRevistaEdicao(linhaCSV);
                        break;
                    default:
                        produtoBO = this.ProcessarTituloImpresso(linhaCSV);
                        break;
                }
            }

            return produtoBO;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seloId"></param>
        /// <returns></returns>
        private Selo ProcessarSelo(int seloId)
        {
            Selo seloBO = new Selo();

            switch (seloId)
            {
                case 1: // Artmed Editora
                    seloBO.SeloId = 1;
                    break;
                case 2: // Bookman
                    seloBO.SeloId = 2;
                    break;
                case 3: // Mc Graw Hill
                    seloBO.SeloId = 5;
                    break;
                case 4: // Penso
                    seloBO.SeloId = 3;
                    break;
                case 19: // Artes Médicas
                    seloBO.SeloId = 4;
                    break;
                case 5: // Tekné
                    seloBO.SeloId = 6;
                    break;
                default:
                    break;
            }

            return seloBO;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="linhaCSV"></param>
        /// <returns></returns>
        private Produto ProcessarTituloImpresso(string linhaCSV)
        {
            Produto produtoBO = new Produto();
            produtoBO.ProdutoTipo = new ProdutoTipo();
            produtoBO.ProdutoFormato = new ProdutoFormato();
            produtoBO.Conteudo = new Conteudo();
            produtoBO.Conteudo.ConteudoTipo = new ConteudoTipo();
            produtoBO.TituloImpresso = new TituloImpresso();
            produtoBO.TituloImpresso.Titulo = new Titulo();
            produtoBO.TituloImpresso.Titulo.TituloAutores = new List<TituloAutor>();

            produtoBO.ProdutoTipo.ProdutoTipoId = 1;
            produtoBO.Fabricante = new Fabricante();
            produtoBO.Fabricante.FabricanteId = 1;
            produtoBO.ExibirSite = false;
            produtoBO.Disponivel = false;
            produtoBO.UtilizaFrete = true;
            produtoBO.TituloImpresso.Titulo.MaisVendido = false;

            string[] colunaCSV = Convert.ToString(linhaCSV).Split(';');

            if (colunaCSV[0] != null && !String.IsNullOrWhiteSpace(colunaCSV[0].ToString().Trim()))
            {
                produtoBO.CodigoProduto = colunaCSV[0].ToString().Trim();
                produtoBO.TituloImpresso.Isbn10 = colunaCSV[0].ToString().Trim();
            }

            if (colunaCSV[11] != null && !String.IsNullOrWhiteSpace(colunaCSV[11].ToString().Trim()))
            {
                produtoBO.TituloImpresso.Isbn13 = colunaCSV[11].ToString().Trim();
            }

            if (colunaCSV[1] != null && !String.IsNullOrWhiteSpace(colunaCSV[1].ToString().Trim()))
            {
                produtoBO.NomeProduto = colunaCSV[1].ToString().Trim();
                produtoBO.TituloImpresso.Titulo.NomeTitulo = colunaCSV[1].ToString().Trim();
            }

            if (
                colunaCSV[5] != null
                && !String.IsNullOrWhiteSpace(colunaCSV[5].ToString().Trim())
                && colunaCSV[17] != null
                && !String.IsNullOrWhiteSpace(colunaCSV[17].ToString().Trim())
                )
            {
                produtoBO.CodigoLegadoCategoria = colunaCSV[5].ToString().Trim();
                produtoBO.NomeLegadoCategoria = colunaCSV[17].ToString().Trim();
            }

            if (colunaCSV[6] != null && !String.IsNullOrWhiteSpace(colunaCSV[6].ToString().Trim()))
            {
                produtoBO.ValorUnitario = Convert.ToDecimal(colunaCSV[6].ToString().Trim().Replace(".", String.Empty));
            }
            else
            {
                produtoBO.ValorUnitario = 0;
            }

            if (colunaCSV[7] != null && !String.IsNullOrWhiteSpace(colunaCSV[7].ToString().Trim()))
            {
                produtoBO.Peso = Convert.ToDecimal(colunaCSV[7].ToString().Trim().Trim().Replace(".", String.Empty));
            }

            if (colunaCSV[10] != null && !String.IsNullOrWhiteSpace(colunaCSV[10].ToString().Trim()))
            {
                produtoBO.TituloImpresso.Titulo.NumeroPaginas = Convert.ToInt32(colunaCSV[10].ToString().Trim());
            }

            if (
                colunaCSV[12] != null
                && !String.IsNullOrWhiteSpace(colunaCSV[12].ToString().Trim())
                && colunaCSV[13] != null
                && !String.IsNullOrWhiteSpace(colunaCSV[13].ToString().Trim())
                )
            {
                TituloAutor tituloAutorBO = new TituloAutor();
                tituloAutorBO.Autor = new Autor();
                tituloAutorBO.Autor.CodigoLegado = colunaCSV[12].ToString().Trim();
                tituloAutorBO.Autor.NomeAutor = colunaCSV[13].ToString().Trim();
                tituloAutorBO.Autor.Email = "autor_importacao@artmed.com.br";

                produtoBO.TituloImpresso.Titulo.TituloAutores.Add(tituloAutorBO);
            }

            if (colunaCSV[14] != null && !String.IsNullOrWhiteSpace(colunaCSV[14].ToString().Trim()))
            {
                Selo seloBO = this.ProcessarSelo(Convert.ToInt32(colunaCSV[14].ToString().Trim()));

                produtoBO.Selos = new List<Selo>();
                produtoBO.Selos.Add(seloBO);
            }

            if (colunaCSV[15] != null && !String.IsNullOrWhiteSpace(colunaCSV[15].ToString().Trim()))
            {
                produtoBO.Isbn13Relacionado = colunaCSV[15].ToString().Trim();
            }
            else
            {
                produtoBO.Isbn13Relacionado = null;
            }

            if (colunaCSV[16] != null && !String.IsNullOrWhiteSpace(colunaCSV[16].ToString().Trim()) && Convert.ToInt32(colunaCSV[16].ToString().Trim()) > 0)
            {
                produtoBO = this.SetarStatus(produtoBO, Convert.ToInt32(colunaCSV[16].Trim()));
            }

            if (colunaCSV[18] != null && !String.IsNullOrWhiteSpace(colunaCSV[18].ToString()))
            {
                try
                {
                    string dataLanc = String.Concat(
                                                    colunaCSV[18].ToString().Substring(6, 2)
                                                    , "/"
                                                    , colunaCSV[18].ToString().Substring(4, 2)
                                                    , "/"
                                                    , colunaCSV[18].ToString().Substring(0, 4)
                                                    );

                    produtoBO.TituloImpresso.Titulo.DataLancamento = Convert.ToDateTime(dataLanc);
                }
                catch { }
            }

            if (colunaCSV[20] != null && !String.IsNullOrWhiteSpace(colunaCSV[20].ToString()))
            {
                var edicao = Convert.ToString(colunaCSV[20]);
                edicao = Regex.Replace(edicao, @"[^0-9]", ""); // remove invalid chars          

                produtoBO.TituloImpresso.Titulo.Edicao = Convert.ToInt32(edicao);
            }

            if (colunaCSV[24] != null && !String.IsNullOrWhiteSpace(colunaCSV[24].ToString()))
            {
                try
                {
                    string dataPub = String.Concat(
                                                    colunaCSV[24].ToString().Substring(6, 2)
                                                    , "/"
                                                    , colunaCSV[24].ToString().Substring(4, 2)
                                                    , "/"
                                                    , colunaCSV[24].ToString().Substring(0, 4)
                                                    );

                    produtoBO.TituloImpresso.Titulo.DataPublicacao = Convert.ToDateTime(dataPub);
                }
                catch { }
            }

            if (colunaCSV[29] != null && !String.IsNullOrWhiteSpace(colunaCSV[29].ToString()))
            {
                //produtoBO.TituloImpresso.Titulo.Formato = Convert.ToString(colunaCSV[29]);
                produtoBO.ProdutoFormato.Formato = Convert.ToString(colunaCSV[29]);
            }

            return produtoBO;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="linhaCSV"></param>
        /// <returns></returns>
        private Produto ProcessarTituloEletronico(string linhaCSV)
        {
            Produto produtoBO = new Produto();
            produtoBO.ProdutoFormato = new ProdutoFormato();
            produtoBO.ProdutoTipo = new ProdutoTipo();
            produtoBO.Conteudo = new Conteudo();
            produtoBO.Conteudo.ConteudoTipo = new ConteudoTipo();
            produtoBO.TituloEletronico = new TituloEletronico();
            produtoBO.TituloEletronico.Titulo = new Titulo();
            produtoBO.TituloEletronico.Titulo.TituloAutores = new List<TituloAutor>();

            produtoBO.ProdutoTipo.ProdutoTipoId = 2;
            produtoBO.Fabricante = new Fabricante();
            produtoBO.Fabricante.FabricanteId = 1;
            produtoBO.ExibirSite = false;
            produtoBO.UtilizaFrete = false;
            produtoBO.TituloEletronico.Titulo.MaisVendido = false;

            string[] colunaCSV = Convert.ToString(linhaCSV).Split(';');

            if (colunaCSV[0] != null && !String.IsNullOrWhiteSpace(colunaCSV[0].ToString().Trim()))
            {
                produtoBO.CodigoProduto = colunaCSV[0].ToString().Trim();
            }

            if (colunaCSV[11] != null && !String.IsNullOrWhiteSpace(colunaCSV[11].ToString().Trim()))
            {
                produtoBO.TituloEletronico.Isbn13 = colunaCSV[11].ToString().Trim();
            }

            if (colunaCSV[1] != null && !String.IsNullOrWhiteSpace(colunaCSV[1].ToString().Trim()))
            {
                produtoBO.NomeProduto = colunaCSV[1].ToString().Trim();
                produtoBO.TituloEletronico.Titulo.NomeTitulo = colunaCSV[1].ToString().Trim();
            }

            if (
                colunaCSV[5] != null
                && !String.IsNullOrWhiteSpace(colunaCSV[5].ToString().Trim())
                && colunaCSV[17] != null
                && !String.IsNullOrWhiteSpace(colunaCSV[17].ToString().Trim())
                )
            {
                produtoBO.CodigoLegadoCategoria = colunaCSV[5].ToString().Trim();
                produtoBO.NomeLegadoCategoria = colunaCSV[17].ToString().Trim();
            }

            if (colunaCSV[6] != null && !String.IsNullOrWhiteSpace(colunaCSV[6].ToString().Trim()))
            {
                produtoBO.ValorUnitario = Convert.ToDecimal(colunaCSV[6].ToString().Trim().Replace(".", String.Empty));
            }

            if (colunaCSV[7] != null && !String.IsNullOrWhiteSpace(colunaCSV[7].ToString().Trim()))
            {
                produtoBO.Peso = Convert.ToDecimal(colunaCSV[7].ToString().Trim().Replace(".", String.Empty));
            }

            //if (colunaCSV[6] != null && !String.IsNullOrWhiteSpace(colunaCSV[6].ToString().Trim()))
            //{
            //    produtoBO.CodigoEAN13 = colunaCSV[6].ToString().Trim();
            //}

            if (colunaCSV[10] != null && !String.IsNullOrWhiteSpace(colunaCSV[10].ToString().Trim()))
            {
                produtoBO.TituloEletronico.Titulo.NumeroPaginas = Convert.ToInt32(colunaCSV[10].ToString().Trim());
            }

            if (
                colunaCSV[12] != null
                && !String.IsNullOrWhiteSpace(colunaCSV[12].ToString().Trim())
                && colunaCSV[13] != null
                && !String.IsNullOrWhiteSpace(colunaCSV[13].ToString().Trim())
                )
            {
                TituloAutor tituloAutorBO = new TituloAutor();
                tituloAutorBO.Autor = new Autor();
                tituloAutorBO.Autor.CodigoLegado = colunaCSV[12].ToString().Trim();
                tituloAutorBO.Autor.NomeAutor = colunaCSV[13].ToString().Trim();
                tituloAutorBO.Autor.Email = "autor_importacao@artmed.com.br";

                produtoBO.TituloEletronico.Titulo.TituloAutores.Add(tituloAutorBO);
            }

            if (colunaCSV[14] != null && !String.IsNullOrWhiteSpace(colunaCSV[14].ToString().Trim()))
            {
                Selo seloBO = this.ProcessarSelo(Convert.ToInt32(colunaCSV[14].ToString().Trim()));

                produtoBO.Selos = new List<Selo>();
                produtoBO.Selos.Add(seloBO);
            }

            if (colunaCSV[15] != null && !String.IsNullOrWhiteSpace(colunaCSV[15].ToString().Trim()))
            {
                produtoBO.Isbn13Relacionado = colunaCSV[15].ToString().Trim();
            }
            else
            {
                produtoBO.Isbn13Relacionado = null;
            }

            if (colunaCSV[16] != null && !String.IsNullOrWhiteSpace(colunaCSV[16].ToString().Trim()))
            {
                produtoBO = this.SetarStatus(produtoBO, Convert.ToInt32(colunaCSV[16].Trim()));
            }

            if (colunaCSV[19] != null && !String.IsNullOrWhiteSpace(colunaCSV[19].ToString()))
            {
                produtoBO.TituloEletronico.Titulo.DataLancamento = Convert.ToDateTime(colunaCSV[19].ToString());
            }

            if (colunaCSV[29] != null && !String.IsNullOrWhiteSpace(colunaCSV[29].ToString()))
            {
                produtoBO.ProdutoFormato.Formato = Convert.ToString(colunaCSV[29]).ToString().Trim();
            }

            return produtoBO;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="linhaCSV"></param>
        /// <returns></returns>
        private Produto ProcessarCapituloImpresso(string linhaCSV)
        {
            Produto produtoBO = new Produto();
            produtoBO.ProdutoFormato = new ProdutoFormato();
            produtoBO.ProdutoTipo = new ProdutoTipo();
            produtoBO.CapituloImpresso = new CapituloImpresso();
            produtoBO.CapituloImpresso.Capitulo = new Capitulo();

            produtoBO.ProdutoTipo.ProdutoTipoId = 3;
            produtoBO.Fabricante = new Fabricante();
            produtoBO.Fabricante.FabricanteId = 1;
            produtoBO.ExibirSite = false;
            produtoBO.UtilizaFrete = true;

            string[] colunaCSV = Convert.ToString(linhaCSV).Split(';');

            if (colunaCSV[0] != null && !String.IsNullOrWhiteSpace(colunaCSV[0].ToString().Trim()))
            {
                produtoBO.CodigoProduto = colunaCSV[0].ToString().Trim();
                produtoBO.CapituloImpresso.Capitulo.CodigoLegado = colunaCSV[0].ToString().Trim();
            }

            if (colunaCSV[1] != null && !String.IsNullOrWhiteSpace(colunaCSV[1].ToString().Trim()))
            {
                produtoBO.NomeProduto = colunaCSV[1].ToString().Trim();
                produtoBO.CapituloImpresso.Capitulo.NomeCapitulo = colunaCSV[1].ToString().Trim();
            }

            if (
                colunaCSV[5] != null
                && !String.IsNullOrWhiteSpace(colunaCSV[5].ToString().Trim())
                && colunaCSV[17] != null
                && !String.IsNullOrWhiteSpace(colunaCSV[17].ToString().Trim())
                )
            {
                produtoBO.CodigoLegadoCategoria = colunaCSV[5].ToString().Trim();
                produtoBO.NomeLegadoCategoria = colunaCSV[17].ToString().Trim();
            }

            if (colunaCSV[6] != null && !String.IsNullOrWhiteSpace(colunaCSV[6].ToString().Trim()))
            {
                produtoBO.ValorUnitario = Convert.ToDecimal(colunaCSV[6].ToString().Trim().Replace(".", String.Empty));
            }

            if (colunaCSV[7] != null && !String.IsNullOrWhiteSpace(colunaCSV[7].ToString().Trim()))
            {
                produtoBO.Peso = Convert.ToDecimal(colunaCSV[7].ToString().Trim().Trim().Replace(".", String.Empty));
            }

            if (colunaCSV[10] != null && !String.IsNullOrWhiteSpace(colunaCSV[10].ToString().Trim()))
            {
                produtoBO.CapituloImpresso.Capitulo.NumeroPaginaCapitulo = Convert.ToInt32(colunaCSV[10].ToString().Trim());
            }

            if (colunaCSV[15] != null && !String.IsNullOrWhiteSpace(colunaCSV[15].ToString().Trim()))
            {
                produtoBO.Isbn13Relacionado = colunaCSV[15].ToString().Trim();
            }
            else
            {
                produtoBO.Isbn13Relacionado = null;
            }

            if (colunaCSV[16] != null && !String.IsNullOrWhiteSpace(colunaCSV[16].ToString().Trim()))
            {
                produtoBO = this.SetarStatus(produtoBO, Convert.ToInt32(colunaCSV[16].Trim()));
            }

            if (colunaCSV[29] != null && !String.IsNullOrWhiteSpace(colunaCSV[29].ToString()))
            {
                produtoBO.ProdutoFormato.Formato = Convert.ToString(colunaCSV[29]);
            }

            return produtoBO;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="linhaCSV"></param>
        /// <returns></returns>
        private Produto ProcessarCapituloEletronico(string linhaCSV)
        {
            Produto produtoBO = new Produto();
            produtoBO.ProdutoFormato = new ProdutoFormato();
            produtoBO.ProdutoTipo = new ProdutoTipo();
            produtoBO.CapituloEletronico = new CapituloEletronico();
            produtoBO.CapituloEletronico.Capitulo = new Capitulo();

            produtoBO.ProdutoTipo.ProdutoTipoId = 4;
            produtoBO.Fabricante = new Fabricante();
            produtoBO.Fabricante.FabricanteId = 1;
            produtoBO.ExibirSite = false;
            produtoBO.UtilizaFrete = true;

            string[] colunaCSV = Convert.ToString(linhaCSV).Split(';');

            if (colunaCSV[0] != null && !String.IsNullOrWhiteSpace(colunaCSV[0].ToString().Trim()))
            {
                produtoBO.CodigoProduto = colunaCSV[0].ToString().Trim();
                produtoBO.CapituloEletronico.Capitulo.CodigoLegado = colunaCSV[0].ToString().Trim();
            }

            if (colunaCSV[1] != null && !String.IsNullOrWhiteSpace(colunaCSV[1].ToString().Trim()))
            {
                produtoBO.NomeProduto = colunaCSV[1].ToString().Trim();
                produtoBO.CapituloEletronico.Capitulo.NomeCapitulo = colunaCSV[1].ToString().Trim();
            }

            if (
                colunaCSV[5] != null
                && !String.IsNullOrWhiteSpace(colunaCSV[5].ToString().Trim())
                && colunaCSV[17] != null
                && !String.IsNullOrWhiteSpace(colunaCSV[17].ToString().Trim())
                )
            {
                produtoBO.CodigoLegadoCategoria = colunaCSV[5].ToString().Trim();
                produtoBO.NomeLegadoCategoria = colunaCSV[17].ToString().Trim();
            }

            if (colunaCSV[6] != null && !String.IsNullOrWhiteSpace(colunaCSV[6].ToString().Trim()))
            {
                produtoBO.ValorUnitario = Convert.ToDecimal(colunaCSV[6].ToString().Trim().Replace(".", String.Empty));
            }

            if (colunaCSV[7] != null && !String.IsNullOrWhiteSpace(colunaCSV[7].ToString().Trim()))
            {
                produtoBO.Peso = Convert.ToDecimal(colunaCSV[7].ToString().Trim().Trim().Replace(".", String.Empty));
            }

            if (colunaCSV[10] != null && !String.IsNullOrWhiteSpace(colunaCSV[10].ToString().Trim()))
            {
                produtoBO.CapituloEletronico.Capitulo.NumeroPaginaCapitulo = Convert.ToInt32(colunaCSV[10].ToString().Trim());
            }

            if (colunaCSV[15] != null && !String.IsNullOrWhiteSpace(colunaCSV[15].ToString().Trim()))
            {
                produtoBO.Isbn13Relacionado = colunaCSV[15].ToString().Trim();
            }
            else
            {
                produtoBO.Isbn13Relacionado = null;
            }

            if (colunaCSV[16] != null && !String.IsNullOrWhiteSpace(colunaCSV[16].ToString().Trim()))
            {
                produtoBO = this.SetarStatus(produtoBO, Convert.ToInt32(colunaCSV[16].Trim()));
            }

            if (colunaCSV[29] != null && !String.IsNullOrWhiteSpace(colunaCSV[29].ToString()))
            {
                produtoBO.ProdutoFormato.Formato = Convert.ToString(colunaCSV[29]);
            }

            return produtoBO;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="linhaCSV"></param>
        /// <returns></returns>
        private Produto ProcessarRevistaAssinatura(string linhaCSV)
        {
            Produto produtoBO = new Produto();
            produtoBO.ProdutoTipo = new ProdutoTipo();
            produtoBO.RevistaAssinatura = new RevistaAssinatura();
            produtoBO.RevistaAssinatura.Revista = new Revista();

            produtoBO.ProdutoTipo.ProdutoTipoId = 7;
            produtoBO.Fabricante = new Fabricante();
            produtoBO.Fabricante.FabricanteId = 1;
            produtoBO.ExibirSite = false;
            produtoBO.UtilizaFrete = true;
            produtoBO.RevistaAssinatura.Revista.Periodicidade = 0;
            produtoBO.RevistaAssinatura.Revista.DescricaoRevista = "";

            string[] colunaCSV = Convert.ToString(linhaCSV).Split(';');

            if (colunaCSV[0] != null && !String.IsNullOrWhiteSpace(colunaCSV[0].ToString().Trim()))
            {
                produtoBO.CodigoProduto = colunaCSV[0].ToString().Trim();
            }

            if (colunaCSV[1] != null && !String.IsNullOrWhiteSpace(colunaCSV[1].ToString().Trim()))
            {
                produtoBO.NomeProduto = colunaCSV[1].ToString().Trim();
                produtoBO.RevistaAssinatura.Revista.NomeRevista = colunaCSV[1].ToString().Trim();
            }

            if (
                colunaCSV[5] != null
                && !String.IsNullOrWhiteSpace(colunaCSV[5].ToString().Trim())
                && colunaCSV[17] != null
                && !String.IsNullOrWhiteSpace(colunaCSV[17].ToString().Trim())
                )
            {
                produtoBO.CodigoLegadoCategoria = colunaCSV[5].ToString().Trim();
                produtoBO.NomeLegadoCategoria = colunaCSV[17].ToString().Trim();
            }

            if (colunaCSV[6] != null && !String.IsNullOrWhiteSpace(colunaCSV[6].ToString().Trim()))
            {
                produtoBO.ValorUnitario = Convert.ToDecimal(colunaCSV[6].ToString().Trim().Replace(".", String.Empty));
            }

            if (colunaCSV[7] != null && !String.IsNullOrWhiteSpace(colunaCSV[7].ToString().Trim()))
            {
                produtoBO.Peso = Convert.ToDecimal(colunaCSV[7].ToString().Trim().Trim().Replace(".", String.Empty));
            }

            if (colunaCSV[14] != null && !String.IsNullOrWhiteSpace(colunaCSV[14].ToString().Trim()))
            {
                Selo seloBO = this.ProcessarSelo(Convert.ToInt32(colunaCSV[14].ToString().Trim()));

                produtoBO.Selos = new List<Selo>();
                produtoBO.Selos.Add(seloBO);
            }

            if (colunaCSV[16] != null && !String.IsNullOrWhiteSpace(colunaCSV[16].ToString().Trim()))
            {
                produtoBO = this.SetarStatus(produtoBO, Convert.ToInt32(colunaCSV[16].Trim()));
            }

            if (colunaCSV[19] != null && !String.IsNullOrWhiteSpace(colunaCSV[19].ToString()))
            {
                produtoBO.RevistaAssinatura.Revista.ISSN = colunaCSV[19].ToString();
            }

            if (colunaCSV[21] != null && !String.IsNullOrWhiteSpace(colunaCSV[21].ToString()))
            {
                produtoBO.RevistaAssinatura.NumeroExemplares = Convert.ToInt32(colunaCSV[21].ToString());
            }

            return produtoBO;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="linhaCSV"></param>
        /// <returns></returns>
        private Produto ProcessarRevistaEdicao(string linhaCSV)
        {
            Produto produtoBO = new Produto();
            produtoBO.ProdutoTipo = new ProdutoTipo();
            produtoBO.RevistaEdicao = new RevistaEdicao();
            produtoBO.RevistaEdicao.Revista = new Revista();

            produtoBO.ProdutoTipo.ProdutoTipoId = 5;
            produtoBO.Fabricante = new Fabricante();
            produtoBO.Fabricante.FabricanteId = 1;
            produtoBO.ExibirSite = false;
            produtoBO.UtilizaFrete = true;
            produtoBO.RevistaEdicao.Ativo = false;
            produtoBO.RevistaEdicao.Revista.Periodicidade = 0;
            produtoBO.RevistaEdicao.Revista.DescricaoRevista = "";

            string[] colunaCSV = Convert.ToString(linhaCSV).Split(';');

            if (colunaCSV[0] != null && !String.IsNullOrWhiteSpace(colunaCSV[0].ToString().Trim()))
            {
                produtoBO.CodigoProduto = colunaCSV[0].ToString().Trim();
            }

            if (colunaCSV[1] != null && !String.IsNullOrWhiteSpace(colunaCSV[1].ToString().Trim()))
            {
                produtoBO.NomeProduto = colunaCSV[1].ToString().Trim();
                produtoBO.RevistaEdicao.Revista.NomeRevista = colunaCSV[1].ToString().Trim();
            }

            if (
                colunaCSV[5] != null
                && !String.IsNullOrWhiteSpace(colunaCSV[5].ToString().Trim())
                && colunaCSV[17] != null
                && !String.IsNullOrWhiteSpace(colunaCSV[17].ToString().Trim())
                )
            {
                produtoBO.CodigoLegadoCategoria = colunaCSV[5].ToString().Trim();
                produtoBO.NomeLegadoCategoria = colunaCSV[17].ToString().Trim();
            }

            if (colunaCSV[6] != null && !String.IsNullOrWhiteSpace(colunaCSV[6].ToString().Trim()))
            {
                produtoBO.ValorUnitario = Convert.ToDecimal(colunaCSV[6].ToString().Trim().Replace(".", String.Empty));
            }

            if (colunaCSV[7] != null && !String.IsNullOrWhiteSpace(colunaCSV[7].ToString().Trim()))
            {
                produtoBO.Peso = Convert.ToDecimal(colunaCSV[7].ToString().Trim().Trim().Replace(".", String.Empty));
            }

            if (colunaCSV[10] != null && !String.IsNullOrWhiteSpace(colunaCSV[10].ToString().Trim()))
            {
                produtoBO.RevistaEdicao.NumeroPaginas = Convert.ToInt32(colunaCSV[10].ToString().Trim());
            }

            if (colunaCSV[14] != null && !String.IsNullOrWhiteSpace(colunaCSV[14].ToString().Trim()))
            {
                Selo seloBO = this.ProcessarSelo(Convert.ToInt32(colunaCSV[14].ToString().Trim()));

                produtoBO.Selos = new List<Selo>();
                produtoBO.Selos.Add(seloBO);
            }

            if (colunaCSV[16] != null && !String.IsNullOrWhiteSpace(colunaCSV[16].ToString().Trim()) && Convert.ToInt32(colunaCSV[16].ToString().Trim()) > 0)
            {
                produtoBO = this.SetarStatus(produtoBO, Convert.ToInt32(colunaCSV[16].Trim()));
            }

            if (colunaCSV[19] != null && !String.IsNullOrWhiteSpace(colunaCSV[19].ToString()))
            {
                produtoBO.RevistaEdicao.Revista.ISSN = colunaCSV[19].ToString();
            }

            if (colunaCSV[20] != null && !String.IsNullOrWhiteSpace(colunaCSV[20].ToString()))
            {
                produtoBO.RevistaEdicao.NumeroEdicao = Convert.ToInt32(colunaCSV[20].ToString());
            }

            if (colunaCSV[24] != null && !String.IsNullOrWhiteSpace(colunaCSV[24].ToString()))
            {
                try
                {
                    string dataPub = String.Concat(
                                                    colunaCSV[24].ToString().Substring(6, 2)
                                                    , "/"
                                                    , colunaCSV[24].ToString().Substring(4, 2)
                                                    , "/"
                                                    , colunaCSV[24].ToString().Substring(0, 4)
                                                    );

                    DateTime dataPublicacao = Convert.ToDateTime(dataPub);
                    produtoBO.RevistaEdicao.AnoPublicacao = Convert.ToInt32(dataPublicacao.ToString("yyyy"));
                    produtoBO.RevistaEdicao.MesPublicacao = Convert.ToInt32(dataPublicacao.ToString("MM"));
                    produtoBO.RevistaEdicao.PeriodoPublicacao = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dataPublicacao.ToString("MMMM"));
                    //produtoBO.RevistaEdicao.AnoEdicao = dataPublicacao.ToString("yyyy");
                }
                catch { }
            }

            return produtoBO;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="linhaCSV"></param>
        /// <returns></returns>
        private Produto ProcessaTituloAluguel(string linhaCSV)
        {
            Produto produtoBO = new Produto();
            produtoBO.ProdutoFormato = new ProdutoFormato();
            produtoBO.ProdutoTipo = new ProdutoTipo();
            produtoBO.Conteudo = new Conteudo();
            produtoBO.Conteudo.ConteudoTipo = new ConteudoTipo();
            produtoBO.TituloEletronico = new TituloEletronico();
            produtoBO.TituloEletronico.TituloEletronicoAlugueis = new List<TituloEletronicoAluguel>();

            produtoBO.ProdutoTipo.ProdutoTipoId = 6;
            produtoBO.Fabricante = new Fabricante();
            produtoBO.Fabricante.FabricanteId = 1;
            produtoBO.ExibirSite = false;
            produtoBO.UtilizaFrete = false;

            string[] colunaCSV = Convert.ToString(linhaCSV).Split(';');

            if (colunaCSV[0] != null && !String.IsNullOrWhiteSpace(colunaCSV[0].ToString().Trim()))
            {
                produtoBO.CodigoProduto = colunaCSV[0].ToString().Trim();
                produtoBO.TituloEletronico.Isbn13 = colunaCSV[0].ToString().Trim();
            }

            if (colunaCSV[1] != null && !String.IsNullOrWhiteSpace(colunaCSV[1].ToString().Trim()))
            {
                produtoBO.NomeProduto = colunaCSV[1].ToString().Trim();
            }

            if (
                colunaCSV[5] != null
                && !String.IsNullOrWhiteSpace(colunaCSV[5].ToString().Trim())
                && colunaCSV[17] != null
                && !String.IsNullOrWhiteSpace(colunaCSV[17].ToString().Trim())
                )
            {
                produtoBO.CodigoLegadoCategoria = colunaCSV[5].ToString().Trim();
                produtoBO.NomeLegadoCategoria = colunaCSV[17].ToString().Trim();
            }

            if (colunaCSV[6] != null && !String.IsNullOrWhiteSpace(colunaCSV[6].ToString().Trim()))
            {
                produtoBO.ValorUnitario = Convert.ToDecimal(colunaCSV[6].ToString().Trim().Replace(".", String.Empty));
            }

            if (colunaCSV[7] != null && !String.IsNullOrWhiteSpace(colunaCSV[7].ToString().Trim()))
            {
                produtoBO.Peso = Convert.ToDecimal(colunaCSV[7].ToString().Trim().Replace(".", String.Empty));
            }

            if (colunaCSV[16] != null && !String.IsNullOrWhiteSpace(colunaCSV[16].ToString().Trim()))
            {
                produtoBO = this.SetarStatus(produtoBO, Convert.ToInt32(colunaCSV[16].Trim()));
            }

            if (colunaCSV[22] != null && !String.IsNullOrWhiteSpace(colunaCSV[22].ToString()))
            {
                TituloEletronicoAluguel tituloEletronicoAluguelBO = new TituloEletronicoAluguel();
                tituloEletronicoAluguelBO.TempoAluguel = Convert.ToInt32(colunaCSV[22].ToString());

                produtoBO.TituloEletronico.TituloEletronicoAlugueis.Add(tituloEletronicoAluguelBO);
            }

            if (colunaCSV[29] != null && !String.IsNullOrWhiteSpace(colunaCSV[29].ToString()))
            {
                produtoBO.ProdutoFormato.Formato = Convert.ToString(colunaCSV[29]);
            }

            return produtoBO;
        }

        #endregion

        #region [ Importar Produto ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtoBOList"></param>
        private void ProcessarProduto(List<Produto> produtoBOList)
        {
            objLogHelper.WriteOnFile("PP1", "ProcessarProduto - Inicio");

            if (produtoBOList != null && produtoBOList.Count > 0)
            {

                List<Produto> produtoBOListTemp = new List<Produto>();

                /* IMPORTANTE
                 * Não alterar a ordem de consulta por LINQ
                 * IMPORTANTE
                 */

                // Consulta 1
                // Importa os produto TituloImpresso
                produtoBOListTemp = (from Produto produtoBOTemp in produtoBOList
                                     where produtoBOTemp.ProdutoTipo != null && produtoBOTemp.ProdutoTipo.ProdutoTipoId == 1
                                     select produtoBOTemp).ToList();

                objLogHelper.WriteOnFile("PP2", "ProcessarProduto - Filtro Tipo 1");

                if (produtoBOListTemp != null && produtoBOListTemp.Any())
                {
                    this.ImportarProduto(produtoBOListTemp);
                }

                // Consulta 2
                // Importa os produto TituloEletronico
                produtoBOListTemp = (from Produto produtoBOTemp in produtoBOList
                                     where produtoBOTemp.ProdutoTipo != null && produtoBOTemp.ProdutoTipo.ProdutoTipoId == 2
                                     select produtoBOTemp).ToList();

                objLogHelper.WriteOnFile("PP3", "ProcessarProduto - Filtro Tipo 2");

                if (produtoBOListTemp != null && produtoBOListTemp.Any())
                {
                    this.ImportarProduto(produtoBOListTemp);
                }

                // Consulta 3
                // Importa os produto CapituloImpresso
                produtoBOListTemp = (from Produto produtoBOTemp in produtoBOList
                                     where produtoBOTemp.ProdutoTipo != null && produtoBOTemp.ProdutoTipo.ProdutoTipoId == 3
                                     select produtoBOTemp).ToList();

                objLogHelper.WriteOnFile("PP4", "ProcessarProduto - Filtro Tipo 3");

                if (produtoBOListTemp != null && produtoBOListTemp.Any())
                {
                    this.ImportarProduto(produtoBOListTemp);
                }

                // Consulta 4
                // Importa os produto CapituloEletronico
                produtoBOListTemp = (from Produto produtoBOTemp in produtoBOList
                                     where produtoBOTemp.ProdutoTipo != null && produtoBOTemp.ProdutoTipo.ProdutoTipoId == 4
                                     select produtoBOTemp).ToList();

                objLogHelper.WriteOnFile("PP5", "ProcessarProduto - Filtro Tipo 4");

                if (produtoBOListTemp != null && produtoBOListTemp.Any())
                {
                    this.ImportarProduto(produtoBOListTemp);
                }

                // Consulta 5
                // Importa os produto TituloAluguel
                produtoBOListTemp = (from Produto produtoBOTemp in produtoBOList
                                     where produtoBOTemp.ProdutoTipo != null && produtoBOTemp.ProdutoTipo.ProdutoTipoId == 6
                                     select produtoBOTemp).ToList();

                objLogHelper.WriteOnFile("PP6", "ProcessarProduto - Filtro Tipo 5");

                if (produtoBOListTemp != null && produtoBOListTemp.Any())
                {
                    this.ImportarProduto(produtoBOListTemp);
                }

                // Consulta 6
                // Importa os produto RevistaAssinatura
                produtoBOListTemp = (from Produto produtoBOTemp in produtoBOList
                                     where produtoBOTemp.ProdutoTipo != null && produtoBOTemp.ProdutoTipo.ProdutoTipoId == 7
                                     select produtoBOTemp).ToList();

                objLogHelper.WriteOnFile("PP7", "ProcessarProduto - Filtro Tipo 6");

                if (produtoBOListTemp != null && produtoBOListTemp.Any())
                {
                    this.ImportarProduto(produtoBOListTemp);
                }

                // Consulta 7
                // Importa os produto RevistaEdicao
                produtoBOListTemp = (from Produto produtoBOTemp in produtoBOList
                                     where produtoBOTemp.ProdutoTipo != null && produtoBOTemp.ProdutoTipo.ProdutoTipoId == 5
                                     select produtoBOTemp).ToList();

                objLogHelper.WriteOnFile("PP8", "ProcessarProduto - Filtro Tipo 7");

                if (produtoBOListTemp != null && produtoBOListTemp.Any())
                {
                    this.ImportarProduto(produtoBOListTemp);
                }
            }
            else
            {
                objLogHelper.WriteOnFile("PP9", "ProcessarProduto - Erro", "produtoBOList é nulo ou não contém elementos");
            }

            objLogHelper.WriteOnFile("PP1", "ProcessarProduto - Término");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtoBOList"></param>
        private void ImportarProduto(List<Produto> produtoBOList)
        {
            objLogHelper.WriteOnFile("IP1", "ImportarProduto - Inicio", "Total de Produtos: " + produtoBOList.Count.ToString());

            foreach (Produto produtoBOTemp in produtoBOList)
            {
                try
                {
                    if (produtoBOTemp != null && produtoBOTemp.ProdutoTipo != null && produtoBOTemp.ProdutoTipo.ProdutoTipoId > 0)
                    {

                        objLogHelper.WriteOnFile("IP2", "ImportarProduto", "Codigo: [" + produtoBOTemp.CodigoProduto + "]  - ProdutoTipo: " + produtoBOTemp.ProdutoTipo.ProdutoTipoId.ToString());

                        switch (produtoBOTemp.ProdutoTipo.ProdutoTipoId)
                        {
                            case 1: // Título Impresso
                                this.ImportarTituloImpresso(produtoBOTemp);
                                break;
                            case 2: // Título Eletrônico
                                this.ImportarTituloEletronico(produtoBOTemp);
                                break;
                            case 3: // Capítulo Impresso
                                this.ImportarCapituloImpresso(produtoBOTemp);
                                break;
                            case 4: // Capítulo Eletrônico
                                this.ImportarCapituloEletronico(produtoBOTemp);
                                break;
                            case 5: // Revista Edição
                                this.ImportarRevistaEdicao(produtoBOTemp);
                                break;
                            case 6: // Titulo Eletronico Aluguel
                                this.ImportarTituloEletronicoAluguel(produtoBOTemp);
                                break;
                            case 7: // Revista Assinatura
                                this.ImportarRevistaAssinatura(produtoBOTemp);
                                break;
                            default:
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    objLogHelper.WriteOnFile("IP3", "ImportarProduto - Erro", ex.ToString());
                }
            }

            objLogHelper.WriteOnFile("IP4", "ImportarProduto - Término");
        }

        /// <summary>
        /// Verifica se o Produto e Selo já estão relacionados, se não estiver, faz o relacionamento
        /// se tiver, é excluido o relacionamento
        /// </summary>
        /// <param name="seloBO"></param>
        /// <param name="produtoBO"></param>
        private void RelacionarProdutoSelo(Selo seloBO, Produto produtoBO)
        {
            bool seloUnico = true;

            if (seloUnico)
            {
                new ProdutoBLL().ExcluirProdutoSelo(produtoBO);
            }

            bool seloRelacionado = new SeloBLL().SeloProdutoRelacionado(seloBO, produtoBO);

            if (!seloRelacionado)
            {
                new ProdutoBLL().RelacionarProdutoSelo(produtoBO, seloBO);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramProdutoFormatoBO"></param>
        private void ImportarProdutoFormato(ProdutoFormato paramProdutoFormatoBO)
        {
            ProdutoFormato produtoFormatoBO = new ProdutoFormato();
            produtoFormatoBO = new ProdutoFormatoBLL().Carregar(paramProdutoFormatoBO);

            if (paramProdutoFormatoBO.ProdutoId > 0)
            {
                if (produtoFormatoBO == null)
                {
                    new ProdutoFormatoBLL().Inserir(paramProdutoFormatoBO);
                }
                else
                {
                    new ProdutoFormatoBLL().Atualizar(paramProdutoFormatoBO);
                }
            }
        }

        /// <summary>
        /// Verifica se o Autor já está cadastrado, consultando pelo código legado, se não estiver, cadastra Autor
        /// </summary>
        /// <param name="paramAutorBO"></param>
        /// <returns></returns>
        private Autor ImportarAutor(Autor paramAutorBO)
        {
            Autor autorBO = new Autor();
            autorBO = new AutorBLL().CarregarAutorCodigoLegado(paramAutorBO);

            if (autorBO == null)
            {
                autorBO = new AutorBLL().InserirNovoAutor(paramAutorBO);
            }
            else if (autorBO != null && autorBO.AutorId > 0)
            {
                autorBO.Url = paramAutorBO.Url;
                autorBO.Email = paramAutorBO.Email;
                autorBO.Blog = paramAutorBO.Blog;
                autorBO.NomeAutor = paramAutorBO.NomeAutor;

                new AutorBLL().AtualizarAutor(autorBO);
            }

            return autorBO;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produto"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        private Produto SetarStatus(Produto produto, int status)
        {
            //####################################
            // Código | Disponível | Exibir Site #
            //####################################
            //   1    |     N      |      S      #
            //   2    |     N      |      S      #
            //   3    |     N      |      N      #
            //   4    |     N      |      N      #
            //   5    |     N      |      S      #
            //   6    |     N      |      N      #
            //   8    |     S      |      S      #
            //   9    |     S      |      S      #
            //####################################

            switch (status)
            {
                case 1: // Falta - Sem Previsão
                    produto.Disponivel = false;
                    produto.ExibirSite = true;
                    break;
                case 2: // Falta - Em reimpressão
                    produto.Disponivel = false;
                    produto.ExibirSite = true;
                    break;
                case 3: // Esgotado
                    produto.Disponivel = false;
                    produto.ExibirSite = false;
                    break;
                case 4: // Fora de Catálogo
                    produto.Disponivel = false;
                    produto.ExibirSite = false;
                    break;
                case 5: // Produção - Nova Edição
                    produto.Disponivel = false;
                    produto.ExibirSite = true;
                    break;
                case 6: // Produção - Em Lançamento
                    produto.Disponivel = false;
                    produto.ExibirSite = false;
                    break;
                case 8: // Estoque Exclusivo do Site
                case 9: // Disponível
                    produto.Disponivel = true;
                    produto.ExibirSite = true;
                    break;
                default:
                    produto.Disponivel = false;
                    produto.ExibirSite = false;
                    break;
            }

            if (produto.ValorUnitario == 0)
            {
                produto.Disponivel = false;
                produto.ExibirSite = false;
            }

            return produto;
        }

        #region [ Categoria ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramCategoriaBO"></param>
        /// <returns></returns>
        private Categoria ImportarCategoria(Categoria paramCategoriaBO)
        {
            Categoria categoriaBO = new Categoria();
            categoriaBO = new CategoriaBLL().CarregarPorCodigoLegado(paramCategoriaBO);

            if (categoriaBO == null)
            {
                List<Produto> produtoBOListTemp = new List<Produto>();

                produtoBOListTemp = (from Produto produtoBOTemp in produtoBOList
                                     //where produtoBOTemp.Categorias != null && produtoBOTemp.Categorias.Where(c => c.CodigoCategoria == "01.04").Count() > 0
                                     where produtoBOTemp.CodigoLegadoCategoria == paramCategoriaBO.CodigoCategoria
                                     select produtoBOTemp).ToList();

                if (produtoBOListTemp != null && produtoBOListTemp.Any() && !String.IsNullOrEmpty(produtoBOListTemp[0].CodigoLegadoCategoria))
                {
                    categoriaBO = new Categoria();
                    categoriaBO.NomeCategoria = produtoBOListTemp[0].NomeLegadoCategoria;
                    categoriaBO.CodigoCategoria = produtoBOListTemp[0].CodigoLegadoCategoria;

                    if (paramCategoriaBO.CategoriaPai != null && paramCategoriaBO.CategoriaPai.CategoriaId != null)
                        categoriaBO.CategoriaPai = paramCategoriaBO.CategoriaPai;

                    new CategoriaBLL().Inserir(categoriaBO);
                }
                else
                {
                    new CategoriaBLL().Inserir(paramCategoriaBO);
                }
            }
            else
            {
                /* Não é para atualizar, pois o Banco de Dados Legado não tem configuração
                 * para caracteres especiais.
                 * As categorias serão somente importados, sofrendo atualização depois pelo
                 * Manager.
                 */
            }

            return categoriaBO;
        }

        /// <summary>
        /// Importa e relaciona as categorias de pai para filho, devolvendo a ultima categoria como filha
        /// </summary>
        /// <param name="categoriaBOList"></param>
        private Categoria RelacionarCategoria(List<Categoria> categoriaBOList)
        {
            Categoria categoriaBO = new Categoria();

            foreach (Categoria categoriaBOTemp in categoriaBOList)
            {
                if (categoriaBO != null)
                {
                    if (categoriaBO.CategoriaId > 0)
                    {
                        categoriaBOTemp.CategoriaPai = new Categoria();
                        categoriaBOTemp.CategoriaPai.CategoriaId = categoriaBO.CategoriaId;
                    }

                    categoriaBO = this.ImportarCategoria(categoriaBOTemp);
                }
            }

            return categoriaBO;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtoBO"></param>
        /// <param name="categoriaBO"></param>
        private void RelacionarProdutoCategoria(Produto produtoBO, Categoria categoriaBO)
        {
            bool categoriaUnica = true;

            if (categoriaUnica)
            {
                new ProdutoBLL().ExcluirProdutoCategoria(produtoBO);
            }

            bool categoriaRelacionada = new ProdutoBLL().ProdutoCategoriaRelacionado(produtoBO, categoriaBO);

            if (!categoriaRelacionada)
            {
                new ProdutoBLL().RelacionarProdutoCategoria(produtoBO, categoriaBO);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtoBO"></param>
        private void ImportarProdutoCategoria(Produto produtoBO)
        {
            string[] codLegadoCategStrList = produtoBO.CodigoLegadoCategoria.Split('.');

            if (
                codLegadoCategStrList != null
                && codLegadoCategStrList.Any()
                && !String.IsNullOrEmpty(produtoBO.NomeLegadoCategoria)
                )
            {
                List<Categoria> categoriaBOList = new List<Categoria>();

                for (int i = 0; i < codLegadoCategStrList.Count(); i++)
                {
                    string codLegado = null;

                    Categoria categoriaBOTemp = new Categoria();
                    categoriaBOTemp.NomeCategoria = i == (codLegadoCategStrList.Count() - 1) ? produtoBO.NomeLegadoCategoria : null;
                    //categoriaBOTemp.NomeCategoria = produtoBO.NomeLegadoCategoria;

                    for (int x = 0; x <= i; x++)
                    {
                        codLegado = String.Concat(String.IsNullOrEmpty(codLegado) ? codLegado : String.Concat(codLegado, "."), codLegadoCategStrList[x].ToString());
                    }

                    if (codLegado.Length == 2)
                    {
                        codLegado = String.Concat(codLegado, ".00.00");
                    }
                    else if (codLegado.Length == 5)
                    {
                        codLegado = String.Concat(codLegado, ".00");
                    }

                    categoriaBOTemp.CodigoCategoria = codLegado;

                    categoriaBOList.Add(categoriaBOTemp);
                }

                if (categoriaBOList != null && categoriaBOList.Any())
                {
                    Categoria categoriaBO = this.RelacionarCategoria(categoriaBOList);

                    if (categoriaBO != null && categoriaBO.CategoriaId > 0)
                    {
                        this.RelacionarProdutoCategoria(produtoBO, categoriaBO);
                    }
                }
            }
        }

        #endregion

        #region [ Titulo ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tituloBO"></param>
        /// <returns></returns>
        private Titulo InserirConteudoTitulo(Titulo tituloBO)
        {
            // Insere Titulo
            Conteudo conteudoTituloBO = new Conteudo();
            conteudoTituloBO.ConteudoTipo = new ConteudoTipo();
            conteudoTituloBO.ConteudoTipo.ConteudoTipoId = 14;
            conteudoTituloBO.DataHoraCadastro = DateTime.Now;

            new ConteudoBLL().InserirConteudo(conteudoTituloBO);

            if (conteudoTituloBO != null && conteudoTituloBO.ConteudoId > 0)
            {
                tituloBO.TituloId = conteudoTituloBO.ConteudoId;

                new TituloBLL().Inserir(tituloBO);
            }

            return tituloBO;
        }

        /// <summary>
        /// Verifica se Autor e o Titulo já estão relacionados, se não estiverem, faz o relacionamento
        /// </summary>
        /// <param name="tituloBO"></param>
        /// <param name="autorBO"></param>
        private void RelacionarTituloAutor(Titulo tituloBO, Autor autorBO)
        {
            autorBO = this.ImportarAutor(autorBO);

            bool autorRelacionado = new TituloBLL().TituloAutorRelacionado(tituloBO, autorBO);

            if (!autorRelacionado)
            {
                TituloAutor tituloAutorBO = new TituloAutor();
                tituloAutorBO.Titulo = new Titulo();
                tituloAutorBO.Titulo.TituloId = tituloBO.TituloId;
                tituloAutorBO.Autor = new Autor();
                tituloAutorBO.Autor.AutorId = autorBO.AutorId;

                List<TituloAutor> tituloAutorBOList = new List<TituloAutor>();
                tituloAutorBOList.Add(tituloAutorBO);

                new TituloBLL().InserirTituloAutor(tituloAutorBOList);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtoBO"></param>
        private void ImportarTituloImpresso(Produto produtoBO)
        {
            if (produtoBO.TituloImpresso != null && !String.IsNullOrEmpty(produtoBO.TituloImpresso.Isbn13))
            {
                TituloImpresso tituloImpressoBO = new TituloImpresso();
                tituloImpressoBO = new TituloImpressoBLL().CarregarPorIsbn13(produtoBO.TituloImpresso.Isbn13);

                if (tituloImpressoBO == null) // Insere Produto, Titulo e TituloImpresso
                {
                    // Insere Produto
                    Conteudo conteudoProdutoBO = new Conteudo();
                    conteudoProdutoBO.ConteudoTipo = new ConteudoTipo();
                    conteudoProdutoBO.ConteudoTipo.ConteudoTipoId = 1;
                    conteudoProdutoBO.DataHoraCadastro = DateTime.Now;

                    new ConteudoBLL().InserirConteudo(conteudoProdutoBO);

                    if (conteudoProdutoBO != null && conteudoProdutoBO.ConteudoId > 0)
                    {
                        produtoBO.ProdutoId = conteudoProdutoBO.ConteudoId;

                        new ProdutoBLL().Inserir(produtoBO);
                    }

                    // Insere Titulo
                    produtoBO.TituloImpresso.Titulo = this.InserirConteudoTitulo(produtoBO.TituloImpresso.Titulo);

                    // Insere TituloImpresso
                    if (produtoBO != null && produtoBO.ProdutoId > 0 && produtoBO.TituloImpresso.Titulo != null && produtoBO.TituloImpresso.Titulo.TituloId > 0)
                    {
                        produtoBO.TituloImpresso.TituloImpressoId = produtoBO.ProdutoId;

                        new TituloImpressoBLL().Inserir(produtoBO.TituloImpresso);
                    }
                }
                else if (tituloImpressoBO.TituloImpressoId > 0 && tituloImpressoBO.Titulo != null && tituloImpressoBO.Titulo.TituloId > 0) // Atualiza Produto e Titulo
                {
                    produtoBO.ProdutoId = tituloImpressoBO.TituloImpressoId;

                    new ProdutoBLL().AtualizarMenosNome(produtoBO);

                    produtoBO.TituloImpresso.Titulo.TituloId = tituloImpressoBO.Titulo.TituloId;

                    new TituloBLL().AtualizarMenosNomeSubtitulo(produtoBO.TituloImpresso.Titulo);
                }

                // Produto Formato
                if (produtoBO != null && produtoBO.ProdutoId > 0 && produtoBO.ProdutoFormato != null && !String.IsNullOrEmpty(produtoBO.ProdutoFormato.Formato))
                {
                    produtoBO.ProdutoFormato.ProdutoId = produtoBO.ProdutoId;

                    this.ImportarProdutoFormato(produtoBO.ProdutoFormato);
                }

                // Selo
                if (produtoBO.Selos != null && produtoBO.Selos.Any() && produtoBO.Selos[0] != null && produtoBO.Selos[0].SeloId > 0) // Selo
                {
                    this.RelacionarProdutoSelo(produtoBO.Selos[0], produtoBO);
                }

                // Autor
                if (produtoBO.TituloImpresso.Titulo.TituloAutores != null && produtoBO.TituloImpresso.Titulo.TituloAutores.Any())
                {
                    //#######################################
                    //#######################################
                    //#######################################
                    //#######################################
                    //this.RelacionarTituloAutor(produtoBO.TituloImpresso.Titulo, produtoBO.TituloImpresso.Titulo.TituloAutores[0].Autor);
                }

                // Categoria
                if (!String.IsNullOrEmpty(produtoBO.CodigoLegadoCategoria) && !String.IsNullOrEmpty(produtoBO.NomeLegadoCategoria))
                {
                    this.ImportarProdutoCategoria(produtoBO);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtoBO"></param>
        private void ImportarTituloEletronico(Produto produtoBO)
        {
            if (produtoBO.TituloEletronico != null && !String.IsNullOrEmpty(produtoBO.TituloEletronico.Isbn13))
            {
                TituloEletronico tituloEletronicoBO = new TituloEletronico();
                tituloEletronicoBO = new TituloEletronicoBLL().CarregarPorIsbn13(produtoBO.TituloEletronico.Isbn13);

                if (tituloEletronicoBO == null) // Insere Produto, Titulo e TituloEletronico
                {
                    Conteudo conteudoProdutoBO = new Conteudo();
                    conteudoProdutoBO.ConteudoTipo = new ConteudoTipo();
                    conteudoProdutoBO.ConteudoTipo.ConteudoTipoId = 2;
                    conteudoProdutoBO.DataHoraCadastro = DateTime.Now;

                    new ConteudoBLL().InserirConteudo(conteudoProdutoBO);

                    if (conteudoProdutoBO != null && conteudoProdutoBO.ConteudoId > 0)
                    {
                        produtoBO.ProdutoId = conteudoProdutoBO.ConteudoId;

                        new ProdutoBLL().Inserir(produtoBO);
                    }

                    if (String.IsNullOrEmpty(produtoBO.Isbn13Relacionado)) // Se não tem ISBN relacionado, insere Titulo
                    {
                        produtoBO.TituloEletronico.Titulo = this.InserirConteudoTitulo(produtoBO.TituloEletronico.Titulo);
                    }
                    else // Se tem ISBN Relacionado, consulta em TituloImpresso e captura tituloId de TituloImpresso e associa ao tituloId de TituloEletronico
                    {
                        TituloImpresso tituloImpressoBO = new TituloImpresso();
                        tituloImpressoBO = new TituloImpressoBLL().CarregarPorIsbn13(produtoBO.Isbn13Relacionado);

                        if (tituloImpressoBO != null && tituloImpressoBO.Titulo != null && tituloImpressoBO.Titulo.TituloId > 0)
                        {
                            produtoBO.TituloEletronico.Titulo.TituloId = tituloImpressoBO.Titulo.TituloId;
                        }
                        else // Se não encontrou o tituloId de TituloImpresso, insere o conteúdo
                        {
                            produtoBO.TituloEletronico.Titulo = this.InserirConteudoTitulo(produtoBO.TituloEletronico.Titulo);
                        }
                    }

                    // Insere TituloEletronico
                    if (produtoBO != null && produtoBO.ProdutoId > 0 && produtoBO.TituloEletronico.Titulo != null && produtoBO.TituloEletronico.Titulo.TituloId > 0)
                    {
                        produtoBO.TituloEletronico.TituloEletronicoId = produtoBO.ProdutoId;

                        new TituloEletronicoBLL().Inserir(produtoBO.TituloEletronico);
                    }
                }
                else if (tituloEletronicoBO.TituloEletronicoId > 0 && tituloEletronicoBO.Titulo != null && tituloEletronicoBO.Titulo.TituloId > 0) // Atualiza Produto e Titulo
                {
                    produtoBO.ProdutoId = tituloEletronicoBO.TituloEletronicoId;

                    new ProdutoBLL().AtualizarMenosNome(produtoBO);

                    produtoBO.TituloEletronico.Titulo.TituloId = tituloEletronicoBO.Titulo.TituloId;

                    //new TituloBLL().AtualizarMenosNomeSubtitulo(produtoBO.TituloEletronico.Titulo);
                }

                // Produto Formato
                if (produtoBO != null && produtoBO.ProdutoId > 0 && produtoBO.ProdutoFormato != null && !String.IsNullOrEmpty(produtoBO.ProdutoFormato.Formato))
                {
                    produtoBO.ProdutoFormato.ProdutoId = produtoBO.ProdutoId;

                    this.ImportarProdutoFormato(produtoBO.ProdutoFormato);
                }

                // Selo
                if (produtoBO.Selos != null && produtoBO.Selos.Any() && produtoBO.Selos[0] != null && produtoBO.Selos[0].SeloId > 0)
                {
                    this.RelacionarProdutoSelo(produtoBO.Selos[0], produtoBO);
                }

                // Autor
                if (produtoBO.TituloEletronico.Titulo.TituloAutores != null && produtoBO.TituloEletronico.Titulo.TituloAutores.Any())
                {
                    //#######################################
                    //#######################################
                    //#######################################
                    //#######################################
                    //this.RelacionarTituloAutor(produtoBO.TituloEletronico.Titulo, produtoBO.TituloEletronico.Titulo.TituloAutores[0].Autor);
                }

                // Categoria
                if (!String.IsNullOrEmpty(produtoBO.CodigoLegadoCategoria) && !String.IsNullOrEmpty(produtoBO.NomeLegadoCategoria))
                {
                    this.ImportarProdutoCategoria(produtoBO);
                }
            }
        }

        #endregion

        #region [ Capitulo ]

        /// <summary>
        /// Verifica se o Capitulo já está cadastrado, consultando pelo código legado, se não estiver, cadastra Capitulo necessitando de tituloId
        /// </summary>
        /// <param name="paramCapituloBO"></param>
        /// <returns></returns>
        private Capitulo ImportarConteudoCapitulo(Capitulo paramCapituloBO)
        {
            Capitulo capituloBO = new CapituloBLL().CarregarPorCodigoLegado(paramCapituloBO);

            // Se Capitulo não existe, insere Conteudo, Capitulo
            if (capituloBO == null && paramCapituloBO.Titulo != null && paramCapituloBO.Titulo.TituloId > 0)
            {
                Conteudo conteudoBO = new Conteudo();
                conteudoBO.ConteudoTipo = new ConteudoTipo();
                conteudoBO.ConteudoTipo.ConteudoTipoId = 15;
                conteudoBO.DataHoraCadastro = DateTime.Now;

                new ConteudoBLL().InserirConteudo(conteudoBO);

                if (conteudoBO != null && conteudoBO.ConteudoId > 0)
                {
                    paramCapituloBO.CapituloId = conteudoBO.ConteudoId;

                    new CapituloBLL().Inserir(paramCapituloBO);

                    capituloBO = paramCapituloBO;
                }
            }
            else if (capituloBO != null && capituloBO.CapituloId > 0)// Se Capitulo existe, atualiza
            {
                capituloBO.NomeCapitulo = paramCapituloBO.NomeCapitulo;
                capituloBO.NumeroPaginaCapitulo = paramCapituloBO.NumeroPaginaCapitulo;
                capituloBO.ResumoCapitulo = paramCapituloBO.ResumoCapitulo;

                new CapituloBLL().AtualizarMenosNomeResumo(capituloBO);
            }

            return capituloBO;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtoBO"></param>
        private void ImportarCapituloImpresso(Produto produtoBO)
        {
            if (produtoBO.CapituloImpresso != null && produtoBO.CapituloImpresso.Capitulo != null && !String.IsNullOrEmpty(produtoBO.CapituloImpresso.Capitulo.CodigoLegado))
            {
                TituloImpresso tituloImpressoBO = new TituloImpresso();
                tituloImpressoBO = new TituloImpressoBLL().CarregarPorIsbn13(produtoBO.Isbn13Relacionado);

                // Se encontrou o TituloImpresso ao qual Capitulo deve estar relacionado
                if (tituloImpressoBO != null && tituloImpressoBO.Titulo != null && tituloImpressoBO.Titulo.TituloId > 0)
                {
                    produtoBO.CapituloImpresso.Capitulo.Titulo = new Titulo();
                    produtoBO.CapituloImpresso.Capitulo.Titulo = tituloImpressoBO.Titulo;

                    Capitulo capituloBO = this.ImportarConteudoCapitulo(produtoBO.CapituloImpresso.Capitulo);

                    if (capituloBO != null && capituloBO.CapituloId > 0) // Existe Capitulo relacionado com Titulo 
                    {
                        CapituloImpresso capituloImpressoBO = new CapituloImpressoBLL().CarregarCapituloImpressoRelacionado(capituloBO, tituloImpressoBO);

                        if (capituloImpressoBO != null && capituloImpressoBO.CapituloImpressoId > 0) // Atualiza
                        {
                            produtoBO.ProdutoId = capituloImpressoBO.CapituloImpressoId;

                            new ProdutoBLL().AtualizarMenosNome(produtoBO);
                        }
                        else // Cadastra Conteudo, Produto e CapituloImpresso
                        {
                            Conteudo conteudoProdutoBO = new Conteudo();
                            conteudoProdutoBO.ConteudoTipo = new ConteudoTipo();
                            conteudoProdutoBO.ConteudoTipo.ConteudoTipoId = 3;
                            conteudoProdutoBO.DataHoraCadastro = DateTime.Now;

                            new ConteudoBLL().InserirConteudo(conteudoProdutoBO);

                            if (conteudoProdutoBO != null && conteudoProdutoBO.ConteudoId > 0)
                            {
                                produtoBO.ProdutoId = conteudoProdutoBO.ConteudoId;

                                new ProdutoBLL().Inserir(produtoBO);

                                if (produtoBO != null && produtoBO.ProdutoId > 0)
                                {
                                    capituloImpressoBO = new CapituloImpresso();
                                    capituloImpressoBO.CapituloImpressoId = produtoBO.ProdutoId;
                                    capituloImpressoBO.Capitulo = capituloBO;
                                    capituloImpressoBO.TituloImpresso = tituloImpressoBO;

                                    new CapituloImpressoBLL().Inserir(capituloImpressoBO);
                                }
                            }
                        }

                        // Produto Formato
                        if (produtoBO != null && produtoBO.ProdutoId > 0 && produtoBO.ProdutoFormato != null && !String.IsNullOrEmpty(produtoBO.ProdutoFormato.Formato))
                        {
                            produtoBO.ProdutoFormato.ProdutoId = produtoBO.ProdutoId;

                            this.ImportarProdutoFormato(produtoBO.ProdutoFormato);
                        }

                        //Relaciona Selo
                        if (produtoBO != null && produtoBO.ProdutoId > 0)
                        {
                            List<Selo> seloBOList = new SeloBLL().Carregar(new Produto() { ProdutoId = tituloImpressoBO.TituloImpressoId }); // Carrega os Selos do TituloImpresso

                            if (seloBOList != null && seloBOList.Any()) // Selo
                            {
                                foreach (Selo seloBOTemp in seloBOList)
                                {
                                    this.RelacionarProdutoSelo(seloBOTemp, produtoBO); // Usa os mesmos Selos do TituloImpresso e relaciona com o CapituloImpresso
                                }
                            }
                        }

                        // Relaciona Autor
                        List<Autor> autorBOList = new TituloBLL().CarregarAutores(tituloImpressoBO.Titulo);

                        if (autorBOList != null && autorBOList.Any())
                        {
                            new CapituloBLL().InserirAutoresDeCapitulo(capituloBO, autorBOList);
                        }

                        // Categoria
                        if (!String.IsNullOrEmpty(produtoBO.CodigoLegadoCategoria) && !String.IsNullOrEmpty(produtoBO.NomeLegadoCategoria))
                        {
                            this.ImportarProdutoCategoria(produtoBO);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtoBO"></param>
        private void ImportarCapituloEletronico(Produto produtoBO)
        {
            if (produtoBO.CapituloEletronico != null && produtoBO.CapituloEletronico.Capitulo != null && !String.IsNullOrEmpty(produtoBO.CapituloEletronico.Capitulo.CodigoLegado))
            {
                TituloEletronico tituloEletronicoBO = new TituloEletronico();
                tituloEletronicoBO = new TituloEletronicoBLL().CarregarPorIsbn13(produtoBO.Isbn13Relacionado);

                // Se encontrou o TituloEletronico ao qual Capitulo deve estar relacionado
                if (tituloEletronicoBO != null && tituloEletronicoBO.Titulo != null && tituloEletronicoBO.Titulo.TituloId > 0)
                {
                    produtoBO.CapituloEletronico.Capitulo.Titulo = new Titulo();
                    produtoBO.CapituloEletronico.Capitulo.Titulo = tituloEletronicoBO.Titulo;

                    Capitulo capituloBO = this.ImportarConteudoCapitulo(produtoBO.CapituloEletronico.Capitulo);

                    if (capituloBO != null && capituloBO.CapituloId > 0) // Existe Capitulo relacionado com Titulo 
                    {
                        CapituloEletronico capituloEletronicoBO = new CapituloEletronicoBLL().CarregarCapituloEletronicoRelacionado(capituloBO, tituloEletronicoBO);

                        if (capituloEletronicoBO != null && capituloEletronicoBO.CapituloEletronicoId > 0) // Atualiza
                        {
                            produtoBO.ProdutoId = capituloEletronicoBO.CapituloEletronicoId;

                            new ProdutoBLL().AtualizarMenosNome(produtoBO);
                        }
                        else // Cadastra Conteudo, Produto e CapituloEletronico
                        {
                            Conteudo conteudoProdutoBO = new Conteudo();
                            conteudoProdutoBO.ConteudoTipo = new ConteudoTipo();
                            conteudoProdutoBO.ConteudoTipo.ConteudoTipoId = 3;
                            conteudoProdutoBO.DataHoraCadastro = DateTime.Now;

                            new ConteudoBLL().InserirConteudo(conteudoProdutoBO);

                            if (conteudoProdutoBO != null && conteudoProdutoBO.ConteudoId > 0)
                            {
                                produtoBO.ProdutoId = conteudoProdutoBO.ConteudoId;

                                new ProdutoBLL().Inserir(produtoBO);

                                if (produtoBO != null && produtoBO.ProdutoId > 0)
                                {
                                    capituloEletronicoBO = new CapituloEletronico();
                                    capituloEletronicoBO.CapituloEletronicoId = produtoBO.ProdutoId;
                                    capituloEletronicoBO.Capitulo = capituloBO;
                                    capituloEletronicoBO.TituloEletronico = tituloEletronicoBO;

                                    new CapituloEletronicoBLL().Inserir(capituloEletronicoBO);
                                }
                            }
                        }

                        // Produto Formato
                        if (produtoBO != null && produtoBO.ProdutoId > 0 && produtoBO.ProdutoFormato != null && !String.IsNullOrEmpty(produtoBO.ProdutoFormato.Formato))
                        {
                            produtoBO.ProdutoFormato.ProdutoId = produtoBO.ProdutoId;

                            this.ImportarProdutoFormato(produtoBO.ProdutoFormato);
                        }

                        //Relaciona Selo
                        if (produtoBO != null && produtoBO.ProdutoId > 0)
                        {
                            List<Selo> seloBOList = new SeloBLL().Carregar(new Produto() { ProdutoId = tituloEletronicoBO.TituloEletronicoId }); // Carrega os Selos do TituloImpresso

                            if (seloBOList != null && seloBOList.Any()) // Selo
                            {
                                foreach (Selo seloBOTemp in seloBOList)
                                {
                                    this.RelacionarProdutoSelo(seloBOTemp, produtoBO); // Usa os mesmos Selos do TituloImpresso e relaciona com o CapituloImpresso
                                }
                            }
                        }

                        // Relaciona Autor
                        List<Autor> autorBOList = new TituloBLL().CarregarAutores(tituloEletronicoBO.Titulo);

                        if (autorBOList != null && autorBOList.Any())
                        {
                            new CapituloBLL().InserirAutoresDeCapitulo(capituloBO, autorBOList);
                        }

                        // Categoria
                        if (!String.IsNullOrEmpty(produtoBO.CodigoLegadoCategoria) && !String.IsNullOrEmpty(produtoBO.NomeLegadoCategoria))
                        {
                            this.ImportarProdutoCategoria(produtoBO);
                        }
                    }
                }
            }
        }

        #endregion

        #region [ Revista ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtoBO"></param>
        /// <returns></returns>
        private Revista ImportarConteudoRevista(Revista paramRevistaBO)
        {
            Revista revistaBO = null;

            if (!String.IsNullOrWhiteSpace(paramRevistaBO.ISSN))
            {
                revistaBO = new Revista();

                revistaBO = new RevistaBLL().CarregarRevistaPorIssn(paramRevistaBO);

                if (revistaBO == null)
                {
                    Conteudo conteudoBO = new Conteudo();
                    conteudoBO.ConteudoTipo = new ConteudoTipo();
                    conteudoBO.ConteudoTipo.ConteudoTipoId = 13;
                    conteudoBO.DataHoraCadastro = DateTime.Now;

                    new ConteudoBLL().InserirConteudo(conteudoBO);

                    if (conteudoBO != null && conteudoBO.ConteudoId > 0)
                    {
                        paramRevistaBO.RevistaId = conteudoBO.ConteudoId;

                        new RevistaBLL().Inserir(paramRevistaBO);

                        revistaBO = paramRevistaBO;
                    }
                }
            }
            else
            {
                objLogHelper.WriteOnFile("ICR1", "ImportarConteudoRevista", "Revista Codigo [" + paramRevistaBO.NomeRevista + "] nao importada");
            }

            return revistaBO;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtoBO"></param>
        private void ImportarRevistaAssinatura(Produto produtoBO)
        {
            if (produtoBO.RevistaAssinatura != null && produtoBO.RevistaAssinatura.Revista != null && !String.IsNullOrEmpty(produtoBO.RevistaAssinatura.Revista.ISSN) && produtoBO.RevistaAssinatura.NumeroExemplares != null && produtoBO.RevistaAssinatura.NumeroExemplares > 0)
            {
                produtoBO.RevistaAssinatura.Revista = this.ImportarConteudoRevista(produtoBO.RevistaAssinatura.Revista);

                if (produtoBO.RevistaAssinatura.Revista != null && produtoBO.RevistaAssinatura.Revista.RevistaId > 0)
                {
                    RevistaAssinatura revistaAssinaturaBO = new RevistaAssinaturaBLL().CarregarPorRevistaNumExemplares(produtoBO.RevistaAssinatura);

                    if (revistaAssinaturaBO == null)
                    {
                        // Insere Conteudo
                        Conteudo conteudoProdutoBO = new Conteudo();
                        conteudoProdutoBO.ConteudoTipo = new ConteudoTipo();
                        conteudoProdutoBO.ConteudoTipo.ConteudoTipoId = 6;
                        conteudoProdutoBO.DataHoraCadastro = DateTime.Now;

                        new ConteudoBLL().InserirConteudo(conteudoProdutoBO);

                        // Insere Produto
                        if (conteudoProdutoBO != null && conteudoProdutoBO.ConteudoId > 0)
                        {
                            produtoBO.ProdutoId = conteudoProdutoBO.ConteudoId;

                            new ProdutoBLL().Inserir(produtoBO);

                            // Insere RevistaAssinatura
                            if (produtoBO != null && produtoBO.ProdutoId > 0)
                            {
                                produtoBO.RevistaAssinatura.RevistaAssinaturaId = produtoBO.ProdutoId;

                                new RevistaAssinaturaBLL().Inserir(produtoBO.RevistaAssinatura);

                                // Selo
                                if (produtoBO.Selos != null && produtoBO.Selos.Any() && produtoBO.Selos[0] != null && produtoBO.Selos[0].SeloId > 0) // Selo
                                {
                                    this.RelacionarProdutoSelo(produtoBO.Selos[0], produtoBO);
                                }

                                // Categoria
                                if (!String.IsNullOrEmpty(produtoBO.CodigoLegadoCategoria) && !String.IsNullOrEmpty(produtoBO.NomeLegadoCategoria))
                                {
                                    this.ImportarProdutoCategoria(produtoBO);
                                }
                            }
                        }
                    }
                    else if (revistaAssinaturaBO.RevistaAssinaturaId > 0) // Atualiza Produto
                    {
                        produtoBO.ProdutoId = revistaAssinaturaBO.RevistaAssinaturaId;

                        new ProdutoBLL().AtualizarMenosNome(produtoBO);
                    }

                    // Selo
                    if (produtoBO.Selos != null && produtoBO.Selos.Any() && produtoBO.Selos[0] != null && produtoBO.Selos[0].SeloId > 0)
                    {
                        this.RelacionarProdutoSelo(produtoBO.Selos[0], produtoBO);
                    }

                    // Categoria
                    if (!String.IsNullOrEmpty(produtoBO.CodigoLegadoCategoria) && !String.IsNullOrEmpty(produtoBO.NomeLegadoCategoria))
                    {
                        this.ImportarProdutoCategoria(produtoBO);
                    }
                }
            }
            else
            {
                objLogHelper.WriteOnFile("IRA1", "ImportarRevistaAssinatura", "Revista não importada");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtoBO"></param>
        private void ImportarRevistaEdicao(Produto produtoBO)
        {
            if (produtoBO.RevistaEdicao != null && produtoBO.RevistaEdicao.Revista != null && !String.IsNullOrEmpty(produtoBO.RevistaEdicao.Revista.ISSN))
            {
                produtoBO.RevistaEdicao.Revista = this.ImportarConteudoRevista(produtoBO.RevistaEdicao.Revista);

                if (produtoBO.RevistaEdicao.Revista != null && produtoBO.RevistaEdicao.Revista.RevistaId > 0)
                {
                    RevistaEdicao revistaEdicaoBO = new RevistaEdicaoBLL().CarregarPorRevistaNumEdicao(produtoBO.RevistaEdicao);

                    if (revistaEdicaoBO == null)
                    {
                        // Insere Conteudo
                        Conteudo conteudoProdutoBO = new Conteudo();
                        conteudoProdutoBO.ConteudoTipo = new ConteudoTipo();
                        conteudoProdutoBO.ConteudoTipo.ConteudoTipoId = 5;
                        conteudoProdutoBO.DataHoraCadastro = DateTime.Now;

                        new ConteudoBLL().InserirConteudo(conteudoProdutoBO);

                        // Insere Produto
                        if (conteudoProdutoBO != null && conteudoProdutoBO.ConteudoId > 0)
                        {
                            produtoBO.ProdutoId = conteudoProdutoBO.ConteudoId;

                            new ProdutoBLL().Inserir(produtoBO);

                            // Insere RevistaEdicao
                            if (produtoBO != null && produtoBO.ProdutoId > 0)
                            {
                                produtoBO.RevistaEdicao.RevistaEdicaoId = produtoBO.ProdutoId;

                                new RevistaEdicaoBLL().Inserir(produtoBO.RevistaEdicao);

                                // Selo
                                if (produtoBO.Selos != null && produtoBO.Selos.Any() && produtoBO.Selos[0] != null && produtoBO.Selos[0].SeloId > 0) // Selo
                                {
                                    this.RelacionarProdutoSelo(produtoBO.Selos[0], produtoBO);
                                }

                                // Categoria
                                if (!String.IsNullOrEmpty(produtoBO.CodigoLegadoCategoria) && !String.IsNullOrEmpty(produtoBO.NomeLegadoCategoria))
                                {
                                    this.ImportarProdutoCategoria(produtoBO);
                                }
                            }
                        }
                    }
                    else if (revistaEdicaoBO.RevistaEdicaoId > 0) // Atualiza Produto
                    {
                        produtoBO.ProdutoId = revistaEdicaoBO.RevistaEdicaoId;

                        new ProdutoBLL().AtualizarMenosNome(produtoBO);
                    }

                    // Selo
                    if (produtoBO.Selos != null && produtoBO.Selos.Any() && produtoBO.Selos[0] != null && produtoBO.Selos[0].SeloId > 0)
                    {
                        this.RelacionarProdutoSelo(produtoBO.Selos[0], produtoBO);
                    }

                    // Categoria
                    if (!String.IsNullOrEmpty(produtoBO.CodigoLegadoCategoria) && !String.IsNullOrEmpty(produtoBO.NomeLegadoCategoria))
                    {
                        this.ImportarProdutoCategoria(produtoBO);
                    }
                }
            }
            else
            {
                objLogHelper.WriteOnFile("IRE1", "ImportarRevistaEdicao", "Revista não importada");
            }
        }

        #endregion

        #region [ Titulo Eltronico Aluguel ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtoBO"></param>
        private void ImportarTituloEletronicoAluguel(Produto produtoBO)
        {
            if (
                produtoBO.TituloEletronico.TituloEletronicoAlugueis != null
                && produtoBO.TituloEletronico.TituloEletronicoAlugueis.Any()
                && produtoBO.TituloEletronico.TituloEletronicoAlugueis[0] != null
                && produtoBO.TituloEletronico.TituloEletronicoAlugueis[0].TempoAluguel > 0
                && produtoBO.TituloEletronico != null
                && !String.IsNullOrEmpty(produtoBO.TituloEletronico.Isbn13)
                )
            {
                TituloEletronico tituloEletronicoBO = new TituloEletronico();
                tituloEletronicoBO = new TituloEletronicoBLL().CarregarPorIsbn13(produtoBO.TituloEletronico.Isbn13);

                if (tituloEletronicoBO != null && tituloEletronicoBO.TituloEletronicoId > 0) // Insere Produto, Titulo e TituloEletronico
                {
                    produtoBO.TituloEletronico.TituloEletronicoAlugueis[0].TituloEletronico = tituloEletronicoBO;

                    bool flag = new TituloEletronicoAluguelBLL().TituloEletronicoTempoAluguelRelacionado(produtoBO.TituloEletronico.TituloEletronicoAlugueis[0]);

                    if (!flag)
                    {
                        Conteudo conteudoProdutoBO = new Conteudo();
                        conteudoProdutoBO.ConteudoTipo = new ConteudoTipo();
                        conteudoProdutoBO.ConteudoTipo.ConteudoTipoId = 16;
                        conteudoProdutoBO.DataHoraCadastro = DateTime.Now;

                        new ConteudoBLL().InserirConteudo(conteudoProdutoBO);

                        if (conteudoProdutoBO != null && conteudoProdutoBO.ConteudoId > 0)
                        {
                            produtoBO.ProdutoId = conteudoProdutoBO.ConteudoId;

                            new ProdutoBLL().Inserir(produtoBO);

                            // Insere TituloEletronicoAluguel
                            if (produtoBO != null && produtoBO.ProdutoId > 0)
                            {
                                produtoBO.TituloEletronico.TituloEletronicoAlugueis[0].TituloEletronicoAluguelId = produtoBO.ProdutoId;

                                new TituloEletronicoAluguelBLL().Inserir(produtoBO.TituloEletronico.TituloEletronicoAlugueis[0]);
                            }

                            // Produto Formato
                            if (produtoBO != null && produtoBO.ProdutoId > 0 && produtoBO.ProdutoFormato != null && !String.IsNullOrEmpty(produtoBO.ProdutoFormato.Formato))
                            {
                                produtoBO.ProdutoFormato.ProdutoId = produtoBO.ProdutoId;

                                this.ImportarProdutoFormato(produtoBO.ProdutoFormato);
                            }

                            // Categoria
                            if (!String.IsNullOrEmpty(produtoBO.CodigoLegadoCategoria) && !String.IsNullOrEmpty(produtoBO.NomeLegadoCategoria))
                            {
                                this.ImportarProdutoCategoria(produtoBO);
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #endregion

        #endregion
    }
}