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
	public partial class Perfil 
	{
		// Construtor
		public Perfil() {}

		// Construtor com identificador
		public Perfil(int perfilId) {
			_perfilId = perfilId;
		}

		private int _perfilId;
		private string _perfilNome;
		private List<Promocao> _promocoes;
		private List<Usuario> _usuarios;

		public int PerfilId {
			get { return _perfilId; }
			set { _perfilId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 50)]
		public string PerfilNome {
			get { return _perfilNome; }
			set { _perfilNome = value; }
		}

		public List<Promocao> Promocoes {
			get { return _promocoes; }
			set { _promocoes = value; }
		}

		public List<Usuario> Usuarios {
			get { return _usuarios; }
			set { _usuarios = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Perfil>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Perfil>(this);
        }
	}
	
	public struct PerfilColunas
	{	
		public static string PerfilId = @"perfilId";
		public static string PerfilNome = @"perfilNome";
	}
}
		