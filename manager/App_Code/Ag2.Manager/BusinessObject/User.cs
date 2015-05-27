using System;
using System.Collections.Generic;
using System.Text;
using Ag2.Manager.ADO.MSSql;
using System.Data;
using System.Web.Security;
using Ag2.Manager.Helper;
using Ag2.Manager.DAL;

namespace Ag2.Manager.BusinessObject
{

    public enum UserStatus { Valid = 1, Invalid, Blocked }


    public class User
    {
        private string _name;
        private Util util = new Util();

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        //Variavel com status do usuario
        //public enum UserStatus 

        public DataTable GetById(int ID)
        {
            UserADO userDb = new UserADO();
            return userDb.GetById(ID);
        }

        /// <summary>Valida login do usuário</summary>
        /// <param name="login">Login do usuário</param>
        /// <param name="password">Senha não criptografada do usuário</param>
        public UserStatus Validate(string login, string password)
        {
            Ag2.Manager.Module.ManagerModule manager = new Ag2.Manager.Module.ManagerModule();
            IUserDAL userDb = (IUserDAL)Util.GetADO("UserADO", System.Reflection.Assembly.GetExecutingAssembly());
            IAg2mngUserLogDAL _ag2mngUserLogADO = (IAg2mngUserLogDAL)Util.GetADO("Ag2mngUserLogADO", (System.Reflection.Assembly)System.Web.Compilation.BuildManager.CodeAssemblies[0]);
            UserStatus userStatus = UserStatus.Invalid;
            Ag2.Manager.Entity.ag2mngUser user = new Ag2.Manager.Entity.ag2mngUser();
            
            user.login = login;
            user.password = password;

            if (userDb.ValidateUser(user))
            {
                FormsAuthentication.RedirectFromLoginPage(user.login, true);

                Ag2.Manager.Helper.CurrentSessions cs = new Ag2.Manager.Helper.CurrentSessions();

                cs.User = user;
                _ag2mngUserLogADO.InserirLogAcesso(user);
                userStatus = UserStatus.Valid;
            }
            else
            {
                userStatus = UserStatus.Invalid;
            }

            return userStatus;
        }

        /// <summary>
        /// Consulta usuário pelo login
        /// </summary>
        /// <param name="login">Login do usuário</param>
        /// <returns>DataTable com dados do usuário</returns>
        public DataTable GetByLogin(string login)
        {
            UserADO userDb = new UserADO();
            return userDb.GetByLogin(login);
        }
    }




}
