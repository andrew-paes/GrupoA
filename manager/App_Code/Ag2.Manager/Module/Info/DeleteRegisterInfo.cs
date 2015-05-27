using System;
using System.Collections.Generic;
using System.Text;

namespace Ag2.Manager.Module.Info
{
    public class DeleteRegisterInfo
    {
        #region PRIVATE
        private string _ID;
        private bool _canDelete;
        private string _errorMessage;
        #endregion
        
        public string ID
        {
            get { return this._ID; }
            set { this._ID = value; }
        }

        public bool CanDelete
        {
            set { this._canDelete = value; }
            get { return this._canDelete; }
        }

        public string ErrorMessage
        {
            set { this._errorMessage = value; }
            get { return this._errorMessage; }
        }
    }

}
