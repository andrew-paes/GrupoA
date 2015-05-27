using System;
using Ag2.Manager.Helper;
using Ag2.Manager.Module;
using Ag2.Manager.Module.Info;
using GrupoA.BusinessLogicalLayer;

/// <summary>
/// Summary description for DeleteOferta
/// </summary>
public class DeleteOferta
{
    public DeleteOferta()
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
            GrupoA.BusinessObject.Oferta ofertaBO = new GrupoA.BusinessObject.Oferta();
            ofertaBO.OfertaId = Convert.ToInt32(id);

            new OfertaBLL().Excluir(ofertaBO);

            Util.ShowMessage("Oferta excluída com sucesso!", Ag2.Manager.Enumerator.typeMessage.Sucesso);
        }
        catch
        {
            Util.ShowMessage("Não foi possível excluir a oferta!", Ag2.Manager.Enumerator.typeMessage.Erro);
        }

        return ObjDelete;
    }
}
