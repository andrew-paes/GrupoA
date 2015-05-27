using System;
using System.Collections.Generic;
using System.Web;

namespace Ag2.Manager.Entity
{
    /// <summary>
    /// Summary description for ItemSessao
    /// </summary>
    public class Idioma
    {
        public Idioma()
        {

        }

        private int _idiomaId;
        public int idiomaId
        {
            get { return _idiomaId; }
            set { _idiomaId = value; }
        }

        private string _name;
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        private bool _active;
        public bool active
        {
            get { return _active; }
            set { _active = value; }
        }

        private bool _default;
        public bool Default
        {
            get { return _default; }
            set { _default = value; }
        }

        private string _flag;
        public string flag
        {
            get { return _flag; }
            set { _flag = value; }
        }

    }
}
