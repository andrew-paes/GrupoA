
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
	public partial class ConteudoADO : ADOSuper, IConteudoDAL {

        /// <summary>
        /// Método que remove um Conteudo da base de dados.
        /// </summary>
        /// <param name="entidade">Conteudo a ser excluído (somente o identificador é necessário).</param>		
        public void ExcluirConteudoRelacionado(Conteudo entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM ConteudoRelacionado ");
            sbSQL.Append("WHERE conteudoIdPai=@conteudoId ");
            sbSQL.Append(" OR conteudoIdRelacionado=@conteudoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@conteudoId", DbType.Int32, entidade.ConteudoId);


            _db.ExecuteNonQuery(command);
        }
			

        #region Conteúdo x Categoria (Área Conhecimento)

        #region public void InserirRelacionamentoAreaConhecimento(Conteudo conteudo, Categoria categoria)
        /// <summary>
        /// Insere um relacionamento entre um conteúdo e uma área de conhecimento.
        /// </summary>
        /// <param name="conteudo">Conteúdo a ser relacionado.</param>
        /// <param name="categoria">Categoria a ser relacionada.</param>
        public void InserirRelacionamentoAreaConhecimento(Conteudo conteudo, Categoria categoria)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO ConteudoAreaConhecimento ");
            sbSQL.Append(" (conteudoId, categoriaId) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@conteudoId, @categoriaId) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@conteudoId", DbType.Int32, conteudo.ConteudoId);

            _db.AddInParameter(command, "@categoriaId", DbType.Int32, categoria.CategoriaId);

            // Executa a query.
            _db.ExecuteNonQuery(command);

            //entidade.ConteudoId = Convert.ToInt32(_db.GetParameterValue(command, "@conteudoId"));
        }
        #endregion

        #region public void ExcluirRelacionamentoAreaConhecimento(Conteudo conteudo, Categoria categoria)
        /// <summary>
        /// Exclui um relacionamento entre Conteúdo e Categoria
        /// </summary>
        /// <param name="conteudo">Conteúdo a ser relacionado.</param>
        /// <param name="categoria">Categoria a ser relacionada.</param>
        public void ExcluirRelacionamentoAreaConhecimento(Conteudo conteudo, Categoria categoria)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM ConteudoAreaConhecimento ");
            sbSQL.Append("WHERE conteudoId=@conteudoId ");
            sbSQL.Append("  AND categoriaId=@categoriaId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@conteudoId", DbType.Int32, conteudo.ConteudoId);

            _db.AddInParameter(command, "@categoriaId", DbType.Int32, categoria.CategoriaId);

            _db.ExecuteNonQuery(command);

        }
        #endregion

        #region public void ExcluirTodasAreasConhecimento(Conteudo conteudo)
        /// <summary>
        /// Exclui todas as Categorias relacionadas a um Conteúdo.
        /// </summary>
        /// <param name="conteudo">Conteúdo a ser limpo.</param>
        public void ExcluirTodasAreasConhecimento(Conteudo conteudo)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM ConteudoAreaConhecimento ");
            sbSQL.Append("WHERE conteudoId=@conteudoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@conteudoId", DbType.Int32, conteudo.ConteudoId);

            _db.ExecuteNonQuery(command);
        }
        #endregion

        #region public IEnumerable<Categoria> CarregarTodasAreasConhecimentoCategoria(Conteudo conteudo)
        /// <summary>
        /// Carregar as Áreas de Conhecimento de uma Categoria.
        /// </summary>
        /// <param name="conteudo">Conteúdo a serem buscadas as Áreas de Conhecimento.</param>
        /// <returns></returns>
        public IEnumerable<Categoria> CarregarTodasAreasConhecimentoCategoria(Conteudo conteudo)
        {
            List<Categoria> entidadesRetorno = new List<Categoria>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM ConteudoAreaConhecimento WHERE conteudoId=@conteudoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@conteudoId", DbType.Int32, conteudo.ConteudoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Categoria entidadeRetorno = new Categoria();
                if (reader["categoriaId"] != DBNull.Value)
                    entidadeRetorno.CategoriaId = Convert.ToInt32(reader["categoriaId"].ToString());

                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
        }
        #endregion 

        #endregion
    }
}
		