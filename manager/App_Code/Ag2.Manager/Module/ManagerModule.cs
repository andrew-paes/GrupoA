using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml.Xsl;
using System.Reflection;
using System.Configuration;
using Ag2.Manager.WebUI;
using Ag2.Manager.Module.Info;
using Ag2.Manager.DAL;
using Ag2.Manager.Helper;
using Ag2.Manager.ADO.MSSql;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Runtime.Serialization.Formatters.Binary;
using Ag2.Manager.BusinessObject;

namespace Ag2.Manager.Module
{
    public class ManagerModule
    {
        #region Declaracao de Variaveis
        private Ag2.Manager.DAL.IManagerModuleDAL _managerModuleDb = (Ag2.Manager.DAL.IManagerModuleDAL)Util.GetADO("ManagerModuleADO", System.Reflection.Assembly.GetExecutingAssembly());
        private ManagerModuleStructure _moduleStructure;
        private XPathDocument _moduleXml;
        private ControlCollection _formControls;
        private int _registerId;
        private XPathNavigator xpath;
        private bool _Multilanguage = false;
        private string modulePath = string.Empty;

        #endregion

        #region Propriedades

        public ManagerModuleStructure ModuleStructure
        {
            get { return _moduleStructure; }
        }

        public ControlCollection FormControls
        {
            get { return _formControls; }
            set { _formControls = value; }
        }

        public int RegisterID
        {
            set { _registerId = value; }
            get { return _registerId; }
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Retorna os idiomas ativos
        /// </summary>
        /// <returns></returns>
        public List<Ag2.Manager.BusinessObject.Idioma> GetActiveIdiomas()
        {
            Ag2.Manager.DAL.IIdiomaDAL idiomaADO = (Ag2.Manager.DAL.IIdiomaDAL)Util.GetADO("IdiomaADO", System.Reflection.Assembly.GetExecutingAssembly());
            return idiomaADO.GetActiveIdiomas();
        }

        /// <summary>
        /// Retorna o idioma default do manager
        /// </summary>
        /// <returns></returns>
        public Ag2.Manager.BusinessObject.Idioma GetIdiomaDefault()
        {
            Ag2.Manager.DAL.IIdiomaDAL idiomaADO = (Ag2.Manager.DAL.IIdiomaDAL)Util.GetADO("IdiomaADO", System.Reflection.Assembly.GetExecutingAssembly());
            return idiomaADO.GetIdiomaDefault();
        }

        /// <summary>
        ///  Carrega as definicoes de um modulo do manger
        /// </summary>
        /// <param name="moduleName">Nome do modulo</param>
        public void Load(string moduleName, Assembly assembly)
        {

            //Instancia do módulo corrente
            //IModule module = (IModule)Util.GetInstance(Util.Capitalize(moduleName.ToLower()), assembly);

            //pega caminho dos modulos configurado no Web.config
            modulePath = String.Format("{0}\\App_Data\\module\\{1}/{1}.xml", HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath), moduleName);
            XmlReader xmlReader = null;

            xmlReader = XmlReader.Create(modulePath);

            _moduleXml = new XPathDocument(xmlReader);
            xmlReader.Close();
            xpath = _moduleXml.CreateNavigator();

            //Monta consulta XPath
            XPathNodeIterator moduleNode;



            //instancia modulo
            _moduleStructure = new ManagerModuleStructure();

            Boolean.TryParse(XmlGetAttributeValue(xpath, "/AG2Manager/module/settings/setting[@name='multiLanguage']", "value"), out _Multilanguage);
            _moduleStructure.Multilanguage = _Multilanguage;

            _moduleStructure.Title = XmlGetNodeValue(xpath, "/AG2Manager/module/settings/title");
            _moduleStructure.Events.AssemblyName = XmlGetAttributeValue(xpath, "/AG2Manager/module/settings/setting[@name='assemblyName']", "value");
            _moduleStructure.Events.ClassName = XmlGetAttributeValue(xpath, "/AG2Manager/module/settings/setting[@name='className']", "value");
            _moduleStructure.Name = moduleName;

            //_moduleStructure.DataBaseName = XmlGetNodeValue(xpath, "/AG2Manager/module/settings/databaseName");
            _moduleStructure.DataBaseName = XmlGetAttributeValue(xpath, "/AG2Manager/module/settings/setting[@name='databaseName']", "value");

            //configura paginacao
            _moduleStructure.AllowPaging = Boolean.Parse(XmlGetAttributeValue(xpath, "/AG2Manager/module/settings/setting[@name='paging']", "value"));
            _moduleStructure.PageSize = Byte.Parse(XmlGetAttributeValue(xpath, "/AG2Manager/module/settings/setting[@name='pageSize']", "value"));
            _moduleStructure.TableName = XmlGetAttributeValue(xpath, "/AG2Manager/module/table", "name");
            _moduleStructure.PrimaryKeyField = XmlGetAttributeValue(xpath, "/AG2Manager/module/table", "primaryKey");
            _moduleStructure.SortDefault = XmlGetAttributeValue(xpath, "/AG2Manager/module/table", "sortDefault");

            //events
            moduleNode = XmlGetTreeNode(xpath, "/AG2Manager/module/events/*");
            if (moduleNode != null)
            {
                while (moduleNode.MoveNext())
                {
                    if (moduleNode.Current.GetAttribute("name", "").Equals("beforeRegisterDelete", StringComparison.InvariantCultureIgnoreCase))
                    {
                        _moduleStructure.Events.BeforeRegisterDelete = true;
                    }

                    if (moduleNode.Current.GetAttribute("name", "").Equals("afterRegisterDelete", StringComparison.InvariantCultureIgnoreCase))
                    {
                        _moduleStructure.Events.AfterRegisterDelete = true;
                    }
                }
            }

            //queries customizadas
            moduleNode = XmlGetTreeNode(xpath, "/AG2Manager/module/queries/*");
            if (moduleNode != null)
            {
                while (moduleNode.MoveNext())
                {
                    if (moduleNode.Current.GetAttribute("section", "").Equals("edit"))
                    {
                        _moduleStructure.QueryEdit = moduleNode.Current.GetAttribute("sql", "");
                    }

                    if (moduleName.Equals("Usuario", StringComparison.OrdinalIgnoreCase))
                    {
                        if (Util.GetBadaBaseType() == Util.DataBaseType.SqlServer)
                        {
                            if (moduleNode.Current.GetAttribute("section", "").Equals("listSql", StringComparison.OrdinalIgnoreCase))
                            {
                                _moduleStructure.QueryList = moduleNode.Current.GetAttribute("sql", "");
                            }
                        }
                        else if (Util.GetBadaBaseType() == Util.DataBaseType.Oracle)
                        {
                            if (moduleNode.Current.GetAttribute("section", "").Equals("listOracle", StringComparison.OrdinalIgnoreCase))
                            {
                                _moduleStructure.QueryList = moduleNode.Current.GetAttribute("sql", "");
                            }
                        }
                    }
                    else
                    {
                        if (moduleNode.Current.GetAttribute("section", "").Equals("list"))
                        {
                            _moduleStructure.QueryList = moduleNode.Current.GetAttribute("sql", "");
                        }
                    }

                    Ag2.Manager.BusinessObject.Query q = new Ag2.Manager.BusinessObject.Query(_moduleStructure.Name);

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(modulePath);
                    XmlNodeList nodes = xmlDoc.SelectNodes(string.Format("/AG2Manager/module/queries/query[@section='{0}']",
                        moduleNode.Current.GetAttribute("section", "").ToString()));

                    if (nodes.Count > 0 && nodes[0].ChildNodes.Count > 0)
                    {
                        QueryParameter qp = null;
                        foreach (XmlNode node in nodes[0].ChildNodes)
                        {
                            if (!node.Name.Equals("sql", StringComparison.OrdinalIgnoreCase))
                            {
                                qp = new QueryParameter();
                                qp.Name = node.Attributes["name"].InnerText;
                                qp.Type = node.Attributes["type"].InnerText;
                                qp.Value = node.Attributes["value"].InnerText;

                                q.QueryParameters.Add(qp);
                            }                            
                        }
                    }

                    if (moduleNode.Current.GetAttribute("type", "").Equals(string.Empty, StringComparison.OrdinalIgnoreCase))
                    {
                        q.Type = Ag2.Manager.BusinessObject.Query.QueryType.Sql;
                    }
                    else if (moduleNode.Current.GetAttribute("type", "").Equals("StoredProcedure", StringComparison.OrdinalIgnoreCase))
                    {
                        q.Type = Ag2.Manager.BusinessObject.Query.QueryType.StoredProcedure;
                    }
                    else if (moduleNode.Current.GetAttribute("type", "").Equals("Sql", StringComparison.OrdinalIgnoreCase))
                    {
                        q.Type = Ag2.Manager.BusinessObject.Query.QueryType.Sql;
                    }
                    else
                    {
                        throw new System.Exception("A propriedade TYPE da query deve ser (Sql) ou (StoredProcedure)");
                    }

                    q.Section = moduleNode.Current.GetAttribute("section", "");
                    q.Sql = moduleNode.Current.GetAttribute("sql", "");

                    if (string.IsNullOrEmpty(q.Sql) && moduleNode.Current.SelectChildren("sql", "") != null)
                    {
                        q.Sql = moduleNode.Current.SelectChildren("sql", "").Current.Value;                    
                    }

                    _moduleStructure.Queries.Add(q);
                }
            }

            //Funcionalidades listagem
            moduleNode = XmlGetTreeNode(xpath, "/AG2Manager/module/options/*");
            if (moduleNode != null)
            {
                Ag2.Manager.BusinessObject.Option opt = null;
                while (moduleNode.MoveNext())
                {
                    opt = new Ag2.Manager.BusinessObject.Option();
                    opt.QuerySection = moduleNode.Current.GetAttribute("querySection", "");
                    opt.Name = moduleNode.Current.GetAttribute("name", "");
                    opt.Value = moduleNode.Current.GetAttribute("value", "");
                    _moduleStructure.Options.Add(opt);
                }
            }


            //consulta campos do modulo
            moduleNode = XmlGetTreeNode(xpath, "/AG2Manager/module/fields/*");

            if (moduleNode != null)
            {

                //Cria lista de Campos
                while (moduleNode.MoveNext())
                {
                    //Estrutura do campo do modulo
                    ManagerModuleField moduleField = null;

                    //vefica tipo do campo que está sendo criado
                    if (moduleNode.Current.Name.Equals("Hidden"))
                    {
                        moduleField = CreateFieldHidden(moduleNode.Current);

                    }
                    if (moduleNode.Current.Name.Equals("SubForm", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldSubForm(moduleNode.Current);
                    }
                    else if (moduleNode.Current.Name.Equals("Date", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldDate(moduleNode.Current);

                    }
                    else if (moduleNode.Current.Name.Equals("Password", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldText(moduleNode.Current);

                    }
                    else if (moduleNode.Current.Name.Equals("TextBox", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldText(moduleNode.Current);
                    }
                    else if (moduleNode.Current.Name.Equals("TextArea", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldTextArea(moduleNode.Current);
                    }
                    else if (moduleNode.Current.Name.Equals("UploadImage", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldUpload(moduleNode.Current);

                    }
                    else if (moduleNode.Current.Name.Equals("ListBox", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldListBox(moduleNode.Current);
                    }
                    else if (moduleNode.Current.Name.Equals("EditableListBox", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldEditableListBox(moduleNode.Current);
                    }
                    else if (moduleNode.Current.Name.Equals("CheckBox", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldCheckBox(moduleNode.Current);
                    }

                    else if (moduleNode.Current.Name.Equals("HtmlTextBox", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldHtmlText(moduleNode.Current);
                    }
                    else if (moduleNode.Current.Name.Equals("ImagePreview", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldImagePreview(moduleNode.Current);
                    }



                    //adiciona campo a coleção
                    if (moduleField != null)
                        _moduleStructure.Fields.Add(moduleField);
                }
            }

            //consulta campos do modulo
            moduleNode = XmlGetTreeNode(xpath, "/AG2Manager/module/fields/group/*");
            if (moduleNode != null)
            {

                //Cria lista de Campos
                while (moduleNode.MoveNext())
                {
                    //Estrutura do campo do modulo
                    ManagerModuleField moduleField = null;

                    //vefica tipo do campo que está sendo criado
                    if (moduleNode.Current.Name.Equals("Hidden"))
                    {
                        moduleField = CreateFieldHidden(moduleNode.Current);

                    }
                    else if (moduleNode.Current.Name.Equals("Date", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldDate(moduleNode.Current);

                    }
                    else if (moduleNode.Current.Name.Equals("Password", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldText(moduleNode.Current);

                    }
                    else if (moduleNode.Current.Name.Equals("TextBox", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldText(moduleNode.Current);
                    }
                    else if (moduleNode.Current.Name.Equals("TextArea", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldTextArea(moduleNode.Current);
                    }
                    else if (moduleNode.Current.Name.Equals("UploadImage", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldUpload(moduleNode.Current);

                    }
                    else if (moduleNode.Current.Name.Equals("ListBox", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldListBox(moduleNode.Current);
                    }
                    else if (moduleNode.Current.Name.Equals("EditableListBox", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldEditableListBox(moduleNode.Current);
                    }
                    else if (moduleNode.Current.Name.Equals("CheckBox", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldCheckBox(moduleNode.Current);
                    }

                    else if (moduleNode.Current.Name.Equals("HtmlTextBox", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldHtmlText(moduleNode.Current);
                    }
                    else if (moduleNode.Current.Name.Equals("ImagePreview", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldImagePreview(moduleNode.Current);
                    }

                    //adiciona campo a coleção
                    if (moduleField != null)
                        _moduleStructure.Fields.Add(moduleField);
                }
            }

            //consulta campos do modulo
            moduleNode = XmlGetTreeNode(xpath, "/AG2Manager/module/fields/TabContainer/TabPanel/*");
            if (moduleNode != null)
            {

                //Cria lista de Campos
                while (moduleNode.MoveNext())
                {

                    //Estrutura do campo do modulo
                    ManagerModuleField moduleField = null;

                    //vefica tipo do campo que está sendo criado
                    if (moduleNode.Current.Name.Equals("Hidden"))
                    {
                        moduleField = CreateFieldHidden(moduleNode.Current);

                    }
                    else if (moduleNode.Current.Name.Equals("Date", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldDate(moduleNode.Current);

                    }
                    else if (moduleNode.Current.Name.Equals("Password", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldText(moduleNode.Current);

                    }
                    else if (moduleNode.Current.Name.Equals("TextBox", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldText(moduleNode.Current);
                    }
                    else if (moduleNode.Current.Name.Equals("TextArea", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldTextArea(moduleNode.Current);
                    }
                    else if (moduleNode.Current.Name.Equals("UploadImage", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldUpload(moduleNode.Current);

                    }
                    else if (moduleNode.Current.Name.Equals("ListBox", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldListBox(moduleNode.Current);
                    }
                    else if (moduleNode.Current.Name.Equals("EditableListBox", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldEditableListBox(moduleNode.Current);
                    }
                    else if (moduleNode.Current.Name.Equals("CheckBox", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldCheckBox(moduleNode.Current);
                    }

                    else if (moduleNode.Current.Name.Equals("HtmlTextBox", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldHtmlText(moduleNode.Current);
                    }
                    else if (moduleNode.Current.Name.Equals("ImagePreview", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleField = CreateFieldImagePreview(moduleNode.Current);
                    }

                    //adiciona campo a coleção
                    if (moduleField != null)
                        _moduleStructure.Fields.Add(moduleField);
                }
            }

            //consulta formularios do manager
            moduleNode = XmlGetTreeNode(xpath, "/AG2Manager/module/forms/*");
            if (moduleNode != null)
            {

                //Cria lista de Campos
                while (moduleNode.MoveNext())
                {
                    //Estrutura do campo do modulo
                    ModuleForm moduleForm = null;

                    //vefica tipo do campo que está sendo criado
                    if (moduleNode.Current.Name.Equals("Edit", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleForm = new ModuleForm();
                        moduleForm.Type = ModuleFormType.Edit;
                        moduleForm.FileSource = moduleNode.Current.GetAttribute("src", "");

                    }
                    else if (moduleNode.Current.Name.Equals("List", StringComparison.OrdinalIgnoreCase))
                    {
                        moduleForm = new ModuleForm();
                        moduleForm.Type = ModuleFormType.List;
                        moduleForm.FileSource = moduleNode.Current.GetAttribute("src", "");
                    }

                    //adiciona campo a coleção
                    if (moduleForm != null)
                        _moduleStructure.Forms.Add(moduleForm);
                }
            }


            //consulta filtros do modulo
            moduleNode = XmlGetTreeNode(xpath, "/AG2Manager/module/filters/*");

            //Cria lista de campos para filtro
            if (moduleNode != null)
            {
                while (moduleNode.MoveNext())
                {
                    //Estrutura do campo do modulo
                    ManagerModuleField moduleField = null;

                    moduleField = FindFieldByName(moduleNode.Current.GetAttribute("field", "").ToString());

                    if (moduleField != null)
                    {
                        moduleField.FilterType = moduleNode.Current.GetAttribute("filterType", "").ToString();
                        moduleField.Label = moduleNode.Current.GetAttribute("label", "").ToString();
                        moduleField.FilterExpression = moduleNode.Current.GetAttribute("filterExpression", "").ToString();
                        if (moduleField.FilterType.ToLower().Equals("range"))
                        {
                            ManagerModuleField moduleField2 = null;
                            for (int i = 0; i < 2; i++)
                            {
                                moduleField2 = new ManagerModuleField();
                                moduleField2 = (ManagerModuleField)moduleField.Clone();
                                moduleField2.Name = string.Concat(moduleField2.Name, i.ToString());
                                if (i == 0)
                                {
                                    moduleField2.Label = string.Concat(moduleField2.Label, " (Início) ");

                                    if (moduleField2.FilterExpression.Equals(string.Empty))
                                    {
                                        moduleField2.FilterExpression = moduleField2.DataFieldName;
                                    }

                                    moduleField2.FilterExpression = string.Concat(moduleField2.FilterExpression, " >= ");
                                }
                                if (i == 1)
                                {
                                    moduleField2.Label = string.Concat(moduleField2.Label, " (Fim) ");

                                    if (moduleField2.FilterExpression.Equals(string.Empty))
                                    {
                                        moduleField2.FilterExpression = moduleField2.DataFieldName;
                                    }

                                    moduleField2.FilterExpression = string.Concat(moduleField2.FilterExpression, " <= ");
                                }

                                _moduleStructure.Filters.Add(moduleField2);
                            }
                        }
                        else
                        {
                            _moduleStructure.Filters.Add(moduleField);
                        }
                    }
                }
            }

            //load constraints list
            _moduleStructure.Constraints = LoadConstraints(xpath);

        }

        public DataSet GetData()
        {
            return GetData(null);
        }

        /// <summary>
        /// Consulta os dados da tabela relacionada ao modulo
        /// </summary>
        /// <returns></returns>
        public DataSet GetData(string sql)
        {
            StringBuilder moduleSQL = new StringBuilder();
            StringBuilder moduleSQLWhere = new StringBuilder();
            String SQLAnd = " ";
            String moduleSQLFinal = "";
            DataSet dsResult = null;

            if (sql != null)
            {
                moduleSQL.Append(sql);
            }
            else
            {
                Query query = _moduleStructure.Queries.Find(delegate(Query q1)
                {
                    return q1.Section.Equals("list", StringComparison.OrdinalIgnoreCase);
                });

                if (query != null)
                {

                    if (query.Type == Query.QueryType.Sql)
                        moduleSQL.Append(query.Sql);
                    else
                    {
                        dsResult = _managerModuleDb.GetDataTableByStoredProcedureCommand(_moduleStructure, query);
                    }
                }
                else
                {
                    //verifica se não existe uma query ja definida para a listagem
                    if (_moduleStructure.QueryList.Equals(""))
                    {
                        moduleSQL.Append("SELECT ");

                        //sempre traz a chave primaria no select
                        moduleSQL.Append(_moduleStructure.PrimaryKeyField).Append(",");

                        //adiciona campos que serao exibidos na listagem
                        foreach (ManagerModuleField field in _moduleStructure.Fields)
                        {
                            if (field.ShowInList && !field.DataFieldName.Equals(""))
                                moduleSQL.Append(field.DataFieldName).Append(",");
                        }

                        //remove ultima virgula da separacao de campos
                        moduleSQL.Remove(moduleSQL.Length - 1, 1);

                        //concatena nome da tabela
                        moduleSQL.Append(" FROM ").Append(_moduleStructure.TableName);

                        //ordenacao
                        if (!String.IsNullOrEmpty(_moduleStructure.SortDefault))
                            moduleSQL.Append(" ORDER BY ").Append(_moduleStructure.SortDefault);
                    }
                }
            }

            CurrentSessions cs = new CurrentSessions();

            //verifica se tem valores de filtro preenchidos
            if (cs.CurrentFilters != null)
            {
                foreach (ManagerModuleField filter in cs.CurrentFilters)
                {
                    if (!filter.FilterValue.Equals(""))
                    {
                        switch (filter.Type)
                        {
                            case ManagerModuleFieldType.Hidden:
                            case ManagerModuleFieldType.Text:
                                if (filter.FilterExpression.Equals(""))
                                {
                                    moduleSQLWhere.AppendFormat("{0} {1} LIKE '%{2}%'", SQLAnd, filter.DataFieldName, filter.FilterValue);
                                }
                                else
                                {
                                    if (filter.FilterExpression.IndexOf("=") > -1)
                                        moduleSQLWhere.AppendFormat("{0} {1} '{2}'", SQLAnd, filter.FilterExpression, filter.FilterValue);
                                    else
                                        moduleSQLWhere.AppendFormat("{0} {1} LIKE '%{2}%'", SQLAnd, filter.FilterExpression, filter.FilterValue);
                                }

                                SQLAnd = " AND ";
                                break;

                            case ManagerModuleFieldType.Date:
                                DateTime datData;
                                if (DateTime.TryParse(filter.FilterValue, out datData))
                                {
                                    if (filter.FilterExpression.Equals(""))
                                    {
                                        moduleSQLWhere.AppendFormat("{0}{1} = '{2:yyyy/MM/dd}'", SQLAnd, filter.DataFieldName, Convert.ToDateTime(filter.FilterValue));
                                    }
                                    else
                                    {
                                        moduleSQLWhere.AppendFormat("{0}{1} '{2:yyyy/MM/dd}'", SQLAnd, filter.FilterExpression, Convert.ToDateTime(filter.FilterValue));
                                    }
                                    SQLAnd = " AND ";
                                }
                                break;

                            case ManagerModuleFieldType.CheckBox:
                                if (filter.FilterExpression.Equals(""))
                                {
                                    moduleSQLWhere.AppendFormat("{0}{1} = {2}", SQLAnd, filter.DataFieldName, filter.FilterValue);
                                }
                                else
                                {
                                    moduleSQLWhere.AppendFormat("{0}{1} {2}", SQLAnd, filter.FilterExpression, filter.FilterValue);
                                }
                                SQLAnd = " AND ";
                                break;
                            case ManagerModuleFieldType.ListBox:
                                if (filter.FilterExpression.Equals(""))
                                {
                                    moduleSQLWhere.AppendFormat("{0}{1} = '{2}'", SQLAnd, filter.DataFieldName, filter.FilterValue);
                                }
                                else
                                {
                                    moduleSQLWhere.AppendFormat("{0}{1} '{2}'", SQLAnd, filter.FilterExpression, filter.FilterValue);
                                }
                                SQLAnd = " AND ";
                                break;
                        }
                    }
                }
            }

            if (moduleSQLWhere.Length > 0)
                moduleSQLFinal = _managerModuleDb.addWhere(moduleSQL.ToString(), moduleSQLWhere.ToString());
            else
                moduleSQLFinal = moduleSQL.ToString();

            _managerModuleDb.DataBaseName = _moduleStructure.DataBaseName;
            moduleSQLFinal = HttpContext.Current.Server.HtmlDecode(moduleSQLFinal);

            if (dsResult == null)
                dsResult = _managerModuleDb.GetDataTable(moduleSQLFinal, cs.CurrentFilters);

            return dsResult;
        }

        /// <summary>
        /// Retorna lista de campos que possuem valores duplicados na base
        /// </summary>
        /// <returns></returns>
        public IList VerifyUniqueValues()
        {
            StringBuilder moduleSQL = new StringBuilder();
            StringBuilder moduleSQLWhere = new StringBuilder();
            String SQLAnd = " ", SQLOr = " ";
            String moduleSQLFinal = "";
            IList duplicatedValues = new List<string>();

            LoadFormValues();

            if (this.ModuleStructure.Constraints.Count > 0)
            {

                //verifica se não existe uma query ja definida para a listagem
                if (_moduleStructure.QueryList.Equals(""))
                {
                    moduleSQL.Append("SELECT ");

                    //sempre traz a chave primaria no select
                    moduleSQL.Append(" * ");

                    //concatena nome da tabela
                    moduleSQL.Append(" FROM ").Append(_moduleStructure.TableName);
                }
                else
                {
                    moduleSQL.Append(_moduleStructure.QueryList);
                }

                //verifica se tem valores de filtro preenchidos
                foreach (Constraint constraint in _moduleStructure.Constraints)
                {
                    moduleSQLWhere.AppendFormat("{0} (", SQLOr);
                    foreach (ConstraintField field in constraint.Fields)
                    {
                        if ((field.Field.DataType == DbType.String) || (field.Field.DataType == DbType.StringFixedLength))
                        {
                            moduleSQLWhere.Append(SQLAnd)
                                .Append(field.DbFieldName)
                                .Append(" COLLATE SQL_Latin1_General_CP1_CI_AI LIKE RTRIM(LTRIM(@")
                                .Append(field.Field.Name).Append("))");
                        }
                        else
                        {
                            moduleSQLWhere.Append(SQLAnd)
                                .Append(field.DbFieldName)
                                .Append(" = @")
                                .Append(field.Field.Name);
                        }
                        SQLAnd = " AND ";
                    }
                    SQLAnd = " ";
                    SQLOr = " OR ";
                    moduleSQLWhere.AppendFormat(") ");
                }

                //se tem valor no RegisterID é atualiza
                if (_registerId > 0)
                {
                    if (moduleSQLWhere.Length > 0)
                    {
                        SQLAnd = " AND ";
                        moduleSQLWhere.Insert(0, "(");
                        moduleSQLWhere.Append(")");
                    }
                    moduleSQLWhere.Append(SQLAnd).Append(_moduleStructure.TableName).Append(".").Append(_moduleStructure.PrimaryKeyField).Append(" <> ").Append(_registerId);
                }

                //verifica se existem filtros populaods
                if (!moduleSQLWhere.Length.Equals(0))
                {
                    moduleSQLFinal = _managerModuleDb.addWhere(moduleSQL.ToString(), moduleSQLWhere.ToString());
                }
                else
                {
                    moduleSQLFinal = moduleSQL.ToString();
                }

                //Log do SQL Gerado


                // Retorna dados consultados da base
                DataTable dataValues = _managerModuleDb.GetDataTableUniqueValue(moduleSQLFinal, _moduleStructure.Constraints).Tables[0];


                if (dataValues.Rows.Count > 0)
                {
                    //verifica quais os campos possuem valores duplicados  
                    foreach (Constraint constraint in _moduleStructure.Constraints)
                    {
                        bool isConstraintInvalid = true;
                        foreach (ConstraintField field in constraint.Fields)
                        {
                            isConstraintInvalid = isConstraintInvalid && field.Field.Value.Equals(dataValues.Rows[0][field.Field.DataFieldName].ToString(), StringComparison.InvariantCultureIgnoreCase);
                        }

                        //verify if all fields in constraint have same values 
                        if (isConstraintInvalid)
                        {
                            foreach (ConstraintField field in constraint.Fields)
                            {
                                if (!field.Field.Value.Trim().Equals(""))
                                {
                                    Control formField = FindControlByID(field.Field.Name);
                                    duplicatedValues.Add(formField.ClientID);
                                }
                            }
                        }

                    }
                }
            }

            return duplicatedValues;
        }

        public void ClearForm()
        {
            foreach (Control formControl in _formControls)
            {
                if (formControl.GetType() == typeof(TextBox))
                {
                    ((TextBox)formControl).Text = "";
                }
                else if (formControl.GetType() == typeof(DateField))
                {
                    ((DateField)formControl).Text = "";
                }
                else if (formControl.GetType().BaseType == typeof(ListControl))
                {
                    ((ListControl)formControl).ClearSelection();
                }
                else if (formControl.GetType().ToString().Equals("ASP.ctl_ag2subform_ascx"))
                {
                    Type type = formControl.GetType();

                    string propertyName = "ClearSelection";
                    BindingFlags flags = BindingFlags.InvokeMethod;
                    Binder binder = null;
                    object[] args = null;

                    //Chama o método (ClearSelection) por reflection
                    type.InvokeMember(propertyName, flags, binder, formControl, args);
                }
                else if (formControl is ManageableDropDownList)
                {
                    ((ManageableDropDownList)formControl).ClearSelection();
                }
                else if (formControl.GetType() == typeof(CheckBox))
                {
                    ((CheckBox)formControl).Checked = false;
                }
                else if (formControl.GetType() == typeof(UploadField))
                {
                    ((UploadField)formControl).FileName = "";
                }
            }

        }

        /// <summary>
        /// Remove da tabela os dados com id enviado
        /// </summary>
        public ICollection<DeleteRegisterInfo> DeleteData(List<string> registerDelete)
        {
            Type moduleClass = null;
            MethodInfo beforeRegisterDelete = null;
            MethodInfo afterRegisterDelete = null;
            object[] arguments = new object[2];
            bool deleteRegister = true;

            ICollection<DeleteRegisterInfo> deleteInfo = new System.Collections.Generic.List<DeleteRegisterInfo>();


            //verifica se tem eventos setados para exclusão de registros
            if (this._moduleStructure.Events.AfterRegisterDelete || this._moduleStructure.Events.BeforeRegisterDelete)
            {
                //carrega assembly com definições 
                moduleClass = LoadAssembly();


                //prepara argumentos
                arguments[0] = this._moduleStructure;

                //verifica que eventos serao executados
                if (this._moduleStructure.Events.BeforeRegisterDelete)
                    beforeRegisterDelete = moduleClass.GetMethod("BeforeRegisterDelete");

                if (this._moduleStructure.Events.AfterRegisterDelete)
                    afterRegisterDelete = moduleClass.GetMethod("AfterRegisterDelete");
            }

            for (int i = 0; i < registerDelete.Count; i++)
            {
                //verifica se tem evento para tratamento antes da exclusão
                if (beforeRegisterDelete != null)
                {
                    //seta ID do registros
                    arguments[1] = registerDelete[i].ToString();

                    //chama o metodo
                    DeleteRegisterInfo deleteReturn = (DeleteRegisterInfo)beforeRegisterDelete.Invoke(null, arguments);

                    //adiciona a coleção se tiver messagem para ser exibida
                    if (!String.IsNullOrEmpty(deleteReturn.ErrorMessage))
                        deleteInfo.Add(deleteReturn);

                    deleteRegister = deleteReturn.CanDelete;
                }

                // exclui registro
                if (deleteRegister)
                {
                    _managerModuleDb.DeleteFormData(_moduleStructure, registerDelete[i]);

                    //_moduleStructure.Fields.
                }

                //verifica se tem evento para tratamento antes da exclusão
                if (afterRegisterDelete != null)
                {
                    //seta ID do registros
                    arguments[1] = Convert.ToInt32(registerDelete[i]);

                    //chama o metodo
                    afterRegisterDelete.Invoke(null, arguments);
                }
            }

            //libera o modulo da memoria
            moduleClass = null;


            //retorno  da função
            return deleteInfo;

        }

        /// <summary>
        /// Associate control fields in ASP.NET form with XML fields
        /// </summary>
        private void FieldAssociation()
        {
            foreach (ManagerModuleField field in _moduleStructure.Fields)
            {
                Control fieldControl = FindControlByID(field.Name);
                if (fieldControl != null)
                {
                    field.FormField = fieldControl;
                }
            }
        }

        /// <summary>
        /// Cria campos para formulário de inclusão transformando o XML via XSL
        /// </summary>
        /// <returns></returns>
        public void CreateForm()
        {
            Page currentPage = (Page)System.Web.HttpContext.Current.Handler;
            Control ctrl = null;
            string modulePath = String.Format("{0}/content/module", HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath));

            // verifica se existe um formulario especifico para o modulo
            if (_moduleStructure.Forms.Count == 0)
            {
                // Load the xslt to do the transformations
                XslCompiledTransform transform = new XslCompiledTransform();
                XsltSettings transformSettings = new XsltSettings(false, true);
                XsltSettings.Default.EnableScript = true;

                transform.Load(string.Concat(modulePath, "/form.xsl"), transformSettings, null);

                // Get the transformed result

                StringWriter sw = new StringWriter();
                transform.Transform(_moduleXml, null, sw);
                string result = sw.ToString();

                // remove the namespace attribute
                result = result.Replace("xmlns:asp=\"remove\"", "");
                result = result.Replace("xmlns:ag2=\"remove\"", "");
                result = result.Replace("xmlns:msxsl=\"urn:schemas-microsoft-com:xslt\" xmlns:user=\"http://mycompany.com/mynamespace\"", "");


                //fecha stream
                sw.Close();

                ctrl = currentPage.ParseControl(result);
            }
            else
            {
                // carrega formulario bom base em um controle
                ctrl = currentPage.LoadControl(string.Concat("module/", _moduleStructure.Name, "/", _moduleStructure.Forms[0].FileSource));

                //esconde barra de botoes
                FindControlRecursive(currentPage.Controls[0], "buttonBar").Visible = false;
            }

            FindControlRecursive(currentPage.Controls[0], "fieldsHolder").Controls.Add(ctrl);
            this._formControls = FindControlRecursive(currentPage.Controls[0], "fieldsHolder").Controls[0].Controls;

            //make association into FORM FIELDS and XML FIELDS
            FieldAssociation();

            //make form validation
            if (_moduleStructure.Forms.Count == 0)
            {

                string scriptValidation = CreateFormValidation();
                HtmlGenericControl scriptTag = new HtmlGenericControl("script");
                scriptTag.Attributes.Add("language", "javascript");
                scriptTag.InnerHtml = scriptValidation;
                FindControlRecursive(currentPage.Controls[0], "fieldsHolder").Controls.Add(scriptTag);
            }

        }

        /// <summary>
        /// Create Javascript to mask fields in the form
        /// </summary>
        /// <returns></returns>
        public string CreateFormValidation()
        {
            StringBuilder scriptValidation = new StringBuilder();
            //scriptValidation.AppendLine("var formRestrict = new Restrict('aspnetForm');");

            foreach (ManagerModuleField field in _moduleStructure.Fields)
            {
                if (field.Type == ManagerModuleFieldType.Text)
                {
                    if (!((ManagerModuleFieldText)field).InputMask.Equals(string.Empty))
                    {
                        ((TextBox)field.FormField).CssClass = string.Format("{0} {1}",
                                    ((TextBox)field.FormField).CssClass,
                                    ((ManagerModuleFieldText)field).InputMask);
                    }
                }
            }



            return scriptValidation.ToString();
        }

        /// <summary>
        /// Carrega as definições do formulario
        /// </summary>
        public void ConfigureForm()
        {
            int fieldIndex = -1;

            //popular combos do formulario
            foreach (Control formField in _formControls)
            {
                if (formField is AjaxControlToolkit.TabContainer)
                {
                    foreach (AjaxControlToolkit.TabPanel tab in formField.Controls)
                    {
                        foreach (Control tabItem in tab.Controls)
                        {
                            foreach (Control item in tabItem.Controls)
                            {
                                fieldIndex = this.FindFieldIndexByName(item.ID);

                                //listbox
                                if (item.GetType().BaseType == typeof(ListControl))
                                {
                                    //Configurar propriedades do ComboBox se ele nao e filtrado por outro
                                    if (((ManagerModuleFieldListBox)_moduleStructure.Fields[fieldIndex]).TriggerField == null)
                                    {
                                        if (!String.IsNullOrEmpty(((ManagerModuleFieldListBox)_moduleStructure.Fields[fieldIndex]).DataSource))
                                        {
                                            ((ListControl)item).AppendDataBoundItems = true;
                                            ((ListControl)item).DataSource = GetComboData((ManagerModuleFieldListBox)_moduleStructure.Fields[fieldIndex]);
                                            ((ListControl)item).DataBind();

                                            if ((item.GetType() == typeof(ListBox)) && ((ManagerModuleFieldListBox)_moduleStructure.Fields[fieldIndex]).MultiSelectType == ManagerModuleMultiSelectType.Single)
                                                ((ListControl)item).Items.Insert(0, (new ListItem(":: selecione ::", "")));
                                        }
                                    }

                                    //veririca se o combo é utilizado para filtrar outro combo
                                    if (!((ManagerModuleFieldListBox)_moduleStructure.Fields[fieldIndex]).FilterListBox.Equals(""))
                                    {
                                        ((ListControl)item).AutoPostBack = true;
                                        ((ListControl)item).SelectedIndexChanged += new EventHandler(this.ListBoxFilterEvent);
                                    }
                                }
                            }
                        }
                    }
                }

                //procura campo na coleção
                fieldIndex = this.FindFieldIndexByName(formField.ID);

                //verifica se o objeto encontrado existe na coleção de campos do manager
                if (fieldIndex >= 0)
                {
                    //desabilita todos os objetos
                    //((WebControl)formField).Enabled = true;

                    //listbox
                    if (formField.GetType().BaseType == typeof(ListControl))
                    {
                        //Configurar propriedades do ComboBox se ele nao e filtrado por outro
                        if (((ManagerModuleFieldListBox)_moduleStructure.Fields[fieldIndex]).TriggerField == null)
                        {
                            if (!String.IsNullOrEmpty(((ManagerModuleFieldListBox)_moduleStructure.Fields[fieldIndex]).DataSource))
                            {
                                ((ListControl)formField).AppendDataBoundItems = true;
                                ((ListControl)formField).DataSource = GetComboData((ManagerModuleFieldListBox)_moduleStructure.Fields[fieldIndex]);
                                ((ListControl)formField).DataBind();

                                if ((formField.GetType() == typeof(ListBox)) && ((ManagerModuleFieldListBox)_moduleStructure.Fields[fieldIndex]).MultiSelectType == ManagerModuleMultiSelectType.Single)
                                    ((ListControl)formField).Items.Insert(0, (new ListItem(":: selecione ::", "")));
                            }
                        }

                        //veririca se o combo é utilizado para filtrar outro combo
                        if (!((ManagerModuleFieldListBox)_moduleStructure.Fields[fieldIndex]).FilterListBox.Equals(""))
                        {
                            ((ListControl)formField).AutoPostBack = true;
                            ((ListControl)formField).SelectedIndexChanged += new EventHandler(this.ListBoxFilterEvent);
                        }
                    }

                    //else if 
                    //campo de data

                }
            }
        }

        public void ListBoxFilterEvent(object sender, EventArgs e)
        {
            //pega ID do combo que disparou o evento
            string senderName = ((ListBox)sender).ID;

            //procura combo na coleção
            int fieldIndex = FindFieldIndexByName(senderName);

            //verifica se o objeto foi encontrado na coleção
            if (fieldIndex >= 0)
            {
                //pega valor selecionado no combo
                string selectedValue = ((ListBox)sender).SelectedValue;

                //verifica que objeto ele estará filtrando
                string targetName = ((ManagerModuleFieldListBox)_moduleStructure.Fields[fieldIndex]).FilterListBox;

                //garente que a propriedade foi preenchida
                if (!targetName.Equals(""))
                {

                    //pega campo que será utilizado para filtro da consulta
                    string filterField = ((ManagerModuleFieldListBox)_moduleStructure.Fields[fieldIndex]).FilterByField;

                    //procura na coleção campo que será alvo do filtro
                    fieldIndex = FindFieldIndexByName(targetName);

                    //
                    ListBox targetListBox = (ListBox)FindControlByID(targetName);

                    if (targetListBox != null)
                    {
                        targetListBox.DataSource = GetComboData((ManagerModuleFieldListBox)_moduleStructure.Fields[fieldIndex], filterField, selectedValue);
                        targetListBox.DataBind();
                        targetListBox.Items.Insert(0, (new ListItem(":: selecione ::", "")));
                    }

                }

            }
        }

        public void LoadFormValues()
        {
            //int fieldIndex = -1;
            //foreach (Control formField in _formControls)
            //foreach (ManagerModuleField   _moduleStructure.Fields in 
            for (int fieldIndex = 0; fieldIndex < this._moduleStructure.Fields.Count; fieldIndex++)
            {

                //procura na coleção de campos no manager,um que possua o mesmo nome
                //fieldIndex = this.FindFieldIndexByName(formField.ID);

                //Verifica se o campo foi encontrado
                //if (fieldIndex >= 0)
                //{
                Control formField = this._moduleStructure.Fields[fieldIndex].FormField;
                switch (_moduleStructure.Fields[fieldIndex].Type)
                {

                    case ManagerModuleFieldType.Date:
                        if (!((DateField)formField).Text.Trim().Equals(""))
                        {
                            string[] dateArray = ((DateField)formField).Text.Split('/');
                            _moduleStructure.Fields[fieldIndex].Value = string.Concat(dateArray[2], "/", dateArray[1], "/", dateArray[0]);
                        }
                        break;

                    case ManagerModuleFieldType.Text:
                    case ManagerModuleFieldType.TextArea:
                        _moduleStructure.Fields[fieldIndex].Value = ((TextBox)formField).Text;
                        break;

                    case ManagerModuleFieldType.HtmlTextBox:
                        _moduleStructure.Fields[fieldIndex].Value = ((IHtmlTextBox)formField).Text;
                        break;

                    case ManagerModuleFieldType.ListBox:
                        ((ManagerModuleFieldListBox)_moduleStructure.Fields[fieldIndex]).SelectedItems.Clear();
                        foreach (ListItem item in ((ListControl)formField).Items)
                        {
                            if (item.Selected)
                                ((ManagerModuleFieldListBox)_moduleStructure.Fields[fieldIndex]).SelectedItems.Add(item.Value);
                        }
                        break;

                    case ManagerModuleFieldType.SubForm:
                        ((ManagerModuleFieldSubForm)_moduleStructure.Fields[fieldIndex]).SelectedItems.Clear();

                        GridView grid = (GridView)formField.FindControl("gvSubForm");

                        foreach (GridViewRow row in grid.Rows)
                        {
                            ((ManagerModuleFieldSubForm)_moduleStructure.Fields[fieldIndex]).SelectedItems.Add(row.Cells[1].Text);
                        }

                        break;

                    case ManagerModuleFieldType.EditableListBox:
                        ((ManagerModuleFieldListBoxEditable)_moduleStructure.Fields[fieldIndex]).SelectedItems.Clear();
                        foreach (ListItem item in ((ManageableDropDownList)formField).Items)
                        {
                            if (item.Selected)
                                ((ManagerModuleFieldListBoxEditable)_moduleStructure.Fields[fieldIndex]).SelectedItems.Add(item.Value);
                        }
                        break;

                    case ManagerModuleFieldType.CheckBox:
                        _moduleStructure.Fields[fieldIndex].Value = ((CheckBox)formField).Checked ? "S" : "N";
                        break;

                    case ManagerModuleFieldType.Upload:
                        _moduleStructure.Fields[fieldIndex].Value = ((UploadField)formField).FileName;
                        break;


                }
                //}
            }
        }

        /// <summary>
        /// Salva os dados do formulario
        /// </summary>
        public bool SaveForm()
        {

            //carrega para o componente os dados do form
            LoadFormValues();

            //salva os dados do modulo
            return _managerModuleDb.SaveFormData(_moduleStructure, _registerId);

        }

        /// <summary>
        /// Preenche dados do formulario que está sendo editado
        /// </summary>
        public void FillForm()
        {
            if (!_moduleStructure.IsEditCustomForm)
            {
                DataTable formData = _managerModuleDb.FillForm(_moduleStructure, RegisterID);
                ManagerModuleField managerField = null;

                int fieldIndex = -1;

                foreach (Control formField in _formControls)
                {
                    //CASO EXISTA UM CONTAINER DE ABAS
                    if (formField is AjaxControlToolkit.TabContainer)
                    {
                        foreach (AjaxControlToolkit.TabPanel tab in formField.Controls)
                        {
                            foreach (Control item in tab.Controls[0].Controls)
                            {
                                LoadValueFields(item, formData, managerField, fieldIndex);
                            }
                        }
                    }
                    else
                    {
                        LoadValueFields(formField, formData, managerField, fieldIndex);
                    }
                }
            }

        }

        private void LoadValueFields(Control formField, DataTable formData, ManagerModuleField managerField, int fieldIndex)
        {
            //procura campo na coleção
            fieldIndex = this.FindFieldIndexByName(formField.ID);

            //Verifica se o campo foi encontrado
            if (fieldIndex >= 0)
            {
                //carrega referencia ao campo do modulo
                managerField = _moduleStructure.Fields[fieldIndex];

                if (formData.Rows.Count.Equals(0))
                    return;

                switch (_moduleStructure.Fields[fieldIndex].Type)
                {

                    case ManagerModuleFieldType.Date:
                        if (formData.Rows[0][managerField.DataFieldName] != DBNull.Value)
                        {
                            managerField.Value = formData.Rows[0][managerField.DataFieldName].ToString().Trim();
                            ((DateField)formField).Text = Convert.ToDateTime(managerField.Value).ToString("dd/MM/yyyy");
                        }
                        break;

                    case ManagerModuleFieldType.Text:
                    case ManagerModuleFieldType.TextArea:


                        managerField.Value = formData.Rows[0][managerField.DataFieldName].ToString().Trim();
                        ((TextBox)formField).Text = managerField.Value;

                        break;

                    case ManagerModuleFieldType.HtmlTextBox:
                        managerField.Value = formData.Rows[0][managerField.DataFieldName].ToString().Trim();
                        ((IHtmlTextBox)formField).Text = managerField.Value;

                        break;



                    case ManagerModuleFieldType.ListBox:

                        ListControl listBoxField = ((ListControl)formField);
                        if (((ManagerModuleFieldListBox)managerField).TriggerField != null)
                        {
                            listBoxField.DataSource = GetComboData((ManagerModuleFieldListBox)managerField);
                            listBoxField.DataBind();

                        }

                        //verifica se é um campo multiselect                            
                        if (((ManagerModuleFieldListBox)managerField).MultiSelectType == ManagerModuleMultiSelectType.Single)
                        {
                            //listBoxField.Items.Insert(0, (new ListItem(":: selecione ::", "")));
                            managerField.Value = formData.Rows[0][((ManagerModuleFieldListBox)managerField).DataFieldName].ToString();
                            listBoxField.SelectedValue = managerField.Value;
                        }
                        else
                        {
                            PopulateListBox((ManagerModuleFieldListBox)managerField, (ListControl)listBoxField);
                        }
                        break;

                    case ManagerModuleFieldType.SubForm:

                        PopulateSubForm((ManagerModuleFieldSubForm)managerField, formField);
                        break;

                    case ManagerModuleFieldType.EditableListBox:
                        ManageableDropDownList manageabledropdownlist = ((ManageableDropDownList)formField);


                        managerField.Value = formData.Rows[0][((ManagerModuleFieldListBoxEditable)managerField).DataValueField].ToString();
                        manageabledropdownlist.SelectedValue = managerField.Value;

                        break;

                    case ManagerModuleFieldType.CheckBox:
                        managerField.Value = Convert.ToBoolean(formData.Rows[0][managerField.DataFieldName]) ? "S" : "N";
                        ((CheckBox)formField).Checked = managerField.Value.Equals("S");
                        break;

                    case ManagerModuleFieldType.Upload:
                        managerField.Value = formData.Rows[0][managerField.DataFieldName].ToString();
                        ((UploadField)formField).FileName = managerField.Value;
                        break;

                    case ManagerModuleFieldType.ImagePreview:
                        managerField.Value = formData.Rows[0][managerField.DataFieldName].ToString().Trim();
                        Image img = ((Image)formField);
                        img.ImageUrl = System.Configuration.ConfigurationManager.AppSettings[img.ImageUrl] + managerField.Value;
                        break;
                }
            }

        }

        private void PopulateSubForm(ManagerModuleFieldSubForm managerModuleFieldSubForm, Control formField)
        {
            string SQL = string.Concat(" SELECT ", managerModuleFieldSubForm.DataValueField, " FROM ", managerModuleFieldSubForm.MultiSelectTable, " WHERE ", _moduleStructure.PrimaryKeyField, " = ", _registerId);
            DataTable multiSelectValue = _managerModuleDb.GetDataTable(SQL).Tables[0];

            List<int> ids = new List<int>();

            foreach (DataRow row in multiSelectValue.Rows)
            {
                ids.Add(Convert.ToInt32(row[0].ToString()));
            }

            Type type = formField.GetType();

            string propertyName = "SetSessionIds";
            BindingFlags flags = BindingFlags.InvokeMethod;
            Binder binder = null;
            object[] args = { ids };

            //Chama o método (ClearSelection) por reflection
            object retorno = type.InvokeMember(propertyName, flags, binder, formField, args);

        }

        private void PopulateListBox(ManagerModuleFieldListBox managerField, ListControl listBoxForm)
        {
            string SQL = string.Concat(" SELECT ", managerField.DataValueField, " FROM ", managerField.MultiSelectTable, " WHERE ", _moduleStructure.PrimaryKeyField, " = ", _registerId);
            DataTable multiSelectValue = _managerModuleDb.GetDataTable(SQL).Tables[0];


            foreach (DataRow row in multiSelectValue.Rows)
            {
                ListItem item = listBoxForm.Items.FindByValue(row[managerField.DataValueField].ToString());
                if (item != null)
                    item.Selected = true;
            }

            //destroi objetos
            multiSelectValue.Dispose();

        }

        /// <summary>
        /// Search a field in collection by name
        /// </summary>
        /// <param name="fieldName">Field Name</param>
        /// <returns>Index of field in collection</returns>
        public Int32 FindFieldIndexByName(string fieldName)
        {
            int fieldIndex = -1;

            for (int i = 0; i < _moduleStructure.Fields.Count; i++)
            {
                if (_moduleStructure.Fields[i].Name.Equals(fieldName))
                {
                    fieldIndex = i;
                    break;
                }
            }

            //return the index of field
            return fieldIndex;
        }

        /// <summary>
        /// Search a field in collection by name
        /// </summary>
        /// <param name="fieldName">Field Name</param>
        /// <returns>Instace of field if this is find.</returns>
        public ManagerModuleField FindFieldByName(string fieldName)
        {
            int fieldIndex = -1;

            for (int i = 0; i < _moduleStructure.Fields.Count; i++)
            {
                if (_moduleStructure.Fields[i].Name.Equals(fieldName))
                {
                    fieldIndex = i;
                    break;
                }
            }

            //return the index of field            
            return (fieldIndex == -1 ? null : _moduleStructure.Fields[fieldIndex]);
        }

        /// <summary>
        /// Procura na coleção de controls do formulario
        /// </summary>
        /// <param name="ID">Nome do controle a ser procurado</param>
        /// <returns>Controle a ser pesquisa</returns>
        public Control FindControlByID(string ID)
        {
            Control controlFind = null;
            foreach (Control formField in _formControls)
            {
                //SE FOR TABCONTAINER
                if (formField is AjaxControlToolkit.TabContainer)
                {
                    foreach (AjaxControlToolkit.TabPanel tab in formField.Controls)
                    {
                        foreach (Control item in tab.Controls[0].Controls)
                        {
                            if (item.ID == ID)
                            {
                                controlFind = item;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    if (formField.ID == ID)
                    {
                        controlFind = formField;
                        break;
                    }
                }
            }

            return controlFind;
        }

        /// <summary>
        /// Retorna um dataset com conteudo montado conforme configuração do ListBox
        /// </summary>
        /// <param name="managerField">Objeto com definições de montagem do combo</param>
        /// <returns>Conteúdo da tabela a que ele faz referencia</returns>
        public DataSet GetComboData(ManagerModuleFieldListBox managerField)
        {
            return _managerModuleDb.GetComboData(managerField, _moduleStructure);
        }

        public DataSet GetComboData(ManagerModuleFieldListBox managerField, string filterField, string filterValue)
        {
            return _managerModuleDb.GetComboData(managerField, filterField, filterValue, _moduleStructure);
        }

        public string XmlGetNodeValue(XPathNavigator xml, string path)
        {
            XPathExpression xmlQuery;
            XPathNodeIterator nodeSelected;

            //consulta campos do modulo
            xmlQuery = xml.Compile(path);
            nodeSelected = xml.Select(xmlQuery);

            if (nodeSelected.Count > 0)
            {
                nodeSelected.MoveNext();
                return nodeSelected.Current.Value;
            }
            else
            {
                return String.Empty;
            }

        }

        public XPathNodeIterator XmlGetTreeNode(XPathNavigator xml, string path)
        {
            XPathExpression xmlQuery;
            XPathNodeIterator nodeSelected;

            //consulta campos do modulo
            xmlQuery = xml.Compile(path);
            nodeSelected = xml.Select(xmlQuery);

            if (nodeSelected.Count > 0)
            {
                return nodeSelected;
            }
            else
            {
                return null;
            }

        }

        public string XmlGetAttributeValue(XPathNavigator xml, string path, string attributeName)
        {
            XPathExpression xmlQuery;
            XPathNodeIterator nodeSelected;

            //consulta campos do modulo
            xmlQuery = xml.Compile(path);
            nodeSelected = xml.Select(xmlQuery);

            if (nodeSelected.Count > 0)
            {
                nodeSelected.MoveNext();
                return nodeSelected.Current.GetAttribute(attributeName, "");
            }
            else
            {
                return String.Empty;
            }

        }

        /// <summary>
        /// Finds a Control recursively. Note finds the first match and exists
        /// </summary>
        /// <param name="ContainerCtl"></param>
        /// <param name="IdToFind"></param>
        /// <returns></returns>
        public static Control FindControlRecursive(Control Root, string Id)
        {
            if (Root.ID == Id)
                return Root;

            foreach (Control Ctl in Root.Controls)
            {

                Control FoundCtl = FindControlRecursive(Ctl, Id);
                if (FoundCtl != null)
                    return FoundCtl;

            }
            return null;
        }

        public Type LoadAssembly()
        {
            Type moduleClass = null;

            if (!this._moduleStructure.Events.AssemblyName.Equals(String.Empty))
            {
                Assembly moduleAssembly = Assembly.Load(this._moduleStructure.Events.AssemblyName);
                moduleClass = moduleAssembly.GetType(this._moduleStructure.Events.ClassName);
            }

            return moduleClass;
        }

        #region Criação de Campos do modulo
        /// <summary>
        /// Cria objeto com estrutura de um campo do tipo texto
        /// </summary>
        /// <param name="fieldStructure">Dados do campo a ser criado</param>
        /// <returns></returns>
        public ManagerModuleField CreateFieldText(XPathNavigator fieldStructure)
        {
            ManagerModuleFieldText moduleField = new ManagerModuleFieldText();

            //carrega definições basicas do campo
            moduleField = (ManagerModuleFieldText)LoadBasicDefinition(fieldStructure, moduleField);

            //carrega definições especificas do tipo de campo
            moduleField.Type = ManagerModuleFieldType.Text;
            moduleField.MaxLength = Int32.Parse(fieldStructure.GetAttribute("maxlength", ""));
            moduleField.InputMask = fieldStructure.GetAttribute("inputMask", "");
            moduleField.CharacterRestriction = fieldStructure.GetAttribute("characterRestriction", "");

            switch (fieldStructure.GetAttribute("contentType", "").ToLower())
            {
                case "currency":
                    moduleField.ContentType = FieldContentType.Currency;
                    break;

                case "cpf":
                    moduleField.ContentType = FieldContentType.CPF;
                    break;

                case "cnpj":
                    moduleField.ContentType = FieldContentType.CNPJ;
                    break;

                case "numeric":
                    moduleField.ContentType = FieldContentType.Numeric;
                    break;

            }

            //log DEBUG                        

            return moduleField;
        }

        public ManagerModuleFieldHtmlText CreateFieldHtmlText(XPathNavigator fieldStructure)
        {
            ManagerModuleFieldHtmlText moduleField = new ManagerModuleFieldHtmlText();

            //carrega definições basicas do campo
            moduleField = (ManagerModuleFieldHtmlText)LoadBasicDefinition(fieldStructure, moduleField);

            //carrega definições especificas do tipo de campo
            moduleField.Type = ManagerModuleFieldType.HtmlTextBox;


            //log DEBUG            

            return moduleField;
        }

        public ManagerModuleField CreateFieldDate(XPathNavigator fieldStructure)
        {
            ManagerModuleFieldDate moduleField = new ManagerModuleFieldDate();

            //carrega definições basicas do campo
            moduleField = (ManagerModuleFieldDate)LoadBasicDefinition(fieldStructure, moduleField);

            //carrega definições especificas do tipo de campo
            moduleField.Type = ManagerModuleFieldType.Date;

            //log DEBUG            

            return moduleField;
        }

        public ManagerModuleField CreateFieldHidden(XPathNavigator fieldStructure)
        {
            ManagerModuleFieldText moduleField = new ManagerModuleFieldText();

            //carrega definições basicas do campo
            moduleField = (ManagerModuleFieldText)LoadBasicDefinition(fieldStructure, moduleField);

            //carrega definições especificas do tipo de campo
            moduleField.Type = ManagerModuleFieldType.Hidden;
            moduleField.Value = fieldStructure.GetAttribute("value", "");
            moduleField.MaxLength = moduleField.Value.Length;


            //log DEBUG            

            return moduleField;
        }

        public ManagerModuleField CreateFieldSubForm(XPathNavigator fieldStructure)
        {
            ManagerModuleFieldSubForm moduleField = new ManagerModuleFieldSubForm();

            //carrega definições basicas do campo
            moduleField = (ManagerModuleFieldSubForm)LoadBasicDefinition(fieldStructure, moduleField);

            //carrega definições especificas do tipo de campo
            moduleField.Type = ManagerModuleFieldType.SubForm;
            moduleField.MultiSelectTable = fieldStructure.GetAttribute("multiSelectTable", "");
            moduleField.DataValueField = fieldStructure.GetAttribute("dataValueField", "");

            String multiSelectType = fieldStructure.GetAttribute("selectType", "");

            if (multiSelectType.Equals("") ||
                multiSelectType.Equals("None", StringComparison.InvariantCultureIgnoreCase) ||
                multiSelectType.Equals("multiple", StringComparison.InvariantCultureIgnoreCase))
            {
                moduleField.MultiSelectType = ManagerModuleMultiSelectType.Multiple;
            }
            else
            {
                moduleField.MultiSelectType = ManagerModuleMultiSelectType.Single;
            }

            XPathNodeIterator moduleNode = fieldStructure.Select("fields/field");
            Ag2.Manager.Entity.FieldSubform field = null;

            foreach (XPathNavigator item in moduleNode)
            {
                field = new Ag2.Manager.Entity.FieldSubform();
                field.DataFieldName = item.GetAttribute("dataFieldName", "");
                field.HeaderTitle = item.GetAttribute("headerTitle", "");

                moduleField.Fields.Add(field);
            }

            //moduleField.Value = fieldStructure.Select("query");

            //XPathNodeIterator moduleNode = fieldStructure.Select("query");

            //foreach (XPathNavigator item in moduleNode)
            //{
            //    moduleField.Query = item.InnerXml;
            //}

            return moduleField;
        }

        public ManagerModuleField CreateFieldListBox(XPathNavigator fieldStructure)
        {
            ManagerModuleFieldListBox moduleField = new ManagerModuleFieldListBox();

            //carrega definições basicas do campo
            moduleField = (ManagerModuleFieldListBox)LoadBasicDefinition(fieldStructure, moduleField);

            //carrega definições especificas do tipo de campo
            moduleField.Type = ManagerModuleFieldType.ListBox;
            moduleField.DataSource = fieldStructure.GetAttribute("dataSource", "");
            moduleField.DataTextField = fieldStructure.GetAttribute("dataTextField", "");
            moduleField.DataValueField = fieldStructure.GetAttribute("dataValueField", "");
            moduleField.MultiSelectTable = fieldStructure.GetAttribute("multiSelectTable", "");

            moduleField.FilterListBox = fieldStructure.GetAttribute("filterListBox", "");
            moduleField.FilterByField = fieldStructure.GetAttribute("filterByField", "");

            String multiSelectType = fieldStructure.GetAttribute("multiSelectType", "");
            if (multiSelectType.Equals("") || multiSelectType.Equals("None", StringComparison.InvariantCultureIgnoreCase) || multiSelectType.Equals("Radio", StringComparison.InvariantCultureIgnoreCase))
            {
                moduleField.MultiSelectType = ManagerModuleMultiSelectType.Single;
            }
            else
            {
                moduleField.MultiSelectType = ManagerModuleMultiSelectType.Multiple;
            }

            //verifica se o campo e' filtrado por outro listbox
            XPathNodeIterator moduleNode = XmlGetTreeNode(xpath, string.Format("/AG2Manager/module/fields/ListBox[@filterListBox='{0}']", moduleField.Name));
            if (moduleNode != null)
            {
                moduleNode.MoveNext();
                int fieldIndex = FindFieldIndexByName(moduleNode.Current.GetAttribute("name", ""));
                if (fieldIndex >= 0)
                {
                    moduleField.TriggerField = (ManagerModuleFieldListBox)_moduleStructure.Fields[fieldIndex];
                }

            }

            //log DEBUG                        

            return moduleField;
        }

        public ManagerModuleField CreateFieldEditableListBox(XPathNavigator fieldStructure)
        {
            ManagerModuleFieldListBoxEditable moduleField = new ManagerModuleFieldListBoxEditable();

            //carrega definições basicas do campo
            moduleField = (ManagerModuleFieldListBoxEditable)LoadBasicDefinition(fieldStructure, moduleField);

            //carrega definições especificas do tipo de campo
            moduleField.Type = ManagerModuleFieldType.EditableListBox;
            moduleField.TableName = fieldStructure.GetAttribute("tableName", "");
            moduleField.DataTextField = fieldStructure.GetAttribute("dataTextField", "");
            moduleField.DataValueField = fieldStructure.GetAttribute("dataValueField", "");

            return moduleField;
        }

        public ManagerModuleFieldCheckBox CreateFieldCheckBox(XPathNavigator fieldStructure)
        {
            ManagerModuleFieldCheckBox moduleField = new ManagerModuleFieldCheckBox();

            //carrega definições basicas do campo
            moduleField = (ManagerModuleFieldCheckBox)LoadBasicDefinition(fieldStructure, moduleField);
            //seta explicitamente o tipo de campo da base (sempre char)
            moduleField.DataType = DbType.StringFixedLength;

            //carrega definições especificas do tipo de campo
            moduleField.Type = ManagerModuleFieldType.CheckBox;



            //log DEBUG            

            return moduleField;
        }

        public ManagerModuleFieldTextArea CreateFieldTextArea(XPathNavigator fieldStructure)
        {
            ManagerModuleFieldTextArea moduleField = new ManagerModuleFieldTextArea();

            //carrega definições basicas do campo
            moduleField = (ManagerModuleFieldTextArea)LoadBasicDefinition(fieldStructure, moduleField);

            moduleField.ShowHtmlBar = fieldStructure.GetAttribute("showHtmlBar", "").Equals("true", StringComparison.OrdinalIgnoreCase);


            //seta explicitamente o tipo de campo da base (sempre char)
            //moduleField.DataType = DbType.Binary;

            //carrega definições especificas do tipo de campo
            moduleField.Type = ManagerModuleFieldType.TextArea;


            //log DEBUG            

            return moduleField;
        }

        public ManagerModuleFieldUpload CreateFieldUpload(XPathNavigator fieldStructure)
        {
            ManagerModuleFieldUpload moduleField = new ManagerModuleFieldUpload();

            //carrega definições basicas do campo
            moduleField = (ManagerModuleFieldUpload)LoadBasicDefinition(fieldStructure, moduleField);

            //seta explicitamente o tipo de campo da base (sempre string)
            moduleField.DataType = DbType.String;
            moduleField.TargetFolder = fieldStructure.GetAttribute("targetFolder", "");

            //carrega definições especificas do tipo de campo
            moduleField.Type = ManagerModuleFieldType.Upload;


            //log DEBUG            

            return moduleField;
        }

        public ManagerModuleFieldImagePreview CreateFieldImagePreview(XPathNavigator fieldStructure)
        {
            ManagerModuleFieldImagePreview moduleField = new ManagerModuleFieldImagePreview();

            //carrega definições basicas do campo
            moduleField = (ManagerModuleFieldImagePreview)LoadBasicDefinition(fieldStructure, moduleField);

            //carrega definições especificas do tipo de campo
            moduleField.Type = ManagerModuleFieldType.ImagePreview;

            moduleField.Caminho = System.Configuration.ConfigurationManager.AppSettings[fieldStructure.GetAttribute("caminho", "")].ToString();
            moduleField.WidthThumb = fieldStructure.GetAttribute("widthThumb", "");



            //log DEBUG            

            return moduleField;
        }

        /// <summary>
        /// Carrega atributos padrão entre campos
        /// </summary>
        /// <param name="fieldStructure">Atributos do campo</param>
        /// <param name="fieldInstance">Instancia do campo a ser populada</param>
        /// <returns></returns>
        private static ManagerModuleField LoadBasicDefinition(XPathNavigator fieldStructure, ManagerModuleField fieldInstance)
        {
            fieldInstance.Label = fieldStructure.GetAttribute("label", "");
            fieldInstance.Name = fieldStructure.GetAttribute("name", "");
            fieldInstance.DataFieldName = fieldStructure.GetAttribute("dataFieldName", "");
            fieldInstance.ListFieldName = fieldStructure.GetAttribute("inListUseField", "");
            fieldInstance.ShowInList = fieldStructure.GetAttribute("showInList", "").Equals("true", StringComparison.OrdinalIgnoreCase);
            fieldInstance.Required = fieldStructure.GetAttribute("required", "").Equals("true", StringComparison.OrdinalIgnoreCase);
            fieldInstance.DataType = GetFieldDbType(fieldStructure.GetAttribute("dbType", ""));
            fieldInstance.Translation = fieldStructure.GetAttribute("translation", "").Equals("true", StringComparison.OrdinalIgnoreCase);
            fieldInstance.Sort = fieldStructure.GetAttribute("sort", "").Equals("true", StringComparison.OrdinalIgnoreCase) ? true : false;

            return fieldInstance;
        }

        /// <summary>
        /// Convert Manager data type to DbType
        /// </summary>
        /// <param name="dataType">The type of data</param>
        /// <returns>DbType relacionado</returns>
        private static DbType GetFieldDbType(string dataType)
        {
            DbType fieldDataType = DbType.String;

            switch (dataType.ToLower())
            {
                case "int":
                case "int32":
                    fieldDataType = DbType.Int32;
                    break;

                case "string":
                    fieldDataType = DbType.String;
                    break;

                case "stringfixedlength":
                    fieldDataType = DbType.StringFixedLength;
                    break;

                case "binary":
                    fieldDataType = DbType.Binary;
                    break;

                case "datetime":
                    fieldDataType = DbType.DateTime;
                    break;

                case "date":
                    fieldDataType = DbType.Date;
                    break;

            }

            //return function
            return fieldDataType;
        }

        private List<Constraint> LoadConstraints(XPathNavigator xml)
        {
            List<Constraint> constraints = new List<Constraint>();

            //carrega nos do XML que possuem referencias de constraints
            XPathNodeIterator fieldsNodes;
            XPathNodeIterator constraintsNodes = XmlGetTreeNode(xml, "/AG2Manager/module/constraints/constraint");

            //verifica se existem constraints
            if (constraintsNodes != null)
            {
                //varre todas as contraints do formulario
                while (constraintsNodes.MoveNext())
                {
                    //instancia constraint 
                    Constraint constraint = new Constraint();
                    constraint.Name = constraintsNodes.Current.GetAttribute("name", "");
                    constraint.Type = ConstraintType.Unique;

                    //carrega campos que fazem parte da constraint
                    fieldsNodes = constraintsNodes.Current.SelectChildren("field", "");

                    //verifica se existem campos
                    if (fieldsNodes != null)
                    {
                        while (fieldsNodes.MoveNext())
                        {
                            ConstraintField field = new ConstraintField();
                            field.Field = FindFieldByName(fieldsNodes.Current.GetAttribute("name", ""));
                            field.DbFieldName = fieldsNodes.Current.GetAttribute("dataFieldName", "");

                            //inclue campo na colecao de constraint
                            constraint.Fields.Add(field);
                        }
                    }

                    //adiciona constraint na coleção
                    constraints.Add(constraint);
                }
            }

            //retorno da funcao
            return constraints;
        }

        public void CarregaListform()
        {
            Page currentPage = (Page)System.Web.HttpContext.Current.Handler;
            Control objForm = FindControlRecursive(currentPage, "holderPrincipal");
            if (objForm != null)
            {
                objForm.Controls.Clear();
                UserControl ctrl = (UserControl)BuscaControleList();

                ctrl.Attributes.Add("ModuleName", this.ModuleStructure.Name);

                objForm.Controls.Add(ctrl);
            }
        }

        private Control BuscaControleList()
        {
            foreach (ModuleForm objModule in this.ModuleStructure.Forms)
            {
                if (objModule.Type == ModuleFormType.List)
                {
                    Page currentPage = (Page)System.Web.HttpContext.Current.Handler;
                    return currentPage.LoadControl(string.Concat("module/", this.ModuleStructure.Name, "/", objModule.FileSource));
                }
            }
            return null;
        }

        #endregion

        #endregion

    }

}
