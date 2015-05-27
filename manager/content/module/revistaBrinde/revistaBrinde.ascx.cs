using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.EnterpriseLibrary.Data;
using GrupoA.BusinessObject;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using System.IO;
using System.Configuration;
using System.Data;
using GrupoA.GlobalResources;

public partial class content_module_revistaBrinde : SmartUserControl
{
    #region [ Properties ]

    private RevistaPacote _revistaPacoteBO;
    protected RevistaPacote RevistaPacoteBO
    {
        get
        {
            if (_revistaPacoteBO == null)
            {
                _revistaPacoteBO = new RevistaPacoteBLL().Carregar(new RevistaPacote() { RevistaPacoteId = Util.GetRequestId() });
            }

            return _revistaPacoteBO;
        }
        set
        {
            _revistaPacoteBO = value;
        }
    }

    #endregion

    #region [ Events ]

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.RevistaPacoteBO != null && this.RevistaPacoteBO.RevistaPacoteId > 0)
        {
            if (!IsPostBack)
            {
                this.LoadForm();
            }
        }

        this.CarregarGridProdutosAdicionados();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPesquisarProduto_Click(object sender, EventArgs e)
    {
        this.CarregarProdutosPorISBN13();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Sender"></param>
    /// <param name="e"></param>
    protected void grids_ItemCreated(Object Sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ImageButton _myButton = (ImageButton)e.Item.FindControl("btnInserir");
            _myButton.Attributes.Add("onclick", "return confirm('Deseja realmente excluir o item selecionado?');");
        }
    }

    /// <summary>
    /// Relaciona a revista com o produto
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dgProdutosEncontrados_EditCommand(object source, DataGridCommandEventArgs e)
    {
        new RevistaPacoteBLL().Inserir(this.RevistaPacoteBO, new Produto() { ProdutoId = Convert.ToInt32(e.CommandArgument) });

        this.CarregarGridProdutosAdicionados();
        //this.CarregarProdutosPorISBN13();

        this.txtISBN13.Text = String.Empty;
        this.pnlProdutosEncontrados.Visible = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dgProdutosAdicionados_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName.ToUpper() == "DELETE")
        {
            new RevistaPacoteBLL().Excluir(this.RevistaPacoteBO, new Produto() { ProdutoId = Convert.ToInt32(e.CommandArgument) });

            this.CarregarGridProdutosAdicionados();

            if (this.pnlProdutosEncontrados.Visible)
            {
                this.CarregarProdutosPorISBN13();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExecute_Click(object sender, EventArgs e)
    {
        this.Page.MaintainScrollPositionOnPostBack = false;
        this.SaveOrUpdate();
    }

    #endregion

    #region [ Methods ]

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    protected void LoadForm()
    {
        if (this.RevistaPacoteBO != null && this.RevistaPacoteBO.RevistaPacoteId > 0)
        {
            this.txtNome.Text = this.RevistaPacoteBO.Nome;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    protected void CarregarProdutosPorISBN13()
    {
        List<Produto> produtoBOList = new List<Produto>();

        if (this.dgProdutosAdicionados.DataKeys.Count > 0)
        {
            foreach (int codigo in this.dgProdutosAdicionados.DataKeys)
            {
                produtoBOList.Add(new Produto() { ProdutoId = codigo });
            }
        }

        produtoBOList = new ProdutoBLL().CarregarProdutosPorIsbn13ExcetoProdutos(txtISBN13.Text, produtoBOList);

        this.dgProdutosEncontrados.DataSource = produtoBOList;
        this.dgProdutosEncontrados.DataKeyField = "produtoId";
        this.dgProdutosEncontrados.DataBind();

        this.pnlProdutosEncontrados.Visible = true;

        if (produtoBOList.Count > 0)
        {
            this.lblTextoPesquisaProdutos.Visible = false;
            this.dgProdutosEncontrados.Visible = true;
        }
        else
        {
            this.lblTextoPesquisaProdutos.Visible = true;
            this.dgProdutosEncontrados.Visible = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    protected void CarregarGridProdutosAdicionados()
    {
        List<Produto> produtoBOList = new ProdutoBLL().Carregar(this.RevistaPacoteBO);

        this.dgProdutosAdicionados.DataSource = produtoBOList;
        this.dgProdutosAdicionados.DataKeyField = "produtoId";
        this.dgProdutosAdicionados.DataBind();

        if (produtoBOList != null && produtoBOList.Any())
        {
            this.dgProdutosAdicionados.Visible = true;
            this.lblTextoProdutosAdicionados.Visible = false;
        }
        else
        {
            this.dgProdutosAdicionados.Visible = false;
            this.lblTextoProdutosAdicionados.Visible = true;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    protected void SaveOrUpdate()
    {
        if (Page.IsValid)
        {
            if (this.RevistaPacoteBO != null && this.RevistaPacoteBO.RevistaPacoteId > 0)
            {
                new RevistaPacoteBLL().Atualizar(this.RevistaPacoteBO);
                Util.ShowUpdateMessage();
            }
        }
    }

    #endregion
}