using System;
using System.Web.Security;

namespace Ag2.Manager.DAL
{
    public interface IUsuarioDAL
    {
        void GetUser();

        void DeleteUser(MembershipUser user);
    }
}
