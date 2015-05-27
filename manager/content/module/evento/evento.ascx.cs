using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GrupoA.DataAccess.ADO;
using GrupoA.DataAccess;
using Ag2.Manager.Helper;
using GrupoA.BusinessObject;
using GrupoA.BusinessLogicalLayer;
using System.IO;
using System.Configuration;
using System.Data;
using GrupoA.GlobalResources;

public partial class content_module_evento_evento : System.Web.UI.UserControl
{
    #region [ Properties ]

    /// <summary>
    /// 
    /// </summary>
    private EventoBLL _eventoBLL;

    /// <summary>
    /// 
    /// </summary>
    EventoBLL eventoBLL
    {
        get
        {
            if (_eventoBLL == null)
                _eventoBLL = new EventoBLL();
            return _eventoBLL;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    string folder = GrupoA.GlobalResources.GrupoA_Resource.PastaImagensEvento;

    #endregion

    #region [ Page Events ]

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        ListFiles1.BindList += new EventHandler<ArquivoEventArgs>(ListFiles1_BindList);
        ListFiles1.DeleteItem += new EventHandler<ArquivoEventArgs>(ListFiles1_DeleteItem);

        if (Util.GetRequestId() > 0)
        {
            hddEventoId.Value = Util.GetRequestId().ToString();
            if (!IsPostBack)
            {
                montaChecklistCategoria();
                loadForm();
            }
        }
        else
            if (!IsPostBack)
                montaChecklistCategoria();

        ListFiles1.RegistroId = Util.GetRequestId();
    }

    /// <summary>
    /// Exclui Imagem Seleciona na Lista de Imagens
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //void ListFiles1_DeleteItem(object sender, ArquivoEventArgs e)
    //{
    //    var arquivo = new ClippingBLL().CarregarArquivo(new Arquivo() { ArquivoId = Convert.ToInt32(e.ArquivoId) });
    //    if (arquivo != null)
    //    {
    //        string pathFile = string.Concat(GrupoA_Resource.baseUrlUpload, folder, arquivo.NomeArquivo);
    //        FileInfo info = new FileInfo(pathFile);
    //        if (info.Exists)
    //            info.Delete();
    //    }
    //    new EventoBLL().ExcluirEventoImagem(arquivo);
    //    this.carregarImagensEvento();
    //}

    /// <summary>
    /// Carrega Lista de Imagens
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //void ListFiles1_BindList(object sender, ArquivoEventArgs e)
    //{
    //    this.carregarImagensEvento();
    //}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void ListFiles1_BindList(object sender, ArquivoEventArgs e)
    {
        int arquivoId = Convert.ToInt32(this.hddEventoArquivoId.Value);

        Evento evento = new EventoBLL().CarregarEvento(new Evento() { EventoId = Convert.ToInt32(hddEventoId.Value) });

        if (evento.ArquivoThumb != null)
        {
            hddEventoArquivoIdDelete.Value = evento.ArquivoThumb.ArquivoId.ToString();
        }

        evento.ArquivoThumb = new ArquivoBLL().CarregarArquivo(new Arquivo() { ArquivoId = e.ArquivoId });

        this.hddEventoArquivoId.Value = e.ArquivoId.ToString();

        this.saveOrUpdate();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void ListFiles1_DeleteItem(object sender, ArquivoEventArgs e)
    {
        this.hddEventoArquivoId.Value = "0";
        this.ListFiles1.DataBind();
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dgGrid_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName.ToUpper() == "DELETE")
        {
            var arquivo = new EventoBLL().CarregarArquivo(new Arquivo() { ArquivoId = Convert.ToInt32(e.CommandArgument) });
            bool exclusaoFisica = true;
            if (arquivo != null)
            {
                string pathFile = string.Concat(Server.MapPath(GrupoA_Resource.baseUrlUpload), GrupoA_Resource.PastaImagensEvento, arquivo.NomeArquivoOriginal);
                FileInfo info = new FileInfo(pathFile);
                if (info.Exists)
                    info.Delete();
                else
                    exclusaoFisica = false;
            }
            if (exclusaoFisica)
            {
                new EventoBLL().ExcluirEventoImagem(arquivo);
                this.carregarImagensEvento();
            }
            else
                throw new FileNotFoundException("Não foi possível encontrar o arquivo físico para exclusão");
        }
    }

    #region Validações

    /// <summary>
    /// Validação do intervalo de Datas do Evento
    /// </summary>
    /// <param name="source">Padrão</param>
    /// <param name="args">Padrão</param>
    protected void cvValidarDatasEvento_ServerValidate(object source, ServerValidateEventArgs args)
    {
        this.ValidarDatasEvento(args);
    }

    /// <summary>
    /// Validação do intervalo de Datas de Publicação
    /// </summary>
    /// <param name="source">padrão</param>
    /// <param name="args">padrão</param>
    protected void cvValidarDatasPublicacao_ServerValidate(object source, ServerValidateEventArgs args)
    {
        this.ValidarDatasPublicacao(args);
    }

    #endregion

    #endregion

    #region [ Methods ]

    /// <summary>
    /// Carrega a listagem de Imagens do Evento
    /// </summary>
    private void carregarImagensEvento()
    {
        List<ItemArquivo> arquivos = new List<ItemArquivo>();
        ItemArquivo arquivo = null;

        var imagensEvento = new EventoBLL()
            .CarregarTodosEventoImagem(
                new Evento()
                {
                    EventoId = Convert.ToInt32(hddEventoId.Value)
                }).OrderBy(i => i.OrdemApresentacao);

        foreach (var item in imagensEvento)
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

    /// <summary>
    /// Popula a lista de checklists conforme as categorias base (áreas de conhecimento)
    /// </summary>
    protected void montaChecklistCategoria()
    {
        cblCategorias.DataSource = eventoBLL.CarregarTodasCategoriasBase();
        cblCategorias.DataTextField = "nomeCategoria";
        cblCategorias.DataValueField = "categoriaId";
        cblCategorias.DataBind();
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
            Evento evento = new Evento();
            evento.EventoId = Convert.ToInt32(hddEventoId.Value);
            evento.Local = txtLocal.Text;
            evento.ExibeFormularioContato = chkExibeFormulario.Checked;

            if (Convert.ToInt32(this.hddEventoArquivoId.Value) > 0)
            {
                evento.ArquivoThumb = new Arquivo() { ArquivoId = Convert.ToInt32(this.hddEventoArquivoId.Value) };
            }

            if (hddEventoArquivoIdDelete.Value != "0")
            {
                //Deleta arquivo anterior
                Arquivo arquivoBO = new ArquivoBLL().CarregarArquivo(new Arquivo() { ArquivoId = Convert.ToInt32(hddEventoArquivoIdDelete.Value) });

                if (arquivoBO != null)
                {
                    string pathFileDelete = string.Concat(Server.MapPath(GrupoA_Resource.baseUrlUpload), ListFiles1.TargetFolder, arquivoBO.NomeArquivo);
                    FileInfo infoDelete = new FileInfo(pathFileDelete);

                    if (infoDelete.Exists)
                    {
                        infoDelete.Delete();
                        new ArquivoBLL().ExcluirArquivo(arquivoBO);
                    }
                }
            }

            if (!string.IsNullOrEmpty(txtDataEventoInicio.Text))
            {
                evento.DataEventoInicio = Convert.ToDateTime(txtDataEventoInicio.Text);
            }
            else
            {
                evento.DataEventoInicio = null;
            }

            if (!string.IsNullOrEmpty(txtDataEventoFim.Text))
            {
                evento.DataEventoFim = Convert.ToDateTime(txtDataEventoFim.Text);
            }
            else
            {
                evento.DataEventoFim = null;
            }

            // Dados de Conteúdo Impresa ----------------
            ConteudoImprensa conteudoImprensa = new ConteudoImprensa();
            conteudoImprensa.Fonte = txtFonte.Text;
            conteudoImprensa.FonteUrl = txtFonteURL.Text;
            conteudoImprensa.Ativo = chkAtivo.Checked;

            if (!string.IsNullOrEmpty(txtDataPublicacaoInicio.Text))
                conteudoImprensa.DataExibicaoInicio = Convert.ToDateTime(txtDataPublicacaoInicio.Text);
            else
                conteudoImprensa.DataExibicaoInicio = null;
            if (!string.IsNullOrEmpty(txtDataPublicacaoFim.Text))
                conteudoImprensa.DataExibicaoFim = Convert.ToDateTime(txtDataPublicacaoFim.Text);
            else
                conteudoImprensa.DataExibicaoFim = null;

            conteudoImprensa.Destaque = chkDestaque.Checked;
            conteudoImprensa.Resumo = txtResumo.Text;
            conteudoImprensa.Texto = txtTexto.Text;
            conteudoImprensa.Titulo = txtNome.Text;
            evento.ConteudoImprensa = conteudoImprensa;
            //Popula categorias
            List<Categoria> categorias = new List<Categoria>();

            foreach (ListItem li in cblCategorias.Items)
            {
                if (li.Selected)
                {
                    Categoria categoria = new Categoria();
                    categoria.CategoriaId = Convert.ToInt32(li.Value);
                    categorias.Add(categoria);
                }
            }

            if (hddEventoId.Value == "0")
            {
                eventoBLL.InserirEvento(evento, categorias);

                Ag2.Security.SecureQueryString sqGrid = new Ag2.Security.SecureQueryString();
                sqGrid["md"] = Util.GetQueryString("md");
                sqGrid["id"] = evento.EventoId.ToString();
                sqGrid["origem"] = "insert";

                Response.Redirect(string.Format("~/content/edit.aspx?q={0}", sqGrid.ToString()));
            }
            else
            {
                eventoBLL.AtualizarEvento(evento, categorias);
                // Envia atualização à todos os usuários que contém alerta desse evento
                Util.ShowUpdateMessage();
            }
        }
    }

    /// <summary>
    /// Carrega o formulário conforme o código contido no campo
    /// oculto "hddEventoId"
    /// </summary>
    public void loadForm()
    {
        // Busca dados
        Evento evento = new Evento();
        evento.EventoId = int.Parse(hddEventoId.Value);
        evento = eventoBLL.CarregarEventoComDependencias(evento);
        // Carregamento dos Campos
        //ddlCategoria.SelectedValue = evento.CategoriaEvento.CategoriaEventoId.ToString();
        txtNome.Text = evento.ConteudoImprensa.Titulo;
        txtResumo.Text = evento.ConteudoImprensa.Resumo;
        txtTexto.Text = evento.ConteudoImprensa.Texto;
        txtLocal.Text = evento.Local;
        txtDataEventoInicio.Text = evento.DataEventoInicio.ToString();
        txtDataEventoFim.Text = evento.DataEventoFim.ToString();
        txtDataPublicacaoInicio.Text = evento.ConteudoImprensa.DataExibicaoInicio.ToString();
        txtDataPublicacaoFim.Text = evento.ConteudoImprensa.DataExibicaoFim.ToString();
        txtFonte.Text = evento.ConteudoImprensa.Fonte;
        txtFonteURL.Text = evento.ConteudoImprensa.FonteUrl;
        chkDestaque.Checked = evento.ConteudoImprensa.Destaque;
        chkAtivo.Checked = evento.ConteudoImprensa.Ativo;
        chkExibeFormulario.Checked = evento.ExibeFormularioContato;
        txtTotalAlertas.Text = evento.EventoAlertas.Count.ToString();

        // Busca as categorias e as seleciona
        IEnumerable<Categoria> categorias = eventoBLL.CarregarCategoriasConteudo(evento.ConteudoImprensa.Conteudo);

        foreach (Categoria cat in categorias)
        {
            for (int i = 0; i < cblCategorias.Items.Count; i++)
            {
                if (cblCategorias.Items[i].Value.Equals(cat.CategoriaId.ToString()))
                    cblCategorias.Items[i].Selected = true;
            }
        }

        //this.carregarImagensEvento();
        if (evento.ArquivoThumb != null)
        {
            Arquivo arquivoBO = new ArquivoBLL().CarregarArquivo(new Arquivo() { ArquivoId = evento.ArquivoThumb.ArquivoId });
            this.hddEventoArquivoId.Value = arquivoBO.ArquivoId.ToString();
            //this.hddEventoArquivoNome.Value = arquivoBO.NomeArquivo.ToString();
            this.ListFiles1.CarregaArquivo(arquivoBO.ArquivoId);
        }

        if (Util.GetQueryString("origem") == "insert")
        {
            Util.ShowInsertMessage();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    private void ValidarDatasEvento(ServerValidateEventArgs args)
    {
        try
        {
            if ((txtDataEventoInicio.Text.Length == 0) && (txtDataEventoInicio.Text.Length == 0))
            {
                args.IsValid = true;
                return;
            }
            DateTime dtIni = DateTime.Parse(txtDataEventoInicio.Text);
            DateTime dtFim = DateTime.Parse(txtDataEventoFim.Text);

            if (DateTime.Compare(dtFim, dtIni) >= 0)
            {
                args.IsValid = true;
            }
            else
            {
                cvValidarDatasEvento.ErrorMessage = "Data final deve ser maior que data inicial.";
                args.IsValid = false;
            }
        }
        catch
        {
            cvValidarDatasEvento.ErrorMessage = "Data incorreta!";
            args.IsValid = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    private void ValidarDatasPublicacao(ServerValidateEventArgs args)
    {
        try
        {
            if ((txtDataPublicacaoInicio.Text.Length == 0) && (txtDataPublicacaoFim.Text.Length == 0))
            {
                args.IsValid = true;
                return;
            }
            DateTime dtIni = DateTime.Parse(txtDataPublicacaoInicio.Text);
            DateTime dtFim = DateTime.Parse(txtDataPublicacaoFim.Text);

            if (DateTime.Compare(dtFim, dtIni) >= 0)
            {
                args.IsValid = true;
            }
            else
            {
                cvValidarDatasPublicacao.ErrorMessage = "Data final deve ser maior que data inicial.";
                args.IsValid = false;
            }
        }
        catch
        {
            cvValidarDatasPublicacao.ErrorMessage = "Data incorreta!";
            args.IsValid = false;
        }
    }

    #endregion
}