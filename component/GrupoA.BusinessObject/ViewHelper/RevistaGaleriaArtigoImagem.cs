/*
'===============================================================================
'
'  Template: Gerador Código C#.csgen
'  Script versão: 0.96
'  Script criado por: Leonardo Alves Lindermann (lindermannla@ag2.com.br)
'  Gerado pelo MyGeneration versão # (???)
'
'===============================================================================
*/
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Text;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace GrupoA.BusinessObject
{

    [Serializable]
    public partial class RevistaGaleriaArtigoImagem
    {
        private int _revistaArtigoId;
        private Arquivo _arquivo;

        public int RevistaArtigoId
        {
            get { return _revistaArtigoId; }
            set { _revistaArtigoId = value; }
        }

        public Arquivo Arquivo
        {
            get { return _arquivo; }
            set { _arquivo = value; }
        }

    }


}
