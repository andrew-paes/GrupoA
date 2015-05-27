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
using GrupoA.BusinessObject.Enumerator;

namespace GrupoA.DataAccess.ADO
{
    public partial class AvisoDisponibilidadeADO : ADOSuper, IAvisoDisponibilidadeDAL
    {
        /// <summary>
        /// Método que persiste os relacionamentos entre AvisoDisponibilidade e Autor.
        /// </summary>
        /// <param name="avisoDisponibilidade">AvisoDisponibilidade com seu identificador configurado.</param>	
        /// <param name="autores">Coleção de Autor com seus identificadores configurados.</param>	
        public void AtualizarSolicitacaoExpirada(DateTime dataLimiteSolicitacao)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append(" UPDATE AvisoDisponibilidade SET ");
            sbSQL.Append(" dataNotificacao = @dataNotificacao ");
            sbSQL.Append(" , avisoDisponibilidadeStatusId = @avisoDisponibilidadeStatusId ");
            sbSQL.Append(" WHERE ");
            sbSQL.Append(" dataSolicitacao <  @dataSolicitacao");
            sbSQL.Append(" AND dataNotificacao IS NULL ");
            sbSQL.Append(" AND avisoDisponibilidadeStatusId = @statusId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@dataSolicitacao", DbType.DateTime, dataLimiteSolicitacao);
            _db.AddInParameter(command, "@dataNotificacao", DbType.DateTime, DateTime.Now);
            _db.AddInParameter(command, "@avisoDisponibilidadeStatusId", DbType.Int32, StatusDeAvisoDisponibilidade.Cancelado.GetHashCode());
            _db.AddInParameter(command, "@statusId", DbType.Int32, StatusDeAvisoDisponibilidade.Aguardando.GetHashCode());

            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Carrega todos as solicitações de avisos pendentes, que estejam em status "Agurdando" 
        /// e que tenham produto disponível
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AvisoDisponibilidade> CarregarAvisoDisponibilidadePendente()
        {
            List<AvisoDisponibilidade> entidadesRetorno = new List<AvisoDisponibilidade>();

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append(@"WITH Avisos (avisoDisponibilidadeId, email, dataSolicitacao, produtoId, tipo, categoriaId)
                            AS
                            (
	                            SELECT AvisoDisponibilidade.avisoDisponibilidadeId,
		                            AvisoDisponibilidade.email,
                                    AvisoDisponibilidade.dataSolicitacao,
		                            Produto.produtoId,
		                            ProdutoTipo.tipo,
		                            ProdutoCategoria.categoriaId
	                            FROM AvisoDisponibilidade
	                            INNER JOIN Produto ON Produto.produtoId = AvisoDisponibilidade.produtoId
	                            INNER JOIN ProdutoTipo ON Produto.produtoTipoId = ProdutoTipo.produtoTipoId
	                            INNER JOIN ProdutoCategoria ON Produto.produtoId = ProdutoCategoria.produtoId
	                            WHERE
		                            AvisoDisponibilidade.dataNotificacao IS NULL
		                            AND AvisoDisponibilidade.avisoDisponibilidadeStatusId = 1
		                            AND Produto.disponivel = 1
		                            AND Produto.exibirSite = 1
		                            AND Produto.homologado = 1
                            ),
                            TitulosAvisos (avisoDisponibilidadeId, email, dataSolicitacao, produtoId, tipo, categoriaId, tituloId)
                            AS
                            (
	                            SELECT Avisos.avisoDisponibilidadeId,
		                            Avisos.email,
                                    Avisos.dataSolicitacao,
		                            Avisos.produtoId,
		                            Avisos.tipo,
		                            Avisos.categoriaId,
		                            TituloImpresso.tituloId
	                            FROM Avisos
	                            INNER JOIN TituloImpresso ON Avisos.produtoId = TituloImpresso.tituloImpressoId
	                            UNION
	                            SELECT Avisos.avisoDisponibilidadeId,
		                            Avisos.email,
                                    Avisos.dataSolicitacao,
		                            Avisos.produtoId,
		                            Avisos.tipo,
		                            Avisos.categoriaId,
		                            TituloEletronico.tituloId
	                            FROM Avisos
	                            INNER JOIN TituloEletronico ON Avisos.produtoId = TituloEletronico.tituloEletronicoId
                            )
                            SELECT 
                                TitulosAvisos.avisoDisponibilidadeId,
                                TitulosAvisos.email,
                                TitulosAvisos.dataSolicitacao,
	                            TitulosAvisos.tipo,
	                            TitulosAvisos.produtoId,
	                            TitulosAvisos.categoriaId,
	                            TitulosAvisos.tituloId,
	                            Titulo.nomeTitulo,
	                            Titulo.subtituloLivro,
	                            Titulo.edicao
                            FROM TitulosAvisos
                            INNER JOIN Titulo ON TitulosAvisos.tituloId = Titulo.tituloId");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                AvisoDisponibilidade entidadeRetorno = new AvisoDisponibilidade();
                PopulaAvisoDisponibilidadeCompleto(reader, entidadeRetorno);
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
        public static void PopulaAvisoDisponibilidadeCompleto(IDataReader reader, AvisoDisponibilidade entidade)
        {
            if (reader["avisoDisponibilidadeId"] != DBNull.Value)
            {
                entidade.AvisoDisponibilidadeId = Convert.ToInt32(reader["avisoDisponibilidadeId"].ToString());
            }

            if (reader["email"] != DBNull.Value)
                entidade.Email = reader["email"].ToString();

            if (reader["dataSolicitacao"] != DBNull.Value)
                entidade.DataSolicitacao = Convert.ToDateTime(reader["dataSolicitacao"].ToString());

            if (reader["produtoId"] != DBNull.Value)
            {
                if (entidade.Produto == null) entidade.Produto = new Produto();
                entidade.Produto.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());
            }

            if (reader["categoriaId"] != DBNull.Value)
            {
                if (entidade.Produto == null) entidade.Produto = new Produto();
                if (entidade.Produto.Categorias == null) entidade.Produto.Categorias = new List<Categoria>();
                entidade.Produto.Categorias.Add(new Categoria() { CategoriaId = Convert.ToInt32(reader["categoriaId"].ToString())});
            }

            if (reader["tipo"] != DBNull.Value)
            {
                if (entidade.Produto == null) entidade.Produto = new Produto();
                if (entidade.Produto.ProdutoTipo == null) entidade.Produto.ProdutoTipo = new ProdutoTipo();
                entidade.Produto.ProdutoTipo.Tipo = reader["tipo"].ToString();
            }

            if (reader["tituloId"] != DBNull.Value)
            {
                if (entidade.Titulo == null) entidade.Titulo = new Titulo();
                entidade.Titulo.TituloId = Convert.ToInt32(reader["tituloId"].ToString());
            }

            if (reader["nomeTitulo"] != DBNull.Value)
            {
                if (entidade.Titulo == null) entidade.Titulo = new Titulo();
                entidade.Titulo.NomeTitulo = reader["nomeTitulo"].ToString();
            }

            if (reader["subtituloLivro"] != DBNull.Value)
            {
                if (entidade.Titulo == null) entidade.Titulo = new Titulo();
                entidade.Titulo.SubtituloLivro = reader["subtituloLivro"].ToString();
            }

            if (reader["edicao"] != DBNull.Value)
            {
                if (entidade.Titulo == null) entidade.Titulo = new Titulo();
                entidade.Titulo.Edicao = Convert.ToInt32(reader["edicao"].ToString());
            }
        }
    }
}
