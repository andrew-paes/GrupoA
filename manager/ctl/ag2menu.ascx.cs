using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Ag2.Manager.BusinessObject;
using Ag2.Manager.Helper;


public partial class content_ag2menu : System.Web.UI.UserControl
{
    CurrentSessions cs = new CurrentSessions();

    protected void Page_Init(object sender, EventArgs e)
    {
        StringBuilder sbMenu = new StringBuilder();
        ManagerMenu managerMenu = new ManagerMenu();
        string destinationUrl = string.Empty;
        Ag2.Security.SecureQueryString sq = new Ag2.Security.SecureQueryString();

        //templates de HTML
        //tempalte principal         
        String templateMenu1 = "<div class=\"itemmenu\"><a href=\"javascript:mostrasub('{0}');\">{1}<img src=\"../img/blt_mais.gif\" width=\"25\" height=\"25\" border=\"0\" alt=\"\" class=\"botao\" id=\"bltmenu{0}\" /></a></div>";

        List<Ag2.Manager.Entity.ag2mngMenu> menuItem = GetStructure(cs.User.Menus, 0);

        for (int i = 0; i < menuItem.Count; i++)
        {
            Ag2.Manager.Entity.ag2mngMenu itemRoot = menuItem[i];
            //monta menu de primeiro nivel
            sbMenu.AppendFormat(templateMenu1, itemRoot.menuId, itemRoot.name);

            //verifica se tem segundo nivel para ele
            if (itemRoot.menus.Count > 0)
            {
                sbMenu.AppendFormat("<div id=\"submenu_{0}\" style=\"display:none;\">", itemRoot.menuId.ToString());
                sbMenu.Append("<ul class=\"primary-nav\">\n");

                //MENU DE SEGUNDO NIVEL
                foreach (Ag2.Manager.Entity.ag2mngMenu item2 in itemRoot.menus)
                {
                    sq["md"] = item2.moduleName;
                    destinationUrl = string.Format("~/content/list.aspx?q={0}", sq.ToString());
                    sbMenu.Append("<li class=\"menuparent\">\n");
                    sbMenu.AppendFormat("<a  href=\"{0}\">&#8226; {1}</a>", ResolveUrl(destinationUrl), item2.name);

                    //MENU DE TERCEIRO NIVEL
                    if (item2.menus.Count > 0)
                    {
                        sbMenu.Append("<ul class=\"submenu\">\n");

                        foreach (Ag2.Manager.Entity.ag2mngMenu item3 in item2.menus)
                        {
                            sq["md"] = item3.moduleName;
                            destinationUrl = string.Format("~/content/list.aspx?q={0}", sq.ToString());
                            sbMenu.AppendFormat("<li><a href=\"{0}\">&#8226; {1}</a></li>\n", ResolveUrl(destinationUrl), item3.name);
                        }

                        sbMenu.Append("</ul>\n");
                    }
                    //MENU DE TERCEIRO NIVEL

                    sbMenu.Append("</li>\n");
                }
                //MENU DE SEGUNDO NIVEL

                sbMenu.Append("</ul>\n");
                sbMenu.Append("</div>");
            }
        }

        //Response.Write(sbMenu.ToString());   
        menuItemContainer.Controls.Add(ParseControl(sbMenu.ToString()));

    }

    private List<Ag2.Manager.Entity.ag2mngMenu> GetStructure(List<Ag2.Manager.Entity.ag2mngMenu> menus, int menuId)
    {
        var menulist = from m in menus
                       where m.parentMenuId == menuId
                       select m;

        foreach (var item in menulist)
        {
            item.menus = GetStructure(menus, item.menuId);
        }

        return menulist.ToList();
    }

}

