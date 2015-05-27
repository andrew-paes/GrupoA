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
	public partial class ConteudoAvaliacao 
	{
		private int _conteudoId;
		private int? _avaliacoes;
		private Conteudo _conteudo;

		public int ConteudoId {
			get { return _conteudoId; }
			set { _conteudoId = value; }
		}

		public int? Avaliacoes {
			get { return _avaliacoes; }
			set { _avaliacoes = value; }
		}

		[NotNullValidator]
		public Conteudo Conteudo {
			get { return _conteudo; }
			set { _conteudo = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<ConteudoAvaliacao>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<ConteudoAvaliacao>(this);
        }
	}
	
	public struct ConteudoAvaliacaoColunas
	{	
		public static string ConteudoId = @"conteudoId";
		public static string Avaliacoes = @"avaliacoes";
	}
}
		