using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;

public partial class content_module_capituloImpresso_capituloImpresso : System.Web.UI.UserControl
{
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
            listRel.TituloOrigem = "Lista de autores";
            listRel.TituloDestino = "Autores associados";
        }

        if (Util.GetRequestId() > 0)
        {
            var _id = Util.GetRequestId();

            if (!IsPostBack)
                loadForm(_id);
        }
    }

    /// <summary>
    /// Pesquisa os Autores do Titulo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        var capituloImpresso = new CapituloImpressoBLL().CarregarComDependencias(
               new CapituloImpresso() { CapituloImpressoId = Util.GetRequestId() });

        var autoresDestino = new TituloBLL().CarregarAutores(new Titulo() { TituloId = capituloImpresso.TituloImpresso.Titulo.TituloId }, new Autor() { NomeAutor = txtTitulo.Text });

        listRel.listaOrigem.Items.Clear();

        // CONTROLE DE IDs JA INSERIDOS
        string ids = "";
        foreach (ListItem lst in listRel.listaDestino.Items)
        {
            if (ids.Length == 0)
                ids = lst.Value;
            else
                ids += ", " + lst.Value;
        }

        foreach (var item in autoresDestino)
        {
            if (!ids.Contains(item.AutorId.ToString()))
                listRel.listaOrigem.Items.Add(new ListItem(item.NomeAutor, item.AutorId.ToString()));
        }
    }

    /// <summary>
    /// Exclui Todos Relacionamentos e Insere os Autores
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExecute_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Util.GetRequestId() != 0)
            {
                var capituloImpresso = new CapituloImpressoBLL().CarregarComDependencias(
                        new CapituloImpresso() { CapituloImpressoId = Util.GetRequestId() });

                var capitulo = new CapituloBLL().CarregarComDependencias(capituloImpresso.Capitulo);

                new CapituloBLL().ExcluirCapituloAutor(capitulo);

                List<Autor> capituloAutores = new List<Autor>();

                foreach (ListItem item in listRel.listaDestino.Items)
                {
                    var autor = new Autor();
                    autor.AutorId = Convert.ToInt32(item.Value);
                    capituloAutores.Add(autor);
                }

                new CapituloBLL().InserirAutoresDeCapitulo(capitulo, capituloAutores);
                Util.ShowUpdateMessage();

                //Atualiza capítulo
                capitulo.NomeCapitulo = txtNomeCapitulo.Text;
                new CapituloBLL().Atualizar(capitulo);

                //atualiza produto
                Produto produto = capituloImpresso.Produto;
                produto.ExibirSite = this.chkExibirSite.Checked;
                produto.Homologado = this.chkHomologado.Checked;
                produto.NomeProduto = txtNomeCapitulo.Text;
                new ProdutoBLL().Atualizar(produto);
            }
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
            var capituloImpresso = new CapituloImpressoBLL().CarregarComDependencias(
                new CapituloImpresso() { CapituloImpressoId = id });

            this.txtNomeCapitulo.Text = capituloImpresso.Produto.NomeProduto;
            this.chkExibirSite.Checked = capituloImpresso.Produto.ExibirSite;
            this.chkHomologado.Checked = capituloImpresso.Produto.Homologado;
            this.txtDisponivel.Text = capituloImpresso.Produto.Disponivel ? "Sim " : "Não";
            this.txtQtdPaginas.Text = capituloImpresso.Capitulo.NumeroPaginaCapitulo.ToString();
            this.txtResumoCapitulo.Text = capituloImpresso.Capitulo.ResumoCapitulo.ToString();
            this.txtValorUnitario.Text = capituloImpresso.Produto.ValorUnitario.ToString();
            this.txtValorOferta.Text = capituloImpresso.Produto.ValorOferta.ToString();
            this.txtTilulo.Text = capituloImpresso.TituloImpresso.Produto.NomeProduto.ToString();
            this.txtSubtitulo.Text = capituloImpresso.TituloImpresso.Titulo.SubtituloLivro.ToString();
            this.txtEdicao.Text = capituloImpresso.TituloImpresso.Titulo.Edicao.ToString();
            this.txtIsbn13.Text = capituloImpresso.TituloImpresso.Isbn13.ToString();
            string dataPublicacao = capituloImpresso.TituloImpresso.Titulo.DataPublicacao.ToString();
            this.txtDataPublicacao.Text = !String.IsNullOrEmpty(dataPublicacao) ? Convert.ToDateTime(dataPublicacao).ToString("dd/MM/yyyy") : string.Empty;

            this.CarregarAutoresCapitulo(capituloImpresso.Capitulo.CapituloId);
        }
    }

    /// <summary>
    /// Carrega Autores do Capitulo Eletrônico
    /// </summary>
    /// <param name="tituloImpressoId"></param>
    private void CarregarAutoresCapitulo(int capituloId)
    {
        var autoresOrigem = new CapituloBLL().CarregarComDependencias(new Capitulo() { CapituloId = capituloId });

        foreach (var item in autoresOrigem.Autores)
            listRel.listaDestino.Items.Add(new ListItem(item.NomeAutor, item.AutorId.ToString()));
    }

    #endregion
}
