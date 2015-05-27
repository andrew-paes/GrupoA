using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using GrupoA.GlobalResources;

public partial class content_module_revistaEdicao : System.Web.UI.UserControl
{
    #region Propriedades

    private int _id = 0;
    private int _idArquivo = 0;
    private string _NomeArquivo = String.Empty;

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
            ListFilesRevista.BindList += new EventHandler<ArquivoEventArgs>(ListFilesRevista_BindList);
            ListFilesRevista.DeleteItem += new EventHandler<ArquivoEventArgs>(ListFilesRevista_DeleteItem);

            _id = Util.GetRequestId();
            hdnRevistaEdicaoId.Value = _id.ToString();

            this.ListFilesRevista_BindList(new object(), new ArquivoEventArgs());

            if (!IsPostBack)
            {
                this.loadForm();
            }
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
        this.SaveOrUpdate();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ListFilesRevista_BindList(object sender, ArquivoEventArgs e)
    {
        if (hdnRevistaEdicaoId.Value != "0")
        {
            ListFilesRevista.RegistroId = Convert.ToInt32(hdnRevistaEdicaoId.Value.ToString());
        }

        ctl_ag2ListFiles ctlAg2ListFiles = ListFilesRevista;

        this.CarregarImagemProduto(ctlAg2ListFiles);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ListFilesRevista_DeleteItem(object sender, ArquivoEventArgs e)
    {
        if (hdnRevistaEdicaoId.Value != "0")
        {
            ListFilesRevista.RegistroId = Convert.ToInt32(hdnRevistaEdicaoId.Value.ToString());
        }

        ctl_ag2ListFiles ctlAg2ListFiles = ListFilesRevista;

        this.ExcluirImagemProduto(ctlAg2ListFiles);
        this.CarregarImagemProduto(ctlAg2ListFiles);
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
            RevistaEdicao revistaEdicao = new RevistaEdicaoBLL().CarregarEdicaoComProduto(Convert.ToInt32(this.hdnRevistaEdicaoId.Value));

            txtNomeRevista.Text = revistaEdicao.Revista.NomeRevista;
            txtISSN.Text = revistaEdicao.Revista.ISSN;

            txtDescricao.Text = revistaEdicao.DescricaoEdicao;
            txtNumeroEdicao.Text = revistaEdicao.NumeroEdicao.ToString();
            txtAnoEdicao.Text = revistaEdicao.AnoEdicao;
            txtMesPublicacao.Text = revistaEdicao.MesPublicacao.ToString();
            txtPeriodoPublicacao.Text = revistaEdicao.PeriodoPublicacao;
            txtAnoPublicacao.Text = revistaEdicao.AnoPublicacao.ToString();
            txtPeriodoPublicacao.Text = revistaEdicao.PeriodoPublicacao;
            txtAnoEdicao.Text = revistaEdicao.AnoEdicao;
            txtNumPaginas.Text = revistaEdicao.NumeroPaginas.ToString();
            txtTitulo.Text = revistaEdicao.TituloEdicao;
            chkAtivo.Checked = revistaEdicao.Ativo;

            txtCodigoProduto.Text = revistaEdicao.Produto.CodigoProduto;
            txtNomeProduto.Text = revistaEdicao.Produto.NomeProduto;
            txtValorUnitario.Text = revistaEdicao.Produto.ValorUnitario.ToString();
            txtValorOferta.Text = revistaEdicao.Produto.ValorOferta.ToString();
            txtPeso.Text = revistaEdicao.Produto.Peso.ToString();
            chkDisponivel.Checked = revistaEdicao.Produto.Disponivel;
            chkExibirSite.Checked = revistaEdicao.Produto.ExibirSite;
            chkFreteGratis.Checked = !revistaEdicao.Produto.UtilizaFrete;
            chkHomologado.Checked = revistaEdicao.Produto.Homologado;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    protected void SaveOrUpdate()
    {

        if (Page.IsValid)
        {
            RevistaEdicao revistaEdicao = new RevistaEdicaoBLL().CarregarComDependencias(new RevistaEdicao() { RevistaEdicaoId = Convert.ToInt32(this.hdnRevistaEdicaoId.Value) });

            if (revistaEdicao.RevistaEdicaoId > 0)
            {
                revistaEdicao.DescricaoEdicao = txtDescricao.Text;
                revistaEdicao.PeriodoPublicacao = txtPeriodoPublicacao.Text;
                revistaEdicao.AnoEdicao = txtAnoEdicao.Text;
                revistaEdicao.TituloEdicao = txtTitulo.Text;
                revistaEdicao.Ativo = chkAtivo.Checked;

                new RevistaEdicaoBLL().Atualizar(revistaEdicao);

                Produto produto = revistaEdicao.Produto;
                produto.Homologado = chkHomologado.Checked;
                produto.NomeProduto = txtNomeProduto.Text;

                new ProdutoBLL().Atualizar(produto);

                Util.ShowUpdateMessage();
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
            string pathFile = string.Concat(GrupoA_Resource.baseUrlUpload, this.ListFilesRevista.TargetFolder, produtoImagemBOTemp.Arquivo.NomeArquivo);
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

    #endregion
}
