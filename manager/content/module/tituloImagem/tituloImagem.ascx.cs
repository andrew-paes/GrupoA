using System;
using System.Collections.Generic;
using System.Web.UI;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using GrupoA.BusinessObject.Enumerator;
using System.IO;
using System.Configuration;
using GrupoA.GlobalResources;
using System.Linq;
using System.Web.UI.WebControls;

public partial class content_module_tituloImagem_tituloImagem : System.Web.UI.UserControl
{
	#region Propriedades

	private int _id
	{
		get { if (Session["_idTitulo"] == null) Session["_idTitulo"] = 0; return (int)Session["_idTitulo"]; }
		set { if (Session["_idTitulo"] == null) Session["_idTitulo"] = 0; Session["_idTitulo"] = (int)value; }
	}

	private int _idArquivo
	{
		get { if (Session["_idArquivo"] == null) Session["_idArquivo"] = 0; return (int)Session["_idArquivo"]; }
		set { if (Session["_idArquivo"] == null) Session["_idArquivo"] = 0; Session["_idArquivo"] = (int)value; }
	}

	private string _NomeArquivo
	{
		get { if (Session["_NomeArquivo"] == null) Session["_NomeArquivo"] = string.Empty; return (string)Session["_NomeArquivo"]; }
		set { if (Session["_NomeArquivo"] == null) Session["_NomeArquivo"] = string.Empty; Session["_NomeArquivo"] = (string)value; }
	}

	private ProdutoBLL _produtoBll;
	private ProdutoBLL ProdutoBll
	{
		get { return _produtoBll ?? (_produtoBll = new ProdutoBLL()); }
		set { _produtoBll = value; }
	}

	private ArquivoBLL _arquivoBll;
	public ArquivoBLL ArquivoBll
	{
		get { return _arquivoBll ?? (_arquivoBll = new ArquivoBLL()); }
		set { _arquivoBll = value; }
	}

    private ImagemThumb.ImagemThumb _geradorDeImagemThumb;

    public ImagemThumb.ImagemThumb GeradorDeImagemThumb
    {
        get { return _geradorDeImagemThumb ?? (_geradorDeImagemThumb = new ImagemThumb.ImagemThumb()); }
        set { _geradorDeImagemThumb = value; }
    }

    #endregion

	#region eventos

	protected void Page_Load(object sender, EventArgs e)
	{
		ListFiles1.BindList += new EventHandler<ArquivoEventArgs>(ListFiles1_BindList);
		ListFiles1.DeleteItem += new EventHandler<ArquivoEventArgs>(ListFiles1_DeleteItem);

		if (Util.GetRequestId() > 0)
		{
			_id = Util.GetRequestId();
			if (!IsPostBack)
				this.loadForm();
			ListFiles1.RegistroId = Util.GetRequestId();
		}
	}

	void ListFiles1_DeleteItem(object sender, ArquivoEventArgs e)
	{
		//Exclui arquivos e relacionamento anterior

		var arquivosDoProduto =
			ProdutoBll.CarregarProdutoImagem(new ProdutoImagem() { Produto = new Produto() { ProdutoId = _id }, Arquivo = new Arquivo() { ArquivoId = Convert.ToInt32(((ImageButton)sender).CommandArgument) } });

		foreach (var produtoImagem in arquivosDoProduto)
		{
			ProdutoBll.ExcluirProdutoImagem(produtoImagem);
            ArquivoBll.ExcluirArquivo(produtoImagem.Arquivo);
		}

		// TODO: Verificar se isso está realmente funcionando
		string strFilePath = Server.MapPath(string.Concat(GrupoA_Resource.baseUrlUpload, "/", ListFiles1.TargetFolder, "/"));
		this.ExcluirArquivosFisicamente(arquivosDoProduto, strFilePath);
		this.loadForm();
		ListFiles1.RegistroId = _id;
		ListFiles1.DataBind();
	}

	void ListFiles1_BindList(object sender, ArquivoEventArgs e)
	{
		var arquivo = ArquivoBll.CarregarArquivo(new Arquivo() { ArquivoId = e.ArquivoId });

		_NomeArquivo = arquivo.NomeArquivo;
		_idArquivo = arquivo.ArquivoId;

		this.Execute();
	}

	#endregion

	#region Métodos

	protected void loadForm()
	{
		if (_id > 0)
		{
			//var produto = new TituloBLL().CarregarComDependencias(new Titulo() { TituloId = _id });
			var produto = ProdutoBll.Carregar(new Produto() { ProdutoId = _id });
			lblTitulo.Text = produto.NomeProduto;

            ProdutoImagem produtoImagem = new ProdutoImagem();
            produtoImagem.Produto = produto;
            produtoImagem.ProdutoImagemTipo = new ProdutoImagemTipo((int)TipoDeImagemDoTitulo.GaleriaDeImagens);

			var arquivoGrande = ProdutoBll.CarregarProdutoImagem(produtoImagem);

			// Ajustar, imagem vem de ProdutoImagem.
            if (arquivoGrande.Count > 0)
            {
                _idArquivo = arquivoGrande[0].Arquivo.ArquivoId;
                _NomeArquivo = arquivoGrande[0].Arquivo.NomeArquivoOriginal;
                ListFiles1.CarregaArquivo(_idArquivo);
			}
		}
	}

	private void Execute()
	{
		if (Page.IsValid)
		{
			if (_idArquivo > 0)
			{
				// TODO: Verificar se isso está funcionando corretamente
				string strFilePath = Server.MapPath(string.Concat(GrupoA_Resource.baseUrlUpload, "/", ListFiles1.TargetFolder, "/"));
				string strFileName = strFilePath + _NomeArquivo;
				string strFileNameGI = Path.GetFileNameWithoutExtension(strFileName) + Path.GetExtension(strFileName);
				//string strFileNameM = Path.GetFileNameWithoutExtension(strFileName) + "_M" + Path.GetExtension(strFileName);
				//string strFileNameP = Path.GetFileNameWithoutExtension(strFileName) + "_P" + Path.GetExtension(strFileName);

                //File.Copy(strFileName, strFilePath + strFileNameGI, true);
				//File.Copy(strFileName, strFilePath + strFileNameM, true);
				//File.Copy(strFileName, strFilePath + strFileNameP, true);

				//File.Delete(strFileName);

                //GeradorDeImagemThumb.GerarProporcao(strFilePath + strFileNameGI, 330, 440,
                //                                                System.Drawing.Color.White);
                //GeradorDeImagemThumb.GerarProporcao(strFilePath + strFileNameM, 165, 220,
                //                                                System.Drawing.Color.White);
                //GeradorDeImagemThumb.GerarProporcao(strFilePath + strFileNameP, 95, 128,
                //                                                System.Drawing.Color.White);

				// Ajustar, imagem vem de ProdutoImagem.
				var produtoImagens = new List<ProdutoImagem>();
                produtoImagens.Add(this.PopularProdutoImagem(strFilePath, strFileNameGI, _NomeArquivo, (int)TipoDeImagemDoTitulo.GaleriaDeImagens));
				//produtoImagens.Add(this.PopularProdutoImagem(strFilePath, strFileNameM, (int)TipoDeImagemDoTitulo.Media));
				//produtoImagens.Add(this.PopularProdutoImagem(strFilePath, strFileNameP, (int)TipoDeImagemDoTitulo.Pequena));

				////Exclui arquivos e relacionamento anterior
                //var produtoImagensParaAtualizacao =
                //    ProdutoBll.CarregarProdutoImagem(new ProdutoImagem() {Produto = new Produto() {ProdutoId = _id}}).ToList();
                //foreach (var produtoImagem in produtoImagensParaAtualizacao)
                //{
                //    ProdutoBll.ExcluirProdutoImagem(produtoImagem);
                //    new ArquivoBLL().ExcluirArquivo(new Arquivo(produtoImagem.Arquivo.ArquivoId));
                //}

				//ProdutoBll.InserirTituloImagem(produtoImagens);
                ProdutoBll.InserirProdutoImagem(produtoImagens);

                //this.ExcluirArquivosFisicamente(produtoImagensParaAtualizacao, strFilePath);

                //new ArquivoBLL().ExcluirArquivo(new Arquivo(_idArquivo));

				Util.ShowUpdateMessage();
			}
			else
			{
				Util.ShowMessage("Você não alterou a imagem.");
			}
			this.loadForm();
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
    private ProdutoImagem PopularProdutoImagem(string strFilePath, string strFileName, string strNomeOriginal, int produtoImagemTipoId)
	{
		var produtoImagem = new ProdutoImagem {ProdutoImagemTipo = new ProdutoImagemTipo()};
		produtoImagem.ProdutoImagemTipo.ProdutoImagemTipoId = produtoImagemTipoId;

		produtoImagem.Produto = new Produto();
		produtoImagem.Produto.ProdutoId = _id;

		produtoImagem.Arquivo = new Arquivo();
		if (File.Exists(strFilePath + strFileName))
		{
			FileInfo info = new FileInfo(strFilePath + strFileName);
			produtoImagem.Arquivo.TamanhoArquivo = Convert.ToInt32(info.Length);
			produtoImagem.Arquivo.NomeArquivo = strFileName;
            produtoImagem.Arquivo.NomeArquivoOriginal = strNomeOriginal;
			produtoImagem.Arquivo.DataHoraUpload = DateTime.Now;
		}
		return produtoImagem;
	}

	#endregion
}