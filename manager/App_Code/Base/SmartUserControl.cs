using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Ag2.Manager.Helper;

/// <summary>
/// Summary description for SmartUserControl
/// </summary>
public class SmartUserControl : System.Web.UI.UserControl
{
    public SmartUserControl() { }

    #region [ Properties ]
    /// <summary>
    /// Retorna o Id do registro que esta em edicao
    /// </summary>
    public int? IdRegistro
    {
        get
        {
            int _idRegistro = 0;
            if (Util.GetRequestId() > 0)
            {
                _idRegistro = Util.GetRequestId();
            }
            else
            {
                _idRegistro = 0;
            }

            return _idRegistro;
        }
    }

    #endregion

	#region [ Validacao de Datas ]

	protected void ServerValidationDateTime(object source, ServerValidateEventArgs args)
	{
		try
		{
			args.IsValid = Ag2.Manager.Helper.Util.IsDate(args.Value);
		}
		catch
		{
			args.IsValid = false;
		}
	}

	#endregion

   
}
