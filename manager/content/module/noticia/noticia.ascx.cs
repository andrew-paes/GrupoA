using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.EnterpriseLibrary.Data;
using GrupoA.BusinessObject;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using System.IO;
using System.Configuration;
using System.Data;
using GrupoA.GlobalResources;

public partial class content_module_noticia_noticia : System.Web.UI.UserControl
{
    private string folder = GrupoA.GlobalResources.GrupoA_Resource.PastaImagensNoticia;

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        ListFiles1.BindList += new EventHandler<ArquivoEventArgs>(ListFiles1_BindList);
        ListFiles1.DeleteItem += new EventHandler<ArquivoEventArgs>(ListFiles1_DeleteItem);

        if (Util.GetRequestId() > 0)
        {
            var _id = Util.GetRequestId();

            if (!IsPostBack)
            {
                hddNoticiaId.Value = _id.ToString();
                montaChecklistCategoria();
                this.loadForm(_id);
            }
        }
        else
        {
            if (!IsPostBack)
            {
                montaChecklistCategoria();
            }
        }

        if (!IsPostBack)
        {
            this.CarregarCategoraNoticia();
        }

        ListFiles1.RegistroId = Util.GetRequestId();
    }

    protected void btnExecute_Click(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = false;
        SaveOrUpdate();
    }

    void ListFiles1_DeleteItem(object sender, ArquivoEventArgs e)
    {
        var arquivo = new NoticiaBLL().CarregarArquivo(new Arquivo() { ArquivoId = e.ArquivoId });
        if (arquivo != null)
        {
            var folder = GrupoA.GlobalResources.GrupoA_Resource.PastaImagensNoticia;
            string pathFile = string.Concat(Server.MapPath(GrupoA_Resource.baseUrlUpload), folder, arquivo.NomeArquivoOriginal);
            FileInfo info = new FileInfo(pathFile);
            if (info != null) info.Delete();
        }
        new NoticiaBLL().ExcluirNoticiaImagem(new NoticiaImagem() { Arquivo = new Arquivo() { ArquivoId = e.ArquivoId } });
        this.CarregarImagensNoticia();
        this.CarregarImagensNoticia();

    }

    void ListFiles1_BindList(object sender, ArquivoEventArgs e)
    {
        this.CarregarImagensNoticia();
    }

    #endregion

    #region Functions

    protected void loadForm(int id)
    {
        if (id > 0)
        {
            var noticia = new NoticiaBLL().Carregar(new GrupoA.BusinessObject.Noticia() { NoticiaId = id });
            hddNoticiaId.Value = noticia.NoticiaId.ToString();
            this.txtTitulo.Text = noticia.ConteudoImprensa.Titulo;
            this.txtIntegra.Text = noticia.ConteudoImprensa.Texto;
            this.txtAutor.Text = noticia.Autor;
            this.txtFonte.Text = noticia.ConteudoImprensa.Fonte;
            this.txtFonteUrl.Text = noticia.ConteudoImprensa.FonteUrl;
            chkAtivo.Checked = noticia.ConteudoImprensa.Ativo;
            chkDestaque.Checked = noticia.ConteudoImprensa.Destaque;
            txtDataPublicacao.Text = noticia.DataPublicacao != null ? noticia.DataPublicacao.Value.ToString("dd/MM/yyyy") : String.Empty;
            txtDataExibicaoInicio.Text = noticia.ConteudoImprensa.DataExibicaoInicio != null ? noticia.ConteudoImprensa.DataExibicaoInicio.Value.ToString("dd/MM/yyyy") : String.Empty;
            txtDataExibicaoFim.Text = noticia.ConteudoImprensa.DataExibicaoFim != null ? noticia.ConteudoImprensa.DataExibicaoFim.Value.ToString("dd/MM/yyyy") : String.Empty;
            this.ddlCategoria.SelectedValue = noticia.CategoriaNoticia.CategoriaNoticiaId.ToString();

            // Busca as categorias e as seleciona
            IEnumerable<Categoria> categorias = new EventoBLL().CarregarCategoriasConteudo(noticia.ConteudoImprensa.Conteudo);

            foreach (Categoria cat in categorias)
            {
                for (int i = 0; i < cblCategorias.Items.Count; i++)
                {
                    if (cblCategorias.Items[i].Value.Equals(cat.CategoriaId.ToString()))
                        cblCategorias.Items[i].Selected = true;
                }
            }

            this.CarregarImagensNoticia();

            if (Util.GetQueryString("origem") == "insert")
            {
                Util.ShowInsertMessage();
            }
        }
    }

    protected void SaveOrUpdate()
    {
        if (Page.IsValid)
        {
            var noticia = new GrupoA.BusinessObject.Noticia();
            noticia.NoticiaId = Convert.ToInt32(hddNoticiaId.Value);
            noticia.ConteudoImprensa = new ConteudoImprensa();
            noticia.ConteudoImprensa.Titulo = this.txtTitulo.Text.Trim();
            noticia.ConteudoImprensa.Texto = this.txtIntegra.Text.Replace("/manager/", "/site/");
            noticia.Autor = this.txtAutor.Text.Trim();
            noticia.ConteudoImprensa.Fonte = this.txtFonte.Text.Trim();
            noticia.ConteudoImprensa.FonteUrl = this.txtFonteUrl.Text.Trim();
            noticia.ConteudoImprensa.Ativo = chkAtivo.Checked;
            noticia.ConteudoImprensa.Destaque = chkDestaque.Checked;

            noticia.ConteudoImprensa.FonteUrl = this.txtFonteUrl.Text.Trim();

            if (!string.IsNullOrEmpty(txtDataPublicacao.Text))
                noticia.DataPublicacao = Convert.ToDateTime(txtDataPublicacao.Text);

            if (!string.IsNullOrEmpty(txtDataExibicaoInicio.Text))
                noticia.ConteudoImprensa.DataExibicaoInicio = Convert.ToDateTime(txtDataExibicaoInicio.Text);
            else
                noticia.ConteudoImprensa.DataExibicaoInicio = null;

            if (!string.IsNullOrEmpty(txtDataExibicaoFim.Text))
                noticia.ConteudoImprensa.DataExibicaoFim = Convert.ToDateTime(txtDataExibicaoFim.Text);
            else
                noticia.ConteudoImprensa.DataExibicaoFim = null;

            noticia.CategoriaNoticia = new CategoriaNoticia() { CategoriaNoticiaId = Convert.ToInt32(this.ddlCategoria.SelectedValue) };

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

            if (noticia.NoticiaId > 0)
            {
                new NoticiaBLL().Atualizar(noticia, categorias);
                Util.ShowUpdateMessage();
            }
            else
            {
                noticia = new NoticiaBLL().Inserir(noticia, categorias);
                
                Ag2.Security.SecureQueryString sqGrid = new Ag2.Security.SecureQueryString();
                sqGrid["md"] = Util.GetQueryString("md");
                sqGrid["id"] = noticia.NoticiaId.ToString();
                sqGrid["origem"] = "insert";

                Response.Redirect(string.Format("~/content/edit.aspx?q={0}", sqGrid.ToString()));
            }
        }
    }

    private void CarregarImagensNoticia()
    {
        List<ItemArquivo> arquivos = new List<ItemArquivo>();
        ItemArquivo arquivo = null;

        var imagensClipping = new NoticiaBLL()
            .CarregarTodosNoticiaImagem(
                    new GrupoA.BusinessObject.Noticia()
                    {
                        NoticiaId = Convert.ToInt32(hddNoticiaId.Value)
                    }
                ).OrderBy(i => i.OrdemApresentacao);

        foreach (var item in imagensClipping)
        {
            arquivo = new ItemArquivo();
            arquivo.ArquivoId = item.Arquivo.ArquivoId;
            string pathFile = string.Concat(GrupoA_Resource.baseUrlUpload, folder, item.Arquivo.NomeArquivo);

            string path = HttpContext.Current.Request.MapPath(pathFile);

            if (File.Exists(path))
            {

                System.Drawing.Image resizedImage;
                using (System.Drawing.Image image = System.Drawing.Image.FromFile(path))
                {
                    int bigHeight = image.Height;
                    int bigWidth = image.Width;
                    if (bigWidth > 215)
                    {
                        bigWidth = 215;
                    }

                    //image.ResizeTo(bigWidth, bigHeight, ResizeModes.Fit).StreamSave(path);

                    resizedImage = image.ResizeTo(bigWidth, bigHeight, ResizeModes.Fit);
                }
                resizedImage.StreamSave(path);
                resizedImage.Dispose();

                arquivo.CaminhoArquivo = pathFile;
                arquivo.TamanhoArquivo = item.Arquivo.TamanhoArquivo.ToString();
                arquivos.Add(arquivo);
            }
        }

        ListFiles1.DataSource = arquivos;
        ListFiles1.DataBind();
    }

    private void CarregarCategoraNoticia()
    {
        ddlCategoria.DataSource = new NoticiaBLL().CarregarTodosCategoriaNoticia();
        ddlCategoria.DataTextField = "NomeCategoriaNoticia";
        ddlCategoria.DataValueField = "CategoriaNoticiaId";
        ddlCategoria.DataBind();
        ddlCategoria.Items.Insert(0, ":: Selecione ::");
    }

    /// <summary>
    /// Popula a lista de checklists conforme as categorias base (áreas de conhecimento)
    /// </summary>
    protected void montaChecklistCategoria()
    {
        cblCategorias.DataSource = new EventoBLL().CarregarTodasCategoriasBase();
        cblCategorias.DataTextField = "nomeCategoria";
        cblCategorias.DataValueField = "categoriaId";
        cblCategorias.DataBind();
    }

    #endregion

    #region Validacoes

    protected void cvValidarDatasPublicacao_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (string.IsNullOrEmpty(txtDataExibicaoInicio.Text) && string.IsNullOrEmpty(txtDataExibicaoFim.Text))
        {
            args.IsValid = true; return;
        }

        try
        {
            DateTime dtIni = DateTime.Parse(txtDataExibicaoInicio.Text);
            DateTime dtFim = DateTime.Parse(txtDataExibicaoFim.Text);

            if (DateTime.Compare(dtFim, dtIni) > 0)
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
