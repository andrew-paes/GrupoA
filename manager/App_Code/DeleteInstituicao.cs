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
public class DeleteInstituicao
{
    public DeleteInstituicao()
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
            Instituicao instituicaoBO = new Instituicao();
            instituicaoBO.InstituicaoId = Convert.ToInt32(id);

            new InstituicaoBLL().Excluir(instituicaoBO);

            Util.ShowMessage("Instituição excluído com sucesso!", Ag2.Manager.Enumerator.typeMessage.Sucesso);
		}
		catch
		{
			Util.ShowMessage("Não foi possível excluir a instituição, pois está relacionada com outros dados!", Ag2.Manager.Enumerator.typeMessage.Erro);
		}

		return ObjDelete;
	}
}
