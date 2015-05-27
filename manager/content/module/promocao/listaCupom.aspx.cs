using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GrupoA.BusinessObject;
using GrupoA.BusinessLogicalLayer;

public partial class content_module_promocao_listaCupom : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Int32 promocaoId = Convert.ToInt32(Request["promocaoId"].ToString());

            gvCupom.DataSource = new PromocaoBLL().CarregarPromocaoCupomPedidoPorPromocao(new Promocao(promocaoId));
            gvCupom.DataBind();
        }
    }

    protected void gvCupom_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PromocaoCupomPedido promocaoCupomPedido = (PromocaoCupomPedido)e.Row.DataItem;

            Label lblCupom = (Label)e.Row.FindControl("lblCupom");
            Label lblPedido = (Label)e.Row.FindControl("lblPedido");
            Label lblNome = (Label)e.Row.FindControl("lblNome");
            Label lblUsuario = (Label)e.Row.FindControl("lblUsuario");

            lblCupom.Text = promocaoCupomPedido.PromocaoCupom.CodigoCupom.ToString();

            if (promocaoCupomPedido.Pedido != null)
            {
                lblPedido.Text = promocaoCupomPedido.Pedido.PedidoId.ToString();
                lblNome.Text = promocaoCupomPedido.Pedido.Usuario.NomeUsuario;
                lblUsuario.Text = promocaoCupomPedido.Pedido.Usuario.UsuarioId.ToString();
            }
            else
            {
                lblPedido.Text = "-";
                lblNome.Text = "-";
                lblUsuario.Text = "-";
            }
        }
    }
}