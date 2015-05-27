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
	public partial class RevistaArtigoControversia 
	{
		// Construtor
		public RevistaArtigoControversia() {}

		// Construtor com identificador
		public RevistaArtigoControversia(int revistaArtigoControversiaId) {
			_revistaArtigoControversiaId = revistaArtigoControversiaId;
		}

		private int _revistaArtigoControversiaId;
		private int _revistaArtigoId;
		private int _posicionamento;
		private string _tituloControversia;
		private string _textoControversia;
		private string _autores;

		public int RevistaArtigoControversiaId {
			get { return _revistaArtigoControversiaId; }
			set { _revistaArtigoControversiaId = value; }
		}

		public int RevistaArtigoId {
			get { return _revistaArtigoId; }
			set { _revistaArtigoId = value; }
		}

		public int Posicionamento {
			get { return _posicionamento; }
			set { _posicionamento = value; }
		}

		public string TituloControversia {
			get { return _tituloControversia; }
			set { _tituloControversia = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 1073741823)]
		public string TextoControversia {
			get { return _textoControversia; }
			set { _textoControversia = value; }
		}

		public string Autores {
			get { return _autores; }
			set { _autores = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<RevistaArtigoControversia>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<RevistaArtigoControversia>(this);
        }
	}
	
	public struct RevistaArtigoControversiaColunas
	{	
		public static string RevistaArtigoControversiaId = @"revistaArtigoControversiaId";
		public static string RevistaArtigoId = @"revistaArtigoId";
		public static string Posicionamento = @"posicionamento";
		public static string TituloControversia = @"tituloControversia";
		public static string TextoControversia = @"textoControversia";
		public static string Autores = @"autores";
	}
}
		