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
	public partial class PedidoFreteGrupo 
	{
		// Construtor
		public PedidoFreteGrupo() {}

		// Construtor com identificador
		public PedidoFreteGrupo(int pedidoFreteGrupoId) {
			_pedidoFreteGrupoId = pedidoFreteGrupoId;
		}

		private int _pedidoFreteGrupoId;
		private string _nomeGrupo;
		private int _cepInicial1;
		private int _cepInicial2;
		private int _cepFinal1;
		private int _cepFinal2;
		private List<PedidoFretePreco> _pedidoFretePrecos;
		private PedidoFreteTipo _pedidoFreteTipo;

		public int PedidoFreteGrupoId {
			get { return _pedidoFreteGrupoId; }
			set { _pedidoFreteGrupoId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 800)]
		public string NomeGrupo {
			get { return _nomeGrupo; }
			set { _nomeGrupo = value; }
		}

		public int CepInicial1 {
			get { return _cepInicial1; }
			set { _cepInicial1 = value; }
		}

		public int CepInicial2 {
			get { return _cepInicial2; }
			set { _cepInicial2 = value; }
		}

		public int CepFinal1 {
			get { return _cepFinal1; }
			set { _cepFinal1 = value; }
		}

		public int CepFinal2 {
			get { return _cepFinal2; }
			set { _cepFinal2 = value; }
		}

		public List<PedidoFretePreco> PedidoFretePrecos {
			get { return _pedidoFretePrecos; }
			set { _pedidoFretePrecos = value; }
		}

		[NotNullValidator]
		public PedidoFreteTipo PedidoFreteTipo {
			get { return _pedidoFreteTipo; }
			set { _pedidoFreteTipo = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<PedidoFreteGrupo>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<PedidoFreteGrupo>(this);
        }
	}
	
	public struct PedidoFreteGrupoColunas
	{	
		public static string PedidoFreteGrupoId = @"pedidoFreteGrupoId";
		public static string NomeGrupo = @"nomeGrupo";
		public static string PedidoFreteTipoId = @"PedidoFreteTipoId";
		public static string CepInicial1 = @"cepInicial1";
		public static string CepInicial2 = @"cepInicial2";
		public static string CepFinal1 = @"cepFinal1";
		public static string CepFinal2 = @"cepFinal2";
	}
}
		