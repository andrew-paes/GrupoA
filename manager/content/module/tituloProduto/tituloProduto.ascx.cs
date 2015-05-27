using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Linq;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using GrupoA.GlobalResources;
using System.Web.UI.WebControls;

public partial class content_module_tituloProduto_tituloProduto : System.Web.UI.UserControl
{
    #region [ Properties ]

    protected Titulo TituloBO = new Titulo();

    private int _tituloId
    {
        get
        {
            if (Session["_idTitulo"] == null)
            {
                Session["_idTitulo"] = 0;
            }

            return (int)Session["_idTitulo"];
        }
        set
        {
            if (Session["_idTitulo"] == null)
            {
                Session["_idTitulo"] = 0;
            }

            Session["_idTitulo"] = (int)value;
        }
    }

    #endregion

    #region [ Page Events ]

    protected void Page_Load(object sender, EventArgs e)
    {
        this._tituloId = Util.GetRequestId();

        if (this._tituloId > 0)
        {
            this.CarregarObjetos();

            this.ListFilesTituloImpresso.BindList += new EventHandler<ArquivoEventArgs>(ListFilesTituloImpresso_BindList);
            this.ListFilesTituloImpresso.DeleteItem += new EventHandler<ArquivoEventArgs>(ListFilesTituloImpresso_DeleteItem);
            this.ListFilesTituloImpresso_BindList(new object(), new ArquivoEventArgs());

            this.ListFilesTituloEletronico.BindList += new EventHandler<ArquivoEventArgs>(ListFilesTituloEletronico_BindList);
            this.ListFilesTituloEletronico.DeleteItem += new EventHandler<ArquivoEventArgs>(ListFilesTituloEletronico_DeleteItem);
            this.ListFilesTituloEletronico_BindList(new object(), new ArquivoEventArgs());

            if (!IsPostBack)
            {
                this.CarregarForm();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_PreRender(object sender, EventArgs e)
    {
        string strScript = String.Empty;

        if (this.hddTableTituloImpresso.Value != null && this.hddTableTituloImpresso.Value == "0")
            strScript += "SetTable($('#fieldTituloImpresso'), $('#tableTituloImpresso'), 'Titulo Impresso');";

        if (this.hddTableTituloEletronico.Value != null && this.hddTableTituloEletronico.Value == "0")
            strScript += "SetTable($('#fieldTituloEletronico'), $('#tableTituloEletronico'), 'Titulo eBook');";

        strScript = String.Concat("$(document).ready(function () {", strScript, "});");
        Page.ClientScript.RegisterClientScriptBlock(GetType(), "jsSetPanel", strScript, true);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ListFilesTituloImpresso_BindList(object sender, ArquivoEventArgs e)
    {
        if (
            this.TituloBO.TituloImpresso != null
            && this.TituloBO.TituloImpresso.TituloImpressoId > 0
            && this.TituloBO.TituloImpresso.Produto != null
            && this.TituloBO.TituloImpresso.Produto.ProdutoId > 0
            )
        {
            this.ListFilesTituloImpresso.RegistroId = this.TituloBO.TituloImpresso.Produto.ProdutoId;
        }

        ctl_ag2ListFiles ctlAg2ListFiles = this.ListFilesTituloImpresso;

        this.CarregarImagemProduto(ctlAg2ListFiles);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ListFilesTituloImpresso_DeleteItem(object sender, ArquivoEventArgs e)
    {
        if (
            this.TituloBO.TituloImpresso != null
            && this.TituloBO.TituloImpresso.TituloImpressoId > 0
            && this.TituloBO.TituloImpresso.Produto != null
            && this.TituloBO.TituloImpresso.Produto.ProdutoId > 0
            )
        {
            this.ListFilesTituloImpresso.RegistroId = this.TituloBO.TituloImpresso.Produto.ProdutoId;
        }

        ctl_ag2ListFiles ctlAg2ListFiles = this.ListFilesTituloImpresso;

        this.ExcluirImagemProduto(ctlAg2ListFiles);
        this.CarregarImagemProduto(ctlAg2ListFiles);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ListFilesTituloEletronico_BindList(object sender, ArquivoEventArgs e)
    {
        if (
            this.TituloBO.TituloEletronico != null
            && this.TituloBO.TituloEletronico.TituloEletronicoId > 0
            && this.TituloBO.TituloEletronico.Produto != null
            && this.TituloBO.TituloEletronico.Produto.ProdutoId > 0
            )
        {
            this.ListFilesTituloEletronico.RegistroId = this.TituloBO.TituloEletronico.Produto.ProdutoId;
        }

        ctl_ag2ListFiles ctlAg2ListFiles = this.ListFilesTituloEletronico;

        this.CarregarImagemProduto(ctlAg2ListFiles);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ListFilesTituloEletronico_DeleteItem(object sender, ArquivoEventArgs e)
    {
        if (
            this.TituloBO.TituloEletronico != null
            && this.TituloBO.TituloEletronico.TituloEletronicoId > 0
            && this.TituloBO.TituloEletronico.Produto != null
            && this.TituloBO.TituloEletronico.Produto.ProdutoId > 0
            )
        {
            this.ListFilesTituloEletronico.RegistroId = this.TituloBO.TituloEletronico.Produto.ProdutoId;
        }

        ctl_ag2ListFiles ctlAg2ListFiles = this.ListFilesTituloEletronico;

        this.ExcluirImagemProduto(ctlAg2ListFiles);
        this.CarregarImagemProduto(ctlAg2ListFiles);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptAutor_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        if (e.Item != null && e.Item.DataItem != null)
        {
            TituloAutor tituloAutorBO = (TituloAutor)e.Item.DataItem;

            Literal ltrAutorId = (Literal)e.Item.FindControl("ltrAutorId");
            Literal ltrAutorNome = (Literal)e.Item.FindControl("ltrAutorNome");

            if (tituloAutorBO.Autor != null && tituloAutorBO.Autor.AutorId > 0)
            {
                ltrAutorId.Text = tituloAutorBO.Autor.AutorId.ToString();
                ltrAutorNome.Text = tituloAutorBO.Autor.NomeAutor;
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
        this.Salvar();
    }

    #endregion

    #region [ Methods ]

    /// <summary>
    /// Carrega todos os obejtos usados no formulario, menos imagens.
    /// </summary>
    private void CarregarObjetos()
    {
        this.CarregarObjetosTitulo();
        this.CarregarObjetosTituloImpresso();
        this.CarregarObjetosTituloEletronico();
    }

    /// <summary>
    /// 
    /// </summary>
    private void CarregarObjetosTitulo()
    {
        this.TituloBO = new TituloBLL().Carregar(new Titulo { TituloId = this._tituloId });

        if (this.TituloBO != null && this.TituloBO.TituloId > 0)
        {
            List<Autor> autorBOList = new TituloBLL().CarregarAutores(this.TituloBO);

            this.TituloBO.TituloAutores = new List<TituloAutor>();

            foreach (Autor autorBOTemp in autorBOList)
            {
                TituloAutor tituloAutorBO = new TituloAutor();
                tituloAutorBO.Autor = new Autor();
                tituloAutorBO.Autor = autorBOTemp;

                this.TituloBO.TituloAutores.Add(tituloAutorBO);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void CarregarObjetosTituloImpresso()
    {
        if (this.TituloBO != null && this.TituloBO.TituloId > 0)
        {
            this.TituloBO.TituloImpresso = new TituloImpresso();
            this.TituloBO.TituloImpresso = new TituloImpressoBLL().CarregarPorTitulo(this.TituloBO.TituloId);

            if (this.TituloBO.TituloImpresso != null && this.TituloBO.TituloImpresso.TituloImpressoId > 0)
            {
                this.TituloBO.TituloImpresso.Produto = new Produto();
                this.TituloBO.TituloImpresso.Produto = new ProdutoBLL().Carregar(new Produto { ProdutoId = this.TituloBO.TituloImpresso.TituloImpressoId });

                if (this.TituloBO.TituloImpresso.Produto != null && this.TituloBO.TituloImpresso.Produto.ProdutoId > 0)
                {
                    this.TituloBO.TituloImpresso.Produto.Conteudo = new Conteudo();
                    this.TituloBO.TituloImpresso.Produto.Conteudo.Categorias = new List<Categoria>();
                    this.TituloBO.TituloImpresso.Produto.Conteudo.Categorias = new CategoriaBLL().CarregarCategoriaPorProduto(this.TituloBO.TituloImpresso.Produto.ProdutoId);

                    this.TituloBO.TituloImpresso.Produto.ProdutoFormato = new ProdutoFormato();
                    this.TituloBO.TituloImpresso.Produto.ProdutoFormato.ProdutoId = this.TituloBO.TituloImpresso.Produto.ProdutoId;
                    this.TituloBO.TituloImpresso.Produto.ProdutoFormato = new ProdutoFormatoBLL().Carregar(this.TituloBO.TituloImpresso.Produto.ProdutoFormato);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void CarregarObjetosTituloEletronico()
    {
        if (this.TituloBO != null && this.TituloBO.TituloId > 0)
        {
            this.TituloBO.TituloEletronico = new TituloEletronico();
            this.TituloBO.TituloEletronico = new TituloEletronicoBLL().CarregarPorTitulo(this.TituloBO);

            if (this.TituloBO.TituloEletronico != null && this.TituloBO.TituloEletronico.TituloEletronicoId > 0)
            {
                this.TituloBO.TituloEletronico.Produto = new Produto();
                this.TituloBO.TituloEletronico.Produto = new ProdutoBLL().Carregar(new Produto { ProdutoId = this.TituloBO.TituloEletronico.TituloEletronicoId });

                if (this.TituloBO.TituloEletronico.Produto != null && this.TituloBO.TituloEletronico.Produto.ProdutoId > 0)
                {
                    this.TituloBO.TituloEletronico.Produto.Conteudo = new Conteudo();
                    this.TituloBO.TituloEletronico.Produto.Conteudo.Categorias = new List<Categoria>();
                    this.TituloBO.TituloEletronico.Produto.Conteudo.Categorias = new CategoriaBLL().CarregarCategoriaPorProduto(this.TituloBO.TituloEletronico.Produto.ProdutoId);

                    this.TituloBO.TituloEletronico.Produto.ProdutoFormato = new ProdutoFormato();
                    this.TituloBO.TituloEletronico.Produto.ProdutoFormato.ProdutoId = this.TituloBO.TituloEletronico.Produto.ProdutoId;
                    this.TituloBO.TituloEletronico.Produto.ProdutoFormato = new ProdutoFormatoBLL().Carregar(this.TituloBO.TituloEletronico.Produto.ProdutoFormato);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ctlAg2ListFiles"></param>
    private void CarregarImagemProduto(ctl_ag2ListFiles ctlAg2ListFiles)
    {
        List<ProdutoImagem> produtoImagemBOList = new ProdutoBLL().CarregarProdutoImagem(new ProdutoImagem() { Produto = new Produto() { ProdutoId = ctlAg2ListFiles.RegistroId } }).ToList();

        List<ItemArquivo> itemArquivoBOList = new List<ItemArquivo>();
        ItemArquivo itemArquivoBO = null;

        foreach (ProdutoImagem produtoImagemBOTemp in produtoImagemBOList)
        {
            itemArquivoBO = new ItemArquivo();
            itemArquivoBO.ArquivoId = produtoImagemBOTemp.Arquivo.ArquivoId;
            string pathFile = string.Concat(GrupoA_Resource.baseUrlUpload, this.ListFilesTituloImpresso.TargetFolder, produtoImagemBOTemp.Arquivo.NomeArquivo);
            itemArquivoBO.CaminhoArquivo = pathFile;
            itemArquivoBO.TamanhoArquivo = produtoImagemBOTemp.Arquivo.TamanhoArquivo.ToString();
            itemArquivoBOList.Add(itemArquivoBO);
        }

        ctlAg2ListFiles.DataSource = itemArquivoBOList;
        ctlAg2ListFiles.DataBind();
    }

    /// <summary>
    /// Exclui todos os relacionamento de ProdutoImagem, pois na inclusão de uma única imagem, o controle gera todas as 3 categorias necessárias
    /// </summary>
    /// <param name="e"></param>
    /// <param name="ctlAg2ListFiles"></param>
    private void ExcluirImagemProduto(ctl_ag2ListFiles ctlAg2ListFiles)
    {
        List<ProdutoImagem> produtoImagemBOList = new ProdutoBLL().CarregarProdutoImagem(new ProdutoImagem() { Produto = new Produto() { ProdutoId = ctlAg2ListFiles.RegistroId } }).ToList();

        try
        {
            foreach (ProdutoImagem produtoImagemBOTemp in produtoImagemBOList)
            {
                string pathFileDelete = string.Concat(Server.MapPath(GrupoA_Resource.baseUrlUpload), ctlAg2ListFiles.TargetFolder, produtoImagemBOTemp.Arquivo.NomeArquivo);
                System.IO.FileInfo infoDelete = new System.IO.FileInfo(pathFileDelete);

                if (infoDelete != null)
                {
                    infoDelete.Delete();
                }
            }
        }
        catch { }
        finally
        {
            new ProdutoBLL().ExcluirArquivosProdutoImagem(produtoImagemBOList);
        }
    }

    #region [ Carregar Form ]

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    protected void CarregarForm()
    {
        this.CarregarTitulo();
        this.CarregarTituloImpresso();
        this.CarregarTituloEletronico();
    }

    /// <summary>
    /// 
    /// </summary>
    private void CarregarTitulo()
    {
        if (this.TituloBO != null && this.TituloBO.TituloId > 0)
        {
            this.txtTituloLivro.Text = this.TituloBO.NomeTitulo;
            this.txtSubtituloLivro.Text = this.TituloBO.SubtituloLivro;
            this.txtEdicaoLivro.Text = String.Concat(this.TituloBO.Edicao != null ? this.TituloBO.Edicao.Value.ToString() : "1", "ª Edição");
            this.txtDataLancamentoLivro.Text = this.TituloBO.DataLancamento != null ? this.TituloBO.DataLancamento.Value.ToString("dd/MM/yyyy") : "";

            if (this.TituloBO.TituloAutores != null && this.TituloBO.TituloAutores.Any())
            {
                this.rptAutor.DataSource = this.TituloBO.TituloAutores;
                this.rptAutor.DataBind();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tituloBO"></param>
    private void CarregarTituloImpresso()
    {
        if (this.TituloBO != null && this.TituloBO.TituloId > 0 && this.TituloBO.TituloImpresso != null && this.TituloBO.TituloImpresso.TituloImpressoId > 0)
        {
            this.pnlTituloImpresso.Visible = true;

            this.txtISBN13TituloImpresso.Text = this.TituloBO.TituloImpresso.Isbn13;

            if (this.TituloBO.TituloImpresso.Produto != null && this.TituloBO.TituloImpresso.Produto.ProdutoId > 0)
            {
                this.txtNomeTituloImpresso.Text = this.TituloBO.TituloImpresso.Produto.NomeProduto;
                this.txtCodigoTituloImpresso.Text = this.TituloBO.TituloImpresso.Produto.CodigoProduto;
                this.txtValorUnitarioTituloImpresso.Text = this.TituloBO.TituloImpresso.Produto.ValorUnitario != null ? this.TituloBO.TituloImpresso.Produto.ValorUnitario.ToString() : "";
                this.txtValorOfertaTituloImpresso.Text = this.TituloBO.TituloImpresso.Produto.ValorOferta != null ? this.TituloBO.TituloImpresso.Produto.ValorOferta.ToString() : "";
                this.txtPesoTituloImpresso.Text = this.TituloBO.TituloImpresso.Produto.Peso != null ? this.TituloBO.TituloImpresso.Produto.Peso.ToString() : "";
                this.chkUtilizarFreteTituloImpresso.Checked = this.TituloBO.TituloImpresso.Produto != null ? this.TituloBO.TituloImpresso.Produto.UtilizaFrete : false;
                this.chkDisponivelTituloImpresso.Checked = this.TituloBO.TituloImpresso.Produto.Disponivel != null ? this.TituloBO.TituloImpresso.Produto.Disponivel : false;
                this.chkExibirSiteTituloImpresso.Checked = this.TituloBO.TituloImpresso.Produto.ExibirSite != null ? this.TituloBO.TituloImpresso.Produto.ExibirSite : false;
                this.chkHomologadoTituloImpresso.Checked = this.TituloBO.TituloImpresso.Produto.Homologado != null ? this.TituloBO.TituloImpresso.Produto.Homologado : false;

                if (
                    this.TituloBO.TituloImpresso.Produto.Conteudo.Categorias != null
                    && this.TituloBO.TituloImpresso.Produto.Conteudo.Categorias.Any()
                    && this.TituloBO.TituloImpresso.Produto.Conteudo.Categorias[0] != null
                    )
                {
                    this.txtAreaTituloImpresso.Text = this.TituloBO.TituloImpresso.Produto.Conteudo.Categorias[0].NomeCategoria;
                }

                if (this.TituloBO.TituloImpresso.Produto.ProdutoFormato != null && this.TituloBO.TituloImpresso.Produto.ProdutoFormato.ProdutoId > 0)
                {
                    this.txtFormatoTituloImpresso.Text = this.TituloBO.TituloImpresso.Produto.ProdutoFormato.Formato;
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void CarregarTituloEletronico()
    {
        if (this.TituloBO != null && this.TituloBO.TituloId > 0 && this.TituloBO.TituloEletronico != null && this.TituloBO.TituloEletronico.TituloEletronicoId > 0)
        {
            this.pnlTituloEletronico.Visible = true;

            this.txtISBN13TituloEletronico.Text = this.TituloBO.TituloEletronico.Isbn13;

            if (this.TituloBO.TituloEletronico.Produto != null && this.TituloBO.TituloEletronico.Produto.ProdutoId > 0)
            {
                this.txtNomeTituloEletronico.Text = this.TituloBO.TituloEletronico.Produto.NomeProduto;
                this.txtCodigoTituloEletronico.Text = this.TituloBO.TituloEletronico.Produto.CodigoProduto;
                this.txtValorUnitarioTituloEletronico.Text = this.TituloBO.TituloEletronico.Produto.ValorUnitario != null ? this.TituloBO.TituloEletronico.Produto.ValorUnitario.ToString() : "";
                this.txtValorOfertaTituloEletronico.Text = this.TituloBO.TituloEletronico.Produto.ValorOferta != null ? this.TituloBO.TituloEletronico.Produto.ValorOferta.ToString() : "";
                this.chkDisponivelTituloEletronico.Checked = this.TituloBO.TituloEletronico.Produto.Disponivel != null ? this.TituloBO.TituloEletronico.Produto.Disponivel : false;
                this.chkExibirSiteTituloEletronico.Checked = this.TituloBO.TituloEletronico.Produto.ExibirSite != null ? this.TituloBO.TituloEletronico.Produto.ExibirSite : false;
                this.chkHomologadoTituloEletronico.Checked = this.TituloBO.TituloEletronico.Produto.Homologado != null ? this.TituloBO.TituloEletronico.Produto.Homologado : false;

                if (
                    this.TituloBO.TituloEletronico.Produto.Conteudo.Categorias != null
                    && this.TituloBO.TituloEletronico.Produto.Conteudo.Categorias.Any()
                    && this.TituloBO.TituloEletronico.Produto.Conteudo.Categorias[0] != null
                    )
                {
                    this.txtAreaTituloEletronico.Text = this.TituloBO.TituloEletronico.Produto.Conteudo.Categorias[0].NomeCategoria;
                }

                if (this.TituloBO.TituloEletronico.Produto.ProdutoFormato != null && this.TituloBO.TituloEletronico.Produto.ProdutoFormato.ProdutoId > 0)
                {
                    this.txtFormatoTituloEletronico.Text = this.TituloBO.TituloEletronico.Produto.ProdutoFormato.Formato;
                }
            }
        }
    }

    #endregion

    #region [ Save ]

    /// <summary>
    /// Salva ou atualiza as informações após validar a página atual.
    /// </summary>
    public void Salvar()
    {
        if (Page.IsValid && this.TituloBO != null && this.TituloBO.TituloId > 0)
        {
            try
            {
                this.SalvarTituloImpresso();
                this.SalvarTituloEletronico();

                this.CarregarObjetos();
                this.CarregarForm();

                Util.ShowUpdateMessage();
            }
            catch
            {
                Util.ShowMessage("Erro ao atualizar!");
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void SalvarTituloImpresso()
    {
        if (
            this.TituloBO.TituloImpresso != null
            && this.TituloBO.TituloImpresso.TituloImpressoId > 0
            && this.TituloBO.TituloImpresso.Produto != null
            && this.TituloBO.TituloImpresso.Produto.ProdutoId > 0
            )
        {
            this.TituloBO.TituloImpresso.Produto.NomeProduto = this.txtTituloLivro.Text;
            this.TituloBO.TituloImpresso.Produto.Homologado = this.chkHomologadoTituloImpresso.Checked;

            new ProdutoBLL().Atualizar(this.TituloBO.TituloImpresso.Produto);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void SalvarTituloEletronico()
    {
        if (
            this.TituloBO.TituloEletronico != null
            && this.TituloBO.TituloEletronico.TituloEletronicoId > 0
            && this.TituloBO.TituloEletronico.Produto != null
            && this.TituloBO.TituloEletronico.Produto.ProdutoId > 0
            )
        {
            this.TituloBO.TituloEletronico.Produto.NomeProduto = string.Concat(this.txtTituloLivro.Text, " - eBook");
            this.TituloBO.TituloEletronico.Produto.Homologado = this.chkHomologadoTituloEletronico.Checked;

            new ProdutoBLL().Atualizar(this.TituloBO.TituloEletronico.Produto);
        }
    }

    #endregion

    #endregion
}