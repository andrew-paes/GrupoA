﻿using System;
using System.Data.Common;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Ag2.Manager;
using Ag2.Manager.Module;
using Ag2.Manager.Module.Info;
using Ag2.Manager.Helper;
using Ag2.Manager.DAL;

/// <summary>
/// Summary description for DeleteUsuario
/// </summary>
public class DeletePerfil
{
    public DeletePerfil()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// Método configurado no XMl do módulo para ser executado quando o usuário executa a ação de delete no manager
    /// </summary>
    /// <param name="module"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static DeleteRegisterInfo BeforeRegisterDelete(ManagerModuleStructure module, string id)
    {
        IPerfilDAL _perfilDAL = (IPerfilDAL)Util.GetADO("PerfilADO", (System.Reflection.Assembly)System.Web.Compilation.BuildManager.CodeAssemblies[0]);

        DeleteRegisterInfo ObjDelete = new DeleteRegisterInfo();
        Database db = DatabaseFactory.CreateDatabase();
        
        //SETADO COMO FALSE PARA ELE NÃO EXECUTAR O DELETE PADRAO DO MANAGER
        ObjDelete.CanDelete = false;

        Ag2.Manager.Entity.ag2mngPerfil perfil = new Ag2.Manager.Entity.ag2mngPerfil();
        perfil.perfilId = Convert.ToInt32(id);

        _perfilDAL.Delete(perfil);

        return ObjDelete;
    }
}
