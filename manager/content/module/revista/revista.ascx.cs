using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;

public partial class content_module_revistas : System.Web.UI.UserControl
{

    #region Propriedades

    private int _id = 0;

    #endregion

    #region Eventos

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Util.GetRequestId() > 0)
        {
            _id = Util.GetRequestId();
            if (!IsPostBack)
            {
                this.hdnRevistaId.Value = _id.ToString();
                this.loadForm();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExecute_Click(object sender, ImageClickEventArgs e)
    {
        this.Execute();
    }

    protected void cvValidarDescricao_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if ((txtDescricao.Text.Trim().Length == 0))
        {
            cvValidarDescricao.ErrorMessage = "Campo obrigatóriao.";
            args.IsValid = false;
            return;
        }
    }

    #endregion

    #region Métodos

    /// <summary>
    /// 
    /// </summary>
    protected void loadForm()
    {
        if (_id > 0)
        {
            Revista revista = new RevistaGrupoABLL().CarregarRevista(new Revista() { RevistaId = Convert.ToInt32(this.hdnRevistaId.Value) });

            txtNomeRevista.Text = revista.NomeRevista;
            txtIssnRevista.Text = revista.ISSN;
            txtDescricao.Text = revista.DescricaoRevista;
            txtPeriodicidade.Text = revista.Periodicidade.ToString();
            txtPublicoAlvo.Text = revista.PublicoAlvo;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void Execute()
    {
        if (Page.IsValid)
        {
            Revista revista = new RevistaGrupoABLL().CarregarRevista(new Revista() { RevistaId = Convert.ToInt32(this.hdnRevistaId.Value) });

            if (revista.RevistaId > 0)
            {
                revista.DescricaoRevista = txtDescricao.Text;
                revista.Periodicidade = Convert.ToInt32(txtPeriodicidade.Text);
                revista.PublicoAlvo = txtPublicoAlvo.Text;

                new RevistaGrupoABLL().AtualizarRevista(revista);

                Util.ShowUpdateMessage();
            }
        }
    }

    #endregion
    
}
