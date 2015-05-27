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
	public partial class Fabricante 
	{
		// Construtor
		public Fabricante() {}

		// Construtor com identificador
		public Fabricante(int fabricanteId) {
			_fabricanteId = fabricanteId;
		}

		private int _fabricanteId;
		private string _nomeFabricante;
		private List<Produto> _produtos;

		public int FabricanteId {
			get { return _fabricanteId; }
			set { _fabricanteId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string NomeFabricante {
			get { return _nomeFabricante; }
			set { _nomeFabricante = value; }
		}

		public List<Produto> Produtos {
			get { return _produtos; }
			set { _produtos = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Fabricante>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Fabricante>(this);
        }
	}
	
	public struct FabricanteColunas
	{	
		public static string FabricanteId = @"fabricanteId";
		public static string NomeFabricante = @"nomeFabricante";
	}
}
		