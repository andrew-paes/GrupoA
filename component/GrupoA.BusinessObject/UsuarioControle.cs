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
	public partial class UsuarioControle 
	{
		private int _usuarioId;
		private DateTime? _dataHoraUltimaSincronia;
		private bool _realizarSincronizacao;
		private string _customerId;
		private string _prospectId;
		private Usuario _usuario;

		public int UsuarioId {
			get { return _usuarioId; }
			set { _usuarioId = value; }
		}

		public DateTime? DataHoraUltimaSincronia {
			get { return _dataHoraUltimaSincronia; }
			set { _dataHoraUltimaSincronia = value; }
		}

		public bool RealizarSincronizacao {
			get { return _realizarSincronizacao; }
			set { _realizarSincronizacao = value; }
		}

		public string CustomerId {
			get { return _customerId; }
			set { _customerId = value; }
		}

		public string ProspectId {
			get { return _prospectId; }
			set { _prospectId = value; }
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
            get { return Validation.Validate<UsuarioControle>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<UsuarioControle>(this);
        }
	}
	
	public struct UsuarioControleColunas
	{	
		public static string UsuarioId = @"usuarioId";
		public static string DataHoraUltimaSincronia = @"dataHoraUltimaSincronia";
		public static string RealizarSincronizacao = @"realizarSincronizacao";
		public static string CustomerId = @"customerId";
		public static string ProspectId = @"prospectId";
	}
}
		