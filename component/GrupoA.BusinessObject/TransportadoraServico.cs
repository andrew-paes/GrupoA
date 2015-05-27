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
	public partial class TransportadoraServico 
	{
		// Construtor
		public TransportadoraServico() {}

		// Construtor com identificador
		public TransportadoraServico(int transportadoraServicoId) {
			_transportadoraServicoId = transportadoraServicoId;
		}

		private int _transportadoraServicoId;
		private string _nomeServicoe;
		private bool _ativo;
		private List<Pedido> _pedidos;
		private Transportadora _transportadora;

		public int TransportadoraServicoId {
			get { return _transportadoraServicoId; }
			set { _transportadoraServicoId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string NomeServicoe {
			get { return _nomeServicoe; }
			set { _nomeServicoe = value; }
		}

		public bool Ativo {
			get { return _ativo; }
			set { _ativo = value; }
		}

		public List<Pedido> Pedidos {
			get { return _pedidos; }
			set { _pedidos = value; }
		}

		[NotNullValidator]
		public Transportadora Transportadora {
			get { return _transportadora; }
			set { _transportadora = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<TransportadoraServico>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<TransportadoraServico>(this);
        }
	}
	
	public struct TransportadoraServicoColunas
	{	
		public static string TransportadoraServicoId = @"transportadoraServicoId";
		public static string TransportadoraId = @"transportadoraId";
		public static string NomeServicoe = @"nomeServicoe";
		public static string Ativo = @"ativo";
	}
}
		