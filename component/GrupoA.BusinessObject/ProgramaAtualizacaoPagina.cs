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
	public partial class ProgramaAtualizacaoPagina 
	{
		// Construtor
		public ProgramaAtualizacaoPagina() {}

		// Construtor com identificador
		public ProgramaAtualizacaoPagina(int programaAtualizacaoPaginaId) {
			_programaAtualizacaoPaginaId = programaAtualizacaoPaginaId;
		}

		private int _programaAtualizacaoPaginaId;
		private string _pagina;
		private List<ProgramaAtualizacaoChamada> _programaAtualizacaoChamadas;

		public int ProgramaAtualizacaoPaginaId {
			get { return _programaAtualizacaoPaginaId; }
			set { _programaAtualizacaoPaginaId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string Pagina {
			get { return _pagina; }
			set { _pagina = value; }
		}

		public List<ProgramaAtualizacaoChamada> ProgramaAtualizacaoChamadas {
			get { return _programaAtualizacaoChamadas; }
			set { _programaAtualizacaoChamadas = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<ProgramaAtualizacaoPagina>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<ProgramaAtualizacaoPagina>(this);
        }
	}
	
	public struct ProgramaAtualizacaoPaginaColunas
	{	
		public static string ProgramaAtualizacaoPaginaId = @"programaAtualizacaoPaginaId";
		public static string Pagina = @"pagina";
	}
}
		