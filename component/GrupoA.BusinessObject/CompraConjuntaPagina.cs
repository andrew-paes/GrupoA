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
	public partial class CompraConjuntaPagina 
	{
		// Construtor
		public CompraConjuntaPagina() {}

		// Construtor com identificador
		public CompraConjuntaPagina(int compraConjuntaPaginaId) {
			_compraConjuntaPaginaId = compraConjuntaPaginaId;
		}

		private int _compraConjuntaPaginaId;
		private string _pagina;
		private List<CompraConjunta> _compraConjuntas;

		public int CompraConjuntaPaginaId {
			get { return _compraConjuntaPaginaId; }
			set { _compraConjuntaPaginaId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 50)]
		public string Pagina {
			get { return _pagina; }
			set { _pagina = value; }
		}

		public List<CompraConjunta> CompraConjuntas {
			get { return _compraConjuntas; }
			set { _compraConjuntas = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<CompraConjuntaPagina>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<CompraConjuntaPagina>(this);
        }
	}
	
	public struct CompraConjuntaPaginaColunas
	{	
		public static string CompraConjuntaPaginaId = @"compraConjuntaPaginaId";
		public static string Pagina = @"pagina";
	}
}
		