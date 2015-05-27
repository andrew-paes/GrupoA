using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web.UI;

namespace Ag2.Manager.Module 
{
    public enum ManagerModuleFieldType { Text, ListBox, EditableListBox, CheckBox, TextArea, Date, Image, File, Hidden, Upload, HtmlTextBox, ImagePreview, Tab, SubForm };
    public enum ManagerModuleMultiSelectType { Single, Multiple };

    [Serializable]
    public class ManagerModuleField : BaseClass
    {
        #region Declaracao de Variaveis
        private string _name;
        private string _label;
        private bool _sort;
        private string _dataFieldName;
        private DbType _dataType;
        private bool _showInList;
        private string _listFieldName;
        private bool _required;
        private ManagerModuleFieldType _type;
        private string _value;
        private string _filterValue;
        private string _filterExpression;
        private Control _formField;
        private string _filterType;
        private bool _translation = false;
        #endregion


        #region Propriedades
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Label
        {
            get { return _label; }
            set { _label = value; }
        }

        public bool Sort
        {
            get { return _sort; }
            set { _sort = value; }
        }

        public bool ShowInList
        {
            get { return _showInList; }
            set { _showInList = value; }
        }

        public string ListFieldName
        {
            get { return _listFieldName; }
            set {
                    if (value.Equals(""))
                    {
                        _listFieldName = _dataFieldName;
                    }
                    else
                    {
                        _listFieldName = value;
                    }
                }
        }

        public bool Required
        {
            get { return _required; }
            set { _required = value; }
        }

        public string DataFieldName
        {
            get { return _dataFieldName; }
            set { _dataFieldName = value; }
        }

        public DbType DataType
        {
            get { return _dataType; }
            set { _dataType = value; }
        }


        public ManagerModuleFieldType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public virtual string Value
        {
            get {return _value; }
            set { _value = value; }

        }

        public string FilterValue
        {
            get { return _filterValue; }
            set { _filterValue = value; }
        }

        public string FilterExpression
        {
            get { return _filterExpression; }
            set { _filterExpression = value; }
        }

        public string FilterType
        {
            get { return _filterType; }
            set { _filterType = value; }
        }

        public Control FormField
        {
            get { return _formField; }
            set { _formField = value; }
        }

        public bool Translation
        {
            get { return _translation; }
            set { _translation = value; }
        }

        #endregion

        public ManagerModuleField()
        {
            _value = "";
            _sort = false;
            _listFieldName = "";
            _dataFieldName = "";
            _filterValue = "";
            _filterExpression = "";
            _filterType = "";
        }
    }
}
