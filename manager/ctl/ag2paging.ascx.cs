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

public partial class ctl_paginacao : System.Web.UI.UserControl
{
    private readonly string DISPLAY_PAGING = "<strong>{0}</strong> a <strong>{1}</strong> de <strong>{2}</strong>";
    private string _pagingGridId;
    private int _rowsCount;
    private GridView _grid;
    private int _pageIndex;


    protected void Page_Load(object sender, EventArgs e)
    {
        _grid = Parent.FindControl(_pagingGridId) as GridView;
        lblPaging.Text = _grid.PageCount.ToString();
        if (!IsPostBack)
            if (_grid.PageCount > 1)
                LoadCombo();
            else
                phComboPaging.Visible = false;

        showNavigationButtons();
    }

    #region Propriedades
    public string PagingGridId
    {
        set
        {
            _pagingGridId = value;
        }
    }

    public int RowsCount
    {
        get { return _rowsCount; }
        set { _rowsCount = value; }
    }
    #endregion

    #region Metodos

    protected void LoadCombo()
    {
        int pageCount = Convert.ToInt32(lblPaging.Text) + 1;
        for (int i = 1; i < pageCount; i++)
        {
            cmbPaging.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
    }

    public void showNavigationButtons()
    {
        // verifica se pode exibir o botao anterior
        btnPreviousPage.Enabled = btnFirstPage.Enabled = (_grid.PageIndex > 0);
        btnNextPage.Enabled = btnLastPage.Enabled = (_grid.PageIndex < (_grid.PageCount - 1));

        //calcula quais registros estao sendo exibidos
        int firstRegister, lastRegister;
        firstRegister = ((_grid.PageIndex * _grid.PageSize) + 1);
        lastRegister = (firstRegister + _grid.PageSize) - 1;

        //verifica s
        if (lastRegister > _rowsCount)
            lastRegister = _rowsCount;

        if (_rowsCount > 0)
        {
            lblPaging.Text = String.Format(DISPLAY_PAGING, firstRegister, lastRegister, _rowsCount.ToString());
        }
        else
        {
            lblPaging.Text = "Nenhum registro localizado";
        }

    }

    public void refreshGrid()
    {
        // pagina o datagrid
        _grid.DataBind();
    }
    #endregion

    #region Eventos
    protected void btnNextPage_Click(object sender, ImageClickEventArgs e)
    {
        // incrementa pagina do grid
        _pageIndex = _grid.PageIndex;
        _pageIndex++;


        _grid.PageIndex = _pageIndex;
        showNavigationButtons();
        refreshGrid();
        SelectComboPaging();
    }

    protected void btnPreviousPage_Click(object sender, ImageClickEventArgs e)
    {
        // decrementa pagina do grid
        _pageIndex = _grid.PageIndex;
        _pageIndex--;

        /* if (_pageIndex < 0)
             _pageIndex=0;*/

        _grid.PageIndex = _pageIndex;
        showNavigationButtons();
        refreshGrid();
        SelectComboPaging();
    }
    protected void btnFirstPage_Click(object sender, ImageClickEventArgs e)
    {
        _grid.PageIndex = 0;
        showNavigationButtons();
        refreshGrid();
        SelectComboPaging();
    }
    protected void btnLastPage_Click(object sender, ImageClickEventArgs e)
    {
        _grid.PageIndex = _grid.PageCount - 1;
        showNavigationButtons();
        refreshGrid();
        SelectComboPaging();
    }

    protected void cmbPaging_SelectedIndexChanged(object sender, EventArgs e)
    {
        _grid.PageIndex = Convert.ToInt32(cmbPaging.SelectedValue) - 1;
        showNavigationButtons();
        refreshGrid();
    }

    protected void SelectComboPaging()
    {
        cmbPaging.SelectedValue = Convert.ToString(_grid.PageIndex + 1);
    }

    #endregion

}
