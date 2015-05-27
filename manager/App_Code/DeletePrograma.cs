using System;
using System.IO;
using Ag2.Manager.Module;
using Ag2.Manager.Module.Info;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using GrupoA.GlobalResources;

/// <summary>
/// Summary description for DeleteUsuario
/// </summary>
public class DeletePrograma
{
    public DeletePrograma()
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

        var cursoPanamericano = new CursoPanamericanoBLL().Carregar(new CursoPanamericano() { CursoPanamericanoId = Convert.ToInt32(Convert.ToInt32(id)) });
        
        new CursoPanamericanoBLL().ExcluiCursoPanamericanoCategoria(Convert.ToInt32(id));
        new CursoPanamericanoBLL().Excluir(new CursoPanamericano() { CursoPanamericanoId = Convert.ToInt32(id) });

        if (cursoPanamericano.ArquivoImagem != null)
        {
            var arquivoDel = new ArquivoBLL().CarregarArquivo(cursoPanamericano.ArquivoImagem);
            if (arquivoDel != null)
            {
                string pathFileDelete = string.Concat(System.Web.HttpContext.Current.Server.MapPath(GrupoA_Resource.baseUrlUpload), GrupoA_Resource.PastaImagensCursoPanamericano, arquivoDel.NomeArquivo);
                FileInfo infoDelete = new FileInfo(pathFileDelete);
                if (infoDelete.Exists)
                {
                    infoDelete.Delete();
                    new ArquivoBLL().ExcluirArquivo(arquivoDel);
                }
            }
        }

        return ObjDelete;
    }
}
