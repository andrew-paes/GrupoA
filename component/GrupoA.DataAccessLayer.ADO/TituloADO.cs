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
    public partial class TituloADO : ADOSuper, ITituloDAL
    {
        /// <summary>
        /// Método que persiste um Titulo.
        /// </summary>
        /// <param name="entidade">Titulo contendo os dados a serem persistidos.</param>	
        public void Inserir(Titulo entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO Titulo ");
            sbSQL.Append(" (tituloId, subtituloLivro, numeroPaginas, edicao, dataLancamento, dataPublicacao, maisVendido, nomeTitulo, formato, maisVendidoOrdem) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@tituloId, @subtituloLivro, @numeroPaginas, @edicao, @dataLancamento, @dataPublicacao, @maisVendido, @nomeTitulo, @formato, @maisVendidoOrdem) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.TituloId);

            if (entidade.SubtituloLivro != null)
                _db.AddInParameter(command, "@subtituloLivro", DbType.String, entidade.SubtituloLivro);
            else
                _db.AddInParameter(command, "@subtituloLivro", DbType.String, null);

            if (entidade.NumeroPaginas != null)
                _db.AddInParameter(command, "@numeroPaginas", DbType.Int32, entidade.NumeroPaginas);
            else
                _db.AddInParameter(command, "@numeroPaginas", DbType.Int32, null);

            if (entidade.Edicao != null)
                _db.AddInParameter(command, "@edicao", DbType.Int32, entidade.Edicao);
            else
                _db.AddInParameter(command, "@edicao", DbType.Int32, null);

            if (entidade.DataLancamento != null && entidade.DataLancamento != DateTime.MinValue)
                _db.AddInParameter(command, "@dataLancamento", DbType.DateTime, entidade.DataLancamento);
            else
                _db.AddInParameter(command, "@dataLancamento", DbType.DateTime, null);

            if (entidade.DataPublicacao != null && entidade.DataPublicacao != DateTime.MinValue)
                _db.AddInParameter(command, "@dataPublicacao", DbType.DateTime, entidade.DataPublicacao);
            else
                _db.AddInParameter(command, "@dataPublicacao", DbType.DateTime, null);

            _db.AddInParameter(command, "@maisVendido", DbType.Int32, entidade.MaisVendido);

            if (entidade.NomeTitulo != null)
                _db.AddInParameter(command, "@nomeTitulo", DbType.String, entidade.NomeTitulo);
            else
                _db.AddInParameter(command, "@nomeTitulo", DbType.String, null);

            if (entidade.Formato != null)
                _db.AddInParameter(command, "@formato", DbType.String, entidade.Formato);
            else
                _db.AddInParameter(command, "@formato", DbType.String, null);

            if (entidade.MaisVendidoOrdem != null)
                _db.AddInParameter(command, "@maisVendidoOrdem", DbType.Int32, entidade.MaisVendidoOrdem);
            else
                _db.AddInParameter(command, "@maisVendidoOrdem", DbType.Int32, null);


            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que atualiza os dados de um Titulo.
        /// </summary>
        /// <param name="entidade">Titulo contendo os dados a serem atualizados.</param>
        public void Atualizar(Titulo entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE Titulo SET ");
            sbSQL.Append(" subtituloLivro=@subtituloLivro, numeroPaginas=@numeroPaginas, edicao=@edicao, dataLancamento=@dataLancamento, dataPublicacao=@dataPublicacao, maisVendido=@maisVendido, nomeTitulo=@nomeTitulo, formato=@formato, maisVendidoOrdem=@maisVendidoOrdem ");
            sbSQL.Append(" WHERE tituloId=@tituloId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.TituloId);
            if (entidade.SubtituloLivro != null)
                _db.AddInParameter(command, "@subtituloLivro", DbType.String, entidade.SubtituloLivro);
            else
                _db.AddInParameter(command, "@subtituloLivro", DbType.String, null);
            if (entidade.NumeroPaginas != null)
                _db.AddInParameter(command, "@numeroPaginas", DbType.Int32, entidade.NumeroPaginas);
            else
                _db.AddInParameter(command, "@numeroPaginas", DbType.Int32, null);
            if (entidade.Edicao != null)
                _db.AddInParameter(command, "@edicao", DbType.Int32, entidade.Edicao);
            else
                _db.AddInParameter(command, "@edicao", DbType.Int32, null);
            if (entidade.DataLancamento != null && entidade.DataLancamento != DateTime.MinValue)
                _db.AddInParameter(command, "@dataLancamento", DbType.DateTime, entidade.DataLancamento);
            else
                _db.AddInParameter(command, "@dataLancamento", DbType.DateTime, null);
            if (entidade.DataPublicacao != null && entidade.DataPublicacao != DateTime.MinValue)
                _db.AddInParameter(command, "@dataPublicacao", DbType.DateTime, entidade.DataPublicacao);
            else
                _db.AddInParameter(command, "@dataPublicacao", DbType.DateTime, null);
            _db.AddInParameter(command, "@maisVendido", DbType.Int32, entidade.MaisVendido);
            if (entidade.NomeTitulo != null)
                _db.AddInParameter(command, "@nomeTitulo", DbType.String, entidade.NomeTitulo);
            else
                _db.AddInParameter(command, "@nomeTitulo", DbType.String, null);
            if (entidade.Formato != null)
                _db.AddInParameter(command, "@formato", DbType.String, entidade.Formato);
            else
                _db.AddInParameter(command, "@formato", DbType.String, null);

            if (entidade.MaisVendidoOrdem != null)
                _db.AddInParameter(command, "@maisVendidoOrdem", DbType.Int32, entidade.MaisVendidoOrdem);
            else
                _db.AddInParameter(command, "@maisVendidoOrdem", DbType.Int32, null);

            // Executa a query.
            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que remove um Titulo da base de dados.
        /// </summary>
        /// <param name="entidade">Titulo a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(Titulo entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM Titulo ");
            sbSQL.Append("WHERE tituloId=@tituloId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.TituloId);

            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que carrega um Titulo.
        /// </summary>
        /// <param name="entidade">Titulo a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Titulo</returns>
        public Titulo Carregar(Titulo entidade)
        {
            Titulo entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM Titulo WHERE tituloId=@tituloId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.TituloId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Titulo();
                PopulaTitulo(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Método que carrega um Titulo com suas dependências.
        /// </summary>
        /// <param name="entidade">Titulo a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Titulo</returns>
        public Titulo CarregarComDependencias(Titulo entidade)
        {
            Titulo entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT Titulo.tituloId, Titulo.subtituloLivro, Titulo.numeroPaginas, Titulo.edicao, Titulo.dataLancamento, Titulo.dataPublicacao, Titulo.maisVendido, Titulo.nomeTitulo, Titulo.formato");
            sbSQL.Append(", conteudoId, conteudoTipoId, dataHoraCadastro");
            sbSQL.Append(", tituloEletronicoId, TituloEletronico.isbn13 isbn13TituloEletronico");
            sbSQL.Append(", tituloImpressoId, isbn10, TituloImpresso.isbn13 isbn13TituloImpresso");
            sbSQL.Append(" FROM Titulo");
            sbSQL.Append(" LEFT JOIN TituloEletronico ON Titulo.tituloId=TituloEletronico.tituloId");
            sbSQL.Append(" LEFT JOIN TituloImpresso ON Titulo.tituloId=TituloImpresso.tituloId");
            sbSQL.Append(" INNER JOIN Conteudo ON Titulo.tituloId=Conteudo.conteudoId");
            sbSQL.Append(" WHERE Titulo.tituloId=@tituloId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.TituloId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Titulo();
                entidadeRetorno.TituloEletronico = new TituloEletronico();
                entidadeRetorno.TituloImpresso = new TituloImpresso();
                entidadeRetorno.Conteudo = new Conteudo();
                //Detectada colisão de nome de colunas, implementar o método de população manualmente.
                PopulaTituloComDependencia(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Método que retorna uma coleção de Titulo.
        /// </summary>
        /// <param name="entidade">Capitulo relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Titulo.</returns>
        public IEnumerable<Titulo> Carregar(Capitulo entidade)
        {
            List<Titulo> entidadesRetorno = new List<Titulo>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Titulo.* FROM Titulo INNER JOIN Capitulo ON Titulo.tituloId=Capitulo.tituloId WHERE Capitulo.capituloId=@capituloId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@capituloId", DbType.Int32, entidade.CapituloId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Titulo entidadeRetorno = new Titulo();
                PopulaTitulo(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// Método que retorna uma coleção de Titulo.
        /// </summary>
        /// <param name="entidade">DestaqueTituloImpresso relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Titulo.</returns>
        public IEnumerable<Titulo> Carregar(DestaqueTituloImpresso entidade)
        {
            List<Titulo> entidadesRetorno = new List<Titulo>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Titulo.* FROM Titulo INNER JOIN DestaqueTituloImpressoRelacionado ON Titulo.tituloId=DestaqueTituloImpressoRelacionado.tituloId WHERE DestaqueTituloImpressoRelacionado.destaqueTituloImpressoId=@destaqueTituloImpressoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@destaqueTituloImpressoId", DbType.Int32, entidade.DestaqueTituloImpressoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Titulo entidadeRetorno = new Titulo();
                PopulaTitulo(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// Método que retorna uma coleção de Titulo.
        /// </summary>
        /// <param name="entidade">TituloConteudoExtraArquivo relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Titulo.</returns>
        public IEnumerable<Titulo> Carregar(TituloConteudoExtraArquivo entidade)
        {
            List<Titulo> entidadesRetorno = new List<Titulo>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Titulo.* FROM Titulo INNER JOIN TituloConteudoExtraArquivo ON Titulo.tituloId=TituloConteudoExtraArquivo.tituloId WHERE TituloConteudoExtraArquivo.tituloConteudoExtraArquivoId=@tituloConteudoExtraArquivoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@tituloConteudoExtraArquivoId", DbType.Int32, entidade.TituloConteudoExtraArquivoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Titulo entidadeRetorno = new Titulo();
                PopulaTitulo(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// Método que retorna uma coleção de Titulo.
        /// </summary>
        /// <param name="entidade">TituloEletronico relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Titulo.</returns>
        public IEnumerable<Titulo> Carregar(TituloEletronico entidade)
        {
            List<Titulo> entidadesRetorno = new List<Titulo>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Titulo.* FROM Titulo INNER JOIN TituloEletronico ON Titulo.tituloId=TituloEletronico.tituloId WHERE TituloEletronico.tituloEletronicoId=@tituloEletronicoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@tituloEletronicoId", DbType.Int32, entidade.TituloEletronicoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Titulo entidadeRetorno = new Titulo();
                PopulaTitulo(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// Método que retorna uma coleção de Titulo.
        /// </summary>
        /// <param name="entidade">TituloImagemResumo relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Titulo.</returns>
        public IEnumerable<Titulo> Carregar(TituloImagemResumo entidade)
        {
            List<Titulo> entidadesRetorno = new List<Titulo>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Titulo.* FROM Titulo INNER JOIN TituloImagemResumo ON Titulo.tituloId=TituloImagemResumo.tituloId WHERE TituloImagemResumo.tituloImagemResumoId=@tituloImagemResumoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@tituloImagemResumoId", DbType.Int32, entidade.TituloImagemResumoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Titulo entidadeRetorno = new Titulo();
                PopulaTitulo(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// Método que retorna um Titulo.
        /// </summary>
        /// <param name="entidade">TituloImpresso relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna um Titulo.</returns>
        public Titulo Carregar(TituloImpresso entidade)
        {
            Titulo entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Titulo.* FROM Titulo INNER JOIN TituloImpresso ON Titulo.tituloId=TituloImpresso.tituloId WHERE TituloImpresso.tituloImpressoId=@tituloImpressoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@tituloImpressoId", DbType.Int32, entidade.TituloImpressoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Titulo();
                PopulaTitulo(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Método que retorna uma coleção de Titulo.
        /// </summary>
        /// <param name="entidade">TituloSolicitacao relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Titulo.</returns>
        public IEnumerable<Titulo> Carregar(TituloSolicitacao entidade)
        {
            List<Titulo> entidadesRetorno = new List<Titulo>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Titulo.* FROM Titulo INNER JOIN TituloSolicitacao ON Titulo.tituloId=TituloSolicitacao.tituloId WHERE TituloSolicitacao.tituloSolicitacaoId=@tituloSolicitacaoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@tituloSolicitacaoId", DbType.Int32, entidade.TituloSolicitacaoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Titulo entidadeRetorno = new Titulo();
                PopulaTitulo(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// Método que retorna uma coleção de Titulo.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos Titulo.</returns>
        public IEnumerable<Titulo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {
            List<Titulo> entidadesRetorno = new List<Titulo>();

            StringBuilder sbSQL = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            StringBuilder sbOrder = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            // Monta o "OrderBy"
            if (ordemColunas != null)
            {
                for (int i = 0; i < ordemColunas.Length; i++)
                {
                    if (sbOrder.Length > 0) { sbOrder.Append(", "); }
                    sbOrder.Append(ordemColunas[i] + " " + ordemSentidos[i]);
                }
                if (sbOrder.Length > 0) { sbOrder.Insert(0, " ORDER BY "); }
            }
            else
            {
                sbOrder.Append(" ORDER BY tituloId");
            }

            if (registrosPagina > 0)
            {
                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Titulo");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Titulo WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Titulo ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT Titulo.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Titulo ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT Titulo.* FROM Titulo ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Titulo entidadeRetorno = new Titulo();
                PopulaTitulo(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna todas os Titulo existentes na base de dados.
        /// </summary>
        public IEnumerable<Titulo> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de Titulo na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de Titulo na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM Titulo");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um Titulo baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Titulo a ser populado(.</param>
        public static void PopulaTitulo(IDataReader reader, Titulo entidade)
        {
            if (reader["subtituloLivro"] != DBNull.Value)
                entidade.SubtituloLivro = reader["subtituloLivro"].ToString();

            if (reader["numeroPaginas"] != DBNull.Value)
                entidade.NumeroPaginas = Convert.ToInt32(reader["numeroPaginas"].ToString());

            if (reader["edicao"] != DBNull.Value)
                entidade.Edicao = Convert.ToInt32(reader["edicao"].ToString());

            if (reader["dataLancamento"] != DBNull.Value)
                entidade.DataLancamento = Convert.ToDateTime(reader["dataLancamento"].ToString());

            if (reader["dataPublicacao"] != DBNull.Value)
                entidade.DataPublicacao = Convert.ToDateTime(reader["dataPublicacao"].ToString());

            if (reader["maisVendido"] != DBNull.Value)
                entidade.MaisVendido = Convert.ToBoolean(reader["maisVendido"].ToString());

            if (reader["nomeTitulo"] != DBNull.Value)
                entidade.NomeTitulo = reader["nomeTitulo"].ToString();

            try
            {
                if (reader["formato"] != DBNull.Value)
                    entidade.Formato = reader["formato"].ToString();
            }
            catch { }

            try
            {
                if (reader["maisVendidoOrdem"] != DBNull.Value)
                    entidade.MaisVendidoOrdem = Convert.ToInt32(reader["maisVendidoOrdem"].ToString());
            }
            catch { }

            if (reader["tituloId"] != DBNull.Value)
            {
                entidade.TituloId = Convert.ToInt32(reader["tituloId"].ToString());
            }


        }
    }
}