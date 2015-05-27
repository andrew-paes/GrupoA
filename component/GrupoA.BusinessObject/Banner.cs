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
	public partial class Banner 
	{
		// Construtor
		public Banner() {}

		// Construtor com identificador
		public Banner(int bannerId) {
			_bannerId = bannerId;
		}

		private int _bannerId;
		private string _nomeBanner;
		private bool _ativo;
		private DateTime? _dataExibicaoInicio;
		private DateTime? _dataExibicaoFim;
		private string _url;
		private bool _targetBlank;
        private int _tempoExibicao;
		private List<BannerArea> _bannerAreas;
		private Arquivo _arquivo;

		public int BannerId {
			get { return _bannerId; }
			set { _bannerId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 50)]
		public string NomeBanner {
			get { return _nomeBanner; }
			set { _nomeBanner = value; }
		}

		public bool Ativo {
			get { return _ativo; }
			set { _ativo = value; }
		}

		public DateTime? DataExibicaoInicio {
			get { return _dataExibicaoInicio; }
			set { _dataExibicaoInicio = value; }
		}

		public DateTime? DataExibicaoFim {
			get { return _dataExibicaoFim; }
			set { _dataExibicaoFim = value; }
		}

		public string Url {
			get { return _url; }
			set { _url = value; }
		}

		public bool TargetBlank {
			get { return _targetBlank; }
			set { _targetBlank = value; }
		}

        public int TempoExibicao
        {
            get { return _tempoExibicao; }
            set { _tempoExibicao = value; }
        }

		public List<BannerArea> BannerAreas {
			get { return _bannerAreas; }
			set { _bannerAreas = value; }
		}

		public Arquivo Arquivo {
			get { return _arquivo; }
			set { _arquivo = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Banner>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Banner>(this);
        }
	}
	
	public struct BannerColunas
	{	
		public static string BannerId = @"bannerId";
		public static string NomeBanner = @"nomeBanner";
		public static string Ativo = @"ativo";
		public static string DataExibicaoInicio = @"dataExibicaoInicio";
		public static string DataExibicaoFim = @"dataExibicaoFim";
		public static string Url = @"url";
		public static string ArquivoId = @"arquivoId";
		public static string TargetBlank = @"targetBlank";
	}
}
		