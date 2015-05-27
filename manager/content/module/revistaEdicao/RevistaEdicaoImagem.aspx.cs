using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using GrupoA.BusinessObject.Enumerator;
using GrupoA.GlobalResources;

public partial class content_module_revistaEdicao_RevistaEdicaoImagem : System.Web.UI.Page
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

    private ImagemThumb.ImagemThumb _geradorDeImagemThumb;

    public ImagemThumb.ImagemThumb GeradorDeImagemThumb
    {
        get { return _geradorDeImagemThumb ?? (_geradorDeImagemThumb = new ImagemThumb.ImagemThumb()); }
        set { _geradorDeImagemThumb = value; }
    }

    private ProdutoBLL _produtoBll;
    private ProdutoBLL ProdutoBll
    {
        get { return _produtoBll ?? (_produtoBll = new ProdutoBLL()); }
        set { _produtoBll = value; }
    }

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

            this.ModuloTitulo();

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

    #region Modulo Titulo

    /// <summary>
    /// Monta regra de negócio do modulo Titulo
    /// </summary>
    private void ModuloTitulo()
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

            //Exclui arquivos e relacionamento anterior
            var produtoImagens = new ProdutoBLL()
                .CarregarProdutoImagem(new ProdutoImagem()
                {
                    Produto = new Produto() { ProdutoId = _registroId },
                    Arquivo = new Arquivo() { ArquivoId = _arquivoId },
                    ProdutoImagemTipo = new ProdutoImagemTipo()
                    {
                        ProdutoImagemTipoId = (int)TipoDeImagemDoProduto.GaleriaDeImagens
                    }
                });

            new ProdutoBLL().ExcluirArquivosProdutoImagem(produtoImagens);
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

        ProdutoImagem produtoImagem = this.InserirProdutoImagem();
        new ProdutoBLL().InserirProdutoImagemArquivo(produtoImagem);

        _idArquivoInserido = produtoImagem.Arquivo.ArquivoId;

        //ChamaFuncaoJS();
        GeraImagens(_idArquivoInserido, _nomeArquivo, produtoImagem.Produto.ProdutoId);

    }

    private void GeraImagens(int idArquivo, string nomeArquivo, int id)
    {
        if (Page.IsValid)
        {
            if (idArquivo > 0)
            {
                // TODO: Verificar se isso está funcionando corretamente
                string strFilePath = Server.MapPath(string.Concat(GrupoA_Resource.baseUrlUpload, "/", "imagensRevista", "/"));
                string strFileName = strFilePath + nomeArquivo;

                string strFileNameG = Path.GetFileNameWithoutExtension(strFileName) + "_G" + Path.GetExtension(strFileName);
                string strFileNameM = Path.GetFileNameWithoutExtension(strFileName) + "_M" + Path.GetExtension(strFileName);
                string strFileNameP = Path.GetFileNameWithoutExtension(strFileName) + "_P" + Path.GetExtension(strFileName);
                
                using (System.Drawing.Image image = System.Drawing.Image.FromFile(strFileName))
                {
                    int widthG = image.Width;
                    int heightG = image.Height;
                    if (widthG > 330 || heightG > 440)
                    {
                        widthG = 330;
                        heightG = 440;
                    }

                    image.ResizeTo(widthG, heightG, ResizeModes.Fit).StreamSave(String.Concat(strFilePath, strFileNameG));

                    int widthM = image.Width;
                    int heightM = image.Height;
                    if (widthM > 165 || heightM > 220)
                    {
                        widthM = 165;
                        heightM = 220;
                    }

                    image.ResizeTo(widthM, heightM, ResizeModes.Fit).StreamSave(String.Concat(strFilePath, strFileNameM));

                    int widthP = image.Width;
                    int heightP = image.Height;
                    if (widthP > 95 || heightP > 128)
                    {
                        widthP = 95;
                        heightP = 128;
                    }

                    image.ResizeTo(widthP, heightP, ResizeModes.Fit).StreamSave(String.Concat(strFilePath, strFileNameP));
                }

                File.Delete(strFileName);

                // Ajustar, imagem vem de ProdutoImagem.
                var produtoImagens = new List<ProdutoImagem>();
                produtoImagens.Add(this.PopularProdutoImagem(strFilePath, strFileNameG, (int)TipoDeImagemDoTitulo.Grande));
                produtoImagens.Add(this.PopularProdutoImagem(strFilePath, strFileNameM, (int)TipoDeImagemDoTitulo.Media));
                produtoImagens.Add(this.PopularProdutoImagem(strFilePath, strFileNameP, (int)TipoDeImagemDoTitulo.Pequena));

                ////Exclui arquivos e relacionamento anterior
                var produtoImagensParaAtualizacao =
                    ProdutoBll.CarregarProdutoImagem(new ProdutoImagem() { Produto = new Produto() { ProdutoId = id } }).ToList();
                foreach (var produtoImagem in produtoImagensParaAtualizacao)
                {
                    ProdutoBll.ExcluirProdutoImagem(produtoImagem);
                }

                ProdutoBll.InserirProdutoImagem(produtoImagens);

                this.ExcluirArquivosFisicamente(produtoImagensParaAtualizacao, strFilePath);
            }
            else
            {
                Util.ShowMessage("Você não alterou a imagem.");
            }

            this.LoadForm();
        }
    }

    // Ajustar, imagem vem de ProdutoImagem.
    private void ExcluirArquivosFisicamente(IList<ProdutoImagem> arquivos, string strFilePath)
    {
        foreach (var item in arquivos)
        {
            if (File.Exists(strFilePath + item.Arquivo.NomeArquivo))
                File.Delete(strFilePath + item.Arquivo.NomeArquivo);
        }
    }

    // Ajustar, imagem vem de ProdutoImagem.
    private ProdutoImagem PopularProdutoImagem(string strFilePath, string strFileName, int produtoImagemTipoId)
    {
        var produtoImagem = new ProdutoImagem { ProdutoImagemTipo = new ProdutoImagemTipo() };
        produtoImagem.ProdutoImagemTipo.ProdutoImagemTipoId = produtoImagemTipoId;

        produtoImagem.Produto = new Produto();
        produtoImagem.Produto.ProdutoId = _registroId;

        produtoImagem.Arquivo = new Arquivo();
        if (File.Exists(strFilePath + strFileName))
        {
            System.IO.FileInfo info = new System.IO.FileInfo(strFilePath + strFileName);
            produtoImagem.Arquivo.TamanhoArquivo = Convert.ToInt32(info.Length);
            produtoImagem.Arquivo.NomeArquivo = strFileName;
            produtoImagem.Arquivo.NomeArquivoOriginal = strFileName;
            produtoImagem.Arquivo.DataHoraUpload = DateTime.Now;
        }
        return produtoImagem;
    }

    private ProdutoImagem InserirProdutoImagem()
    {
        var produtoImagem = new ProdutoImagem();
        produtoImagem.ProdutoImagemTipo = new ProdutoImagemTipo();
        produtoImagem.ProdutoImagemTipo.ProdutoImagemTipoId = (int)TipoDeImagemDoProduto.GaleriaDeImagens;

        produtoImagem.Produto = new Produto();
        produtoImagem.Produto.ProdutoId = _registroId;

        produtoImagem.Arquivo = new Arquivo();
        produtoImagem.Arquivo.TamanhoArquivo = (int)FileUpload1.FileContent.Length;
        produtoImagem.Arquivo.NomeArquivo = _nomeArquivo;
        produtoImagem.Arquivo.NomeArquivoOriginal = FileUpload1.FileName;
        produtoImagem.Arquivo.DataHoraUpload = DateTime.Now;
        return produtoImagem;
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