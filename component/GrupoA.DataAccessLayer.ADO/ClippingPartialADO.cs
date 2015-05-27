
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
	public partial class ClippingADO : ADOSuper, IClippingDAL 
    {				
		/// <summary>
        /// Método que retorna uma coleção de Clipping com conteudo Imprensa.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos Clipping.</returns>
        public IEnumerable<Clipping> CarregarTodosValidosComDependencias(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {
		
			List<Clipping> entidadesRetorno = new List<Clipping>();
			
			StringBuilder sbSql = new StringBuilder();
			StringBuilder sbOrder = new StringBuilder();

		    // Monta o "OrderBy"
			if (ordemColunas!=null) {
				for(int i=0; i<ordemColunas.Length; i++) {
					if (sbOrder.Length>0) { sbOrder.Append( ", " ); }
					sbOrder.Append(ordemColunas[i] + " " + ordemSentidos[i]);
				} 
				if (sbOrder.Length > 0) { sbOrder.Insert(0, " ORDER BY "); }				
			} else {
				sbOrder.Append( " ORDER BY clippingId" );
			}
				
			
			if (registrosPagina>0)
            {
				
				sbSql.Append("SELECT * FROM ( ");	
			
                sbSql.Append("SELECT Clipping.clippingId, Clipping.autor, Clipping.dataPublicacao");
                sbSql.Append(", conteudoImprensaId, fonte, fonteUrl, ativo, dataExibicaoInicio, dataExibicaoFim, resumo, texto, destaque, titulo");
                sbSql.Append(", conteudoId, conteudoTipoId, dataHoraCadastro");
                sbSql.Append(", ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R ");
                sbSql.Append(" FROM Clipping");
                sbSql.Append(" INNER JOIN ConteudoImprensa ON Clipping.clippingId=ConteudoImprensa.conteudoImprensaId");
                sbSql.Append(" INNER JOIN Conteudo ON ConteudoImprensa.conteudoImprensaId=Conteudo.conteudoId");

                sbSql.Append(" WHERE (GETDATE() >= ISNULL(dbo.ConteudoImprensa.dataExibicaoInicio, GETDATE()) ");
                sbSql.Append("AND GETDATE() <= ISNULL(dbo.ConteudoImprensa.dataExibicaoFim, GETDATE())) ");
                sbSql.Append("AND ConteudoImprensa.ativo = 1 ");

                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                {
                    sbSql.Append(string.Concat(" AND ", filtro.GetWhereString()));
                }
				sbSql.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} 
            else 
            {
                sbSql.Append("SELECT Clipping.* ");
                sbSql.Append(", ConteudoImprensa.* ");
                sbSql.Append(", Conteudo.* ");
                sbSql.Append(" FROM Clipping");
                sbSql.Append(" INNER JOIN ConteudoImprensa ON Clipping.clippingId=ConteudoImprensa.conteudoImprensaId");
                sbSql.Append(" INNER JOIN Conteudo ON ConteudoImprensa.conteudoImprensaId=Conteudo.conteudoId");

                sbSql.Append(" WHERE (GETDATE() >= ISNULL(dbo.ConteudoImprensa.dataExibicaoInicio, GETDATE()) ");
                sbSql.Append("AND GETDATE() <= ISNULL(dbo.ConteudoImprensa.dataExibicaoFim, GETDATE())) ");
                sbSql.Append("AND ConteudoImprensa.ativo = 1 ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                {
                    sbSql.Append(string.Concat(" AND ",filtro.GetWhereString()));
                }
                
				if ( sbOrder.Length > 0 ) { sbSql.Append(sbOrder.ToString()); }
			}
			
			DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Clipping entidadeRetorno = new Clipping();
                PopulaClipping(reader, entidadeRetorno);
                entidadeRetorno.ConteudoImprensa = new ConteudoImprensa();
                ConteudoImprensaADO.PopulaConteudoImprensa(reader, entidadeRetorno.ConteudoImprensa);
                entidadeRetorno.ConteudoImprensa.Conteudo = new Conteudo();
                ConteudoADO.PopulaConteudo(reader, entidadeRetorno.ConteudoImprensa.Conteudo);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}

        public int ContarTodosValidosComDependencias()
        {

            int total = 0;

            StringBuilder sbSql = new StringBuilder();


            sbSql.Append("SELECT count(ClippingId) total ");
            sbSql.Append(" FROM Clipping");
            sbSql.Append(" INNER JOIN ConteudoImprensa ON Clipping.clippingId=ConteudoImprensa.conteudoImprensaId");
            sbSql.Append(" INNER JOIN Conteudo ON ConteudoImprensa.conteudoImprensaId=Conteudo.conteudoId");

            sbSql.Append(" WHERE (GETDATE() >= ISNULL(dbo.ConteudoImprensa.dataExibicaoInicio, GETDATE()) ");
            sbSql.Append("AND GETDATE() <= ISNULL(dbo.ConteudoImprensa.dataExibicaoFim, GETDATE())) ");
            sbSql.Append("AND ConteudoImprensa.ativo = 1 ");

            
            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());
            IDataReader reader = _db.ExecuteReader(command);

            if((reader.Read()) && (reader["total"] != null))
            {
                total = Int32.Parse(reader["total"].ToString());
            }
            reader.Close();

            return total;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public List<Clipping> CarregarClippingsPorCategoria(Categoria categoria)
        {
            List<Clipping> entidadesRetorno = new List<Clipping>();

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            sbSQL.AppendFormat("SELECT TOP 3  ");
	        sbSQL.AppendFormat("    Clipping.clippingId, ");
	        sbSQL.AppendFormat("    ConteudoImprensa.titulo, ");
	        sbSQL.AppendFormat("    ConteudoImprensa.fonte, ");
	        sbSQL.AppendFormat("    Convert(varchar(20), Conteudo.dataHoraCadastro, 103) + ' ' + Convert(varchar(20), Conteudo.dataHoraCadastro, 108) as dataHoraCadastro ");
            sbSQL.AppendFormat("FROM Clipping ");
            sbSQL.AppendFormat("INNER JOIN ConteudoImprensa ");
	        sbSQL.AppendFormat("    ON Clipping.clippingId = ConteudoImprensa.conteudoImprensaId ");
            sbSQL.AppendFormat("INNER JOIN Conteudo ");
            sbSQL.AppendFormat("    ON Clipping.clippingId = Conteudo.conteudoId ");
            if (categoria != null)
            {
                sbSQL.AppendFormat("INNER JOIN ConteudoAreaConhecimento ");
                sbSQL.AppendFormat("    ON Conteudo.conteudoId = ConteudoAreaConhecimento.conteudoId ");
                sbSQL.AppendFormat("WHERE ConteudoAreaConhecimento.categoriaId = @categoriaId ");
            }
            sbSQL.AppendFormat("ORDER BY Conteudo.dataHoraCadastro DESC ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            if (categoria != null)
            {
                _db.AddInParameter(command, "@categoriaId", DbType.String, categoria.CategoriaId);
            }

            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Clipping entidadeRetorno = new Clipping();
                PopulaUltimosClippings(reader, entidadeRetorno);

                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        public static void PopulaUltimosClippings(IDataReader reader, Clipping entidade)
        {
            if (reader["clippingId"] != DBNull.Value)
            {
                entidade.ClippingId = Convert.ToInt32(reader["clippingId"].ToString());
            }

            if (reader["titulo"] != DBNull.Value)
            {
                if (entidade.ConteudoImprensa == null) entidade.ConteudoImprensa = new ConteudoImprensa();
                entidade.ConteudoImprensa.Titulo = reader["titulo"].ToString();
            }

            if (reader["fonte"] != DBNull.Value)
            {
                if (entidade.ConteudoImprensa == null) entidade.ConteudoImprensa = new ConteudoImprensa();
                entidade.ConteudoImprensa.Fonte = reader["fonte"].ToString();
            }

            if (reader["dataHoraCadastro"] != DBNull.Value)
            {
                if (entidade.ConteudoImprensa == null) entidade.ConteudoImprensa = new ConteudoImprensa();
                if (entidade.ConteudoImprensa.Conteudo == null) entidade.ConteudoImprensa.Conteudo = new Conteudo();
                entidade.ConteudoImprensa.Conteudo.DataHoraCadastro = Convert.ToDateTime(reader["dataHoraCadastro"].ToString());
            }

        }
	}
}
		