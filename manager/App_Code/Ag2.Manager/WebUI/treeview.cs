using System.Data;
using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Text;

namespace Ag2.Manager.WebUI
{
    public class TreeViewEventArgs
    {
        public string[] listIDs;
        public TreeViewEventArgs(string[] plistIDs) { listIDs = plistIDs; }
    }
    public delegate void TreeViewEventHandler(object sender, TreeViewEventArgs e);

    [ValidationProperty("FileName")]
    [DefaultProperty("Value"), ToolboxData("<{0}:TreeView runat=server></{0}:TreeView>")]
    [Serializable]
    public class TreeView : System.Web.UI.WebControls.WebControl, INamingContainer
    {
        public event TreeViewEventHandler Adicionar;
        
        #region PRIVATE VARIABLE

        //### Componentes da Interface ###

        private Label _lblTexto;
        private List<TreeView> _TreeView;
        private List<clsItemTreeView> _DadosTreeView;

        private int _nodoId = 0;
        private int _nodoIdPai = 0;
        private string _Texto = "";

        private string _DataNodoId = "";
        private string _DataNodoIdPai = "";
        private string _DataNodoTitulo = "";

        private DataTable _DataSource;

        private string _sA = "~/img/img_a.JPG";
        private string _sB = "~/img/img_b.JPG";
        private string _sC = "~/img/img_c.JPG";
        private string _sD = "~/img/img_d.JPG";
        private string _sE = "~/img/img_e.JPG";
        private string _sF = "~/img/img_f.JPG";

        #endregion

        #region Propriedades
        

        public string DataNodoId { get { return _DataNodoId; } set { _DataNodoId = value; } }
        public string DataNodoIdPai { get { return _DataNodoIdPai; } set { _DataNodoIdPai = value; } }
        public string DataNodoTitulo { get { return _DataNodoTitulo; } set { _DataNodoTitulo = value; } }

        public DataTable DataSource { get { return _DataSource; } set { _DataSource = value; } }

        private string _OnClientClick = "";
        public string OnClientClick { get { return _OnClientClick; } set { _OnClientClick = value; } }

        #endregion


        /// <summary>
        /// Constructor
        /// </summary>
        public TreeView()
        {
            _lblTexto = new Label();
            _TreeView = new List<TreeView>();
            _DadosTreeView = new List<clsItemTreeView>();

            _lblTexto.ID = "lblTexto";

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        public override void DataBind()
        {
            base.DataBind();
            _DadosTreeView.Clear();
            foreach (DataRow objRow in DataSource.Rows)
            {
                clsItemTreeView objItem = new clsItemTreeView();

                objItem.nodoId = objRow[DataNodoId].ToString().Replace("-", "_");
                objItem.nodoIdPai = (objRow[DataNodoIdPai] is DBNull) ? "0" : objRow[DataNodoIdPai].ToString().Replace("-", "_");
                objItem.Texto = objRow[DataNodoTitulo].ToString();

                _DadosTreeView.Add(objItem);
            }
        }

        /// <summary>
        /// Component Initialization
        /// </summary>
        /// <param name="e">Event</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        /// <summary>
        /// Create Child Controls 
        /// </summary>        
        protected override void CreateChildControls()
        {
            this.Controls.Clear();

            Control child = MontaInterface("0", "", new List<int>());

            if (child != null) this.Controls.Add(child);

            MontaEventos(this.Controls);

            base.CreateChildControls();
            this.ChildControlsCreated = true;
        }

        /// <summary>
        /// Render Method
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            base.RenderChildren(writer);
        }

        protected Control MontaInterface(string nodoIdPai, string strTexto, List<int> lstImg)
        {
            Control ctrl = null;

            List<clsItemTreeView> lstTreeView = _DadosTreeView.FindAll(delegate(clsItemTreeView objItem) { return objItem.nodoIdPai == nodoIdPai; });
            lstTreeView.Sort(delegate(clsItemTreeView x, clsItemTreeView y) { return x.Texto.CompareTo(y.Texto); });

            foreach (clsItemTreeView objItem in lstTreeView)
            {
                if (ctrl == null) ctrl = new Control();

                // VERIFICA SE ESSE ITEM POSSUI FILHOS
                bool bTemFilhos = (_DadosTreeView.Find(delegate(clsItemTreeView objItemAux) { return objItemAux.nodoIdPai == objItem.nodoId; }) != null);

                // PANEL DO ITEM
                Panel pnlItem = new Panel();
                pnlItem.ID = string.Format("pnlItem{0}", objItem.nodoId);

                #region // ESPAÇAMENTO 

                List<int> lstImgItem = new List<int>();
                foreach (int a in lstImg)
                {
                    Image imgE = new Image();
                    lstImgItem.Add(a);
                    switch (a)
                    {
                        case 1:
                            imgE.ImageUrl = ResolveUrl(_sC);
                            break;
                        case 2:
                            imgE.ImageUrl = ResolveUrl(_sF);
                            break;
                    }
                    pnlItem.Controls.Add(imgE);
                }
                Image imgF = new Image();
                if (lstTreeView.IndexOf(objItem) < (lstTreeView.Count - 1))
                {
                    imgF.ImageUrl = ResolveUrl(_sD);
                    lstImgItem.Add(2);
                }
                else
                {
                    imgF.ImageUrl = ResolveUrl(_sE);
                    lstImgItem.Add(1);
                }
                pnlItem.Controls.Add(imgF);

                #endregion

                #region // IMAGEM 
                Image img = new Image();
                img.ID = string.Format("img{0}", objItem.nodoId);
                
                if (bTemFilhos)
                {
                    img.ImageUrl = ResolveUrl(_sB);
                    img.Style["cursor"] = "hand";
                }
                else
                {
                    img.ImageUrl = ResolveUrl(_sC);
                    img.Style["cursor"] = "";
                }
                pnlItem.Controls.Add(img);
                #endregion

                #region // TEXTO 
                
                LinkButton lblTexto = new LinkButton();
                lblTexto.ID = string.Format("lblTexto{0}", objItem.nodoId);
                lblTexto.Text = objItem.Texto;
                if (OnClientClick != "") lblTexto.OnClientClick = string.Format("return {0}('{1}');", OnClientClick, objItem.nodoId);
                pnlItem.Controls.Add(lblTexto);
                #endregion

                #region // ITENS 
                Panel pnlItens = new Panel();
                pnlItens.ID = string.Format("pnlItens{0}", objItem.nodoId);

                string strTextoF = strTexto + ((lstTreeView.IndexOf(objItem) == (lstTreeView.Count - 1)) ? "\\" : "|");

                Control ctrlFilhos = MontaInterface(objItem.nodoId, strTextoF, lstImgItem);

                if (ctrlFilhos != null) pnlItens.Controls.Add(ctrlFilhos);
                #endregion

                pnlItem.Controls.Add(pnlItens);
                ctrl.Controls.Add(pnlItem);
            }

            return ctrl;
        }
        /*
        protected Control MontaInterface2()
        {
            Control ctrl = new Control();

            _lblTexto.Text = Texto;
            ctrl.Controls.Add(_lblTexto);

            foreach (TreeView objItem in _TreeView)
            {
                ctrl.Controls.Add(objItem);
            }

            return ctrl;
        }*/

        protected void MontaEventos(ControlCollection objControls)
        {

            foreach (Control objItem in objControls)
            {
                if (objItem is Image && ((Image)objItem).Style["cursor"] == "hand")
                {
                    string strScript = "";
                    strScript += "var obj = getElementById('{0}');";
                    strScript += "if(obj.style.display == 'none'){";
                    strScript += "obj.style.display = '';";
                    strScript += "this.src = '" + ResolveUrl(_sB) + "';";
                    strScript += "}else{";
                    strScript += "obj.style.display = 'none';";
                    strScript += "this.src = '" + ResolveUrl(_sA) + "';";
                    strScript += "}";

                    string strObjName = objItem.ClientID.Replace(objItem.ID, objItem.ID.Replace("img", "pnlItens"));

                    strScript = strScript.Replace("{0}", strObjName);
                    ((Image)objItem).Attributes.Add("onclick", strScript);

                }
                MontaEventos(objItem.Controls);
            }

        }

        protected void btnAdicionar_Click(object sender, ImageClickEventArgs e)
        {
        }

        protected void btnExcluir_Click(object sender, ImageClickEventArgs e)
        {
        }

        protected override void LoadViewState(object savedState)
        {
            List<object> totalState = null;
            if (savedState != null)
            {
                totalState = (List<object>)savedState;
                // Load base state.
                base.LoadViewState(totalState[0]);

                _lblTexto.Text = totalState[1].ToString();
                _DadosTreeView = (List<clsItemTreeView>)totalState[2];

            }
        }

        protected override object SaveViewState()
        {
            List<object> totalState = new List<object>();

            totalState.Clear();
            totalState.Add(base.SaveViewState());
            
            totalState.Add(_lblTexto.Text);
            totalState.Add(_DadosTreeView);

            return totalState;
        }
    }

    [Serializable]
    public class clsItemTreeView
    {
        public string nodoId = "0";
        public string nodoIdPai = "0";
        public string Texto = "";
    }

}
