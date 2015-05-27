using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using GrupoA.GlobalResources;

public partial class content_module_midia_midia : System.Web.UI.UserControl
{
    #region Propriedades

    private int _id
    {
        get
        {
            if (Session["_idMidia"] == null)
                Session["_idMidia"] = 0;

            return (int)Session["_idMidia"];
        }
        set
        {
            Session["_idMidia"] = (int)value;
        }
    }

    #endregion

    #region Eventos

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void upArquivo_BindList(object sender, ArquivoEventArgs e)
    {
        this.upArquivo.ArquivoId = e.ArquivoId;
        this.upArquivo.RegistroId = Convert.ToInt32(_id);

        //cria o relacionamento
        if (e.ArquivoId > 0)
        {
            Midia midia = new MidiaBLL().CarregarComDependencia(new Midia() { MidiaId = Convert.ToInt32(_id) });
            Arquivo arquivoDeletar = null;
            if (midia.Arquivo != null)
            {
                arquivoDeletar = midia.Arquivo;
            }
            midia.Arquivo = new ArquivoBLL().CarregarArquivo(new Arquivo() { ArquivoId = this.upArquivo.ArquivoId });
            new MidiaBLL().Atualizar(midia);

            //Redimensiona a imagem pequena
            string imageFile = string.Concat(GrupoA_Resource.baseUrlUpload, GrupoA_Resource.PastaMidia, midia.Arquivo.NomeArquivo);

            if (arquivoDeletar != null)
            {
                DeletarArquivo(arquivoDeletar);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void upArquivo_DeleteItem(object sender, ArquivoEventArgs e)
    {
        Midia midia = new MidiaBLL().CarregarComDependencia(new Midia() { MidiaId = Convert.ToInt32(_id) });

        if (midia != null && midia.Arquivo != null)
        {
            Arquivo arquivo = midia.Arquivo;
            //remove relacionamento
            midia.Arquivo = null;
            new MidiaBLL().Atualizar(midia);

            arquivo = DeletarArquivo(arquivo);

            //atualiza ListFile
            this.upArquivo.RegistroId = Convert.ToInt32(_id);
            this.upArquivo.DataBind();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        this.upArquivo.BindList += new EventHandler<ArquivoEventArgs>(upArquivo_BindList);
        this.upArquivo.DeleteItem += new EventHandler<ArquivoEventArgs>(upArquivo_DeleteItem);
        this.upArquivo.RegistroId = Convert.ToInt32(_id);

        if (Util.GetRequestId() > 0)
        {
            _id = Util.GetRequestId();

            if (!IsPostBack)
            {
                this.montaChecklistCategoria();
                this.montaChecklistRevistas();
                this.CarregarTiposMidia();

                loadForm(_id);
            }
        }
        else
        {
            _id = 0;

            if (!IsPostBack)
            {
                this.montaChecklistCategoria();
                this.montaChecklistRevistas();
                this.CarregarTiposMidia();
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
        Page.MaintainScrollPositionOnPostBack = false;
        SaveOrUpdate();
    }

    #endregion

    #region Métodos

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_id"></param>
    private void loadForm(int _id)
    {
        if (_id > 0)
        {
            this.pnlArquivo.Visible = true;

            Midia midia = new MidiaBLL().CarregarComDependencia(new Midia() { MidiaId = _id });
            txtTitulo.Text = midia.TituloMidia;
            txtDescricao.Text = midia.DescricaoMidia;
            txtUrl.Text = midia.UrlMidia;
            txtAutor.Text = midia.Autor;
            ddlTipo.SelectedValue = midia.MidiaTipo.MidiaTipoId.ToString();
            cbAtivo.Checked = midia.Ativo;

            if (midia.Arquivo != null && midia.Arquivo.ArquivoId > 0)
            {
                this.upArquivo.ArquivoId = midia.Arquivo.ArquivoId;
            }

            List<MidiaCategoria> midiaCategorias = new MidiaBLL().CarregarMidiaCategoriaPorMidia(midia);

            foreach (MidiaCategoria cat in midiaCategorias)
            {
                for (int i = 0; i < cblCategorias.Items.Count; i++)
                {
                    if (cblCategorias.Items[i].Value.Equals(cat.Categoria.CategoriaId.ToString()))
                        cblCategorias.Items[i].Selected = true;
                }
            }

            List<MidiaRevista> midiaRevistas = new MidiaBLL().CarregarMidiaRevistaPorMidia(midia);

            foreach (MidiaRevista rev in midiaRevistas)
            {
                for (int i = 0; i < cblRevistas.Items.Count; i++)
                {
                    if (cblRevistas.Items[i].Value.Equals(rev.Revista.RevistaId.ToString()))
                        cblRevistas.Items[i].Selected = true;
                }
            }

            if (Util.GetQueryString("origem") == "insert")
            {
                Util.ShowInsertMessage();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void SaveOrUpdate()
    {
        if (Page.IsValid)
        {
            if (this.ValidarCategorias())
            {
                Midia midia = new Midia();
                if (_id == 0)
                {
                    midia = new Midia();
                }
                else
                {
                    midia = new MidiaBLL().CarregarComDependencia(new Midia() { MidiaId = Convert.ToInt32(_id) });
                }

                midia.TituloMidia = txtTitulo.Text;
                midia.DescricaoMidia = txtDescricao.Text;

                if (Convert.ToInt32(ddlTipo.SelectedValue.ToString()) == 3)
                {
                    midia.UrlMidia = txtUrl.Text;
                }
                else
                {
                    midia.UrlMidia = null;
                }

                midia.Autor = txtAutor.Text;
                midia.MidiaId = _id;
                midia.MidiaTipo = new MidiaTipo();
                midia.MidiaTipo.MidiaTipoId = Convert.ToInt32(ddlTipo.SelectedValue.ToString());
                midia.Ativo = cbAtivo.Checked;

                List<Categoria> categorias = new List<Categoria>();

                foreach (ListItem li in cblCategorias.Items)
                {
                    if (li.Selected)
                    {
                        Categoria categoria = new Categoria();
                        categoria.CategoriaId = Convert.ToInt32(li.Value);
                        categorias.Add(categoria);
                    }
                }

                List<Revista> revistas = new List<Revista>();

                foreach (ListItem li in cblRevistas.Items)
                {
                    if (li.Selected)
                    {
                        Revista revista = new Revista();
                        revista.RevistaId = Convert.ToInt32(li.Value);
                        revistas.Add(revista);
                    }
                }

                if (_id == 0)
                {
                    new MidiaBLL().Inserir(midia, categorias, revistas);
                    _id = midia.MidiaId;
                    Util.ShowInsertMessage();

                    Ag2.Security.SecureQueryString sqGrid = new Ag2.Security.SecureQueryString();
                    sqGrid["md"] = Util.GetQueryString("md");
                    sqGrid["id"] = midia.MidiaId.ToString();
                    sqGrid["origem"] = "insert";

                    Response.Redirect(string.Format("~/content/edit.aspx?q={0}", sqGrid.ToString()));
                }
                else
                {
                    new MidiaBLL().Atualizar(midia, categorias, revistas);
                    Util.ShowUpdateMessage();
                }
            }
            else
            {
                Util.ShowMessage("Categoria é obrigatório.", Ag2.Manager.Enumerator.typeMessage.Erro);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="arquivo"></param>
    /// <returns></returns>
    private Arquivo DeletarArquivo(Arquivo arquivo)
    {
        arquivo = new ArquivoBLL().CarregarArquivo(arquivo);

        //remove da tabela Arquivo
        new ArquivoBLL().ExcluirArquivo(arquivo);

        //apaga arquivo físico
        string pathFile = string.Concat(Server.MapPath(GrupoA_Resource.baseUrlUpload), GrupoA_Resource.PastaMidia, arquivo.NomeArquivo);
        FileInfo info = new FileInfo(pathFile);
        if (info.Exists)
            info.Delete();

        //string thumbPath = Regex.Replace(pathFile, "(\\.[^\\.]+)$", "-big$1");
        //info = new FileInfo(thumbPath);
        //if (info.Exists)
        //    info.Delete();

        return arquivo;
    }

    /// <summary>
    /// 
    /// </summary>
    private void CarregarTiposMidia()
    {
        ddlTipo.DataTextField = "tipoMidia";
        ddlTipo.DataValueField = "midiaTipoId";

        ddlTipo.DataSource = new MidiaBLL().CarregarTodosTiposMidia();
        ddlTipo.DataBind();
    }

    /// <summary>
    /// 
    /// </summary>
    protected void montaChecklistCategoria()
    {
        cblCategorias.DataSource = new EventoBLL().CarregarTodasCategoriasBase();
        cblCategorias.DataTextField = "nomeCategoria";
        cblCategorias.DataValueField = "categoriaId";
        cblCategorias.DataBind();
    }

    protected void montaChecklistRevistas()
    {
        cblRevistas.DataSource = new RevistaBLL().CarregarTodos();
        cblRevistas.DataTextField = "nomeRevista";
        cblRevistas.DataValueField = "revistaId";
        cblRevistas.DataBind();
    }

    public Boolean ValidarCategorias()
    {
        Boolean retorno = false;

        foreach (ListItem li in cblCategorias.Items)
        {
            if (li.Selected)
            {
                retorno = true;
                break;
            }
        }

        return retorno;
    }

    #endregion
}
