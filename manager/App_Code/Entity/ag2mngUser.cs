using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ag2.Manager.Entity
{
    /// <summary>
    /// Summary description for ag2mngUser
    /// </summary>
    public class ag2mngUser
    {
        public ag2mngUser()
        {
            _perfis = new List<ag2mngPerfil>();
            _menus = new List<ag2mngMenu>();
        }

        private int _userId;
        public int userId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private string _name;
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _email;
        public string email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _login;
        public string login
        {
            get { return _login; }
            set { _login = value; }
        }

        private string _password;
        public string password
        {
            get { return _password; }
            set { _password = value; }
        }

        private bool _active;
        public bool active
        {
            get { return _active; }
            set { _active = value; }
        }

        private bool _deleted;
        public bool deleted
        {
            get { return _deleted; }
            set { _deleted = value; }
        }

        private List<ag2mngPerfil> _perfis;
        public List<ag2mngPerfil> perfis
        {
            get { return _perfis; }
            set { _perfis = value; }
        }

        private List<ag2mngMenu> _menus;
        public List<ag2mngMenu> Menus
        {
            get { return _menus; }
            set { _menus = value; }
        }

    }
}
