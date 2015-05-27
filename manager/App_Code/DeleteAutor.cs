using System;
using System.IO;
using Ag2.Manager.Helper;
using Ag2.Manager.Module;
using Ag2.Manager.Module.Info;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using GrupoA.GlobalResources;

/// <summary>
/// Summary description for DeleteAutor
/// </summary>
public class DeleteAutor
{
    public DeleteAutor()
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
            Autor autorBO = new Autor();
            autorBO.AutorId = Convert.ToInt32(id);
            autorBO = new AutorBLL().CarregarAutorComDependencia(autorBO);

            if (autorBO != null && autorBO.AutorId > 0)
			{
                if (new TituloBLL().ContarTituloPorAutor(autorBO) == 0)
                {
                    if (autorBO.ArquivoImagem != null)
                    {
                        Arquivo arquivoBO = new Arquivo();
                        arquivoBO.ArquivoId = autorBO.ArquivoImagem.ArquivoId;
                        arquivoBO = new ArquivoBLL().CarregarArquivo(arquivoBO);

                        string pathFile = System.Web.HttpContext.Current.Server.MapPath(String.Concat(GrupoA_Resource.baseUrlUpload, GrupoA.GlobalResources.GrupoA_Resource.PastaImagensAutor, arquivoBO.NomeArquivoOriginal));

                        FileInfo info = new FileInfo(pathFile);

                        if (info != null && File.Exists(pathFile))
                        {
                            info.Delete();
                        }

                        new ArquivoBLL().ExcluirArquivo(arquivoBO);
                    }

                    new AutorBLL().Excluir(autorBO);

                    Util.ShowMessage("Autor excluído com sucesso!", Ag2.Manager.Enumerator.typeMessage.Sucesso);
                }
                else
                {
                    Util.ShowMessage("Não foi possível excluir o autor. Títulos associados a este autor.", Ag2.Manager.Enumerator.typeMessage.Erro);
                }
			}
            else
            {
                Util.ShowMessage("Não foi possível excluir o autor!", Ag2.Manager.Enumerator.typeMessage.Erro);
            }

			
		}
		catch
		{
			Util.ShowMessage("Não foi possível excluir o autor!", Ag2.Manager.Enumerator.typeMessage.Erro);
		}

		return ObjDelete;
	}
}
