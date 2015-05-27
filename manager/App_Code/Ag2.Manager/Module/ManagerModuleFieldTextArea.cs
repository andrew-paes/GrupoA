using System;
using System.Collections.Generic;
using System.Text;

namespace Ag2.Manager.Module
{
    [Serializable]
    public class ManagerModuleFieldTextArea : ManagerModuleField
    {
        private bool _showHtmlBar;

        public bool ShowHtmlBar
        {
            get { return _showHtmlBar; }
            set { _showHtmlBar = value; }
        }
    }
}
