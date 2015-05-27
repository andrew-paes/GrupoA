using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Ag2.Manager.BusinessObject
{
    public class Query
    {
        private string _section = string.Empty;
        private string _sql = string.Empty;
        private string _moduleName = string.Empty;
        private QueryType _queryType;
        private List<QueryParameter> _queryParameters = new List<QueryParameter>();

        public enum QueryType
        {
            StoredProcedure,
            Sql
        }

        public Query(string moduleName)
        {
            _moduleName = moduleName;
        }

        public QueryType Type
        {
            get { return _queryType; }
            set { _queryType = value; }
        }

        public List<QueryParameter> QueryParameters
        {
            get { return _queryParameters; }
            set { _queryParameters = value; }
        }

        public string Section
        {
            get { return _section; }
            set { _section = value; }
        }

        public string Sql
        {
            get {

                //Procura por expressão especial na Query SQL
                Ag2.Manager.Dicionary.SqlClause sqlclause = new Ag2.Manager.Dicionary.SqlClause();

                Regex regexp1 = new Regex("{(.*?)}", (RegexOptions.Compiled | RegexOptions.IgnoreCase));
                MatchCollection matchSQL = regexp1.Matches(_sql);

                if (matchSQL.Count > 0)
                {
                    foreach (System.Text.RegularExpressions.Group item in matchSQL)
                    {
                        if (!sqlclause.Clauses.ContainsKey(item.ToString().ToUpper()))
                        {
                            throw new Ag2.Manager.Exception.ParameterNotFoundException(item.ToString(), _moduleName);
                        }
                    }

                    foreach (KeyValuePair<string, string> item in sqlclause.Clauses)
                    {
                        Regex regexp2 = new Regex(item.Key, (RegexOptions.Compiled | RegexOptions.IgnoreCase));

                        _sql = regexp2.Replace(_sql, item.Value);
                    }
                }

                return _sql.Trim();
            }
            set { _sql = value; }
        }

        
    }
}
