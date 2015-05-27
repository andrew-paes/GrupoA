using System;
using System.IO;
using Ag2.Manager.Module;
using Ag2.Manager.Module.Info;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using GrupoA.GlobalResources;

/// <summary>
/// Summary description for DeleteClipping
/// </summary>
public class DeleteClipping
{
    public DeleteClipping()
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

        ClippingImagem clippingImagem = new ClippingBLL().CarregarClippingImagem(new Clipping() { ClippingId = Convert.ToInt32(id) });

        if (clippingImagem != null)
        {
            Arquivo arquivo = new ArquivoBLL().CarregarArquivo(new Arquivo() { ArquivoId = clippingImagem.Arquivo.ArquivoId });
            new ClippingBLL().ExcluirClippingImagem(clippingImagem);

            string pathFileDelete = string.Concat(System.Web.HttpContext.Current.Server.MapPath(GrupoA_Resource.baseUrlUpload), GrupoA_Resource.PastaImagensClipping, arquivo.NomeArquivo);
            FileInfo infoDelete = new FileInfo(pathFileDelete);
            if (infoDelete.Exists)
            {
                infoDelete.Delete();
                new ArquivoBLL().ExcluirArquivo(arquivo);
            }
        }
        return ObjDelete;
    }
}
