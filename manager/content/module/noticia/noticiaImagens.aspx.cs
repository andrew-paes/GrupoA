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
using GrupoA.GlobalResources;

public partial class content_module_noticia_noticiaImagens : System.Web.UI.Page
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
	private string _uploadRoot = GrupoA_Resource.baseUrlUpload; // ConfigurationSettings.AppSettings["uploadRoot"].ToString();

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

            this.ModuleNoticia();

            //não apagar
            ChamaFuncaoJS();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alerta", "alert('Tipo de arquivo não aceito pelo sistema.');", true);
        }
    }

    private void UploadFile()
    {
        long tamanho = 0;
        string nomeArquivo = string.Format("{0}_{1}", DateTime.Now.ToString("yyyyMMddhhmmss"), FileUpload1.FileName);
		string pathFile = string.Concat(Server.MapPath(GrupoA_Resource.baseUrlUpload), _targetFolder);
        if (!Directory.Exists(pathFile))
            Directory.CreateDirectory(pathFile);
        FileUpload1.SaveAs(pathFile + nomeArquivo);

        //salvar arquivo
        FileInfo info = new FileInfo(pathFile + nomeArquivo);
        if (info != null)
        {
            tamanho = info.Length / 1024;

            if (tamanho > _maxFileLength)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alerta", "alert('O tamanho do arquivo é maior que o permitido.');", true);
                return;
            }
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
            var noticiaImagem = new NoticiaBLL()
                .CarregarNoticiaImagem(new NoticiaImagem()
                {
                    Noticia = new GrupoA.BusinessObject.Noticia() { NoticiaId = _registroId },
                    Arquivo = new Arquivo() { ArquivoId = _arquivoId }
                });
            
            string caminhoArquivo = string.Concat(_uploadRoot, _targetFolder, noticiaImagem.Arquivo.NomeArquivo);
            this.MontaThumb(caminhoArquivo);
            this.txtOrdem.Text = noticiaImagem.OrdemApresentacao.ToString();
            this.reqArquivo.Enabled = false;
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

    #region Modulo Noticia

    /// <summary>
    /// Monta regra de negócio do modulo Clipping
    /// </summary>
    private void ModuleNoticia()
    {
        if (FileUpload1.HasFile)
        {
            long tamanho = 0;
            string nomeArquivo = string.Format("{0}_{1}", DateTime.Now.ToString("yyyyMMddhhmmss"), FileUpload1.FileName);
			string pathFile = string.Concat(Server.MapPath(GrupoA_Resource.baseUrlUpload), _targetFolder);
            if (!Directory.Exists(pathFile))
                Directory.CreateDirectory(pathFile);
            FileUpload1.SaveAs(pathFile + nomeArquivo);

            //salvar arquivo
            FileInfo info = new FileInfo(pathFile + nomeArquivo);
            if (info != null)
            {
                tamanho = info.Length / 1024;

                if (tamanho > _maxFileLength)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alerta", "alert('O tamanho do arquivo é maior que o permitido.');", true);
                    return;
                }
            }

            if (_arquivoId > 0)
            {
                //Deleta arquivo anterior
                var arquivo = new NoticiaBLL().CarregarArquivo(new Arquivo() { ArquivoId = _arquivoId });
                if (arquivo != null)
                {
                    string pathFileDelete = string.Concat(Server.MapPath(_uploadRoot), _targetFolder, arquivo.NomeArquivoOriginal);
                    FileInfo infoDelete = new FileInfo(pathFileDelete);
                    if (infoDelete != null)
                        infoDelete.Delete();
                }
                new NoticiaBLL().ExcluirNoticiaImagem(new NoticiaImagem() { Arquivo = new Arquivo() { ArquivoId = _arquivoId } });
            }

            NoticiaImagem noticiaImagem = new NoticiaImagem();
            noticiaImagem.Arquivo = new Arquivo();
            noticiaImagem.Arquivo.TamanhoArquivo = (int)tamanho;
            noticiaImagem.Arquivo.DataHoraUpload = DateTime.Now;
            noticiaImagem.Arquivo.NomeArquivo = nomeArquivo;
            noticiaImagem.Arquivo.NomeArquivoOriginal = FileUpload1.FileName;
            noticiaImagem.OrdemApresentacao = Convert.ToInt32(txtOrdem.Text);
            noticiaImagem.Noticia = new GrupoA.BusinessObject.Noticia();
            noticiaImagem.Noticia.NoticiaId = Convert.ToInt32(_registroId);
            new NoticiaBLL().InserirNoticiaImagem(noticiaImagem);
        }
        else
        {
            var noticiaImagem = new NoticiaBLL()
                .CarregarNoticiaImagem(new NoticiaImagem()
                {
                    Noticia = new GrupoA.BusinessObject.Noticia() { NoticiaId = _registroId },
                    Arquivo = new Arquivo() { ArquivoId = _arquivoId }
                });
            noticiaImagem.OrdemApresentacao = Convert.ToInt32(txtOrdem.Text);
            new NoticiaBLL()
                .AtualizarNoticiaImagem(noticiaImagem);
        }
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