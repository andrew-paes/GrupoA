using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Text;
using System.Globalization;
using System.Web.Caching;
using Ag2.Manager.BusinessObject;
using GrupoA.GlobalResources;

public partial class test_upload : System.Web.UI.Page
{
    private static bool _reportRemovedFromCache = false;

    protected void Page_Load(object sender, EventArgs e)
    {

        HttpFileCollection uploadedFiles = Request.Files;
		string saveToFolder = Page.ResolveClientUrl(GrupoA_Resource.baseUrlUpload);
        string newFileName = string.Empty;
        string Path = string.Empty;
        try
        {
            if (!String.IsNullOrEmpty(Request.QueryString["path"]))
            {
                saveToFolder += Request.QueryString["path"];
            }

            Path = Server.MapPath(saveToFolder);

            newFileName = Request.QueryString["newFileName"].ToString();

            //cria pastas
            CreateDirectory(Path);


            HttpPostedFile F = uploadedFiles[0];
            if (uploadedFiles[0] != null && F.ContentLength > 0)
            {
                string fileName = F.FileName.ToLower();
                string newName = newFileName;
                string extension = System.IO.Path.GetExtension(newName).Replace(".", "");

                FileInfo img = new FileInfo();

                if (string.Compare("aspx", extension, true) == 0 ||
                    string.Compare("asp", extension, true) == 0 ||
                    string.Compare("bat", extension, true) == 0 ||
                    string.Compare("exe", extension, true) == 0)
                {
                    img.Error = "Arquivo não suportado";
                    CacheAdd(img);
                    return;
                }

                F.SaveAs(string.Concat(Path, "\\", newName));

                img.Name = newName;
                img.Format = extension.ToUpper();
                img.Size = String.Format("{0:n}", (F.ContentLength / 1024));
                img.Url = string.Concat("../", saveToFolder, "/", newName);

                CacheAdd(img);
            }
        }
        catch (Exception exc)
        {
            FileInfo img = new FileInfo();
            img.Error = exc.Message;
            CacheAdd(img);
        }
    }

    private static void CacheAdd(FileInfo img)
    {
        HttpRuntime.Cache.Remove("ImageInfo");
        HttpRuntime.Cache.Add("ImageInfo",
           img, null, Cache.NoAbsoluteExpiration,
           new TimeSpan(0, 0, 0),
           System.Web.Caching.CacheItemPriority.Default,
           null);
    }

    public void CreateDirectory(string path)
    {
        if (!System.IO.Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
        }
    }

    static string stringSanitize(string url)
    {
        url = Regex.Replace(url, @"[\s-]+", "_");
        string stFormD = url.Normalize(NormalizationForm.FormD);

        StringBuilder sb = new StringBuilder();

        for (int ich = 0; ich < stFormD.Length; ich++)
        {

            UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
            if (uc != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(stFormD[ich]);
            }
        }

        return (sb.ToString());
    }

}
