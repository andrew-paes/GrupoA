using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using Ag2.Manager.BusinessObject;

namespace Ag2.Manager.Module
{
    [Serializable]
    public class ManagerModuleStructure
    {
        #region Declaraçao de Variaveis
        private bool _allowPaging;
        private bool _allowSorting;
        private bool _hasMultiSelectField;

        private string _name;
        private string _version;
        private string _title;
        private string _description;

        private string _dataBaseName;


        private byte _pageSize;
        private Collection<ManagerModuleField> _fields;
        private Collection<ManagerModuleField> _filters;
        private Collection<ModuleForm> _forms;
        private List<Constraint> _constraints;
        private ModuleEvents _events;

        private string _tableName;
        private string _primaryKeyField;
        private string _sortDefault;

        private string _queryEdit;
        private string _queryList;
        private List<Query> _queries = new List<Query>();
        private List<Option> _options = new List<Option>();
        private bool _multilanguage = false;

        #endregion

        #region Metodos
        /// <summary>
        /// Construtor padrao
        /// </summary>
        public ManagerModuleStructure()
        {
            _queryEdit = "";
            _queryList = "";
            _tableName = "";
            _fields = new Collection<ManagerModuleField>();
            _filters = new Collection<ManagerModuleField>();
            _forms = new Collection<ModuleForm>();
            _constraints = new List<Constraint>();
            _events = new ModuleEvents();
        }
        #endregion

        #region Propriedades

        public bool Multilanguage
        {
            get { return _multilanguage; }
            set { _multilanguage = value; }
        }

        public string DataBaseName
        {
            get { return _dataBaseName; }
            set { _dataBaseName = value; }
        }


        public ModuleEvents Events
        {
            get { return _events; }
            set { _events = value; }
        }

        /// <summary>
        /// Nome do modulo
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Titulo do modulo
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool AllowPaging
        {
            get { return _allowPaging; }
            set { _allowPaging = value; }
        }


        public byte PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        public bool HasFilters
        {
            get { return _filters.Count > 0; }
        }

        public bool HasListforms
        {
            get
            {
                Boolean ret = false;
                for (int i = 0; i < _forms.Count; i++)
                {
                    if (_forms[i].Type == ModuleFormType.List)
                    {
                        ret = true;
                    }
                }
                return ret;
            }
        }

        public bool HasMultiSelectFields
        {

            get
            {
                _hasMultiSelectField = false;
                foreach (ManagerModuleField multiSelectField in _fields)
                {
                    if (multiSelectField.Type == ManagerModuleFieldType.ListBox)
                    {
                        if (((ManagerModuleFieldListBox)multiSelectField).MultiSelectType == ManagerModuleMultiSelectType.Multiple)
                        {
                            _hasMultiSelectField = true;
                            break;
                        }
                    }
                    if (multiSelectField.Type == ManagerModuleFieldType.SubForm)
                    {
                        if (((ManagerModuleFieldSubForm)multiSelectField).MultiSelectType == ManagerModuleMultiSelectType.Multiple)
                        {
                            _hasMultiSelectField = true;
                            break;
                        }
                    }
                }

                return _hasMultiSelectField;
            }
        }

        public Collection<ManagerModuleField> Fields
        {
            get { return _fields; }
            set { _fields = value; }
        }

        public Collection<ManagerModuleField> Filters
        {
            get { return _filters; }
        }

        public Collection<ModuleForm> Forms
        {
            get { return _forms; }
        }

        public List<Constraint> Constraints
        {
            set { _constraints = value; }
            get { return _constraints; }
        }

        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        public string PrimaryKeyField
        {
            get { return _primaryKeyField; }
            set { _primaryKeyField = value; }
        }

        public string SortDefault
        {
            get { return _sortDefault; }
            set { _sortDefault = value; }
        }

        public string QueryEdit
        {
            get { return _queryEdit; }
            set { _queryEdit = value; }
        }

        public List<Query> Queries
        {
            get { return _queries; }
            set { _queries = value; }
        }

        public string QueryList
        {
            get
            {
                //Procura por expressão especial na Query SQL
                Ag2.Manager.Dicionary.SqlClause sqlclause = new Ag2.Manager.Dicionary.SqlClause();

                Regex regexp1 = new Regex("{(.*?)}", (RegexOptions.Compiled | RegexOptions.IgnoreCase));
                MatchCollection matchSQL = regexp1.Matches(_queryList);

                if (matchSQL.Count > 0)
                {
                    foreach (System.Text.RegularExpressions.Group item in matchSQL)
                    {
                        if (!sqlclause.Clauses.ContainsKey(item.ToString().ToUpper()))
                        {
                            throw new Ag2.Manager.Exception.ParameterNotFoundException(item.ToString(), _name);
                        }
                    }

                    foreach (KeyValuePair<string, string> item in sqlclause.Clauses)
                    {
                        Regex regexp2 = new Regex(item.Key, (RegexOptions.Compiled | RegexOptions.IgnoreCase));

                        _queryList = regexp2.Replace(_queryList, item.Value);
                    }
                }

                return _queryList.Trim();
            }
            set { _queryList = value; }
        }

        public bool IsEditCustomForm
        {
            get
            {
                foreach (ModuleForm form in _forms)
                {
                    if (form.Type.Equals(ModuleFormType.Edit))
                    {
                        return true;
                    }
                }
                return false;
            }

        }

        public List<Option> Options
        {
            get { return _options; }
            set { _options = value; }
        }

        #endregion
    }
}
