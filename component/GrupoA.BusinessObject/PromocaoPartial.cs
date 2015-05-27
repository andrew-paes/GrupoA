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
	public partial class Promocao 
	{
		private Boolean _reutilizavel;
        private int? _numeroMaximoCupomDif;

        public Boolean Reutilizavel
        {
            get { return _reutilizavel; }
            set { _reutilizavel = value; }
		}

        public int? NumeroMaximoCupomDif
        {
            get { return _numeroMaximoCupomDif; }
            set { _numeroMaximoCupomDif = value; }
        }
	}
}
		