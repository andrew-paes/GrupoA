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


public partial class content_module_clipping_clipping : SmartUserControl
{
	#region Eventos

	/// <summary>
	/// 
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void Page_Load(object sender, EventArgs e)
	{
		this.LoadPage();
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	void ListFiles1_BindList(object sender, ArquivoEventArgs e)
	{
		this.CarregarImagensClipping(e);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	void ListFiles1_DeleteItem(object sender, ArquivoEventArgs e)
	{
		this.ExcluirImagensClipping(e);
		this.AtualizaListFile();
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void btnExecute_Click(object sender, EventArgs e)
	{
		this.ExecuteClick();
	}

	#region Validações

	/// <summary>
	/// 
	/// </summary>
	/// <param name="source"></param>
	/// <param name="args"></param>
	protected void cvValidarDatasPublicacao_ServerValidate(object source, ServerValidateEventArgs args)
	{
		if (!txtDataExibicaoInicio.Text.Equals(string.Empty) && !txtDataExibicaoFim.Text.Equals(string.Empty))
		{
			DateTime dtIni = DateTime.Parse(txtDataExibicaoInicio.Text);
			DateTime dtFim = DateTime.Parse(txtDataExibicaoFim.Text);

			if (DateTime.Compare(dtFim, dtIni) > 0)
			{
				args.IsValid = true;
			}
			else
			{
				this.cvValidarDatasPublicacao.ErrorMessage = "Data final deve ser maior que data inicial.";
				args.IsValid = false;
			}
		}
		else
		{
			args.IsValid = true;
		}
	}

	#endregion

	#endregion

	#region Métodos

	/// <summary>
	/// 
	/// </summary>
	private void LoadPage()
	{
		if (IdRegistro > 0)
		{
			if (!IsPostBack)
			{
				this.hddClippingId.Value = IdRegistro.ToString();
                montaChecklistCategoria();
				this.LoadForm();
			}
		}
        else
        {
            if (!IsPostBack)
            {
                montaChecklistCategoria();
            }
        }

		this.BindListFile();
	}

	/// <summary>
	/// 
	/// </summary>
	private void BindListFile()
	{
		this.ListFiles1.BindList += new EventHandler<ArquivoEventArgs>(ListFiles1_BindList);
		this.ListFiles1.DeleteItem += new EventHandler<ArquivoEventArgs>(ListFiles1_DeleteItem);

		if (!String.IsNullOrEmpty(this.hddClippingId.Value) && Convert.ToInt32(this.hddClippingId.Value) > 0)
		{
			this.ListFiles1.RegistroId = Convert.ToInt32(this.hddClippingId.Value);
		}

		if (!String.IsNullOrEmpty(this.hddArquivoId.Value) && Convert.ToInt32(this.hddArquivoId.Value) > 0)
		{
			this.ListFiles1.ArquivoId = Convert.ToInt32(this.hddArquivoId.Value);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	protected void LoadForm()
	{
		if (IdRegistro > 0)
		{
			Clipping clippingBO = new ClippingBLL().Carregar(new Clipping() { ClippingId = (int)IdRegistro });
			this.hddClippingId.Value = clippingBO.ClippingId.ToString();
			this.txtTitulo.Text = clippingBO.ConteudoImprensa.Titulo;
			this.txtIntegra.Text = clippingBO.ConteudoImprensa.Texto;
			this.txtAutor.Text = clippingBO.Autor;
			this.txtFonte.Text = clippingBO.ConteudoImprensa.Fonte;
			this.txtFonteUrl.Text = clippingBO.ConteudoImprensa.FonteUrl;
			this.chkAtivo.Checked = clippingBO.ConteudoImprensa.Ativo;
			this.chkDestaque.Checked = clippingBO.ConteudoImprensa.Destaque;
			this.txtFonteUrl.Text = clippingBO.ConteudoImprensa.FonteUrl;
			this.txtDataPublicacao.Text = clippingBO.DataPublicacao.ToString();
			this.txtDataExibicaoInicio.Text = clippingBO.ConteudoImprensa.DataExibicaoInicio.ToString();
			this.txtDataExibicaoFim.Text = clippingBO.ConteudoImprensa.DataExibicaoFim.ToString();

            // Busca as categorias e as seleciona
            IEnumerable<Categoria> categorias = new EventoBLL().CarregarCategoriasConteudo(clippingBO.ConteudoImprensa.Conteudo);

            foreach (Categoria cat in categorias)
            {
                for (int i = 0; i < cblCategorias.Items.Count; i++)
                {
                    if (cblCategorias.Items[i].Value.Equals(cat.CategoriaId.ToString()))
                        cblCategorias.Items[i].Selected = true;
                }
            }

			ClippingImagem clippingImagemBO = new ClippingImagem();
			clippingImagemBO.Clipping = new Clipping();
			clippingImagemBO.Clipping = clippingBO;

			IEnumerable<ClippingImagem> clippingImagemBOIEnum = new ClippingBLL().CarregarTodosClippingImagem(clippingImagemBO).OrderBy(i => i.OrdemApresentacao);

			foreach (ClippingImagem clippingImagemBOTemp in clippingImagemBOIEnum)
			{
				if (clippingImagemBOTemp.Arquivo != null && clippingImagemBOTemp.Arquivo.ArquivoId > 0)
				{
					this.hddArquivoId.Value = clippingImagemBOTemp.Arquivo.ArquivoId.ToString();
				}
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	private void ExecuteClick()
	{
		Page.MaintainScrollPositionOnPostBack = false;
		this.SaveOrUpdate();
	}

	/// <summary>
	/// 
	/// </summary>
	protected void SaveOrUpdate()
	{
		if (Page.IsValid)
		{
			Clipping clippingBO = new Clipping();
			clippingBO.ClippingId = Convert.ToInt32(this.hddClippingId.Value);
			clippingBO.ConteudoImprensa = new ConteudoImprensa();
			clippingBO.ConteudoImprensa.Titulo = this.txtTitulo.Text.Trim();
			clippingBO.ConteudoImprensa.Texto = this.txtIntegra.Text.Replace("/manager/", "/site/");
			clippingBO.Autor = this.txtAutor.Text.Trim();
			clippingBO.ConteudoImprensa.Fonte = this.txtFonte.Text.Trim();
			clippingBO.ConteudoImprensa.FonteUrl = this.txtFonteUrl.Text.Trim();
			clippingBO.ConteudoImprensa.Ativo = chkAtivo.Checked;
			clippingBO.ConteudoImprensa.Destaque = chkDestaque.Checked;
			clippingBO.ConteudoImprensa.FonteUrl = this.txtFonteUrl.Text.Trim();
			if (!string.IsNullOrEmpty(txtDataPublicacao.Text))
				clippingBO.DataPublicacao = Convert.ToDateTime(txtDataPublicacao.Text);
			if (!string.IsNullOrEmpty(txtDataExibicaoInicio.Text))
				clippingBO.ConteudoImprensa.DataExibicaoInicio = Convert.ToDateTime(txtDataExibicaoInicio.Text);
			else
				clippingBO.ConteudoImprensa.DataExibicaoInicio = null;
			if (!string.IsNullOrEmpty(txtDataExibicaoFim.Text))
				clippingBO.ConteudoImprensa.DataExibicaoFim = Convert.ToDateTime(txtDataExibicaoFim.Text);
			else
				clippingBO.ConteudoImprensa.DataExibicaoFim = null;

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

			if (clippingBO.ClippingId > 0)
			{
                new ClippingBLL().Atualizar(clippingBO, categorias);
				Util.ShowUpdateMessage();
			}
			else
			{
                clippingBO = new ClippingBLL().Inserir(clippingBO, categorias);
				this.hddClippingId.Value = clippingBO.ClippingId.ToString();
				this.BindListFile();
				Util.ShowInsertMessage();
			}
		}
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="e"></param>
	private void CarregarImagensClipping(ArquivoEventArgs e)
	{	
		if (e.ArquivoId > 0)
		{
			if (!String.IsNullOrEmpty(this.hddArquivoId.Value) && Convert.ToInt32(this.hddArquivoId.Value) > 0)
			{
				this.ExcluirImagensClipping(new ArquivoEventArgs() { ArquivoId = Convert.ToInt32(this.hddArquivoId.Value) });
			}

			this.ListFiles1.ArquivoId = e.ArquivoId;
			this.ListFiles1.RegistroId = Convert.ToInt32(IdRegistro);

			Arquivo arquivoBO = new Arquivo();
			arquivoBO = new ArquivoBLL().CarregarArquivo(new Arquivo() { ArquivoId = this.ListFiles1.ArquivoId });

			if (arquivoBO != null && arquivoBO.ArquivoId > 0)
			{
				this.hddArquivoId.Value = arquivoBO.ArquivoId.ToString();

				//cria o relacionamento
				ClippingImagem clippingImagemBO = new ClippingImagem();
				clippingImagemBO.OrdemApresentacao = 1;
				clippingImagemBO.Clipping = new Clipping();
				clippingImagemBO.Clipping.ClippingId = (int)IdRegistro;
				clippingImagemBO.Arquivo = new Arquivo();
				clippingImagemBO.Arquivo = arquivoBO;

				new ClippingBLL().InserirClippingImagem(clippingImagemBO);
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="e"></param>
	private void ExcluirImagensClipping(ArquivoEventArgs e)
	{
		Arquivo arquivoBO = new ClippingBLL().CarregarArquivo(new Arquivo() { ArquivoId = Convert.ToInt32(e.ArquivoId) });

		if (arquivoBO != null)
		{
			string pathFile = String.Concat(Server.MapPath(GrupoA_Resource.baseUrlUpload), ListFiles1.TargetFolder, arquivoBO.NomeArquivo);

			FileInfo fileInfo = new FileInfo(pathFile);

			if (fileInfo.Exists)
			{
				fileInfo.Delete();
			}
		}

		new ClippingBLL().ExcluirClippingImagem(new ClippingImagem() { Arquivo = new Arquivo() { ArquivoId = e.ArquivoId } });
		this.hddArquivoId.Value = "0";

		if (arquivoBO != null && arquivoBO.ArquivoId > 0)
		{
			new ArquivoBLL().ExcluirArquivo(arquivoBO);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	private void AtualizaListFile()
	{
		//atualiza ListFile
		this.ListFiles1.RegistroId = (int)IdRegistro;
		this.ListFiles1.DataSource = null;
		this.ListFiles1.DataBind();
	}

	/*
	/// <summary>
	/// 
	/// </summary>
	private void CarregarImagensClipping()
	{
		List<ItemArquivo> arquivos = new List<ItemArquivo>();
		ItemArquivo arquivo = null;

		IEnumerable<ClippingImagem> clippingImagemBOIEnum = new ClippingBLL()
			.CarregarTodosClippingImagem(
				new ClippingImagem()
				{
					Clipping = new Clipping()
					{
						ClippingId = Convert.ToInt32(this.hddClippingId.Value)
					}
				}).OrderBy(i => i.OrdemApresentacao);

		foreach (ClippingImagem clippingImagemBOTemp in clippingImagemBOIEnum)
		{
			string pathFile = string.Concat(GrupoA_Resource.baseUrlUpload, ListFiles1.TargetFolder, clippingImagemBOTemp.Arquivo.NomeArquivo);

			arquivo = new ItemArquivo();
			arquivo.ArquivoId = clippingImagemBOTemp.Arquivo.ArquivoId;
			arquivo.CaminhoArquivo = pathFile;
			arquivo.TamanhoArquivo = clippingImagemBOTemp.Arquivo.TamanhoArquivo.ToString();
			arquivos.Add(arquivo);
		}

		this.ListFiles1.DataSource = arquivos;
		this.ListFiles1.DataBind();
	}
	*/

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