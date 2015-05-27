using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.BusinessObject.ViewHelper
{
    public partial class TituloSolicitacaoVH : TituloSolicitacao
    {
        private Int32 produtoId;
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

        private Int32 areaId;
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

        private Int32 categoriaId;
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
    }
}
