using System;
using System.Collections.Generic;
using System.Text;

namespace Ag2.Manager.DAL
{
    public abstract class IHtmlTextBox : System.Web.UI.UserControl 
    {
        string _text;

        #region Propriedates



        public virtual string Text
        {
             set
            {
                _text = value;

            }
            get
            {
                return _text;

            }
        }

        #endregion
    }
}
