using System;
using System.Collections.Generic;
using System.IO;
using Ag2.Manager.Helper;
using Ag2.Manager.Module;
using Ag2.Manager.Module.Info;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using GrupoA.GlobalResources;

/// <summary>
/// Summary description for DeleteUsuario
/// </summary>
public class DeleteNoticia
{
	public DeleteNoticia()
	{
		//
		// TODO: Add constructor logic here
		//
	}

	/// <summary>
	/// Método configurado no Web.Config para ser executado quando o usuário executa a ação de delete no manager
	/// </summary>
	/// <param name="module"></param>
	/// <param name="id"></param>
	/// <returns></returns>
	public static DeleteRegisterInfo BeforeRegisterDelete(ManagerModuleStructure module, string id)
	{
		DeleteRegisterInfo ObjDelete = new DeleteRegisterInfo();

		//SETADO COMO FALSE PARA ELE NÃO EXECUTAR O DELETE PADRAO DO MANAGER
		ObjDelete.CanDelete = false;

		try
		{
			GrupoA.BusinessObject.Noticia noticiaBO = new GrupoA.BusinessObject.Noticia();
			noticiaBO.NoticiaId = Convert.ToInt32(id);
			noticiaBO = new NoticiaBLL().Carregar(noticiaBO);

			if (noticiaBO != null && noticiaBO.NoticiaId > 0)
			{
				List<Arquivo> arquivoBOList = new List<Arquivo>();
				Arquivo arquivoBO = null;

				IEnumerable<NoticiaImagem> noticiaImagemBOIEnum = new NoticiaBLL().CarregarTodosNoticiaImagem(noticiaBO);

				if (noticiaImagemBOIEnum != null)
				{
					foreach (NoticiaImagem noticiaImagemBOTemp in noticiaImagemBOIEnum)
					{
						arquivoBO = new Arquivo();
						arquivoBO.ArquivoId = noticiaImagemBOTemp.Arquivo.ArquivoId;
						arquivoBO = new ArquivoBLL().CarregarArquivo(arquivoBO);

                        string pathFile = System.Web.HttpContext.Current.Server.MapPath(String.Concat(GrupoA_Resource.baseUrlUpload, GrupoA.GlobalResources.GrupoA_Resource.PastaImagensNoticia, arquivoBO.NomeArquivoOriginal));

						FileInfo info = new FileInfo(pathFile);

						if (info != null && File.Exists(pathFile))
						{
							info.Delete();
						}

						new NoticiaBLL().ExcluirNoticiaImagem(new NoticiaImagem() { Arquivo = arquivoBO });
						new ArquivoBLL().ExcluirArquivo(arquivoBO);
					}
				}

				new NoticiaBLL().Excluir(noticiaBO);

				if (noticiaBO.ConteudoImprensa != null && noticiaBO.ConteudoImprensa.ConteudoImprensaId > 0)
				{
					new ConteudoImprensaBLL().Excluir(noticiaBO.ConteudoImprensa);

					if (noticiaBO.ConteudoImprensa.Conteudo != null && noticiaBO.ConteudoImprensa.Conteudo.ConteudoId > 0)
					{
						new ConteudoBLL().ExcluirConteudo(noticiaBO.ConteudoImprensa.Conteudo);
					}
				}
			}

			Util.ShowMessage("Release excluído com sucesso!", Ag2.Manager.Enumerator.typeMessage.Sucesso);
		}
		catch
		{
			Util.ShowMessage("Não foi possível excluir o release!", Ag2.Manager.Enumerator.typeMessage.Erro);
		}

		return ObjDelete;
	}
}
