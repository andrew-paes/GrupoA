using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;

public partial class content_module_tituloAreaDestaque_tituloAreaDestaque : SmartUserControl
{
    #region Eventos
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadForm();
        }
    }    
    
    protected void btnExecute_Click(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = false;
        SaveEntidadeTemplate();

    }    

    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        TituloImpresso tituloImpresso = new TituloBLL().CarregarPorISBN13(this.txtISBN13.Text);

        listRel.listaOrigem.Items.Clear();

        if (tituloImpresso != null && (tituloImpresso.Titulo != null && tituloImpresso.Titulo.TituloId > 0))
        {
            Titulo titulo = tituloImpresso.Titulo;
            // CONTROLE DE IDs JA INSERIDOS
            string ids = "";
            foreach (ListItem lst in listRel.listaDestino.Items)
            {
                if (ids.Length == 0)
                    ids = lst.Value;
                else
                    ids += ", " + lst.Value;
            }
            if (!ids.Contains(titulo.TituloId.ToString()))
            {
                listRel.listaOrigem.Items.Add(new ListItem(titulo.NomeTitulo, titulo.TituloId.ToString()));
            }
            else
            {
                Util.ShowMessage("Título com o ISBN13 informado já está relacionado.");
            }
        }
        else
        {
            Util.ShowMessage("Nenhum título encontrado com o ISBN13 informado.");
        }
    }
    #endregion

    #region Métodos

    protected void LoadForm()
    {
        listRel.TituloOrigem = "Lista de Títulos";
        listRel.TituloDestino = "Lista de Títulos Relacionados";
        if (IdRegistro > 0)
        {
            DestaqueTituloImpresso destaqueTituloImpresso = new DestaqueTituloImpressoBLL().CarregarTitulosRelacionados(new DestaqueTituloImpresso() { DestaqueTituloImpressoId = Convert.ToInt32(IdRegistro) });

            //Area de Destaque
            this.txtNomeArea.Text = destaqueTituloImpresso.NomeArea;

            //Titulos
            if (destaqueTituloImpresso.Titulos != null && destaqueTituloImpresso.Titulos.Count > 0)
            {
                foreach (Titulo item in destaqueTituloImpresso.Titulos)
                {
                    listRel.listaDestino.Items.Add(new ListItem(item.NomeTitulo, item.TituloId.ToString()));
                }
            }

        }
    }
    protected void SaveEntidadeTemplate()
    {
        DestaqueTituloImpressoBLL destaqueTituloImpressoBLL = new DestaqueTituloImpressoBLL();
        DestaqueTituloImpresso destaqueTituloImpresso = destaqueTituloImpressoBLL.CarregarTitulosRelacionados(new DestaqueTituloImpresso() { DestaqueTituloImpressoId = Convert.ToInt32(IdRegistro) });
        if (destaqueTituloImpresso.Titulos != null && destaqueTituloImpresso.Titulos.Count > 0)
        {
            destaqueTituloImpressoBLL.ExcluirTitulosRelacionados(destaqueTituloImpresso);
        }

        destaqueTituloImpresso.Titulos = new List<Titulo>();
        foreach (ListItem item in listRel.listaDestino.Items)
        {
            Titulo titulo = new Titulo();
            titulo = new Titulo() { TituloId = Convert.ToInt32(item.Value) };
            destaqueTituloImpresso.Titulos.Add(titulo);            
        }
        destaqueTituloImpressoBLL.InserirTitulosRelacionados(destaqueTituloImpresso);
        Util.ShowUpdateMessage();

    }

    #endregion
}