using System;
using System.Collections.Generic;
using System.Text;
using Ag2.Manager.DAL;
using System.ComponentModel;

namespace Ag2.Manager.BusinessObject
{
    public class Module : IModule
    {
        private string _moduleName = string.Empty;
        private Settings _settings = new Settings();
        private ConfigTable _configTable = new ConfigTable();
        private List<Query> _queries = new List<Query>();
        private List<FormsType> _forms = new List<FormsType>();
        private List<Option> _options = new List<Option>();
        private List<ConfigWebControl> _fields = new List<ConfigWebControl>();
        private List<Filter> _filters = new List<Filter>();
                        
        public Module()
        {
            this.ModuleName = this.GetType().Name;
        }

        public List<ConfigWebControl> Fields
        {
            get { return _fields; }
            set { _fields = value; }
        }

        public ConfigTable ConfigTable
        {
            get { return _configTable; }
            set { _configTable = value; }
        }

        public virtual Settings Settings
        {
            get { return _settings; }
            set { _settings = value; }
        }

        public virtual string ModuleName
        {
            get { return _moduleName; }
            set { _moduleName = value; }
        }

        public List<Query> Queries
        {
            get { return _queries; }
            set { _queries = value; }
        }

        public List<Option> Options
        {
            get { return _options; }
            set { _options = value; }
        }

        public List<FormsType> Forms
        {
            get { return _forms; }
            set { _forms = value; }
        }

        public List<Filter> Filters
        {
            get { return _filters; }
            set { _filters = value; }
        }
    }

    public class Settings
    {
        private string _title = "Título não informado";
        private string _description = "Descrição não informada";
        private bool _paging = true;
        private int _pageSize = 15;

        public Settings()
        {
            //
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public bool Paging
        {
            get { return _paging; }
            set { _paging = value; }
        }

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }
    }

    public class ConfigTable
    {
        private string _primaryKey = string.Empty;
        private string _tableName = string.Empty;

        public ConfigTable()
        {
            //
        }

        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        public string PrimaryKey
        {
            get { return _primaryKey; }
            set { _primaryKey = value; }
        }
                
    }

    public class FormsType
    {
        public enum ControlType
        { 
            control,
            list
        }

        private ControlType _controlType;
        private string _src = string.Empty;

        public FormsType()
        {
            //
        }

        public ControlType Type
        {
            get { return _controlType; }
            set { _controlType = value; }
        }

        public string Src
        {
            get { return _src; }
            set { _src = value; }
        }
    }

    public class ConfigWebControl
    {
        private string _name = string.Empty;
        private string _label = string.Empty;
        private bool _sort = true;
        private string _dataFieldName = string.Empty;
        private bool _showInList = true;
        private int _maxlength = 80;
        private ControlTypeRender _controlType;

        public enum ControlTypeRender
        { 
            TEXTBOX,
            DATEFIELD,
            UPLOADFILE,
            DROPDOWN,
            CHECKBOX,
            RADIOBUTTON
        }

        public ConfigWebControl()
        {
            //
        }

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

        public string DataFieldName
        {
            get { return _dataFieldName; }
            set { _dataFieldName = value; }
        }

        public bool ShowInList
        {
            get { return _showInList; }
            set { _showInList = value; }
        }

        public int Maxlength
        {
            get { return _maxlength; }
            set { _maxlength = value; }
        }

        public ControlTypeRender ControlType
        {
            get { return _controlType; }
            set { _controlType = value; }
        }
    }

    public class Filter
    {
        private string _fieldName = string.Empty;
        private string _label = string.Empty;
        private string _filterExpression = string.Empty;

        public Filter()
        {
            //
        }

        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        public string Label
        {
            get { return _label; }
            set { _label = value; }
        }

        public string FilterExpression
        {
            get { return _filterExpression; }
            set { _filterExpression = value; }
        }
    }

}
