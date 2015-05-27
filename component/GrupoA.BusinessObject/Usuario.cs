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
	public partial class Usuario 
	{
		// Construtor
		public Usuario() {}

		// Construtor com identificador
		public Usuario(int usuarioId) {
			_usuarioId = usuarioId;
		}

		private int _usuarioId;
		private int _tipoPessoa;
		private string _sexo;
		private bool _ativo;
		private string _nomeUsuario;
		private string _cadastroPessoa;
		private string _emailUsuario;
		private string _login;
		private DateTime? _dataNascimento;
		private DateTime _dataHoraCadastro;
		private bool _optinSMS;
		private bool _optinNewsletter;
		private string _codigoUsuario;
		private string _senha;
		private List<Carrinho> _carrinhos;
		private List<Endereco> _enderecos;
		private List<Enquete> _enquetes;
		private List<EventoAlerta> _eventoAlertas;
		private List<LogOcorrencia> _logOcorrencias;
		private List<NotificacaoDisponibilidade> _notificacaoDisponibilidades;
		private List<Pedido> _pedidos;
		private Professor _professor;
		private List<Promocao> _promocoes;
		private List<Telefone> _telefones;
		private UsuarioControle _usuarioControle;
		private List<Categoria> _categorias;
		private List<Perfil> _perfis;
		private ProfissionalOcupacao _profissionalOcupacao;

		public int UsuarioId {
			get { return _usuarioId; }
			set { _usuarioId = value; }
		}

		public int TipoPessoa {
			get { return _tipoPessoa; }
			set { _tipoPessoa = value; }
		}

		public string Sexo {
			get { return _sexo; }
			set { _sexo = value; }
		}

		public bool Ativo {
			get { return _ativo; }
			set { _ativo = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 200)]
		public string NomeUsuario {
			get { return _nomeUsuario; }
			set { _nomeUsuario = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 50)]
		public string CadastroPessoa {
			get { return _cadastroPessoa; }
			set { _cadastroPessoa = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 200)]
		public string EmailUsuario {
			get { return _emailUsuario; }
			set { _emailUsuario = value; }
		}

		public string Login {
			get { return _login; }
			set { _login = value; }
		}

		public DateTime? DataNascimento {
			get { return _dataNascimento; }
			set { _dataNascimento = value; }
		}

		[NotNullValidator]
		public DateTime DataHoraCadastro {
			get { return _dataHoraCadastro; }
			set { _dataHoraCadastro = value; }
		}

		public bool OptinSMS {
			get { return _optinSMS; }
			set { _optinSMS = value; }
		}

		public bool OptinNewsletter {
			get { return _optinNewsletter; }
			set { _optinNewsletter = value; }
		}

		public string CodigoUsuario {
			get { return _codigoUsuario; }
			set { _codigoUsuario = value; }
		}

		public string Senha {
			get { return _senha; }
			set { _senha = value; }
		}

		public List<Carrinho> Carrinhos {
			get { return _carrinhos; }
			set { _carrinhos = value; }
		}

		public List<Endereco> Enderecos {
			get { return _enderecos; }
			set { _enderecos = value; }
		}

		public List<Enquete> Enquetes {
			get { return _enquetes; }
			set { _enquetes = value; }
		}

		public List<EventoAlerta> EventoAlertas {
			get { return _eventoAlertas; }
			set { _eventoAlertas = value; }
		}

		public List<LogOcorrencia> LogOcorrencias {
			get { return _logOcorrencias; }
			set { _logOcorrencias = value; }
		}

		public List<NotificacaoDisponibilidade> NotificacaoDisponibilidades {
			get { return _notificacaoDisponibilidades; }
			set { _notificacaoDisponibilidades = value; }
		}

		public List<Pedido> Pedidos {
			get { return _pedidos; }
			set { _pedidos = value; }
		}

		[NotNullValidator]
		public Professor Professor {
			get { return _professor; }
			set { _professor = value; }
		}

		public List<Promocao> Promocoes {
			get { return _promocoes; }
			set { _promocoes = value; }
		}

		public List<Telefone> Telefones {
			get { return _telefones; }
			set { _telefones = value; }
		}

		[NotNullValidator]
		public UsuarioControle UsuarioControle {
			get { return _usuarioControle; }
			set { _usuarioControle = value; }
		}

		public List<Categoria> Categorias {
			get { return _categorias; }
			set { _categorias = value; }
		}

		public List<Perfil> Perfis {
			get { return _perfis; }
			set { _perfis = value; }
		}

		public ProfissionalOcupacao ProfissionalOcupacao {
			get { return _profissionalOcupacao; }
			set { _profissionalOcupacao = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Usuario>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Usuario>(this);
        }
	}
	
	public struct UsuarioColunas
	{	
		public static string UsuarioId = @"usuarioId";
		public static string TipoPessoa = @"tipoPessoa";
		public static string Sexo = @"sexo";
		public static string Ativo = @"ativo";
		public static string NomeUsuario = @"nomeUsuario";
		public static string CadastroPessoa = @"cadastroPessoa";
		public static string EmailUsuario = @"emailUsuario";
		public static string Login = @"login";
		public static string DataNascimento = @"dataNascimento";
		public static string DataHoraCadastro = @"dataHoraCadastro";
		public static string OptinSMS = @"optinSMS";
		public static string OptinNewsletter = @"optinNewsletter";
		public static string CodigoUsuario = @"codigoUsuario";
		public static string ProfissionalOcupacaoId = @"profissionalOcupacaoId";
		public static string Senha = @"senha";
	}
}
		