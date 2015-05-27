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
public class DeleteMidia
{
    public DeleteMidia()
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
            Midia midiaBO = new Midia();
            midiaBO.MidiaId = Convert.ToInt32(id);
            midiaBO = new MidiaBLL().CarregarComDependencia(midiaBO);

            if (midiaBO != null && midiaBO.MidiaId > 0)
            {
                new MidiaBLL().Excluir(midiaBO);

                Util.ShowMessage("Mídia excluída com sucesso!", Ag2.Manager.Enumerator.typeMessage.Sucesso);
            }
            else
            {
                Util.ShowMessage("Não foi possível excluir a mídia!", Ag2.Manager.Enumerator.typeMessage.Erro);
            }
        }
        catch
        {
            Util.ShowMessage("Não foi possível excluir a mídia!", Ag2.Manager.Enumerator.typeMessage.Erro);
        }

        return ObjDelete;
    }
}
