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
	public partial class TituloEletronicoAluguel 
	{
		private int _tituloEletronicoAluguelId;
		private int? _tempoAluguel;
		private Produto _produto;
		private TituloEletronico _tituloEletronico;

		public int TituloEletronicoAluguelId {
			get { return _tituloEletronicoAluguelId; }
			set { _tituloEletronicoAluguelId = value; }
		}

		public int? TempoAluguel {
			get { return _tempoAluguel; }
			set { _tempoAluguel = value; }
		}

		[NotNullValidator]
		public Produto Produto {
			get { return _produto; }
			set { _produto = value; }
		}

		[NotNullValidator]
		public TituloEletronico TituloEletronico {
			get { return _tituloEletronico; }
			set { _tituloEletronico = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<TituloEletronicoAluguel>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<TituloEletronicoAluguel>(this);
        }
	}
	
	public struct TituloEletronicoAluguelColunas
	{	
		public static string TituloEletronicoAluguelId = @"tituloEletronicoAluguelId";
		public static string TituloEletronicoId = @"tituloEletronicoId";
		public static string TempoAluguel = @"tempoAluguel";
	}
}
		