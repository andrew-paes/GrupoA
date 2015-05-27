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

public partial class content_module_chamada_atualizacao : SmartUserControl
{

    #region Propriedades

    #region eventoBLL
    private EventoBLL _eventoBLL;
    private EventoBLL eventoBLL
    {
        get
        {
            if (_eventoBLL == null)
                _eventoBLL = new EventoBLL();
            return _eventoBLL;
        }
    }
    #endregion

    #region programaAtualizacaoChamadaBLL
    private ProgramaAtualizacaoChamadaBLL _programaAtualizacaoChamadaBLL;
    private ProgramaAtualizacaoChamadaBLL programaAtualizacaoChamadaBLL
    {
        get
        {
            if (_programaAtualizacaoChamadaBLL == null)
                _programaAtualizacaoChamadaBLL = new ProgramaAtualizacaoChamadaBLL();
            return _programaAtualizacaoChamadaBLL;
        }
    }

    #endregion

    #endregion

    #region Eventos

    void upArquivoImagem_BindList(object sender, ArquivoEventArgs e)
    {
        this.upArquivoImagem.ArquivoId = e.ArquivoId;
        this.upArquivoImagem.RegistroId = Convert.ToInt32(IdRegistro);

        //cria o relacionamento
        if (e.ArquivoId > 0)
        {
            ProgramaAtualizacaoChamada programaAtualizacaoChamada = new ProgramaAtualizacaoChamadaBLL().CarregarCompleto(new ProgramaAtualizacaoChamada() { ProgramaAtualizacaoChamadaId = Convert.ToInt32(IdRegistro) });
            Arquivo arquivoDeletar = null;
            if (programaAtualizacaoChamada.ArquivoImagem != null)
            {
                arquivoDeletar = programaAtualizacaoChamada.ArquivoImagem;
            }

            programaAtualizacaoChamada.ArquivoImagem = new ArquivoBLL().CarregarArquivo(new Arquivo() { ArquivoId = this.upArquivoImagem.ArquivoId });
            new ProgramaAtualizacaoChamadaBLL().AtualizarProgramaAtualizacaoChamada(programaAtualizacaoChamada);

            string imageFile = string.Concat(GrupoA_Resource.baseUrlUpload, GrupoA_Resource.PastaChamadaPrograma, programaAtualizacaoChamada.ArquivoImagem.NomeArquivo);
            string path = HttpContext.Current.Request.MapPath(imageFile);

            System.Drawing.Image resizedImage;

            using(System.Drawing.Image image = System.Drawing.Image.FromFile(path))
            {
                resizedImage = image.ResizeTo(96, 128, ResizeModes.Fit);
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
        ProgramaAtualizacaoChamada programaAtualizacaoChamada = new ProgramaAtualizacaoChamadaBLL().CarregarCompleto(new ProgramaAtualizacaoChamada() { ProgramaAtualizacaoChamadaId = Convert.ToInt32(IdRegistro) }); 

        if (programaAtualizacaoChamada != null && programaAtualizacaoChamada.ArquivoImagem != null)
        {
            Arquivo arquivo = programaAtualizacaoChamada.ArquivoImagem;
            //remove relacionamento
            programaAtualizacaoChamada.ArquivoImagem = null;
            new ProgramaAtualizacaoChamadaBLL().AtualizarProgramaAtualizacaoChamada(programaAtualizacaoChamada);

            arquivo = DeletarArquivo(arquivo);

            //atualiza ListFile
            this.upArquivoImagem.RegistroId = Convert.ToInt32(IdRegistro);
            this.upArquivoImagem.DataBind();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.upArquivoImagem.BindList += new EventHandler<ArquivoEventArgs>(upArquivoImagem_BindList);
        this.upArquivoImagem.DeleteItem += new EventHandler<ArquivoEventArgs>(upArquivoImagem_DeleteItem);
        this.upArquivoImagem.RegistroId = Convert.ToInt32(IdRegistro);

        if (IdRegistro > 0)
        {
            this.pnlArquivo.Visible = true;
            if (!IsPostBack)
            {

                this.CarregarNovaJanela();
                MontaChecklistPagina();
                this.LoadForm();

            }

        }
        else
        {
            if (!IsPostBack)
            {
                MontaChecklistPagina();
                this.CarregarNovaJanela();
            }
        }

    }

    protected void LoadForm()
    {
        if (IdRegistro > 0)
        {

            ProgramaAtualizacaoChamada programaAtualizacaoChamada = new ProgramaAtualizacaoChamadaBLL().CarregarCompleto(new ProgramaAtualizacaoChamada() { ProgramaAtualizacaoChamadaId = Convert.ToInt32(IdRegistro) });
            txtPrimeiraChamadaTitulo.Text = programaAtualizacaoChamada.PrimeiraChamadaTitulo;
            txtPrimeiraChamadaTexto.Text = programaAtualizacaoChamada.PrimeiraChamadaTexto;
            txtPrimeiraChamadaURL.Text = programaAtualizacaoChamada.PrimeiraChamadaUrl;
            this.ddlPrimeiraChamadaNovaJanela.SelectedIndex = programaAtualizacaoChamada.PrimeiraChamadaTargetBlank == true ? 1 : 2;
            
            //this.ddlSegundaChamadaNovaJanela.SelectedIndex = programaAtualizacaoChamada.SegundaChamadaTargetBlank == true ? 1 : 2;

            //txtSegundaChamadaTitulo.Text = programaAtualizacaoChamada.SegundaChamadaTitulo;
            //txtSegundaChamadaTexto.Text = programaAtualizacaoChamada.SegundaChamadaTexto;
            //txtSegundaChamadaURL.Text = programaAtualizacaoChamada.SegundaChamadaUrl;
            this.chkAtivo.Checked = programaAtualizacaoChamada.Ativo;
            if (programaAtualizacaoChamada.ArquivoImagem != null && programaAtualizacaoChamada.ArquivoImagem.ArquivoId > 0)
            {
                this.upArquivoImagem.ArquivoId = programaAtualizacaoChamada.ArquivoImagem.ArquivoId;
            }
            // marca as páginas que tem relacionamento
            foreach (ProgramaAtualizacaoPagina pagina in programaAtualizacaoChamada.ProgramaAtualizacaoPaginas)
            {
                for (int i = 0; i < cblPaginas.Items.Count; i++)
                {
                    if (cblPaginas.Items[i].Value.Equals(pagina.ProgramaAtualizacaoPaginaId.ToString()))
                        cblPaginas.Items[i].Selected = true;
                }
            }
            
            // Se o número de páginas for maior ou igual a 1 deve permitir ativar
            //areaOpcoes.Visible = true;



            

        }
    }

    protected void btnExecute_Click(object sender, ImageClickEventArgs e)
    {
        saveOrUpdate();
    }

    protected void dgGrid_ItemCreated(Object Sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ImageButton _myButton = (ImageButton)e.Item.FindControl("btnDelete");
            _myButton.Attributes.Add("onclick", "return confirm('Deseja realmente excluir o item selecionado?');");
        }
    }

    #endregion

    #region Métodos

    protected void MontaChecklistPagina()
    {
        cblPaginas.DataSource = programaAtualizacaoChamadaBLL.CarregarPaginas();
        cblPaginas.DataValueField = "programaAtualizacaoPaginaId";
        cblPaginas.DataTextField = "pagina";
        cblPaginas.DataBind();
    }

    public void saveOrUpdate()
    {

        if (Page.IsValid)
        {
            
            ProgramaAtualizacaoChamada programaAtualizacaoChamada = new ProgramaAtualizacaoChamada();
            programaAtualizacaoChamada.ProgramaAtualizacaoChamadaId = Convert.ToInt32(IdRegistro.Value);
            programaAtualizacaoChamada.PrimeiraChamadaTitulo = txtPrimeiraChamadaTitulo.Text;
            programaAtualizacaoChamada.PrimeiraChamadaTexto = txtPrimeiraChamadaTexto.Text;
            programaAtualizacaoChamada.PrimeiraChamadaUrl = txtPrimeiraChamadaURL.Text;
            programaAtualizacaoChamada.PrimeiraChamadaTargetBlank = ddlPrimeiraChamadaNovaJanela.SelectedItem.Value == "Sim" ? true : false;
            //programaAtualizacaoChamada.SegundaChamadaTitulo = txtSegundaChamadaTitulo.Text;
            //programaAtualizacaoChamada.SegundaChamadaTexto = txtSegundaChamadaTexto.Text;
            //programaAtualizacaoChamada.SegundaChamadaUrl = txtSegundaChamadaURL.Text;
            //programaAtualizacaoChamada.SegundaChamadaTargetBlank = ddlSegundaChamadaNovaJanela.SelectedItem.Value == "Sim" ? true : false;
            programaAtualizacaoChamada.SegundaChamadaTitulo = null;
            programaAtualizacaoChamada.SegundaChamadaTexto = null;
            programaAtualizacaoChamada.SegundaChamadaUrl = null;
            programaAtualizacaoChamada.SegundaChamadaTargetBlank = (Nullable<bool>)null;
            programaAtualizacaoChamada.Ativo = this.chkAtivo.Checked;

            // Popula Páginas referentes a Enquete
            List<ProgramaAtualizacaoPagina> programaAtualizacaoPaginas = new List<ProgramaAtualizacaoPagina>();
            foreach (ListItem li in cblPaginas.Items)
            {
                if (li.Selected)
                {
                    ProgramaAtualizacaoPagina ep = new ProgramaAtualizacaoPagina();
                    ep.ProgramaAtualizacaoPaginaId = Convert.ToInt32(li.Value);
                    programaAtualizacaoPaginas.Add(ep);
                }
            }
            programaAtualizacaoChamada.ProgramaAtualizacaoPaginas = programaAtualizacaoPaginas;

            if (IdRegistro.Value == 0)
            {
                programaAtualizacaoChamadaBLL.InserirProgramaAtualizacaoChamada(programaAtualizacaoChamada);
                Util.ShowInsertMessage();
                //pnlProgramaAtualizacaoChamada.Visible = true;
            }
            else
            {
                programaAtualizacaoChamada.ArquivoImagem = new ProgramaAtualizacaoChamadaBLL().CarregarCompleto(new ProgramaAtualizacaoChamada() { ProgramaAtualizacaoChamadaId = Convert.ToInt32(IdRegistro) }).ArquivoImagem;
                programaAtualizacaoChamadaBLL.AtualizarProgramaAtualizacaoChamada(programaAtualizacaoChamada);
                // Envia atualização à todos os usuários que contém alerta desse evento
                Util.ShowUpdateMessage();

            }
        }
    }

    protected void cvValidarAtivacao_ServerValidate(object source, ServerValidateEventArgs args)
    {
        //try
        //{

        //    if (chkAtivo.Checked)
        //    {
        //        // Condições:
        //        // 1. Ter ao menos uma página
        //        // 2. Ter ao menos 2 opções
        //        int totalOpcoes = dgGrid.Items.Count;
        //        int totalPaginas = 0;
        //        foreach (ListItem item in cblPaginas.Items)
        //            if (item.Selected)
        //                totalPaginas++;
        //        if ((totalOpcoes > 1) && (totalPaginas > 0))
        //            args.IsValid = true;

        //        if (totalOpcoes < 2)
        //            cvValidarAtivacao.ErrorMessage = "É necessário ter ao menos 2 opções para ativar a Chamada";
        //        else
        //            cvValidarAtivacao.ErrorMessage = string.Empty;
        //        if (totalPaginas < 1)
        //            cvPaginas.ErrorMessage = "É necessário ter ao menos 1 página para ativar a Chamada";
        //        else
        //            cvPaginas.ErrorMessage = string.Empty;
        //        args.IsValid = false;
        //    }
        //    else
        //        args.IsValid = true;

        //}
        //catch
        //{
        //    cvValidarAtivacao.ErrorMessage = "Erro ao ativar enquete!";
        //    args.IsValid = false;
        //}
    }

    private void CarregarNovaJanela()
    {
        ddlPrimeiraChamadaNovaJanela.Items.Insert(0, ":: Selecione ::");
        ddlPrimeiraChamadaNovaJanela.Items.Insert(1, "Sim");
        ddlPrimeiraChamadaNovaJanela.Items.Insert(2, "Não");
        
        //ddlSegundaChamadaNovaJanela.Items.Insert(0, ":: Selecione ::");
        //ddlSegundaChamadaNovaJanela.Items.Insert(1, "Sim");
        //ddlSegundaChamadaNovaJanela.Items.Insert(2, "Não");

    }

    private Arquivo DeletarArquivo(Arquivo arquivo)
    {
        arquivo = new ArquivoBLL().CarregarArquivo(arquivo);

        //remove da tabela Arquivo
        new ArquivoBLL().ExcluirArquivo(arquivo);

        //apaga arquivo físico
        string pathFile = string.Concat(Server.MapPath(GrupoA_Resource.baseUrlUpload), GrupoA_Resource.PastaChamadaPrograma, arquivo.NomeArquivo);
        FileInfo info = new FileInfo(pathFile);
        if (info.Exists)
            info.Delete();
        return arquivo;
    }

    #endregion

}
