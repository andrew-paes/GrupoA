
/*
'===============================================================================
'
'  Template: Gerador Código C#.csgen
'  Script versão: 0.96
'  Script criado por: Leonardo Alves Lindermann (lindermannla@ag2.com.br)
'  Gerado pelo MyGeneration versão # (???)
'
'===============================================================================
*/
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace GrupoA.DataAccess
{
    public abstract class ADOSuper
    {
        protected Database _db;

        public ADOSuper()
        {
            _db = DatabaseFactory.CreateDatabase();
		}
    }
	
    public struct OrderBy
    {
        public static string Ascendente = @"ASC";
        public static string Descendente = @"DESC";
    }	
}
