using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Text;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Ag2.Manager.BusinessObject;
using Ag2.Manager.Module;
using Ag2.Manager.Helper;

/// <summary>
/// Classe responsável por dar funcionalidade ao combo de opções da página de listagem
/// </summary>
public class OptionsController
{
    public OptionsController()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// Faz download de arquivo de exportação de listagens
    /// </summary>
    /// <param name="query"></param>
    /// <param name="moduleName"></param>
    public void Export(Query query, string moduleName, ListItem SelectedItem)
    {
        ManagerModule managermodule = new ManagerModule();
        managermodule.Load(moduleName, (System.Reflection.Assembly)System.Web.Compilation.BuildManager.CodeAssemblies[0]);

        foreach (Option item in managermodule.ModuleStructure.Options)
        {
            if (SelectedItem.Text.Equals(item.Name, StringComparison.OrdinalIgnoreCase))
            {
                if (item.Value.Equals("excel", StringComparison.OrdinalIgnoreCase))
                    ExportXLS(query, managermodule);
                if (item.Value.Equals("csv", StringComparison.OrdinalIgnoreCase))
                    ExportCSV(query, managermodule);

                break;
            }
        }


    }

    /// <summary>
    /// Método de exportação para XLS dos registros mostrado na listagem
    /// </summary>
    /// <param name="query"></param>
    protected void ExportXLS(Query query, ManagerModule managermodule)
    {
        DataTable dt = managermodule.GetData(query.Sql).Tables[0];

        Table table = new Table();
        TableRow tr = null;
        TableCell tc = null;

        //Cria linha
        tr = new TableRow();

        //Header
        foreach (DataColumn dc in dt.Columns)
        {
            tc = new TableCell();
            tc.Text = dc.Caption;
            tr.Cells.Add(tc);
        }

        //Adiciona linha na tabela
        table.Rows.Add(tr);

        //Itens
        foreach (DataRow dr in dt.Rows)
        {
            //Cria linha
            tr = new TableRow();

            for (int i = 0; i < table.Rows[0].Cells.Count; i++)
            {
                tc = new TableCell();
                tc.Text = dr[i].ToString();
                tr.Cells.Add(tc);
            }

            //Adiciona linha na tabela
            table.Rows.Add(tr);
        }

        //Traz o render HTML do controle
        string renderHtml = Util.GetHtmlRender(table);

        Util.DownloadFile(renderHtml, "Exportacao.xls");
    }

    /// <summary>
    /// Método de exportação para XLS dos registros mostrado na listagem
    /// </summary>
    /// <param name="query"></param>
    protected void ExportCSV(Query query, ManagerModule managermodule)
    {
        DataTable dt = managermodule.GetData(query.Sql).Tables[0];

        Table table = new Table();
        TableRow tr = null;
        TableCell tc = null;

        //Cria linha
        tr = new TableRow();

        //Header
        foreach (DataColumn dc in dt.Columns)
        {
            tc = new TableCell();
            tc.Text = dc.Caption;
            tr.Cells.Add(tc);
        }

        //Adiciona linha na tabela
        table.Rows.Add(tr);

        //Itens
        foreach (DataRow dr in dt.Rows)
        {
            //Cria linha
            tr = new TableRow();

            for (int i = 0; i < table.Rows[0].Cells.Count; i++)
            {
                tc = new TableCell();
                tc.Text = dr[i].ToString();
                tr.Cells.Add(tc);
            }

            //Adiciona linha na tabela
            table.Rows.Add(tr);
        }

        //Traz o render HTML do controle
        string renderHtml = Util.GetHtmlRender(table);

        Util.DownloadFile(renderHtml, "Exportacao.xls");
    }
}
