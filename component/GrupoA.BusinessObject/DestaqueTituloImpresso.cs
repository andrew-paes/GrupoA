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
	public partial class DestaqueTituloImpresso 
	{
		// Construtor
		public DestaqueTituloImpresso() {}

		// Construtor com identificador
		public DestaqueTituloImpresso(int destaqueTituloImpressoId) {
			_destaqueTituloImpressoId = destaqueTituloImpressoId;
		}

		private int _destaqueTituloImpressoId;
		private string _nomeArea;
		private List<Titulo> _titulos;

		public int DestaqueTituloImpressoId {
			get { return _destaqueTituloImpressoId; }
			set { _destaqueTituloImpressoId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string NomeArea {
			get { return _nomeArea; }
			set { _nomeArea = value; }
		}

		public List<Titulo> Titulos {
			get { return _titulos; }
			set { _titulos = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<DestaqueTituloImpresso>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<DestaqueTituloImpresso>(this);
        }
	}
	
	public struct DestaqueTituloImpressoColunas
	{	
		public static string DestaqueTituloImpressoId = @"destaqueTituloImpressoId";
		public static string NomeArea = @"nomeArea";
	}
}
		