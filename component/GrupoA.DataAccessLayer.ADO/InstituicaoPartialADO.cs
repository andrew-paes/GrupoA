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
    public partial class InstituicaoADO : ADOSuper, IInstituicaoDAL
    {
        /// <summary>
        /// Método que retorna uma coleção de Instituicao.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos Instituicao.</returns>
        public IEnumerable<Instituicao> CarregarPorProfessor(Professor professor)
        {
            List<Instituicao> entidadesRetorno = new List<Instituicao>();

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            sbSQL.Append(" SELECT Instituicao.* FROM ProfessorInstituicao ");
            sbSQL.Append(" JOIN Instituicao ON Instituicao.InstituicaoId = ProfessorInstituicao.InstituicaoId");
            sbSQL.Append(" WHERE ProfessorInstituicao.ProfessorId = @professorId");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@professorId", DbType.Int32, professor.ProfessorId);

            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Instituicao entidadeRetorno = new Instituicao();
                PopulaInstituicao(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }

        public IEnumerable<Instituicao> CarregarExcetoCadastradosPorProfessor(Professor professor)
        {
            List<Instituicao> entidadesRetorno = new List<Instituicao>();

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            sbSQL.Append("SELECT * FROM Instituicao ");
            sbSQL.Append(" WHERE InstituicaoId NOT IN (");
            sbSQL.Append(" SELECT ProfessorInstituicao.InstituicaoId FROM ProfessorInstituicao ");
            sbSQL.Append(" WHERE ProfessorInstituicao.ProfessorId = @professorId)");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@professorId", DbType.Int32, professor.ProfessorId);

            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Instituicao entidadeRetorno = new Instituicao();
                PopulaInstituicao(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        public Instituicao Carregar(String nomeInstituicao)
        {
            Instituicao entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM Instituicao WHERE nomeInstituicao=@nomeInstituicao");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@nomeInstituicao", DbType.String, nomeInstituicao);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Instituicao();
                PopulaInstituicao(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }
    }
}