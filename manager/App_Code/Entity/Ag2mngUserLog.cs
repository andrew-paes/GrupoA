using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ag2.Manager.Entity
{
    /// <summary>
    /// Summary description for Ag2mngUserLog
    /// </summary>
    public class Ag2mngUserLog
    {
        public Ag2mngUserLog()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private int _userId;
        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private DateTime _dataAcesso;
        public DateTime dataAcesso
        {
            get { return _dataAcesso; }
            set { _dataAcesso = value; }
        }

        private string _ipAcesso;
        public string ipAcesso
        {
            get { return _ipAcesso; }
            set { _ipAcesso = value; }
        }

    }
}
