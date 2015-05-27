using System;
using System.Collections.Generic;
using System.Text;

namespace Ag2.Manager.BusinessObject
{
    public class Perfil
    {
        private int _perfilId = 0;
        private string _name = string.Empty;

        public int PerfilId
        {
            get { return _perfilId; }
            set { _perfilId = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Perfil()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
