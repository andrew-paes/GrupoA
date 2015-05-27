using System;
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
public class DeleteBanner
{
	public DeleteBanner()
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
			Banner bannerBO = new Banner();
			bannerBO.BannerId = Convert.ToInt32(id);
			bannerBO = new BannerBLL().CarregarBanner(bannerBO);

			if (bannerBO != null && bannerBO.BannerId > 0)
			{
				new BannerBLL().ExcluirAreasPorBanner(bannerBO);
				new BannerBLL().Excluir(bannerBO);

				Arquivo arquivoBO = new Arquivo();
				arquivoBO.ArquivoId = bannerBO.Arquivo.ArquivoId;
				arquivoBO = new ArquivoBLL().CarregarArquivo(arquivoBO);

                string pathFile = System.Web.HttpContext.Current.Server.MapPath(String.Concat(GrupoA_Resource.baseUrlUpload, GrupoA.GlobalResources.GrupoA_Resource.PastaImagensBanner, arquivoBO.NomeArquivoOriginal));

				FileInfo info = new FileInfo(pathFile);

				if (info != null && File.Exists(pathFile))
				{
					info.Delete();
				}

				new ArquivoBLL().ExcluirArquivo(arquivoBO);
			}

			Util.ShowMessage("Banner excluído com sucesso!", Ag2.Manager.Enumerator.typeMessage.Sucesso);
		}
		catch
		{
			Util.ShowMessage("Não foi possível excluir o banner!", Ag2.Manager.Enumerator.typeMessage.Erro);
		}

		return ObjDelete;
	}
}
