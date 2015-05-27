using System;
using System.Collections.Generic;
using System.Text;

namespace Ag2.Manager.Module
{
    [Serializable]
    public class ManagerModuleFieldListBoxEditable : ManagerModuleField
    {
        private string _dataTextField;
        private string _dataValueField;
        private string _tableName;
        private string _multiSelectTable;
        private IList<string> _selectedItems;

        #region Propriedades

        public IList<string> SelectedItems
        {
            get { return _selectedItems; }
        }

        public override string Value
        {
            get { return _selectedItems.Count > 0 ? _selectedItems[0] : ""; }
            set
            {
                //se não tem registro adiciona
                if (_selectedItems.Count == 0)
                {
                    _selectedItems.Add(value);
                }
                else
                {
                    _selectedItems[0] = value;
                }
            }
        }

        public string DataTextField
        {
            get { return _dataTextField; }
            set { _dataTextField = value; }
        }

        public string DataValueField
        {
            get { return _dataValueField; }
            set { _dataValueField = value; }
        }

        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        public string MultiSelectTable
        {
            get { return _multiSelectTable; }
            set { _multiSelectTable = value; }
        }
        
        #endregion

        public ManagerModuleFieldListBoxEditable()
        {
            _selectedItems = new List<string>();
            _multiSelectTable = "";
        }
    }
}
