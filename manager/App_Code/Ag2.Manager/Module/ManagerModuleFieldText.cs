using System;
using System.Collections.Generic;
using System.Text;

namespace Ag2.Manager.Module
{
    public enum FieldContentType {None,Custom,Currency,CPF,CNPJ,Numeric};

    [Serializable]
    public class ManagerModuleFieldText : ManagerModuleField
    {
        #region Declaracao de Variaveis
        private int _maxlength;
        private FieldContentType _contentType;
        private string _inputMask;
        private string _characterRestriction;
        #endregion

        #region Propriedades

        /// <summary>
        /// Tamanho maximo do campo de texto
        /// </summary>
        public int MaxLength
        {
            get { return _maxlength; }
            set { _maxlength = value; }
        }
        
        public FieldContentType ContentType
        {
            get { return _contentType; }
            set { _contentType = value; }
        }

        public string InputMask
        {
            get { return _inputMask; }
            set { _inputMask = value; }
        }

        public string CharacterRestriction
        {
            get { return _characterRestriction; }
            set { _characterRestriction = value; }
        }

        #endregion
    }
}
