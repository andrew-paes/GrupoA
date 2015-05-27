using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using GrupoA.GlobalResources;

public partial class content_module_assinaturaRevista : System.Web.UI.UserControl
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
            this.hdnAssinaturaRevistaId.Value = _id.ToString();

            this.ListFilesRevista_BindList(new object(), new ArquivoEventArgs());

            if (!IsPostBack)
            {
                this.loadForm();
            }
            //ListFiles1.RegistroId = Util.GetRequestId();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //void ListFiles1_DeleteItem(object sender, ArquivoEventArgs e)
    //{
    //    //Exclui arquivos e relacionamento anterior

    //    var produtoImagens = new ProdutoBLL()
    //        .CarregarProdutoImagem(new ProdutoImagem()
    //        {
    //            Produto = new Produto() { ProdutoId = Convert.ToInt32(this.hdnAssinaturaRevistaId.Value) }
    //        });

    //    new ProdutoBLL().ExcluirArquivosProdutoImagem(produtoImagens);

    //    this.loadForm();
    //    ListFiles1.RegistroId = _id;
    //    ListFiles1.DataBind();
    //}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //void ListFiles1_BindList(object sender, ArquivoEventArgs e)
    //{
    //    var arquivo = new ArquivoBLL().CarregarArquivo(new Arquivo() { ArquivoId = e.ArquivoId });

    //    _NomeArquivo = arquivo.NomeArquivo;
    //    _idArquivo = arquivo.ArquivoId;

    //    this.Execute();
    //}

    protected void ListFilesRevista_BindList(object sender, ArquivoEventArgs e)
    {
        if (hdnAssinaturaRevistaId.Value != "0")
        {
            ListFilesRevista.RegistroId = Convert.ToInt32(hdnAssinaturaRevistaId.Value.ToString());
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
        if (hdnAssinaturaRevistaId.Value != "0")
        {
            ListFilesRevista.RegistroId = Convert.ToInt32(hdnAssinaturaRevistaId.Value.ToString());
        }

        ctl_ag2ListFiles ctlAg2ListFiles = ListFilesRevista;

        this.ExcluirImagemProduto(ctlAg2ListFiles);
        this.CarregarImagemProduto(ctlAg2ListFiles);
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

    #endregion

    #region Métodos

    /// <summary>
    /// 
    /// </summary>
    protected void loadForm()
    {
        if (_id > 0)
        {
            RevistaAssinatura revistaAssinatura = new RevistaAssinaturaBLL().CarregarComDependencias(new RevistaAssinatura() { RevistaAssinaturaId = Convert.ToInt32(this.hdnAssinaturaRevistaId.Value) });

            revistaAssinatura.Revista = new RevistaBLL().Carregar(revistaAssinatura.Revista);

            var produtoimagens = new ProdutoBLL().CarregarProdutoImagem(new ProdutoImagem() { Produto = new Produto() { ProdutoId = Convert.ToInt32(this.hdnAssinaturaRevistaId.Value) } });

            txtNomeRevista.Text = revistaAssinatura.Revista.NomeRevista;
            txtISSN.Text = revistaAssinatura.Revista.ISSN;

            txtNumeroExemplares.Text = revistaAssinatura.NumeroExemplares.Value.ToString();
            txtDescricao.Text = revistaAssinatura.DescricaoAssinatura;

            txtCodigoProduto.Text = revistaAssinatura.Produto.CodigoProduto;
            txtNomeProduto.Text = revistaAssinatura.Produto.NomeProduto;
            txtValorUnitario.Text = revistaAssinatura.Produto.ValorUnitario.ToString();
            txtValorOferta.Text = revistaAssinatura.Produto.ValorOferta == null ? String.Empty : revistaAssinatura.Produto.ValorOferta.Value.ToString();
            chkHomologado.Checked = revistaAssinatura.Produto.Homologado;
            chkFreteGratis.Checked = revistaAssinatura.Produto.UtilizaFrete;
            chkDisponivel.Checked = revistaAssinatura.Produto.Disponivel;
            chkExibirSite.Checked = revistaAssinatura.Produto.ExibirSite;

            this.hdnRevistaId.Value = revistaAssinatura.Revista.RevistaId.ToString();

            //if (produtoimagens.Count > 0)
            //{
            //    _idArquivo = produtoimagens[0].Arquivo.ArquivoId;
            //    _NomeArquivo = produtoimagens[0].Arquivo.NomeArquivoOriginal;
            //    ListFiles1.CarregaArquivo(produtoimagens[0].Arquivo.ArquivoId);
            //}
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void Execute()
    {
        if (Page.IsValid)
        {
            //if (_idArquivo > 0)
            //{
            //    // TODO: Verificar se isso está funcionando corretamente
            //    string strFilePath = Server.MapPath(string.Concat(GrupoA_Resource.baseUrlUpload, "/", ListFiles1.TargetFolder, "/"));
            //    string strFileName = strFilePath + _NomeArquivo;

            //    string strFileNameP = Path.GetFileNameWithoutExtension(strFileName) + "_P" + Path.GetExtension(strFileName);

            //    File.Copy(strFileName, strFilePath + strFileNameP, true);

            //    File.Delete(strFileName);

            //    try
            //    {
            //        new ImagemThumb.ImagemThumb().GerarProporcao(strFilePath + strFileNameP, 95, 128, System.Drawing.Color.White);
            //    }
            //    catch (Exception ex)
            //    {

            //    }

            //    var produtoImagensAdd = new List<ProdutoImagem>();

            //    produtoImagensAdd.Add(this.InserirTituloImagem(strFilePath, strFileNameP, 1));

            //    //Exclui arquivos e relacionamento anterior
            //    var produtoImagens = new ProdutoBLL().CarregarProdutoImagem(new ProdutoImagem() { Produto = new Produto() { ProdutoId = Convert.ToInt32(this.hdnAssinaturaRevistaId.Value) } });

            //    new ProdutoBLL().ExcluirArquivosProdutoImagem(produtoImagens);
            //    new ProdutoBLL().InserirProdutoImagem(produtoImagensAdd);

            //    Util.ShowUpdateMessage();
            //}
            //else
            //{
            //    Util.ShowMessage("Você não alterou a imagem.");
            //}
            //this.loadForm();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="strFilePath"></param>
    /// <param name="strFileName"></param>
    /// <param name="produtoImagemTipoId"></param>
    /// <returns></returns>
    //private ProdutoImagem InserirTituloImagem(string strFilePath, string strFileName, int produtoImagemTipoId)
    //{
    //    var produtoImagem = new ProdutoImagem();
    //    produtoImagem.ProdutoImagemTipo = new ProdutoImagemTipo();
    //    produtoImagem.ProdutoImagemTipo.ProdutoImagemTipoId = produtoImagemTipoId;

    //    produtoImagem.Produto = new Produto();
    //    produtoImagem.Produto.ProdutoId = _id;

    //    produtoImagem.Arquivo = new Arquivo();
    //    if (File.Exists(strFilePath + strFileName))
    //    {
    //        System.IO.FileInfo info = new System.IO.FileInfo(strFilePath + strFileName);
    //        produtoImagem.Arquivo.TamanhoArquivo = Convert.ToInt32(info.Length);
    //        produtoImagem.Arquivo.NomeArquivo = strFileName;
    //        produtoImagem.Arquivo.NomeArquivoOriginal = strFileName;
    //        produtoImagem.Arquivo.DataHoraUpload = DateTime.Now;
    //    }
    //    return produtoImagem;
    //}

    /// <summary>
    /// 
    /// </summary>
    protected void SaveOrUpdate()
    {
        if (Page.IsValid)
        {
            RevistaAssinaturaBLL revistaAssinaturaBLL = new RevistaAssinaturaBLL();
            RevistaAssinatura revistaAssinatura = new RevistaAssinaturaBLL().CarregarComDependencias(new RevistaAssinatura() { RevistaAssinaturaId = Convert.ToInt32(this.hdnAssinaturaRevistaId.Value) });
            revistaAssinatura.DescricaoAssinatura = txtDescricao.Text;

            revistaAssinaturaBLL.Atualizar(revistaAssinatura);

            ProdutoBLL produtoBLL = new ProdutoBLL();
            //Produto produto = produtoBLL.Carregar(new Produto() { ProdutoId = Convert.ToInt32(this.hdnAssinaturaRevistaId.Value) });
            Produto produto = revistaAssinatura.Produto;

            produto.Homologado = chkHomologado.Checked;
            produto.NomeProduto = txtNomeProduto.Text;

            produtoBLL.Atualizar(produto);

            Util.ShowUpdateMessage();
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ctlAg2ListFiles"></param>
    //private void CarregarImagemProduto(ctl_ag2ListFiles ctlAg2ListFiles)
    //{
    //    List<ProdutoImagem> produtoImagemBOList = new ProdutoBLL().CarregarProdutoImagem(new ProdutoImagem() { Produto = new Produto() { ProdutoId = ctlAg2ListFiles.RegistroId } }).ToList();

    //    List<ItemArquivo> itemArquivoBOList = new List<ItemArquivo>();
    //    ItemArquivo itemArquivoBO = null;

    //    foreach (ProdutoImagem produtoImagemBOTemp in produtoImagemBOList)
    //    {
    //        itemArquivoBO = new ItemArquivo();
    //        itemArquivoBO.ArquivoId = produtoImagemBOTemp.Arquivo.ArquivoId;
    //        string pathFile = string.Concat(GrupoA_Resource.baseUrlUpload, this.ListFilesRevista.TargetFolder, produtoImagemBOTemp.Arquivo.NomeArquivo);
    //        itemArquivoBO.CaminhoArquivo = pathFile;
    //        itemArquivoBO.TamanhoArquivo = produtoImagemBOTemp.Arquivo.TamanhoArquivo.ToString();
    //        itemArquivoBOList.Add(itemArquivoBO);
    //    }

    //    ctlAg2ListFiles.DataSource = itemArquivoBOList;
    //    ctlAg2ListFiles.DataBind();
    //}
    #endregion
}
