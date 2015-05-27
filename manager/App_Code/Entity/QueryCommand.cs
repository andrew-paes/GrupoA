using System;
using System.Collections.Generic;
using System.Web;


namespace Ag2.Manager.Entity
{
    /// <summary>
    /// Summary description for QueryCommand
    /// </summary>
    public class QueryCommand
    {
        private string _sql = string.Empty;
        private string _primaryKey = string.Empty;
        private int _idiomaId = 0;
        private List<int> _whereIds = null;
        private List<FieldSubform> _fields = null;

        public QueryCommand()
        {
            _whereIds = new List<int>();
            _fields = new List<FieldSubform>();
        }

        /// <summary>
        /// Informa a String SQL para execução do comando
        /// </summary>
        public string Sql
        {
            get { return this._sql; }
            set { this._sql = value; }
        }

        /// <summary>
        /// Informe o nome da primary key na base de dados
        /// </summary>
        public string PrimaryKey
        {
            get { return this._primaryKey; }
            set { this._primaryKey = value; }
        }

        /// <summary>
        /// Caso o campo tenha tradução informe o idioma id (Opcional)
        /// </summary>
        public int IdiomaId
        {
            get { return this._idiomaId; }
            set { this._idiomaId = value; }
        }

        public List<int> WhereIds
        {
            get { return this._whereIds; }
            set { this._whereIds = value; }
        }

        public List<FieldSubform> Fields
        {
            get { return this._fields; }
            set { this._fields = value; }
        }

    }
}
