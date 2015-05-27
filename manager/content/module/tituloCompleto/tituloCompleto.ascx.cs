using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Linq;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using GrupoA.GlobalResources;
using System.IO;
using System.Web.UI.WebControls;

[Serializable()]
public class AutorGrid
{
    public Int32 AutorId { get; set; }
    public String NomeAutor { get; set; }
}

public partial class content_module_tituloCompleto_tituloCompleto : System.Web.UI.UserControl
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

    public Int32 TituloConteudoExtraArquivoId
    {
        get
        {
            return String.IsNullOrEmpty(this.hddTituloConteudoExtraArquivoId.Value) ? 0 : Convert.ToInt32(this.hddTituloConteudoExtraArquivoId.Value);
        }
        set
        {
            this.hddTituloConteudoExtraArquivoId.Value = value.ToString();

            if (value != null && Convert.ToInt32(value) > 0)
            {
                this.btnAdicionarConteudoExtraArquivo.ImageUrl = "~/img/btn_atualizar.gif";
            }
            else
            {
                this.btnAdicionarConteudoExtraArquivo.ImageUrl = "~/img/btn_Adicionar.png";
            }
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

    #region [ Page Events ]

    protected void Page_Load(object sender, EventArgs e)
    {
        this._tituloId = Util.GetRequestId();

        if (this._tituloId > 0)
        {
            this.CarregarObjetos();

            this.ListFilesImagemResumo.BindList += new EventHandler<ArquivoEventArgs>(ListFilesImagemResumo_BindList);
            this.ListFilesImagemResumo.DeleteItem += new EventHandler<ArquivoEventArgs>(ListFilesImagemResumo_DeleteItem);
            this.ListFilesImagemResumo_BindList(new object(), new ArquivoEventArgs());

            this.ListFilesImagemEspecialista.BindList += new EventHandler<ArquivoEventArgs>(ListFilesImagemEspecialista_BindList);
            this.ListFilesImagemEspecialista.DeleteItem += new EventHandler<ArquivoEventArgs>(ListFilesImagemEspecialista_DeleteItem);
            this.ListFilesImagemEspecialista_BindList(new object(), new ArquivoEventArgs());

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

        if (this.hddTableTitulo.Value != null && this.hddTableTitulo.Value == "0")
            strScript += "SetTable($('#fieldTitulo'), $('#tableTitulo'), 'Titulo');";

        if (this.hddTableComentarioEspecialista.Value != null && this.hddTableComentarioEspecialista.Value == "0")
            strScript += "SetTable($('#fieldComentarioEspecialista'), $('#tableComentarioEspecialista'), 'Comentário Especialista');";

        if (this.hddTableMaterialComplementar.Value != null && this.hddTableMaterialComplementar.Value == "0")
            strScript += "SetTable($('#fieldMaterialComplementar'), $('#tableMaterialComplementar'), 'Material Complementar');";

        if (this.hddTableAutor.Value != null && this.hddTableAutor.Value == "0")
            strScript += "SetTable($('#fieldAutor'), $('#tableAutor'), 'Autores');";

        if (this.hddTableInfoComplementares.Value != null && this.hddTableInfoComplementares.Value == "0")
            strScript += "setTimeout(\"SetTable($('#fieldInfoComplementares'), $('#tableInfoComplementares'), 'Informações Complementares');\", 2000);";

        strScript = String.Concat("$(document).ready(function () {", strScript, "});");
        Page.ClientScript.RegisterClientScriptBlock(GetType(), "jsSetPanel", strScript, true);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ListFilesImagemResumo_BindList(object sender, ArquivoEventArgs e)
    {
        if (this.TituloBO != null && this.TituloBO.TituloId > 0)
        {
            this.ListFilesImagemResumo.RegistroId = this.TituloBO.TituloId;

            if (
                this.TituloBO.TituloImagemResumos != null
                && this.TituloBO.TituloImagemResumos.Any()
                && this.TituloBO.TituloImagemResumos[0] != null
                && this.TituloBO.TituloImagemResumos[0].TituloImagemResumoId > 0
                && this.TituloBO.TituloImagemResumos[0].Arquivo != null
                && this.TituloBO.TituloImagemResumos[0].Arquivo.ArquivoId > 0
                )
            {
                this.ListFilesImagemResumo.ArquivoId = this.TituloBO.TituloImagemResumos[0].Arquivo.ArquivoId;
            }

            if (e.ArquivoId > 0)
            {
                this.ExcluirImagemResumo();

                try
                {
                    TituloImagemResumo tituloImagemResumoBO = new TituloImagemResumo();
                    tituloImagemResumoBO.Titulo = new Titulo();
                    tituloImagemResumoBO.Titulo = this.TituloBO;
                    tituloImagemResumoBO.Arquivo = new Arquivo();
                    tituloImagemResumoBO.Arquivo.ArquivoId = e.ArquivoId;

                    tituloImagemResumoBO = new TituloImagemResumoBLL().Inserir(tituloImagemResumoBO);

                    this.ListFilesImagemResumo.ArquivoId = e.ArquivoId;
                }
                catch { }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ListFilesImagemResumo_DeleteItem(object sender, ArquivoEventArgs e)
    {
        if (this.TituloBO != null && this.TituloBO.TituloId > 0)
        {
            this.ListFilesImagemResumo.RegistroId = this.TituloBO.TituloId;
            this.ExcluirImagemResumo();

            this.ListFilesImagemResumo.DataSource = null;
            this.ListFilesImagemResumo.ArquivoId = 0;
            this.ListFilesImagemResumo.DataBind();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ListFilesImagemEspecialista_BindList(object sender, ArquivoEventArgs e)
    {
        if (this.TituloBO != null && this.TituloBO.TituloId > 0)
        {
            this.ListFilesImagemEspecialista.RegistroId = this.TituloBO.TituloId;

            if (
                this.TituloBO.TituloInformacaoComentarioEspecialista != null
                && this.TituloBO.TituloInformacaoComentarioEspecialista.ArquivoImagem != null
                && this.TituloBO.TituloInformacaoComentarioEspecialista.ArquivoImagem.ArquivoId > 0
                )
            {
                this.ListFilesImagemEspecialista.ArquivoId = this.TituloBO.TituloInformacaoComentarioEspecialista.ArquivoImagem.ArquivoId;
            }
        }

        if (e.ArquivoId > 0)
        {
            this.SalvarTituloComentarioEspecialista(); // Necessita salvar antes TituloInformacaoComentarioEspecialista para poder ter um registro onde será salva a imagem
            this.CarregarObjetosComentarioEspecialista();

            if (this.TituloBO.TituloInformacaoComentarioEspecialista != null && this.TituloBO.TituloInformacaoComentarioEspecialista.TituloInformacaoComentarioEspecialistaId > 0)
            {
                this.ExcluirArquivoImagemComentarioEspecialista(this.TituloBO.TituloInformacaoComentarioEspecialista);

                this.TituloBO.TituloInformacaoComentarioEspecialista.ArquivoImagem = new Arquivo();
                this.TituloBO.TituloInformacaoComentarioEspecialista.ArquivoImagem.ArquivoId = e.ArquivoId;

                new TituloInformacaoComentarioEspecialistaBLL().Atualizar(this.TituloBO.TituloInformacaoComentarioEspecialista);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ListFilesImagemEspecialista_DeleteItem(object sender, ArquivoEventArgs e)
    {
        if (this.TituloBO != null && this.TituloBO.TituloId > 0)
        {
            this.ListFilesImagemEspecialista.RegistroId = this.TituloBO.TituloId;

            if (this.TituloBO.TituloInformacaoComentarioEspecialista != null && this.TituloBO.TituloInformacaoComentarioEspecialista.TituloInformacaoComentarioEspecialistaId > 0)
            {
                this.ExcluirArquivoImagemComentarioEspecialista(this.TituloBO.TituloInformacaoComentarioEspecialista);
            }

            this.ListFilesImagemEspecialista.DataSource = null;
            this.ListFilesImagemEspecialista.ArquivoId = 0;
            this.ListFilesImagemEspecialista.DataBind();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptTituloConteudoExtraArquivo_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item != null && e.Item.DataItem != null)
        {
            TituloConteudoExtraArquivo tituloConteudoExtraArquivoBO = (TituloConteudoExtraArquivo)e.Item.DataItem;

            Literal ltrNome = (Literal)e.Item.FindControl("ltrNome");
            Literal ltrSomenteLogado = (Literal)e.Item.FindControl("ltrSomenteLogado");
            Literal ltrRestritoProfessor = (Literal)e.Item.FindControl("ltrRestritoProfessor");
            Literal ltrAtivo = (Literal)e.Item.FindControl("ltrAtivo");
            Literal ltrDataHoraCadastro = (Literal)e.Item.FindControl("ltrDataHoraCadastro");
            ImageButton btnEditarConteudoExtraArquivo = (ImageButton)e.Item.FindControl("btnEditarConteudoExtraArquivo");
            ImageButton btnExcluirConteudoExtraArquivo = (ImageButton)e.Item.FindControl("btnExcluirConteudoExtraArquivo");

            ltrNome.Text = tituloConteudoExtraArquivoBO.NomeConteudo;
            ltrSomenteLogado.Text = tituloConteudoExtraArquivoBO.SomenteLogado != null && tituloConteudoExtraArquivoBO.SomenteLogado ? "Sim" : "Não";
            ltrRestritoProfessor.Text = tituloConteudoExtraArquivoBO.RestritoProfessor != null && tituloConteudoExtraArquivoBO.RestritoProfessor ? "Sim" : "Não";
            ltrAtivo.Text = tituloConteudoExtraArquivoBO.Ativo != null && tituloConteudoExtraArquivoBO.Ativo ? "Sim" : "Não";

            if (tituloConteudoExtraArquivoBO.DataCadastro != null)
            {
                ltrDataHoraCadastro.Text = String.Concat(tituloConteudoExtraArquivoBO.DataCadastro.ToShortDateString(), " ", tituloConteudoExtraArquivoBO.DataCadastro.ToShortTimeString());
            }

            btnEditarConteudoExtraArquivo.CommandArgument = tituloConteudoExtraArquivoBO.TituloConteudoExtraArquivoId.ToString();
            btnExcluirConteudoExtraArquivo.CommandArgument = tituloConteudoExtraArquivoBO.TituloConteudoExtraArquivoId.ToString();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdicionarConteudoExtraArquivo_Click(object sender, ImageClickEventArgs e)
    {
        TituloConteudoExtraArquivo tituloConteudoExtraArquivoBO = new TituloConteudoExtraArquivo();

        if (this.TituloConteudoExtraArquivoId > 0)
        {
            tituloConteudoExtraArquivoBO = new TituloConteudoExtraArquivoBLL().CarregarComDependencia(new TituloConteudoExtraArquivo(this.TituloConteudoExtraArquivoId));
        }
        else
        {
            tituloConteudoExtraArquivoBO.Arquivo = new Arquivo();
        }

        tituloConteudoExtraArquivoBO.Arquivo.NomeArquivoOriginal = this.txtNomeArquivoConteudoExtraArquivo.Text;
        tituloConteudoExtraArquivoBO.Arquivo.NomeArquivo = this.txtArquivoConteudoExtraArquivo.Text;
        tituloConteudoExtraArquivoBO.NomeConteudo = this.txtNomeArquivoConteudoExtraArquivo.Text;
        tituloConteudoExtraArquivoBO.SomenteLogado = this.chkSomenteLogadoConteudoExtraArquivo.Checked;
        tituloConteudoExtraArquivoBO.RestritoProfessor = this.chkRestritoProfessorConteudoExtraArquivo.Checked;
        tituloConteudoExtraArquivoBO.Ativo = this.chkAtivoConteudoExtraArquivo.Checked;
        tituloConteudoExtraArquivoBO.Titulo = new Titulo();
        tituloConteudoExtraArquivoBO.Titulo = this.TituloBO;

        if (this.TituloConteudoExtraArquivoId > 0)
        {
            new TituloConteudoExtraArquivoBLL().Atualizar(tituloConteudoExtraArquivoBO);

            Util.ShowMessage("Arquivo alterado com sucesso.", Ag2.Manager.Enumerator.typeMessage.Sucesso);
        }
        else
        {
            new TituloConteudoExtraArquivoBLL().Inserir(tituloConteudoExtraArquivoBO);

            Util.ShowMessage("Arquivo incluído com sucesso.", Ag2.Manager.Enumerator.typeMessage.Sucesso);
        }

        this.TituloConteudoExtraArquivoId = 0;
        this.txtNomeArquivoConteudoExtraArquivo.Text = String.Empty;
        this.txtArquivoConteudoExtraArquivo.Text = String.Empty;
        this.chkSomenteLogadoConteudoExtraArquivo.Checked = false;
        this.chkRestritoProfessorConteudoExtraArquivo.Checked = false;
        this.chkAtivoConteudoExtraArquivo.Checked = false;

        this.CarregarObjetosMaterialComplementar();
        this.CarregarTituloMaterialComplementar();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEditarConteudoExtraArquivo_Click(object sender, ImageClickEventArgs e)
    {
        this.TituloConteudoExtraArquivoId = Convert.ToInt32(((ImageButton)sender).CommandArgument.ToString());

        TituloConteudoExtraArquivo tituloConteudoExtraArquivo = new TituloConteudoExtraArquivoBLL().CarregarComDependencia(new TituloConteudoExtraArquivo(this.TituloConteudoExtraArquivoId));

        if (tituloConteudoExtraArquivo != null)
        {
            this.txtNomeArquivoConteudoExtraArquivo.Text = tituloConteudoExtraArquivo.NomeConteudo;
            this.txtArquivoConteudoExtraArquivo.Text = tituloConteudoExtraArquivo.Arquivo.NomeArquivo;
            this.chkSomenteLogadoConteudoExtraArquivo.Checked = tituloConteudoExtraArquivo.SomenteLogado;
            this.chkRestritoProfessorConteudoExtraArquivo.Checked = tituloConteudoExtraArquivo.RestritoProfessor;
            this.chkAtivoConteudoExtraArquivo.Checked = tituloConteudoExtraArquivo.Ativo;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExcluirConteudoExtraArquivo_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.TituloConteudoExtraArquivoId = Convert.ToInt32(((ImageButton)sender).CommandArgument.ToString());

            new TituloConteudoExtraArquivoBLL().Excluir(new TituloConteudoExtraArquivo(this.TituloConteudoExtraArquivoId));
            TituloConteudoExtraArquivoId = 0;

            this.CarregarObjetosMaterialComplementar();
            this.CarregarTituloMaterialComplementar();

            Util.ShowMessage("Arquivo excluído com sucesso.", Ag2.Manager.Enumerator.typeMessage.Sucesso);
        }
        catch
        {
            Util.ShowMessage("Não foi possível excluir o arquivo.", Ag2.Manager.Enumerator.typeMessage.Erro);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnInclusaoAutor_Click(object sender, ImageClickEventArgs e)
    {
        this.lblMensagem.Text = string.Empty;

        if (!String.IsNullOrWhiteSpace(this.txtInclusaoAutor.Text.Trim()))
        {
            Autor autorBO = new AutorBLL().CarregarAutorPorNome(new Autor() { NomeAutor = this.txtInclusaoAutor.Text.Trim() });

            if (autorBO != null && autorBO.AutorId > 0)
            {
                lstDestino.Items.Add(new ListItem(autorBO.NomeAutor, autorBO.AutorId.ToString()));
            }
            else
            {
                autorBO = new Autor() { NomeAutor = this.txtInclusaoAutor.Text.Trim() };

                try
                {
                    new AutorBLL().InserirNovoAutor(autorBO);

                    if (autorBO.AutorId > 0)
                    {
                        this.lstDestino.Items.Add(new ListItem(autorBO.NomeAutor, autorBO.AutorId.ToString()));
                    }
                }
                catch
                {
                    this.lblMensagem.Text = "Erro ao inserir autor.";
                }
            }
        }
        else
        {
            this.lblMensagem.Text = "Preencha o campo antes de incluir um autor.";
        }

        this.txtInclusaoAutor.Text = string.Empty;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPesquisarAutor_Click(object sender, ImageClickEventArgs e)
    {
        if (this.TituloBO != null && this.TituloBO.TituloId > 0)
        {
            if (!String.IsNullOrWhiteSpace(this.txtPesquisaAutor.Text.Trim()))
            {
                Autor autorBO = new Autor() { NomeAutor = this.txtPesquisaAutor.Text.Trim() };

                List<Autor> autorBOList = new AutorBLL().CarregarAutoresPorNome(autorBO, this.TituloBO);

                this.CarregaAutorListaOrigem(autorBOList);
            }
            else
            {
                this.lblMensagem.Text = "Preencha o campo antes de pesquisar um autor.";
            }
        }

        this.txtInclusaoAutor.Text = String.Empty;
        this.txtPesquisaAutor.Text = String.Empty;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdicionarT_Click(object sender, EventArgs e)
    {
        foreach (ListItem listItemTemp in this.lstOrigem.Items)
        {
            this.lstDestino.Items.Add(listItemTemp);
        }

        this.lstOrigem.Items.Clear();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdicionar_Click(object sender, EventArgs e)
    {
        if (this.lstOrigem.SelectedIndex >= 0)
        {
            List<ListItem> listItemRemover = new List<ListItem>();

            for (int i = 0; i < this.lstOrigem.Items.Count; i++)
            {
                if (lstOrigem.Items[i].Selected)
                {
                    this.lstDestino.Items.Add(this.lstOrigem.Items[i]);
                    listItemRemover.Add(this.lstOrigem.Items[i]);
                }
            }

            foreach (ListItem listItemTemp in listItemRemover)
            {
                this.lstOrigem.Items.Remove(listItemTemp);
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
        foreach (ListItem listItemTemp in this.lstDestino.Items)
        {
            this.lstOrigem.Items.Add(listItemTemp);
        }

        this.lstDestino.Items.Clear();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRemover_Click(object sender, EventArgs e)
    {
        if (this.lstDestino.SelectedIndex >= 0)
        {
            List<ListItem> listItemRemover = new List<ListItem>();

            for (int i = 0; i < this.lstDestino.Items.Count; i++)
            {
                if (this.lstDestino.Items[i].Selected)
                {
                    this.lstOrigem.Items.Add(this.lstDestino.SelectedItem);
                    listItemRemover.Add(this.lstDestino.SelectedItem);
                }
            }

            foreach (ListItem listItemTemp in listItemRemover)
            {
                this.lstDestino.Items.Remove(listItemTemp);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAutorSubir_Click(object sender, ImageClickEventArgs e)
    {
        if (this.lstDestino.Items.Count > 1)
        {
            if (this.lstDestino.SelectedIndex > 0)
            {
                Int32 posicaoAnterior = 0;
                ListItem liAnterior = null;

                for (int i = 0; i < this.lstDestino.Items.Count; i++)
                {
                    if (this.lstDestino.Items[i].Selected)
                    {
                        ListItem liAux = new ListItem();
                        liAux.Text = liAnterior.Text;
                        liAux.Value = liAnterior.Value;

                        this.lstDestino.Items[posicaoAnterior].Text = this.lstDestino.Items[i].Text;
                        this.lstDestino.Items[posicaoAnterior].Value = this.lstDestino.Items[i].Value;
                        this.lstDestino.Items[i].Text = liAux.Text;
                        this.lstDestino.Items[i].Value = liAux.Value;

                        this.lstDestino.SelectedIndex = lstDestino.SelectedIndex - 1;
                    }

                    posicaoAnterior = i;
                    liAnterior = this.lstDestino.Items[i];
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAutorDescer_Click(object sender, ImageClickEventArgs e)
    {
        if (this.lstDestino.Items.Count > 1)
        {
            if (this.lstDestino.SelectedIndex < (this.lstDestino.Items.Count - 1))
            {
                Int32 posicaoProxima = 0;
                ListItem liProxima = null;

                for (int i = this.lstDestino.Items.Count - 1; i >= 0; i--)
                {
                    if (this.lstDestino.Items[i].Selected)
                    {
                        ListItem liAux = new ListItem();
                        liAux.Text = liProxima.Text;
                        liAux.Value = liProxima.Value;

                        this.lstDestino.Items[posicaoProxima].Text = this.lstDestino.Items[i].Text;
                        this.lstDestino.Items[posicaoProxima].Value = this.lstDestino.Items[i].Value;
                        this.lstDestino.Items[i].Text = liAux.Text;
                        this.lstDestino.Items[i].Value = liAux.Value;

                        this.lstDestino.SelectedIndex = this.lstDestino.SelectedIndex + 1;
                    }

                    posicaoProxima = i;
                    liProxima = this.lstDestino.Items[i];
                }
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
    /// 
    /// </summary>
    private void ExcluirImagemResumo()
    {
        if (
            this.TituloBO.TituloImagemResumos != null
            && this.TituloBO.TituloImagemResumos.Any()
            )
        {
            try
            {
                foreach (TituloImagemResumo tituloImagemResumoBOTemp in this.TituloBO.TituloImagemResumos)
                {
                    if (tituloImagemResumoBOTemp.Arquivo != null && tituloImagemResumoBOTemp.Arquivo.ArquivoId > 0)
                    {
                        Arquivo arquivoBO = new ArquivoBLL().CarregarArquivo(new Arquivo { ArquivoId = tituloImagemResumoBOTemp.Arquivo.ArquivoId });

                        new ArquivoBLL().ExcluirArquivo(arquivoBO); // Remove da tabela Arquivo

                        string pathFile = string.Concat(Server.MapPath(GrupoA_Resource.baseUrlUpload), GrupoA_Resource.PastaImagensTitulo, arquivoBO.NomeArquivo);
                        FileInfo fileInfo = new FileInfo(pathFile);

                        if (fileInfo.Exists)
                        {
                            fileInfo.Delete(); // Apaga arquivo físico
                        }
                    }
                }
            }
            catch { }
            finally
            {
                foreach (TituloImagemResumo tituloImagemResumoBOTemp in this.TituloBO.TituloImagemResumos)
                {
                    new TituloImagemResumoBLL().Excluir(tituloImagemResumoBOTemp);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="arquivoBO"></param>
    private void ExcluirArquivoImagemComentarioEspecialista(TituloInformacaoComentarioEspecialista tituloInformacaoComentarioEspecialistaBO)
    {
        if (
            this.TituloBO.TituloInformacaoComentarioEspecialista != null
            && this.TituloBO.TituloInformacaoComentarioEspecialista.TituloInformacaoComentarioEspecialistaId > 0
            && this.TituloBO.TituloInformacaoComentarioEspecialista.ArquivoImagem != null
            && this.TituloBO.TituloInformacaoComentarioEspecialista.ArquivoImagem.ArquivoId > 0
            )
        {
            Arquivo arquivoBO = new ArquivoBLL().CarregarArquivo(tituloInformacaoComentarioEspecialistaBO.ArquivoImagem);

            if (arquivoBO != null)
            {
                arquivoBO = new ArquivoBLL().CarregarArquivo(arquivoBO);

                this.TituloBO.TituloInformacaoComentarioEspecialista.ArquivoImagem = null;
                new TituloInformacaoComentarioEspecialistaBLL().Atualizar(this.TituloBO.TituloInformacaoComentarioEspecialista);

                new ArquivoBLL().ExcluirArquivo(arquivoBO);

                string pathFile = string.Concat(Server.MapPath(GrupoA_Resource.baseUrlUpload), GrupoA_Resource.PastaComentarioEspecialista, arquivoBO.NomeArquivo);
                FileInfo info = new FileInfo(pathFile);

                if (info.Exists)
                {
                    info.Delete();
                }
            }
        }
    }

    #region [ Carregar Objetos ]

    /// <summary>
    /// Carrega todos os obejtos usados no formulario, menos imagens.
    /// </summary>
    private void CarregarObjetos()
    {
        //  Titulo
        this.TituloBO = new TituloBLL().Carregar(new Titulo { TituloId = this._tituloId });

        if (this.TituloBO != null && this.TituloBO.TituloId > 0)
        {
            this.CarregarObjetosInformacoesComplementares();

            this.CarregarObjetosComentarioEspecialista();

            this.CarregarObjetosMaterialComplementar();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void CarregarObjetosInformacoesComplementares()
    {
        // TituloImagemResumos
        this.TituloBO.TituloImagemResumos = new List<TituloImagemResumo>();
        this.TituloBO.TituloImagemResumos = new TituloImagemResumoBLL().Carregar(this.TituloBO);

        // TituloInformacaoResumo
        this.TituloBO.TituloInformacaoResumo = new TituloInformacaoResumo();
        this.TituloBO.TituloInformacaoResumo = new TituloInformacaoResumoBLL().Carregar(this.TituloBO);

        // TituloInformacaoSobreAutor
        this.TituloBO.TituloInformacaoSobreAutor = new TituloInformacaoSobreAutor();
        this.TituloBO.TituloInformacaoSobreAutor = new TituloInformacaoSobreAutorBLL().Carregar(this.TituloBO);

        // TituloInformacaoSumario
        this.TituloBO.TituloInformacaoSumario = new TituloInformacaoSumario();
        this.TituloBO.TituloInformacaoSumario = new TituloInformacaoSumarioBLL().Carregar(this.TituloBO);

        // TituloInformacaoMaterialDidatico
        this.TituloBO.TituloInformacaoMaterialDidatico = new TituloInformacaoMaterialDidatico();
        this.TituloBO.TituloInformacaoMaterialDidatico = new TituloInformacaoMaterialDidaticoBLL().Carregar(this.TituloBO);
    }

    /// <summary>
    /// 
    /// </summary>
    private void CarregarObjetosComentarioEspecialista()
    {
        // TituloInformacaoComentarioEspecialista
        this.TituloBO.TituloInformacaoComentarioEspecialista = new TituloInformacaoComentarioEspecialista();
        this.TituloBO.TituloInformacaoComentarioEspecialista = new TituloInformacaoComentarioEspecialistaBLL().Carregar(this.TituloBO);
    }

    /// <summary>
    /// 
    /// </summary>
    private void CarregarObjetosMaterialComplementar()
    {
        if (this.TituloBO != null && this.TituloBO.TituloId > 0)
        {
            this.TituloBO.TituloConteudoExtraUrl = new TituloConteudoExtraUrl();
            this.TituloBO.TituloConteudoExtraUrl = new TituloConteudoExtraUrlBLL().Carregar(this.TituloBO);

            this.TituloBO.TituloConteudoExtraMidia = new TituloConteudoExtraMidia();
            this.TituloBO.TituloConteudoExtraMidia = new TituloConteudoExtraMidiaBLL().Carregar(this.TituloBO);

            this.CarregarObjetosMaterialComplementarArquivos();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void CarregarObjetosMaterialComplementarArquivos()
    {
        this.TituloBO.TituloConteudoExtraArquivos = new List<TituloConteudoExtraArquivo>();
        this.TituloBO.TituloConteudoExtraArquivos = new TituloConteudoExtraArquivoBLL().CarregarTodosComDependenciaPorTitulo(this.TituloBO.TituloId);
    }

    #endregion

    #region [ Carregar Form ]

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    protected void CarregarForm()
    {
        if (this.TituloBO != null && this.TituloBO.TituloId > 0)
        {
            this.CarregarTitulo();
            this.CarregarTituloComentarioEspecialista();
            this.CarregarTituloMaterialComplementar();
            this.CarregarAutorListaDestino();
            this.CarregarTituloInformacoesComplementares();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void CarregarTitulo()
    {
        this.txtNomeTitulo.Text = this.TituloBO.NomeTitulo;
        this.txtSubtituloTitulo.Text = this.TituloBO.SubtituloLivro;
        this.txtNroPaginasTitulo.Text = this.TituloBO.NumeroPaginas != null ? this.TituloBO.NumeroPaginas.ToString() : "";
        this.txtEdicaoTitulo.Text = String.Concat(this.TituloBO.Edicao != null ? this.TituloBO.Edicao.Value.ToString() : "1", "ª Edição");
        this.txtDataLancamentoTitulo.Text = this.TituloBO.DataLancamento != null ? this.TituloBO.DataLancamento.Value.ToString("dd/MM/yyyy") : "";
        this.txtDataPublicacaoTitulo.Text = this.TituloBO.DataPublicacao != null ? this.TituloBO.DataPublicacao.Value.ToString("dd/MM/yyyy") : "";
        this.txtFormatoTitulo.Text = this.TituloBO.Formato;
    }

    /// <summary>
    /// 
    /// </summary>
    private void CarregarTituloInformacoesComplementares()
    {
        if (this.TituloBO.TituloInformacaoResumo != null && this.TituloBO.TituloInformacaoResumo.TituloInformacaoResumoId > 0)
        {
            this.htmlResumoInfoComplementares.Text = this.TituloBO.TituloInformacaoResumo.TextoResumo;
        }

        if (this.TituloBO.TituloInformacaoSobreAutor != null && this.TituloBO.TituloInformacaoSobreAutor.TituloInformacaoSobreAutorId > 0)
        {
            this.htmlInformacaoSobreAutor.Text = this.TituloBO.TituloInformacaoSobreAutor.TextoAutor;
        }

        if (this.TituloBO.TituloInformacaoSumario != null && this.TituloBO.TituloInformacaoSumario.TituloInformacaoSumarioId > 0)
        {
            this.htmlSumarioInfoComplementares.Text = this.TituloBO.TituloInformacaoSumario.TextoSumario;
        }

        if (this.TituloBO.TituloInformacaoMaterialDidatico != null && this.TituloBO.TituloInformacaoMaterialDidatico.TituloInformacaoMaterialDidaticoId > 0)
        {
            this.htmlMaterialInfoComplementares.Text = this.TituloBO.TituloInformacaoMaterialDidatico.TextoMaterial;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void CarregarTituloComentarioEspecialista()
    {
        if (this.TituloBO.TituloInformacaoComentarioEspecialista != null && this.TituloBO.TituloInformacaoComentarioEspecialista.TituloInformacaoComentarioEspecialistaId > 0)
        {
            this.txtNomeEspecialista.Text = this.TituloBO.TituloInformacaoComentarioEspecialista.NomeEspecialista;
            this.txtEspecialidade.Text = this.TituloBO.TituloInformacaoComentarioEspecialista.Especialidade;
            this.txtUrlComentarioEspecialista.Text = this.TituloBO.TituloInformacaoComentarioEspecialista.UrlMidia;
            this.txtResumoComentario.Text = this.TituloBO.TituloInformacaoComentarioEspecialista.ResumoComentario;
            this.htmlComentarioEspecialista.Text = this.TituloBO.TituloInformacaoComentarioEspecialista.TextoComentario;

            this.chkDestaqueComentarioEspecialista.Checked = this.TituloBO.TituloInformacaoComentarioEspecialista.DestaqueAreaConhecimento;

            //this.CarregarCblCategorias();

            IEnumerable<Categoria> categoriaBOIEnum = new TituloBLL().CarregarCategoriasComentarioEspecialista(this.TituloBO.TituloInformacaoComentarioEspecialista.TituloInformacaoComentarioEspecialistaId);

            //foreach (Categoria categoriaBOTemp in categoriaBOIEnum)
            //{
            //    for (int i = 0; i < cblCategorias.Items.Count; i++)
            //    {
            //        if (cblCategorias.Items[i].Value.Equals(categoriaBOTemp.CategoriaId.ToString()))
            //        {
            //            cblCategorias.Items[i].Selected = true;
            //        }
            //    }
            //}
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void CarregarTituloMaterialComplementar()
    {
        if (this.TituloBO.TituloConteudoExtraUrl != null && this.TituloBO.TituloConteudoExtraUrl.TituloConteudoExtraUrlId > 0)
        {
            this.txtUrlConteudoExtra.Text = this.TituloBO.TituloConteudoExtraUrl.Url;
        }

        if (this.TituloBO.TituloConteudoExtraMidia != null && this.TituloBO.TituloConteudoExtraMidia.TituloConteudoExtraMidiaId > 0)
        {
            this.htmlInformacaoConteudoExtra.Text = this.TituloBO.TituloConteudoExtraMidia.Informacao;
        }

        this.CarregarTituloMaterialComplementarArquivo();
    }

    /// <summary>
    /// 
    /// </summary>
    private void CarregarTituloMaterialComplementarArquivo()
    {
        if (this.TituloBO.TituloConteudoExtraArquivos != null && this.TituloBO.TituloConteudoExtraArquivos.Any())
        {
            this.rptTituloConteudoExtraArquivo.Visible = true;
            this.rptTituloConteudoExtraArquivo.DataSource = this.TituloBO.TituloConteudoExtraArquivos;
            this.rptTituloConteudoExtraArquivo.DataBind();
        }
        else
        {
            this.rptTituloConteudoExtraArquivo.Visible = false;
        }
    }

    /// <summary>
    /// Popula a lista de checklists conforme as categorias base (áreas de conhecimento)
    /// </summary>
    //protected void CarregarCblCategorias()
    //{
    //    cblCategorias.DataSource = new EventoBLL().CarregarTodasCategoriasBase();
    //    cblCategorias.DataTextField = "nomeCategoria";
    //    cblCategorias.DataValueField = "categoriaId";
    //    cblCategorias.DataBind();
    //}

    /// <summary>
    /// 
    /// </summary>
    protected void CarregarAutorListaDestino()
    {
        if (this.TituloBO != null && this.TituloBO.TituloId > 0)
        {
            List<Autor> autorBOList = new TituloBLL().CarregarAutores(this.TituloBO);

            if (this.AutoresGrid == null)
            {
                this.AutoresGrid = new List<AutorGrid>();
            }

            if (autorBOList.Count > 0)
            {
                foreach (Autor autorBOTemp in autorBOList)
                {
                    AutorGrid autorGridBO = new AutorGrid();
                    autorGridBO.AutorId = autorBOTemp.AutorId;
                    autorGridBO.NomeAutor = autorBOTemp.NomeAutor;

                    if (AutoresGrid.Find(p => p.AutorId == autorGridBO.AutorId) == null)
                    {
                        this.AutoresGrid.Add(autorGridBO);
                    }
                }
            }

            this.lstOrigem.Items.Clear();

            foreach (Autor autorBOTemp in autorBOList)
            {
                this.lstDestino.Items.Add(new ListItem(autorBOTemp.NomeAutor, autorBOTemp.AutorId.ToString()));
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="autorBOList"></param>
    protected void CarregaAutorListaOrigem(List<Autor> autorBOList)
    {
        this.lstOrigem.Items.Clear();

        string strId = ""; // Controle de IDs já inseridos

        foreach (ListItem listItemTemp in this.lstDestino.Items)
        {
            if (strId.Length == 0)
            {
                strId = String.Concat("#", listItemTemp.Value, "#");
            }
            else
            {
                strId = String.Concat(strId, ", ", "#", listItemTemp.Value, "#");
            }
        }

        foreach (Autor autorBOTemp in autorBOList)
        {
            if (!strId.Contains(String.Concat("#", autorBOTemp.AutorId.ToString(), "#")))
            {
                lstOrigem.Items.Add(new ListItem(autorBOTemp.NomeAutor, autorBOTemp.AutorId.ToString()));
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
                this.SalvarTitulo();
                this.SalvarTituloInformacoesComplementares();
                this.SalvarTituloComentarioEspecialista();
                this.SalvarTituloMaterialComplementar();
                this.SalvarTituloAutor();

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
    private void SalvarTitulo()
    {
        this.TituloBO.NomeTitulo = this.txtNomeTitulo.Text;
        this.TituloBO.SubtituloLivro = this.txtSubtituloTitulo.Text;

        new TituloBLL().Atualizar(this.TituloBO);
    }

    /// <summary>
    /// 
    /// </summary>
    protected void SalvarTituloComentarioEspecialista()
    {
        if (this.TituloBO.TituloInformacaoComentarioEspecialista == null)
        {
            this.TituloBO.TituloInformacaoComentarioEspecialista = new TituloInformacaoComentarioEspecialista();
        }

        this.TituloBO.TituloInformacaoComentarioEspecialista.NomeEspecialista = this.txtNomeEspecialista.Text;
        this.TituloBO.TituloInformacaoComentarioEspecialista.ResumoComentario = this.txtResumoComentario.Text;
        this.TituloBO.TituloInformacaoComentarioEspecialista.Especialidade = this.txtEspecialidade.Text;
        this.TituloBO.TituloInformacaoComentarioEspecialista.TextoComentario = this.htmlComentarioEspecialista.Text;
        this.TituloBO.TituloInformacaoComentarioEspecialista.UrlMidia = this.txtUrlComentarioEspecialista.Text;
        this.TituloBO.TituloInformacaoComentarioEspecialista.DestaqueAreaConhecimento = this.chkDestaqueComentarioEspecialista.Checked;

        int comentarioFormatoId = 1; // Texto

        if (!String.IsNullOrEmpty(this.TituloBO.TituloInformacaoComentarioEspecialista.UrlMidia))
        {
            comentarioFormatoId = 3; // Video
        }
        else if (this.TituloBO.TituloInformacaoComentarioEspecialista.ArquivoAudio != null)
        {
            comentarioFormatoId = 2; // Audio
        }

        this.TituloBO.TituloInformacaoComentarioEspecialista.ComentarioFormato = new ComentarioFormato() { ComentarioFormatoId = comentarioFormatoId };

        // Categorias de Comentário de Especialista
        this.TituloBO.TituloInformacaoComentarioEspecialista.TituloInformacaoComentarioEspecialistaCategorias = new List<TituloInformacaoComentarioEspecialistaCategoria>();

        TituloInformacaoComentarioEspecialistaCategoria tituloInformacaoComentarioEspecialistaCategoriaBO = new TituloInformacaoComentarioEspecialistaCategoria();
        tituloInformacaoComentarioEspecialistaCategoriaBO.Categoria = new Categoria() { CategoriaId = new TituloBLL().CarregarCategoriaPorTituloId(this.TituloBO.TituloId) };
        //tituloInformacaoComentarioEspecialistaCategoriaBO.Categoria = new CategoriaBLL().CarregarAreaPorCategoria(tituloInformacaoComentarioEspecialistaCategoriaBO.Categoria);
        tituloInformacaoComentarioEspecialistaCategoriaBO.TituloInformacaoComentarioEspecialista = new TituloInformacaoComentarioEspecialista();
        tituloInformacaoComentarioEspecialistaCategoriaBO.TituloInformacaoComentarioEspecialista = this.TituloBO.TituloInformacaoComentarioEspecialista;

        this.TituloBO.TituloInformacaoComentarioEspecialista.TituloInformacaoComentarioEspecialistaCategorias.Add(tituloInformacaoComentarioEspecialistaCategoriaBO);
        
        if (this.TituloBO.TituloInformacaoComentarioEspecialista.TituloInformacaoComentarioEspecialistaId > 0) // Atualizar
        {
            new TituloInformacaoComentarioEspecialistaBLL().Atualizar(this.TituloBO.TituloInformacaoComentarioEspecialista);
        }
        else // Inserir
        {
            this.TituloBO.TituloInformacaoComentarioEspecialista.TituloInformacaoComentarioEspecialistaId = this.TituloBO.TituloId;

            new TituloInformacaoComentarioEspecialistaBLL().Inserir(this.TituloBO.TituloInformacaoComentarioEspecialista);
        }

        //  Atualizar Categorias de Comentário de Especialista
        if (this.TituloBO.TituloInformacaoComentarioEspecialista != null && this.TituloBO.TituloInformacaoComentarioEspecialista.TituloInformacaoComentarioEspecialistaId > 0)
        {
            new TituloInformacaoComentarioEspecialistaCategoriaBLL().ExcluirTodosPorComentarioEspecialista(this.TituloBO.TituloInformacaoComentarioEspecialista);

            if (
                this.TituloBO.TituloInformacaoComentarioEspecialista.TituloInformacaoComentarioEspecialistaCategorias != null
                && this.TituloBO.TituloInformacaoComentarioEspecialista.TituloInformacaoComentarioEspecialistaCategorias.Any()
                )
            {
                foreach (TituloInformacaoComentarioEspecialistaCategoria TituloInformacaoComentarioEspecialistaCategoriaBOTemp in this.TituloBO.TituloInformacaoComentarioEspecialista.TituloInformacaoComentarioEspecialistaCategorias)
                {
                    new TituloInformacaoComentarioEspecialistaCategoriaBLL().Inserir(TituloInformacaoComentarioEspecialistaCategoriaBOTemp);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void SalvarTituloMaterialComplementar()
    {
        if (this.TituloBO.TituloConteudoExtraUrl == null)
        {
            this.TituloBO.TituloConteudoExtraUrl = new TituloConteudoExtraUrl();
        }

        this.TituloBO.TituloConteudoExtraUrl.Url = this.txtUrlConteudoExtra.Text;

        if (this.TituloBO.TituloConteudoExtraUrl.TituloConteudoExtraUrlId > 0)
        {
            new TituloConteudoExtraUrlBLL().Atualizar(this.TituloBO.TituloConteudoExtraUrl);
        }
        else
        {
            this.TituloBO.TituloConteudoExtraUrl.TituloConteudoExtraUrlId = this.TituloBO.TituloId;

            new TituloConteudoExtraUrlBLL().Inserir(this.TituloBO.TituloConteudoExtraUrl);
        }

        if (this.TituloBO.TituloConteudoExtraMidia == null)
        {
            this.TituloBO.TituloConteudoExtraMidia = new TituloConteudoExtraMidia();
        }

        this.TituloBO.TituloConteudoExtraMidia.Informacao = this.htmlInformacaoConteudoExtra.Text;

        if (this.TituloBO.TituloConteudoExtraMidia.TituloConteudoExtraMidiaId > 0)
        {
            new TituloConteudoExtraMidiaBLL().Atualizar(this.TituloBO.TituloConteudoExtraMidia);
        }
        else
        {
            this.TituloBO.TituloConteudoExtraMidia.TituloConteudoExtraMidiaId = this.TituloBO.TituloId;

            new TituloConteudoExtraMidiaBLL().Inserir(this.TituloBO.TituloConteudoExtraMidia);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void SalvarTituloAutor()
    {
        if (this.TituloBO != null && this.TituloBO.TituloId > 0)
        {
            List<TituloAutor> tituloAutoBOList = new List<TituloAutor>();

            if (this.lstDestino.Items.Count > 0)
            {
                Int32 ordem = 0;

                foreach (ListItem listItemTemp in this.lstDestino.Items)
                {
                    ordem++;

                    TituloAutor tituloAutor = new TituloAutor();
                    tituloAutor.Autor = new Autor();
                    tituloAutor.Autor.AutorId = Convert.ToInt32(listItemTemp.Value);
                    tituloAutor.Titulo = this.TituloBO;
                    tituloAutor.Ordem = ordem;

                    tituloAutoBOList.Add(tituloAutor);
                }

                new TituloBLL().AtualizarTituloAutor(tituloAutoBOList, this.TituloBO);
            }
            else
            {
                new TituloBLL().ExcluirTodosTituloAutor(this.TituloBO);
            }
        }

        this.lstOrigem.Items.Clear();
        this.lstDestino.Items.Clear();
    }

    /// <summary>
    /// 
    /// </summary>
    private void SalvarTituloInformacoesComplementares()
    {
        if (this.TituloBO.TituloInformacaoResumo != null && this.TituloBO.TituloInformacaoResumo.TituloInformacaoResumoId > 0)
        {
            this.TituloBO.TituloInformacaoResumo.TextoResumo = this.htmlResumoInfoComplementares.Text;
            new TituloInformacaoBLL().Atualizar(this.TituloBO.TituloInformacaoResumo);
        }
        else
        {
            this.TituloBO.TituloInformacaoResumo = new TituloInformacaoResumo();
            this.TituloBO.TituloInformacaoResumo.TituloInformacaoResumoId = this.TituloBO.TituloId;
            this.TituloBO.TituloInformacaoResumo.TextoResumo = this.htmlResumoInfoComplementares.Text;

            new TituloInformacaoBLL().Inserir(this.TituloBO.TituloInformacaoResumo);
        }

        if (this.TituloBO.TituloInformacaoSobreAutor != null && this.TituloBO.TituloInformacaoSobreAutor.TituloInformacaoSobreAutorId > 0)
        {
            this.TituloBO.TituloInformacaoSobreAutor.TextoAutor = this.htmlInformacaoSobreAutor.Text;
            new TituloInformacaoBLL().Atualizar(this.TituloBO.TituloInformacaoSobreAutor);
        }
        else
        {
            this.TituloBO.TituloInformacaoSobreAutor = new TituloInformacaoSobreAutor();
            this.TituloBO.TituloInformacaoSobreAutor.TituloInformacaoSobreAutorId = this.TituloBO.TituloId;
            this.TituloBO.TituloInformacaoSobreAutor.TextoAutor = this.htmlInformacaoSobreAutor.Text;

            new TituloInformacaoBLL().Inserir(this.TituloBO.TituloInformacaoSobreAutor);
        }

        if (this.TituloBO.TituloInformacaoSumario != null && this.TituloBO.TituloInformacaoSumario.TituloInformacaoSumarioId > 0)
        {
            this.TituloBO.TituloInformacaoSumario.TextoSumario = this.htmlSumarioInfoComplementares.Text;
            new TituloInformacaoBLL().Atualizar(this.TituloBO.TituloInformacaoSumario);
        }
        else
        {
            this.TituloBO.TituloInformacaoSumario = new TituloInformacaoSumario();
            this.TituloBO.TituloInformacaoSumario.TituloInformacaoSumarioId = this.TituloBO.TituloId;
            this.TituloBO.TituloInformacaoSumario.TextoSumario = this.htmlSumarioInfoComplementares.Text;

            new TituloInformacaoBLL().Inserir(this.TituloBO.TituloInformacaoSumario);
        }

        if (this.TituloBO.TituloInformacaoMaterialDidatico != null && this.TituloBO.TituloInformacaoMaterialDidatico.TituloInformacaoMaterialDidaticoId > 0)
        {
            this.TituloBO.TituloInformacaoMaterialDidatico.TextoMaterial = this.htmlMaterialInfoComplementares.Text;
            new TituloInformacaoBLL().Atualizar(this.TituloBO.TituloInformacaoMaterialDidatico);
        }
        else
        {
            this.TituloBO.TituloInformacaoMaterialDidatico = new TituloInformacaoMaterialDidatico();
            this.TituloBO.TituloInformacaoMaterialDidatico.TituloInformacaoMaterialDidaticoId = this.TituloBO.TituloId;
            this.TituloBO.TituloInformacaoMaterialDidatico.TextoMaterial = this.htmlMaterialInfoComplementares.Text;

            new TituloInformacaoBLL().Inserir(this.TituloBO.TituloInformacaoMaterialDidatico);
        }
    }

    #endregion

    #endregion
}