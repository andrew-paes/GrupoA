using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;

public partial class content_module_tituloEletronicoAutor_tituloEletronicoAutor : System.Web.UI.UserControl
{
    #region Propriedades

    private int _id
    {
        get { if (Session["_idTitulo"] == null) Session["_idTitulo"] = 0; return (int)Session["_idTitulo"]; }
        set { if (Session["_idTitulo"] == null) Session["_idTitulo"] = 0; Session["_idTitulo"] = (int)value; }
    }

    #endregion

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            listRel.TituloOrigem = "Lista de autores";
            listRel.TituloDestino = "Autores associados";
        }

        if (Util.GetRequestId() > 0)
        {
            _id = Util.GetRequestId();
            if (!IsPostBack)
                loadForm(_id);
        }
    }

    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        var autoresDestino = new AutorBLL().CarregarAutores(txtTitulo.Text);

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

    protected void btnExecute_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (_id != 0)
            {

                var tituloEletronico = new TituloEletronicoBLL().Carregar(new TituloEletronico() { TituloEletronicoId = _id });
                
                var tituloId = tituloEletronico.Titulo.TituloId;

                new TituloBLL().ExcluirTituloAutor(
                    new TituloAutor() { Titulo = new Titulo() { TituloId = tituloId } });

                List<TituloAutor> tituloAutores = new List<TituloAutor>();

                foreach (ListItem item in listRel.listaDestino.Items)
                {
                    var tituloAutor = new TituloAutor();
                    tituloAutor.Titulo = new Titulo() { TituloId = tituloId };
                    tituloAutor.Autor = new Autor() { AutorId = Convert.ToInt32(item.Value) };
                    tituloAutores.Add(tituloAutor);
                }
                new TituloBLL().InserirTituloAutor(tituloAutores);
                Util.ShowUpdateMessage();
            }
        }
    }

    #endregion

    #region Métodos

    protected void loadForm(int id)
    {
        if (id > 0)
        {
            var titulo = new TituloEletronicoBLL().CarregarComDependencias(new TituloEletronico() { TituloEletronicoId = id });
            lblTitulo.Text = titulo.Produto.NomeProduto;
            lblIsbn13.Text = titulo.Isbn13;
            _id = titulo.TituloEletronicoId;
            carregarAutores(id);
        }
    }

    private void carregarAutores(int tituloId)
    {
        var autoresOrigem = new TituloEletronicoBLL().CarregarComDependencias(new TituloEletronico() { TituloEletronicoId = tituloId });
        autoresOrigem.Titulo.TituloAutores = new List<TituloAutor>();
        var autores = new TituloBLL().CarregarAutores(autoresOrigem.Titulo);
        foreach (var item in autores)
        {
            listRel.listaDestino.Items.Add(new ListItem(item.NomeAutor, item.AutorId.ToString()));
        }
    }

    #endregion
}
