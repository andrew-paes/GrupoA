using System;
using System.Collections.Generic;
using System.Web;
using Ag2.Manager.BusinessObject;

/// <summary>
/// Summary description for Noticias
/// </summary>
public class Noticia : Module
{
    public Noticia()
    {
        ////Configura Módulo
        //this.Settings.Title = "Cadastro Notícia";
        //this.Settings.Description = "Descrição noticia";
        //this.Settings.Paging = true;
        //this.Settings.PageSize = 15;

        ////Configuta tabela data base
        //this.ConfigTable.TableName = "noticia";
        //this.ConfigTable.PrimaryKey = "noticiaId";

        ////Monta as queries
        //Query query = new Query();
        //query.Section = "list";
        //query.Sql = "SELECT * FROM noticia";
        //this.Queries.Add(query);

        //query = new Query();
        //query.Section = "export";
        //query.Sql = "SELECT noticiaId as 'Código da Notícia', titulo as 'Título da Notícia' FROM noticia ";
        //this.Queries.Add(query);

        ////Configura controle (ASCX)
        //FormsType formstype = new FormsType();
        //formstype.Type = FormsType.ControlType.control;
        //formstype.Src = "noticia.ascx";
        //this.Forms.Add(formstype);

        ////Configura opções do combo da listagem.
        //Option option = new Option();
        //option.QuerySection = "export";
        //option.Value = "excel";
        //option.Name = "Exportar (Excel)";
        //this.Options.Add(option);
        
        //option = new Option();
        //option.QuerySection = "export";
        //option.Value = "csv";
        //option.Name = "Exportar (CSV)";
        //this.Options.Add(option);

        ////Configura controles do formulário
        //ConfigWebControl ag2control = new ConfigWebControl();
        //ag2control.ControlType = ConfigWebControl.ControlTypeRender.TEXTBOX;
        //ag2control.Label = "Título";
        //ag2control.Maxlength = 80;
        //ag2control.Name = "titulo";
        //ag2control.ShowInList = true;
        //ag2control.Sort = true;
        //this.Fields.Add(ag2control);

        ////Configura os filtros da listagem
        //Filter filter = new Filter();
        //filter.Label = "Título";
        //filter.FieldName = "titulo";
        //this.Filters.Add(filter);
    }

}
