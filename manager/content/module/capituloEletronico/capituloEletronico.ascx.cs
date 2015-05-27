using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;

public partial class content_module_capituloEletronico_capituloEletronico : System.Web.UI.UserControl
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
            {
                loadForm(_id);
            }
        }
    }

    /// <summary>
    /// Pesquisa os Autores do Titulo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        var capituloEletronico = new CapituloEletronicoBLL().CarregarComDependencias(
               new CapituloEletronico() { CapituloEletronicoId = Util.GetRequestId() });


        var autoresDestino = new TituloBLL().CarregarAutores(new Titulo() { TituloId = capituloEletronico.TituloEletronico.Titulo.TituloId }, new Autor() { NomeAutor = txtTitulo.Text });

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
                var capituloEletronico = new CapituloEletronicoBLL().CarregarComDependencias(
                        new CapituloEletronico() { CapituloEletronicoId = Util.GetRequestId() });

                var capitulo = new CapituloBLL().CarregarComDependencias(capituloEletronico.Capitulo);

                new CapituloBLL().ExcluirCapituloAutor(capitulo);

                List<Autor> capituloAutores = new List<Autor>();

                foreach (ListItem item in listRel.listaDestino.Items)
                {
                    var autor = new Autor();
                    autor.AutorId = Convert.ToInt32(item.Value);
                    capituloAutores.Add(autor);
                }
                new CapituloBLL().InserirAutoresDeCapitulo(capitulo, capituloAutores);

                //atualiza produto
                Produto produto = capituloEletronico.Produto;
                produto.ExibirSite = chkExibirSite.Checked;
                produto.Homologado = chkHomologado.Checked;
                produto.NomeProduto = txtNomeCapitulo.Text;
                new ProdutoBLL().Atualizar(produto);

                Util.ShowUpdateMessage();
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
            var capituloEletronico = new CapituloEletronicoBLL().CarregarComDependencias(
                new CapituloEletronico() { CapituloEletronicoId = id });

            this.txtNomeCapitulo.Text = capituloEletronico.Produto.NomeProduto;
            this.chkExibirSite.Checked = capituloEletronico.Produto.ExibirSite;
            chkHomologado.Checked = capituloEletronico.Produto.Homologado;
            this.txtDisponivel.Text = capituloEletronico.Produto.Disponivel ? "Sim " : "Não";
            this.txtQtdPaginas.Text = capituloEletronico.Capitulo.NumeroPaginaCapitulo.ToString();
            this.txtResumoCapitulo.Text = capituloEletronico.Capitulo.ResumoCapitulo.ToString();
            this.txtValorUnitario.Text = capituloEletronico.Produto.ValorUnitario.ToString();
            this.txtValorOferta.Text = capituloEletronico.Produto.ValorOferta.ToString();
            this.txtTilulo.Text = capituloEletronico.TituloEletronico.Produto.NomeProduto.ToString();
            this.txtSubtitulo.Text = capituloEletronico.TituloEletronico.Titulo.SubtituloLivro.ToString();
            this.txtEdicao.Text = capituloEletronico.TituloEletronico.Titulo.Edicao.ToString();
            this.txtIsbn13.Text = capituloEletronico.TituloEletronico.Isbn13.ToString();
            this.txtDataPublicacao.Text = capituloEletronico.TituloEletronico.Titulo.DataPublicacao != null ?
                Convert.ToDateTime(capituloEletronico.TituloEletronico.Titulo.DataPublicacao).ToString("dd/MM/yyyy") : string.Empty;
            
            this.CarregarAutoresCapitulo(capituloEletronico.Capitulo.CapituloId);
        }
    }

    /// <summary>
    /// Carrega Autores do Capitulo Eletrônico
    /// </summary>
    /// <param name="tituloEletronicoId"></param>
    private void CarregarAutoresCapitulo(int capituloId)
    {
        var autoresOrigem = new CapituloBLL().CarregarComDependencias(new Capitulo() { CapituloId = capituloId });

        foreach (var item in autoresOrigem.Autores)
            listRel.listaDestino.Items.Add(new ListItem(item.NomeAutor, item.AutorId.ToString()));
    }

    #endregion
}
