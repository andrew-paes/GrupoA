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
    public class ProdutoRestauradorISBN
    {
        #region [ Proprerties ]

        List<Produto> produtoBOList = null;
        string remoteHost = Convert.ToString(ConfigurationManager.AppSettings["RemoteHost"]);
        string remoteUser = Convert.ToString(ConfigurationManager.AppSettings["RemoteUser"]);
        string remotePass = Convert.ToString(ConfigurationManager.AppSettings["RemotePass"]);

        string folderRemotePath = Convert.ToString(ConfigurationManager.AppSettings["SincProdutoFolderRemotePath"]);
        string folderLocalPath = Convert.ToString(ConfigurationManager.AppSettings["SincProdutoFolderLocalPath"]);

        private LogHelper objLogHelper = new LogHelper("ProdutoRestauradorISBN");

        #endregion

        #region [ Methods ]

        /// <summary>
        /// 
        /// </summary>
        /// 
        public void RestaurarISBN()
        {
            objLogHelper.WriteOnFile(Environment.NewLine + "P1", "SincronizarProduto - Início Sincronização", "");

            try
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
                        reqFTP.RenameTo = String.Concat("../processados/", fileName.Replace(".csv", String.Concat(".csvOLD_", DateTime.Now.ToString("yyyyMMddhhmmss"))));
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
                    File.Move(fileLocalPath, String.Concat(folderLocalPathToStore, fileName.Replace(".csv", String.Concat(".csvOLD_", DateTime.Now.ToString("yyyyMMddhhmmss")))));
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

            System.IO.StreamReader file = new System.IO.StreamReader(pathCSV); // Read the file and display it line by line.

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
                    case "44.00.00": // Periódicos BMJ
                    case "44.01.01": // Edição BMJ
                    case "55.00.00": // Periódicos Patio Infantil
                    case "55.01.01": // Edição Patio Infantil
                    case "55.02.01": // Edição Patio Pedagogica
                    case "55.03.01": // Edição Patio Ensino Medio
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
        /// <param name="linhaCSV"></param>
        /// <returns></returns>
        private Produto ProcessarTituloImpresso(string linhaCSV)
        {
            Produto produtoBO = new Produto();
            produtoBO.ProdutoTipo = new ProdutoTipo();
            produtoBO.Conteudo = new Conteudo();
            produtoBO.Conteudo.ConteudoTipo = new ConteudoTipo();
            produtoBO.TituloImpresso = new TituloImpresso();
            produtoBO.TituloImpresso.Titulo = new Titulo();
            produtoBO.TituloImpresso.Titulo.TituloAutores = new List<TituloAutor>();

            produtoBO.ProdutoTipo.ProdutoTipoId = 1;

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
            produtoBO.ProdutoTipo = new ProdutoTipo();
            produtoBO.Conteudo = new Conteudo();
            produtoBO.Conteudo.ConteudoTipo = new ConteudoTipo();
            produtoBO.TituloEletronico = new TituloEletronico();
            produtoBO.TituloEletronico.Titulo = new Titulo();
            produtoBO.TituloEletronico.Titulo.TituloAutores = new List<TituloAutor>();

            produtoBO.ProdutoTipo.ProdutoTipoId = 2;

            string[] colunaCSV = Convert.ToString(linhaCSV).Split(';');

            if (colunaCSV[0] != null && !String.IsNullOrWhiteSpace(colunaCSV[0].ToString().Trim()))
            {
                produtoBO.CodigoProduto = colunaCSV[0].ToString().Trim();
            }

            if (colunaCSV[11] != null && !String.IsNullOrWhiteSpace(colunaCSV[11].ToString().Trim()))
            {
                produtoBO.TituloEletronico.Isbn13 = colunaCSV[11].ToString().Trim();
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
            }
            else
            {
                objLogHelper.WriteOnFile("PP4", "ProcessarProduto - Erro", "produtoBOList é nulo ou não contém elementos");
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

        #region [ Titulo ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtoBO"></param>
        private void ImportarTituloImpresso(Produto produtoBO)
        {
            objLogHelper.WriteOnFile("ITI1", "ImportarTituloImpresso");

            if (produtoBO.TituloImpresso != null && !String.IsNullOrEmpty(produtoBO.CodigoProduto))
            {
                objLogHelper.WriteOnFile("ITI2", "ImportarTituloImpresso", "produtoCodigo: " + produtoBO.CodigoProduto);

                TituloImpresso tituloImpressoBO = new TituloImpresso();
                tituloImpressoBO = new TituloImpressoBLL().CarregarPorIsbn13(produtoBO.CodigoProduto);

                if (!String.IsNullOrEmpty(produtoBO.TituloImpresso.Isbn13) && tituloImpressoBO != null && tituloImpressoBO.TituloImpressoId > 0 && tituloImpressoBO.Titulo != null && tituloImpressoBO.Titulo.TituloId > 0)
                {
                    tituloImpressoBO.Isbn10 = produtoBO.TituloImpresso.Isbn10;
                    tituloImpressoBO.Isbn13 = produtoBO.TituloImpresso.Isbn13;
                    new TituloImpressoBLL().AtualizarISBN(tituloImpressoBO);

                    objLogHelper.WriteOnFile("ITI3", "ImportarTituloImpresso", "Alterado: " + tituloImpressoBO.Isbn13);
                }
                else
                {
                    objLogHelper.WriteOnFile("ITI4", "ImportarTituloImpresso", "Não Alterado");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtoBO"></param>
        private void ImportarTituloEletronico(Produto produtoBO)
        {
            objLogHelper.WriteOnFile("ITE1", "ImportarTituloEletronico");

            if (produtoBO.TituloEletronico != null && !String.IsNullOrEmpty(produtoBO.CodigoProduto))
            {
                objLogHelper.WriteOnFile("ITE2", "ImportarTituloEletronico", "produtoCodigo: " + produtoBO.CodigoProduto);

                TituloEletronico tituloEletronicoBO = new TituloEletronico();
                tituloEletronicoBO = new TituloEletronicoBLL().CarregarPorIsbn13(produtoBO.CodigoProduto);

                if (!String.IsNullOrEmpty(produtoBO.TituloEletronico.Isbn13) && tituloEletronicoBO != null && tituloEletronicoBO.TituloEletronicoId > 0 && tituloEletronicoBO.Titulo != null && tituloEletronicoBO.Titulo.TituloId > 0)
                {
                    tituloEletronicoBO.Isbn13 = produtoBO.TituloEletronico.Isbn13;
                    new TituloEletronicoBLL().AtualizarISBN(tituloEletronicoBO);

                    objLogHelper.WriteOnFile("ITE3", "ImportarTituloEletronico", "Alterado: " + tituloEletronicoBO.Isbn13);
                }
                else
                {
                    objLogHelper.WriteOnFile("ITE4", "ImportarTituloEletronico", "Não Alterado");
                }
            }
        }

        #endregion

        #endregion

        #endregion
    }
}
