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
	public partial class CarrinhoStatus 
	{
		// Construtor
		public CarrinhoStatus() {}

		// Construtor com identificador
		public CarrinhoStatus(int carrinhoStatusId) {
			_carrinhoStatusId = carrinhoStatusId;
		}

		private int _carrinhoStatusId;
		private string _statusCarrinho;
		private List<Carrinho> _carrinhos;

		public int CarrinhoStatusId {
			get { return _carrinhoStatusId; }
			set { _carrinhoStatusId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string StatusCarrinho {
			get { return _statusCarrinho; }
			set { _statusCarrinho = value; }
		}

		public List<Carrinho> Carrinhos {
			get { return _carrinhos; }
			set { _carrinhos = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<CarrinhoStatus>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<CarrinhoStatus>(this);
        }
	}
	
	public struct CarrinhoStatusColunas
	{	
		public static string CarrinhoStatusId = @"carrinhoStatusId";
		public static string StatusCarrinho = @"statusCarrinho";
	}
}
		