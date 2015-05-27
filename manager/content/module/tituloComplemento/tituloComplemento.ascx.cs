using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using GrupoA.GlobalResources;

public partial class content_module_tituloComplemento_tituloComplemento : SmartUserControl
{
    #region Eventos

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //upArquivoSumario.BindList += new EventHandler<ArquivoEventArgs>(upArquivoSumario_BindList);
        //upArquivoSumario.DeleteItem += new EventHandler<ArquivoEventArgs>(upArquivoSumario_DeleteItem);
        //upArquivoSumario.RegistroId = Convert.ToInt32(IdRegistro);

        upArquivoAudioAutor.BindList += new EventHandler<ArquivoEventArgs>(upArquivoAudioAutor_BindList);
        upArquivoAudioAutor.DeleteItem += new EventHandler<ArquivoEventArgs>(upArquivoAudioAutor_DeleteItem);
        upArquivoAudioAutor.RegistroId = Convert.ToInt32(IdRegistro);

        upArquivoTag.BindList += new EventHandler<ArquivoEventArgs>(upArquivoTag_BindList);
        upArquivoTag.DeleteItem += new EventHandler<ArquivoEventArgs>(upArquivoTag_DeleteItem);
        upArquivoTag.RegistroId = Convert.ToInt32(IdRegistro);

        if (!IsPostBack)
        {
            LoadForm();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void upArquivoSumario_BindList(object sender, ArquivoEventArgs e)
    {
        //upArquivoSumario.ArquivoId = e.ArquivoId;
        //upArquivoSumario.RegistroId = Convert.ToInt32(IdRegistro);

        //cria o relacionamento
        if (e.ArquivoId > 0)
        {
            TituloBLL tituloBLL = new TituloBLL();
            TituloImpresso tituloImpresso = tituloBLL.CarregarInformacoesComplementares(new TituloImpresso() { TituloImpressoId = Convert.ToInt32(IdRegistro) });
            Titulo titulo = tituloImpresso.Titulo;
            Arquivo arquivoDeletar = null;
            if (titulo.TituloInformacaoSumario == null || titulo.TituloInformacaoSumario.TituloInformacaoSumarioId < 1)
            {
                //titulo.TituloInformacaoSumario = new TituloInformacaoSumario() { TituloInformacaoSumarioId = titulo.TituloId };
                titulo.TituloInformacaoSumario = new TituloInformacaoSumario();
            }
            else
            {
                if (titulo.TituloInformacaoSumario.ArquivoSumario != null)
                {
                    arquivoDeletar = titulo.TituloInformacaoSumario.ArquivoSumario;
                }
            }
            //titulo.TituloInformacaoSumario.ArquivoSumario = new ArquivoBLL().CarregarArquivo(new Arquivo() { ArquivoId = upArquivoSumario.ArquivoId });

            tituloBLL.AtualizarTituloInformacoesComplementares(titulo);

            if (arquivoDeletar != null)
            {
                DeletarArquivoSumario(arquivoDeletar);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void upArquivoAudioAutor_BindList(object sender, ArquivoEventArgs e)
    {
        upArquivoAudioAutor.ArquivoId = e.ArquivoId;
        upArquivoAudioAutor.RegistroId = Convert.ToInt32(IdRegistro);
        Arquivo arquivoDeletar = null;

        //cria o relacionamento
        if (e.ArquivoId > 0)
        {
            TituloBLL tituloBLL = new TituloBLL();
            TituloImpresso tituloImpresso = tituloBLL.CarregarInformacoesComplementares(new TituloImpresso() { TituloImpressoId = Convert.ToInt32(IdRegistro) });
            Titulo titulo = tituloImpresso.Titulo;
            if (titulo.TituloInformacaoSobreAutor == null || titulo.TituloInformacaoSobreAutor.TituloInformacaoSobreAutorId < 1)
            {
                //titulo.TituloInformacaoSobreAutor = new TituloInformacaoSobreAutor() { TituloInformacaoSobreAutorId = titulo.TituloId };
                titulo.TituloInformacaoSobreAutor = new TituloInformacaoSobreAutor();
            }
            else
            {
                if (titulo.TituloInformacaoSobreAutor.ArquivoImagem != null)
                {
                    arquivoDeletar = titulo.TituloInformacaoSobreAutor.ArquivoImagem;
                }
            }

            titulo.TituloInformacaoSobreAutor.ArquivoImagem = new ArquivoBLL().CarregarArquivo(new Arquivo() { ArquivoId = upArquivoAudioAutor.ArquivoId });

            tituloBLL.AtualizarTituloInformacoesComplementares(titulo);

            if (arquivoDeletar != null)
            {
                DeletarArquivoSobreAutor(arquivoDeletar);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void upArquivoTag_BindList(object sender, ArquivoEventArgs e)
    {
        upArquivoTag.ArquivoId = e.ArquivoId;
        upArquivoTag.RegistroId = Convert.ToInt32(IdRegistro);

        // Cria o relacionamento
        if (e.ArquivoId > 0)
        {
            TituloImagemResumo tituloImagemResumoBO = new TituloImagemResumo();
            Arquivo arquivoDeletar = new Arquivo();

            TituloImpresso tituloImpressoBO = new TituloBLL().CarregarInformacoesComplementares(new TituloImpresso() { TituloImpressoId = Convert.ToInt32(IdRegistro) });

            Titulo tituloBO = new Titulo();
            tituloBO = tituloImpressoBO.Titulo;

            List<TituloImagemResumo> tituloImagemResumoBOList = new List<TituloImagemResumo>();
            tituloImagemResumoBOList = new TituloImagemResumoBLL().Carregar(tituloBO);

            if (tituloImagemResumoBOList != null && tituloImagemResumoBOList.Any())
            {
                // Carregar arquivoDeletar
                if (tituloImagemResumoBOList[0].Arquivo != null && tituloImagemResumoBOList[0].Arquivo.ArquivoId > 0)
                {
                    arquivoDeletar.ArquivoId = tituloImagemResumoBOList[0].Arquivo.ArquivoId;
                    arquivoDeletar = new ArquivoBLL().CarregarArquivo(arquivoDeletar);
                }

                // Excluir relacionamento
                tituloImagemResumoBO = tituloImagemResumoBOList[0];

                if (tituloImagemResumoBO != null && tituloImagemResumoBO.TituloImagemResumoId > 0)
                {
                    new TituloImagemResumoBLL().Excluir(tituloImagemResumoBO);
                }

                // Excluir arquivoDeletar
                if (arquivoDeletar != null && arquivoDeletar.ArquivoId > 0)
                {
                    new ArquivoBLL().ExcluirArquivo(arquivoDeletar); // Remove da tabela Arquivo

                    string pathFile = string.Concat(Server.MapPath(GrupoA_Resource.baseUrlUpload), GrupoA_Resource.PastaImagensTitulo, arquivoDeletar.NomeArquivo);
                    FileInfo fileInfo = new FileInfo(pathFile);

                    if (fileInfo.Exists)
                    {
                        fileInfo.Delete(); // Apaga arquivo físico
                    }
                }
            }

            // Criar relacionamento
            tituloImagemResumoBO = new TituloImagemResumo();
            tituloImagemResumoBO.Arquivo = new Arquivo();
            tituloImagemResumoBO.Arquivo.ArquivoId = e.ArquivoId;
            tituloImagemResumoBO.Titulo = new Titulo();
            tituloImagemResumoBO.Titulo = tituloBO;

            tituloImagemResumoBO = new TituloImagemResumoBLL().Inserir(tituloImagemResumoBO);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void upArquivoSumario_DeleteItem(object sender, ArquivoEventArgs e)
    {
        TituloBLL tituloBLL = new TituloBLL();
        TituloImpresso tituloImpresso = tituloBLL.CarregarInformacoesComplementares(new TituloImpresso() { TituloImpressoId = Convert.ToInt32(IdRegistro) });
        Titulo titulo = tituloImpresso.Titulo;
        if (titulo.TituloInformacaoSumario != null && titulo.TituloInformacaoSumario.ArquivoSumario != null)
        {
            Arquivo arquivo = titulo.TituloInformacaoSumario.ArquivoSumario;
            //remove relacionamento
            titulo.TituloInformacaoSumario.ArquivoSumario = null;
            tituloBLL.AtualizarTituloInformacoesComplementares(titulo);

            DeletarArquivoSumario(arquivo);

            //atualiza ListFile
            //upArquivoSumario.RegistroId = Convert.ToInt32(IdRegistro);
            //upArquivoSumario.DataBind();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void upArquivoAudioAutor_DeleteItem(object sender, ArquivoEventArgs e)
    {
        TituloBLL tituloBLL = new TituloBLL();
        TituloImpresso tituloImpresso = tituloBLL.CarregarInformacoesComplementares(new TituloImpresso() { TituloImpressoId = Convert.ToInt32(IdRegistro) });
        Titulo titulo = tituloImpresso.Titulo;
        if (titulo.TituloInformacaoSobreAutor != null && titulo.TituloInformacaoSobreAutor.ArquivoImagem != null)
        {
            Arquivo arquivo = titulo.TituloInformacaoSobreAutor.ArquivoImagem;
            //remove relacionamento
            titulo.TituloInformacaoSobreAutor.ArquivoImagem = null;
            tituloBLL.AtualizarTituloInformacoesComplementares(titulo);

            DeletarArquivoSobreAutor(arquivo);

            //atualiza ListFile
            upArquivoAudioAutor.RegistroId = Convert.ToInt32(IdRegistro);
            upArquivoAudioAutor.DataBind();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void upArquivoTag_DeleteItem(object sender, ArquivoEventArgs e)
    {
        TituloImagemResumo tituloImagemResumoBO = new TituloImagemResumo();
        Arquivo arquivoDeletar = new Arquivo();

        TituloImpresso tituloImpressoBO = new TituloBLL().CarregarInformacoesComplementares(new TituloImpresso() { TituloImpressoId = Convert.ToInt32(IdRegistro) });

        Titulo tituloBO = new Titulo();
        tituloBO = tituloImpressoBO.Titulo;

        List<TituloImagemResumo> tituloImagemResumoBOList = new List<TituloImagemResumo>();
        tituloImagemResumoBOList = new TituloImagemResumoBLL().Carregar(tituloBO);

        if (tituloImagemResumoBOList != null && tituloImagemResumoBOList.Any())
        {
            // Carregar arquivoDeletar
            if (tituloImagemResumoBOList[0].Arquivo != null && tituloImagemResumoBOList[0].Arquivo.ArquivoId > 0)
            {
                arquivoDeletar.ArquivoId = tituloImagemResumoBOList[0].Arquivo.ArquivoId;
                arquivoDeletar = new ArquivoBLL().CarregarArquivo(arquivoDeletar);
            }

            // Excluir relacionamento
            tituloImagemResumoBO = tituloImagemResumoBOList[0];

            if (tituloImagemResumoBO != null && tituloImagemResumoBO.TituloImagemResumoId > 0)
            {
                new TituloImagemResumoBLL().Excluir(tituloImagemResumoBO);
            }

            // Excluir arquivoDeletar
            if (arquivoDeletar != null && arquivoDeletar.ArquivoId > 0)
            {
                new ArquivoBLL().ExcluirArquivo(arquivoDeletar); // Remove da tabela Arquivo

                string pathFile = string.Concat(Server.MapPath(GrupoA_Resource.baseUrlUpload), GrupoA_Resource.PastaImagensTitulo, arquivoDeletar.NomeArquivo);
                FileInfo fileInfo = new FileInfo(pathFile);

                if (fileInfo.Exists)
                {
                    fileInfo.Delete(); // Apaga arquivo físico
                }
            }

            upArquivoTag.RegistroId = Convert.ToInt32(IdRegistro);
            upArquivoTag.DataBind();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExecute_Click(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = false;
        SaveInformacoesComplementares();
    }

    #endregion

    #region Métodos

    /// <summary>
    /// 
    /// </summary>
    protected void LoadForm()
    {
        if (IdRegistro > 0)
        {
            TituloImpresso tituloImpresso = new TituloBLL().CarregarInformacoesComplementares(new TituloImpresso() { TituloImpressoId = Convert.ToInt32(IdRegistro) });
            Titulo titulo = tituloImpresso.Titulo;
            ltrNomeTitulo.Text = titulo.NomeTitulo;
            ltrISBN13.Text = tituloImpresso.Isbn13;

            //sobre autores
            if (titulo.TituloInformacaoSobreAutor != null && titulo.TituloInformacaoSobreAutor.TituloInformacaoSobreAutorId > 0)
            {
                htmlTextoAutor.Text = titulo.TituloInformacaoSobreAutor.TextoAutor;
                if (titulo.TituloInformacaoSobreAutor.ArquivoImagem != null && titulo.TituloInformacaoSobreAutor.ArquivoImagem.ArquivoId > 0)
                {
                    upArquivoAudioAutor.ArquivoId = titulo.TituloInformacaoSobreAutor.ArquivoImagem.ArquivoId;
                }
            }

            //sobre resumo
            if (titulo.TituloInformacaoResumo != null && titulo.TituloInformacaoResumo.TituloInformacaoResumoId > 0)
            {
                htmlTextoResumo.Text = titulo.TituloInformacaoResumo.TextoResumo;
            }

            if (titulo.TituloImagemResumos != null && titulo.TituloImagemResumos.Count > 0)
            {
                if (titulo.TituloImagemResumos[0] != null && titulo.TituloImagemResumos[0].Arquivo != null && titulo.TituloImagemResumos[0].Arquivo.ArquivoId > 0)
                {
                    upArquivoTag.ArquivoId = titulo.TituloImagemResumos[0].Arquivo.ArquivoId;
                }
            }

            //sobre sumário
            if (titulo.TituloInformacaoSumario != null && titulo.TituloInformacaoSumario.TituloInformacaoSumarioId > 0)
            {
                HtmlTextoSumario.Text = titulo.TituloInformacaoSumario.TextoSumario;
                if (titulo.TituloInformacaoSumario.ArquivoSumario != null && titulo.TituloInformacaoSumario.ArquivoSumario.ArquivoId > 0)
                {
                    //upArquivoSumario.RegistroId = titulo.TituloInformacaoSumario.ArquivoSumario.ArquivoId;
                    //upArquivoSumario.CarregaArquivo(titulo.TituloInformacaoSumario.ArquivoSumario.ArquivoId);
                }
            }

            //sobre material didático
            if (titulo.TituloInformacaoMaterialDidatico != null && titulo.TituloInformacaoMaterialDidatico.TituloInformacaoMaterialDidaticoId > 0)
            {
                htmlTextoMaterial.Text = titulo.TituloInformacaoMaterialDidatico.TextoMaterial;
            }

            //HOMOLOGADO
            tituloImpresso.Produto = new ProdutoBLL().Carregar(tituloImpresso.Produto);
            if (tituloImpresso.Produto != null && tituloImpresso.Produto.ProdutoId > 0)
            {
                chkHomologado.Checked = tituloImpresso.Produto.Homologado;
                chkDisponivel.Checked = tituloImpresso.Produto.Disponivel;
                chkExibirSite.Checked = tituloImpresso.Produto.ExibirSite;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    protected void SaveInformacoesComplementares()
    {
        TituloBLL tituloBLL = new TituloBLL();
        TituloImpresso tituloImpresso = tituloBLL.CarregarInformacoesComplementares(new TituloImpresso() { TituloImpressoId = Convert.ToInt32(IdRegistro) });
        Titulo titulo = tituloImpresso.Titulo;
        var produtoBLL = new ProdutoBLL();

        //HOMOLOGADO
        tituloImpresso.Produto = produtoBLL.Carregar(tituloImpresso.Produto);

        if (tituloImpresso.Produto != null && tituloImpresso.Produto.ProdutoId > 0)
        {
            tituloImpresso.Produto.Homologado = chkHomologado.Checked;
            new ProdutoBLL().AtualizaHomologado(tituloImpresso.Produto);
        }

        //SOBRE AUTORES
        //if (!string.IsNullOrEmpty(htmlTextoAutor.Text.Trim()))
        {
            if (titulo.TituloInformacaoSobreAutor == null || titulo.TituloInformacaoSobreAutor.TituloInformacaoSobreAutorId < 1)
            {
                titulo.TituloInformacaoSobreAutor = new TituloInformacaoSobreAutor();
            }
            titulo.TituloInformacaoSobreAutor.TextoAutor = htmlTextoAutor.Text.Trim();
            titulo.TituloInformacaoSobreAutor.UrlMidia = string.Empty; //campo não é mais utilizado
        }

        //SOBRE RESUMO
        //if (!string.IsNullOrEmpty(htmlTextoResumo.Text.Trim()))
        {
            if (titulo.TituloInformacaoResumo == null || titulo.TituloInformacaoResumo.TituloInformacaoResumoId < 1)
            {
                titulo.TituloInformacaoResumo = new TituloInformacaoResumo();
            }
            titulo.TituloInformacaoResumo.TextoResumo = htmlTextoResumo.Text.Trim();
        }

        //SOBRE SUMÁRIO
        //if (!string.IsNullOrEmpty(HtmlTextoSumario.Text.Trim()))
        {
            if (titulo.TituloInformacaoSumario == null || titulo.TituloInformacaoSumario.TituloInformacaoSumarioId < 1)
            {
                titulo.TituloInformacaoSumario = new TituloInformacaoSumario();
            }
            titulo.TituloInformacaoSumario.TextoSumario = HtmlTextoSumario.Text.Trim();
        }

        //SOBRE MATERIAL DIDÁTICO
        //if (!string.IsNullOrEmpty(htmlTextoMaterial.Text.Trim()))
        {
            if (titulo.TituloInformacaoMaterialDidatico == null || titulo.TituloInformacaoMaterialDidatico.TituloInformacaoMaterialDidaticoId < 1)
            {
                titulo.TituloInformacaoMaterialDidatico = new TituloInformacaoMaterialDidatico();
            }
            titulo.TituloInformacaoMaterialDidatico.TextoMaterial = htmlTextoMaterial.Text.Trim();
        }

        tituloBLL.AtualizarTituloInformacoesComplementares(titulo);
        Util.ShowInsertMessage();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="arquivo"></param>
    private void DeletarArquivoSumario(Arquivo arquivo)
    {
        //remove da tabela Arquivo
        new ArquivoBLL().ExcluirArquivo(arquivo);

        //apaga arquivo físico
        string pathFile = string.Concat(Server.MapPath(GrupoA_Resource.baseUrlUpload), GrupoA_Resource.PastaImagensTitulo, arquivo.NomeArquivo);
        FileInfo info = new FileInfo(pathFile);
        if (info.Exists)
        {
            info.Delete();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="arquivo"></param>
    private void DeletarArquivoSobreAutor(Arquivo arquivo)
    {
        //remove da tabela Arquivo
        new ArquivoBLL().ExcluirArquivo(arquivo);

        //apaga arquivo físico
        string pathFile = string.Concat(Server.MapPath(GrupoA_Resource.baseUrlUpload), GrupoA_Resource.PastaImagensAutor, arquivo.NomeArquivo);
        FileInfo info = new FileInfo(pathFile);
        if (info.Exists)
        {
            info.Delete();
        }
    }

    #endregion
}