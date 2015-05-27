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
	public partial class BannerArea 
	{
		// Construtor
		public BannerArea() {}

		// Construtor com identificador
		public BannerArea(int bannerAreaId) {
			_bannerAreaId = bannerAreaId;
		}

		private int _bannerAreaId;
		private string _area;
		private string _dimensao;
		private List<Banner> _banneres;

		public int BannerAreaId {
			get { return _bannerAreaId; }
			set { _bannerAreaId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 50)]
		public string Area {
			get { return _area; }
			set { _area = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 50)]
		public string Dimensao {
			get { return _dimensao; }
			set { _dimensao = value; }
		}

		public List<Banner> Banneres {
			get { return _banneres; }
			set { _banneres = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<BannerArea>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<BannerArea>(this);
        }
	}
	
	public struct BannerAreaColunas
	{	
		public static string BannerAreaId = @"bannerAreaId";
		public static string Area = @"area";
		public static string Dimensao = @"dimensao";
	}
}
		