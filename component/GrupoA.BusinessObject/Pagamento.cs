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
	public partial class Pagamento
	{
		// Construtor
		public Pagamento() { }

		// Construtor com identificador
		public Pagamento(int pagamentoId)
		{
			_pagamentoId = pagamentoId;
		}

		private int _pagamentoId;
		private int _numeroParcelas;
		private string _codigoTransacao;
		private string _codigoLegadoMeioPagamentoFaixa;
		private Pedido _pedido;
		private MeioPagamento _meioPagamento;

		public int PagamentoId
		{
			get { return _pagamentoId; }
			set { _pagamentoId = value; }
		}

		public int NumeroParcelas
		{
			get { return _numeroParcelas; }
			set { _numeroParcelas = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string CodigoTransacao
		{
			get { return _codigoTransacao; }
			set { _codigoTransacao = value; }
		}

		public string CodigoLegadoMeioPagamentoFaixa
		{
			get { return _codigoLegadoMeioPagamentoFaixa; }
			set { _codigoLegadoMeioPagamentoFaixa = value; }
		}

		[NotNullValidator]
		public Pedido Pedido
		{
			get { return _pedido; }
			set { _pedido = value; }
		}

		public MeioPagamento MeioPagamento
		{
			get { return _meioPagamento; }
			set { _meioPagamento = value; }
		}

		/// <summary>
		/// Propriedade que informa se a entidade é válida para persistência.
		/// </summary>
		/// <returns>booleano informando se é a entidade é válida ou não.</returns>
		public bool Valido
		{
			get { return Validation.Validate<Pagamento>(this).IsValid; }
		}

		/// <summary>
		/// Método que valida e retorna os dados de validação da entidade.
		/// </summary>
		/// <returns>ValidationResults contendo as informações da validação.</returns>
		public ValidationResults Validar()
		{
			return Validation.Validate<Pagamento>(this);
		}
	}

	public struct PagamentoColunas
	{
		public static string PagamentoId = @"pagamentoId";
		public static string NumeroParcelas = @"numeroParcelas";
		public static string MeioPagamentoId = @"meioPagamentoId";
		public static string CodigoTransacao = @"codigoTransacao";
		public static string CodigoLegadoMeioPagamentoFaixa = @"codigoLegadoMeioPagamentoFaixa";
	}
}
