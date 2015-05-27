using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.OracleClient;
using System.Text.RegularExpressions;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Ag2.Manager.Module;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Ag2.Manager.BusinessObject;
using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;

namespace Ag2.Manager.ADO.MSSql
{
    public class ManagerModuleADO : BaseADO, Ag2.Manager.DAL.IManagerModuleDAL
    {
        private static readonly ManagerModuleADO _instance = new ManagerModuleADO();
        private string _dataBaseName = "";
        private Helper.CurrentSessions cs = new Ag2.Manager.Helper.CurrentSessions();

        public string DataBaseName
        {
            get { return _dataBaseName; }
            set { _dataBaseName = value; }
        }

        private Database criarDataBase()
        {

            if (DataBaseName.Length > 0)
            {
                return DatabaseFactory.CreateDatabase(DataBaseName);
            }
            else
            {
                return DatabaseFactory.CreateDatabase();
            }

        }

        /// <summary>
        /// Contrutor da classe
        /// </summary>
        public ManagerModuleADO()
        {

        }

        public DataSet GetComboData(ManagerModuleFieldListBox managerField, ManagerModuleStructure _moduleStructure)
        {
            //monta SQL com consulta a tabela
            StringBuilder SQL = new StringBuilder();

            if (managerField.DataSource.Substring(0, 5).ToUpper().Equals("PROC:"))
            {
                string strDataSource = managerField.DataSource.ToUpper().Replace("PROC:", "");
                //filterByField
                //filterListBox

                if (strDataSource.Contains("@IDPAI"))
                {
                    if (!String.IsNullOrEmpty(HttpContext.Current.Request["id"]))
                    {
                        strDataSource = strDataSource.Replace("@IDPAI", HttpContext.Current.Request["id"]);
                    }
                    else
                    {
                        strDataSource = strDataSource.Replace("@IDPAI", "0");
                    }
                }
                SQL.AppendFormat("{0}", strDataSource);
            }
            else
            {

                SQL.AppendFormat("SELECT {0}, {1} ", managerField.DataValueField, managerField.DataTextField);
                SQL.AppendFormat(" FROM {0}{1} ", managerField.DataSource, _moduleStructure.Multilanguage.Equals(true) ? Ag2.Manager.Helper.ConfigurationManager.SufixoTabelaIdioma : string.Empty);

                if (managerField.TriggerField != null)
                    if (!managerField.TriggerField.Value.Equals(""))
                        SQL.AppendFormat(" WHERE {0}='{1}' ", managerField.TriggerField.FilterByField, managerField.TriggerField.Value);

                if (_moduleStructure.Multilanguage.Equals(true))
                {
                    SQL.Append(Helper.Util.AddWhereOrAnd(SQL.ToString()));
                    SQL.AppendFormat(" idiomaId = {0} ", cs.CurrentIdioma.IdiomaId.ToString());
                }

                SQL.Append(" ORDER BY ").Append(managerField.DataTextField);
            }


            return GetDataTable(SQL.ToString());
        }

        public DataSet GetComboData(ManagerModuleFieldListBox managerField, string filterField, string filterValue, ManagerModuleStructure _moduleStructure)
        {
            //monta SQL com consulta a tabela
            StringBuilder SQL = new StringBuilder();
            SQL.AppendFormat("SELECT {0}, {1} ", managerField.DataValueField, managerField.DataTextField);
            SQL.AppendFormat(" FROM {0}{1} ", managerField.DataSource, _moduleStructure.Multilanguage.Equals(true) ? "_idioma" : string.Empty);
            SQL.Append(" WHERE ").Append(filterField).Append("='").Append(filterValue).Append("'");
            if (_moduleStructure.Multilanguage.Equals(true))
            {
                SQL.AppendFormat(" AND idiomaId = {0} ", cs.CurrentIdioma.IdiomaId.ToString());
            }
            SQL.Append(" ORDER BY ").Append(managerField.DataTextField);

            return GetDataTable(SQL.ToString());
        }

        /// <summary>
        /// Executa command Procedure configurado no Xml do módulo
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public DataSet GetDataTableByStoredProcedureCommand(Ag2.Manager.Module.ManagerModuleStructure _moduleStructure, Query query)
        {
            DbCommand dbc = db.GetStoredProcCommand(query.Sql);
            Ag2.Manager.Helper.CurrentSessions cs = new Ag2.Manager.Helper.CurrentSessions();
            Ag2.Manager.Helper.Util util = new Ag2.Manager.Helper.Util();

            foreach (QueryParameter qp in query.QueryParameters)
            {
                DbType dbType = new DbType();
                object value = null;

                if (qp.Type.Equals("string", StringComparison.OrdinalIgnoreCase))
                {
                    dbType = DbType.String;
                    value = Convert.ToString(qp.Value);
                }
                else if (qp.Type.Equals("int32", StringComparison.OrdinalIgnoreCase))
                {
                    dbType = DbType.Int32;
                    value = Convert.ToInt32(qp.Value);
                }
                else if (qp.Type.Equals("int64", StringComparison.OrdinalIgnoreCase))
                {
                    dbType = DbType.Int64;
                    value = Convert.ToInt64(qp.Value);
                }
                else if (qp.Type.Equals("datetime", StringComparison.OrdinalIgnoreCase))
                {
                    dbType = DbType.DateTime;
                    value = Convert.ToDateTime(qp.Value);
                }
                else if (qp.Type.Equals("decimal", StringComparison.OrdinalIgnoreCase))
                {
                    dbType = DbType.Decimal;
                    value = Convert.ToDecimal(qp.Value);
                }
                else if (qp.Type.Equals("double", StringComparison.OrdinalIgnoreCase))
                {
                    dbType = DbType.Double;
                    value = Convert.ToDouble(qp.Value);
                }
                else if (qp.Type.Equals("bool", StringComparison.OrdinalIgnoreCase) || qp.Type.Equals("boolean", StringComparison.OrdinalIgnoreCase))
                {
                    dbType = DbType.Boolean;
                    if (qp.Value.Equals("1") || qp.Value.Equals("true", StringComparison.OrdinalIgnoreCase))
                        value = true;
                    else if (qp.Value.Equals("0") || qp.Value.Equals("false", StringComparison.OrdinalIgnoreCase))
                        value = false;
                    else
                        throw new System.Exception(string.Format("String ({0}) não pode ser convertido para o tipo Boolean", qp.Value));
                }
                else
                {
                    throw new System.Exception(string.Format("DbType não encontrado ({0})", qp.Type));
                }

                db.AddInParameter(dbc, qp.Name, dbType, value);
            }

            //CASO SEJA MULTI IDIOMA PASSA O PARAMETRO IDIOMAID
            if (_moduleStructure.Multilanguage)
            {
                db.AddInParameter(dbc, "@idiomaId", DbType.Int32, Convert.ToInt32(util.CurrentIdioma));
            }

            DataSet ds = null;

            ds = db.ExecuteDataSet(dbc);

            if (_moduleStructure.Multilanguage)
            {
                DataTable dt2 = new DataTable();
                dbc = db.GetSqlStringCommand(string.Format("SELECT {0}, idiomaId FROM {1}_Idioma", _moduleStructure.PrimaryKeyField, _moduleStructure.TableName));
                dt2 = db.ExecuteDataSet(dbc).Tables[0].Copy();
                dt2.TableName = "idioma";
                ds.Tables.Add(dt2);
            }

            return ds;
        }

        public DataTable FillForm(Ag2.Manager.Module.ManagerModuleStructure _moduleStructure, int registerID)
        {
            string moduleSQL;
            Database db = DatabaseFactory.CreateDatabase();

            if (_moduleStructure.QueryEdit.Equals(string.Empty))
            {
                moduleSQL = string.Format("SELECT * FROM {0}{1} WHERE {2} = {3}",
                    _moduleStructure.TableName, _moduleStructure.Multilanguage == true ? "_idioma" : string.Empty,
                    _moduleStructure.PrimaryKeyField,
                    registerID);
            }
            else
            {
                Query query = _moduleStructure.Queries.Find(delegate(Query q1)
                {
                    return q1.Section.Equals("edit", StringComparison.OrdinalIgnoreCase);
                });

                if (query.Type == Query.QueryType.Sql)
                    moduleSQL = string.Concat(_moduleStructure.QueryEdit, Helper.Util.AddWhereOrAnd(_moduleStructure.QueryEdit), _moduleStructure.TableName, ".", _moduleStructure.PrimaryKeyField, " = ", registerID);
                else
                {
                    QueryParameter qp = new QueryParameter();
                    qp.Name = string.Concat("@", _moduleStructure.PrimaryKeyField);
                    qp.Type = "Int32";
                    qp.Value = registerID.ToString();

                    query.QueryParameters.Add(qp);
                    return GetDataTableByStoredProcedureCommand(_moduleStructure, query).Tables[0];
                }

            }

            return GetDataTable(moduleSQL).Tables[0];
        }

        /// <summary>
        /// Obtem uma instancia da classe
        /// </summary>
        /// <returns>Retorna uma instancia da classe ManagerModuleDAO</returns>
        public static ManagerModuleADO GetManagerModuleDAO()
        {
            return _instance;
        }

        /// <summary>
        /// Consulta dados de uma tabela
        /// </summary>
        /// <param name="SQL">Query a ser executada</param>
        /// <returns>DataSet com dados consultados</returns>
        public DataSet GetDataTable(string SQL, Collection<ManagerModuleField> filters)
        {

            SQL = System.Web.HttpContext.Current.Server.HtmlDecode(SQL);

            // Cria o objeto de acesso a base de dados

            DbCommand dbc = db.GetSqlStringCommand(SQL);
            dbc.CommandType = CommandType.Text;

            //verifica se existem filtros setados
            foreach (ManagerModuleField filter in filters)
            {

                //Verifica se é tipo data e se é filtro por período
                //Se for ajusta a data para comparar hora também
                if (filter.DataType == DbType.Date)
                {
                    if (!filter.FilterValue.Equals(string.Empty))
                    {
                        if (filter.FilterExpression.IndexOf(">=") > -1)
                        {
                            filter.FilterValue = !filter.FilterValue.Contains(" 00:00:00") ? string.Concat(filter.FilterValue, " 00:00:00") : filter.FilterValue;
                        }
                        if (filter.FilterExpression.IndexOf("<=") > -1)
                        {
                            filter.FilterValue = !filter.FilterValue.Contains(" 23:59:59") ? string.Concat(filter.FilterValue, " 23:59:59") : filter.FilterValue;
                        }
                    }
                }

                if (!filter.FilterValue.Equals(""))
                    if (filter.Type == ManagerModuleFieldType.Text && (filter.DataType == DbType.String || filter.DataType == DbType.StringFixedLength))
                    {
                        db.AddInParameter(dbc, filter.Name.ToString(), filter.DataType, string.Concat("%", filter.FilterValue, "%"));
                    }
                    else
                    {
                        db.AddInParameter(dbc, filter.Name.ToString(), filter.DataType, filter.FilterValue);
                    }
            }

            //executa consulta            
            return db.ExecuteDataSet(dbc);
        }

        public DataSet GetDataTable(string SQL)
        {
            //executa consulta
            return db.ExecuteDataSet(CommandType.Text, SQL);
        }

        public DataSet GetDataTableUniqueValue(string SQL, List<Ag2.Manager.Module.Constraint> constraints)
        {
            DbCommand dbc = db.GetSqlStringCommand(SQL);
            dbc.CommandType = CommandType.Text;

            //verifica se existem filtros setados
            foreach (Ag2.Manager.Module.Constraint constraint in constraints)
            {
                foreach (ConstraintField field in constraint.Fields)
                {
                    if (String.IsNullOrEmpty(field.Field.Value))
                    {
                        db.AddInParameter(dbc, field.Field.Name, field.Field.DataType, DBNull.Value);
                    }
                    else
                    {
                        db.AddInParameter(dbc, field.Field.Name, field.Field.DataType, field.Field.Value);
                    }
                }
            }

            //executa consulta
            return db.ExecuteDataSet(dbc);
        }

        /// <summary>
        /// Adiciona a query a clausula WHERE
        /// </summary>
        /// <param name="SQL"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public string addWhere(string SQL, string where)
        {
            Regex regexp = new Regex(@"(WHERE|GROUP|ORDER)", (RegexOptions.Compiled | RegexOptions.IgnoreCase));

            MatchCollection matchSQL = regexp.Matches(SQL);
            StringBuilder newSQL = new StringBuilder();

            //verifica se encontrou alguma ocorrencia
            if (matchSQL.Count > 0)
            {
                if (!matchSQL[0].Value.Equals("WHERE", StringComparison.OrdinalIgnoreCase))
                {
                    newSQL.Append(SQL.Substring(0, matchSQL[0].Index));
                    newSQL.AppendFormat(" WHERE {0} ", where);
                    newSQL.Append(SQL.Substring(matchSQL[0].Index));
                }
                else
                {
                    newSQL.Append(SQL.Substring(0, matchSQL[0].Index + 6));
                    newSQL.AppendFormat(" {0} AND ", where);
                    newSQL.Append(SQL.Substring(matchSQL[0].Index + 6));
                }
            }
            else
            {
                newSQL.AppendFormat(" {0} WHERE {1} ", SQL, where);
            }

            //retorno da função
            return newSQL.ToString();
        }

        /// <summary>
        /// Salva o form na base de dados
        /// </summary>
        /// <param name="managerModule"></param>
        /// <param name="registerId"></param>
        /// <returns></returns>
        public bool SaveFormData(ManagerModuleStructure managerModule, int registerId)
        {
            //verifica se é inclusão ou atualização de um registro
            string command = (registerId > 0) ? "Update" : "Insert";
            bool _saveStatus = true;
            DbCommand dataProc = null;

            DbConnection conn = db.CreateConnection();
            conn.Open();
            DbTransaction transaction = conn.BeginTransaction();

            List<ManagerModuleField> fields = null;
            List<ManagerModuleField> fieldsMultiLanguage = null;

            try
            {
                //Seta procedure a ser usada para a oparação corrente
                dataProc = db.GetStoredProcCommand(string.Concat(managerModule.TableName, command));

                //se é atualização, passa chave primaria como parametro
                if (registerId > 0)
                {
                    db.AddInParameter(dataProc, managerModule.PrimaryKeyField, DbType.Int32, registerId);
                }
                else
                {
                    //cria parametro de retorno padrao
                    db.AddOutParameter(dataProc, managerModule.PrimaryKeyField, DbType.Int32, 4);
                }

                bool IsSingleValue = true;

                fields = new List<ManagerModuleField>();
                fieldsMultiLanguage = new List<ManagerModuleField>();

                foreach (ManagerModuleField moduleField in managerModule.Fields)
                {
                    if (!moduleField.Translation)
                    {
                        fields.Add(moduleField);
                    }
                    else
                    {
                        fieldsMultiLanguage.Add(moduleField);
                    }
                }

                #region "SALVA CAMPOS NORMAIS"

                //configura parametros da stored procedure
                foreach (ManagerModuleField moduleField in fields)
                {
                    //considera que sempre é uma linha para insert
                    IsSingleValue = true;

                    if (moduleField.Type == ManagerModuleFieldType.ListBox)
                    {
                        IsSingleValue = !(((ManagerModuleFieldListBox)moduleField).MultiSelectType == ManagerModuleMultiSelectType.Multiple);
                    }

                    if (!moduleField.DataFieldName.Equals(""))
                    {
                        if (moduleField.Type == ManagerModuleFieldType.CheckBox)
                        {
                            db.AddInParameter(dataProc, moduleField.DataFieldName, DbType.Boolean, moduleField.Value.Equals("S") ? 1 : 0);
                        }
                        else if (String.IsNullOrEmpty(moduleField.Value))
                        {
                            db.AddInParameter(dataProc, moduleField.DataFieldName, moduleField.DataType, DBNull.Value);
                        }
                        else
                        {
                            db.AddInParameter(dataProc, moduleField.DataFieldName, moduleField.DataType, moduleField.Value);
                        }
                    }
                }

                //executa procedure
                db.ExecuteNonQuery(dataProc, transaction);

                #endregion

                //pega ID do conteudo inserido
                if (registerId.Equals(0))
                {
                    registerId = Convert.ToInt32(db.GetParameterValue(dataProc, managerModule.PrimaryKeyField));
                }

                if (managerModule.Multilanguage)
                {
                    #region "SALVA CAMPOS MULTI IDIOMA"

                    //DETETA REGISTROS
                    dataProc = db.GetSqlStringCommand(string.Format(" DELETE FROM {0}_idioma WHERE idiomaId = @idiomaId AND {1} = @{1} ", managerModule.TableName, managerModule.PrimaryKeyField));
                    db.AddInParameter(dataProc, "@idiomaId", DbType.Int32, cs.CurrentIdioma.IdiomaId);
                    db.AddInParameter(dataProc, string.Concat("@", managerModule.PrimaryKeyField), DbType.Int32, registerId);
                    db.ExecuteNonQuery(dataProc, transaction);

                    //Insere registro multi linguagem
                    dataProc = db.GetStoredProcCommand(string.Concat(managerModule.TableName, "_idioma", "Insert"));
                    db.AddInParameter(dataProc, string.Concat("@", managerModule.PrimaryKeyField), DbType.Int32, registerId);
                    db.AddInParameter(dataProc, "@idiomaId", DbType.Int32, cs.CurrentIdioma.IdiomaId);

                    //configura parametros da stored procedure
                    foreach (ManagerModuleField moduleField in fieldsMultiLanguage)
                    {
                        //considera que sempre é uma linha para insert
                        IsSingleValue = true;

                        if (moduleField.Type == ManagerModuleFieldType.ListBox)
                        {
                            IsSingleValue = !(((ManagerModuleFieldListBox)moduleField).MultiSelectType == ManagerModuleMultiSelectType.Multiple);
                        }

                        if (!moduleField.DataFieldName.Equals(""))
                        {
                            if (moduleField.Type == ManagerModuleFieldType.CheckBox)
                            {
                                db.AddInParameter(dataProc, moduleField.DataFieldName, DbType.Boolean, moduleField.Value.Equals("S") ? 1 : 0);
                            }
                            else if (String.IsNullOrEmpty(moduleField.Value))
                            {
                                db.AddInParameter(dataProc, moduleField.DataFieldName, moduleField.DataType, DBNull.Value);
                            }
                            else
                            {
                                db.AddInParameter(dataProc, moduleField.DataFieldName, moduleField.DataType, moduleField.Value);
                            }
                        }
                    }

                    //executa procedure
                    db.ExecuteNonQuery(dataProc, transaction);

                    #endregion
                }

                //SerializaValoresForm(managerModule, registerId);

                //verifica se tem campos multiselect
                if (managerModule.HasMultiSelectFields)
                {
                    //faz insert dos campos multiselect
                    DbCommand multiProcInsert = db.GetStoredProcCommand("ag2mngListBoxInsert");
                    DbCommand multiProcClear = db.GetStoredProcCommand("ag2mngListBoxClear");

                    //varre todos os campos 
                    foreach (ManagerModuleField moduleField in managerModule.Fields)
                    {
                        if (moduleField.Type == ManagerModuleFieldType.ListBox)
                        {
                            if (((ManagerModuleFieldListBox)moduleField).MultiSelectType == ManagerModuleMultiSelectType.Multiple)
                            {
                                //apaga o conteudo da tabela de multiselect
                                multiProcClear.Parameters.Clear();
                                db.AddInParameter(multiProcClear, "table", DbType.String, ((ManagerModuleFieldListBox)moduleField).MultiSelectTable);
                                db.AddInParameter(multiProcClear, "primaryKeyField", DbType.String, managerModule.PrimaryKeyField);
                                db.AddInParameter(multiProcClear, "primaryKeyValue", DbType.Int32, registerId);
                                db.ExecuteNonQuery(multiProcClear);


                                //configura stored procedure para inclusão na tabela
                                multiProcInsert.Parameters.Clear();
                                db.AddInParameter(multiProcInsert, "table", DbType.String, ((ManagerModuleFieldListBox)moduleField).MultiSelectTable);
                                db.AddInParameter(multiProcInsert, "primaryKeyField", DbType.String, managerModule.PrimaryKeyField);
                                db.AddInParameter(multiProcInsert, "relationField", DbType.String, ((ManagerModuleFieldListBox)moduleField).DataValueField);
                                db.AddInParameter(multiProcInsert, "primaryKeyValue", DbType.Int32, registerId);
                                db.AddInParameter(multiProcInsert, "relationValue", DbType.Int32, 0);

                                //verifica se tem itens selecionados
                                if (((ManagerModuleFieldListBox)moduleField).SelectedItems.Count > 0)
                                {
                                    //loop para inserir todos os loops selecionados
                                    foreach (string value in ((ManagerModuleFieldListBox)moduleField).SelectedItems)
                                    {

                                        //configura valor do parametro
                                        multiProcInsert.Parameters["@relationValue"].Value = value;

                                        //executa stored procedure
                                        db.ExecuteNonQuery(multiProcInsert);
                                    }
                                }
                            }
                        }
                        else if (moduleField.Type == ManagerModuleFieldType.SubForm)
                        {
                            if (((ManagerModuleFieldSubForm)moduleField).MultiSelectType == ManagerModuleMultiSelectType.Multiple)
                            {
                                //apaga o conteudo da tabela de multiselect
                                multiProcClear.Parameters.Clear();
                                db.AddInParameter(multiProcClear, "table", DbType.String, ((ManagerModuleFieldSubForm)moduleField).MultiSelectTable);
                                db.AddInParameter(multiProcClear, "primaryKeyField", DbType.String, managerModule.PrimaryKeyField);
                                db.AddInParameter(multiProcClear, "primaryKeyValue", DbType.Int32, registerId);
                                db.ExecuteNonQuery(multiProcClear, transaction);

                                //configura stored procedure para inclusão na tabela
                                multiProcInsert.Parameters.Clear();
                                db.AddInParameter(multiProcInsert, "table", DbType.String, ((ManagerModuleFieldSubForm)moduleField).MultiSelectTable);
                                db.AddInParameter(multiProcInsert, "primaryKeyField", DbType.String, managerModule.PrimaryKeyField);
                                db.AddInParameter(multiProcInsert, "relationField", DbType.String, ((ManagerModuleFieldSubForm)moduleField).DataValueField);
                                db.AddInParameter(multiProcInsert, "primaryKeyValue", DbType.Int32, registerId);
                                db.AddInParameter(multiProcInsert, "relationValue", DbType.Int32, 0);

                                //verifica se tem itens selecionados
                                if (((ManagerModuleFieldSubForm)moduleField).SelectedItems.Count > 0)
                                {
                                    //loop para inserir todos os loops selecionados
                                    foreach (string value in ((ManagerModuleFieldSubForm)moduleField).SelectedItems)
                                    {

                                        //configura valor do parametro
                                        multiProcInsert.Parameters["@relationValue"].Value = value;

                                        //executa stored procedure
                                        db.ExecuteNonQuery(multiProcInsert, transaction);
                                    }
                                }
                            }
                        }
                    }
                }

                //FAZ O COMMIT DOS DADOS
                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return _saveStatus;
        }

        private void SerializaValoresForm(ManagerModuleStructure managerModule, int registerId)
        {
            List<Ag2.Manager.BusinessObject.Publicacao> formValues = new List<Ag2.Manager.BusinessObject.Publicacao>();
            Ag2.Manager.BusinessObject.Publicacao pub = null;
            DbCommand dataProc = null;

            foreach (ManagerModuleField moduleField in managerModule.Fields)
            {
                pub = new Ag2.Manager.BusinessObject.Publicacao();
                pub.Campo = moduleField.Name;
                pub.Valor = moduleField.Value;
                pub.Type = moduleField.Type;
                pub.DataValueField = moduleField.DataFieldName;
                pub.DataFieldName = moduleField.DataFieldName;

                formValues.Add(pub);
            }

            MemoryStream ms = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, formValues);

            StringWriter OutputXML = new StringWriter(new StringBuilder());
            System.Xml.Serialization.XmlSerializer serializXml = new System.Xml.Serialization.XmlSerializer(formValues.GetType());
            serializXml.Serialize(OutputXML, formValues);

            dataProc = db.GetSqlStringCommand(" SELECT registroId FROM TB_PUBLICACAO WHERE registroId = @registroId ");
            db.AddInParameter(dataProc, "@registroId", DbType.Int32, registerId);
            int _count = Convert.ToInt32(db.ExecuteScalar(dataProc));

            if (_count.Equals(0))
                dataProc = db.GetSqlStringCommand(" INSERT INTO TB_PUBLICACAO (entidade, entidadeXML, modulo, registroId) VALUES (@entidade, @entidadeXML, @modulo, @registroId) ");
            else
            {
                dataProc = db.GetSqlStringCommand(" UPDATE TB_PUBLICACAO SET entidade = @entidade, entidadeXML = @entidadeXML, modulo = @modulo  WHERE registroId = @registroId ");
            }

            db.AddInParameter(dataProc, "@registroId", DbType.Int32, registerId);
            db.AddInParameter(dataProc, "@modulo", DbType.String, managerModule.Name);
            db.AddInParameter(dataProc, "@entidade", DbType.Binary, ms.ToArray());
            db.AddInParameter(dataProc, "@entidadeXML", DbType.String, OutputXML.ToString());
            db.ExecuteNonQuery(dataProc);

            //dataProc = db.GetSqlStringCommand("SELECT TOP 1 * FROM TB_PUBLICACAO");
            //IDataReader dr = db.ExecuteReader(dataProc);

            //while (dr.Read())
            //{
            //    //BINARY
            //    ms = new MemoryStream((byte[])dr["entidade"]);
            //    formValues = (List<Ag2.Manager.BusinessObject.Publicacao>)formatter.Deserialize(ms);

            //    //XML
            //    StringReader sr = new StringReader(dr["entidadeXML"].ToString());
            //    formValues = (List<Ag2.Manager.BusinessObject.Publicacao>)serializXml.Deserialize(sr);
            //}
        }

        public void DeleteFormData(ManagerModuleStructure managerModule, string registerId)
        {
            Ag2.Manager.Helper.CurrentSessions cs = new Ag2.Manager.Helper.CurrentSessions();
            
            string dbType = db.ToString();
            StringBuilder sbSQL = new StringBuilder();

            DbCommand dataProc = null;
                        
            sbSQL.Length = 0;
            sbSQL.Append(" DELETE ");
            sbSQL.AppendFormat(" FROM {0}{1} ", managerModule.TableName, managerModule.Multilanguage == true ? "_idioma" : string.Empty);
            sbSQL.AppendFormat(" WHERE {0} = @{0} ", managerModule.PrimaryKeyField);

            if (managerModule.Multilanguage)
                sbSQL.AppendFormat(" AND idiomaId = @idiomaId ", cs.CurrentIdioma.IdiomaId.ToString());

            dataProc = db.GetSqlStringCommand(sbSQL.ToString());

            db.AddInParameter(dataProc, managerModule.PrimaryKeyField, DbType.Int32, registerId);

            if (managerModule.Multilanguage)
                db.AddInParameter(dataProc, "@idiomaId", DbType.Int32, cs.CurrentIdioma.IdiomaId);

            //DELETA REGISTRO
            db.ExecuteNonQuery(dataProc);

            //SE FOR MULTI IDIOMA
            if (managerModule.Multilanguage)
            {
                sbSQL.AppendFormat(" SELECT {0} ", managerModule.PrimaryKeyField);
                sbSQL.AppendFormat(" FROM {0}_Idioma ", managerModule.TableName);
                sbSQL.AppendFormat(" WHERE {0} = @{0} ", managerModule.PrimaryKeyField);

                dataProc = db.GetSqlStringCommand(sbSQL.ToString());
                db.AddInParameter(dataProc, managerModule.PrimaryKeyField, DbType.Int32, registerId);

                if (managerModule.Multilanguage)
                    db.AddInParameter(dataProc, "@idiomaId", DbType.Int32, cs.CurrentIdioma.IdiomaId);

                //DELETA REGISTRO PAI CASO NAO EXISTA NENHUMA TRADUÇAO
                if (db.ExecuteDataSet(dataProc).Tables[0].Rows.Count == 0)
                {
                    sbSQL.Length = 0;
                    sbSQL.Append(" DELETE ");
                    sbSQL.AppendFormat(" FROM {0} ", managerModule.TableName);
                    sbSQL.AppendFormat(" WHERE {0} = @{0} ", managerModule.PrimaryKeyField);

                    dataProc = db.GetSqlStringCommand(sbSQL.ToString());
                    db.AddInParameter(dataProc, managerModule.PrimaryKeyField, DbType.Int32, registerId);

                    db.ExecuteNonQuery(dataProc);
                }
            }
        }
    }
}
