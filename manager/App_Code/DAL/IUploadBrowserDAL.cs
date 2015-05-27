using System;
using System.Collections.Generic;
namespace Ag2.Manager.DAL
{
    public interface IUploadBrowserDAL
    {
        Ag2.Manager.Entity.Arquivo Save(Ag2.Manager.Entity.Arquivo arquivo);
        void Delete(Ag2.Manager.Entity.Arquivo arquivo);
        List<Ag2.Manager.Entity.Arquivo> CarregarAquivos(Ag2.Manager.Enumerator.tipoArquivoBrowser tipoArquivo);
        Ag2.Manager.Entity.Arquivo CarregarAquivoByName(Ag2.Manager.Entity.Arquivo arquivo);
        void RenomearArquivo(Ag2.Manager.Entity.Arquivo arquivoOriginal, Ag2.Manager.Entity.Arquivo arquivoNovo);
        void SaveArquivoTags(Ag2.Manager.Entity.Arquivo arquivo, List<Ag2.Manager.Entity.Tag> tags);
        List<Ag2.Manager.Entity.Arquivo> BuscaArquivoTags(List<Ag2.Manager.Entity.Tag> tags);
        void CarregarAquivos(List<Ag2.Manager.Entity.Arquivo> arquivos);
    }
}
