using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ag2.Manager.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using System.Configuration;
using System.IO;
using GrupoA.GlobalResources;

public partial class content_module_comentarioEspecialista_comentarioEspecialista : SmartUserControl
{
    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        this.upArquivoImagem.BindList += new EventHandler<ArquivoEventArgs>(upArquivoImagem_BindList);
        this.upArquivoImagem.DeleteItem += new EventHandler<ArquivoEventArgs>(upArquivoImagem_DeleteItem);
        this.upArquivoImagem.RegistroId = Convert.ToInt32(IdRegistro);
        //this.upArquivoAudio.BindList += new EventHandler<ArquivoEventArgs>(upArquivoAudio_BindList);
        //this.upArquivoAudio.DeleteItem += new EventHandler<ArquivoEventArgs>(upArquivoAudio_DeleteItem);
        //this.upArquivoAudio.RegistroId = Convert.ToInt32(IdRegistro);

        if (!IsPostBack)
        {
            LoadForm();
        }
    }

    void upArquivoImagem_BindList(object sender, ArquivoEventArgs e)
    {
        this.upArquivoImagem.ArquivoId = e.ArquivoId;
        this.upArquivoImagem.RegistroId = Convert.ToInt32(IdRegistro);

        //cria o relacionamento
        if (e.ArquivoId > 0)
        {
            TituloBLL tituloBLL = new TituloBLL();
            TituloImpresso tituloImpresso = tituloBLL.CarregarTituloImpressoComComentarioDoEspecialista(new TituloImpresso() { TituloImpressoId = Convert.ToInt32(IdRegistro) });
            Titulo titulo = tituloImpresso.Titulo;
            Arquivo arquivoDeletar = null;

            if (titulo.TituloInformacaoComentarioEspecialista == null || titulo.TituloInformacaoComentarioEspecialista.TituloInformacaoComentarioEspecialistaId < 1)
            {
                //titulo.TituloInformacaoComentarioEspecialista = new TituloInformacaoComentarioEspecialista() { TituloInformacaoComentarioEspecialistaId = titulo.TituloId };
                titulo.TituloInformacaoComentarioEspecialista = new TituloInformacaoComentarioEspecialista();
            }
            else
            {
                if (titulo.TituloInformacaoComentarioEspecialista.ArquivoImagem != null)
                {
                    arquivoDeletar = titulo.TituloInformacaoComentarioEspecialista.ArquivoImagem;
                }
            }

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

            titulo.TituloInformacaoComentarioEspecialista.ArquivoImagem = new ArquivoBLL().CarregarArquivo(new Arquivo() { ArquivoId = this.upArquivoImagem.ArquivoId });
            tituloBLL.AtualizarTituloComentarioEspecialista(titulo, categorias);

            if (arquivoDeletar != null)
            {
                DeletarArquivoComentarioEspecialista(arquivoDeletar);
            }
        }
    }

    //void upArquivoAudio_BindList(object sender, ArquivoEventArgs e)
    //{
    //    this.upArquivoAudio.ArquivoId = e.ArquivoId;
    //    this.upArquivoAudio.RegistroId = Convert.ToInt32(IdRegistro);

    //    //cria o relacionamento
    //    if (e.ArquivoId > 0)
    //    {
    //        TituloBLL tituloBLL = new TituloBLL();
    //        TituloImpresso tituloImpresso = tituloBLL.CarregarTituloImpressoComComentarioDoEspecialista(new TituloImpresso() { TituloImpressoId = Convert.ToInt32(IdRegistro) });
    //        Titulo titulo = tituloImpresso.Titulo;
    //        if (titulo.TituloInformacaoComentarioEspecialista == null || titulo.TituloInformacaoComentarioEspecialista.TituloInformacaoComentarioEspecialistaId < 1)
    //        {
    //            titulo.TituloInformacaoComentarioEspecialista = new TituloInformacaoComentarioEspecialista() { TituloInformacaoComentarioEspecialistaId = titulo.TituloId };
    //        }
    //        titulo.TituloInformacaoComentarioEspecialista.ArquivoAudio = new ArquivoBLL().CarregarArquivo(new Arquivo() { ArquivoId = this.upArquivoAudio.ArquivoId });
    //        tituloBLL.AtualizarTituloComentarioEspecialista(titulo);
    //    }
    //}

    void upArquivoImagem_DeleteItem(object sender, ArquivoEventArgs e)
    {
        TituloBLL tituloBLL = new TituloBLL();
        TituloImpresso tituloImpresso = tituloBLL.CarregarTituloImpressoComComentarioDoEspecialista(new TituloImpresso() { TituloImpressoId = Convert.ToInt32(IdRegistro) });
        Titulo titulo = tituloImpresso.Titulo;
        if (titulo.TituloInformacaoComentarioEspecialista != null && titulo.TituloInformacaoComentarioEspecialista.ArquivoImagem != null)
        {
            Arquivo arquivo = titulo.TituloInformacaoComentarioEspecialista.ArquivoImagem;
            //remove relacionamento
            titulo.TituloInformacaoComentarioEspecialista.ArquivoImagem = null;

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

            tituloBLL.AtualizarTituloComentarioEspecialista(titulo, categorias);

            DeletarArquivoComentarioEspecialista(arquivo);

            //atualiza ListFile
            this.upArquivoImagem.RegistroId = Convert.ToInt32(IdRegistro);
            this.upArquivoImagem.DataBind();
        }
    }

    //void upArquivoAudio_DeleteItem(object sender, ArquivoEventArgs e)
    //{
    //    TituloBLL tituloBLL = new TituloBLL();
    //    TituloImpresso tituloImpresso = tituloBLL.CarregarTituloImpressoComComentarioDoEspecialista(new TituloImpresso() { TituloImpressoId = Convert.ToInt32(IdRegistro) });
    //    Titulo titulo = tituloImpresso.Titulo;
    //    if (titulo.TituloInformacaoComentarioEspecialista != null && titulo.TituloInformacaoComentarioEspecialista.ArquivoAudio != null)
    //    {
    //        Arquivo arquivo = titulo.TituloInformacaoComentarioEspecialista.ArquivoAudio;
    //        //remove relacionamento
    //        titulo.TituloInformacaoComentarioEspecialista.ArquivoAudio = null;
    //        tituloBLL.AtualizarTituloComentarioEspecialista(titulo);

    //        //remove da tabela Arquivo
    //        new ArquivoBLL().ExcluirArquivo(arquivo);

    //        //apaga arquivo físico
    //        string pathFile = string.Concat(Server.MapPath(GrupoA_Resource.baseUrlUpload), "imagensClipping\\", arquivo.NomeArquivo);
    //        FileInfo info = new FileInfo(pathFile);
    //        if (info.Exists)
    //            info.Delete();

    //        //atualiza ListFile
    //        this.upArquivoAudio.RegistroId = Convert.ToInt32(IdRegistro);
    //        this.upArquivoAudio.DataBind();
    //    }
    //}

    protected void btnExecute_Click(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = false;
        if (Page.IsValid)
        {
            SaveComentarioEspecialista();
        }

    }

    protected void cvValidaHTML_ServerValidate(object source, ServerValidateEventArgs args)
    {
        //if (string.IsNullOrEmpty(this.HtmlComentarioEspecialista.Text.Trim()))
        //{
        //    cvValidaHTML.ErrorMessage = "Campo obrigatório.";
        //    args.IsValid = false;
        //}
    }

    protected void cvValidaResumo_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (this.txtResumoComentario.Text.Length > 352)
        {
            cvValidaResumo.ErrorMessage = "Campo maior que 352 caracteres.";
            args.IsValid = false;
        }
    }

    protected void cvValidaNome_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (this.txtNomeEspecialista.Text.Length > 30)
        {
            cvValidaNome.ErrorMessage = "Campo maior que 30 caracteres.";
            args.IsValid = false;
        }
    }

    protected void cvValidaEspecialidade_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (this.txtEspecialidade.Text.Length > 63)
        {
            cvValidaEspecialidade.ErrorMessage = "Campo maior que 63 caracteres.";
            args.IsValid = false;
        }
    }

    #endregion

    #region Métodos

    protected void LoadForm()
    {
        this.montaChecklistCategoria();

        if (IdRegistro > 0)
        {
            HtmlComentarioEspecialista.Width = 850;

            TituloImpresso tituloImpresso = new TituloBLL().CarregarTituloImpressoComComentarioDoEspecialista(new TituloImpresso() { TituloImpressoId = Convert.ToInt32(IdRegistro) });
            Titulo titulo = tituloImpresso.Titulo;
            this.ltrNomeTitulo.Text = titulo.NomeTitulo;
            this.ltrISBN13.Text = tituloImpresso.Isbn13;

            //sobre autores
            if (titulo.TituloInformacaoComentarioEspecialista != null && titulo.TituloInformacaoComentarioEspecialista.TituloInformacaoComentarioEspecialistaId > 0)
            {
                this.txtNomeEspecialista.Text = titulo.TituloInformacaoComentarioEspecialista.NomeEspecialista;
                this.txtEspecialidade.Text = titulo.TituloInformacaoComentarioEspecialista.Especialidade;
                this.txtResumoComentario.Text = titulo.TituloInformacaoComentarioEspecialista.ResumoComentario;
                this.HtmlComentarioEspecialista.Text = titulo.TituloInformacaoComentarioEspecialista.TextoComentario;

                this.txtUrlMidia.Text = titulo.TituloInformacaoComentarioEspecialista.UrlMidia;
                this.chkDestaque.Checked = titulo.TituloInformacaoComentarioEspecialista.DestaqueAreaConhecimento;
                //if (titulo.TituloInformacaoComentarioEspecialista.ArquivoAudio != null && titulo.TituloInformacaoComentarioEspecialista.ArquivoAudio.ArquivoId > 0)
                //{
                //    this.upArquivoAudio.ArquivoId = titulo.TituloInformacaoComentarioEspecialista.ArquivoAudio.ArquivoId;
                //}
                if (titulo.TituloInformacaoComentarioEspecialista.ArquivoImagem != null && titulo.TituloInformacaoComentarioEspecialista.ArquivoImagem.ArquivoId > 0)
                {
                    this.upArquivoImagem.ArquivoId = titulo.TituloInformacaoComentarioEspecialista.ArquivoImagem.ArquivoId;
                }

                // Busca as categorias e as seleciona
                IEnumerable<Categoria> categorias = new TituloBLL().CarregarCategoriasComentarioEspecialista(titulo.TituloInformacaoComentarioEspecialista.TituloInformacaoComentarioEspecialistaId);

                foreach (Categoria cat in categorias)
                {
                    for (int i = 0; i < cblCategorias.Items.Count; i++)
                    {
                        if (cblCategorias.Items[i].Value.Equals(cat.CategoriaId.ToString()))
                            cblCategorias.Items[i].Selected = true;
                    }
                }
            }
        }
    }

    protected void SaveComentarioEspecialista()
    {
        TituloBLL tituloBLL = new TituloBLL();
        TituloImpresso tituloImpresso = tituloBLL.CarregarTituloImpressoComComentarioDoEspecialista(new TituloImpresso() { TituloImpressoId = Convert.ToInt32(IdRegistro) });
        Titulo titulo = tituloImpresso.Titulo;

        if (titulo.TituloInformacaoComentarioEspecialista == null || titulo.TituloInformacaoComentarioEspecialista.TituloInformacaoComentarioEspecialistaId < 1)
        {
            titulo.TituloInformacaoComentarioEspecialista = new TituloInformacaoComentarioEspecialista();
        }

        titulo.TituloInformacaoComentarioEspecialista.NomeEspecialista = this.txtNomeEspecialista.Text;
        titulo.TituloInformacaoComentarioEspecialista.ResumoComentario = this.txtResumoComentario.Text;
        titulo.TituloInformacaoComentarioEspecialista.Especialidade = this.txtEspecialidade.Text;
        titulo.TituloInformacaoComentarioEspecialista.TextoComentario = this.HtmlComentarioEspecialista.Text;
        titulo.TituloInformacaoComentarioEspecialista.UrlMidia = this.txtUrlMidia.Text;
        titulo.TituloInformacaoComentarioEspecialista.DestaqueAreaConhecimento = this.chkDestaque.Checked;

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

        tituloBLL.AtualizarTituloComentarioEspecialista(titulo, categorias);
        Util.ShowInsertMessage();
    }

    private void DeletarArquivoComentarioEspecialista(Arquivo arquivo)
    {
        arquivo = new ArquivoBLL().CarregarArquivo(arquivo);

        //remove da tabela Arquivo
        new ArquivoBLL().ExcluirArquivo(arquivo);

        //apaga arquivo físico
        string pathFile = string.Concat(Server.MapPath(GrupoA_Resource.baseUrlUpload), GrupoA_Resource.PastaComentarioEspecialista, arquivo.NomeArquivo);
        FileInfo info = new FileInfo(pathFile);
        if (info.Exists)
            info.Delete();
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
}