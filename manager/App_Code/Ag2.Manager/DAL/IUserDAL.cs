using System;
using System.Collections.Generic;
namespace Ag2.Manager.DAL
{
    public interface IUserDAL
    {
        System.Data.DataTable GetById(int ID);
        System.Data.DataTable GetByLogin(string login);
        bool ValidateUser(Ag2.Manager.Entity.ag2mngUser user);
        List<Ag2.Manager.Entity.ag2mngPerfil> GetPerfilByUser(Ag2.Manager.Entity.ag2mngUser user);
        void ChangePassword(Ag2.Manager.Entity.ag2mngUser user);
        Ag2.Manager.Entity.ag2mngUser Load(Ag2.Manager.Entity.ag2mngUser _user);
        List<Ag2.Manager.Entity.ag2mngPerfil> GetAllPerfil();
        void Save(Ag2.Manager.Entity.ag2mngUser _user);
        List<Ag2.Manager.Entity.ag2mngPerfil> GetPerfilsByPerfis(List<Ag2.Manager.Entity.ag2mngPerfil> perfis);
        bool Exists(Ag2.Manager.Entity.ag2mngUser user);
        void DeleteUser(Ag2.Manager.Entity.ag2mngUser user);
        void ResetPassword(Ag2.Manager.Entity.ag2mngUser _user);
        List<Ag2.Manager.Entity.ag2mngMenu> GetMenusPermission(List<Ag2.Manager.Entity.ag2mngPerfil> perfis);
        Ag2.Manager.Entity.ag2mngMenu GetMenuPermissionByMenu(Ag2.Manager.Entity.ag2mngMenu menu);
    }
}
