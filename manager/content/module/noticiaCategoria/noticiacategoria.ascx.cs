using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GrupoA.BusinessLogicalLayer;
using Ag2.Manager.Helper;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;

public partial class content_module_noticiaCategoria_noticiacategoria : System.Web.UI.UserControl
{
    #region Propriedades
    private NoticiaBLL _noticiaBll;
    NoticiaBLL noticiaBLL
    {
        get { return _noticiaBll ?? (_noticiaBll = new NoticiaBLL()); }
    }
    #endregion

    #region Noticias
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Util.GetRequestId() > 0)
        {
            hddCategoriaEventoId.Value = Util.GetRequestId().ToString();
            if (!IsPostBack)
            {
                loadCategoriaNoticia();
            }
        }
    }

    protected void btnExecute_Click(object sender, ImageClickEventArgs e)
    {
        
        if (Page.IsValid)
        {
            //verifica se ja existe uma categoria igual
            List<CategoriaNoticia> categoriaNoticias = (List<CategoriaNoticia>)noticiaBLL.CarregarTodosCategoriaNoticia(0, 0, null, null, new CategoriaNoticiaFH() { NomeCategoriaNoticia = txtNome.Text });
            if (categoriaNoticias != null && categoriaNoticias.Count > 0)
            {
                Util.ShowMessage("Já existe uma Categorina de Notícia com o mesmo nome.");
                return;
            }
            //insere/atualiza
            CategoriaNoticia categoriaNoticia = new CategoriaNoticia();
            categoriaNoticia.NomeCategoriaNoticia = txtNome.Text;

            if (hddCategoriaEventoId.Value == "0")
            {
                noticiaBLL.InserirCategoriaDeNoticia(categoriaNoticia);
                Util.ShowInsertMessage();
                hddCategoriaEventoId.Value = categoriaNoticia.CategoriaNoticiaId.ToString();
            }
            else
            {
                categoriaNoticia.CategoriaNoticiaId = Convert.ToInt32(hddCategoriaEventoId.Value);
                noticiaBLL.AtualizarNoticiaCategoria(categoriaNoticia);
                Util.ShowUpdateMessage();
            }
        }

    }
    #endregion

    #region Métodos
    /// <summary>
    /// Carrega a página com as informações
    /// </summary>
    private void loadCategoriaNoticia()
    {
        var categoriaNoticia = noticiaBLL.CarregarCategoriaNoticia(
                            new CategoriaNoticia() { CategoriaNoticiaId = Convert.ToInt32(hddCategoriaEventoId.Value) });

        txtNome.Text = categoriaNoticia.NomeCategoriaNoticia;
        hddCategoriaEventoId.Value = categoriaNoticia.CategoriaNoticiaId.ToString();
    }
    #endregion
}
