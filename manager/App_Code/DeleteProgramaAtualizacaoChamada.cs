using System;
using Ag2.Manager.Module;
using Ag2.Manager.Module.Info;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;

/// <summary>
/// Summary description for DeleteUsuario
/// </summary>
public class DeleteProgramaAtualizacaoChamada
{
    public DeleteProgramaAtualizacaoChamada()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static DeleteRegisterInfo BeforeRegisterDelete(ManagerModuleStructure module, string id)
    {
        DeleteRegisterInfo ObjDelete = new DeleteRegisterInfo();
        //SETADO COMO FALSE PARA ELE NÃO EXECUTAR O DELETE PADRAO DO MANAGER
        ObjDelete.CanDelete = false;
        new ProgramaAtualizacaoChamadaBLL().ExcluirProgramaAtualizacaoChamada(new ProgramaAtualizacaoChamada() { ProgramaAtualizacaoChamadaId = Convert.ToInt32(id) });

        return ObjDelete;
    }
}
