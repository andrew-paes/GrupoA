using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using GrupoA.GlobalResources;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;

public partial class content_module_autor_autor : System.Web.UI.UserControl
{
    #region Propriedades

    private int _id
    {
        get
        {
            if (Session["_idAutor"] == null)
                Session["_idAutor"] = 0;

            return (int)Session["_idAutor"];
        }
        set
        {
            Session["_idAutor"] = (int)value;
        }
    }

    #endregion

    #region Eventos

    void upArquivoImagem_BindList(object sender, ArquivoEventArgs e)
    {
        this.upArquivoImagem.ArquivoId = e.ArquivoId;
        this.upArquivoImagem.RegistroId = Convert.ToInt32(_id);

        //cria o relacionamento
        if (e.ArquivoId > 0)
        {
            Autor autor = new AutorBLL().CarregarAutorComDependencia(new Autor() { AutorId = Convert.ToInt32(_id) });
            Arquivo arquivoDeletar = null;
            if (autor.ArquivoImagem != null)
            {
                arquivoDeletar = autor.ArquivoImagem;
            }
            autor.ArquivoImagem = new ArquivoBLL().CarregarArquivo(new Arquivo() { ArquivoId = this.upArquivoImagem.ArquivoId });
            new AutorBLL().AtualizarAutor(autor);

            //Redimensiona a imagem pequena
            string imageFile = string.Concat(GrupoA_Resource.baseUrlUpload, GrupoA_Resource.PastaImagensAutor, autor.ArquivoImagem.NomeArquivo);
            string path = HttpContext.Current.Request.MapPath(imageFile);

            System.Drawing.Image resizedImage;

            using (System.Drawing.Image image = System.Drawing.Image.FromFile(path))
            {
                string thumbPath = Regex.Replace(path, "(\\.[^\\.]+)$", "-big$1");

                int bigHeight = image.Height;
                int bigWidth = image.Width;
                if (bigHeight > 712 || bigWidth > 950)
                {
                    bigHeight = 712;
                    bigWidth = 950;
                }

                image.ResizeTo(bigWidth, bigHeight, ResizeModes.Fit).StreamSave(thumbPath);

                resizedImage = image.ResizeTo(215, 160, ResizeModes.Fit);
            }

            resizedImage.StreamSave(path);
            resizedImage.Dispose();

            if (arquivoDeletar != null)
            {
                DeletarArquivo(arquivoDeletar);
            }
        }
    }

    void upArquivoImagem_DeleteItem(object sender, ArquivoEventArgs e)
    {
        Autor autor = new AutorBLL().CarregarAutorComDependencia(new Autor() { AutorId = Convert.ToInt32(_id) });

        if (autor != null && autor.ArquivoImagem != null)
        {
            Arquivo arquivo = autor.ArquivoImagem;
            //remove relacionamento
            autor.ArquivoImagem = null;
            new AutorBLL().AtualizarAutor(autor);

            arquivo = DeletarArquivo(arquivo);

            //atualiza ListFile
            this.upArquivoImagem.RegistroId = Convert.ToInt32(_id);
            this.upArquivoImagem.DataBind();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.upArquivoImagem.BindList += new EventHandler<ArquivoEventArgs>(upArquivoImagem_BindList);
        this.upArquivoImagem.DeleteItem += new EventHandler<ArquivoEventArgs>(upArquivoImagem_DeleteItem);
        this.upArquivoImagem.RegistroId = Convert.ToInt32(_id);

        if (Util.GetRequestId() > 0)
        {
            _id = Util.GetRequestId();

            if (!IsPostBack)
            {
                loadForm(_id);
            }
        }
        else
        {
            divTitulosRelacionados.Visible = false;
        }
    }

    protected void rptTitulosRelacionados_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Titulo titulo = (Titulo)e.Item.DataItem;
        Label lblNomeTitulo = (Label)e.Item.FindControl("lblNomeTitulo");

        lblNomeTitulo.Text = titulo.NomeTitulo;
    }

    protected void btnExecute_Click(object sender, ImageClickEventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = false;
        SaveOrUpdate();
    }

    #endregion

    #region Métodos

    private void loadForm(int _id)
    {
        if (_id > 0)
        {
            this.pnlArquivo.Visible = true;

            var autor = new AutorBLL().CarregarAutorComDependencia(new Autor() { AutorId = _id });
            txtNome.Text = autor.NomeAutor;
            txtEmail.Text = autor.Email;
            txtUrl.Text = autor.Url;
            txtBlog.Text = autor.Blog;
            //txtCodigoLegado.Text = autor.CodigoLegado;
            txtBiografia.Text = autor.Biografia;

            rptTitulosRelacionados.DataSource = new TituloBLL().CarregarTitulosPorAutor(autor);
            rptTitulosRelacionados.DataBind();

            if (autor.ArquivoImagem != null && autor.ArquivoImagem.ArquivoId > 0)
            {
                this.upArquivoImagem.ArquivoId = autor.ArquivoImagem.ArquivoId;
            }
        }
    }

    private void SaveOrUpdate()
    {
        if (Page.IsValid)
        {
            var autor = new Autor();
            if (_id == 0)
            {
                autor = new Autor();
            }
            else
            {
                autor = new AutorBLL().CarregarAutorComDependencia(new Autor() { AutorId = Convert.ToInt32(_id) });
            }

            autor.NomeAutor = txtNome.Text;
            autor.Email = txtEmail.Text;
            autor.Url = txtUrl.Text;
            autor.Blog = txtBlog.Text;
            autor.AutorId = _id;
            //autor.CodigoLegado = txtCodigoLegado.Text;
            autor.Biografia = txtBiografia.Text;

            if (_id == 0)
            {
                new AutorBLL().InserirNovoAutor(autor);
                _id = autor.AutorId;
                Util.ShowInsertMessage();
                _id = 0;
                txtNome.Text = String.Empty;
                txtUrl.Text = String.Empty;
                txtEmail.Text = String.Empty;
                txtBlog.Text = String.Empty;
                txtBiografia.Text = String.Empty;
            }
            else
            {
                new AutorBLL().AtualizarAutor(autor);
                Util.ShowUpdateMessage();
            }
        }
    }

    private Arquivo DeletarArquivo(Arquivo arquivo)
    {
        arquivo = new ArquivoBLL().CarregarArquivo(arquivo);

        //remove da tabela Arquivo
        new ArquivoBLL().ExcluirArquivo(arquivo);

        //apaga arquivo físico
        string pathFile = string.Concat(Server.MapPath(GrupoA_Resource.baseUrlUpload), GrupoA_Resource.PastaImagensAutor, arquivo.NomeArquivo);
        FileInfo info = new FileInfo(pathFile);
        if (info.Exists)
            info.Delete();

        string thumbPath = Regex.Replace(pathFile, "(\\.[^\\.]+)$", "-big$1");
        info = new FileInfo(thumbPath);
        if (info.Exists)
            info.Delete();

        return arquivo;
    }

    #endregion
}
