using System;
using Ag2.Manager.Helper;
using Ag2.Manager.Module;
using Ag2.Manager.Module.Info;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;

/// <summary>
/// Summary description for DeleteUsuario
/// </summary>
public class DeleteRevistaArtigo
{

    #region Métodos

    public DeleteRevistaArtigo()
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
            int revistaArtigoId = Convert.ToInt32(id);
            new RevistaGrupoABLL().ExcluirRelacionamentoRevistaArtigoIdAssociado(revistaArtigoId);
            new RevistaGrupoABLL().ExcluirRevistaArtigoImagem(revistaArtigoId, 0, true);
            new RevistaGrupoABLL().ExcluirRevistaArtigo(new RevistaArtigo() { RevistaArtigoId = revistaArtigoId });
            new RevistaArtigoControversiaBLL().ExcluirTodosPorRevistaArtigoId(revistaArtigoId);

            Util.ShowMessage("Revista Artigo excluído com sucesso!", Ag2.Manager.Enumerator.typeMessage.Sucesso);
            return ObjDelete;
        }
        catch
        {
            Util.ShowMessage("Este Artigo não pode ser excluído pois ocorreu um erro inesperado!", Ag2.Manager.Enumerator.typeMessage.Erro);
            return ObjDelete;
        }

    }

    #endregion

}
