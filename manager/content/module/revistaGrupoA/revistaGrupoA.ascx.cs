using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using GrupoA.GlobalResources;

public partial class content_module_revistaGrupoA_revistaGrupoA : SmartUserControl
{
    #region [ Properties ]

    /// <summary>
    /// 
    /// </summary>
    private RevistaGrupoABLL _revistaGrupoABLL;

    /// <summary>
    /// 
    /// </summary>
    RevistaGrupoABLL revistaBLL
    {
        get
        {
            if (_revistaGrupoABLL == null)
                _revistaGrupoABLL = new RevistaGrupoABLL();
            return _revistaGrupoABLL;
        }
    }

    #endregion

    #region [ Page Events ]

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        this.upArquivoImagem.BindList += new EventHandler<ArquivoEventArgs>(upArquivoImagem_BindList);
        this.upArquivoImagem.DeleteItem += new EventHandler<ArquivoEventArgs>(upArquivoImagem_DeleteItem);
        this.upArquivoImagem.RegistroId = Convert.ToInt32(IdRegistro);

        if (Util.GetRequestId() > 0)
        {
            hddRevistaId.Value = Util.GetRequestId().ToString();
            if (!IsPostBack)
            {
                //montaChecklistCategoria();
                loadForm();
            }
        }
        else
            //if (!IsPostBack)
            //    montaChecklistCategoria();
            msg.Text = "";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void upArquivoImagem_BindList(object sender, ArquivoEventArgs e)
    {
        this.upArquivoImagem.ArquivoId = e.ArquivoId;
        this.upArquivoImagem.RegistroId = Convert.ToInt32(IdRegistro);

        //cria o relacionamento
        if (e.ArquivoId > 0)
        {
            RevistaGrupoABLL revistaGrupoABLL = new RevistaGrupoABLL();
            RevistaGrupoAEdicao revistaGrupoAEdicao = revistaGrupoABLL.CarregarRevistaGrupoAEdicao(new RevistaGrupoAEdicao() { RevistaGrupoAEdicaoId = Convert.ToInt32(IdRegistro) });

            ArquivoBLL arquivoBLL = new ArquivoBLL();
            revistaGrupoAEdicao.ArquivoGrande = arquivoBLL.CarregarArquivo(new Arquivo() { ArquivoId = e.ArquivoId });

            revistaGrupoAEdicao.ArquivoPequena = this.criaThumb(revistaGrupoAEdicao.ArquivoGrande);

            revistaGrupoABLL.AtualizarRevistaGrupoAEdicao(revistaGrupoAEdicao);
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void upArquivoImagem_DeleteItem(object sender, ArquivoEventArgs e)
    {
        RevistaGrupoABLL revistaGrupoABLL = new RevistaGrupoABLL();
        RevistaGrupoAEdicao revistaGrupoAEdicao = revistaGrupoABLL.CarregarRevistaGrupoAEdicaoComArquivos(new RevistaGrupoAEdicao() { RevistaGrupoAEdicaoId = Convert.ToInt32(IdRegistro) });
        if (revistaGrupoAEdicao.ArquivoGrande != null)
        {
            Arquivo arquivo = revistaGrupoAEdicao.ArquivoGrande;
            //remove relacionamento
            revistaGrupoAEdicao.ArquivoGrande = null;
            revistaGrupoABLL.AtualizarRevistaGrupoAEdicao(revistaGrupoAEdicao);

            //remove da tabela Arquivo
            new ArquivoBLL().ExcluirArquivo(arquivo);

            //apaga arquivo físico
            string pathFile = string.Concat(Server.MapPath(GrupoA_Resource.baseUrlUpload), GrupoA_Resource.PastaImagensClipping, arquivo.NomeArquivo);
            FileInfo info = new FileInfo(pathFile);
            if (info.Exists)
                info.Delete();

            //atualiza ListFile
            this.upArquivoImagem.RegistroId = Convert.ToInt32(IdRegistro);
            this.upArquivoImagem.DataBind();
        }
        if (revistaGrupoAEdicao.ArquivoPequena != null)
        {
            Arquivo arquivo = revistaGrupoAEdicao.ArquivoPequena;
            //remove relacionamento
            revistaGrupoAEdicao.ArquivoPequena = null;
            revistaGrupoABLL.AtualizarRevistaGrupoAEdicao(revistaGrupoAEdicao);

            //remove da tabela Arquivo
            new ArquivoBLL().ExcluirArquivo(arquivo);

            //apaga arquivo físico
            string pathFile = string.Concat(Server.MapPath(GrupoA_Resource.baseUrlUpload), GrupoA_Resource.PastaImagensClipping, arquivo.NomeArquivo);
            FileInfo info = new FileInfo(pathFile);
            if (info.Exists)
                info.Delete();
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

    #region [ Validation ]

    /// <summary>
    /// Valida se já existe uma revista com o mesmo ano, mês e número de edição.
    /// </summary>
    /// <param name="source">Padrão</param>
    /// <param name="args">Padrão</param>
    protected void cvValidarExists_ServerValidate(object source, ServerValidateEventArgs args)
    {
        this.ValidarExists(args);
    }

    #endregion

    #endregion

    #region [ Methods ]

    /// <summary>
    /// 
    /// </summary>
    /// <param name="arquivo"></param>
    /// <returns></returns>
    private Arquivo criaThumb(Arquivo arquivo)
    {

        string saveToFolder = Page.ResolveClientUrl(string.Concat(GrupoA_Resource.baseUrlUpload, GrupoA_Resource.PastaImagensRevista));
        string newFileName = string.Empty;
        string oldFileName = string.Empty;
        string sPath = string.Empty;
        long tamanho = 0;

        sPath = Server.MapPath(saveToFolder);

        oldFileName = arquivo.NomeArquivo;
        newFileName = string.Concat(Path.GetFileNameWithoutExtension(oldFileName), "_thumb", Path.GetExtension(oldFileName)).Replace(" ", "_");

        File.Copy(string.Concat(sPath, "\\", oldFileName), string.Concat(sPath, "\\", newFileName), false);

        try { new ImagemThumb.ImagemThumb().GerarProporcao(string.Concat(sPath, "\\", newFileName), 165, 220, System.Drawing.Color.Transparent); }
        catch (Exception ex) { }

        //salvar arquivo
        FileInfo info = new FileInfo(string.Concat(sPath, "\\", newFileName));
        if (info != null)
        {
            tamanho = info.Length / 1024;
        }

        Arquivo arquivoThumb = new Arquivo();
        arquivoThumb.TamanhoArquivo = (int)tamanho;
        arquivoThumb.DataHoraUpload = DateTime.Now;
        arquivoThumb.NomeArquivo = newFileName;
        arquivoThumb.NomeArquivoOriginal = oldFileName;
        new ArquivoBLL().InserirNovoArquivo(arquivoThumb);

        return arquivoThumb;
    }

    /// <summary>
    /// Salva ou atualiza as informações após validar a página atual.
    /// A informação entre salvar ou atualizar será feita com base no 
    /// campo oculto "hddRevistaId".
    /// </summary>
    public void saveOrUpdate()
    {

        if (Page.IsValid)
        {
            if (hddRevistaId.Value == "0")
            {
                RevistaGrupoAEdicao revista = new RevistaGrupoAEdicao();
                revista = carregarCampos(revista);

                revistaBLL.InserirRevistaGrupoAEdicao(revista);
                hddRevistaId.Value = revista.RevistaGrupoAEdicaoId.ToString();

                Ag2.Security.SecureQueryString sqGrid = new Ag2.Security.SecureQueryString();
                sqGrid["md"] = Util.GetQueryString("md");
                sqGrid["id"] = revista.RevistaGrupoAEdicaoId.ToString();
                sqGrid["origem"] = "insert";

                Response.Redirect(string.Format("~/content/edit.aspx?q={0}", sqGrid.ToString()));
            }
            else
            {
                RevistaGrupoAEdicao revista = revistaBLL.CarregarRevistaGrupoAEdicao(new RevistaGrupoAEdicao() { RevistaGrupoAEdicaoId = Convert.ToInt32(hddRevistaId.Value) });
                revista = revistaBLL.CarregarRevistaGrupoAEdicao(new RevistaGrupoAEdicao() { RevistaGrupoAEdicaoId = Convert.ToInt32(hddRevistaId.Value) });
                revista = carregarCampos(revista);
                // Envia atualização à todos os usuários que contém alerta desse revista
                revistaBLL.AtualizarRevistaGrupoAEdicao(revista);
                Util.ShowUpdateMessage();
            }
        }
    }

    /// <summary>
    /// Carrega o formulário conforme o código contido no campo
    /// oculto "hddRevistaId"
    /// </summary>
    public void loadForm()
    {
        // Busca dados
        RevistaGrupoAEdicao revista = new RevistaGrupoAEdicao();
        revista.RevistaGrupoAEdicaoId = int.Parse(hddRevistaId.Value);
        revista = revistaBLL.CarregarRevistaGrupoAEdicao(revista);
        // Carregamento dos Campos
        txtMes.Text = revista.MesPublicacao;
        txtAnoPublicacao.Text = revista.AnoPublicacao;
        txtNumeroEdicao.Text = revista.NumeroEdicao.ToString();
        txtChamada.Text = revista.TextoChamada;
        txtTitulo.Text = revista.TituloRevista;
        txtUrlRevista.Text = revista.UrlRevista;
        if (revista.ArquivoGrande != null && revista.ArquivoGrande.ArquivoId > 0)
        {
            upArquivoImagem.ArquivoId = revista.ArquivoGrande.ArquivoId;
        }
        pnlImagemRevista.Visible = true;

        if (Util.GetQueryString("origem") == "insert")
        {
            Util.ShowInsertMessage();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="revista"></param>
    /// <returns></returns>
    public RevistaGrupoAEdicao carregarCampos(RevistaGrupoAEdicao revista)
    {

        revista.MesPublicacao = txtMes.Text;
        revista.AnoPublicacao = txtAnoPublicacao.Text;
        revista.NumeroEdicao = Convert.ToInt32(txtNumeroEdicao.Text);
        revista.TextoChamada = txtChamada.Text;
        revista.TituloRevista = txtTitulo.Text;
        revista.UrlRevista = txtUrlRevista.Text;
        return revista;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    private void ValidarExists(ServerValidateEventArgs args)
    {
        RevistaGrupoAEdicao revista = new RevistaGrupoAEdicao();
        revista = carregarCampos(revista);
        if (!string.IsNullOrEmpty(hddRevistaId.Value))
        {
            revista.RevistaGrupoAEdicaoId = Convert.ToInt32(hddRevistaId.Value);
        }
        if (revistaBLL.ExisteRevistaGrupoAEdicaoAleracao(revista))
        {
            cvValidarExists.ErrorMessage = "Já existe uma revista com as mesmas informações de ano, mês e número de edicação.";
            args.IsValid = false;
        }
        else
            args.IsValid = true;
        return;
    }

    #endregion
}