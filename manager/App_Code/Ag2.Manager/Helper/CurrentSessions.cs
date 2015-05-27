using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Ag2.Manager.BusinessObject;
using Ag2.Manager.Module;
using System.Web;

namespace Ag2.Manager.Helper
{
    public class CurrentSessions
    {
        public CurrentSessions()
        {
            //
        }
        
        public Ag2.Manager.BusinessObject.Idioma CurrentIdioma
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["CurrentIdioma"] == null)
                    System.Web.HttpContext.Current.Session["CurrentIdioma"] = new Ag2.Manager.BusinessObject.Idioma();
                return (Ag2.Manager.BusinessObject.Idioma)System.Web.HttpContext.Current.Session["CurrentIdioma"];
            }
            set
            {
                System.Web.HttpContext.Current.Session["CurrentIdioma"] = value;
            }
        }

        public Ag2.Manager.Entity.ag2mngUser User
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["_User"] == null)
                    System.Web.HttpContext.Current.Session["_User"] = new Ag2.Manager.Entity.ag2mngUser();
                return (Ag2.Manager.Entity.ag2mngUser)System.Web.HttpContext.Current.Session["_User"];
            }
            set
            {
                System.Web.HttpContext.Current.Session["_User"] = value;
            }
        }

        public List<Int32> RegisterNavigator
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["RegisterNavigator"] == null)
                    System.Web.HttpContext.Current.Session["RegisterNavigator"] = new List<Int32>();
                return (List<Int32>)System.Web.HttpContext.Current.Session["RegisterNavigator"];
            }
            set
            {
                System.Web.HttpContext.Current.Session["RegisterNavigator"] = value;
            }
        }

        public Collection<ManagerModuleField> CurrentFilters
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["Filters"] == null)
                    System.Web.HttpContext.Current.Session["Filters"] = new Collection<ManagerModuleField>();
                return (Collection<ManagerModuleField>)System.Web.HttpContext.Current.Session["Filters"];
            }
            set
            {
                System.Web.HttpContext.Current.Session["Filters"] = value;
            }
        }

        public ManagerModule ActiveManagerModule
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["ManagerModule"] == null)
                    System.Web.HttpContext.Current.Session["ManagerModule"] = new ManagerModule();
                return (ManagerModule)System.Web.HttpContext.Current.Session["ManagerModule"];
            }
            set
            {
                System.Web.HttpContext.Current.Session["ManagerModule"] = value;
            }
        }

        public static void AddContentControl(string controlId, string value)
        {
            Dictionary<string, string> contentValues = null;

            if (HttpContext.Current.Session["dictionaryContentValues"] == null)
            {
                contentValues = new Dictionary<string, string>();
                HttpContext.Current.Session["dictionaryContentValues"] = contentValues;
            }
            else
            {
                contentValues = (Dictionary<string, string>)HttpContext.Current.Session["dictionaryContentValues"];
            }

            if (contentValues.ContainsKey(controlId))
            {
                contentValues.Remove(controlId);
            }

            contentValues.Add(controlId, value);

            HttpContext.Current.Session["dictionaryContentValues"] = contentValues;
        }

        public static string GetContentControl(string controlId)
        {
            Dictionary<string, string> contentValues = null;

            if (HttpContext.Current.Session["dictionaryContentValues"] == null)
            {
                return null;
            }

            contentValues = (Dictionary<string, string>)HttpContext.Current.Session["dictionaryContentValues"];

            if (!contentValues.ContainsKey(controlId))
            {
                return null;
            }

            return contentValues[controlId];
        }

    }
}
