using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess.ADO
{
    public partial class RevistaPaginaADO : ADOSuper, IRevistaPaginaDAL
    {
        /// <summary>
        /// Método que retorna uma coleção de RevistaPagina.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos RevistaPagina.</returns>
        public List<RevistaPagina> CarregarRevistaPaginasMenu(Revista revista)
        {
            List<RevistaPagina> entidadesRetorno = new List<RevistaPagina>();

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            sbSQL.Append(@"SELECT * 
                           FROM RevistaPagina
                           WHERE revistaId = @revistaId AND ativo = 1 AND exibirMenu = 1
                           ORDER BY ordem ");
            
            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaId", DbType.Int32, revista.RevistaId);

            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                RevistaPagina entidadeRetorno = new RevistaPagina();
                PopulaRevistaPagina(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
        }

        public RevistaPagina CarregarRevistaPaginaPorNome(RevistaPagina revistaPagina)
        {
            RevistaPagina entidadeRetorno = new RevistaPagina();

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            sbSQL.Append(@"SELECT * 
                           FROM RevistaPagina
                           WHERE nomePagina = @nomePagina
                                AND revistaId = @revistaId
                           ORDER BY ordem ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@nomePagina", DbType.String, revistaPagina.NomePagina);
            _db.AddInParameter(command, "@revistaId", DbType.Int32, revistaPagina.Revista.RevistaId);

            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                PopulaRevistaPagina(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }
    }
}
