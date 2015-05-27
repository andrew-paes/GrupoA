using System;
using System.Collections.Generic;
using System.Web.UI;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using GrupoA.GlobalResources;

public partial class content_module_titulo_titulo : System.Web.UI.UserControl
{
    #region Propriedades

    private int _id
    {
        get { if (Session["_idTitulo"] == null) Session["_idTitulo"] = 0; return (int)Session["_idTitulo"]; }
        set { if (Session["_idTitulo"] == null) Session["_idTitulo"] = 0; Session["_idTitulo"] = (int)value; }
    }

    private int _idArquivo
    {
        get { if (Session["_idArquivo"] == null) Session["_idArquivo"] = 0; return (int)Session["_idArquivo"]; }
        set { if (Session["_idArquivo"] == null) Session["_idArquivo"] = 0; Session["_idArquivo"] = (int)value; }
    }

    private string _NomeArquivo
    {
        get { if (Session["_NomeArquivo"] == null) Session["_NomeArquivo"] = string.Empty; return (string)Session["_NomeArquivo"]; }
        set { if (Session["_NomeArquivo"] == null) Session["_NomeArquivo"] = string.Empty; Session["_NomeArquivo"] = (string)value; }
    }

    #endregion

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        ListFiles1.BindList += new EventHandler<ArquivoEventArgs>(ListFiles1_BindList);
        ListFiles1.DeleteItem += new EventHandler<ArquivoEventArgs>(ListFiles1_DeleteItem);

        if (Util.GetRequestId() > 0)
        {
            var _id = Util.GetRequestId();

            if (!IsPostBack)
                loadForm(_id);
            ListFiles1.RegistroId = _id;
            this.CarregarImagensTitulo();
        }
    }


    void ListFiles1_DeleteItem(object sender, ArquivoEventArgs e)
    {
        //var arquivo = new ArquivoBLL().CarregarArquivo(new Arquivo() { ArquivoId = e.ArquivoId });
        //if (arquivo != null)
        //{
        //    string pathFileDelete = string.Concat(Server.MapPath(GrupoA_Resource.baseUrlUpload), ListFiles1.TargetFolder, arquivo.NomeArquivo);
        //    System.IO.FileInfo infoDelete = new System.IO.FileInfo(pathFileDelete);
        //    if (infoDelete != null)
        //        infoDelete.Delete();
        //}

        //Exclui arquivos e relacionamento anterior
        var produtoImagens = new ProdutoBLL()
            .CarregarProdutoImagem(new ProdutoImagem()
            {
                Produto = new Produto() { ProdutoId = Util.GetRequestId() },
                //Arquivo = new Arquivo() { ArquivoId = e.ArquivoId },
                //ProdutoImagemTipo = new ProdutoImagemTipo()
                //{
                //    ProdutoImagemTipoId = (int)TipoDeImagemDoProduto.GaleriaDeImagens
                //}
            });

        foreach (ProdutoImagem produtoImagem in produtoImagens)
        {
            string pathFileDelete = string.Concat(Server.MapPath(GrupoA_Resource.baseUrlUpload), ListFiles1.TargetFolder, produtoImagem.Arquivo.NomeArquivo);
            System.IO.FileInfo infoDelete = new System.IO.FileInfo(pathFileDelete);
            if (infoDelete != null)
                infoDelete.Delete();
        }

        new ProdutoBLL().ExcluirArquivosProdutoImagem(produtoImagens);

        this.CarregarImagensTitulo();

    }

    void ListFiles1_BindList(object sender, ArquivoEventArgs e)
    {
        this.CarregarImagensTitulo();
    }

    private void CarregarImagensTitulo()
    {
        var produtoimagens = new ProdutoBLL()
            .CarregarProdutoImagem(
                new ProdutoImagem()
                {
                    Produto = new Produto() { ProdutoId = ListFiles1.RegistroId }
                }
            );

        List<ItemArquivo> arquivos = new List<ItemArquivo>();
        ItemArquivo arquivo = null;
        foreach (var item in produtoimagens)
        {

            arquivo = new ItemArquivo();
            arquivo.ArquivoId = item.Arquivo.ArquivoId;
            string pathFile = string.Concat(GrupoA_Resource.baseUrlUpload, ListFiles1.TargetFolder, item.Arquivo.NomeArquivo);
            arquivo.CaminhoArquivo = pathFile;
            arquivo.TamanhoArquivo = item.Arquivo.TamanhoArquivo.ToString();
            arquivos.Add(arquivo);
        }

        ListFiles1.DataSource = arquivos;
        ListFiles1.DataBind();


    }

    protected void btnExecute_Click(object sender, ImageClickEventArgs e)
    {
        saveOrUpdate();
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
            _id = id;

            var tituloImpresso = new TituloImpressoBLL().CarregarTituloImpressoComDependencias(new TituloImpresso() { TituloImpressoId = id });
            tituloImpresso.Titulo = new TituloBLL().Carregar(tituloImpresso.Titulo);
            tituloImpresso.Produto.Conteudo.Categorias = new ConteudoBLL().CarregarTodasAreasConhecimentoCategoria(tituloImpresso.Produto.Conteudo);
            tituloImpresso.Titulo.TituloAutores = new List<TituloAutor>();
            var autores = new TituloBLL().CarregarAutores(tituloImpresso.Titulo);
            foreach (var item in autores)
            {
                var tituloAutor = new TituloAutor();
                tituloAutor.Autor = item;
                tituloImpresso.Titulo.TituloAutores.Add(tituloAutor);
            }
            tituloImpresso.Produto.Conteudo.Categorias = new CategoriaBLL().CarregarCategoriaPorProduto(tituloImpresso.TituloImpressoId);
            if (tituloImpresso.Produto.Conteudo.Categorias != null && tituloImpresso.Produto.Conteudo.Categorias.Count > 0)
            {
                txtNomeCategoria.Text = tituloImpresso.Produto.Conteudo.Categorias[0].NomeCategoria;
            }
            txtTituloLivro.Text = tituloImpresso.Titulo.NomeTitulo;
            txtSubtituloLivro.Text = tituloImpresso.Titulo.SubtituloLivro;
            lblPaginas.Text = tituloImpresso.Titulo.NumeroPaginas.ToString();
            lblEdicao.Text = tituloImpresso.Titulo.Edicao.ToString();
            lblDtLancamento.Text = tituloImpresso.Titulo.DataLancamento == null ? String.Empty : tituloImpresso.Titulo.DataLancamento.Value.ToString("dd/MM/yyyy");
            lblISBN10.Text = tituloImpresso.Isbn10;
            lblISBN13.Text = tituloImpresso.Isbn13;
            lblDtCadastro.Text = tituloImpresso.Titulo.DataLancamento == null ? String.Empty : tituloImpresso.Titulo.DataLancamento.Value.ToString("dd/MM/yyyy");
            lblDtPublicacao.Text = tituloImpresso.Titulo.DataPublicacao == null ? String.Empty : tituloImpresso.Titulo.DataPublicacao.Value.ToString("dd/MM/yyyy");
            lblValorUnitario.Text = tituloImpresso.Produto.ValorUnitario.ToString();
            lblValorPromocao.Text = tituloImpresso.Produto.ValorOferta.ToString();
            hddProdutoId.Value = tituloImpresso.Produto.ProdutoId.ToString();
            hddTituloId.Value = tituloImpresso.Titulo.TituloId.ToString();
            chkHomologado.Checked = tituloImpresso.Produto.Homologado;
            chkDisponivel.Checked = tituloImpresso.Produto.Disponivel;
            chkExibirSite.Checked = tituloImpresso.Produto.ExibirSite;
            txtFormato.Text = tituloImpresso.Titulo.Formato;

            gvAutores.DataSource = tituloImpresso.Titulo.TituloAutores;
            gvAutores.DataBind();
        }
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
            Titulo titulo = new Titulo();
            titulo.TituloId = Convert.ToInt32(hddTituloId.Value.ToString());
            titulo.NomeTitulo = txtTituloLivro.Text;
            if (!String.IsNullOrEmpty(txtSubtituloLivro.Text))
            {
                titulo.SubtituloLivro = txtSubtituloLivro.Text;
            }
            else
            {
                titulo.SubtituloLivro = null;
            }

            Produto produto = new Produto();
            produto.ProdutoId = Convert.ToInt32(hddProdutoId.Value.ToString());
            produto.NomeProduto = txtTituloLivro.Text;

            new TituloBLL().AtualizarNomeSubTitulo(titulo, produto);
            // Envia atualização à todos os usuários que contém alerta desse evento
            Util.ShowUpdateMessage();
        }
    }



    #endregion
}
