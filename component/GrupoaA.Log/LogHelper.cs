using System;
using System.Configuration;
using System.IO;

namespace GrupoaA.Log
{
    public class LogHelper
    {
        public LogHelper(String logName) 
        {
            _logName = logName;
        }

        #region Private Members

        //private static LogHelper instance;
        private StreamWriter instaceFileStream;
        private String _logName;
        private String _logFileName;
        private String _logFolder;

        #endregion

        #region Singleton

        //singleton
        //public static LogHelper Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            instance = new LogHelper();
        //        }
        //        return instance;
        //    }
        //} 

        #endregion

        #region Public Properties

        public String LogFileName
        {
            get
            {
                return String.IsNullOrEmpty(_logFileName) ? String.Concat("log", _logName, "_" ,DateTime.Now.ToString("yyyyMMdd"), ".txt") : _logFileName;
            }

            set
            {
                _logFileName = value;
            }
        }

        public String LogFolder
        {
            get
            {
                return String.IsNullOrEmpty(_logFolder) ? ConfigurationManager.AppSettings["pathLogSincronizadorTemp"] : _logFolder;
            }
            set
            {
                _logFolder = value;
            }
        }

        public String LogFilePath
        {
            get
            {
                return Path.Combine(LogFolder, LogFileName);
            }
        }

        public StreamWriter FileStream
        {
            get
            {
                if (instaceFileStream == null)
                {
                    instaceFileStream = new StreamWriter(LogFilePath, true); ;
                }
                return instaceFileStream;
            }
            set
            {
                instaceFileStream = value;
            }
        }

        public Boolean IsFileLocked
        {
            get
            {
                var file = new FileInfo(LogFilePath);
                FileStream stream = null;

                try
                {
                    stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                }
                catch (IOException)
                {
                    //the file is unavailable because it is:
                    //still being written to
                    //or being processed by another thread
                    //or does not exist (has already been processed)
                    return true;
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Close();
                    }
                }

                //file is not locked
                return false;
            }
        } 

        #endregion

        #region Methods

        public void WriteOnFile(string identifyer, string description, string exception = "")
        {
            FileStream.WriteLine(String.Concat(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.FFF"), " | ", identifyer, " | ", description, " | ", exception));
            FileStream.Close();
            FileStream = null;
        } 



        #endregion
    }
}
