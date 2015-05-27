using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;

public partial class content_module_tituloConteudoExtra_tituloConteudoExtra : SmartUserControl
{
    #region [ Propriedades ]

    private Titulo titulo;

    public Int32 TituloConteudoExtraArquivoId
    {
        get
        {
            return String.IsNullOrEmpty(hddTituloConteudoExtraArquivoId.Value) ? 0 : Convert.ToInt32(hddTituloConteudoExtraArquivoId.Value);
        }
        set
        {
            hddTituloConteudoExtraArquivoId.Value = value.ToString();
        }
    }

    public Int32 TituloId
    {
        get
        {
            return String.IsNullOrEmpty(hddTituloId.Value) ? 0 : Convert.ToInt32(hddTituloId.Value);
        }
        set
        {
            hddTituloId.Value = value.ToString();
        }
    }

    #endregion

    #region [ Eventos ]

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.CarregaPagina();
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSalvar_Click(object sender, ImageClickEventArgs e)
    {
        this.SalvarArquivo();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        this.EditarArquivo(sender);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExcluirArquivo_Click(object sender, ImageClickEventArgs e)
    {
        this.ExcluirArquivo(sender);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptArquivos_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        this.BindArquivos(e);
    }

    #endregion

    #region [ Metodos ]
    /// <summary>
    /// Carrega as informacoes da pagina, titulo, html, arquivos
    /// </summary>
    private void CarregaPagina()
    {
        // Carrega as informacoes do titulo caso 
        // o registro ja exista.
        if (this.IdRegistro > 0)
        {
            this.CarregaInformacoesTitulo();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void CarregaInformacoesTitulo()
    {
        TituloBLL tituloBLL = new TituloBLL();

        this.titulo = tituloBLL.CarregarComDependencias(new Titulo() { TituloId = (int)this.IdRegistro });

        TituloId = this.titulo.TituloId;

        this.txtTituloLivro.Text = titulo.NomeTitulo;
        if (titulo.DataLancamento != null)
        {
            this.lblDtLancamento.Text = titulo.DataLancamento.Value.ToString("dd/MM/yyyy");
        }
        this.lblSubtitulo.Text = titulo.SubtituloLivro;

        this.titulo = tituloBLL.CarregarConteudoExtra(titulo);

        this.CarregarArquivos();

        this.CarregaInformacoesMidia(titulo.TituloConteudoExtraMidia);

        this.CarregaInformacoesURL(titulo.TituloConteudoExtraUrl);
    }

    /// <summary>
    /// Carrega a URL relacionada ao titulo
    /// </summary>
    /// <param name="tituloConteudoExtraMidia"></param>
    private void CarregaInformacoesMidia(TituloConteudoExtraMidia tituloConteudoExtraMidia)
    {
        if (tituloConteudoExtraMidia != null
            && tituloConteudoExtraMidia.TituloConteudoExtraMidiaId > 0)
        {
            this.htmlTextoMidia.Text = tituloConteudoExtraMidia.Informacao;
        }

    }

    /// <summary>
    /// Carrega as informacoes de Midia
    /// </summary>
    /// <param name="tituloConteudoExtraUrl"></param>
    private void CarregaInformacoesURL(TituloConteudoExtraUrl tituloConteudoExtraUrl)
    {
        if (tituloConteudoExtraUrl != null
            && tituloConteudoExtraUrl.TituloConteudoExtraUrlId > 0)
        {
            this.txtUrlConteudoExtra.Text = tituloConteudoExtraUrl.Url;
        }
    }

    /// <summary>
    /// Salva as informções da Tela
    /// </summary>
    private void Salvar()
    {

        this.titulo = new TituloBLL().CarregarConteudoExtra(new Titulo { TituloId = (int)this.IdRegistro });

        // Salvar Informacoes da URL 
        if (!(this.titulo.TituloConteudoExtraUrl != null
            && this.titulo.TituloConteudoExtraUrl.TituloConteudoExtraUrlId > 0))
        {
            this.titulo.TituloConteudoExtraUrl = new TituloConteudoExtraUrl() { Titulo = this.titulo };
        }

        this.titulo.TituloConteudoExtraUrl.Url = this.txtUrlConteudoExtra.Text;

        // Salvar Informacoes da Midia
        if (!(this.titulo.TituloConteudoExtraMidia != null
            && this.titulo.TituloConteudoExtraMidia.TituloConteudoExtraMidiaId > 0))
        {
            this.titulo.TituloConteudoExtraMidia = new TituloConteudoExtraMidia() { Titulo = this.titulo };
        }

        this.titulo.TituloConteudoExtraMidia.Informacao = this.htmlTextoMidia.Text;

        TituloBLL tituloBLL = new TituloBLL();

        tituloBLL.AtualizarConteudoExtra(titulo);

        Util.ShowUpdateMessage();


    }

    /// <summary>
    /// 
    /// </summary>
    public void CarregarArquivos()
    {
        var arquivosConteudoExtra = new TituloConteudoExtraArquivoBLL().CarregarTodosComDependenciaPorTitulo(TituloId);
        if (arquivosConteudoExtra != null && arquivosConteudoExtra.Count > 0)
        {
            rptArquivos.Visible = true;
            rptArquivos.DataSource = arquivosConteudoExtra;
            rptArquivos.DataBind();
        }
        else
        {
            rptArquivos.Visible = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void SalvarArquivo()
    {
        TituloConteudoExtraArquivo tituloConteudoExtraArquivo = null;

        if (TituloConteudoExtraArquivoId > 0)
        {
            tituloConteudoExtraArquivo = new TituloConteudoExtraArquivoBLL().CarregarComDependencia(new TituloConteudoExtraArquivo(TituloConteudoExtraArquivoId));
        }
        else
        {
            tituloConteudoExtraArquivo = new TituloConteudoExtraArquivo();
            tituloConteudoExtraArquivo.Arquivo = new Arquivo();
        }

        tituloConteudoExtraArquivo.Arquivo.NomeArquivoOriginal = txtNomeArquivo.Text;
        tituloConteudoExtraArquivo.Arquivo.NomeArquivo = txtArquivo.Text;
        tituloConteudoExtraArquivo.NomeConteudo = txtNomeArquivo.Text;
        tituloConteudoExtraArquivo.SomenteLogado = cbSomenteLogado.Checked;
        tituloConteudoExtraArquivo.RestritoProfessor = cbRestritoProfessor.Checked;
        tituloConteudoExtraArquivo.Ativo = cbAtivo.Checked;
        tituloConteudoExtraArquivo.Titulo = new Titulo();
        tituloConteudoExtraArquivo.Titulo.TituloId = TituloId;

        if (TituloConteudoExtraArquivoId > 0)
        {
            new TituloConteudoExtraArquivoBLL().Atualizar(tituloConteudoExtraArquivo);
            TituloConteudoExtraArquivoId = 0;
            txtNomeArquivo.Text = String.Empty;
            txtArquivo.Text = String.Empty;
            cbSomenteLogado.Checked = false;
            cbRestritoProfessor.Checked = false;
            cbAtivo.Checked = false;
            this.CarregarArquivos();

            Util.ShowMessage("Arquivo alterado com sucesso.", Ag2.Manager.Enumerator.typeMessage.Sucesso);
        }
        else
        {
            new TituloConteudoExtraArquivoBLL().Inserir(tituloConteudoExtraArquivo);
            txtNomeArquivo.Text = String.Empty;
            txtArquivo.Text = String.Empty;
            cbSomenteLogado.Checked = false;
            cbRestritoProfessor.Checked = false;
            cbAtivo.Checked = false;
            this.CarregarArquivos();

            Util.ShowMessage("Arquivo incluído com sucesso.", Ag2.Manager.Enumerator.typeMessage.Sucesso);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void EditarArquivo(object sender)
    {
        TituloConteudoExtraArquivoId = Convert.ToInt32(((ImageButton)sender).CommandArgument.ToString());

        TituloConteudoExtraArquivo tituloConteudoExtraArquivo = new TituloConteudoExtraArquivoBLL().CarregarComDependencia(new TituloConteudoExtraArquivo(TituloConteudoExtraArquivoId));

        if (tituloConteudoExtraArquivo != null)
        {
            txtNomeArquivo.Text = tituloConteudoExtraArquivo.NomeConteudo;
            txtArquivo.Text = tituloConteudoExtraArquivo.Arquivo.NomeArquivo;
            cbSomenteLogado.Checked = tituloConteudoExtraArquivo.SomenteLogado;
            cbRestritoProfessor.Checked = tituloConteudoExtraArquivo.RestritoProfessor;
            cbAtivo.Checked = tituloConteudoExtraArquivo.Ativo;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void ExcluirArquivo(object sender)
    {
        TituloConteudoExtraArquivoId = Convert.ToInt32(((ImageButton)sender).CommandArgument.ToString());

        new TituloConteudoExtraArquivoBLL().Excluir(new TituloConteudoExtraArquivo(TituloConteudoExtraArquivoId));
        TituloConteudoExtraArquivoId = 0;
        this.CarregarArquivos();

        Util.ShowMessage("Arquivo excluído com sucesso.", Ag2.Manager.Enumerator.typeMessage.Sucesso);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    private void BindArquivos(RepeaterItemEventArgs e)
    {
        if (e.Item != null && e.Item.DataItem != null)
        {
            TituloConteudoExtraArquivo tituloConteudoExtraArquivo = (TituloConteudoExtraArquivo)e.Item.DataItem;
            Literal lNome = (Literal)e.Item.FindControl("lNome");
            Literal lSomenteLogado = (Literal)e.Item.FindControl("lSomenteLogado");
            Literal lRestritoProfessor = (Literal)e.Item.FindControl("lRestritoProfessor");
            Literal lAtivo = (Literal)e.Item.FindControl("lAtivo");
            Literal lDataHoraCadastro = (Literal)e.Item.FindControl("lDataHoraCadastro");
            ImageButton btnEditar = (ImageButton)e.Item.FindControl("btnEditar");
            ImageButton btnExcluirArquivo = (ImageButton)e.Item.FindControl("btnExcluirArquivo");

            lNome.Text = tituloConteudoExtraArquivo.NomeConteudo;
            lSomenteLogado.Text = tituloConteudoExtraArquivo.SomenteLogado ? "Sim" : "Não";
            lRestritoProfessor.Text = tituloConteudoExtraArquivo.RestritoProfessor ? "Sim" : "Não";
            lAtivo.Text = tituloConteudoExtraArquivo.Ativo ? "Sim" : "Não";
            lDataHoraCadastro.Text = String.Concat(tituloConteudoExtraArquivo.DataCadastro.ToShortDateString(), " ", tituloConteudoExtraArquivo.DataCadastro.ToShortTimeString());

            btnEditar.CommandArgument = tituloConteudoExtraArquivo.TituloConteudoExtraArquivoId.ToString();
            btnExcluirArquivo.CommandArgument = tituloConteudoExtraArquivo.TituloConteudoExtraArquivoId.ToString();
        }
    }

    #endregion
}
