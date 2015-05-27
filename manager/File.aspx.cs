using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Services;
using System.Configuration;
using Ag2.Manager.BusinessObject;
using GrupoA.GlobalResources;

[WebService]
public partial class File : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    [ScriptMethod]
    public static FileInfo ImageInfo()
    {
        //FileInfo img = (FileInfo)HttpContext.Current.Session["ImageInfo"];
        FileInfo img = (FileInfo)HttpRuntime.Cache["ImageInfo"];
        return img;
    }

    [WebMethod]
    [ScriptMethod]
    public static bool DeleteFile(string path)
    {
		path = string.Concat(GrupoA_Resource.baseUrlUpload, path);
        System.IO.File.Delete(HttpContext.Current.Server.MapPath(path));
        return true;
    }
}
