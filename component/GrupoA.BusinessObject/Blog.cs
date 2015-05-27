using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Text;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace GrupoA.BusinessObject
{
    [Serializable]
    public partial class Blog
    {
        // Construtor
        public Blog() { }

        private String _postId;
        private DateTime _dateCreated;
        private String _title;
        private String _categoryId;

        public String PostId
        {
            get { return _postId; }
            set { _postId = value; }
        }

        public DateTime DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }

        public String Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public String CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; }
        }
    }
}