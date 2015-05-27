using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Ag2.Manager.DAL;
using Ag2.Manager.BusinessObject;
using Ag2.Manager.Helper;
using System.Web.Security;
using System.Data.Common;
using System.Data;

namespace Ag2.Manager.ADO.Oracle
{
    /// <summary>
    /// Summary description for UploadBrowser
    /// </summary>
    public class UploadBrowser : BaseADO, IUploadBrowserDAL
    {
        public UploadBrowser()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void CarregarAquivos(List<Ag2.Manager.Entity.Arquivo> arquivos)
        {
            throw new NotImplementedException();
        }

        public List<Ag2.Manager.Entity.Arquivo> BuscaArquivoTags(List<Ag2.Manager.Entity.Tag> tags)
        {
            throw new NotImplementedException();
        }

        public void SaveArquivoTags(Ag2.Manager.Entity.Arquivo arquivo, List<Ag2.Manager.Entity.Tag> tags)
        {
            throw new NotImplementedException();
        }

        public void RenomearArquivo(Ag2.Manager.Entity.Arquivo arquivoOriginal, Ag2.Manager.Entity.Arquivo arquivoNovo)
        {
            throw new NotImplementedException();
        }

        public Ag2.Manager.Entity.Arquivo CarregarAquivoByName(Ag2.Manager.Entity.Arquivo arquivo)
        {
            throw new NotImplementedException();
        }

        public List<Ag2.Manager.Entity.Arquivo> CarregarAquivos(Ag2.Manager.Enumerator.tipoArquivoBrowser tipoArquivo)
        {
            throw new NotImplementedException();
        }

        public Ag2.Manager.Entity.Arquivo Save(Ag2.Manager.Entity.Arquivo arquivo)
        {
            throw new NotImplementedException();
        }

        public void Delete(Ag2.Manager.Entity.Arquivo arquivo)
        {
            throw new NotImplementedException();
        }
    }
}
