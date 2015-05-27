using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using GrupoA.BusinessObject;
using System.Configuration;
using GrupoA.BusinessLogicalLayer;
using Ag2.Manager.Helper;
using GrupoA.BusinessObject.Enumerator;
using GrupoA.GlobalResources;

public partial class content_module_tituloImagem_tituloImagem : System.Web.UI.Page
{
    #region Propriedades

    private string _idObjPostback = string.Empty;
    private int _idArquivoInserido = 0;
    private string _tipoArquivo = string.Empty;
    private int _registroId = 0;
    private int _arquivoId = 0;
    private Int64 _maxFileLength = 300;
    private string _targetFolder = string.Empty;
    private string _moduleName = string.Empty;
    private string _uploadRoot = GrupoA_Resource.baseUrlUpload;
    private string _nomeArquivo = string.Empty;
    private string _pathFile = string.Empty;

    #endregion

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        //Anexa o event handler que executa o evendo de Click para gravar o arquivo na base de dados
        btnUploadFile.Click += new EventHandler(btnUploadFile_Click);
        imgThumb.Visible = false;
        //não apagar
        CarregaVariaveisQueryString();

        if (!IsPostBack)
            this.LoadForm();
   }

    void btnUploadFile_Click(object sender, EventArgs e)
    {
        if (ValidaArquivo())
        {
            _idArquivoInserido = 0;

            this.ModuloTituloImagem();

            //não apagar
            ChamaFuncaoJS();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alerta", "alert('Tipo de arquivo não aceito pelo sistema.');", true);
        }
    }

    /// <summary>
    /// Verifica se o arquivo enviado para o server é válido ou não
    /// </summary>
    /// <returns></returns>
    private bool ValidaArquivo()
    {

        bool retorno = true;
        string ext = Path.GetExtension(FileUpload1.FileName).ToLower().Replace(".", string.Empty);

        if (_tipoArquivo.Equals("IMAGE", StringComparison.OrdinalIgnoreCase))
        {
            string[] extAceitas = { "jpg", "gif", "png" };

            if (!extAceitas.Contains(ext))
                return false;
        }
        else if (_tipoArquivo.Equals("DOCUMENT", StringComparison.OrdinalIgnoreCase))
        {
            string[] extAceitas = { "doc", "docx", "ppt", "pptx", "xls", "xlsx", "pdf" };

            if (!extAceitas.Contains(ext))
                return false;
        }
        else if (_tipoArquivo.Equals("ALL", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }
        else
        {
            if (!_tipoArquivo.Equals(ext, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
        }

        return retorno;
    }

    #endregion

    #region Métodos

    private void LoadForm()
    {
        if (_arquivoId > 0)
        {
            var arquivo = new Arquivo();
            arquivo.ArquivoId = _arquivoId;
            arquivo = new ArquivoBLL().CarregarArquivo(arquivo);
            string caminhoArquivo = string.Concat(_uploadRoot, _targetFolder, arquivo.NomeArquivo);
            this.MontaThumb(caminhoArquivo);
        }
    }

    private void CarregaVariaveisQueryString()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["idObjPostback"]))
        {
            _idObjPostback = Request.QueryString["idObjPostback"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["tipo"]))
        {
            _tipoArquivo = Request.QueryString["tipo"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["tipo"]))
        {
            _registroId = Convert.ToInt32(Request.QueryString["id"].ToString());
        }
        if (!string.IsNullOrEmpty(Request.QueryString["mfl"]))
        {
            _maxFileLength = Convert.ToInt64(Request.QueryString["mfl"].ToString());
        }
        if (!string.IsNullOrEmpty(Request.QueryString["tf"]))
        {
            _targetFolder = Request.QueryString["tf"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["arquivoId"]))
        {
            _arquivoId = Convert.ToInt32(Request.QueryString["arquivoId"].ToString());
        }
        if (!string.IsNullOrEmpty(Request.QueryString["md"]))
        {
            _moduleName = Request.QueryString["md"].ToString();
        }
    }

    private void ChamaFuncaoJS()
    {
        //CHAMA FUNCAO JS DA PAGINA PAI RESPONSAVEL EM EXECUTAR O EVENTO DE POSTBACK NA PAGINA PAI
        this.ClientScript.RegisterStartupScript(Page.GetType(), "postBackParent", "parent.ExecuteDelegateEvent('" + _idObjPostback + "', " + _idArquivoInserido + ");", true);
    }

    #endregion

    #region Modulo Titulo Conteudo Extra

    /// <summary>
    /// Monta regra de negócio do modulo ModuloTituloConteudoExtra
    /// </summary>
    private void ModuloTituloImagem()
    {
        //Deleta arquivo anterior
        if (_arquivoId > 0)
        {
            var arquivo = new ArquivoBLL().CarregarArquivo(new Arquivo() { ArquivoId = _arquivoId });
            if (arquivo != null)
            {
                string pathFileDelete = string.Concat(Server.MapPath(_uploadRoot), _targetFolder, arquivo.NomeArquivo);
                FileInfo infoDelete = new FileInfo(pathFileDelete);
                if (infoDelete != null)
                    infoDelete.Delete();
            }

            var titulo = new TituloBLL().CarregarConteudoExtra(new Titulo() { TituloId = _registroId });
            List<TituloConteudoExtraArquivo> tituloConteudoExtraArquivoExcluir = new List<TituloConteudoExtraArquivo>();
            bool delete = false;

            if (titulo.TituloConteudoExtraArquivos.Count > 0)
                foreach (var item in titulo.TituloConteudoExtraArquivos)
                {
                    if (item.Arquivo != null && item.Arquivo.ArquivoId == _arquivoId)
                    {
                        item.Arquivo = null;
                        delete = true;
                        tituloConteudoExtraArquivoExcluir.Add(item);
                    }
                }
            if (delete)
            new TituloBLL().ExcluirConteudoExtraArquivo(tituloConteudoExtraArquivoExcluir);
        }

        _nomeArquivo = string.Format("{0}_{1}", DateTime.Now.ToString("yyyyMMddhhmmss"), FileUpload1.FileName);
        _pathFile = string.Concat(Server.MapPath(_uploadRoot), _targetFolder);
        if (!Directory.Exists(_pathFile))
            Directory.CreateDirectory(_pathFile);

        if (FileUpload1.HasFile)
        {
            if (FileUpload1.FileContent.Length > (_maxFileLength * 1024))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alerta", "alert('O tamanho do arquivo é maior que o permitido.');", true);
                return;
            }
            FileUpload1.SaveAs(_pathFile + _nomeArquivo);
        }

        new TituloBLL().InserirConteudoExtraArquivo(this.InserirTituloConteudoExtraArquivo());

    }

    /// <summary>
    /// Popula a Entidade TituloConteudoExtraArquivo para Inserção
    /// </summary>
    /// <returns></returns>
    private TituloConteudoExtraArquivo InserirTituloConteudoExtraArquivo()
    {
        var tituloConteudoExtraArquivo  = new TituloConteudoExtraArquivo();
        tituloConteudoExtraArquivo.Titulo = new Titulo() { TituloId = _registroId };

        tituloConteudoExtraArquivo.Arquivo = new Arquivo();
        tituloConteudoExtraArquivo.Arquivo.TamanhoArquivo = (int)FileUpload1.FileContent.Length;
        tituloConteudoExtraArquivo.Arquivo.NomeArquivo = _nomeArquivo;
        tituloConteudoExtraArquivo.Arquivo.NomeArquivoOriginal = FileUpload1.FileName;
        tituloConteudoExtraArquivo.Arquivo.DataHoraUpload = DateTime.Now;
        new ArquivoBLL().InserirNovoArquivo(tituloConteudoExtraArquivo.Arquivo);
        return tituloConteudoExtraArquivo;
    }

    private void MontaThumb(string caminhoArquivo)
    {
        if (Path.GetExtension(caminhoArquivo).Equals(".doc", StringComparison.OrdinalIgnoreCase)
            || Path.GetExtension(caminhoArquivo).Equals(".docx", StringComparison.OrdinalIgnoreCase))
        {
            divThumb.Attributes.Add("class", "divItemThumbDOC");
        }
        else if (Path.GetExtension(caminhoArquivo).Equals(".ppt", StringComparison.OrdinalIgnoreCase)
            || Path.GetExtension(caminhoArquivo).Equals(".pptx", StringComparison.OrdinalIgnoreCase))
        {
            divThumb.Attributes.Add("class", "divItemThumbPPT");
        }
        else if (Path.GetExtension(caminhoArquivo).Equals(".xls", StringComparison.OrdinalIgnoreCase)
            || Path.GetExtension(caminhoArquivo).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
        {
            divThumb.Attributes.Add("class", "divItemThumbXLS");
        }
        else if (Path.GetExtension(caminhoArquivo).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
        {
            divThumb.Attributes.Add("class", "divItemThumbPDF");
        }
        else if (Path.GetExtension(caminhoArquivo).Equals(".zip", StringComparison.OrdinalIgnoreCase))
        {
            divThumb.Attributes.Add("class", "divItemThumbZIP");
        }
        else if (Path.GetExtension(caminhoArquivo).Equals(".txt", StringComparison.OrdinalIgnoreCase))
        {
            divThumb.Attributes.Add("class", "divItemThumbTXT");
        }
        else if (Path.GetExtension(caminhoArquivo).Equals(".gif", StringComparison.OrdinalIgnoreCase)
            || Path.GetExtension(caminhoArquivo).Equals(".png", StringComparison.OrdinalIgnoreCase)
            || Path.GetExtension(caminhoArquivo).Equals(".jpg", StringComparison.OrdinalIgnoreCase))
        {
            divThumb.Attributes.Add("class", "divItemThumbIMG");
            imgThumb.Visible = true;
            imgThumb.ImageUrl = string.Format("~/Thumb.aspx?file={0}&w={1}&h={2}", caminhoArquivo, "122", "90");
        }
    }

    #endregion

}