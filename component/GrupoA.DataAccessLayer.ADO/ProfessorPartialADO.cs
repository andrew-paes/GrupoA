
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
    public partial class ProfessorADO : ADOSuper, IProfessorDAL
    {
    
        #region [ Metodos ]

        public Professor CarregarAvaliacaoComDependencias(Professor entidade)
        {
            Professor entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM Professor WHERE professorId=@professorId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@professorId", DbType.Int32, entidade.ProfessorId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Professor();
                PopulaProfessorAvaliacoes(reader, entidadeRetorno);

                //ProfessorADO.PopulaProfessor(reader, entidadeRetorno.Usuario);
            }
            reader.Close();

            return entidadeRetorno;
        }

        public static void PopulaProfessorAvaliacoes(IDataReader reader, Professor entidade)
        {
            if (reader["professorId"] != DBNull.Value)
            {
                entidade.ProfessorId = Convert.ToInt32(reader["professorId"].ToString());

                //Carrega TituloSolicitacoes e TituloAvaliacoes
                entidade.TituloSolicitacoes = (List<TituloSolicitacao>)new TituloSolicitacaoADO().CarregarTodos(0, 0, null, null, new TituloSolicitacaoFH() { ProfessorId = entidade.ProfessorId.ToString() });

                if (entidade.TituloSolicitacoes != null)
                {
                    for (int i = 0; i < entidade.TituloSolicitacoes.Count; i++)
                    {
                        entidade.TituloSolicitacoes[i].TituloAvaliacoes = (List<TituloAvaliacao>)new TituloAvaliacaoADO().CarregarTodos(0, 0, null, null, new TituloAvaliacaoFH() { TituloSolicitacaoId = entidade.TituloSolicitacoes[i].TituloSolicitacaoId.ToString() });
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <param name="docencia"></param>
        /// <returns></returns>
		public Professor CarregarInstituicoesProfessor(Professor entidade)
		{
			//Professor entidadeRetorno = null;
			List<ProfessorInstituicao> professorInstituicoes = new List<ProfessorInstituicao>();

			entidade = this.CarregarComDependencias(entidade);

				StringBuilder sbSQL = new StringBuilder();
				sbSQL.Append(" SELECT * FROM ProfessorInstituicao profInst ");
				sbSQL.Append(" INNER JOIN Telefone t ON t.telefoneId = profInst.telefoneId ");
				sbSQL.Append(" INNER JOIN Instituicao i on i.instituicaoId = profInst.instituicaoId ");

				sbSQL.Append(" WHERE profInst.professorid = @professorId ");

				DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

				_db.AddInParameter(command, "@professorId", DbType.Int32, entidade.ProfessorId);

				IDataReader reader = _db.ExecuteReader(command);
				
				while (reader.Read())
				{
					ProfessorInstituicao professorInstituicao = new ProfessorInstituicao();
					ProfessorInstituicaoADO.PopulaProfessorInstituicao(reader, professorInstituicao);
					InstituicaoADO.PopulaInstituicao( reader, professorInstituicao.Instituicao );
					TelefoneADO.PopulaTelefone( reader, professorInstituicao.Telefone );
					professorInstituicoes.Add(professorInstituicao);
				}
				reader.Close();
				
			entidade.ProfessorInstituicoes = professorInstituicoes;

			return entidade;
		}		
		
        public Professor CarregarComDependencias(Professor entidade, bool docencia)
        {
            //Professor entidadeRetorno = null;

           entidade = this.CarregarComDependencias(entidade);

           return entidade;
        }

        #endregion
    }
}
