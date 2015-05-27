using System;
using System.Collections.Generic;
using System.Text;
using Ag2.Manager.Module;

namespace Ag2.Manager.BusinessObject
{
    [Serializable]
    public class Publicacao
    {
        private int _registroId = 0;
        private string _campo = string.Empty;
        private string _valor = string.Empty;
        private ManagerModuleFieldType _type;
        private string _dataValueField = string.Empty;
        private string _dataFieldName = string.Empty;
                        
        public Publicacao()
        {
            //
        }

        public int RegistroId
        {
            get { return _registroId; }
            set { _registroId = value; }
        }

        public string Campo
        {
            get { return _campo; }
            set { _campo = value; }
        }

        public string Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }

        public ManagerModuleFieldType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string DataValueField
        {
            get { return _dataValueField; }
            set { _dataValueField = value; }
        }

        public string DataFieldName
        {
            get { return _dataFieldName; }
            set { _dataFieldName = value; }
        }
    }
}
