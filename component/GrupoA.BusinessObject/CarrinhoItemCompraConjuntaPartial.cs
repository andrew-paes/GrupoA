using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.BusinessObject
{
    public partial class CarrinhoItemCompraConjunta
    {
        private CompraConjuntaDesconto _descontoAtual;

        public CompraConjuntaDesconto DescontoAtual
        {
            get { return _descontoAtual; }
            set { _descontoAtual = value; }
        }

    }
}
