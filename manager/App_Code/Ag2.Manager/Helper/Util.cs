using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;
using System.IO;
using System.Linq;
using System.Web;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI;


namespace Ag2.Manager.Helper
{
    public class Util
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public Util()
        {
            //
        }

        public string CurrentIdioma
        {
            get
            {
                return ((HiddenField)((Page)HttpContext.Current.Handler).Master.FindControl("hdnIdioma")).Value;
            }
        }

        /// <summary>
        /// Retorna o caminho relativo da pasta Uploads
        /// </summary>
        public static string UploadRoot
        {
            get
            {
                return GrupoA.GlobalResources.GrupoA_Resource.baseUrlUpload;
            }
        }

        /// <summary>
        /// Retorna o Html do controle setado
        /// </summary>
        /// <param name="control">Controle a ser recuperado o htmls</param>
        /// <returns></returns>
        public static string GetHtmlRender(System.Web.UI.WebControls.WebControl control)
        {
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(new System.IO.StringWriter());
            control.RenderControl(hw);

            return hw.InnerWriter.ToString(); ;
        }

        /// <summary>
        /// Mostra a mensagem padrão de registro inserido com sucesso
        /// </summary>
        public static void ShowInsertMessage()
        {
            ShowMessage("Registro inserido com sucesso", Ag2.Manager.Enumerator.typeMessage.Sucesso);
        }

        /// <summary>
        /// Mostra a mensagem padrão de registro alterado com sucesso
        /// </summary>
        public static void ShowUpdateMessage()
        {
            ShowMessage("Registro alterado com sucesso", Ag2.Manager.Enumerator.typeMessage.Sucesso);
        }

        /// <summary>
        /// Exibe a mensagem padrão do sistema
        /// </summary>
        /// <param name="message"></param>
        public static void ShowMessage(string message)
        {
            ShowMessage(message, Ag2.Manager.Enumerator.typeMessage.None);
        }

        /// <summary>
        /// Exibe a mensagem padrão do sistema
        /// </summary>
        /// <param name="message"></param>
        public static void ShowMessage(string message, Ag2.Manager.Enumerator.typeMessage typeMessage)
        {
            System.Web.UI.Page page = (System.Web.UI.Page)HttpContext.Current.Handler;
            System.Web.UI.ScriptManager.RegisterStartupScript(page, page.GetType(), "msgBox", string.Format("showMessage('{0}', '{1}');", message, typeMessage.ToString()), true);
            //page.ClientScript.RegisterStartupScript(page.GetType(), "msgBox", string.Format("showMessage('{0}');", message), true);
        }

        /// <summary>
        /// Faz download de um arquivo por stream
        /// </summary>
        /// <param name="conteudo">Conteudo a ser escrito no arquivo de download</param>
        /// <param name="fileName">Nome para exibição na caixa de dialogo do navegador</param>
        public static void DownloadFile(string conteudo, string fileName)
        {
            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms, Encoding.Default);
            sw.Write(conteudo);

            sw.Flush();
            sw.Close();

            byte[] byteArray = ms.ToArray();
            //Clean up the memory stream 
            ms.Flush();
            ms.Close();
            // Clear all content output from the buffer stream 
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Charset = "iso-8859-1";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");
            HttpContext.Current.Response.HeaderEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");

            // Add a HTTP header to the output stream that specifies the default filename 
            // for the browser's download dialog 
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", fileName));
            // Add a HTTP header to the output stream that contains the 
            // content length(File Size). This lets the browser know how much data is being transfered 
            HttpContext.Current.Response.AddHeader("Content-Length", byteArray.Length.ToString());
            // Set the HTTP MIME type of the output stream 
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            // Write the data out to the client. 
            HttpContext.Current.Response.BinaryWrite(byteArray);
        }

        /// <summary>
        /// Faz download de um arquivo gravado no file system do servidor
        /// </summary>
        /// <param name="ArquivoFisico">Caminho físico do arquivo</param>
        /// <param name="ArquivoVirtual">Nome para exibição na caixa de dialogo do navegador</param>
        /// <param name="forceDownload">Força ou não o downloas</param>
        public static void DownloadFile(string ArquivoFisico, string ArquivoVirtual, bool forceDownload)
        {
            string path = ArquivoFisico;
            string name = Path.GetFileName(path);
            string ext = Path.GetExtension(path);
            HttpContext.Current.Response.Charset = "iso-8859-1";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");
            HttpContext.Current.Response.HeaderEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");
            if (forceDownload)
            {
                HttpContext.Current.Response.AppendHeader("content-disposition", string.Concat("attachment; filename=", ArquivoVirtual));
            }
            HttpContext.Current.Response.ContentType = "application/octet-stream";

            if (File.Exists(path))
            {
                HttpContext.Current.Response.WriteFile(path);
                HttpContext.Current.Response.End();
            }
        }

        /// <summary>
        /// Retorna o id contido na querystring, caso não seja um número inteiro ele faz o redirecionamento para a página de login do sistema
        /// </summary>
        /// <returns></returns>
        public static int GetRequestId()
        {
            int _id = 0;
            Ag2.Security.SecureQueryString sq = null;

            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["q"]))
            {
                sq = new Security.SecureQueryString(HttpContext.Current.Request.QueryString["q"]);
            }

            if (!string.IsNullOrEmpty(sq["id"]))
            {
                Boolean isInt = int.TryParse(sq["id"], out _id);
                if (!isInt)
                    HttpContext.Current.Response.Redirect("~/Default.aspx", true);
            }

            return _id;
        }

        public static string GetQueryString(string key)
        {
            Ag2.Security.SecureQueryString sq = null;
            string _keyValue = string.Empty;

            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["q"]))
            {
                sq = new Security.SecureQueryString(HttpContext.Current.Request.QueryString["q"]);
            }

            if (!string.IsNullOrEmpty(sq[key]))
            {
                _keyValue = sq[key].ToString();
            }

            return _keyValue;
        }

        /// <summary>
        /// Faz com que as primeiras letras de uma frase fiquem com as letras maiusculas.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Capitalize(string value)
        {
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
        }

        /// <summary>
        /// Retorna o nome da dase de dados padrão setada no web.config
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultDataBase()
        {
            Database db = DatabaseFactory.CreateDatabase();

            DatabaseSettings databasesettings = new DatabaseSettings();
            string defaultDataBaseName = string.Empty;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HttpContext.Current.Server.MapPath("~/Web.Config"));

            defaultDataBaseName = xmlDoc.SelectSingleNode("//dataConfiguration").Attributes["defaultDatabase"].Value;

            return defaultDataBaseName;
        }

        /// <summary>
        /// <para>Retorna uma instancia da classe setada.</para>
        /// <para>O namespace da classe deve ser sempre setado como (Ag2.Manager.ADO.Oracle) ou (Ag2.Manager.ADO.MSSql).</para>
        /// </summary>
        /// <param name="className">Nome da classe a ser instanciada</param>
        /// <param name="assembly">Assembly que contem a classe a ser instanciada</param>
        /// <returns></returns>
        public static object GetADO(string className, System.Reflection.Assembly assembly)
        {
            string nameSpace = string.Empty;
            string defaultDataBaseName = GetDefaultDataBase();

            ConnectionStringSettingsCollection connectionStrings = System.Configuration.ConfigurationManager.ConnectionStrings;

            foreach (ConnectionStringSettings connection in connectionStrings)
            {
                if (connection.Name.Equals(defaultDataBaseName))
                {
                    if (connection.ProviderName.IndexOf("oracle", StringComparison.OrdinalIgnoreCase) > -1)
                    {
                        nameSpace = "Ag2.Manager.ADO.Oracle";
                        break;
                    }
                    else if (connection.ProviderName.IndexOf("SqlClient", StringComparison.OrdinalIgnoreCase) > -1)
                    {
                        nameSpace = "Ag2.Manager.ADO.MSSql";
                        break;
                    }
                }
            }
            return assembly.CreateInstance(string.Format("{0}.{1}", nameSpace, className));
        }

        public static DataBaseType GetBadaBaseType()
        {
            string defaultDataBaseName = GetDefaultDataBase();
            DataBaseType retorno = DataBaseType.SqlServer;

            ConnectionStringSettingsCollection connectionStrings = System.Configuration.ConfigurationManager.ConnectionStrings;

            foreach (ConnectionStringSettings connection in connectionStrings)
            {
                if (connection.Name.Equals(defaultDataBaseName))
                {
                    if (connection.ProviderName.IndexOf("oracle", StringComparison.OrdinalIgnoreCase) > -1)
                    {
                        retorno = DataBaseType.Oracle;
                        break;
                    }
                    else if (connection.ProviderName.IndexOf("SqlClient", StringComparison.OrdinalIgnoreCase) > -1)
                    {
                        retorno = DataBaseType.SqlServer;
                        break;
                    }
                }
            }

            return retorno;
        }

        public static string AddWhereOrAnd(string sql)
        {
            string retorno = string.Empty;

            if (sql.IndexOf(" WHERE ", StringComparison.OrdinalIgnoreCase) > -1)
            {
                retorno = " AND ";
            }
            else
            {
                retorno = " WHERE ";
            }

            return retorno;
        }

        public static object GetInstance(string className, System.Reflection.Assembly assembly)
        {
            return assembly.CreateInstance(className);
        }

        public static bool ExistInDicionary(string value)
        {
            List<string> list = new List<string>();

            list.Add("a");
            list.Add("e");
            list.Add("i");
            list.Add("o");
            list.Add("u");
            list.Add("da");
            list.Add("de");
            list.Add("di");
            list.Add("do");
            list.Add("du");
            list.Add("para");
            list.Add("pra");
            list.Add(";");
            list.Add("?");
            list.Add(":");
            list.Add("script");
            list.Add("select");
            list.Add("update");
            list.Add("delete");
            list.Add("insert");
            list.Add("|");
            list.Add("*");
            list.Add("-");
            list.Add("+");
            list.Add("=");
            list.Add("!");
            list.Add("@");
            list.Add("#");
            list.Add("$");
            list.Add("%");
            list.Add("&");
            list.Add("(");
            list.Add(")");
            list.Add("\"");
            list.Add("'");
            list.Add(">");
            list.Add("<");
            list.Add(",");
            list.Add("/");
            list.Add("\\");
            list.Add(".");

            return list.Contains(value.ToLower());
        }

        public static Ag2.Manager.Entity.ag2mngMenu GetModulePermissions(string moduleName)
        {
            Ag2.Manager.Helper.CurrentSessions cs = new Ag2.Manager.Helper.CurrentSessions();
            var menu = from m in cs.User.Menus where m.moduleName == moduleName select m;

            if (menu.Count() > 0)
                return menu.First();
            else
                return null;
        }

        public static Ag2.Manager.Entity.ag2mngMenu GetModulePermissions(int menuId)
        {
            Ag2.Manager.Helper.CurrentSessions cs = new Ag2.Manager.Helper.CurrentSessions();
            var menu = from m in cs.User.Menus where m.menuId == menuId select m;

            if (menu.Count() > 0)
                return menu.First();
            else
                return null;
        }


        public enum DataBaseType
        {
            SqlServer,
            Oracle
        }

        public static bool IsDate(string date)
        {
            try
            {
                Convert.ToDateTime(date);
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
