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
	public partial class TituloAvaliacao 
	{
		// Construtor
		public TituloAvaliacao() {}

		// Construtor com identificador
		public TituloAvaliacao(int tituloAvaliacaoId) {
			_tituloAvaliacaoId = tituloAvaliacaoId;
		}

		private int _tituloAvaliacaoId;
		private string _avaliacao;
		private bool _finalizada;
		private DateTime? _dataRealizacaoAvaliacao;
		private int _relevanciaObra;
		private string _relevanciaObraObs;
		private int _conteudoAtualizado;
		private string _conteudoAtualizadoObs;
		private int _qualidadeTexto;
		private string _qualidadeTextoObs;
		private int _apresentacaoGrafica;
		private string _apresentacaoGraficaObs;
		private int _materialComplementar;
		private string _materialComplementarObs;
		private int _avaliacaoGeral;
		private string _avaliacaoGeralObs;
		private string _pontosFortes;
		private string _pontosFracos;
		private string _sugestoes;
		private bool _seraAdotada;
		private string _seraAdotadaQuais;
		private bool _seraRecomendada;
		private string _seraRecomendadaQuais;
		private bool _naoAplica;
		private string _naoAplicaPorque;
		private string _naoAplicaAdotada;
		private string _naoAplicaAutor;
		private bool _revisorTecnico;
		private bool _tradutorIngles;
		private bool _tradutorEspanhol;
		private bool _tradutorFrances;
		private bool _tradutorAlemao;
		private string _nomeAvaliador;
		private TituloSolicitacao _tituloSolicitacao;

		public int TituloAvaliacaoId {
			get { return _tituloAvaliacaoId; }
			set { _tituloAvaliacaoId = value; }
		}

		public string Avaliacao {
			get { return _avaliacao; }
			set { _avaliacao = value; }
		}

		public bool Finalizada {
			get { return _finalizada; }
			set { _finalizada = value; }
		}

		public DateTime? DataRealizacaoAvaliacao {
			get { return _dataRealizacaoAvaliacao; }
			set { _dataRealizacaoAvaliacao = value; }
		}

		public int RelevanciaObra {
			get { return _relevanciaObra; }
			set { _relevanciaObra = value; }
		}

		public string RelevanciaObraObs {
			get { return _relevanciaObraObs; }
			set { _relevanciaObraObs = value; }
		}

		public int ConteudoAtualizado {
			get { return _conteudoAtualizado; }
			set { _conteudoAtualizado = value; }
		}

		public string ConteudoAtualizadoObs {
			get { return _conteudoAtualizadoObs; }
			set { _conteudoAtualizadoObs = value; }
		}

		public int QualidadeTexto {
			get { return _qualidadeTexto; }
			set { _qualidadeTexto = value; }
		}

		public string QualidadeTextoObs {
			get { return _qualidadeTextoObs; }
			set { _qualidadeTextoObs = value; }
		}

		public int ApresentacaoGrafica {
			get { return _apresentacaoGrafica; }
			set { _apresentacaoGrafica = value; }
		}

		public string ApresentacaoGraficaObs {
			get { return _apresentacaoGraficaObs; }
			set { _apresentacaoGraficaObs = value; }
		}

		public int MaterialComplementar {
			get { return _materialComplementar; }
			set { _materialComplementar = value; }
		}

		public string MaterialComplementarObs {
			get { return _materialComplementarObs; }
			set { _materialComplementarObs = value; }
		}

		public int AvaliacaoGeral {
			get { return _avaliacaoGeral; }
			set { _avaliacaoGeral = value; }
		}

		public string AvaliacaoGeralObs {
			get { return _avaliacaoGeralObs; }
			set { _avaliacaoGeralObs = value; }
		}

		public string PontosFortes {
			get { return _pontosFortes; }
			set { _pontosFortes = value; }
		}

		public string PontosFracos {
			get { return _pontosFracos; }
			set { _pontosFracos = value; }
		}

		public string Sugestoes {
			get { return _sugestoes; }
			set { _sugestoes = value; }
		}

		public bool SeraAdotada {
			get { return _seraAdotada; }
			set { _seraAdotada = value; }
		}

		public string SeraAdotadaQuais {
			get { return _seraAdotadaQuais; }
			set { _seraAdotadaQuais = value; }
		}

		public bool SeraRecomendada {
			get { return _seraRecomendada; }
			set { _seraRecomendada = value; }
		}

		public string SeraRecomendadaQuais {
			get { return _seraRecomendadaQuais; }
			set { _seraRecomendadaQuais = value; }
		}

		public bool NaoAplica {
			get { return _naoAplica; }
			set { _naoAplica = value; }
		}

		public string NaoAplicaPorque {
			get { return _naoAplicaPorque; }
			set { _naoAplicaPorque = value; }
		}

		public string NaoAplicaAdotada {
			get { return _naoAplicaAdotada; }
			set { _naoAplicaAdotada = value; }
		}

		public string NaoAplicaAutor {
			get { return _naoAplicaAutor; }
			set { _naoAplicaAutor = value; }
		}

		public bool RevisorTecnico {
			get { return _revisorTecnico; }
			set { _revisorTecnico = value; }
		}

		public bool TradutorIngles {
			get { return _tradutorIngles; }
			set { _tradutorIngles = value; }
		}

		public bool TradutorEspanhol {
			get { return _tradutorEspanhol; }
			set { _tradutorEspanhol = value; }
		}

		public bool TradutorFrances {
			get { return _tradutorFrances; }
			set { _tradutorFrances = value; }
		}

		public bool TradutorAlemao {
			get { return _tradutorAlemao; }
			set { _tradutorAlemao = value; }
		}

		public string NomeAvaliador {
			get { return _nomeAvaliador; }
			set { _nomeAvaliador = value; }
		}

		[NotNullValidator]
		public TituloSolicitacao TituloSolicitacao {
			get { return _tituloSolicitacao; }
			set { _tituloSolicitacao = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<TituloAvaliacao>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<TituloAvaliacao>(this);
        }
	}
	
	public struct TituloAvaliacaoColunas
	{	
		public static string TituloAvaliacaoId = @"tituloAvaliacaoId";
		public static string TituloSolicitacaoId = @"tituloSolicitacaoId";
		public static string Avaliacao = @"avaliacao";
		public static string Finalizada = @"finalizada";
		public static string DataRealizacaoAvaliacao = @"dataRealizacaoAvaliacao";
		public static string RelevanciaObra = @"relevanciaObra";
		public static string RelevanciaObraObs = @"relevanciaObraObs";
		public static string ConteudoAtualizado = @"conteudoAtualizado";
		public static string ConteudoAtualizadoObs = @"conteudoAtualizadoObs";
		public static string QualidadeTexto = @"qualidadeTexto";
		public static string QualidadeTextoObs = @"qualidadeTextoObs";
		public static string ApresentacaoGrafica = @"apresentacaoGrafica";
		public static string ApresentacaoGraficaObs = @"apresentacaoGraficaObs";
		public static string MaterialComplementar = @"materialComplementar";
		public static string MaterialComplementarObs = @"materialComplementarObs";
		public static string AvaliacaoGeral = @"avaliacaoGeral";
		public static string AvaliacaoGeralObs = @"avaliacaoGeralObs";
		public static string PontosFortes = @"pontosFortes";
		public static string PontosFracos = @"pontosFracos";
		public static string Sugestoes = @"sugestoes";
		public static string SeraAdotada = @"seraAdotada";
		public static string SeraAdotadaQuais = @"seraAdotadaQuais";
		public static string SeraRecomendada = @"seraRecomendada";
		public static string SeraRecomendadaQuais = @"seraRecomendadaQuais";
		public static string NaoAplica = @"naoAplica";
		public static string NaoAplicaPorque = @"naoAplicaPorque";
		public static string NaoAplicaAdotada = @"naoAplicaAdotada";
		public static string NaoAplicaAutor = @"naoAplicaAutor";
		public static string RevisorTecnico = @"revisorTecnico";
		public static string TradutorIngles = @"tradutorIngles";
		public static string TradutorEspanhol = @"tradutorEspanhol";
		public static string TradutorFrances = @"tradutorFrances";
		public static string TradutorAlemao = @"tradutorAlemao";
		public static string NomeAvaliador = @"nomeAvaliador";
	}
}
		