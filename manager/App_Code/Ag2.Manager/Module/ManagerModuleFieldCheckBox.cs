using System;
using System.Collections.Generic;
using System.Text;

namespace Ag2.Manager.Module
{
    [Serializable]
    public class ManagerModuleFieldCheckBox : ManagerModuleField
    {
        private bool _checked;


        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }
    }
}
