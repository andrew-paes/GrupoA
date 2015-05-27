using System;
using System.Collections.Generic;
using System.Web;

namespace Ag2.Manager.BusinessObject
{
    /// <summary>
    /// Summary description for FileInfo
    /// </summary>
    public class FileInfo
    {
        private string _name;
        private string _format;
        private string _size;
        private string _url;
        private string _extension;
        private string _error = string.Empty;
        private DateTime _creationdate;

        public string Error
        {
            get { return _error; }
            set { _error = value; }
        }

        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }

        public string Extension
        {
            set { _extension = value; }
            get { return _extension; }
        }

        public string Format
        {
            set { _format = value; }
            get { return _format; }
        }

        public string Size
        {
            set { _size = value; }
            get { return _size; }
        }

        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }

        public DateTime CreationDate
        {
            get { return DateTime.Now; }
        }

    }
}