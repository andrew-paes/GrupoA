using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class ctl_ag2RelacionarList : System.Web.UI.UserControl
{
    #region --- Propriedades ---

    public string TituloOrigem
    {
        set { lblTitulo1.Text = value; }
    }

    public string TituloDestino
    {
        set { lblTitulo2.Text = value; }
    }

    public ListBox listaOrigem
    {
        get { return lstOrigem; }
    }

    public ListBox listaDestino
    {
        get { return lstDestino; }
    }

    #endregion

    #region --- Métodos ---

    public void adicionarOrigem(ListItem item)
    {
        lstOrigem.Items.Add(item);
    }

    public void adicionarDestino(ListItem item)
    {
        lstDestino.Items.Add(item);

    }
    
    #endregion

    #region --- Métodos Eventos ---

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnAdicionarT_Click(object sender, EventArgs e)
    {
        foreach (ListItem lst in lstOrigem.Items)
        {
            lstDestino.Items.Add(lst);
        }
        lstOrigem.Items.Clear();
    }

    protected void btnAdicionar_Click(object sender, EventArgs e)
    {
        if (lstOrigem.SelectedIndex >= 0)
        {
            lstDestino.Items.Add(lstOrigem.SelectedItem);
            lstOrigem.Items.Remove(lstOrigem.SelectedItem);
            lstDestino.SelectedIndex = -1;
            lstOrigem.SelectedIndex = -1;
        }
    }

    protected void btnRemoverT_Click(object sender, EventArgs e)
    {
        foreach (ListItem lst in lstDestino.Items)
        {
            lstOrigem.Items.Add(lst);
        }
        lstDestino.Items.Clear();
    }

    protected void btnRemover_Click(object sender, EventArgs e)
    {
        if (lstDestino.SelectedIndex >= 0)
        {
            lstOrigem.Items.Add(lstDestino.SelectedItem);
            lstDestino.Items.Remove(lstDestino.SelectedItem);
            lstDestino.SelectedIndex = -1;
            lstOrigem.SelectedIndex = -1;
        }
    }

    #endregion
}
