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
	public partial class Promocao 
	{
		// Construtor
		public Promocao() {}

		// Construtor com identificador
		public Promocao(int promocaoId) {
			_promocaoId = promocaoId;
		}

		private int _promocaoId;
		private string _nomePromocao;
		private string _codigoPromocao;
		private DateTime _dataHoraInicio;
		private DateTime _dataHoraFim;
		private bool _aplicaAutomaticamente;
		private bool _ativa;
		private string _descricaoPromocao;
		private int? _numeroMaximoCupom;
		private bool _origemSistema;
		private List<PedidoItemPromocao> _pedidoItemPromocoes;
		private List<Pedido> _pedidos;
		private List<Categoria> _categorias;
		private List<PromocaoCupom> _promocaoCupons;
		private List<PromocaoFaixa> _promocaoFaixas;
		private List<Perfil> _perfis;
		private List<Produto> _produtos;
		private List<ProdutoTipo> _produtoTipos;
		private List<Revista> _revistas;
		private List<Usuario> _usuarios;
		private PromocaoTipo _promocaoTipo;

		public int PromocaoId {
			get { return _promocaoId; }
			set { _promocaoId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string NomePromocao {
			get { return _nomePromocao; }
			set { _nomePromocao = value; }
		}

		public string CodigoPromocao {
			get { return _codigoPromocao; }
			set { _codigoPromocao = value; }
		}

		[NotNullValidator]
		public DateTime DataHoraInicio {
			get { return _dataHoraInicio; }
			set { _dataHoraInicio = value; }
		}

		[NotNullValidator]
		public DateTime DataHoraFim {
			get { return _dataHoraFim; }
			set { _dataHoraFim = value; }
		}

		public bool AplicaAutomaticamente {
			get { return _aplicaAutomaticamente; }
			set { _aplicaAutomaticamente = value; }
		}

		public bool Ativa {
			get { return _ativa; }
			set { _ativa = value; }
		}

		public string DescricaoPromocao {
			get { return _descricaoPromocao; }
			set { _descricaoPromocao = value; }
		}

		public int? NumeroMaximoCupom {
			get { return _numeroMaximoCupom; }
			set { _numeroMaximoCupom = value; }
		}

		public bool OrigemSistema {
			get { return _origemSistema; }
			set { _origemSistema = value; }
		}

		public List<PedidoItemPromocao> PedidoItemPromocoes {
			get { return _pedidoItemPromocoes; }
			set { _pedidoItemPromocoes = value; }
		}

		public List<Pedido> Pedidos {
			get { return _pedidos; }
			set { _pedidos = value; }
		}

		public List<Categoria> Categorias {
			get { return _categorias; }
			set { _categorias = value; }
		}

		public List<PromocaoCupom> PromocaoCupons {
			get { return _promocaoCupons; }
			set { _promocaoCupons = value; }
		}

		public List<PromocaoFaixa> PromocaoFaixas {
			get { return _promocaoFaixas; }
			set { _promocaoFaixas = value; }
		}

		public List<Perfil> Perfis {
			get { return _perfis; }
			set { _perfis = value; }
		}

		public List<Produto> Produtos {
			get { return _produtos; }
			set { _produtos = value; }
		}

		public List<ProdutoTipo> ProdutoTipos {
			get { return _produtoTipos; }
			set { _produtoTipos = value; }
		}

		public List<Revista> Revistas {
			get { return _revistas; }
			set { _revistas = value; }
		}

		public List<Usuario> Usuarios {
			get { return _usuarios; }
			set { _usuarios = value; }
		}

		[NotNullValidator]
		public PromocaoTipo PromocaoTipo {
			get { return _promocaoTipo; }
			set { _promocaoTipo = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Promocao>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Promocao>(this);
        }
	}
	
	public struct PromocaoColunas
	{	
		public static string PromocaoId = @"promocaoId";
		public static string NomePromocao = @"nomePromocao";
		public static string CodigoPromocao = @"codigoPromocao";
		public static string DataHoraInicio = @"dataHoraInicio";
		public static string DataHoraFim = @"dataHoraFim";
		public static string AplicaAutomaticamente = @"aplicaAutomaticamente";
		public static string PromocaoTipoId = @"promocaoTipoId";
		public static string Ativa = @"ativa";
		public static string DescricaoPromocao = @"descricaoPromocao";
		public static string NumeroMaximoCupom = @"numeroMaximoCupom";
		public static string OrigemSistema = @"origemSistema";
	}
}
		