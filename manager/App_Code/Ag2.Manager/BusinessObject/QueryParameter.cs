using System;
using System.Collections.Generic;
using System.Web;
using Ag2.Manager.Dicionary;

namespace Ag2.Manager.BusinessObject
{
    /// <summary>
    /// Summary description for QueryParameter
    /// </summary>
    public class QueryParameter
    {
        private string _name = string.Empty;
        private string _type = string.Empty;
        private string _value = string.Empty;

        public QueryParameter()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public string Value
        {
            get
            {
                SqlClause sc = new SqlClause();
                Dictionary<string, string> d = sc.Clauses;
                if (d.ContainsKey(_value.ToString()))
                {
                    _value = d[_value.ToString()].ToString();
                }

                return _value;
            }
            set
            {
                _value = value;
            }
        }
    }
}
