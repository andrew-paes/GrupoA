using System;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Collections.Generic;
using Ag2.Manager.BusinessObject;

namespace Ag2.Manager.DAL
{
    public interface IPerfilDAL
    {
        /// <summary>
        /// Faz a associação de um perfil para um usuário
        /// </summary>
        /// <param name="user"></param>
        /// <param name="perfil"></param>
        void InsertUserPerfil(MembershipUser user, Perfil perfil);

        /// <summary>
        /// Deleta perfil relacionado ao usuário
        /// </summary>
        /// <param name="user"></param>
        void DeleteUserPerfil(MembershipUser user);

        void Delete(Ag2.Manager.Entity.ag2mngPerfil perfil);

        /// <summary>
        /// Retirna uma lista de Perfis
        /// </summary>
        /// <returns></returns>
        List<Perfil> GetAllPerfil();

        /// <summary>
        /// Retorna uma entidade do tipo Perfil 
        /// </summary>
        /// <param name="user">Usuário que se deseja saber o perfil</param>
        /// <returns></returns>
        Perfil GetPerfilByUser(MembershipUser user);

        /// <summary>
        /// Carrega todos do menus filho de um menu pai
        /// </summary>
        /// <param name="MenuPaiId"></param>
        /// <returns></returns>
        System.Data.DataTable MenuLoadByParentId(int MenuPaiId);

        /// <summary>
        /// Carrega menus de acordo mcom a permissão
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="perfil"></param>
        /// <returns></returns>
        System.Data.IDataReader LoadPermissionByMenuIdAndPerfilId(string menuId, Perfil perfil);
        
        /// <summary>
        /// Salva o registro de perfil e suas pemissões
        /// </summary>
        /// <param name="perfil">perfil a ser salvo</param>
        /// <param name="repeater">Repeater que contem as permissões seleciondas para o perfil</param>
        void Save(Ag2.Manager.Entity.ag2mngPerfil perfil, Repeater repeater);

        /// <summary>
        /// Carrega perfil 
        /// </summary>
        /// <param name="perfil">Id do perfil a ser carregado</param>
        /// <returns></returns>
        System.Data.DataTable LoadPermission();

        Ag2.Manager.Entity.ag2mngPerfil LoadById(Ag2.Manager.Entity.ag2mngPerfil perfil);
    }
}
