using System;
using System.Collections.Generic;
using System.Text;

namespace Ag2.Manager.Module
{
    public enum ModuleFormType { List, Edit };

    [Serializable]
    public class ModuleForm
    {
        #region Declaracao de Variaveis
        private ModuleFormType _type;
        private string _fileSource;
        #endregion

        #region Propriedades
        public string FileSource
        {
            get { return _fileSource; }
            set { _fileSource = value; }
        }

        public ModuleFormType Type
        {
            get { return _type; }
            set { _type = value; }
        }
        #endregion
    }
}
