using System;
using System.Collections.Generic;
using System.Text;

namespace Ag2.Manager.BusinessObject
{
    public class ManagerMenuItem
    {
        #region Declaração de variáveis privadas
        private int _id;
        private string _name;
        private string _destinationUrl;
        private bool _userFullControl;
        private bool _userCanInsert;
        private bool _userCanRead;
        private bool _userCanDelete;
        private bool _userCanUpdate;
        private Int32 _niConteudoId;
        private List<ManagerMenuItem> _childItems;
        #endregion

        /// <summary>
        /// Construtor da Classe
        /// </summary>
        public ManagerMenuItem()
        {
            _childItems = new List<ManagerMenuItem>();
        }


        #region Propriedades
        public bool HasChildItems
        {
            get { return _childItems.Count>0; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string DestinationUrl
        {
            get { return _destinationUrl; }
            set { _destinationUrl = value; }
        }

        public List<ManagerMenuItem> ChildItems
        {
            get { return _childItems; }
        }

        public bool UserHasFullControl
        {
            get { return _userFullControl; }
            set { _userFullControl = value; }
        }

        public bool UserCanRead
        {
            get { return _userCanRead || _userFullControl ; }
            set { _userCanRead = value; }
        }

        public bool UserCanInsert
        {
            get { return _userCanInsert || _userFullControl; }
            set { _userCanInsert = value; }
        }

        public bool UserCanUpdate
        {
            get { return _userCanUpdate || _userFullControl; }
            set { _userCanUpdate = value; }
        }

        public bool UserCanDelete
        {
            get { return _userCanDelete || _userFullControl; }
            set { _userCanDelete = value; }
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }


        public int niConteudoId
        {
            get { return _niConteudoId; }
            set { _niConteudoId = value; }
        }


        #endregion
    }
}
