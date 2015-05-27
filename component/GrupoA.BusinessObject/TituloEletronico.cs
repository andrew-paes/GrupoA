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
	public partial class TituloEletronico 
	{
		private int _tituloEletronicoId;
		private string _isbn13;
		private List<CapituloEletronico> _capituloEletronicos;
		private List<TituloEletronicoAluguel> _tituloEletronicoAlugueis;
		private Produto _produto;
		private Titulo _titulo;

		public int TituloEletronicoId {
			get { return _tituloEletronicoId; }
			set { _tituloEletronicoId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 50)]
		public string Isbn13 {
			get { return _isbn13; }
			set { _isbn13 = value; }
		}

		public List<CapituloEletronico> CapituloEletronicos {
			get { return _capituloEletronicos; }
			set { _capituloEletronicos = value; }
		}

		public List<TituloEletronicoAluguel> TituloEletronicoAlugueis {
			get { return _tituloEletronicoAlugueis; }
			set { _tituloEletronicoAlugueis = value; }
		}

		[NotNullValidator]
		public Produto Produto {
			get { return _produto; }
			set { _produto = value; }
		}

		[NotNullValidator]
		public Titulo Titulo {
			get { return _titulo; }
			set { _titulo = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<TituloEletronico>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<TituloEletronico>(this);
        }
	}
	
	public struct TituloEletronicoColunas
	{	
		public static string TituloEletronicoId = @"tituloEletronicoId";
		public static string Isbn13 = @"isbn13";
		public static string TituloId = @"tituloId";
	}
}
		