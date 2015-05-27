using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using GrupoA.GlobalResources;
using System.Web;
using System.Text.RegularExpressions;

public partial class content_module_revista_artigo : SmartUserControl
{
    #region Propriedades

    private ArquivoBLL _arquivoBll;
    private ArquivoBLL ArquivoBll
    {
        get { return _arquivoBll ?? (_arquivoBll = new ArquivoBLL()); }
    }

    private RevistaGrupoABLL _revistaGrupoABll;
    public RevistaGrupoABLL RevistaGrupoABll
    {
        get { return _revistaGrupoABll ?? (_revistaGrupoABll = new RevistaGrupoABLL()); }
    }

    #endregion

    #region Eventos

    #region RevistaArtigo

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        this.LoadPage();
    }

    protected void ddlRevistaAux_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CarregarEdicao();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExecute_Click(object sender, EventArgs e)
    {
        this.ExecuteClick();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void ListFiles1_BindList(object sender, ArquivoEventArgs e)
    {
        int arquivoId = Convert.ToInt32(this.hddRevistaArtigoArquivoId.Value);

        RevistaArtigo revistaArtigo = new RevistaGrupoABLL().CarregarRevistaArtigo(new RevistaArtigo() { RevistaArtigoId = Convert.ToInt32(hdnRevistaArtigoId.Value) });

        if (revistaArtigo.ArquivoThumbM != null)
        {
            hddRevistaArtigoArquivoIdDelete.Value = revistaArtigo.ArquivoThumbM.ArquivoId.ToString();
        }
        if (revistaArtigo.ArquivoThumbP != null)
        {
            hddRevistaArtigoArquivoIdDeleteP.Value = revistaArtigo.ArquivoThumbP.ArquivoId.ToString();
        }

        revistaArtigo.ArquivoThumbM = new ArquivoBLL().CarregarArquivo(new Arquivo() { ArquivoId = e.ArquivoId });

        //Redimensiona a imagem pequena
        string imageFile = string.Concat(GrupoA_Resource.baseUrlUpload, GrupoA_Resource.PastaImagensRevista, revistaArtigo.ArquivoThumbM.NomeArquivo);
        string path = HttpContext.Current.Request.MapPath(imageFile);

        string thumbPath = Regex.Replace(path, "(\\.[^\\.]+)$", "P$1");

        using (System.Drawing.Image image = System.Drawing.Image.FromFile(path))
        {
            int bigHeight = image.Height;
            int bigWidth = image.Width;
            if (bigHeight > 70 || bigWidth > 95)
            {
                bigHeight = 70;
                bigWidth = 95;
            }

            image.ResizeTo(bigWidth, bigHeight, ResizeModes.Fit).StreamSave(thumbPath);
        }

        Arquivo arquivoP = null;

        FileInfo info = new FileInfo(thumbPath);
        if (info.Exists)
        {
            arquivoP = new Arquivo();
            arquivoP.NomeArquivo = Regex.Replace(revistaArtigo.ArquivoThumbM.NomeArquivo, "(\\.[^\\.]+)$", "P$1");
            arquivoP.NomeArquivoOriginal = revistaArtigo.ArquivoThumbM.NomeArquivoOriginal;
            arquivoP.TamanhoArquivo = Convert.ToInt32(info.Length);
        }

        if (arquivoP != null)
        {
            new ArquivoBLL().InserirNovoArquivo(arquivoP);
            hddRevistaArtigoArquivoIdP.Value = arquivoP.ArquivoId.ToString();

        }

        this.hddRevistaArtigoArquivoId.Value = e.ArquivoId.ToString();

        this.SaveOrUpdate();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void ListFiles1_DeleteItem(object sender, ArquivoEventArgs e)
    {
        this.hddRevistaArtigoArquivoId.Value = "0";
        this.hddRevistaArtigoArquivoIdP.Value = "0";
        this.ListFiles1.DataBind();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void lfArquivoCapa_BindList(object sender, ArquivoEventArgs e)
    {
        int arquivoId = Convert.ToInt32(hddRevistaArtigoArquivoCapaId.Value);

        if (!e.ArquivoId.ToString().Equals(hddRevistaArtigoArquivoCapaId.Value))
        {
            hddRevistaArtigoArquivoCapaIdDelete.Value = hddRevistaArtigoArquivoCapaId.Value;
        }

        hddRevistaArtigoArquivoCapaId.Value = e.ArquivoId.ToString();

        this.SaveOrUpdate();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void lfArquivoCapa_DeleteItem(object sender, ArquivoEventArgs e)
    {
        hddRevistaArtigoArquivoCapaId.Value = "0";
        lfArquivoCapa.DataBind();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void lfArquivoLateral_BindList(object sender, ArquivoEventArgs e)
    {
        int arquivoId = Convert.ToInt32(hddRevistaArtigoArquivoLateralId.Value);

        if (!e.ArquivoId.ToString().Equals(hddRevistaArtigoArquivoLateralId.Value))
        {
            hddRevistaArtigoArquivoLateralIdDelete.Value = hddRevistaArtigoArquivoLateralId.Value;
        }

        hddRevistaArtigoArquivoLateralId.Value = e.ArquivoId.ToString();

        this.SaveOrUpdate();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void lfArquivoLateral_DeleteItem(object sender, ArquivoEventArgs e)
    {
        hddRevistaArtigoArquivoLateralId.Value = "0";
        lfArquivoLateral.DataBind();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //void ListFiles2_BindList(object sender, ArquivoEventArgs e)
    //{
    //    this.CarregarImagensRevistaArtigoGaleria();
    //}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void ListFiles2_DeleteItem(object sender, ArquivoEventArgs e)
    {
        Arquivo arquivoBO = new RevistaGrupoABLL().CarregarArquivo(new Arquivo() { ArquivoId = Convert.ToInt32(e.ArquivoId) });

        if (arquivoBO != null)
        {
            string pathFile = Server.MapPath(string.Concat(GrupoA_Resource.baseUrlUpload, ListFiles1.TargetFolder, arquivoBO.NomeArquivo));
            FileInfo info = new FileInfo(pathFile);

            if (info.Exists)
            {
                info.Delete();
            }
        }

        RevistaGrupoABLL revistaGrupoABLL = new RevistaGrupoABLL();
        revistaGrupoABLL.ExcluirRevistaArtigoImagem(Convert.ToInt32(hdnRevistaArtigoId.Value), e.ArquivoId, false);
        //this.CarregarImagensRevistaArtigoGaleria();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlEdicao_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.AlteraEdicao();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void cvValidarSecao_ServerValidate(object source, ServerValidateEventArgs args)
    {
        this.ValidarSecao(args);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void cvValidarRevista_ServerValidate(object source, ServerValidateEventArgs args)
    {
        this.ValidarRevista(args);
    }

    #endregion

    #region Controversia



    #endregion

    #region Produtos

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPesquisarProduto_Click(object sender, EventArgs e)
    {
        this.carregarProdutosPorISBN13();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dgProdutosEncontrados_EditCommand(object source, DataGridCommandEventArgs e)
    {
        RevistaArtigo revistaArtigo = new RevistaArtigo() { RevistaArtigoId = Convert.ToInt32(hdnRevistaArtigoId.Value) };
        Produto produto = new Produto() { ProdutoId = Convert.ToInt32(e.CommandArgument) };
        new RevistaArtigoBLL().IncluirRevistaArtigoProduto(revistaArtigo, produto);
        this.carregarGridProdutosAdicionados();
        this.carregarProdutosPorISBN13();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dgProdutosAdicionados_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName.ToUpper() == "DELETE")
        {
            RevistaArtigo revistaArtigo = new RevistaArtigo() { RevistaArtigoId = Convert.ToInt32(hdnRevistaArtigoId.Value) };
            Produto produto = new Produto() { ProdutoId = Convert.ToInt32(e.CommandArgument) };
            new RevistaArtigoBLL().ExcluirRevistaArtigoProduto(revistaArtigo, produto);
            this.carregarGridProdutosAdicionados();
            if (pnlProdutosEncontrados.Visible)
                this.carregarProdutosPorISBN13();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Sender"></param>
    /// <param name="e"></param>
    protected void grids_ItemCreated(Object Sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ImageButton _myButton = (ImageButton)e.Item.FindControl("btnInserir");
            _myButton.Attributes.Add("onclick", "return confirm('Deseja realmente excluir o item selecionado?');");
        }
    }

    #endregion

    #endregion

    #region Métodos

    #region RevistaArtigo

    /// <summary>
    /// 
    /// </summary>
    private void LoadPage()
    {
        this.ListFiles1.BindList += new EventHandler<ArquivoEventArgs>(ListFiles1_BindList);
        this.ListFiles1.DeleteItem += new EventHandler<ArquivoEventArgs>(ListFiles1_DeleteItem);

        //this.ListFiles2.BindList += new EventHandler<ArquivoEventArgs>(ListFiles2_BindList);
        //this.ListFiles2.DeleteItem += new EventHandler<ArquivoEventArgs>(ListFiles2_DeleteItem);

        lfArquivoCapa.BindList += new EventHandler<ArquivoEventArgs>(lfArquivoCapa_BindList);
        lfArquivoCapa.DeleteItem += new EventHandler<ArquivoEventArgs>(lfArquivoCapa_DeleteItem);

        lfArquivoLateral.BindList += new EventHandler<ArquivoEventArgs>(lfArquivoLateral_BindList);
        lfArquivoLateral.DeleteItem += new EventHandler<ArquivoEventArgs>(lfArquivoLateral_DeleteItem);

        if (!IsPostBack)
        {
            this.CarregarRevista();
        }

        if (IdRegistro > 0)
        {
            if (!IsPostBack)
            {
                this.hdnRevistaArtigoId.Value = IdRegistro.ToString();
                montaChecklistCategoria();
                this.LoadForm();
            }

            this.ListFiles1.RegistroId = (int)IdRegistro;
            //this.ListFiles2.RegistroId = (int)IdRegistro;
            lfArquivoCapa.RegistroId = (int)IdRegistro;
            lfArquivoLateral.RegistroId = (int)IdRegistro;
        }
        else
        {
            pnlVisualizacao.Visible = false;

            if (!IsPostBack)
            {
                montaChecklistCategoria();
            }
        }


    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    protected void LoadForm()
    {
        if (IdRegistro > 0)
        {
            RevistaArtigo revistaArtigoBO = RevistaGrupoABll.CarregarRevistaArtigo(new GrupoA.BusinessObject.RevistaArtigo() { RevistaArtigoId = (int)IdRegistro });
            this.hdnRevistaArtigoId.Value = revistaArtigoBO.RevistaArtigoId.ToString();

            if (revistaArtigoBO.ArquivoThumbP != null)
            {
                this.hddRevistaArtigoArquivoIdP.Value = revistaArtigoBO.ArquivoThumbP.ArquivoId.ToString();
            }

            if (revistaArtigoBO.ArquivoThumbM != null)
            {
                Arquivo arquivoBO = ArquivoBll.CarregarArquivo(new Arquivo() { ArquivoId = revistaArtigoBO.ArquivoThumbM.ArquivoId });
                this.hddRevistaArtigoArquivoId.Value = arquivoBO.ArquivoId.ToString();
                this.hddRevistaArtigoArquivoNome.Value = arquivoBO.NomeArquivo.ToString();
                this.ListFiles1.CarregaArquivo(arquivoBO.ArquivoId);
            }

            if (revistaArtigoBO.ArquivoCapa != null)
            {
                Arquivo arquivoBO = ArquivoBll.CarregarArquivo(new Arquivo() { ArquivoId = revistaArtigoBO.ArquivoCapa.ArquivoId });
                hddRevistaArtigoArquivoCapaId.Value = arquivoBO.ArquivoId.ToString();
                hddRevistaArtigoArquivoNome.Value = arquivoBO.NomeArquivo.ToString();
                lfArquivoCapa.CarregaArquivo(arquivoBO.ArquivoId);
            }

            if (revistaArtigoBO.ArquivoLateral != null)
            {
                Arquivo arquivoBO = ArquivoBll.CarregarArquivo(new Arquivo() { ArquivoId = revistaArtigoBO.ArquivoLateral.ArquivoId });
                hddRevistaArtigoArquivoLateralId.Value = arquivoBO.ArquivoId.ToString();
                hddRevistaArtigoArquivoNome.Value = arquivoBO.NomeArquivo.ToString();
                lfArquivoLateral.CarregaArquivo(arquivoBO.ArquivoId);
            }

            this.ddlRevistaAux.SelectedValue = revistaArtigoBO.RevistaSecao.Revista.RevistaId.ToString();

            String revistaUrl = String.Empty;

            if (revistaArtigoBO.RevistaSecao.Revista.RevistaId == 1)
            {
                revistaUrl = "bmj";
            }
            else
            {
                revistaUrl = "patio";
            }

            hlVisualizarArtigo.NavigateUrl = String.Format("{0}revista-{1}/Detalhe_Materia.aspx?revistaArtigoId={2}&origem={3}", System.Configuration.ConfigurationManager.AppSettings["CaminhoSite"], revistaUrl, revistaArtigoBO.RevistaArtigoId, "manager");

            List<RevistaEdicao> revistaEdicoes = new RevistaGrupoABLL().CarregarTodasEdicoesPorRevistaId(Convert.ToInt32(revistaArtigoBO.RevistaSecao.Revista.RevistaId.ToString())).ToList();
            revistaEdicoes.Insert(0, new RevistaEdicao() { NumeroEdicao = null, RevistaEdicaoId = 0 });

            this.ddlEdicao.Enabled = true;
            this.ddlEdicao.DataSource = revistaEdicoes;
            this.ddlEdicao.DataTextField = "numeroEdicao";
            this.ddlEdicao.DataValueField = "RevistaEdicaoId";
            this.ddlEdicao.DataBind();
            if (revistaArtigoBO.RevistaEdicao != null)
            {
                this.ddlEdicao.SelectedValue = revistaArtigoBO.RevistaEdicao.RevistaEdicaoId.ToString();
            }
            else
            {
                this.ddlEdicao.SelectedValue = "0";
            }

            this.ddlSecao.Enabled = true;
            this.ddlSecao.DataSource = new RevistaGrupoABLL().CarregarTodasSecoesPorRevistaId(Convert.ToInt32(revistaArtigoBO.RevistaSecao.Revista.RevistaId.ToString()));
            this.ddlSecao.DataTextField = "nomeSecao";
            this.ddlSecao.DataValueField = "RevistaSecaoId";
            this.ddlSecao.DataBind();
            this.ddlSecao.SelectedValue = revistaArtigoBO.RevistaSecao.RevistaSecaoId.ToString();

            if (revistaArtigoBO.RevistaSecao.NomeSecao.ToLower().Equals("controvérsia") && revistaArtigoBO.RevistaSecao.Revista.RevistaId == 1)
            {
                divSim.Visible = true;
                divNao.Visible = true;

                this.CarregarPosicionamentoSim(revistaArtigoBO.RevistaArtigoId);
                this.CarregarPosicionamentoNao(revistaArtigoBO.RevistaArtigoId);
            }

            this.ddlPermiteVisualizacao.Enabled = true;
            this.ddlPermiteVisualizacao.DataSource = new RevistaGrupoABLL().CarregarTodasRevistaArtigoPermissao();
            this.ddlPermiteVisualizacao.DataTextField = "permissao";
            this.ddlPermiteVisualizacao.DataValueField = "RevistaArtigoPermissaoId";
            this.ddlPermiteVisualizacao.DataBind();
            this.ddlPermiteVisualizacao.SelectedValue = revistaArtigoBO.RevistaArtigoPermissao.RevistaArtigoPermissaoId.ToString();

            chkDestaqueHome.Checked = revistaArtigoBO.DestaqueHome;
            chkDestaquePrincipal.Checked = revistaArtigoBO.DestaquePrincipal;
            //chkConteudoOnline.Checked = revistaArtigoBO.ConteudoOnline;
            chkAtivo.Checked = revistaArtigoBO.Ativo;

            lblArquivoCapaRevistaArtigo.Visible = true;
            this.lblArquivoRevistaArtigo.Visible = true;
            lblArquivoLateralRevistaArtigo.Visible = true;
            //this.lblArquivoRevistaArtigoGaleria.Visible = true;

            this.txtTituloArtigo.Enabled = true;
            //this.txtSubTituloArtigo.Enabled = true;
            //this.txtResumoArtigo.Enabled = true;

            this.txtTituloArtigo.Text = revistaArtigoBO.TituloArtigo;
            //this.txtSubTituloArtigo.Text = revistaArtigoBO.SubTituloArtigo;
            this.txtResumoArtigo.Text = revistaArtigoBO.Resumo;
            this.txtTextoArtigo.Text = revistaArtigoBO.TextoArtigo;
            this.txtAutores.Text = revistaArtigoBO.Autores;

            //this.txtBibliografia.Enabled = true;
            this.txtBibliografia.Text = revistaArtigoBO.Bibliografia;
            this.txtDataPublicacao.Text = revistaArtigoBO.DataPublicacao.ToShortDateString();

            this.ddlArtigoPrincipal.Enabled = true;
            this.ddlArtigoPrincipal.DataSource = new RevistaGrupoABLL().CarregarTodosArtigosPorRevistaEdicaoId(Convert.ToInt32(this.ddlEdicao.SelectedValue));
            this.ddlArtigoPrincipal.DataTextField = "tituloArtigo";
            this.ddlArtigoPrincipal.DataValueField = "RevistaArtigoId";
            this.ddlArtigoPrincipal.DataBind();
            this.ddlArtigoPrincipal.Items.Insert(0, ":: Selecione ::");

            if (revistaArtigoBO.RevistaArtigoAssociado != null)
            {
                this.ddlArtigoPrincipal.SelectedValue = revistaArtigoBO.RevistaArtigoAssociado.RevistaArtigoId.ToString();
            }

            //this.CarregarImagensRevistaArtigoGaleria();

            if (revistaArtigoBO.Conteudo != null)
            {
                // Busca as categorias e as seleciona
                IEnumerable<Categoria> categorias = new EventoBLL().CarregarCategoriasConteudo(revistaArtigoBO.Conteudo);

                foreach (Categoria cat in categorias)
                {
                    for (int i = 0; i < cblCategorias.Items.Count; i++)
                    {
                        if (cblCategorias.Items[i].Value.Equals(cat.CategoriaId.ToString()))
                            cblCategorias.Items[i].Selected = true;
                    }
                }
            }

            pnlRestricaoProduto.Visible = true;
            this.carregarGridProdutosAdicionados();

            if (Util.GetQueryString("origem") == "insert")
            {
                Util.ShowInsertMessage();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void ExecuteClick()
    {
        Page.MaintainScrollPositionOnPostBack = false;
        this.SaveOrUpdate();
    }

    /// <summary>
    /// 
    /// </summary>
    protected void SaveOrUpdate()
    {
        if (Page.IsValid)
        {
            RevistaArtigo revistaArtigoBO = new GrupoA.BusinessObject.RevistaArtigo();

            revistaArtigoBO.RevistaArtigoId = Convert.ToInt32(hdnRevistaArtigoId.Value);

            if (!String.IsNullOrEmpty(this.ddlArtigoPrincipal.SelectedValue) && this.ddlArtigoPrincipal.SelectedIndex != 0)
            {
                revistaArtigoBO.RevistaArtigoAssociado = new RevistaArtigo() { RevistaArtigoId = Convert.ToInt32(this.ddlArtigoPrincipal.SelectedValue) };
            }

            if (ddlEdicao.SelectedValue != null && !String.IsNullOrEmpty(ddlEdicao.SelectedValue.ToString()) && Convert.ToInt32(ddlEdicao.SelectedValue.ToString()) > 0)
            {
                revistaArtigoBO.RevistaEdicao = new RevistaEdicao() { RevistaEdicaoId = Convert.ToInt32(this.ddlEdicao.SelectedValue) };
            }
            else
            {
                revistaArtigoBO.RevistaEdicao = null;
            }

            revistaArtigoBO.TituloArtigo = this.txtTituloArtigo.Text;
            revistaArtigoBO.SubTituloArtigo = null;// this.txtSubTituloArtigo.Text;
            revistaArtigoBO.Resumo = this.txtResumoArtigo.Text;
            revistaArtigoBO.TextoArtigo = this.txtTextoArtigo.Text;
            revistaArtigoBO.Autores = this.txtAutores.Text.Replace("<p>", "").Replace("</p>", "");
            revistaArtigoBO.Bibliografia = this.txtBibliografia.Text;
            revistaArtigoBO.DataPublicacao = Convert.ToDateTime(txtDataPublicacao.Text);

            if (Convert.ToInt32(this.hddRevistaArtigoArquivoId.Value) > 0)
            {
                revistaArtigoBO.ArquivoThumbM = new Arquivo() { ArquivoId = Convert.ToInt32(this.hddRevistaArtigoArquivoId.Value) };
            }

            if (Convert.ToInt32(this.hddRevistaArtigoArquivoIdP.Value) > 0)
            {
                revistaArtigoBO.ArquivoThumbP = new Arquivo() { ArquivoId = Convert.ToInt32(this.hddRevistaArtigoArquivoIdP.Value) };
            }

            if (chkDestaquePrincipal.Checked && Convert.ToInt32(this.hddRevistaArtigoArquivoCapaId.Value) > 0)
            {
                revistaArtigoBO.ArquivoCapa = new Arquivo() { ArquivoId = Convert.ToInt32(this.hddRevistaArtigoArquivoCapaId.Value) };
            }
            else
            {
                revistaArtigoBO.ArquivoCapa = null;
            }

            if (Convert.ToInt32(this.hddRevistaArtigoArquivoLateralId.Value) > 0)
            {
                revistaArtigoBO.ArquivoLateral = new Arquivo() { ArquivoId = Convert.ToInt32(this.hddRevistaArtigoArquivoLateralId.Value) };
            }

            revistaArtigoBO.RevistaSecao = new RevistaSecao() { RevistaSecaoId = Convert.ToInt32(this.ddlSecao.SelectedValue) };

            revistaArtigoBO.RevistaArtigoPermissao = new RevistaArtigoPermissao() { RevistaArtigoPermissaoId = Convert.ToInt32(this.ddlPermiteVisualizacao.SelectedValue) };

            revistaArtigoBO.DestaqueHome = chkDestaqueHome.Checked;
            revistaArtigoBO.DestaquePrincipal = chkDestaquePrincipal.Checked;
            //revistaArtigoBO.ConteudoOnline = chkConteudoOnline.Checked;
            revistaArtigoBO.ConteudoOnline = revistaArtigoBO.RevistaEdicao == null ? true : false;
            revistaArtigoBO.Ativo = chkAtivo.Checked;

            //Popula categorias
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

            if (revistaArtigoBO.RevistaArtigoId > 0)
            {
                List<RevistaArtigoControversia> controversias = null;
                if (((ListItem)ddlSecao.SelectedItem).Text.ToLower().Equals("controvérsia") && ddlRevistaAux.SelectedValue.ToString() == "1")
                {
                    controversias = new List<RevistaArtigoControversia>();
                    controversias.Add(this.PopulaPosicionamentoSim(revistaArtigoBO.RevistaArtigoId));
                    controversias.Add(this.PopulaPosicionamentoNao(revistaArtigoBO.RevistaArtigoId));
                }

                new RevistaGrupoABLL().AtualizarRevistaArtigo(revistaArtigoBO, categorias, controversias);
                Util.ShowUpdateMessage();
            }
            else
            {
                Conteudo conteudo = new Conteudo();
                conteudo.DataHoraCadastro = DateTime.Now;

                ConteudoTipo conteudoTipo = new ConteudoTipo();
                conteudoTipo.ConteudoTipoId = 13;

                conteudo.ConteudoTipo = conteudoTipo;

                ConteudoBLL conteudoBLL = new ConteudoBLL();
                conteudoBLL.InserirConteudo(conteudo);

                RevistaGrupoABLL revistaBLL = new RevistaGrupoABLL();
                revistaArtigoBO.Conteudo = conteudo;
                revistaArtigoBO.RevistaArtigoId = conteudo.ConteudoId;

                revistaBLL.InserirRevistaArtigo(revistaArtigoBO, categorias);
                hdnRevistaArtigoId.Value = revistaArtigoBO.RevistaArtigoId.ToString();

                Ag2.Security.SecureQueryString sqGrid = new Ag2.Security.SecureQueryString();
                sqGrid["md"] = Util.GetQueryString("md");
                sqGrid["id"] = revistaArtigoBO.RevistaArtigoId.ToString();
                sqGrid["origem"] = "insert";

                Response.Redirect(string.Format("~/content/edit.aspx?q={0}", sqGrid.ToString()));
            }

            if (hddRevistaArtigoArquivoIdDelete.Value != "0")
            {
                //Deleta arquivo anterior
                Arquivo arquivoBO = new ArquivoBLL().CarregarArquivo(new Arquivo() { ArquivoId = Convert.ToInt32(hddRevistaArtigoArquivoIdDelete.Value) });

                if (arquivoBO != null)
                {
                    string pathFileDelete = string.Concat(Server.MapPath(GrupoA_Resource.baseUrlUpload), ListFiles1.TargetFolder, arquivoBO.NomeArquivo);
                    FileInfo infoDelete = new FileInfo(pathFileDelete);

                    if (infoDelete.Exists)
                    {
                        infoDelete.Delete();
                        new ArquivoBLL().ExcluirArquivo(arquivoBO);
                    }
                }
            }

            if (hddRevistaArtigoArquivoIdDeleteP.Value != "0")
            {
                //Deleta arquivo anterior
                Arquivo arquivoBO = new ArquivoBLL().CarregarArquivo(new Arquivo() { ArquivoId = Convert.ToInt32(hddRevistaArtigoArquivoIdDeleteP.Value) });

                if (arquivoBO != null)
                {
                    string pathFileDelete = string.Concat(Server.MapPath(GrupoA_Resource.baseUrlUpload), ListFiles1.TargetFolder, arquivoBO.NomeArquivo);
                    FileInfo infoDelete = new FileInfo(pathFileDelete);

                    if (infoDelete.Exists)
                    {
                        infoDelete.Delete();
                        new ArquivoBLL().ExcluirArquivo(arquivoBO);
                    }
                }
            }

            if (hddRevistaArtigoArquivoCapaIdDelete.Value != "0")
            {
                //Deleta arquivo anterior
                Arquivo arquivoBO = new ArquivoBLL().CarregarArquivo(new Arquivo() { ArquivoId = Convert.ToInt32(hddRevistaArtigoArquivoCapaIdDelete.Value) });

                if (arquivoBO != null)
                {
                    string pathFileDelete = string.Concat(Server.MapPath(GrupoA_Resource.baseUrlUpload), lfArquivoCapa.TargetFolder, arquivoBO.NomeArquivo);
                    FileInfo infoDelete = new FileInfo(pathFileDelete);

                    if (infoDelete.Exists)
                    {
                        infoDelete.Delete();
                        new ArquivoBLL().ExcluirArquivo(arquivoBO);
                    }
                }
            }

            if (hddRevistaArtigoArquivoLateralIdDelete.Value != "0")
            {
                //Deleta arquivo anterior
                Arquivo arquivoBO = new ArquivoBLL().CarregarArquivo(new Arquivo() { ArquivoId = Convert.ToInt32(hddRevistaArtigoArquivoLateralIdDelete.Value) });

                if (arquivoBO != null)
                {
                    string pathFileDelete = string.Concat(Server.MapPath(GrupoA_Resource.baseUrlUpload), lfArquivoLateral.TargetFolder, arquivoBO.NomeArquivo);
                    FileInfo infoDelete = new FileInfo(pathFileDelete);

                    if (infoDelete.Exists)
                    {
                        infoDelete.Delete();
                        new ArquivoBLL().ExcluirArquivo(arquivoBO);
                    }
                }
            }
        }
        else
        {
            Util.ShowMessage("Preencha corretamente os campos do formulário com a sinalização vermelha.", Ag2.Manager.Enumerator.typeMessage.Erro);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void CarregarRevista()
    {
        List<Revista> revistas = new RevistaGrupoABLL().CarregarTodasRevistas().ToList();
        revistas.Insert(0, new Revista() { RevistaId = 0, NomeRevista = ":: Selecione ::" });

        ddlRevistaAux.DataSource = revistas;
        ddlRevistaAux.DataTextField = "NomeRevista";
        ddlRevistaAux.DataValueField = "RevistaId";
        ddlRevistaAux.DataBind();

        ddlRevistaAux.SelectedIndex = 0;
    }

    /// <summary>
    /// 
    /// </summary>
    private void AlteraEdicao()
    {
        this.ddlArtigoPrincipal.Enabled = true;
        this.ddlArtigoPrincipal.DataSource = new RevistaGrupoABLL().CarregarTodosArtigosPorRevistaEdicaoId(Convert.ToInt32(this.ddlEdicao.SelectedValue));
        this.ddlArtigoPrincipal.DataTextField = "tituloArtigo";
        this.ddlArtigoPrincipal.DataValueField = "RevistaArtigoId";
        this.ddlArtigoPrincipal.DataBind();
        this.ddlArtigoPrincipal.Items.Insert(0, ":: Selecione ::");

        if (this.ddlArtigoPrincipal.SelectedIndex == -1)
        {
            this.ddlArtigoPrincipal.Enabled = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void CarregarEdicao()
    {
        List<RevistaEdicao> revistaEdicoes = new RevistaGrupoABLL().CarregarTodasEdicoesPorRevistaId(Convert.ToInt32(this.ddlRevistaAux.SelectedValue)).ToList();
        revistaEdicoes.Insert(0, new RevistaEdicao() { NumeroEdicao = null, RevistaEdicaoId = 0 });

        this.ddlEdicao.Enabled = true;
        this.ddlEdicao.DataSource = revistaEdicoes;
        this.ddlEdicao.DataTextField = "numeroEdicao";
        this.ddlEdicao.DataValueField = "RevistaEdicaoId";
        this.ddlEdicao.DataBind();

        this.ddlEdicao.SelectedValue = "0";

        this.ddlSecao.Enabled = true;
        this.ddlSecao.DataSource = new RevistaGrupoABLL().CarregarTodasSecoesPorRevistaId(Convert.ToInt32(this.ddlRevistaAux.SelectedValue));
        this.ddlSecao.DataTextField = "nomeSecao";
        this.ddlSecao.DataValueField = "RevistaSecaoId";
        this.ddlSecao.DataBind();

        this.ddlPermiteVisualizacao.Enabled = true;
        this.ddlPermiteVisualizacao.DataSource = new RevistaGrupoABLL().CarregarTodasRevistaArtigoPermissao();
        this.ddlPermiteVisualizacao.DataTextField = "permissao";
        this.ddlPermiteVisualizacao.DataValueField = "RevistaArtigoPermissaoId";
        this.ddlPermiteVisualizacao.DataBind();

        this.ddlArtigoPrincipal.Enabled = true;
        this.ddlArtigoPrincipal.DataSource = new RevistaGrupoABLL().CarregarTodosArtigosPorRevistaEdicaoId(Convert.ToInt32(this.ddlEdicao.SelectedValue));
        this.ddlArtigoPrincipal.DataTextField = "tituloArtigo";
        this.ddlArtigoPrincipal.DataValueField = "RevistaArtigoId";
        this.ddlArtigoPrincipal.DataBind();
        this.ddlArtigoPrincipal.Items.Insert(0, ":: Selecione ::");

        this.txtTituloArtigo.Enabled = true;
        //this.txtSubTituloArtigo.Enabled = true;
        //this.txtResumoArtigo.Enabled = true;
        //this.txtBibliografia.Enabled = true;
    }

    /// <summary>
    /// Popula a lista de checklists conforme as categorias base (áreas de conhecimento)
    /// </summary>
    protected void montaChecklistCategoria()
    {
        cblCategorias.DataSource = new EventoBLL().CarregarTodasCategoriasBase();
        cblCategorias.DataTextField = "nomeCategoria";
        cblCategorias.DataValueField = "categoriaId";
        cblCategorias.DataBind();
    }

    #endregion

    #region Controversia

    /// <summary>
    /// 
    /// </summary>
    private void CarregarPosicionamentoSim(Int32 revistaArtigoId)
    {
        RevistaArtigoControversia revistaArtigoControversia = new RevistaArtigoControversia();
        revistaArtigoControversia.RevistaArtigoId = revistaArtigoId;
        revistaArtigoControversia.Posicionamento = 1;
        revistaArtigoControversia = new RevistaArtigoControversiaBLL().CarregarPorArtigoIdPosicionamento(revistaArtigoControversia);

        if (revistaArtigoControversia != null)
        {
            hddControversiaSimId.Value = revistaArtigoControversia.RevistaArtigoControversiaId.ToString();
            txtTituloSim.Text = revistaArtigoControversia.TituloControversia;
            txtTextoSim.Text = revistaArtigoControversia.TextoControversia;
            txtAutorSim.Text = revistaArtigoControversia.Autores;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="revistaArtigoId"></param>
    private void CarregarPosicionamentoNao(Int32 revistaArtigoId)
    {
        RevistaArtigoControversia revistaArtigoControversia = new RevistaArtigoControversia();
        revistaArtigoControversia.RevistaArtigoId = revistaArtigoId;
        revistaArtigoControversia.Posicionamento = 2;
        revistaArtigoControversia = new RevistaArtigoControversiaBLL().CarregarPorArtigoIdPosicionamento(revistaArtigoControversia);

        if (revistaArtigoControversia != null)
        {
            hddControversiaNaoId.Value = revistaArtigoControversia.RevistaArtigoControversiaId.ToString();
            txtTituloNao.Text = revistaArtigoControversia.TituloControversia;
            txtTextoNao.Text = revistaArtigoControversia.TextoControversia;
            txtAutorNao.Text = revistaArtigoControversia.Autores;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private RevistaArtigoControversia PopulaPosicionamentoSim(Int32 revistaArtigoId)
    {
        RevistaArtigoControversia revistaArtigoControversia = new RevistaArtigoControversia();
        revistaArtigoControversia.RevistaArtigoId = revistaArtigoId;
        revistaArtigoControversia.Posicionamento = 1;
        revistaArtigoControversia.RevistaArtigoControversiaId = Convert.ToInt32(hddControversiaSimId.Value);

        if (!String.IsNullOrEmpty(txtTituloSim.Text.Trim()))
        {
            revistaArtigoControversia.TituloControversia = txtTituloSim.Text;
        }
        else
        {
            revistaArtigoControversia.TituloControversia = null;
        }

        revistaArtigoControversia.TextoControversia = txtTextoSim.Text;

        if (!String.IsNullOrEmpty(txtAutorSim.Text.Trim()))
        {
            revistaArtigoControversia.Autores = txtAutorSim.Text;
        }
        else
        {
            revistaArtigoControversia.Autores = null;
        }

        return revistaArtigoControversia;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private RevistaArtigoControversia PopulaPosicionamentoNao(Int32 revistaArtigoId)
    {
        RevistaArtigoControversia revistaArtigoControversia = new RevistaArtigoControversia();
        revistaArtigoControversia.RevistaArtigoId = revistaArtigoId;
        revistaArtigoControversia.Posicionamento = 2;
        revistaArtigoControversia.RevistaArtigoControversiaId = Convert.ToInt32(hddControversiaNaoId.Value);

        if (!String.IsNullOrEmpty(txtTituloNao.Text.Trim()))
        {
            revistaArtigoControversia.TituloControversia = txtTituloNao.Text;
        }
        else
        {
            revistaArtigoControversia.TituloControversia = null;
        }

        revistaArtigoControversia.TextoControversia = txtTextoNao.Text;

        if (!String.IsNullOrEmpty(txtAutorNao.Text.Trim()))
        {
            revistaArtigoControversia.Autores = txtAutorNao.Text;
        }
        else
        {
            revistaArtigoControversia.Autores = null;
        }

        return revistaArtigoControversia;
    }

    #endregion

    #region Produtos

    /// <summary>
    /// 
    /// </summary>
    protected void carregarProdutosPorISBN13()
    {
        List<Produto> produtos = new List<Produto>();

        if (dgProdutosAdicionados.DataKeys.Count > 0)
            foreach (int codigo in dgProdutosAdicionados.DataKeys)
                produtos.Add(new Produto() { ProdutoId = codigo });

        produtos = new RevistaArtigoBLL().CarregarProdutosPorIsbn13ExcetoProdutos(txtISBN13.Text, produtos);

        dgProdutosEncontrados.DataSource = produtos;
        dgProdutosEncontrados.DataKeyField = "produtoId";
        dgProdutosEncontrados.DataBind();

        pnlProdutosEncontrados.Visible = true;

        if (produtos.Count > 0)
        {
            lblTextoPesquisaProdutos.Visible = false;
            dgProdutosEncontrados.Visible = true;
        }
        else
        {
            lblTextoPesquisaProdutos.Visible = true;
            dgProdutosEncontrados.Visible = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    protected void carregarGridProdutosAdicionados()
    {
        RevistaArtigo revistaArtigo = new RevistaArtigo() { RevistaArtigoId = Convert.ToInt32(hdnRevistaArtigoId.Value) };
        List<Produto> produtos = new RevistaArtigoBLL().CarregarProdutosPorRevistaArtigo(revistaArtigo);
        dgProdutosAdicionados.DataSource = produtos;
        dgProdutosAdicionados.DataKeyField = "produtoId";
        dgProdutosAdicionados.DataBind();
        if (produtos.Count > 0)
        {
            dgProdutosAdicionados.Visible = true;
            lblTextoProdutosAdicionados.Visible = false;
        }
        else
        {
            dgProdutosAdicionados.Visible = false;
            lblTextoProdutosAdicionados.Visible = true;
        }
    }

    #endregion

    #endregion

    #region Validações

    #region RevistaArtigo

    /// <summary>
    /// Validação do intervalo de Datas da Promocao
    /// </summary>
    /// <param name="source">Padrão</param>
    /// <param name="args">Padrão</param>
    protected void cvValidarData_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            if ((txtDataPublicacao.Text.Length == 0))
            {
                cvValidarData.ErrorMessage = "Data é obrigatória.";
                args.IsValid = false;
                return;
            }
            DateTime dtIni = DateTime.Parse(txtDataPublicacao.Text);
        }
        catch
        {
            cvValidarData.ErrorMessage = "Data incorreta!";
            args.IsValid = false;
            return;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    private void ValidarRevista(ServerValidateEventArgs args)
    {
        if (ddlRevistaAux.SelectedIndex == 0)
        {
            this.cvValidarRevista.ErrorMessage = "Campo Obrigatório";
            args.IsValid = false;
            return;
        }
        else
        {
            this.cvValidarRevista.ErrorMessage = "";
            args.IsValid = true;
            return;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    private void ValidarSecao(ServerValidateEventArgs args)
    {
        if (ddlSecao.SelectedIndex == 0)
        {
            this.cvValidarRevista.ErrorMessage = "Campo Obrigatório";
            args.IsValid = false;
            return;
        }
        else
        {
            this.cvValidarRevista.ErrorMessage = "";
            args.IsValid = true;
            return;
        }
    }

    #endregion

    #region Controversia

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void cvValidarSim_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if ((txtTextoSim.Text.Trim().Length == 0))
        {
            cvValidarSim.ErrorMessage = "Campo obrigatóriao.";
            args.IsValid = false;
            return;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void cvValidarNao_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if ((txtTextoNao.Text.Trim().Length == 0))
        {
            cvValidarNao.ErrorMessage = "Campo obrigatóriao.";
            args.IsValid = false;
            return;
        }
    }

    #endregion

    #endregion
}