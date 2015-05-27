using System;
using System.Collections.Generic;
using System.Web;


namespace Ag2.Manager.Entity
{
    /// <summary>
    /// Summary description for QueryCommand
    /// </summary>
    public class FieldSubform
    {
        private string _dataFieldName = string.Empty;
        private string _headerTitle = string.Empty;

        public FieldSubform()
        {
            
        }
  
        public string DataFieldName
        {
            get { return this._dataFieldName; }
            set { this._dataFieldName = value; }
        }

        public string HeaderTitle
        {
            get { return this._headerTitle; }
            set { this._headerTitle = value; }
        }
    }
}
