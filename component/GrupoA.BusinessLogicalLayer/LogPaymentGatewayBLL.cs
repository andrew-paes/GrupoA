using System.Collections.Generic;
using GrupoA.DataAccess;
using GrupoA.BusinessObject;
using GrupoA.DataAccess.ADO;
using GrupoA.FilterHelper;
using System;
using System.Text;

namespace GrupoA.BusinessLogicalLayer
{
    public class LogPaymentGatewayBLL
    {
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
            new GrupoA.PaymentGateway.ServicoLogPaymentGateway().Inserir(dParametros, xmlParametros, pedidoId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public LogPaymentGateway Carregar(LogPaymentGateway entidade)
        {
            return LogPaymentGatewayDal.Carregar(entidade);
        }

        #endregion
    }
}