using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ag2.Manager.Helper;

namespace Ag2.Manager.Entity
{
    /// <summary>
    /// Summary description for Ag2mngUserLog
    /// </summary>
    public class ag2mngMenu
    {
        public ag2mngMenu()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public override string ToString()
        {
            return this.moduleName;
        }

        private int _menuId;
        public int menuId
        {
            get { return _menuId; }
            set { _menuId = value; }
        }

        private int _parentMenuId;
        public int parentMenuId
        {
            get { return _parentMenuId; }
            set { _parentMenuId = value; }
        }

        private string _name;
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        private bool _active;
        public bool active
        {
            get { return _active; }
            set { _active = value; }
        }

        private string _tooltip;
        public string tooltip
        {
            get { return _tooltip; }
            set { _tooltip = value; }
        }

        private int _menuOrder;
        public int menuOrder
        {
            get { return _menuOrder; }
            set { _menuOrder = value; }
        }

        private string _moduleName;
        public string moduleName
        {
            get { return _moduleName; }
            set { _moduleName = value; }
        }

        private DateTime _lastEditDate;
        public DateTime LastEditDate
        {
            get { return _lastEditDate; }
            set { _lastEditDate = value; }
        }

        private DateTime _creationDate;
        public DateTime CreationDate
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }

        private bool _fullControl;
        public bool fullControl
        {
            get { return _fullControl; }
            set { _fullControl = value; }
        }

        private bool _canInsert;
        public bool canInsert
        {
            get { return _canInsert; }
            set { _canInsert = value; }
        }

        private bool _canDelete;
        public bool canDelete
        {
            get { return _canDelete; }
            set { _canDelete = value; }
        }

        private bool _canRead;
        public bool canRead
        {
            get { return _canRead; }
            set { _canRead = value; }
        }

        private bool _canUpdate;
        public bool canUpdate
        {
            get { return _canUpdate; }
            set { _canUpdate = value; }
        }

        private bool _canPublish;
        public bool canPublish
        {
            get { return _canPublish; }
            set { _canPublish = value; }
        }

        private List<ag2mngMenu> _menus;
        public List<ag2mngMenu> menus
        {
            get { return _menus; }
            set { _menus = value; }
        }

    }
}
