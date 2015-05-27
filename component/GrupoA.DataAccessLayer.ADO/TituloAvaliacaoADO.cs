
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
	public partial class TituloAvaliacaoADO : ADOSuper, ITituloAvaliacaoDAL {
	
	    /// <summary>
        /// Método que persiste um TituloAvaliacao.
        /// </summary>
        /// <param name="entidade">TituloAvaliacao contendo os dados a serem persistidos.</param>	
		public void Inserir(TituloAvaliacao entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO TituloAvaliacao ");
			sbSQL.Append(" (tituloSolicitacaoId, avaliacao, finalizada, dataRealizacaoAvaliacao, relevanciaObra, relevanciaObraObs, conteudoAtualizado, conteudoAtualizadoObs, qualidadeTexto, qualidadeTextoObs, apresentacaoGrafica, apresentacaoGraficaObs, materialComplementar, materialComplementarObs, avaliacaoGeral, avaliacaoGeralObs, pontosFortes, pontosFracos, sugestoes, seraAdotada, seraAdotadaQuais, seraRecomendada, seraRecomendadaQuais, naoAplica, naoAplicaPorque, naoAplicaAdotada, naoAplicaAutor, revisorTecnico, tradutorIngles, tradutorEspanhol, tradutorFrances, tradutorAlemao, nomeAvaliador) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@tituloSolicitacaoId, @avaliacao, @finalizada, @dataRealizacaoAvaliacao, @relevanciaObra, @relevanciaObraObs, @conteudoAtualizado, @conteudoAtualizadoObs, @qualidadeTexto, @qualidadeTextoObs, @apresentacaoGrafica, @apresentacaoGraficaObs, @materialComplementar, @materialComplementarObs, @avaliacaoGeral, @avaliacaoGeralObs, @pontosFortes, @pontosFracos, @sugestoes, @seraAdotada, @seraAdotadaQuais, @seraRecomendada, @seraRecomendadaQuais, @naoAplica, @naoAplicaPorque, @naoAplicaAdotada, @naoAplicaAutor, @revisorTecnico, @tradutorIngles, @tradutorEspanhol, @tradutorFrances, @tradutorAlemao, @nomeAvaliador) ");											

			sbSQL.Append(" ; SET @tituloAvaliacaoId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@tituloAvaliacaoId", DbType.Int32, 8);

			_db.AddInParameter(command, "@tituloSolicitacaoId", DbType.Int32, entidade.TituloSolicitacao.TituloSolicitacaoId);

			if (entidade.Avaliacao != null ) 
				_db.AddInParameter(command, "@avaliacao", DbType.String, entidade.Avaliacao);
			else
				_db.AddInParameter(command, "@avaliacao", DbType.String, null);

			_db.AddInParameter(command, "@finalizada", DbType.Int32, entidade.Finalizada);

			if (entidade.DataRealizacaoAvaliacao != null && entidade.DataRealizacaoAvaliacao != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataRealizacaoAvaliacao", DbType.DateTime, entidade.DataRealizacaoAvaliacao);
			else
				_db.AddInParameter(command, "@dataRealizacaoAvaliacao", DbType.DateTime, null);

			_db.AddInParameter(command, "@relevanciaObra", DbType.Int32, entidade.RelevanciaObra);

			if (entidade.RelevanciaObraObs != null ) 
				_db.AddInParameter(command, "@relevanciaObraObs", DbType.String, entidade.RelevanciaObraObs);
			else
				_db.AddInParameter(command, "@relevanciaObraObs", DbType.String, null);

			_db.AddInParameter(command, "@conteudoAtualizado", DbType.Int32, entidade.ConteudoAtualizado);

			if (entidade.ConteudoAtualizadoObs != null ) 
				_db.AddInParameter(command, "@conteudoAtualizadoObs", DbType.String, entidade.ConteudoAtualizadoObs);
			else
				_db.AddInParameter(command, "@conteudoAtualizadoObs", DbType.String, null);

			_db.AddInParameter(command, "@qualidadeTexto", DbType.Int32, entidade.QualidadeTexto);

			if (entidade.QualidadeTextoObs != null ) 
				_db.AddInParameter(command, "@qualidadeTextoObs", DbType.String, entidade.QualidadeTextoObs);
			else
				_db.AddInParameter(command, "@qualidadeTextoObs", DbType.String, null);

			_db.AddInParameter(command, "@apresentacaoGrafica", DbType.Int32, entidade.ApresentacaoGrafica);

			if (entidade.ApresentacaoGraficaObs != null ) 
				_db.AddInParameter(command, "@apresentacaoGraficaObs", DbType.String, entidade.ApresentacaoGraficaObs);
			else
				_db.AddInParameter(command, "@apresentacaoGraficaObs", DbType.String, null);

			_db.AddInParameter(command, "@materialComplementar", DbType.Int32, entidade.MaterialComplementar);

			if (entidade.MaterialComplementarObs != null ) 
				_db.AddInParameter(command, "@materialComplementarObs", DbType.String, entidade.MaterialComplementarObs);
			else
				_db.AddInParameter(command, "@materialComplementarObs", DbType.String, null);

			_db.AddInParameter(command, "@avaliacaoGeral", DbType.Int32, entidade.AvaliacaoGeral);

			if (entidade.AvaliacaoGeralObs != null ) 
				_db.AddInParameter(command, "@avaliacaoGeralObs", DbType.String, entidade.AvaliacaoGeralObs);
			else
				_db.AddInParameter(command, "@avaliacaoGeralObs", DbType.String, null);

			if (entidade.PontosFortes != null ) 
				_db.AddInParameter(command, "@pontosFortes", DbType.String, entidade.PontosFortes);
			else
				_db.AddInParameter(command, "@pontosFortes", DbType.String, null);

			if (entidade.PontosFracos != null ) 
				_db.AddInParameter(command, "@pontosFracos", DbType.String, entidade.PontosFracos);
			else
				_db.AddInParameter(command, "@pontosFracos", DbType.String, null);

			if (entidade.Sugestoes != null ) 
				_db.AddInParameter(command, "@sugestoes", DbType.String, entidade.Sugestoes);
			else
				_db.AddInParameter(command, "@sugestoes", DbType.String, null);

			_db.AddInParameter(command, "@seraAdotada", DbType.Int32, entidade.SeraAdotada);

			if (entidade.SeraAdotadaQuais != null ) 
				_db.AddInParameter(command, "@seraAdotadaQuais", DbType.String, entidade.SeraAdotadaQuais);
			else
				_db.AddInParameter(command, "@seraAdotadaQuais", DbType.String, null);

			_db.AddInParameter(command, "@seraRecomendada", DbType.Int32, entidade.SeraRecomendada);

			if (entidade.SeraRecomendadaQuais != null ) 
				_db.AddInParameter(command, "@seraRecomendadaQuais", DbType.String, entidade.SeraRecomendadaQuais);
			else
				_db.AddInParameter(command, "@seraRecomendadaQuais", DbType.String, null);

			_db.AddInParameter(command, "@naoAplica", DbType.Int32, entidade.NaoAplica);

			if (entidade.NaoAplicaPorque != null ) 
				_db.AddInParameter(command, "@naoAplicaPorque", DbType.String, entidade.NaoAplicaPorque);
			else
				_db.AddInParameter(command, "@naoAplicaPorque", DbType.String, null);

			if (entidade.NaoAplicaAdotada != null ) 
				_db.AddInParameter(command, "@naoAplicaAdotada", DbType.String, entidade.NaoAplicaAdotada);
			else
				_db.AddInParameter(command, "@naoAplicaAdotada", DbType.String, null);

			if (entidade.NaoAplicaAutor != null ) 
				_db.AddInParameter(command, "@naoAplicaAutor", DbType.String, entidade.NaoAplicaAutor);
			else
				_db.AddInParameter(command, "@naoAplicaAutor", DbType.String, null);

			_db.AddInParameter(command, "@revisorTecnico", DbType.Int32, entidade.RevisorTecnico);

			_db.AddInParameter(command, "@tradutorIngles", DbType.Int32, entidade.TradutorIngles);

			_db.AddInParameter(command, "@tradutorEspanhol", DbType.Int32, entidade.TradutorEspanhol);

			_db.AddInParameter(command, "@tradutorFrances", DbType.Int32, entidade.TradutorFrances);

			_db.AddInParameter(command, "@tradutorAlemao", DbType.Int32, entidade.TradutorAlemao);

			if (entidade.NomeAvaliador != null ) 
				_db.AddInParameter(command, "@nomeAvaliador", DbType.String, entidade.NomeAvaliador);
			else
				_db.AddInParameter(command, "@nomeAvaliador", DbType.String, null);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.TituloAvaliacaoId = Convert.ToInt32(_db.GetParameterValue(command, "@tituloAvaliacaoId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um TituloAvaliacao.
        /// </summary>
        /// <param name="entidade">TituloAvaliacao contendo os dados a serem atualizados.</param>
		public void Atualizar(TituloAvaliacao entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE TituloAvaliacao SET ");
			sbSQL.Append(" tituloSolicitacaoId=@tituloSolicitacaoId, avaliacao=@avaliacao, finalizada=@finalizada, dataRealizacaoAvaliacao=@dataRealizacaoAvaliacao, relevanciaObra=@relevanciaObra, relevanciaObraObs=@relevanciaObraObs, conteudoAtualizado=@conteudoAtualizado, conteudoAtualizadoObs=@conteudoAtualizadoObs, qualidadeTexto=@qualidadeTexto, qualidadeTextoObs=@qualidadeTextoObs, apresentacaoGrafica=@apresentacaoGrafica, apresentacaoGraficaObs=@apresentacaoGraficaObs, materialComplementar=@materialComplementar, materialComplementarObs=@materialComplementarObs, avaliacaoGeral=@avaliacaoGeral, avaliacaoGeralObs=@avaliacaoGeralObs, pontosFortes=@pontosFortes, pontosFracos=@pontosFracos, sugestoes=@sugestoes, seraAdotada=@seraAdotada, seraAdotadaQuais=@seraAdotadaQuais, seraRecomendada=@seraRecomendada, seraRecomendadaQuais=@seraRecomendadaQuais, naoAplica=@naoAplica, naoAplicaPorque=@naoAplicaPorque, naoAplicaAdotada=@naoAplicaAdotada, naoAplicaAutor=@naoAplicaAutor, revisorTecnico=@revisorTecnico, tradutorIngles=@tradutorIngles, tradutorEspanhol=@tradutorEspanhol, tradutorFrances=@tradutorFrances, tradutorAlemao=@tradutorAlemao, nomeAvaliador=@nomeAvaliador ");
			sbSQL.Append(" WHERE tituloAvaliacaoId=@tituloAvaliacaoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@tituloAvaliacaoId", DbType.Int32, entidade.TituloAvaliacaoId);
			_db.AddInParameter(command, "@tituloSolicitacaoId", DbType.Int32, entidade.TituloSolicitacao.TituloSolicitacaoId);
			if (entidade.Avaliacao != null ) 
				_db.AddInParameter(command, "@avaliacao", DbType.String, entidade.Avaliacao);
			else
				_db.AddInParameter(command, "@avaliacao", DbType.String, null);
			_db.AddInParameter(command, "@finalizada", DbType.Int32, entidade.Finalizada);
			if (entidade.DataRealizacaoAvaliacao != null && entidade.DataRealizacaoAvaliacao != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataRealizacaoAvaliacao", DbType.DateTime, entidade.DataRealizacaoAvaliacao);
			else
				_db.AddInParameter(command, "@dataRealizacaoAvaliacao", DbType.DateTime, null);
			_db.AddInParameter(command, "@relevanciaObra", DbType.Int32, entidade.RelevanciaObra);
			if (entidade.RelevanciaObraObs != null ) 
				_db.AddInParameter(command, "@relevanciaObraObs", DbType.String, entidade.RelevanciaObraObs);
			else
				_db.AddInParameter(command, "@relevanciaObraObs", DbType.String, null);
			_db.AddInParameter(command, "@conteudoAtualizado", DbType.Int32, entidade.ConteudoAtualizado);
			if (entidade.ConteudoAtualizadoObs != null ) 
				_db.AddInParameter(command, "@conteudoAtualizadoObs", DbType.String, entidade.ConteudoAtualizadoObs);
			else
				_db.AddInParameter(command, "@conteudoAtualizadoObs", DbType.String, null);
			_db.AddInParameter(command, "@qualidadeTexto", DbType.Int32, entidade.QualidadeTexto);
			if (entidade.QualidadeTextoObs != null ) 
				_db.AddInParameter(command, "@qualidadeTextoObs", DbType.String, entidade.QualidadeTextoObs);
			else
				_db.AddInParameter(command, "@qualidadeTextoObs", DbType.String, null);
			_db.AddInParameter(command, "@apresentacaoGrafica", DbType.Int32, entidade.ApresentacaoGrafica);
			if (entidade.ApresentacaoGraficaObs != null ) 
				_db.AddInParameter(command, "@apresentacaoGraficaObs", DbType.String, entidade.ApresentacaoGraficaObs);
			else
				_db.AddInParameter(command, "@apresentacaoGraficaObs", DbType.String, null);
			_db.AddInParameter(command, "@materialComplementar", DbType.Int32, entidade.MaterialComplementar);
			if (entidade.MaterialComplementarObs != null ) 
				_db.AddInParameter(command, "@materialComplementarObs", DbType.String, entidade.MaterialComplementarObs);
			else
				_db.AddInParameter(command, "@materialComplementarObs", DbType.String, null);
			_db.AddInParameter(command, "@avaliacaoGeral", DbType.Int32, entidade.AvaliacaoGeral);
			if (entidade.AvaliacaoGeralObs != null ) 
				_db.AddInParameter(command, "@avaliacaoGeralObs", DbType.String, entidade.AvaliacaoGeralObs);
			else
				_db.AddInParameter(command, "@avaliacaoGeralObs", DbType.String, null);
			if (entidade.PontosFortes != null ) 
				_db.AddInParameter(command, "@pontosFortes", DbType.String, entidade.PontosFortes);
			else
				_db.AddInParameter(command, "@pontosFortes", DbType.String, null);
			if (entidade.PontosFracos != null ) 
				_db.AddInParameter(command, "@pontosFracos", DbType.String, entidade.PontosFracos);
			else
				_db.AddInParameter(command, "@pontosFracos", DbType.String, null);
			if (entidade.Sugestoes != null ) 
				_db.AddInParameter(command, "@sugestoes", DbType.String, entidade.Sugestoes);
			else
				_db.AddInParameter(command, "@sugestoes", DbType.String, null);
			_db.AddInParameter(command, "@seraAdotada", DbType.Int32, entidade.SeraAdotada);
			if (entidade.SeraAdotadaQuais != null ) 
				_db.AddInParameter(command, "@seraAdotadaQuais", DbType.String, entidade.SeraAdotadaQuais);
			else
				_db.AddInParameter(command, "@seraAdotadaQuais", DbType.String, null);
			_db.AddInParameter(command, "@seraRecomendada", DbType.Int32, entidade.SeraRecomendada);
			if (entidade.SeraRecomendadaQuais != null ) 
				_db.AddInParameter(command, "@seraRecomendadaQuais", DbType.String, entidade.SeraRecomendadaQuais);
			else
				_db.AddInParameter(command, "@seraRecomendadaQuais", DbType.String, null);
			_db.AddInParameter(command, "@naoAplica", DbType.Int32, entidade.NaoAplica);
			if (entidade.NaoAplicaPorque != null ) 
				_db.AddInParameter(command, "@naoAplicaPorque", DbType.String, entidade.NaoAplicaPorque);
			else
				_db.AddInParameter(command, "@naoAplicaPorque", DbType.String, null);
			if (entidade.NaoAplicaAdotada != null ) 
				_db.AddInParameter(command, "@naoAplicaAdotada", DbType.String, entidade.NaoAplicaAdotada);
			else
				_db.AddInParameter(command, "@naoAplicaAdotada", DbType.String, null);
			if (entidade.NaoAplicaAutor != null ) 
				_db.AddInParameter(command, "@naoAplicaAutor", DbType.String, entidade.NaoAplicaAutor);
			else
				_db.AddInParameter(command, "@naoAplicaAutor", DbType.String, null);
			_db.AddInParameter(command, "@revisorTecnico", DbType.Int32, entidade.RevisorTecnico);
			_db.AddInParameter(command, "@tradutorIngles", DbType.Int32, entidade.TradutorIngles);
			_db.AddInParameter(command, "@tradutorEspanhol", DbType.Int32, entidade.TradutorEspanhol);
			_db.AddInParameter(command, "@tradutorFrances", DbType.Int32, entidade.TradutorFrances);
			_db.AddInParameter(command, "@tradutorAlemao", DbType.Int32, entidade.TradutorAlemao);
			if (entidade.NomeAvaliador != null ) 
				_db.AddInParameter(command, "@nomeAvaliador", DbType.String, entidade.NomeAvaliador);
			else
				_db.AddInParameter(command, "@nomeAvaliador", DbType.String, null);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um TituloAvaliacao da base de dados.
        /// </summary>
        /// <param name="entidade">TituloAvaliacao a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(TituloAvaliacao entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM TituloAvaliacao ");
			sbSQL.Append("WHERE tituloAvaliacaoId=@tituloAvaliacaoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@tituloAvaliacaoId", DbType.Int32, entidade.TituloAvaliacaoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um TituloAvaliacao.
		/// </summary>
        /// <param name="entidade">TituloAvaliacao a ser carregado (somente o identificador é necessário).</param>
		/// <returns>TituloAvaliacao</returns>
		public TituloAvaliacao Carregar(int tituloAvaliacaoId) {		
			TituloAvaliacao entidade = new TituloAvaliacao();
			entidade.TituloAvaliacaoId = tituloAvaliacaoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um TituloAvaliacao.
		/// </summary>
        /// <param name="entidade">TituloAvaliacao a ser carregado (somente o identificador é necessário).</param>
		/// <returns>TituloAvaliacao</returns>
		public TituloAvaliacao Carregar(TituloAvaliacao entidade) {		
		
			TituloAvaliacao entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM TituloAvaliacao WHERE tituloAvaliacaoId=@tituloAvaliacaoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@tituloAvaliacaoId", DbType.Int32, entidade.TituloAvaliacaoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new TituloAvaliacao();
				PopulaTituloAvaliacao(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de TituloAvaliacao.
        /// </summary>
        /// <param name="entidade">TituloSolicitacao relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de TituloAvaliacao.</returns>
		public IEnumerable<TituloAvaliacao> Carregar(TituloSolicitacao entidade)
		{		
			List<TituloAvaliacao> entidadesRetorno = new List<TituloAvaliacao>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT TituloAvaliacao.* FROM TituloAvaliacao WHERE TituloAvaliacao.tituloSolicitacaoId=@tituloSolicitacaoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@tituloSolicitacaoId", DbType.Int32, entidade.TituloSolicitacaoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TituloAvaliacao entidadeRetorno = new TituloAvaliacao();
                PopulaTituloAvaliacao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de TituloAvaliacao.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos TituloAvaliacao.</returns>
		public IEnumerable<TituloAvaliacao> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<TituloAvaliacao> entidadesRetorno = new List<TituloAvaliacao>();
			
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
				sbOrder.Append( " ORDER BY tituloAvaliacaoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM TituloAvaliacao");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloAvaliacao WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloAvaliacao ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT TituloAvaliacao.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM TituloAvaliacao ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT TituloAvaliacao.* FROM TituloAvaliacao ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                TituloAvaliacao entidadeRetorno = new TituloAvaliacao();
                PopulaTituloAvaliacao(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os TituloAvaliacao existentes na base de dados.
        /// </summary>
		public IEnumerable<TituloAvaliacao> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de TituloAvaliacao na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de TituloAvaliacao na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM TituloAvaliacao");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um TituloAvaliacao baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">TituloAvaliacao a ser populado(.</param>
		public static void PopulaTituloAvaliacao(IDataReader reader, TituloAvaliacao entidade) 
		{						
			if (reader["tituloAvaliacaoId"] != DBNull.Value)
				entidade.TituloAvaliacaoId = Convert.ToInt32(reader["tituloAvaliacaoId"].ToString());
			
			if (reader["avaliacao"] != DBNull.Value)
				entidade.Avaliacao = reader["avaliacao"].ToString();
			
			if (reader["finalizada"] != DBNull.Value)
				entidade.Finalizada = Convert.ToBoolean(reader["finalizada"].ToString());
			
			if (reader["dataRealizacaoAvaliacao"] != DBNull.Value)
				entidade.DataRealizacaoAvaliacao = Convert.ToDateTime(reader["dataRealizacaoAvaliacao"].ToString());
			
			if (reader["relevanciaObra"] != DBNull.Value)
				entidade.RelevanciaObra = Convert.ToInt32(reader["relevanciaObra"].ToString());
			
			if (reader["relevanciaObraObs"] != DBNull.Value)
				entidade.RelevanciaObraObs = reader["relevanciaObraObs"].ToString();
			
			if (reader["conteudoAtualizado"] != DBNull.Value)
				entidade.ConteudoAtualizado = Convert.ToInt32(reader["conteudoAtualizado"].ToString());
			
			if (reader["conteudoAtualizadoObs"] != DBNull.Value)
				entidade.ConteudoAtualizadoObs = reader["conteudoAtualizadoObs"].ToString();
			
			if (reader["qualidadeTexto"] != DBNull.Value)
				entidade.QualidadeTexto = Convert.ToInt32(reader["qualidadeTexto"].ToString());
			
			if (reader["qualidadeTextoObs"] != DBNull.Value)
				entidade.QualidadeTextoObs = reader["qualidadeTextoObs"].ToString();
			
			if (reader["apresentacaoGrafica"] != DBNull.Value)
				entidade.ApresentacaoGrafica = Convert.ToInt32(reader["apresentacaoGrafica"].ToString());
			
			if (reader["apresentacaoGraficaObs"] != DBNull.Value)
				entidade.ApresentacaoGraficaObs = reader["apresentacaoGraficaObs"].ToString();
			
			if (reader["materialComplementar"] != DBNull.Value)
				entidade.MaterialComplementar = Convert.ToInt32(reader["materialComplementar"].ToString());
			
			if (reader["materialComplementarObs"] != DBNull.Value)
				entidade.MaterialComplementarObs = reader["materialComplementarObs"].ToString();
			
			if (reader["avaliacaoGeral"] != DBNull.Value)
				entidade.AvaliacaoGeral = Convert.ToInt32(reader["avaliacaoGeral"].ToString());
			
			if (reader["avaliacaoGeralObs"] != DBNull.Value)
				entidade.AvaliacaoGeralObs = reader["avaliacaoGeralObs"].ToString();
			
			if (reader["pontosFortes"] != DBNull.Value)
				entidade.PontosFortes = reader["pontosFortes"].ToString();
			
			if (reader["pontosFracos"] != DBNull.Value)
				entidade.PontosFracos = reader["pontosFracos"].ToString();
			
			if (reader["sugestoes"] != DBNull.Value)
				entidade.Sugestoes = reader["sugestoes"].ToString();
			
			if (reader["seraAdotada"] != DBNull.Value)
				entidade.SeraAdotada = Convert.ToBoolean(reader["seraAdotada"].ToString());
			
			if (reader["seraAdotadaQuais"] != DBNull.Value)
				entidade.SeraAdotadaQuais = reader["seraAdotadaQuais"].ToString();
			
			if (reader["seraRecomendada"] != DBNull.Value)
				entidade.SeraRecomendada = Convert.ToBoolean(reader["seraRecomendada"].ToString());
			
			if (reader["seraRecomendadaQuais"] != DBNull.Value)
				entidade.SeraRecomendadaQuais = reader["seraRecomendadaQuais"].ToString();
			
			if (reader["naoAplica"] != DBNull.Value)
				entidade.NaoAplica = Convert.ToBoolean(reader["naoAplica"].ToString());
			
			if (reader["naoAplicaPorque"] != DBNull.Value)
				entidade.NaoAplicaPorque = reader["naoAplicaPorque"].ToString();
			
			if (reader["naoAplicaAdotada"] != DBNull.Value)
				entidade.NaoAplicaAdotada = reader["naoAplicaAdotada"].ToString();
			
			if (reader["naoAplicaAutor"] != DBNull.Value)
				entidade.NaoAplicaAutor = reader["naoAplicaAutor"].ToString();
			
			if (reader["revisorTecnico"] != DBNull.Value)
				entidade.RevisorTecnico = Convert.ToBoolean(reader["revisorTecnico"].ToString());
			
			if (reader["tradutorIngles"] != DBNull.Value)
				entidade.TradutorIngles = Convert.ToBoolean(reader["tradutorIngles"].ToString());
			
			if (reader["tradutorEspanhol"] != DBNull.Value)
				entidade.TradutorEspanhol = Convert.ToBoolean(reader["tradutorEspanhol"].ToString());
			
			if (reader["tradutorFrances"] != DBNull.Value)
				entidade.TradutorFrances = Convert.ToBoolean(reader["tradutorFrances"].ToString());
			
			if (reader["tradutorAlemao"] != DBNull.Value)
				entidade.TradutorAlemao = Convert.ToBoolean(reader["tradutorAlemao"].ToString());
			
			if (reader["nomeAvaliador"] != DBNull.Value)
				entidade.NomeAvaliador = reader["nomeAvaliador"].ToString();
			
			if (reader["tituloSolicitacaoId"] != DBNull.Value) {
				entidade.TituloSolicitacao = new TituloSolicitacao();
				entidade.TituloSolicitacao.TituloSolicitacaoId = Convert.ToInt32(reader["tituloSolicitacaoId"].ToString());
			}


		}		
		
	}
}
		