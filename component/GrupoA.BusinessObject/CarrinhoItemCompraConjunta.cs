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
	public partial class CarrinhoItemCompraConjunta 
	{
		private int _carrinhoItemCompraConjuntaId;
		private CarrinhoItem _carrinhoItem;
		private CompraConjunta _compraConjunta;

		public int CarrinhoItemCompraConjuntaId {
			get { return _carrinhoItemCompraConjuntaId; }
			set { _carrinhoItemCompraConjuntaId = value; }
		}

		[NotNullValidator]
		public CarrinhoItem CarrinhoItem {
			get { return _carrinhoItem; }
			set { _carrinhoItem = value; }
		}

		[NotNullValidator]
		public CompraConjunta CompraConjunta {
			get { return _compraConjunta; }
			set { _compraConjunta = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<CarrinhoItemCompraConjunta>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<CarrinhoItemCompraConjunta>(this);
        }
	}
	
	public struct CarrinhoItemCompraConjuntaColunas
	{	
		public static string CarrinhoItemCompraConjuntaId = @"carrinhoItemCompraConjuntaId";
		public static string CompraConjuntaId = @"compraConjuntaId";
	}
}
		