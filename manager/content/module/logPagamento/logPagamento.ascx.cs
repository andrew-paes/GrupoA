using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using GrupoA.GlobalResources;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;

public partial class content_module_logPagamento_logPagamento : System.Web.UI.UserControl
{
    #region Propriedades

    private int _id
    {
        get
        {
            if (Session["_idLogPaymentGateway"] == null)
                Session["_idLogPaymentGateway"] = 0;

            return (int)Session["_idLogPaymentGateway"];
        }
        set
        {
            Session["_idLogPaymentGateway"] = (int)value;
        }
    }

    #endregion

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Util.GetRequestId() > 0)
        {
            _id = Util.GetRequestId();

            if (!IsPostBack)
            {
                LoadForm(_id);
            }
        }
        else
        {

        }
    }

    #endregion

    #region Métodos

    private void LoadForm(int _id)
    {
        if (_id > 0)
        {
            LogPaymentGateway logPaymentGatewayBO = new LogPaymentGatewayBLL().Carregar(new LogPaymentGateway() { LogPaymentGatewayId = _id });
            txtCodigoPedido.Text = logPaymentGatewayBO.CodigoPedido.ToString();
            txtConteudoParametros.Text = logPaymentGatewayBO.ConteudoParametros.Replace("  ", Environment.NewLine);
            txtDataHora.Text = logPaymentGatewayBO.DataHora.ToString("dd/MM/yyyy HH:mm:ss");
            txtConteudoXML.Text = logPaymentGatewayBO.ConteudoXML;
        }
    }

    #endregion
}
