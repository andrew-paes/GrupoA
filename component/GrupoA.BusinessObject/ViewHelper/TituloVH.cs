using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.BusinessObject.ViewHelper
{
	public partial class TituloVH : Titulo
	{
		private Int32 produtoId;
        private Int32 areaId;
        private Int32 categoriaId;
        private String autores;
        private String tipo;
        private Arquivo arquivo;
        private Decimal valorUnitario;
        private Decimal valorOferta;
        private Decimal valor;
        private Int32 parcelas;
        private Decimal taxaJuros = 0;
        private Boolean disponivel;

		public Int32 ProdutoId
		{
			get
			{
				return produtoId;
			}
			set
			{
				produtoId = value;
			}
		}

		public Int32 AreaId
		{
			get
			{
				return areaId;
			}
			set
			{
				areaId = value;
			}
		}

		public Int32 CategoriaId
		{
			get
			{
				return categoriaId;
			}
			set
			{
				categoriaId = value;
			}
		}

		public String Autores
		{
			get
			{
				return autores;
			}
			set
			{
				autores = value;
			}
		}

		public String Tipo
		{
			get
			{
				return tipo;
			}
			set
			{
				tipo = value;
			}
		}

		public Arquivo Arquivo
		{
			get
			{
				return arquivo;
			}
			set
			{
				arquivo = value;
			}
		}

		public Decimal ValorUnitario
		{
			get
			{
				return valorUnitario;
			}
			set
			{
				valorUnitario = value;
			}
		}

		public Decimal ValorOferta
		{
			get
			{
				return valorOferta;
			}
			set
			{
				valorOferta = value;
			}
		}

		public Decimal Valor
		{
			get
			{
				return valor;
			}
			set
			{
				valor = value;
			}
		}

		public Int32 Parcelas
		{
			get
			{
				return parcelas;
			}
			set
			{
				parcelas = value;
			}
		}

        public Decimal TaxaJuros
        {
            get
            {
                return taxaJuros;
            }
            set
            {
                taxaJuros = value;
            }
        }

        public Boolean Disponivel
        {
            get
            {
                return disponivel;
            }
            set
            {
                disponivel = value;
            }
        }
	}
}
