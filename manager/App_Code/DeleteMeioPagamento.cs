using System;
using Ag2.Manager.Module;
using Ag2.Manager.Module.Info;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;

/// <summary>
/// Summary description for DeletePromocao
/// </summary>
public class DeleteMeioPagamento
{
    public DeleteMeioPagamento()
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
        ObjDelete.CanDelete = true;

        new MeioPagamentoFaixaBLL().ExcluirRelacionado(new MeioPagamento() { MeioPagamentoId = Convert.ToInt32(id) });

        return ObjDelete;
    }
}
