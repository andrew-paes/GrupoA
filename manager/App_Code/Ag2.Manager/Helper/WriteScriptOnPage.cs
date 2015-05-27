using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;


namespace Ag2.Manager.Helper
{
    /// <summary>
    /// Summary description for WriteScriptOnPage
    /// </summary>
    public class WriteScriptOnPage
    {
        private System.Text.StringBuilder _script;

        public WriteScriptOnPage()
        {
            _script = new System.Text.StringBuilder();
            _script.Append("$(document).ready(function() {").AppendLine();
        }

        public WriteScriptOnPage Add(string script)
        {
            _script.Append(script).AppendLine();

            return this;
        }

        public WriteScriptOnPage AddAlert(string message)
        {
            _script.Append("alert('").Append(message).Append("');").AppendLine();

            return this;
        }

        public void Bind()
        {
            _script.Append("});").AppendLine();

            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.CurrentHandler, ((Page)HttpContext.Current.CurrentHandler).GetType(), DateTime.Now.ToString("ddMMyyyyHHmmssffff"), _script.ToString(), true);
        }
    }
}
