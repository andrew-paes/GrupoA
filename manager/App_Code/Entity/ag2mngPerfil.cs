using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ag2.Manager.Entity
{
    /// <summary>
    /// Summary description for ag2mngPerfil
    /// </summary>
    public class ag2mngPerfil
    {
        public ag2mngPerfil()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public override string ToString()
        {
            return _name;
        }

        private int _perfilId;
        public int perfilId
        {
            get { return _perfilId; }
            set { _perfilId = value; }
        }

        private string _name;
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        private bool _resetPassword;
        public bool resetPassword
        {
            get { return _resetPassword; }
            set { _resetPassword = value; }
        }

    }
}
