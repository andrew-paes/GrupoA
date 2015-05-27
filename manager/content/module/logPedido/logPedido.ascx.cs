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

public partial class content_module_logPedido_logPedido : System.Web.UI.UserControl
{
    #region Propriedades

    private int _id
    {
        get
        {
            if (Session["_idLogOcorrencia"] == null)
                Session["_idLogOcorrencia"] = 0;

            return (int)Session["_idLogOcorrencia"];
        }
        set
        {
            Session["_idLogOcorrencia"] = (int)value;
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
            LogOcorrencia logOcorrenciaBO = new LogBLL().CarregarLogOcorrencia(new LogOcorrencia() { LogOcorrenciaId = _id });

            if (logOcorrenciaBO != null && logOcorrenciaBO.LogOcorrenciaId > 0)
            {
                if (logOcorrenciaBO.LogEvento != null && logOcorrenciaBO.LogEvento.LogEventoId > 0)
                {
                    this.txtEvento.Text = logOcorrenciaBO.LogEvento.Evento;

                    if (logOcorrenciaBO.LogEvento.LogCategoria != null && logOcorrenciaBO.LogEvento.LogCategoria.LogCategoriaId > 0)
                    {
                        this.txtCategoria.Text = logOcorrenciaBO.LogEvento.LogCategoria.Categoria;
                    }
                }

                this.txtDataHora.Text = logOcorrenciaBO.DataHoraOcorrencia.ToString("dd/MM/yyyy HH:mm:ss");
                this.txtConteudoXML.Text = logOcorrenciaBO.Dados.ToString();
            }
        }
    }

    #endregion
}
