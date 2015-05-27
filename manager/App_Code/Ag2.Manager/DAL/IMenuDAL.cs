using System;
using System.Data;

namespace Ag2.Manager.DAL
{
    public interface IMenuDAL
    {
        DataTable GetByModuleName(string moduleName, int perfilId);
        DataTable GetByParentId(int parentId, int perfilId);
        DataTable GetBySiteId(int parentId, int perfilId, int siteId);
        DataTable GetStructure(int perfilId);

        /// <summary>
        /// Retorna o registro do menu de acordo com seu id
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        IDataReader LoadMenuByMenuId(int menuId);

        /// <summary>
        /// Retorna todos do menus pais
        /// </summary>
        /// <returns></returns>
        DataTable GetAllParentMenus();

        /// <summary>
        /// Retorna as permissões de cada perfil para o menu informado
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        DataTable GetPermissionPerfilByMenuId(int menuId);

        /// <summary>
        /// Retorna o registro do menu pelo Nome
        /// </summary>
        /// <param name="menuName"></param>
        /// <returns></returns>
        IDataReader GetMenuByName(string menuName);

        /// <summary>
        /// Insere o novo menu na base de dados
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="ativo"></param>
        /// <param name="toolTip"></param>
        /// <param name="orderm"></param>
        /// <param name="nomeModulo"></param>
        /// <param name="parentMenuId"></param>
        /// <returns></returns>
        int InsertMenu(string nome, bool ativo, string toolTip, int orderm, string nomeModulo, int parentMenuId);

        /// <summary>
        /// Retorna o registro do menu referente ao nome enviado, descartando o id do menu enviado
        /// </summary>
        /// <param name="name"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        IDataReader GetMenuByNameDiscartCurrentMenuId(string name, int menuId);

        /// <summary>
        /// Atualiza menu
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="nome"></param>
        /// <param name="ativo"></param>
        /// <param name="toolTip"></param>
        /// <param name="orderm"></param>
        /// <param name="nomeModulo"></param>
        /// <param name="parentMenuId"></param>
        void UpdateMenu(int menuId, string nome, bool ativo, string toolTip, int orderm, string nomeModulo, int parentMenuId);

        /// <summary>
        /// Deleta permissões do referido Menu
        /// </summary>
        /// <param name="menuId"></param>
        void DeleteMenuPermissionByMenuId(int menuId);

        /// <summary>
        /// Insere permissão para os Menus
        /// </summary>
        /// <param name="perfilId"></param>
        /// <param name="menuId"></param>
        /// <param name="fullControl"></param>
        /// <param name="canInsert"></param>
        /// <param name="canDelete"></param>
        /// <param name="canRead"></param>
        /// <param name="canUpdate"></param>
        /// <param name="liberaMenuPai"></param>
        void InsertMenuPermission(int perfilId, int menuId, bool fullControl, bool canInsert, bool canDelete, bool canRead, bool canUpdate, bool liberaMenuPai);

    }
}
