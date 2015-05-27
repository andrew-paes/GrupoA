using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.BusinessObject.ViewHelper
{
    public partial class AbasEstanteVH
    {
        private Boolean lancamento = false;
        public Boolean Lancamento
        {
            get
            {
                return lancamento;
            }
            set
            {
                lancamento = value;
            }
        }

        private Boolean oferta = false;
        public Boolean Oferta
        {
            get
            {
                return oferta;
            }
            set
            {
                oferta = value;
            }
        }

        private Boolean ebook = false;
        public Boolean Ebook
        {
            get
            {
                return ebook;
            }
            set
            {
                ebook = value;
            }
        }

        private Boolean compraColetiva = false;
        public Boolean CompraColetiva
        {
            get
            {
                return compraColetiva;
            }
            set
            {
                compraColetiva = value;
            }
        }

        private Boolean maisVendido = false;
        public Boolean MaisVendido
        {
            get
            {
                return maisVendido;
            }
            set
            {
                maisVendido = value;
            }
        }
    }
}
