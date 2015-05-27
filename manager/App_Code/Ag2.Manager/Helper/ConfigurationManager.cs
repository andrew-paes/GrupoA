using System;
using System.Collections.Generic;
using System.Web;


namespace Ag2.Manager.Helper
{
    /// <summary>
    /// Summary description for ConfigurationManager
    /// </summary>
    public class ConfigurationManager
    {
        private static string _siteRoot = string.Empty;
        private static string _baseUrlUpload = string.Empty;
        private static string _build = string.Empty;
        private static string _uploadRoot = string.Empty;
        private static bool _enableMultiLanguage = false;

        public ConfigurationManager()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string UploadRoot
        {
            get
            {
                if (System.Configuration.ConfigurationManager.AppSettings["uploadRoot"] != null)
                    _uploadRoot = System.Configuration.ConfigurationManager.AppSettings["uploadRoot"].ToString();

                return _uploadRoot;
            }
        }

        public static string SufixoTabelaIdioma
        {
            get
            {
                if (System.Configuration.ConfigurationManager.AppSettings["sufixoTabelaIdioma"] != null)
                    _baseUrlUpload = System.Configuration.ConfigurationManager.AppSettings["sufixoTabelaIdioma"].ToString();

                return _baseUrlUpload;
            }
        }

        public static string BaseUrlUpload
        {
            get
            {
                if (System.Configuration.ConfigurationManager.AppSettings["baseUrlUpload"] != null)
                    _baseUrlUpload = System.Configuration.ConfigurationManager.AppSettings["baseUrlUpload"].ToString();

                return _baseUrlUpload;
            }
        }

        public static string Build
        {
            get
            {
                if (System.Configuration.ConfigurationManager.AppSettings["build"] != null)
                    _build = System.Configuration.ConfigurationManager.AppSettings["build"].ToString();

                return _build;
            }
        }


        public static bool EnableMultiLanguage
        {
            get
            {
                if (System.Configuration.ConfigurationManager.AppSettings["enableMultiLanguage"] != null)
                    Boolean.TryParse(System.Configuration.ConfigurationManager.AppSettings["enableMultiLanguage"].ToString(), out _enableMultiLanguage);

                return _enableMultiLanguage;
            }
        }
    }
}
