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
	public partial class Oferta 
	{
		// Construtor
		public Oferta() {}

		// Construtor com identificador
		public Oferta(int ofertaId) {
			_ofertaId = ofertaId;
		}

		private int _ofertaId;
		private decimal? _percentual;
		private decimal? _precoOferta;
		private string _nomeOferta;
		private DateTime _dataHoraInicio;
        private DateTime _dataHoraTermino;
		private List<OfertaCategoria> _ofertaCategorias;
		private List<OfertaProduto> _ofertaProdutos;
		private OfertaTipo _ofertaTipo;

		public int OfertaId {
			get { return _ofertaId; }
			set { _ofertaId = value; }
		}

		public decimal? Percentual {
			get { return _percentual; }
			set { _percentual = value; }
		}

		public decimal? PrecoOferta {
			get { return _precoOferta; }
			set { _precoOferta = value; }
		}

		public string NomeOferta {
			get { return _nomeOferta; }
			set { _nomeOferta = value; }
		}

		[NotNullValidator]
		public DateTime DataHoraInicio {
			get { return _dataHoraInicio; }
			set { _dataHoraInicio = value; }
		}

		public DateTime DataHoraTermino {
			get { return _dataHoraTermino; }
			set { _dataHoraTermino = value; }
		}
        
		public List<OfertaCategoria> OfertaCategorias {
			get { return _ofertaCategorias; }
			set { _ofertaCategorias = value; }
		}

		public List<OfertaProduto> OfertaProdutos {
			get { return _ofertaProdutos; }
			set { _ofertaProdutos = value; }
		}

		[NotNullValidator]
		public OfertaTipo OfertaTipo {
			get { return _ofertaTipo; }
			set { _ofertaTipo = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Oferta>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Oferta>(this);
        }
	}
	
	public struct OfertaColunas
	{	
		public static string OfertaId = @"ofertaId";
		public static string OfertaTipoId = @"ofertaTipoId";
		public static string Percentual = @"percentual";
		public static string PrecoOferta = @"precoOferta";
		public static string NomeOferta = @"nomeOferta";
		public static string DataHoraInicio = @"dataHoraInicio";
		public static string DataHoraTermino = @"dataHoraTermino";
	}
}
		