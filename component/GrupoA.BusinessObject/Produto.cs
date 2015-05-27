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
	public partial class Produto 
	{
		private int _produtoId;
		private bool _disponivel;
		private decimal _valorUnitario;
		private decimal? _valorOferta;
		private string _codigoEAN13;
		private string _codigoProduto;
		private bool _exibirSite;
        private bool _homologado;
		private string _nomeProduto;
		private bool _utilizaFrete;
		private decimal _peso;
		private List<AvisoDisponibilidade> _avisoDisponibilidades;
		private CapituloEletronico _capituloEletronico;
		private CapituloImpresso _capituloImpresso;
		private List<CarrinhoItem> _carrinhoItens;
		private List<CompraConjunta> _compraConjuntas;
		private List<NotificacaoDisponibilidade> _notificacaoDisponibilidades;
		private List<PedidoItem> _pedidoItens;
		private List<Categoria> _categorias;
		private List<Produto> _produtoComposicoesCombo;
		private List<Produto> _produtoComposicoesRelacionado;
		private List<ProdutoImagem> _produtoImagens;
		private List<Selo> _selos;
		private List<Promocao> _promocoes;
		private RevistaAssinatura _revistaAssinatura;
		private RevistaEdicao _revistaEdicao;
		private TituloEletronico _tituloEletronico;
		private TituloEletronicoAluguel _tituloEletronicoAluguel;
		private TituloImpresso _tituloImpresso;
		private Conteudo _conteudo;
		private Fabricante _fabricante;
		private ProdutoTipo _produtoTipo;
		private ProdutoFormato _produtoFormato;

		public int ProdutoId {
			get { return _produtoId; }
			set { _produtoId = value; }
		}

		public bool Disponivel {
			get { return _disponivel; }
			set { _disponivel = value; }
		}

		public decimal ValorUnitario {
			get { return _valorUnitario; }
			set { _valorUnitario = value; }
		}

		public decimal? ValorOferta {
			get { return _valorOferta; }
			set { _valorOferta = value; }
		}

		public string CodigoEAN13 {
			get { return _codigoEAN13; }
			set { _codigoEAN13 = value; }
		}

		public string CodigoProduto {
			get { return _codigoProduto; }
			set { _codigoProduto = value; }
		}

		public bool ExibirSite {
			get { return _exibirSite; }
			set { _exibirSite = value; }
		}

        public bool Homologado
        {
            get { return _homologado; }
            set { _homologado = value; }
        }

		[NotNullValidator]
		[StringLengthValidator(0, 200)]
		public string NomeProduto {
			get { return _nomeProduto; }
			set { _nomeProduto = value; }
		}

		public bool UtilizaFrete {
			get { return _utilizaFrete; }
			set { _utilizaFrete = value; }
		}

		public decimal Peso {
			get { return _peso; }
			set { _peso = value; }
		}

		public List<AvisoDisponibilidade> AvisoDisponibilidades {
			get { return _avisoDisponibilidades; }
			set { _avisoDisponibilidades = value; }
		}

		[NotNullValidator]
		public CapituloEletronico CapituloEletronico {
			get { return _capituloEletronico; }
			set { _capituloEletronico = value; }
		}

		[NotNullValidator]
		public CapituloImpresso CapituloImpresso {
			get { return _capituloImpresso; }
			set { _capituloImpresso = value; }
		}

		public List<CarrinhoItem> CarrinhoItens {
			get { return _carrinhoItens; }
			set { _carrinhoItens = value; }
		}

		public List<CompraConjunta> CompraConjuntas {
			get { return _compraConjuntas; }
			set { _compraConjuntas = value; }
		}

		public List<NotificacaoDisponibilidade> NotificacaoDisponibilidades {
			get { return _notificacaoDisponibilidades; }
			set { _notificacaoDisponibilidades = value; }
		}

		public List<PedidoItem> PedidoItens {
			get { return _pedidoItens; }
			set { _pedidoItens = value; }
		}

		public List<Categoria> Categorias {
			get { return _categorias; }
			set { _categorias = value; }
		}

		public List<Produto> ProdutoComposicoesCombo {
			get { return _produtoComposicoesCombo; }
			set { _produtoComposicoesCombo = value; }
		}

		public List<Produto> ProdutoComposicoesRelacionado {
			get { return _produtoComposicoesRelacionado; }
			set { _produtoComposicoesRelacionado = value; }
		}

		public List<ProdutoImagem> ProdutoImagens {
			get { return _produtoImagens; }
			set { _produtoImagens = value; }
		}

		public List<Selo> Selos {
			get { return _selos; }
			set { _selos = value; }
		}

		public List<Promocao> Promocoes {
			get { return _promocoes; }
			set { _promocoes = value; }
		}

		[NotNullValidator]
		public RevistaAssinatura RevistaAssinatura {
			get { return _revistaAssinatura; }
			set { _revistaAssinatura = value; }
		}

		[NotNullValidator]
		public RevistaEdicao RevistaEdicao {
			get { return _revistaEdicao; }
			set { _revistaEdicao = value; }
		}

		[NotNullValidator]
		public TituloEletronico TituloEletronico {
			get { return _tituloEletronico; }
			set { _tituloEletronico = value; }
		}

		[NotNullValidator]
		public TituloEletronicoAluguel TituloEletronicoAluguel {
			get { return _tituloEletronicoAluguel; }
			set { _tituloEletronicoAluguel = value; }
		}

		[NotNullValidator]
		public TituloImpresso TituloImpresso {
			get { return _tituloImpresso; }
			set { _tituloImpresso = value; }
		}

		[NotNullValidator]
		public Conteudo Conteudo {
			get { return _conteudo; }
			set { _conteudo = value; }
		}

		[NotNullValidator]
		public Fabricante Fabricante {
			get { return _fabricante; }
			set { _fabricante = value; }
		}

		[NotNullValidator]
		public ProdutoTipo ProdutoTipo {
			get { return _produtoTipo; }
			set { _produtoTipo = value; }
		}

		[NotNullValidator]
        public ProdutoFormato ProdutoFormato
        {
            get { return _produtoFormato; }
            set { _produtoFormato = value; }
        }

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Produto>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Produto>(this);
        }
	}
	
	public struct ProdutoColunas
	{	
		public static string ProdutoId = @"produtoId";
		public static string ProdutoTipoId = @"produtoTipoId";
		public static string Disponivel = @"disponivel";
		public static string FabricanteId = @"fabricanteId";
		public static string ValorUnitario = @"valorUnitario";
		public static string ValorOferta = @"valorOferta";
		public static string CodigoEAN13 = @"codigoEAN13";
		public static string CodigoProduto = @"codigoProduto";
		public static string ExibirSite = @"exibirSite";
		public static string NomeProduto = @"nomeProduto";
		public static string UtilizaFrete = @"utilizaFrete";
		public static string Peso = @"peso";
	}
}
		