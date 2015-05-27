using System;
using System.Collections.Generic;
using System.Text;

namespace Ag2.Manager.Module
{
    [Serializable]
    public class ManagerModuleFieldListBox : ManagerModuleField
    {
        private string _dataTextField;
        private string _dataValueField;
        private string _dataSource;
        private string _multiSelectTable;
        private ManagerModuleMultiSelectType _multiSelectType;
        private IList<string> _selectedItems;

        private string _filterListBox;          //nome do listbox que será recarregado
        private string _filterByField;          //campo do listbox filtrado que será utilizado na condição where
        private bool _isTargetField;            //indica se e' um combo que e' filtro

        private ManagerModuleFieldListBox _triggerField;    //campo que dispara o reload deste campo 

        #region Propriedades

        public IList<string> SelectedItems
        {
            get { return _selectedItems; }
        }


        public override string Value
        {
            get { return _selectedItems.Count>0 ? _selectedItems[0] : ""; }
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

        public string DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; }
        }

        public string FilterListBox
        {
            get { return _filterListBox; }
            set { _filterListBox = value; }
        }

        public string FilterByField
        {
            get { return _filterByField; }
            set { _filterByField = value; }
        }

        public bool IsTargetField
        {
            get { return _isTargetField; }
            set { _isTargetField = value; }
        }


        public string MultiSelectTable
        {
            get { return _multiSelectTable; }
            set { _multiSelectTable = value; }
        }

        public ManagerModuleFieldListBox TriggerField
        {
            get { return _triggerField; }
            set { _triggerField = value; }
        }

        public ManagerModuleMultiSelectType MultiSelectType
        {
            get { return _multiSelectType; }
            set { _multiSelectType = value; }
        }

        #endregion

        public ManagerModuleFieldListBox()
        {
            _triggerField = null;
            _selectedItems = new List<string>();
            _multiSelectTable = "";
        }
    }
}
