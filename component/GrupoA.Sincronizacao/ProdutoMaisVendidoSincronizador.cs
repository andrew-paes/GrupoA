using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.BusinessLogicalLayer;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using GrupoaA.Log;

namespace GrupoA.Sincronizacao
{
    public class ProdutoMaisVendidoSincronizador
    {
        #region [ Proprerties ]

        string remoteHost = Convert.ToString(ConfigurationManager.AppSettings["RemoteHost"]);
        string remoteUser = Convert.ToString(ConfigurationManager.AppSettings["RemoteUser"]);
        string remotePass = Convert.ToString(ConfigurationManager.AppSettings["RemotePass"]);

        string folderRemotePath = Convert.ToString(ConfigurationManager.AppSettings["SincMaisVendidoFolderRemotePath"]);
        string folderLocalPath = Convert.ToString(ConfigurationManager.AppSettings["SincProdutoFolderLocalPath"]);

        private LogHelper objLogHelper = new LogHelper("SincronizadorMaisVendido");
        private bool _flagTesteLocal = false;

        #endregion

        #region [ Methods ]

        /// <summary>
        /// 
        /// </summary>
        public void SincronizarProduto()
        {
            objLogHelper.WriteOnFile(Environment.NewLine + "P1", "SincronizarMaisVendido - Início Sincronização", "");

            string report = String.Empty;
            string fileLocalPath = String.Empty;
            List<Titulo> titulosMaisVendidos = null;

            try
            {
                if (_flagTesteLocal)
                {
                    fileLocalPath = "C:\\inetpub\\wwwroot\\Artmed\\trunk\\uploads\\sincronizador\\TMP\\maisvendidos.csv";
                }
                else
                {
                    string fileName = ConfigurationManager.AppSettings["SincMaisVendidoFileName"].ToString();
                    fileLocalPath = SincronizarFTP(fileName);
                }

                if (!string.IsNullOrEmpty(fileLocalPath))
                {
                    titulosMaisVendidos = this.ProcessarCSV(fileLocalPath);

                    if (titulosMaisVendidos != null && titulosMaisVendidos.Count() > 0)
                    {
                        new TituloBLL().AtualizarTitulosMaisVendidos(titulosMaisVendidos);

                        ArquivarCSV(fileLocalPath);
                    }
                }

            }
            catch (Exception ex)
            {
                objLogHelper.WriteOnFile("P3", "Erro no processamento", ex.ToString());
            }

            objLogHelper.WriteOnFile("P2", "SincronizarMaisVendido - Término Sincronização", "");
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
                //reqFTP.UsePassive = false;
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
                        reqFTP.RenameTo = String.Concat("processados/", fileName.Replace(".csv", String.Concat(".csvOLD_", DateTime.Now.ToString("yyyyMMddhhmmssFFFF"))));
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

        #region [ Processar CSV]

        /// <summary>
        /// Lê o CSV por OLEDB e devolve uma Lista de string
        /// </summary>
        private List<Titulo> ProcessarCSV(string fileLocalPath)
        {
            objLogHelper.WriteOnFile(Environment.NewLine + "PC1", "ProcessarCSV - Início", "");

            List<Titulo> tituloBOList = new List<Titulo>();
            DataTable dataTableCSV = new DataTable();
            string pathCSV = fileLocalPath; //ConfigurationManager.AppSettings["pathCSVMaisVendidos"].ToString();

            if (!File.Exists(pathCSV))
            {
                return null;
            }

            string line;
            Int32 numeroLinha = 0;

            System.IO.StreamReader file = new System.IO.StreamReader(pathCSV); // Read the file and display it line by line.

            while ((line = file.ReadLine()) != null)
            {
                try
                {
                    if (line != null)
                    {
                        String isbn13 = line.ToString().Replace(";", "");

                        Titulo tituloBO = new TituloBLL().CarregarTituloPorISBN13(isbn13);

                        if (tituloBO != null && tituloBO.TituloId > 0)
                        {
                            tituloBO.TituloImpresso = new TituloImpresso();
                            tituloBO.TituloImpresso.Isbn13 = isbn13;
                            tituloBO.MaisVendidoOrdem = numeroLinha;

                            tituloBOList.Add(tituloBO);
                        }

                        numeroLinha++;
                    }
                }
                catch (Exception ex)
                {
                    objLogHelper.WriteOnFile("PC", "Erro no processamento", ex.ToString());
                }
            }

            file.Close();

            objLogHelper.WriteOnFile("PC2", "ProcessarCSV - Término", "");

            return tituloBOList;
        }

        #endregion

        /// <summary>
        /// Move o CSV para uma pasta de backup local e renomeia a extensão, e apaga o arquivo no FTP
        /// </summary>
        /// <param name="fileLocalPath"></param>
        public void ArquivarCSV(string fileLocalPath)
        {
            string folderLocalPathToStore = ConfigurationManager.AppSettings["SincProdutoFolderLocalPathToStore"].ToString();
            string fileName = ConfigurationManager.AppSettings["SincMaisVendidoFileName"].ToString();

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

        #endregion
    }
}