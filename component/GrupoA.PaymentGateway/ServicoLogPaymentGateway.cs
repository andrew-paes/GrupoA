using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;
using GrupoA.BusinessObject;

namespace GrupoA.PaymentGateway
{
    public class ServicoLogPaymentGateway
    {
        public ServicoLogPaymentGateway() { }

        #region Declarações DAL

        private ILogPaymentGatewayDAL _logPaymentGatewayDal;
        private ILogPaymentGatewayDAL LogPaymentGatewayDal
        {
            get { return _logPaymentGatewayDal ?? (_logPaymentGatewayDal = new LogPaymentGatewayADO()); }
        }

        #endregion

        #region Métodos: LogPaymentGateway

        public void Inserir(Dictionary<String, String> dParametros, Int32? pedidoId)
        {
            this.Inserir(dParametros, null, pedidoId);
        }

        /// <summary>
        /// Método que persiste um LogPaymentGateway.
        /// </summary>
        /// <param name="autor">Objeto LogPaymentGateway com seus dados configurados.</param>
        /// <returns>Objeto LogPaymentGateway passado no método com o identificador configurado.</returns>
        public void Inserir(Dictionary<String, String> dParametros, String xmlParametros, Int32? pedidoId)
        {
            LogPaymentGateway logPaymentGateway = new LogPaymentGateway();
            logPaymentGateway.CodigoPedido = pedidoId;
            logPaymentGateway.ConteudoXML = xmlParametros;

            StringBuilder sbParametros = new StringBuilder();

            foreach (var item in dParametros)
            {
                sbParametros.Append(String.Concat(item.Key.ToString(), "=", item.Value.ToString(), Environment.NewLine));
            }

            logPaymentGateway.ConteudoParametros = sbParametros.ToString();

            LogPaymentGatewayDal.Inserir(logPaymentGateway);
        }

        #endregion
    }
}
