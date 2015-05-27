using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;

[Serializable()]
public class AutorGrid
{
    public Int32 AutorId { get; set; }
    public String NomeAutor { get; set; }
}

public partial class content_module_autorTitulo_autorTitulo : System.Web.UI.UserControl
{
    #region Propriedades

    private int _id
    {
        get
        {
            if (Session["_idTitulo"] == null)
                Session["_idTitulo"] = 0;

            return (int)Session["_idTitulo"];
        }
        set
        {
            if (Session["_idTitulo"] == null)
                Session["_idTitulo"] = 0;

            Session["_idTitulo"] = (int)value;
        }
    }
    public List<AutorGrid> AutoresGrid
    {
        get
        {
            return ViewState["AutoresGrid"] as List<AutorGrid>;
        }
        set
        {
            ViewState["AutoresGrid"] = value;
        }
    }

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
            _id = Util.GetRequestId();

            if (!IsPostBack)
            {
                this.MontaAutoresAdicionados();
                loadForm(_id);
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
        txtInclusaoRapida.Text = string.Empty;
        Autor autor = new Autor();
        autor.NomeAutor = txtFiltroAutor.Text;

        Titulo titulo = new Titulo();
        titulo.TituloId = _id;

        var autores = new AutorBLL().CarregarAutoresPorNome(autor, titulo);

        this.PopulaListas(autores);
    }

    protected void PopulaListas(List<Autor> autores)
    {
        lstOrigem.Items.Clear();

        // CONTROLE DE IDs JA INSERIDOS
        string ids = "";
        foreach (ListItem lst in lstDestino.Items)
        {
            if (ids.Length == 0)
            {
                ids = String.Concat("#", lst.Value, "#");
            }
            else
            {
                ids = String.Concat(ids, ", ", "#", lst.Value, "#");
            }
        }

        foreach (var item in autores)
        {
            if (!ids.Contains(String.Concat("#", item.AutorId.ToString(), "#")))
            {
                lstOrigem.Items.Add(new ListItem(item.NomeAutor, item.AutorId.ToString()));
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdicionarT_Click(object sender, EventArgs e)
    {
        foreach (ListItem lst in lstOrigem.Items)
        {
            lstDestino.Items.Add(lst);
        }
        lstOrigem.Items.Clear();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdicionar_Click(object sender, EventArgs e)
    {
        if (lstOrigem.SelectedIndex >= 0)
        {
            List<ListItem> lRemover = new List<ListItem>();
            for (int i = 0; i < lstOrigem.Items.Count; i++)
            {
                if (lstOrigem.Items[i].Selected)
                {
                    lstDestino.Items.Add(lstOrigem.Items[i]);
                    lRemover.Add(lstOrigem.Items[i]);
                }
            }

            foreach (ListItem item in lRemover)
            {
                lstOrigem.Items.Remove(item);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRemoverT_Click(object sender, EventArgs e)
    {
        foreach (ListItem lst in lstDestino.Items)
        {
            lstOrigem.Items.Add(lst);
        }
        lstDestino.Items.Clear();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRemover_Click(object sender, EventArgs e)
    {
        if (lstDestino.SelectedIndex >= 0)
        {
            List<ListItem> lRemover = new List<ListItem>();
            for (int i = 0; i < lstDestino.Items.Count; i++)
            {
                if (lstDestino.Items[i].Selected)
                {
                    lstOrigem.Items.Add(lstDestino.SelectedItem);
                    lRemover.Add(lstDestino.SelectedItem);
                }
            }

            foreach (ListItem item in lRemover)
            {
                lstDestino.Items.Remove(item);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubir_Click(object sender, EventArgs e)
    {
        if (lstDestino.Items.Count > 1)
        {
            if (lstDestino.SelectedIndex > 0)
            {
                Int32 posicaoAnterior = 0;
                ListItem liAnterior = null;

                for (int i = 0; i < lstDestino.Items.Count; i++)
                {
                    if (lstDestino.Items[i].Selected)
                    {
                        ListItem liAux = new ListItem();
                        liAux.Text = liAnterior.Text;
                        liAux.Value = liAnterior.Value;

                        lstDestino.Items[posicaoAnterior].Text = lstDestino.Items[i].Text;
                        lstDestino.Items[posicaoAnterior].Value = lstDestino.Items[i].Value;
                        lstDestino.Items[i].Text = liAux.Text;
                        lstDestino.Items[i].Value = liAux.Value;

                        lstDestino.SelectedIndex = lstDestino.SelectedIndex - 1;
                    }
                    posicaoAnterior = i;
                    liAnterior = lstDestino.Items[i];
                }

                //lstDestino.SelectedIndex = -1;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDescer_Click(object sender, EventArgs e)
    {
        if (lstDestino.Items.Count > 1)
        {
            if (lstDestino.SelectedIndex < (lstDestino.Items.Count - 1))
            {
                Int32 posicaoProxima = 0;
                ListItem liProxima = null;

                for (int i = lstDestino.Items.Count - 1; i >= 0; i--)
                {
                    if (lstDestino.Items[i].Selected)
                    {
                        ListItem liAux = new ListItem();
                        liAux.Text = liProxima.Text;
                        liAux.Value = liProxima.Value;

                        lstDestino.Items[posicaoProxima].Text = lstDestino.Items[i].Text;
                        lstDestino.Items[posicaoProxima].Value = lstDestino.Items[i].Value;
                        lstDestino.Items[i].Text = liAux.Text;
                        lstDestino.Items[i].Value = liAux.Value;

                        lstDestino.SelectedIndex = lstDestino.SelectedIndex + 1;
                    }
                    posicaoProxima = i;
                    liProxima = lstDestino.Items[i];
                }

                //lstDestino.SelectedIndex = -1;
            }
        }
    }

    #endregion

    #region Métodos

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_id"></param>
    private void loadForm(int _id)
    {
        if (_id > 0)
        {
            var titulo = new TituloBLL().CarregarComDependencias(new Titulo() { TituloId = _id });
            txtTitulo.Text = titulo.NomeTitulo;
            txtSubtitulo.Text = titulo.SubtituloLivro;

            if (titulo.TituloImpresso != null)
            {
                txtIsbn.Text = titulo.TituloImpresso.Isbn13;
            }
            else if (titulo.TituloEletronico != null)
            {
                txtIsbn.Text = titulo.TituloEletronico.Isbn13;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void SaveOrUpdate()
    {
        if (Page.IsValid)
        {
            Titulo titulo = new Titulo();
            titulo.TituloId = _id;

            List<TituloAutor> tituloAutores = new List<TituloAutor>();

            if (lstDestino.Items.Count > 0)
            {
                Int32 ordem = 0;
                foreach (ListItem item in lstDestino.Items)
                {
                    ordem++;
                    TituloAutor tituloAutor = new TituloAutor();
                    tituloAutor.Autor = new Autor();
                    tituloAutor.Autor.AutorId = Convert.ToInt32(item.Value);
                    tituloAutor.Titulo = titulo;
                    tituloAutor.Ordem = ordem;

                    tituloAutores.Add(tituloAutor);
                }

                new TituloBLL().AtualizarTituloAutor(tituloAutores, titulo);
            }
            else
            {
                new TituloBLL().ExcluirTodosTituloAutor(titulo);
            }


            Util.ShowUpdateMessage();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    protected void MontaAutoresAdicionados()
    {
        Titulo titulo = new Titulo();
        titulo.TituloId = _id;

        List<Autor> autores = new TituloBLL().CarregarAutores(titulo);

        if (AutoresGrid == null)
        {
            AutoresGrid = new List<AutorGrid>();
        }

        if (autores.Count > 0)
        {
            foreach (Autor item in autores)
            {
                AutorGrid autorGrid = new AutorGrid();
                autorGrid.AutorId = item.AutorId;
                autorGrid.NomeAutor = item.NomeAutor;

                if (AutoresGrid.Find(p => p.AutorId == autorGrid.AutorId) == null)
                {
                    AutoresGrid.Add(autorGrid);
                }
            }
        }

        lstOrigem.Items.Clear();

        foreach (var item in autores)
        {
            lstDestino.Items.Add(new ListItem(item.NomeAutor, item.AutorId.ToString()));
        }
    }

    #endregion

    protected void btnInclusaoRapida_Click(object sender, ImageClickEventArgs e)
    {
        lblMensagem.Text = string.Empty;
        string nomeAutor = txtInclusaoRapida.Text.Trim();
        txtInclusaoRapida.Text = string.Empty;
        if (!String.IsNullOrWhiteSpace(nomeAutor))
        {
            var autorBLL = new AutorBLL();

            var autorExiste = autorBLL.CarregarAutorPorNome(new Autor() { NomeAutor = nomeAutor });

            if (autorExiste == null || autorExiste.AutorId == 0)
            {
                var autor = new Autor() { NomeAutor = nomeAutor };
                autorBLL.InserirNovoAutor(autor);

                if (autor.AutorId > 0)
                {
                    lstDestino.Items.Add(new ListItem(autor.NomeAutor, autor.AutorId.ToString()));
                }
                else
                {
                    lblMensagem.Text = "Erro ao inserir autor.";
                }
            }
            else
            {
                //autor já existe
                lblMensagem.Text = "Autor já cadastrado anteriormente. Selecione-o na lista abaixo e clique em adicionar para relacionar este autor.";
                var autores = new List<Autor>();
                autores.Add(autorExiste);
                this.PopulaListas(autores);
            }
        }
        else
        {
            lblMensagem.Text = "Preencha o campo antes de incluir um autor.";
        }

    }
}
