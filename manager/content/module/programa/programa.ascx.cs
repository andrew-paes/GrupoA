using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using System.Configuration;
using System.IO;
using GrupoA.GlobalResources;

public partial class content_module_programa : SmartUserControl
{

    #region Propriedades
    #region cursoPanamericanoBLL
    private CursoPanamericanoBLL _cursoPanamericanoBLL;
    private CursoPanamericanoBLL cursoPanamericanoBLL
    {
        get
        {
            if (_cursoPanamericanoBLL == null)
                _cursoPanamericanoBLL = new CursoPanamericanoBLL();
            return _cursoPanamericanoBLL;
        }
    }
    #endregion

    #endregion

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        ListFiles1.BindList += new EventHandler<ArquivoEventArgs>(ListFiles1_BindList);
        ListFiles1.DeleteItem += new EventHandler<ArquivoEventArgs>(ListFiles1_DeleteItem);

        if (IdRegistro > 0)
        {
            if (!IsPostBack)
            {
                this.hddCursoPanamericanoId.Value = IdRegistro.ToString();
                this.MontaChecklistPagina();
                this.LoadForm();
            }

            ListFiles1.RegistroId = Util.GetRequestId();
        }
    }

    protected void btnExecute_Click(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = false;
        SaveOrUpdate();
    }

    #endregion

    #region Métodos

    private void LoadForm()
    {
        if (IdRegistro > 0)
        {
            this.CarregarTela(IdRegistro.Value);
        }
    }

    private void CarregarTela(int id)
    {
        var cursoPanamericano = new CursoPanamericanoBLL().Carregar(new CursoPanamericano() { CursoPanamericanoId = id });

        cursoPanamericano.Categorias = cursoPanamericanoBLL.CarregarCategoriasDoCursoPanamericano(id);

        hddCursoPanamericanoId.Value = cursoPanamericano.CursoPanamericanoId.ToString();

        if (cursoPanamericano.ArquivoImagem != null)
        {
            var arquivo = new CursoPanamericanoBLL().CarregarArquivo(new Arquivo() { ArquivoId = cursoPanamericano.ArquivoImagem.ArquivoId });
            hddCursoPanamericanoArquivoId.Value = arquivo.ArquivoId.ToString();
            hddCursoPanamericanoArquivoNome.Value = arquivo.NomeArquivo.ToString();
            ListFiles1.CarregaArquivo(arquivo.ArquivoId);
        }

        this.txtTituloCurso.Text = cursoPanamericano.Titulo;
        this.txtSubTituloCurso.Text = cursoPanamericano.Subtitulo;
        this.txtDescricao.Text = cursoPanamericano.Descricao;
        this.txtFonteUrl.Text = cursoPanamericano.UrlLink;

        //chkAtivo.Checked = cursoPanamericano.Ativo;
        chkTargetBlank.Checked = cursoPanamericano.TargetBlank;

        lblArquivoCurso.Visible = true;

        foreach (Categoria categoriaItem in cursoPanamericano.Categorias)
        {
            for (int i = 0; i < cblCategorias.Items.Count; i++)
            {
                if (cblCategorias.Items[i].Value.Equals(categoriaItem.CategoriaId.ToString()))
                {
                    cblCategorias.Items[i].Selected = true;
                }
            }
        }
    }

    void ListFiles1_DeleteItem(object sender, ArquivoEventArgs e)
    {
        hddCursoPanamericanoArquivoIdDelete.Value = hddCursoPanamericanoArquivoId.Value;
        hddCursoPanamericanoArquivoId.Value = "0";
        SaveOrUpdate();
        ListFiles1.DataBind();
    }

    void ListFiles1_BindList(object sender, ArquivoEventArgs e)
    {
        var arquivoId = Convert.ToInt32(hddCursoPanamericanoArquivoId.Value);

        if (!e.ArquivoId.ToString().Equals(hddCursoPanamericanoArquivoId.Value))
            hddCursoPanamericanoArquivoIdDelete.Value = hddCursoPanamericanoArquivoId.Value;

        hddCursoPanamericanoArquivoId.Value = e.ArquivoId.ToString();

        SaveOrUpdate();

        Arquivo arquivo = new ArquivoBLL().CarregarArquivo(new Arquivo() { ArquivoId = Convert.ToInt32(hddCursoPanamericanoArquivoId.Value) });

        string imageFile = string.Concat(GrupoA_Resource.baseUrlUpload, GrupoA_Resource.PastaImagensCursoPanamericano, arquivo.NomeArquivo);
        string path = HttpContext.Current.Request.MapPath(imageFile);

        System.Drawing.Image resizedImage;

        using (System.Drawing.Image image = System.Drawing.Image.FromFile(path))
        {
            resizedImage = image.ResizeTo(96, 128, ResizeModes.Fit);
        }

        resizedImage.StreamSave(path);
        resizedImage.Dispose();
    }

    protected void SaveOrUpdate()
    {
        if (Page.IsValid)
        {
            var cursoPanamericano = new CursoPanamericano();
            cursoPanamericano.CursoPanamericanoId = Convert.ToInt32(hddCursoPanamericanoId.Value);

            cursoPanamericano.Titulo = this.txtTituloCurso.Text.Trim();
            cursoPanamericano.Subtitulo = this.txtSubTituloCurso.Text.Trim();
            cursoPanamericano.Descricao = this.txtDescricao.Text.Trim();
            cursoPanamericano.UrlLink = this.txtFonteUrl.Text.Trim();
            cursoPanamericano.TargetBlank = chkTargetBlank.Checked;

            List<Categoria> categoria = new List<Categoria>();
            foreach (ListItem li in cblCategorias.Items)
            {
                if (li.Selected)
                {
                    Categoria ep = new Categoria();
                    ep.CategoriaId = Convert.ToInt32(li.Value);
                    categoria.Add(ep);
                }
            }

            cursoPanamericano.Categorias = categoria;

            if (Convert.ToInt32(hddCursoPanamericanoArquivoId.Value) > 0)
                cursoPanamericano.ArquivoImagem = new Arquivo() { ArquivoId = Convert.ToInt32(hddCursoPanamericanoArquivoId.Value) };

            if (cursoPanamericano.CursoPanamericanoId > 0)
            {
                new CursoPanamericanoBLL().Atualizar(cursoPanamericano);
                if (cursoPanamericano.ArquivoImagem != null)
                {
                    hddCursoPanamericanoArquivoId.Value = cursoPanamericano.ArquivoImagem.ArquivoId.ToString();
                    hddCursoPanamericanoArquivoNome.Value = cursoPanamericano.ArquivoImagem.NomeArquivo;
                }

                Util.ShowUpdateMessage();
            }
            else
            {
                cursoPanamericano = new CursoPanamericanoBLL().Inserir(cursoPanamericano);
                hddCursoPanamericanoId.Value = cursoPanamericano.CursoPanamericanoId.ToString();

                this.MontaChecklistPagina();

                ListFiles1.RegistroId = cursoPanamericano.CursoPanamericanoId;

                this.CarregarTela(cursoPanamericano.CursoPanamericanoId);


                Util.ShowInsertMessage();
            }

            if (hddCursoPanamericanoArquivoIdDelete.Value != "0")
            {
                DeletarArquivo();
            }
        }
    }

    private void DeletarArquivo()
    {
        //Deleta arquivo anterior
        var arquivoDel = new ArquivoBLL().CarregarArquivo(new Arquivo() { ArquivoId = Convert.ToInt32(hddCursoPanamericanoArquivoIdDelete.Value) });
        if (arquivoDel != null)
        {
            string pathFileDelete = string.Concat(Server.MapPath(GrupoA_Resource.baseUrlUpload), ListFiles1.TargetFolder, arquivoDel.NomeArquivo);
            FileInfo infoDelete = new FileInfo(pathFileDelete);
            if (infoDelete.Exists)
            {
                infoDelete.Delete();
                new ArquivoBLL().ExcluirArquivo(arquivoDel);
            }
        }
    }

    protected void MontaChecklistPagina()
    {
        this.lblCategorias.Visible = true;

        cblCategorias.DataSource = cursoPanamericanoBLL.CarregarTodasCategoriasFilhas();
        cblCategorias.DataValueField = "categoriaId";
        cblCategorias.DataTextField = "nomeCategoria";
        cblCategorias.DataBind();
    }

    #endregion

}