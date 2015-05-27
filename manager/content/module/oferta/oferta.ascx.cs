using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using Ag2.Manager.Helper;
using GrupoA.BusinessObject.Enumerator;

[Serializable()] 
public class OfertaProdutoGrid
{
    public Int32 ProdutoId { get; set; }
    public String NomeProduto { get; set; }
}

public partial class content_module_oferta_oferta : System.Web.UI.UserControl
{
    public List<OfertaProdutoGrid> OfertaProdutosGrid
    {
        get
        {
            return ViewState["OfertaProdutosGrid"] as List<OfertaProdutoGrid>;
        }
        set
        {
            ViewState["OfertaProdutosGrid"] = value;
        }
    }

    #region Eventos

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.CarregarOfertaTipos();
            this.MontaChecklistPagina();
        }

        if (Util.GetRequestId() > 0)
        {
            var _id = Util.GetRequestId();

            if (!IsPostBack)
            {
                hddOfertaId.Value = _id.ToString();
                this.MontaChecklistPagina();
                this.MontaTitulosAdicionados();
                this.loadForm(_id);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptCatNivel1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.DataItem != null)
        {
            Categoria categoria = (Categoria)e.Item.DataItem;
            Repeater rptCatNivel2 = (Repeater)e.Item.FindControl("rptCatNivel2");
            CheckBox cbCatNivel1 = (CheckBox)e.Item.FindControl("cbCatNivel1");

            cbCatNivel1.Text = categoria.NomeCategoria;
            cbCatNivel1.InputAttributes.Add("value", categoria.CategoriaId.ToString());

            if (categoria.Categorias != null && categoria.Categorias.Count > 0)
            {
                rptCatNivel2.DataSource = categoria.Categorias;
                rptCatNivel2.DataBind();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptCatNivel2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.DataItem != null)
        {
            Categoria categoria = (Categoria)e.Item.DataItem;
            Repeater rptCatNivel3 = (Repeater)e.Item.FindControl("rptCatNivel3");
            CheckBox cbCatNivel2 = (CheckBox)e.Item.FindControl("cbCatNivel2");

            cbCatNivel2.Text = categoria.NomeCategoria;
            cbCatNivel2.InputAttributes.Add("value", categoria.CategoriaId.ToString());

            if (categoria.Categorias != null && categoria.Categorias.Count > 0)
            {
                rptCatNivel3.DataSource = categoria.Categorias;
                rptCatNivel3.DataBind();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptCatNivel3_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.DataItem != null)
        {
            Categoria categoria = (Categoria)e.Item.DataItem;
            CheckBox cbCatNivel3 = (CheckBox)e.Item.FindControl("cbCatNivel3");

            cbCatNivel3.Text = categoria.NomeCategoria;
            cbCatNivel3.InputAttributes.Add("value", categoria.CategoriaId.ToString());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptTitulos_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.DataItem != null)
        {
            OfertaProdutoGrid produto = (OfertaProdutoGrid)e.Item.DataItem;
            ImageButton imgExcluir = (ImageButton)e.Item.FindControl("btnExcluir");
            Literal ltTitulo = (Literal)e.Item.FindControl("ltTitulo");

            imgExcluir.CommandArgument = produto.ProdutoId.ToString();
            ltTitulo.Text = produto.NomeProduto;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExecute_Click(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = false;
        SaveOrUpdate();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        Produto produto = new Produto();
        produto.NomeProduto = txtFiltroTitulo.Text;

        Oferta oferta = new Oferta();
        oferta.OfertaId = Convert.ToInt32(hddOfertaId.Value);

        List<Produto> produtos = new ProdutoBLL().CarregarProdutosPorNome(produto, oferta);

        if (produtos.Count > 0)
        {
            divTitulosNovos.Visible = true;
            produtos.Insert(0, new Produto() { ProdutoId = 0, NomeProduto = "Todos" });

            cblTitulos.DataTextField = "nomeProduto";
            cblTitulos.DataValueField = "produtoId";
            cblTitulos.DataSource = produtos;
            cblTitulos.DataBind();
        }
        else
        {
            divTitulosNovos.Visible = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        OfertaProdutoGrid ofertaProdutoGrid = OfertaProdutosGrid.Find(p=> p.ProdutoId == Convert.ToInt32(((ImageButton)sender).CommandArgument.ToString()));
        
        OfertaProdutosGrid.Remove(ofertaProdutoGrid);

        if (OfertaProdutosGrid.Count > 0)
        {
            divTitulosAdiocionados.Visible = true;
            rptTitulos.DataSource = OfertaProdutosGrid.OrderBy(p => p.NomeProduto);
            rptTitulos.DataBind();
        }
        else
        {
            divTitulosAdiocionados.Visible = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        if (OfertaProdutosGrid == null)
        {
            OfertaProdutosGrid = new List<OfertaProdutoGrid>();
        }

        foreach (ListItem li in cblTitulos.Items)
        {
            if (li.Selected)
            {
                if (Convert.ToInt32(li.Value) > 0)
                {
                    OfertaProdutoGrid ofertaProduto = new OfertaProdutoGrid();
                    ofertaProduto.ProdutoId = Convert.ToInt32(li.Value);
                    ofertaProduto.NomeProduto = li.Text;

                    if (OfertaProdutosGrid.Find(p=> p.ProdutoId == ofertaProduto.ProdutoId) == null)
                    {
                        OfertaProdutosGrid.Add(ofertaProduto);
                    }
                }
            }
        }

        divTitulosNovos.Visible = false;
        txtFiltroTitulo.Text = String.Empty;

        if (OfertaProdutosGrid.Count > 0)
        {
            divTitulosAdiocionados.Visible = true;
            rptTitulos.DataSource = OfertaProdutosGrid.OrderBy(p => p.NomeProduto);
            rptTitulos.DataBind();
        }
        else
        {
            divTitulosAdiocionados.Visible = false;
        }
    }

    #endregion

    #region Métodos

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    protected void loadForm(int id)
    {
        if (id > 0)
        {
            var oferta = new OfertaBLL().Carregar(new GrupoA.BusinessObject.Oferta() { OfertaId = id });
            hddOfertaId.Value = oferta.OfertaId.ToString();
            this.txtPercentual.Text = oferta.Percentual != null ? oferta.Percentual.Value.ToString() : String.Empty;
            this.txtPreco.Text = oferta.PrecoOferta != null ? oferta.PrecoOferta.Value.ToString() : String.Empty;
            this.txtNome.Text = oferta.NomeOferta;
            txtDataInicio.Text = oferta.DataHoraInicio.ToString("dd/MM/yyyy");
            txtDataFim.Text = oferta.DataHoraTermino.ToString("dd/MM/yyyy");
            this.ddlTipo.SelectedValue = oferta.OfertaTipo.OfertaTipoId.ToString();

            //Seleciona as categorias
            hddCategorias.Value = new OfertaBLL().CarregarTodosOfertaCategoriaPorOferta(oferta).ToString();
        }
        else
        {
            hddFormaOferta.Value = "0";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    protected void SaveOrUpdate()
    {
        if (Page.IsValid)
        {
            List<Categoria> categorias = new List<Categoria>();

            List<Produto> produtos = new List<Produto>();

            if (ddlTipo.SelectedValue.ToString() == "3")
            {
                for (int countNivel1 = 0; countNivel1 < rptCatNivel1.Items.Count; countNivel1++)
                {
                    CheckBox cbCatNivel1 = (CheckBox)rptCatNivel1.Items[countNivel1].FindControl("cbCatNivel1");
                    if (cbCatNivel1.Checked)
                    {
                        Categoria categoria = new Categoria();
                        categoria.CategoriaId = Convert.ToInt32(cbCatNivel1.InputAttributes["value"].ToString());
                        categorias.Add(categoria);
                    }

                    Repeater rptCatNivel2 = (Repeater)rptCatNivel1.Items[countNivel1].FindControl("rptCatNivel2");

                    for (int countNivel2 = 0; countNivel2 < rptCatNivel2.Items.Count; countNivel2++)
                    {
                        CheckBox cbCatNivel2 = (CheckBox)rptCatNivel2.Items[countNivel2].FindControl("cbCatNivel2");
                        if (cbCatNivel2.Checked)
                        {
                            Categoria categoria = new Categoria();
                            categoria.CategoriaId = Convert.ToInt32(cbCatNivel2.InputAttributes["value"].ToString());
                            categorias.Add(categoria);
                        }

                        Repeater rptCatNivel3 = (Repeater)rptCatNivel2.Items[countNivel2].FindControl("rptCatNivel3");

                        for (int countNivel3 = 0; countNivel3 < rptCatNivel3.Items.Count; countNivel3++)
                        {
                            CheckBox cbCatNivel3 = (CheckBox)rptCatNivel3.Items[countNivel3].FindControl("cbCatNivel3");
                            if (cbCatNivel3.Checked)
                            {
                                Categoria categoria = new Categoria();
                                categoria.CategoriaId = Convert.ToInt32(cbCatNivel3.InputAttributes["value"].ToString());
                                categorias.Add(categoria);
                            }
                        }
                    }
                }
            }
            else if (ddlTipo.SelectedValue.ToString() == "1")
            {
                foreach (OfertaProdutoGrid item in OfertaProdutosGrid)
                {
                    Produto produto = new Produto();
                    produto.ProdutoId = item.ProdutoId;
                    produto.NomeProduto = item.NomeProduto;

                    produtos.Add(produto);
                }
            }

            var oferta = new GrupoA.BusinessObject.Oferta();
            oferta.OfertaId = Convert.ToInt32(hddOfertaId.Value);

            if (hddFormaOferta.Value.ToString() == "0")
            {
                oferta.PrecoOferta = Convert.ToDecimal(this.txtPreco.Text);
                oferta.Percentual = (Nullable<Decimal>)null;
            }
            else
            {
                oferta.Percentual = Convert.ToDecimal(this.txtPercentual.Text);
                oferta.PrecoOferta = (Nullable<Decimal>)null;
            }

            oferta.NomeOferta = txtNome.Text;
            oferta.DataHoraInicio = Convert.ToDateTime(txtDataInicio.Text);
            oferta.DataHoraTermino = Convert.ToDateTime(txtDataFim.Text);
            oferta.DataHoraTermino = new DateTime(oferta.DataHoraTermino.Year, oferta.DataHoraTermino.Month, oferta.DataHoraTermino.Day, 23, 59, 59);

            oferta.OfertaTipo = new OfertaTipo();
            oferta.OfertaTipo.OfertaTipoId = Convert.ToInt32(ddlTipo.SelectedValue.ToString());

            if (oferta.OfertaId > 0)
            {
                oferta = new OfertaBLL().Atualizar(oferta, categorias, produtos);
                if (oferta == null)
                {
                    Util.ShowUpdateMessage();
                }
                else
                {
                    Util.ShowMessage("Período da oferta conflitante com uma ou mais ofertas.", Ag2.Manager.Enumerator.typeMessage.Erro);
                }
            }
            else
            {
                oferta = new OfertaBLL().Inserir(oferta, categorias, produtos);

                if (oferta != null)
                {
                    hddOfertaId.Value = oferta.OfertaId.ToString();

                    this.MontaChecklistPagina();

                    Util.ShowInsertMessage();
                }
                else
                {
                    Util.ShowMessage("Período da oferta conflitante com uma ou mais ofertas.", Ag2.Manager.Enumerator.typeMessage.Erro);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void CarregarOfertaTipos()
    {
        ddlTipo.DataSource = new OfertaBLL().CarregarTodasOfertaTipos();
        ddlTipo.DataTextField = "tipoOferta";
        ddlTipo.DataValueField = "ofertaTipoId";
        ddlTipo.DataBind();
    }

    /// <summary>
    /// 
    /// </summary>
    protected void MontaChecklistPagina()
    {
        this.lblCategorias.Visible = true;

        List<Categoria> categorias = new CategoriaBLL().CarregarCategoriasComFilhos();

        rptCatNivel1.DataSource = categorias;
        rptCatNivel1.DataBind();
    }

    /// <summary>
    /// 
    /// </summary>
    protected void MontaTitulosAdicionados()
    {
        Oferta oferta = new Oferta();
        oferta.OfertaId = Convert.ToInt32(hddOfertaId.Value);

        List<OfertaProduto> produtos = new OfertaBLL().CarregarProdutosPorOferta(oferta);

        if (OfertaProdutosGrid == null)
        {
            OfertaProdutosGrid = new List<OfertaProdutoGrid>();
        }

        if(produtos.Count > 0)
        {
            foreach (OfertaProduto item in produtos)
            {
                OfertaProdutoGrid ofertaProduto = new OfertaProdutoGrid();
                ofertaProduto.ProdutoId = item.Produto.ProdutoId;
                ofertaProduto.NomeProduto = item.Produto.NomeProduto;

                if (OfertaProdutosGrid.Find(p => p.ProdutoId == ofertaProduto.ProdutoId) == null)
                {
                    OfertaProdutosGrid.Add(ofertaProduto);
                }
            }
        }

        if (OfertaProdutosGrid.Count > 0)
        {
            divTitulosAdiocionados.Visible = true;
            rptTitulos.DataSource = OfertaProdutosGrid.OrderBy(p => p.NomeProduto);
            rptTitulos.DataBind();
        }
        else
        {
            divTitulosAdiocionados.Visible = false;
        }
    }

    #endregion

    #region Validacoes

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void cvValidarDatasPublicacao_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (string.IsNullOrEmpty(txtDataInicio.Text) && string.IsNullOrEmpty(txtDataFim.Text))
        {
            args.IsValid = true; return;
        }

        try
        {
            DateTime dtIni = DateTime.Parse(txtDataInicio.Text);
            DateTime dtFim = DateTime.Parse(txtDataFim.Text);

            if (DateTime.Compare(dtFim, dtIni) > 0)
            {
                args.IsValid = true;
            }
            else
            {
                cvValidarDatasPublicacao.ErrorMessage = "Data final deve ser maior que data inicial.";
                args.IsValid = false;
            }
        }
        catch
        {
            cvValidarDatasPublicacao.ErrorMessage = "Data incorreta!";
            args.IsValid = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void cvValidarOferta_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (hddFormaOferta.Value.ToString() == "0")
        {
            if (String.IsNullOrEmpty(txtPreco.Text))
            {
                cvValidarOferta.ErrorMessage = "Preço é obrigatório.";
                args.IsValid = false;
            }

            if (Convert.ToDecimal(txtPreco.Text) <= 0)
            {
                cvValidarOferta.ErrorMessage = "Preço deve ser maior que zero.";
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true; return;
            }
        }
        else
        {
            if (String.IsNullOrEmpty(txtPercentual.Text))
            {
                cvValidarOferta.ErrorMessage = "Percentual é obrigatório.";
                args.IsValid = false;
            }

            if (Convert.ToDecimal(txtPercentual.Text) <= 0)
            {
                cvValidarOferta.ErrorMessage = "Percentual deve ser maior que zero.";
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true; return;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void cvValidarTipo_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (ddlTipo.SelectedValue.ToString() == "1")
        {
            if (OfertaProdutosGrid != null && OfertaProdutosGrid.Count>0)
            {
                args.IsValid = true; return;
            }
            else
            {
                cvValidarTipo.ErrorMessage = "Títulos devem ser selecionados.";
                args.IsValid = false;
            }
        }
        else if (ddlTipo.SelectedValue.ToString() == "2")
        {
            args.IsValid = true; return;
        }
        else
        {
            Boolean selecionou = false;

            for (int countNivel1 = 0; countNivel1 < rptCatNivel1.Items.Count; countNivel1++)
            {
                if (selecionou)
                {
                    break;
                }

                CheckBox cbCatNivel1 = (CheckBox)rptCatNivel1.Items[countNivel1].FindControl("cbCatNivel1");
                if (cbCatNivel1.Checked)
                {
                    selecionou = true;
                    break;
                }

                Repeater rptCatNivel2 = (Repeater)rptCatNivel1.Items[countNivel1].FindControl("rptCatNivel2");

                for (int countNivel2 = 0; countNivel2 < rptCatNivel2.Items.Count; countNivel2++)
                {
                    if (selecionou)
                    {
                        break;
                    }

                    CheckBox cbCatNivel2 = (CheckBox)rptCatNivel2.Items[countNivel2].FindControl("cbCatNivel2");
                    if (cbCatNivel2.Checked)
                    {
                        selecionou = true;
                        break;
                    }

                    Repeater rptCatNivel3 = (Repeater)rptCatNivel2.Items[countNivel2].FindControl("rptCatNivel3");

                    for (int countNivel3 = 0; countNivel3 < rptCatNivel3.Items.Count; countNivel3++)
                    {
                        CheckBox cbCatNivel3 = (CheckBox)rptCatNivel3.Items[countNivel3].FindControl("cbCatNivel3");
                        if (cbCatNivel3.Checked)
                        {
                            selecionou = true;
                            break;
                        }
                    }
                }
            }

            if (!selecionou)
            {
                cvValidarTipo.ErrorMessage = "Categorias devem ser selecionadas.";
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true; return;
            }
        }
    }
    #endregion
}
