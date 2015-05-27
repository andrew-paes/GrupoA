using System;
using System.Collections.Generic;
using System.Text;
using Ag2.Manager.Entity;

namespace Ag2.Manager.Module
{
    [Serializable]
    public class ManagerModuleFieldSubForm : ManagerModuleField
    {
        private string _query = string.Empty;
        private string _multiSelectTable = string.Empty;
        private ManagerModuleMultiSelectType _multiSelectType;
        private List<FieldSubform> _fields;
        private string _dataValueField = string.Empty;
        private List<string> _selectedItems;

        //Construtor
        public ManagerModuleFieldSubForm()
        {
            _fields = new List<FieldSubform>();
            _selectedItems = new List<string>();
        }

        public List<string> SelectedItems
        {
            get { return _selectedItems; }
            set { _selectedItems = value; }
        }

        public string DataValueField
        {
            get { return _dataValueField; }
            set { _dataValueField = value; }
        }

        public List<FieldSubform> Fields
        {
            get { return _fields; }
            set { _fields = value; }
        }

        public string Query
        {
            get { return this._query; }
            set { this._query = value; }
        }

        public string MultiSelectTable
        {
            get { return _multiSelectTable; }
            set { _multiSelectTable = value; }
        }

        public ManagerModuleMultiSelectType MultiSelectType
        {
            get { return _multiSelectType; }
            set { _multiSelectType = value; }
        }
    }
}
