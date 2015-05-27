using System;
using Ag2.Manager.Helper;
using Ag2.Manager.Module;
using Ag2.Manager.Module.Info;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;

/// <summary>
/// Summary description for DeleteUsuario
/// </summary>
public class DeleteContatoSetor
{

    public DeleteContatoSetor()
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
        ObjDelete.CanDelete = false;

        try
        {
            new ContatoSetorBLL().ExcluirContatoSetor(new ContatoSetor() { ContatoSetorId = Convert.ToInt32(id) });
            Util.ShowMessage("Setor excluído com sucesso!", Ag2.Manager.Enumerator.typeMessage.Sucesso);
            return ObjDelete;
        }
        catch
        {
            Util.ShowMessage("Este Setor não pode ser excluído pois está vinculado a um Assunto!", Ag2.Manager.Enumerator.typeMessage.Erro);
            return ObjDelete;
        }

    }

}
