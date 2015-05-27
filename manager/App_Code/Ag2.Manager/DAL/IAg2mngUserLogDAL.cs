using System;
using System.Web.Security;

namespace Ag2.Manager.DAL
{
    public interface IAg2mngUserLogDAL
    {
        Ag2.Manager.Entity.Ag2mngUserLog CarregaUltimoAcesso(Ag2.Manager.Entity.ag2mngUser user);
        void InserirLogAcesso(Ag2.Manager.Entity.ag2mngUser user);
    }
}
