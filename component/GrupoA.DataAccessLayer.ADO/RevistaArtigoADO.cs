
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
	public partial class RevistaArtigoADO : ADOSuper, IRevistaArtigoDAL {
	
	    /// <summary>
        /// Método que persiste um RevistaArtigo.
        /// </summary>
        /// <param name="entidade">RevistaArtigo contendo os dados a serem persistidos.</param>	
		public void Inserir(RevistaArtigo entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO RevistaArtigo ");
			sbSQL.Append(" (revistaArtigoId, revistaEdicaoId, tituloArtigo, subTituloArtigo, resumo, textoArtigo, autores, revistaSecaoId, revistaArtigoPermissaoId, arquivoIdThumbP, arquivoIdThumbM, arquivoIdCapa, arquivoIdLateral, bibliografia, destaquePrincipal, destaqueHome, revistaArtigoIdAssociado, conteudoOnline, ativo, dataPublicacao) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@revistaArtigoId, @revistaEdicaoId, @tituloArtigo, @subTituloArtigo, @resumo, @textoArtigo, @autores, @revistaSecaoId, @revistaArtigoPermissaoId, @arquivoIdThumbP, @arquivoIdThumbM, @arquivoIdCapa, @arquivoIdLateral, @bibliografia, @destaquePrincipal, @destaqueHome, @revistaArtigoIdAssociado, @conteudoOnline, @ativo, @dataPublicacao) ");											

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@revistaArtigoId", DbType.Int32, entidade.RevistaArtigoId);

			if (entidade.RevistaEdicao != null ) 
				_db.AddInParameter(command, "@revistaEdicaoId", DbType.Int32, entidade.RevistaEdicao.RevistaEdicaoId);
			else
				_db.AddInParameter(command, "@revistaEdicaoId", DbType.Int32, null);

			_db.AddInParameter(command, "@tituloArtigo", DbType.String, entidade.TituloArtigo);

			if (entidade.SubTituloArtigo != null ) 
				_db.AddInParameter(command, "@subTituloArtigo", DbType.String, entidade.SubTituloArtigo);
			else
				_db.AddInParameter(command, "@subTituloArtigo", DbType.String, null);

			_db.AddInParameter(command, "@resumo", DbType.String, entidade.Resumo);

			_db.AddInParameter(command, "@textoArtigo", DbType.String, entidade.TextoArtigo);

			if (entidade.Autores != null ) 
				_db.AddInParameter(command, "@autores", DbType.String, entidade.Autores);
			else
				_db.AddInParameter(command, "@autores", DbType.String, null);

			_db.AddInParameter(command, "@revistaSecaoId", DbType.Int32, entidade.RevistaSecao.RevistaSecaoId);

			_db.AddInParameter(command, "@revistaArtigoPermissaoId", DbType.Int32, entidade.RevistaArtigoPermissao.RevistaArtigoPermissaoId);

			if (entidade.ArquivoThumbP != null ) 
				_db.AddInParameter(command, "@arquivoIdThumbP", DbType.Int32, entidade.ArquivoThumbP.ArquivoId);
			else
				_db.AddInParameter(command, "@arquivoIdThumbP", DbType.Int32, null);

			if (entidade.ArquivoThumbM != null ) 
				_db.AddInParameter(command, "@arquivoIdThumbM", DbType.Int32, entidade.ArquivoThumbM.ArquivoId);
			else
				_db.AddInParameter(command, "@arquivoIdThumbM", DbType.Int32, null);

			if (entidade.ArquivoCapa != null ) 
				_db.AddInParameter(command, "@arquivoIdCapa", DbType.Int32, entidade.ArquivoCapa.ArquivoId);
			else
				_db.AddInParameter(command, "@arquivoIdCapa", DbType.Int32, null);

			if (entidade.ArquivoLateral != null ) 
				_db.AddInParameter(command, "@arquivoIdLateral", DbType.Int32, entidade.ArquivoLateral.ArquivoId);
			else
				_db.AddInParameter(command, "@arquivoIdLateral", DbType.Int32, null);

			if (entidade.Bibliografia != null ) 
				_db.AddInParameter(command, "@bibliografia", DbType.String, entidade.Bibliografia);
			else
				_db.AddInParameter(command, "@bibliografia", DbType.String, null);

			_db.AddInParameter(command, "@destaquePrincipal", DbType.Int32, entidade.DestaquePrincipal);

			_db.AddInParameter(command, "@destaqueHome", DbType.Int32, entidade.DestaqueHome);

			if (entidade.RevistaArtigoAssociado != null ) 
				_db.AddInParameter(command, "@revistaArtigoIdAssociado", DbType.Int32, entidade.RevistaArtigoAssociado.RevistaArtigoId);
			else
				_db.AddInParameter(command, "@revistaArtigoIdAssociado", DbType.Int32, null);

			_db.AddInParameter(command, "@conteudoOnline", DbType.Int32, entidade.ConteudoOnline);

			_db.AddInParameter(command, "@ativo", DbType.Int32, entidade.Ativo);

			_db.AddInParameter(command, "@dataPublicacao", DbType.DateTime, entidade.DataPublicacao);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um RevistaArtigo.
        /// </summary>
        /// <param name="entidade">RevistaArtigo contendo os dados a serem atualizados.</param>
		public void Atualizar(RevistaArtigo entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE RevistaArtigo SET ");
			sbSQL.Append(" revistaEdicaoId=@revistaEdicaoId, tituloArtigo=@tituloArtigo, subTituloArtigo=@subTituloArtigo, resumo=@resumo, textoArtigo=@textoArtigo, autores=@autores, revistaSecaoId=@revistaSecaoId, revistaArtigoPermissaoId=@revistaArtigoPermissaoId, arquivoIdThumbP=@arquivoIdThumbP, arquivoIdThumbM=@arquivoIdThumbM, arquivoIdCapa=@arquivoIdCapa, arquivoIdLateral=@arquivoIdLateral, bibliografia=@bibliografia, destaquePrincipal=@destaquePrincipal, destaqueHome=@destaqueHome, revistaArtigoIdAssociado=@revistaArtigoIdAssociado, conteudoOnline=@conteudoOnline, ativo=@ativo, dataPublicacao=@dataPublicacao ");
			sbSQL.Append(" WHERE revistaArtigoId=@revistaArtigoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@revistaArtigoId", DbType.Int32, entidade.RevistaArtigoId);
			if (entidade.RevistaEdicao != null ) 
				_db.AddInParameter(command, "@revistaEdicaoId", DbType.Int32, entidade.RevistaEdicao.RevistaEdicaoId);
			else
				_db.AddInParameter(command, "@revistaEdicaoId", DbType.Int32, null);
			_db.AddInParameter(command, "@tituloArtigo", DbType.String, entidade.TituloArtigo);
			if (entidade.SubTituloArtigo != null ) 
				_db.AddInParameter(command, "@subTituloArtigo", DbType.String, entidade.SubTituloArtigo);
			else
				_db.AddInParameter(command, "@subTituloArtigo", DbType.String, null);
			_db.AddInParameter(command, "@resumo", DbType.String, entidade.Resumo);
			_db.AddInParameter(command, "@textoArtigo", DbType.String, entidade.TextoArtigo);
			if (entidade.Autores != null ) 
				_db.AddInParameter(command, "@autores", DbType.String, entidade.Autores);
			else
				_db.AddInParameter(command, "@autores", DbType.String, null);
			_db.AddInParameter(command, "@revistaSecaoId", DbType.Int32, entidade.RevistaSecao.RevistaSecaoId);
			_db.AddInParameter(command, "@revistaArtigoPermissaoId", DbType.Int32, entidade.RevistaArtigoPermissao.RevistaArtigoPermissaoId);
			if (entidade.ArquivoThumbP != null ) 
				_db.AddInParameter(command, "@arquivoIdThumbP", DbType.Int32, entidade.ArquivoThumbP.ArquivoId);
			else
				_db.AddInParameter(command, "@arquivoIdThumbP", DbType.Int32, null);
			if (entidade.ArquivoThumbM != null ) 
				_db.AddInParameter(command, "@arquivoIdThumbM", DbType.Int32, entidade.ArquivoThumbM.ArquivoId);
			else
				_db.AddInParameter(command, "@arquivoIdThumbM", DbType.Int32, null);
			if (entidade.ArquivoCapa != null ) 
				_db.AddInParameter(command, "@arquivoIdCapa", DbType.Int32, entidade.ArquivoCapa.ArquivoId);
			else
				_db.AddInParameter(command, "@arquivoIdCapa", DbType.Int32, null);
			if (entidade.ArquivoLateral != null ) 
				_db.AddInParameter(command, "@arquivoIdLateral", DbType.Int32, entidade.ArquivoLateral.ArquivoId);
			else
				_db.AddInParameter(command, "@arquivoIdLateral", DbType.Int32, null);
			if (entidade.Bibliografia != null ) 
				_db.AddInParameter(command, "@bibliografia", DbType.String, entidade.Bibliografia);
			else
				_db.AddInParameter(command, "@bibliografia", DbType.String, null);
			_db.AddInParameter(command, "@destaquePrincipal", DbType.Int32, entidade.DestaquePrincipal);
			_db.AddInParameter(command, "@destaqueHome", DbType.Int32, entidade.DestaqueHome);
			if (entidade.RevistaArtigoAssociado != null ) 
				_db.AddInParameter(command, "@revistaArtigoIdAssociado", DbType.Int32, entidade.RevistaArtigoAssociado.RevistaArtigoId);
			else
				_db.AddInParameter(command, "@revistaArtigoIdAssociado", DbType.Int32, null);
			_db.AddInParameter(command, "@conteudoOnline", DbType.Int32, entidade.ConteudoOnline);
			_db.AddInParameter(command, "@ativo", DbType.Int32, entidade.Ativo);
			_db.AddInParameter(command, "@dataPublicacao", DbType.DateTime, entidade.DataPublicacao);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um RevistaArtigo da base de dados.
        /// </summary>
        /// <param name="entidade">RevistaArtigo a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(RevistaArtigo entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM RevistaArtigo ");
			sbSQL.Append("WHERE revistaArtigoId=@revistaArtigoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@revistaArtigoId", DbType.Int32, entidade.RevistaArtigoId);

								
			_db.ExecuteNonQuery(command);
		}
		

		/// <summary>
		/// Método que carrega um RevistaArtigo.
		/// </summary>
        /// <param name="entidade">RevistaArtigo a ser carregado (somente o identificador é necessário).</param>
		/// <returns>RevistaArtigo</returns>
		public RevistaArtigo Carregar(RevistaArtigo entidade) {		
		
			RevistaArtigo entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM RevistaArtigo WHERE revistaArtigoId=@revistaArtigoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@revistaArtigoId", DbType.Int32, entidade.RevistaArtigoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new RevistaArtigo();
				PopulaRevistaArtigo(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		
		
		/// <summary>
		/// Método que carrega um RevistaArtigo com suas dependências.
		/// </summary>
        /// <param name="entidade">RevistaArtigo a ser carregado (somente o identificador é necessário).</param>
		/// <returns>RevistaArtigo</returns>
		public RevistaArtigo CarregarComDependencias(RevistaArtigo entidade) {		
		
			RevistaArtigo entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			sbSQL.Append("SELECT RevistaArtigo.revistaArtigoId, RevistaArtigo.revistaEdicaoId, RevistaArtigo.tituloArtigo, RevistaArtigo.subTituloArtigo, RevistaArtigo.resumo, RevistaArtigo.textoArtigo, RevistaArtigo.autores, RevistaArtigo.revistaSecaoId, RevistaArtigo.revistaArtigoPermissaoId, RevistaArtigo.arquivoIdThumbP, RevistaArtigo.arquivoIdThumbM, RevistaArtigo.arquivoIdCapa, RevistaArtigo.arquivoIdLateral, RevistaArtigo.bibliografia, RevistaArtigo.destaquePrincipal, RevistaArtigo.destaqueHome, RevistaArtigo.revistaArtigoIdAssociado, RevistaArtigo.conteudoOnline, RevistaArtigo.ativo, RevistaArtigo.dataPublicacao");
			sbSQL.Append(", conteudoId, conteudoTipoId, dataHoraCadastro");
			sbSQL.Append(" FROM RevistaArtigo");
			sbSQL.Append(" INNER JOIN Conteudo ON RevistaArtigo.revistaArtigoId=Conteudo.conteudoId");
			sbSQL.Append(" WHERE RevistaArtigo.revistaArtigoId=@revistaArtigoId");
						
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@revistaArtigoId", DbType.Int32, entidade.RevistaArtigoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new RevistaArtigo();
				PopulaRevistaArtigo(reader, entidadeRetorno);
				entidadeRetorno.Conteudo = new Conteudo();
				ConteudoADO.PopulaConteudo(reader, entidadeRetorno.Conteudo);
			}
			reader.Close();
			
			return entidadeRetorno;
		}		


		/// <summary>
        /// Método que retorna uma coleção de RevistaArtigo.
        /// </summary>
        /// <param name="entidade">RevistaArtigoControversia relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de RevistaArtigo.</returns>
		public IEnumerable<RevistaArtigo> Carregar(RevistaArtigoControversia entidade)
		{		
			List<RevistaArtigo> entidadesRetorno = new List<RevistaArtigo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT RevistaArtigo.* FROM RevistaArtigo INNER JOIN RevistaArtigoControversia ON RevistaArtigo.revistaArtigoId=RevistaArtigoControversia.revistaArtigoId WHERE RevistaArtigoControversia.revistaArtigoControversiaId=@revistaArtigoControversiaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@revistaArtigoControversiaId", DbType.Int32, entidade.RevistaArtigoControversiaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                RevistaArtigo entidadeRetorno = new RevistaArtigo();
                PopulaRevistaArtigo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de RevistaArtigo.
        /// </summary>
        /// <param name="entidade">Arquivo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de RevistaArtigo.</returns>
		public IEnumerable<RevistaArtigo> Carregar(Arquivo entidade)
		{		
			List<RevistaArtigo> entidadesRetorno = new List<RevistaArtigo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT RevistaArtigo.* FROM RevistaArtigo INNER JOIN RevistaArtigoGaleriaImagem ON RevistaArtigo.revistaArtigoId=RevistaArtigoGaleriaImagem.revistaArtigoId WHERE RevistaArtigoGaleriaImagem.arquivoId=@arquivoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.ArquivoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                RevistaArtigo entidadeRetorno = new RevistaArtigo();
                PopulaRevistaArtigo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de RevistaArtigo.
        /// </summary>
        /// <param name="entidade">Produto relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de RevistaArtigo.</returns>
		public IEnumerable<RevistaArtigo> Carregar(Produto entidade)
		{		
			List<RevistaArtigo> entidadesRetorno = new List<RevistaArtigo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT RevistaArtigo.* FROM RevistaArtigo INNER JOIN RevistaArtigoProduto ON RevistaArtigo.revistaArtigoId=RevistaArtigoProduto.revistaArtigoId WHERE RevistaArtigoProduto.produtoId=@produtoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                RevistaArtigo entidadeRetorno = new RevistaArtigo();
                PopulaRevistaArtigo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de RevistaArtigo.
        /// </summary>
        /// <param name="entidade">RevistaArtigoPermissao relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de RevistaArtigo.</returns>
		public IEnumerable<RevistaArtigo> Carregar(RevistaArtigoPermissao entidade)
		{		
			List<RevistaArtigo> entidadesRetorno = new List<RevistaArtigo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT RevistaArtigo.* FROM RevistaArtigo WHERE RevistaArtigo.revistaArtigoPermissaoId=@revistaArtigoPermissaoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@revistaArtigoPermissaoId", DbType.Int32, entidade.RevistaArtigoPermissaoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                RevistaArtigo entidadeRetorno = new RevistaArtigo();
                PopulaRevistaArtigo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de RevistaArtigo.
        /// </summary>
        /// <param name="entidade">RevistaEdicao relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de RevistaArtigo.</returns>
		public IEnumerable<RevistaArtigo> Carregar(RevistaEdicao entidade)
		{		
			List<RevistaArtigo> entidadesRetorno = new List<RevistaArtigo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT RevistaArtigo.* FROM RevistaArtigo WHERE RevistaArtigo.revistaEdicaoId=@revistaEdicaoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@revistaEdicaoId", DbType.Int32, entidade.RevistaEdicaoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                RevistaArtigo entidadeRetorno = new RevistaArtigo();
                PopulaRevistaArtigo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de RevistaArtigo.
        /// </summary>
        /// <param name="entidade">RevistaSecao relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de RevistaArtigo.</returns>
		public IEnumerable<RevistaArtigo> Carregar(RevistaSecao entidade)
		{		
			List<RevistaArtigo> entidadesRetorno = new List<RevistaArtigo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT RevistaArtigo.* FROM RevistaArtigo WHERE RevistaArtigo.revistaSecaoId=@revistaSecaoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@revistaSecaoId", DbType.Int32, entidade.RevistaSecaoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                RevistaArtigo entidadeRetorno = new RevistaArtigo();
                PopulaRevistaArtigo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de RevistaArtigo.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos RevistaArtigo.</returns>
		public IEnumerable<RevistaArtigo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<RevistaArtigo> entidadesRetorno = new List<RevistaArtigo>();
			
			StringBuilder sbSQL = new StringBuilder();
			StringBuilder sbWhere = new StringBuilder();
			StringBuilder sbOrder = new StringBuilder();
			DbCommand command;
			IDataReader reader;
			
			// Monta o "OrderBy"
			if (ordemColunas!=null) {
				for(int i=0; i<ordemColunas.Length; i++) {
					if (sbOrder.Length>0) { sbOrder.Append( ", " ); }
					sbOrder.Append(ordemColunas[i] + " " + ordemSentidos[i]);
				} 
				if (sbOrder.Length > 0) { sbOrder.Insert(0, " ORDER BY "); }				
			} else {
				sbOrder.Append( " ORDER BY revistaArtigoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM RevistaArtigo");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM RevistaArtigo WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM RevistaArtigo ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT RevistaArtigo.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM RevistaArtigo ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT RevistaArtigo.* FROM RevistaArtigo ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                RevistaArtigo entidadeRetorno = new RevistaArtigo();
                PopulaRevistaArtigo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os RevistaArtigo existentes na base de dados.
        /// </summary>
		public IEnumerable<RevistaArtigo> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de RevistaArtigo na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de RevistaArtigo na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM RevistaArtigo");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um RevistaArtigo baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">RevistaArtigo a ser populado(.</param>
		public static void PopulaRevistaArtigo(IDataReader reader, RevistaArtigo entidade) 
		{						
			if (reader["tituloArtigo"] != DBNull.Value)
				entidade.TituloArtigo = reader["tituloArtigo"].ToString();
			
			if (reader["subTituloArtigo"] != DBNull.Value)
				entidade.SubTituloArtigo = reader["subTituloArtigo"].ToString();
			
			if (reader["resumo"] != DBNull.Value)
				entidade.Resumo = reader["resumo"].ToString();
			
			if (reader["textoArtigo"] != DBNull.Value)
				entidade.TextoArtigo = reader["textoArtigo"].ToString();
			
			if (reader["autores"] != DBNull.Value)
				entidade.Autores = reader["autores"].ToString();
			
			if (reader["bibliografia"] != DBNull.Value)
				entidade.Bibliografia = reader["bibliografia"].ToString();
			
			if (reader["destaquePrincipal"] != DBNull.Value)
				entidade.DestaquePrincipal = Convert.ToBoolean(reader["destaquePrincipal"].ToString());
			
			if (reader["destaqueHome"] != DBNull.Value)
				entidade.DestaqueHome = Convert.ToBoolean(reader["destaqueHome"].ToString());
			
			if (reader["conteudoOnline"] != DBNull.Value)
				entidade.ConteudoOnline = Convert.ToBoolean(reader["conteudoOnline"].ToString());
			
			if (reader["ativo"] != DBNull.Value)
				entidade.Ativo = Convert.ToBoolean(reader["ativo"].ToString());
			
			if (reader["dataPublicacao"] != DBNull.Value)
				entidade.DataPublicacao = Convert.ToDateTime(reader["dataPublicacao"].ToString());
			
			if (reader["revistaArtigoId"] != DBNull.Value) {
				entidade.RevistaArtigoId = Convert.ToInt32(reader["revistaArtigoId"].ToString());
			}

			if (reader["revistaEdicaoId"] != DBNull.Value) {
				entidade.RevistaEdicao = new RevistaEdicao();
				entidade.RevistaEdicao.RevistaEdicaoId = Convert.ToInt32(reader["revistaEdicaoId"].ToString());
			}

			if (reader["revistaSecaoId"] != DBNull.Value) {
				entidade.RevistaSecao = new RevistaSecao();
				entidade.RevistaSecao.RevistaSecaoId = Convert.ToInt32(reader["revistaSecaoId"].ToString());
			}

			if (reader["revistaArtigoPermissaoId"] != DBNull.Value) {
				entidade.RevistaArtigoPermissao = new RevistaArtigoPermissao();
				entidade.RevistaArtigoPermissao.RevistaArtigoPermissaoId = Convert.ToInt32(reader["revistaArtigoPermissaoId"].ToString());
			}

			if (reader["arquivoIdThumbP"] != DBNull.Value) {
				entidade.ArquivoThumbP = new Arquivo();
				entidade.ArquivoThumbP.ArquivoId = Convert.ToInt32(reader["arquivoIdThumbP"].ToString());
			}

			if (reader["arquivoIdThumbM"] != DBNull.Value) {
				entidade.ArquivoThumbM = new Arquivo();
				entidade.ArquivoThumbM.ArquivoId = Convert.ToInt32(reader["arquivoIdThumbM"].ToString());
			}

			if (reader["arquivoIdCapa"] != DBNull.Value) {
				entidade.ArquivoCapa = new Arquivo();
				entidade.ArquivoCapa.ArquivoId = Convert.ToInt32(reader["arquivoIdCapa"].ToString());
			}

			if (reader["arquivoIdLateral"] != DBNull.Value) {
				entidade.ArquivoLateral = new Arquivo();
				entidade.ArquivoLateral.ArquivoId = Convert.ToInt32(reader["arquivoIdLateral"].ToString());
			}

			if (reader["revistaArtigoIdAssociado"] != DBNull.Value) {
				entidade.RevistaArtigoAssociado = new RevistaArtigo();
				entidade.RevistaArtigoAssociado.RevistaArtigoId = Convert.ToInt32(reader["revistaArtigoIdAssociado"].ToString());
			}


		}		
		
	}
}
		