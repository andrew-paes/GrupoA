using System;
using System.Collections.Generic;
using System.Web.UI;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;

public partial class content_module_tituloEletronico_tituloEletronico : System.Web.UI.UserControl
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
            var _id = Util.GetRequestId();

            if (!IsPostBack)
                loadForm(_id);
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
            var tituloEletronico = new TituloEletronicoBLL().CarregarComDependencias(new TituloEletronico() { TituloEletronicoId = id });
            tituloEletronico.Titulo = new TituloBLL().Carregar(tituloEletronico.Titulo);
            tituloEletronico.Produto.Conteudo.Categorias = new ConteudoBLL().CarregarTodasAreasConhecimentoCategoria(tituloEletronico.Produto.Conteudo);
            tituloEletronico.Titulo.TituloAutores = new List<TituloAutor>();
            var autores = new TituloBLL().CarregarAutores(tituloEletronico.Titulo);

            foreach (var item in autores)
            {
                var tituloAutor = new TituloAutor();
                tituloAutor.Autor = item;
                tituloEletronico.Titulo.TituloAutores.Add(tituloAutor);
            }

            tituloEletronico.Produto.Conteudo.Categorias = new CategoriaBLL().CarregarCategoriaPorProduto(tituloEletronico.TituloEletronicoId);

            if (tituloEletronico.Produto.Conteudo.Categorias != null && tituloEletronico.Produto.Conteudo.Categorias.Count > 0)
            {
                this.txtNomeCategoria.Text = tituloEletronico.Produto.Conteudo.Categorias[0].NomeCategoria;
            }

            this.txtTituloLivro.Text = tituloEletronico.Titulo.NomeTitulo;
            this.txtSubtituloLivro.Text = tituloEletronico.Titulo.SubtituloLivro;
            this.lblPaginas.Text = tituloEletronico.Titulo.NumeroPaginas != null ? tituloEletronico.Titulo.NumeroPaginas.ToString() : "";
            this.lblEdicao.Text = tituloEletronico.Titulo.Edicao != null && tituloEletronico.Titulo.Edicao.Value != null ? tituloEletronico.Titulo.Edicao.Value.ToString() : "";

            this.lblDtLancamento.Text = tituloEletronico.Titulo.DataLancamento != null && tituloEletronico.Titulo.DataLancamento.Value != null ? tituloEletronico.Titulo.DataLancamento.Value.ToString("dd/MM/yyyy") : "";

            this.lblISBN13.Text = tituloEletronico.Isbn13;
            this.lblDtCadastro.Text = tituloEletronico.Produto.Conteudo.DataHoraCadastro != null ? tituloEletronico.Produto.Conteudo.DataHoraCadastro.ToString("dd/MM/yyyy") : "";

            this.lblDtPublicacao.Text = tituloEletronico.Titulo.DataPublicacao != null && tituloEletronico.Titulo.DataPublicacao.Value != null ? tituloEletronico.Titulo.DataPublicacao.Value.ToString("dd/MM/yyyy") : "";

            this.lblValorUnitario.Text = tituloEletronico.Produto.ValorUnitario.ToString();
            this.lblValorPromocao.Text = tituloEletronico.Produto.ValorOferta.ToString();
            this.chkHomologado.Checked = tituloEletronico.Produto.Homologado;

            this.hddProdutoId.Value = tituloEletronico.Produto.ProdutoId.ToString();
            this.hddTituloId.Value = tituloEletronico.Titulo.TituloId.ToString();

            this.gvAutores.DataSource = tituloEletronico.Titulo.TituloAutores;
            this.gvAutores.DataBind();
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
            produto.Homologado = chkHomologado.Checked;

            new TituloBLL().AtualizarNomeSubTitulo(titulo, produto);
            // Envia atualização à todos os usuários que contém alerta desse evento

            new ProdutoBLL().AtualizaHomologado(produto);

            Util.ShowUpdateMessage();
        }
    }

    #endregion
}
