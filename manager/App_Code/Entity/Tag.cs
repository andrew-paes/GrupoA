using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Ag2.Manager.Entity
{
    /// <summary>
    /// Summary description for Tag
    /// </summary>
    public class Tag
    {
        public Tag()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private int _tagId;
        public int tagId
        {
            get { return _tagId; }
            set { _tagId = value; }
        }

        private string _nome;
        public string nome
        {
            get { return _nome; }
            set { _nome = value; }
        }
    }
}
