using System;
using System.Collections.Generic;
using System.Text;

namespace Ag2.Manager.BusinessObject
{
    public class Idioma
    {
        private int _idiomaId = 0;
        private string _name = string.Empty;
        private bool _active = false;
        private bool _default = false;
        private string _flag = string.Empty;

        public Idioma()
        {
            //
        }

        public Idioma(int idiomaId)
        {
            _idiomaId = idiomaId;
        }

        public int IdiomaId
        {
            get { return _idiomaId; }
            set { _idiomaId = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        public bool Default
        {
            get { return _default; }
            set { _default = value; }
        }

        public string Flag
        {
            get { return _flag; }
            set { _flag = value; }
        }

    }
}
