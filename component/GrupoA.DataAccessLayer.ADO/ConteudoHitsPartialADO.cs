
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
using System;
using System.Data;
using System.Data.Common;

using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using GrupoA.BusinessObject;
using GrupoA.FilterHelper;

namespace GrupoA.DataAccess.ADO
{
	public partial class ConteudoHitsADO : ADOSuper, IConteudoHitsDAL {
	
        /// <summary>
        /// Método que remove um ConteudoHits da base de dados.
        /// </summary>
        /// <param name="entidade">ConteudoHits a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(Conteudo entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM ConteudoHits ");
			sbSQL.Append("WHERE conteudoId=@conteudoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@conteudoId", DbType.Int32, entidade.ConteudoId);

								
			_db.ExecuteNonQuery(command);
		}
		
	}
}
		