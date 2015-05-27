using System;
using System.Collections.Generic;
using System.Text;

namespace Ag2.Manager.Module
{
    [Serializable]
    public class ManagerModuleFieldUpload : ManagerModuleField
    {
        private string _targetFolder;


        public string TargetFolder
        {
            get { return _targetFolder; }
            set { _targetFolder = value; }
        }
    }
}
