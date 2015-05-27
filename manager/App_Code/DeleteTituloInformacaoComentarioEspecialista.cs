using System;
using Ag2.Manager.Helper;
using Ag2.Manager.Module;
using Ag2.Manager.Module.Info;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;

/// <summary>
/// Summary description for DeleteOferta
/// </summary>
public class DeleteTituloInformacaoComentarioEspecialista
{
    public DeleteTituloInformacaoComentarioEspecialista()
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
            TituloImpresso tituloImpresso = new TituloBLL().CarregarTituloImpressoComComentarioDoEspecialista(new TituloImpresso() { TituloImpressoId = Convert.ToInt32(id) });
            Titulo titulo = tituloImpresso.Titulo;

            if (titulo.TituloInformacaoComentarioEspecialista != null && titulo.TituloInformacaoComentarioEspecialista.TituloInformacaoComentarioEspecialistaId > 0)
            {
                new TituloBLL().ExcluirComentarioEspecialista(titulo.TituloInformacaoComentarioEspecialista);

                Util.ShowMessage("Comentário do especialista excluído com sucesso!", Ag2.Manager.Enumerator.typeMessage.Sucesso);
            }
            else
            {
                Util.ShowMessage("Nenhum comentário do especialista para ser excluído.", Ag2.Manager.Enumerator.typeMessage.None);
            }
        }
        catch
        {
            Util.ShowMessage("Não foi possível excluir comentário do especialista!", Ag2.Manager.Enumerator.typeMessage.Erro);
        }

        return ObjDelete;
    }
}
