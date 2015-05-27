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
	public partial class UsuarioRevista 
	{
		// Construtor
		public UsuarioRevista() {}

		// Construtor com identificador
		public UsuarioRevista(int usuarioRevistaId) {
			_usuarioRevistaId = usuarioRevistaId;
		}

		private int _usuarioRevistaId;
		private DateTime _dataFimAssinatura;
		private DateTime _dataInicioAssinatura;
		private Revista _revista;
		private Usuario _usuario;

		public int UsuarioRevistaId {
			get { return _usuarioRevistaId; }
			set { _usuarioRevistaId = value; }
		}

		[NotNullValidator]
		public DateTime DataFimAssinatura {
			get { return _dataFimAssinatura; }
			set { _dataFimAssinatura = value; }
		}

		[NotNullValidator]
		public DateTime DataInicioAssinatura {
			get { return _dataInicioAssinatura; }
			set { _dataInicioAssinatura = value; }
		}

		[NotNullValidator]
		public Revista Revista {
			get { return _revista; }
			set { _revista = value; }
		}

		[NotNullValidator]
		public Usuario Usuario {
			get { return _usuario; }
			set { _usuario = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<UsuarioRevista>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<UsuarioRevista>(this);
        }
	}
	
	public struct UsuarioRevistaColunas
	{	
		public static string UsuarioRevistaId = @"usuarioRevistaId";
		public static string UsuarioId = @"usuarioId";
		public static string RevistaId = @"revistaId";
		public static string DataFimAssinatura = @"dataFimAssinatura";
		public static string DataInicioAssinatura = @"dataInicioAssinatura";
	}
}
		