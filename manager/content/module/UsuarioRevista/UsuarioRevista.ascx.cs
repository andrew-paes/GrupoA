using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;

public partial class content_module_UsuarioRevista_UsuarioRevista : System.Web.UI.UserControl
{
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
            hddUsuarioRevistaId.Value = Util.GetRequestId().ToString();
            if (!IsPostBack)
            {
                this.CarregarRevista();
                loadForm();
            }
        }
        else
        {
            pnlEdicao.Visible = false;
            pnlInclusao.Visible = true;

            if (!IsPostBack)
            {
                txtDataInicio.Text = DateTime.Now.ToShortDateString();

                this.CarregarRevista();
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
        saveOrUpdate();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFiltrar_Click(object sender, ImageClickEventArgs e)
    {
        this.CarregarUsuario();
    }

    #region Validações

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source">Padrão</param>
    /// <param name="args">Padrão</param>
    protected void cvValidarDataInicio_ServerValidate(object source, ServerValidateEventArgs args)
    {
        this.ValidarDataInicio(args);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source">Padrão</param>
    /// <param name="args">Padrão</param>
    protected void cvValidarDataFim_ServerValidate(object source, ServerValidateEventArgs args)
    {
        this.ValidarDataFim(args);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void cvUsuarios_ServerValidate(object source, ServerValidateEventArgs args)
    {
        this.ValidarUsuario(args);
    }

    #endregion

    #endregion

    #region Metodos

    /// <summary>
    /// 
    /// </summary>
    private void CarregarRevista()
    {
        List<Revista> revistas = new RevistaGrupoABLL().CarregarTodasRevistas().ToList();
        revistas.Insert(0, new Revista() { RevistaId = 0, NomeRevista = ":: Selecione ::" });

        ddlRevista.DataSource = revistas;
        ddlRevista.DataTextField = "NomeRevista";
        ddlRevista.DataValueField = "RevistaId";
        ddlRevista.DataBind();

        ddlRevista.SelectedIndex = 0;
    }

    /// <summary>
    /// Salva ou atualiza as informações após validar a página atual.
    /// A informação entre salvar ou atualizar será feita com base no 
    /// campo oculto "hddEventoId".
    /// </summary>
    public void saveOrUpdate()
    {
        if (Page.IsValid)
        {
            UsuarioRevista usuarioRevistaBO = new UsuarioRevista();

            if (hddUsuarioRevistaId.Value == "0")
            {
                DateTime data = Convert.ToDateTime(txtDataFim.Text);
                usuarioRevistaBO.DataFimAssinatura = new DateTime(data.Year, data.Month, data.Day, 23, 59, 59);

                data = Convert.ToDateTime(txtDataInicio.Text);
                usuarioRevistaBO.DataInicioAssinatura = new DateTime(data.Year, data.Month, data.Day, 00, 00, 00);

                usuarioRevistaBO.Usuario = new Usuario();

                foreach (ListItem li in rblUsuario.Items)
                {
                    if (li.Selected)
                    {
                        usuarioRevistaBO.Usuario.UsuarioId = Convert.ToInt32(li.Value);
                        break;
                    }
                }

                usuarioRevistaBO.Revista = new Revista();
                usuarioRevistaBO.Revista.RevistaId = Convert.ToInt32(ddlRevista.SelectedValue);
                usuarioRevistaBO.Revista = new RevistaBLL().Carregar(usuarioRevistaBO.Revista);

                string caminhoTemplate = String.Empty;

                if (usuarioRevistaBO.Revista.RevistaId == 1)
                {
                    caminhoTemplate = System.Configuration.ConfigurationManager.AppSettings["CaminhoEmailAssinaturaBMJ"].ToString();
                }
                else
                {
                    caminhoTemplate = System.Configuration.ConfigurationManager.AppSettings["CaminhoEmailAssinaturaPatio"].ToString();
                }

                new UsuarioRevistaBLL().Inserir(usuarioRevistaBO, caminhoTemplate);

                Ag2.Security.SecureQueryString sqGrid = new Ag2.Security.SecureQueryString();
                sqGrid["md"] = Util.GetQueryString("md");
                sqGrid["id"] = usuarioRevistaBO.UsuarioRevistaId.ToString();
                sqGrid["origem"] = "insert";

                Response.Redirect(string.Format("~/content/edit.aspx?q={0}", sqGrid.ToString()));
            }
            else
            {
                usuarioRevistaBO.UsuarioRevistaId = Convert.ToInt32(hddUsuarioRevistaId.Value);
                usuarioRevistaBO = new UsuarioRevistaBLL().Carregar(usuarioRevistaBO);

                DateTime data = Convert.ToDateTime(txtDataFim.Text);
                usuarioRevistaBO.DataFimAssinatura = new DateTime(data.Year, data.Month, data.Day, 23, 59, 59);

                data = Convert.ToDateTime(txtDataInicio.Text);
                usuarioRevistaBO.DataInicioAssinatura = new DateTime(data.Year, data.Month, data.Day, 00, 00, 00);


                //new UsuarioRevistaBLL().Atualizar(usuarioRevistaBO, System.Configuration.ConfigurationManager.AppSettings["CaminhoEmailAssinatura"].ToString());
                new UsuarioRevistaBLL().Atualizar(usuarioRevistaBO, null);

                Util.ShowUpdateMessage();
            }
        }
    }

    /// <summary>
    /// Carrega o formulário conforme o código contido no campo
    /// oculto "hddEventoId"
    /// </summary>
    public void loadForm()
    {
        pnlEdicao.Visible = true;
        pnlInclusao.Visible = false;

        // Busca dados
        UsuarioRevista usuarioRevista = new UsuarioRevista();
        usuarioRevista.UsuarioRevistaId = int.Parse(hddUsuarioRevistaId.Value);
        usuarioRevista = new UsuarioRevistaBLL().Carregar(usuarioRevista);

        Usuario usuario = new UsuarioBLL().CarregarUsuario(usuarioRevista.Usuario);

        lblNome.Text = usuario.NomeUsuario;
        lblCadastroPessoa.Text = usuario.CadastroPessoa;
        ddlRevista.SelectedValue = usuarioRevista.Revista.RevistaId.ToString();
        txtDataFim.Text = usuarioRevista.DataFimAssinatura.ToShortDateString();
        txtDataInicio.Text = usuarioRevista.DataInicioAssinatura.ToShortDateString();

        ddlRevista.Enabled = false;

        if (Util.GetQueryString("origem") == "insert")
        {
            Util.ShowInsertMessage();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    private void ValidarDataInicio(ServerValidateEventArgs args)
    {
        try
        {
            if (txtDataInicio.Text.Length == 0)
            {
                cvValidarDataInicio.ErrorMessage = "Campo Obrigatório";
                args.IsValid = false;
                return;
            }
            DateTime dtInicio = DateTime.Parse(txtDataInicio.Text);
        }
        catch
        {
            cvValidarDataInicio.ErrorMessage = "Data incorreta";
            args.IsValid = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    private void ValidarDataFim(ServerValidateEventArgs args)
    {
        try
        {
            if (txtDataFim.Text.Length == 0)
            {
                cvValidarDataFim.ErrorMessage = "Campo Obrigatório";
                args.IsValid = false;
                return;
            }
            DateTime dtInicio = DateTime.Parse(txtDataInicio.Text);
            DateTime dtFim = DateTime.Parse(txtDataFim.Text);

            if (dtFim < dtInicio)
            {
                cvValidarDataFim.ErrorMessage = "Data deve ser maior ou igual a data inicial da assinatura";
                args.IsValid = false;
                return;
            }

            if (dtFim < DateTime.Parse(DateTime.Now.ToShortDateString()))
            {
                cvValidarDataFim.ErrorMessage = "Data deve ser maior ou igual a data atual";
                args.IsValid = false;
                return;
            }
        }
        catch
        {
            cvValidarDataFim.ErrorMessage = "Data incorreta";
            args.IsValid = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    private void ValidarUsuario(ServerValidateEventArgs args)
    {
        foreach (ListItem li in rblUsuario.Items)
        {
            if (li.Selected)
            {
                args.IsValid = true;
                return;
            }
        }

        cvUsuarios.ErrorMessage = "Campo Obrigatório";
        args.IsValid = false;
        return;
    }

    /// <summary>
    /// 
    /// </summary>
    private void CarregarUsuario()
    {
        pnlUsuarios.Visible = true;

        rblUsuario.DataTextField = "nomeUsuario";
        rblUsuario.DataValueField = "usuarioId";
        rblUsuario.DataSource = new UsuarioBLL().CarregarUsuarioParaAssinatura(txtCadastroPessoa.Text, txtNome.Text);
        rblUsuario.DataBind();
    }

    #endregion
}