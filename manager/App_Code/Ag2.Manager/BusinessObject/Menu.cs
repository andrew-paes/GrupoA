using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using Ag2.Manager.ADO.MSSql;
using Ag2.Manager.Helper;
using Ag2.Manager.DAL;

namespace Ag2.Manager.BusinessObject
{
    public class ManagerMenu
    {
        private int _perfilId;
        private IMenuDAL _menuDb = (IMenuDAL)Util.GetADO("MenuADO", System.Reflection.Assembly.GetExecutingAssembly());
        private CurrentSessions cs = new CurrentSessions();

        public ManagerMenu()
        {
        }


        #region Metodos

        /// <summary>
        /// Consulta estrutura de menus cadastrada
        /// </summary>
        /// <returns></returns>
        public List<ManagerMenuItem> GetStructure(int perfilId)
        {
            //coleção de itens do menu
            List<ManagerMenuItem> menuRoot = new List<ManagerMenuItem>();

            //consulta menu raiz            
            DataTable menuInfo = _menuDb.GetByParentId(0, perfilId);

            //monta menu
            foreach (DataRow row in menuInfo.Rows)
            {
                ManagerMenuItem menu = new ManagerMenuItem();
                menu.ID = Convert.ToInt32(row["menuId"].ToString());
                menu.Name = row["name"].ToString();
                menu.DestinationUrl = "";
                GetByParentId(Convert.ToInt32(row["menuId"].ToString()), perfilId, menu.ChildItems);
                //verifica se tem filhos para exibir
                if (menu.ChildItems.Count > 0)
                    menuRoot.Add(menu);
            }

            return menuRoot;
        }

        /// <summary>
        /// Consulta estrutura do menu a partir do seu menu Pai
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public void GetByParentId(int parentId, int perfilId, List<ManagerMenuItem> menuRoot)
        {
            //consulta menu
            DataTable menuInfo = _menuDb.GetByParentId(parentId, perfilId);

            //monta menu
            foreach (DataRow row in menuInfo.Rows)
            {
                ManagerMenuItem menuChild = new ManagerMenuItem();
                menuChild.ID = Convert.ToInt32(row["menuId"].ToString());
                menuChild.Name = row["name"].ToString();
                menuChild.UserCanDelete = Convert.ToBoolean(row["canDelete"]);
                menuChild.UserCanInsert = Convert.ToBoolean(row["canInsert"]);
                menuChild.UserCanRead = Convert.ToBoolean(row["canRead"]);
                menuChild.UserCanUpdate = Convert.ToBoolean(row["canUpdate"]);
                menuChild.UserHasFullControl = Convert.ToBoolean(row["fullControl"]);

                //verifica se de acordo com as permissões o menu pode ser exibido
                if (CanShow(menuChild))
                {
                    Ag2.Security.SecureQueryString sq = new Security.SecureQueryString();
                    sq["md"] = row["moduleName"].ToString();

                    //pega pagina inicial do item de menu
                    //Se tiver permissão somente de insert, o sistema direciona direto para a página de edição
                    if (!menuChild.UserCanDelete && menuChild.UserCanInsert && !menuChild.UserCanRead && !menuChild.UserCanUpdate && !menuChild.UserHasFullControl)
                        menuChild.DestinationUrl = string.Format("~/content/edit.aspx?q={0}", sq.ToString());
                    else
                        menuChild.DestinationUrl = string.Format("~/content/list.aspx?q={0}", sq.ToString());

                    menuRoot.Add(menuChild);
                    GetByParentId(Convert.ToInt32(row["menuId"]), perfilId, menuChild.ChildItems);
                }
            }
        }

        /// <summary>
        /// Verifica se um item de menu pode ser exibido de acordo com suas permissões de acesso
        /// </summary>
        /// <param name="menuItem">Item do menu</param>
        /// <returns>true se o item pode ser exibido</returns>
        public bool CanShow(ManagerMenuItem menuItem)
        {
            return (menuItem.UserHasFullControl || menuItem.UserCanRead || menuItem.UserCanDelete || menuItem.UserCanUpdate || menuItem.UserCanInsert);
        }

        /// <summary>
        /// Define a página inicial de um item de menu de acorod com suas permissões de acesso
        /// </summary>
        /// <param name="menuItem">Item do menu</param>
        /// <returns></returns>
        public string GetDestinationUrl(ManagerMenuItem menuItem)
        {
            string pageName = "";
            //se tem permissao de controle total ou de leitura, abrir sempre na listagem
            if ((menuItem.UserHasFullControl || menuItem.UserCanRead) || (menuItem.UserCanRead && menuItem.UserCanInsert))
            {
                pageName = "list.aspx";
            }
            else if (menuItem.UserCanInsert && !menuItem.UserCanRead)
            {
                pageName = "0edit.aspx";
            }

            return pageName;
        }


        /// <summary>
        /// Salva a configuração de um item de menu, para um determinado perfil
        /// </summary>
        /// <param name="menuItem"></param>
        public void SaveItemPermission(ManagerMenuItem menuItem, int perfilId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Propriedades
        /// <summary>
        /// Propriedade para definir o 
        /// Perfil do usuario que está consultando o menu
        /// </summary>
        public int PerfilId
        {
            get { return _perfilId; }
            set { _perfilId = value; }
        }
        #endregion
    }

}
