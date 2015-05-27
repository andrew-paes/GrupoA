
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
	public partial class ArquivoADO : ADOSuper, IArquivoDAL {
	
	    /// <summary>
        /// Método que persiste um Arquivo.
        /// </summary>
        /// <param name="entidade">Arquivo contendo os dados a serem persistidos.</param>	
		public void Inserir(Arquivo entidade) 
		{
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de insert.
			sbSQL.Append(" INSERT INTO Arquivo ");
			sbSQL.Append(" (tamanhoArquivo, dataHoraUpload, nomeArquivo, nomeArquivoOriginal) ");
			sbSQL.Append(" VALUES ");
			sbSQL.Append(" (@tamanhoArquivo, @dataHoraUpload, @nomeArquivo, @nomeArquivoOriginal) ");											

			sbSQL.Append(" ; SET @arquivoId = SCOPE_IDENTITY(); ");

			command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddOutParameter(command, "@arquivoId", DbType.Int32, 8);

			_db.AddInParameter(command, "@tamanhoArquivo", DbType.Int32, entidade.TamanhoArquivo);

			if (entidade.DataHoraUpload != null && entidade.DataHoraUpload != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataHoraUpload", DbType.DateTime, entidade.DataHoraUpload);
			else
				_db.AddInParameter(command, "@dataHoraUpload", DbType.DateTime, null);

			_db.AddInParameter(command, "@nomeArquivo", DbType.String, entidade.NomeArquivo);

			if (entidade.NomeArquivoOriginal != null ) 
				_db.AddInParameter(command, "@nomeArquivoOriginal", DbType.String, entidade.NomeArquivoOriginal);
			else
				_db.AddInParameter(command, "@nomeArquivoOriginal", DbType.String, null);

						
			// Executa a query.
			_db.ExecuteNonQuery(command);			

			entidade.ArquivoId = Convert.ToInt32(_db.GetParameterValue(command, "@arquivoId"));
			
		}
		
        /// <summary>
        /// Método que atualiza os dados de um Arquivo.
        /// </summary>
        /// <param name="entidade">Arquivo contendo os dados a serem atualizados.</param>
		public void Atualizar(Arquivo entidade) {
		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;
			
			// Monta a string de atualização.
			sbSQL.Append(" UPDATE Arquivo SET ");
			sbSQL.Append(" tamanhoArquivo=@tamanhoArquivo, dataHoraUpload=@dataHoraUpload, nomeArquivo=@nomeArquivo, nomeArquivoOriginal=@nomeArquivoOriginal ");
			sbSQL.Append(" WHERE arquivoId=@arquivoId ");
										       
			command = _db.GetSqlStringCommand(sbSQL.ToString());			
			
			// Parâmetros
			_db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.ArquivoId);
			_db.AddInParameter(command, "@tamanhoArquivo", DbType.Int32, entidade.TamanhoArquivo);
			if (entidade.DataHoraUpload != null && entidade.DataHoraUpload != DateTime.MinValue ) 
				_db.AddInParameter(command, "@dataHoraUpload", DbType.DateTime, entidade.DataHoraUpload);
			else
				_db.AddInParameter(command, "@dataHoraUpload", DbType.DateTime, null);
			_db.AddInParameter(command, "@nomeArquivo", DbType.String, entidade.NomeArquivo);
			if (entidade.NomeArquivoOriginal != null ) 
				_db.AddInParameter(command, "@nomeArquivoOriginal", DbType.String, entidade.NomeArquivoOriginal);
			else
				_db.AddInParameter(command, "@nomeArquivoOriginal", DbType.String, null);
			
			// Executa a query.
			_db.ExecuteNonQuery(command);			
			
		}
		
        /// <summary>
        /// Método que remove um Arquivo da base de dados.
        /// </summary>
        /// <param name="entidade">Arquivo a ser excluído (somente o identificador é necessário).</param>		
		public void Excluir(Arquivo entidade) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM Arquivo ");
			sbSQL.Append("WHERE arquivoId=@arquivoId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());				
			
			_db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.ArquivoId);

								
			_db.ExecuteNonQuery(command);
		}
			
		/// <summary>
		/// Método que carrega um Arquivo.
		/// </summary>
        /// <param name="entidade">Arquivo a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Arquivo</returns>
		public Arquivo Carregar(int arquivoId) {		
			Arquivo entidade = new Arquivo();
			entidade.ArquivoId = arquivoId;
			return Carregar(entidade);
		
		}
		

		/// <summary>
		/// Método que carrega um Arquivo.
		/// </summary>
        /// <param name="entidade">Arquivo a ser carregado (somente o identificador é necessário).</param>
		/// <returns>Arquivo</returns>
		public Arquivo Carregar(Arquivo entidade) {		
		
			Arquivo entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM Arquivo WHERE arquivoId=@arquivoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.ArquivoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new Arquivo();
				PopulaArquivo(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
		


		/// <summary>
        /// Método que retorna uma coleção de Arquivo.
        /// </summary>
        /// <param name="entidade">Autor relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Arquivo.</returns>
		public IEnumerable<Arquivo> Carregar(Autor entidade)
		{		
			List<Arquivo> entidadesRetorno = new List<Arquivo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Arquivo.* FROM Arquivo INNER JOIN Autor ON Arquivo.arquivoId=Autor.arquivoId WHERE Autor.autorId=@autorId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@autorId", DbType.Int32, entidade.AutorId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Arquivo entidadeRetorno = new Arquivo();
                PopulaArquivo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Arquivo.
        /// </summary>
        /// <param name="entidade">Banner relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Arquivo.</returns>
		public IEnumerable<Arquivo> Carregar(Banner entidade)
		{		
			List<Arquivo> entidadesRetorno = new List<Arquivo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Arquivo.* FROM Arquivo INNER JOIN Banner ON Arquivo.arquivoId=Banner.arquivoId WHERE Banner.bannerId=@bannerId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@bannerId", DbType.Int32, entidade.BannerId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Arquivo entidadeRetorno = new Arquivo();
                PopulaArquivo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Arquivo.
        /// </summary>
        /// <param name="entidade">ClippingImagem relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Arquivo.</returns>
		public IEnumerable<Arquivo> Carregar(ClippingImagem entidade)
		{		
			List<Arquivo> entidadesRetorno = new List<Arquivo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Arquivo.* FROM Arquivo INNER JOIN ClippingImagem ON Arquivo.arquivoId=ClippingImagem.arquivoId WHERE ClippingImagem.clippingImagemId=@clippingImagemId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@clippingImagemId", DbType.Int32, entidade.ClippingImagemId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Arquivo entidadeRetorno = new Arquivo();
                PopulaArquivo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Arquivo.
        /// </summary>
        /// <param name="entidade">CursoPanamericano relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Arquivo.</returns>
		public IEnumerable<Arquivo> Carregar(CursoPanamericano entidade)
		{		
			List<Arquivo> entidadesRetorno = new List<Arquivo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Arquivo.* FROM Arquivo INNER JOIN CursoPanamericano ON Arquivo.arquivoId=CursoPanamericano.arquivoId WHERE CursoPanamericano.cursoPanamericanoId=@cursoPanamericanoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@cursoPanamericanoId", DbType.Int32, entidade.CursoPanamericanoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Arquivo entidadeRetorno = new Arquivo();
                PopulaArquivo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Arquivo.
        /// </summary>
        /// <param name="entidade">Evento relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Arquivo.</returns>
		public IEnumerable<Arquivo> Carregar(Evento entidade)
		{		
			List<Arquivo> entidadesRetorno = new List<Arquivo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Arquivo.* FROM Arquivo INNER JOIN Evento ON Arquivo.arquivoId=Evento.arquivoId WHERE Evento.eventoId=@eventoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@eventoId", DbType.Int32, entidade.EventoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Arquivo entidadeRetorno = new Arquivo();
                PopulaArquivo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Arquivo.
        /// </summary>
        /// <param name="entidade">EventoImagem relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Arquivo.</returns>
		public IEnumerable<Arquivo> Carregar(EventoImagem entidade)
		{		
			List<Arquivo> entidadesRetorno = new List<Arquivo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Arquivo.* FROM Arquivo INNER JOIN EventoImagem ON Arquivo.arquivoId=EventoImagem.arquivoId WHERE EventoImagem.eventoImagemId=@eventoImagemId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@eventoImagemId", DbType.Int32, entidade.EventoImagemId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Arquivo entidadeRetorno = new Arquivo();
                PopulaArquivo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Arquivo.
        /// </summary>
        /// <param name="entidade">Midia relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Arquivo.</returns>
		public IEnumerable<Arquivo> Carregar(Midia entidade)
		{		
			List<Arquivo> entidadesRetorno = new List<Arquivo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Arquivo.* FROM Arquivo INNER JOIN Midia ON Arquivo.arquivoId=Midia.arquivoId WHERE Midia.midiaId=@midiaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@midiaId", DbType.Int32, entidade.MidiaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Arquivo entidadeRetorno = new Arquivo();
                PopulaArquivo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Arquivo.
        /// </summary>
        /// <param name="entidade">Noticia relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Arquivo.</returns>
		public IEnumerable<Arquivo> Carregar(Noticia entidade)
		{		
			List<Arquivo> entidadesRetorno = new List<Arquivo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Arquivo.* FROM Arquivo INNER JOIN Noticia ON Arquivo.arquivoId=Noticia.arquivoId WHERE Noticia.noticiaId=@noticiaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@noticiaId", DbType.Int32, entidade.NoticiaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Arquivo entidadeRetorno = new Arquivo();
                PopulaArquivo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Arquivo.
        /// </summary>
        /// <param name="entidade">NoticiaImagem relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Arquivo.</returns>
		public IEnumerable<Arquivo> Carregar(NoticiaImagem entidade)
		{		
			List<Arquivo> entidadesRetorno = new List<Arquivo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Arquivo.* FROM Arquivo INNER JOIN NoticiaImagem ON Arquivo.arquivoId=NoticiaImagem.arquivoId WHERE NoticiaImagem.noticiaImagemId=@noticiaImagemId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@noticiaImagemId", DbType.Int32, entidade.NoticiaImagemId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Arquivo entidadeRetorno = new Arquivo();
                PopulaArquivo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Arquivo.
        /// </summary>
        /// <param name="entidade">PaginaPromocional relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Arquivo.</returns>
		public IEnumerable<Arquivo> Carregar(PaginaPromocional entidade)
		{		
			List<Arquivo> entidadesRetorno = new List<Arquivo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Arquivo.* FROM Arquivo INNER JOIN PaginaPromocional ON Arquivo.arquivoId=PaginaPromocional.arquivoId WHERE PaginaPromocional.paginaPromocionalId=@paginaPromocionalId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@paginaPromocionalId", DbType.Int32, entidade.PaginaPromocionalId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Arquivo entidadeRetorno = new Arquivo();
                PopulaArquivo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Arquivo.
        /// </summary>
        /// <param name="entidade">ProdutoImagem relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Arquivo.</returns>
		public IEnumerable<Arquivo> Carregar(ProdutoImagem entidade)
		{		
			List<Arquivo> entidadesRetorno = new List<Arquivo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Arquivo.* FROM Arquivo INNER JOIN ProdutoImagem ON Arquivo.arquivoId=ProdutoImagem.arquivoId WHERE ProdutoImagem.produtoImagemId=@produtoImagemId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@produtoImagemId", DbType.Int32, entidade.ProdutoImagemId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Arquivo entidadeRetorno = new Arquivo();
                PopulaArquivo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Arquivo.
        /// </summary>
        /// <param name="entidade">ProfessorComprovanteDocencia relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Arquivo.</returns>
		public IEnumerable<Arquivo> Carregar(ProfessorComprovanteDocencia entidade)
		{		
			List<Arquivo> entidadesRetorno = new List<Arquivo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Arquivo.* FROM Arquivo INNER JOIN ProfessorComprovanteDocencia ON Arquivo.arquivoId=ProfessorComprovanteDocencia.arquivoId WHERE ProfessorComprovanteDocencia.professorComprovanteDocenciaId=@professorComprovanteDocenciaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@professorComprovanteDocenciaId", DbType.Int32, entidade.ProfessorComprovanteDocenciaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Arquivo entidadeRetorno = new Arquivo();
                PopulaArquivo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Arquivo.
        /// </summary>
        /// <param name="entidade">ProgramaAtualizacaoChamada relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Arquivo.</returns>
		public IEnumerable<Arquivo> Carregar(ProgramaAtualizacaoChamada entidade)
		{		
			List<Arquivo> entidadesRetorno = new List<Arquivo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Arquivo.* FROM Arquivo INNER JOIN ProgramaAtualizacaoChamada ON Arquivo.arquivoId=ProgramaAtualizacaoChamada.arquivoId WHERE ProgramaAtualizacaoChamada.programaAtualizacaoChamadaId=@programaAtualizacaoChamadaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@programaAtualizacaoChamadaId", DbType.Int32, entidade.ProgramaAtualizacaoChamadaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Arquivo entidadeRetorno = new Arquivo();
                PopulaArquivo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Arquivo.
        /// </summary>
        /// <param name="entidade">RevistaArtigo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Arquivo.</returns>
		public IEnumerable<Arquivo> Carregar(RevistaArtigo entidade)
		{		
			List<Arquivo> entidadesRetorno = new List<Arquivo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Arquivo.* FROM Arquivo INNER JOIN RevistaArtigo ON Arquivo.arquivoId=RevistaArtigo.arquivoId WHERE RevistaArtigo.revistaArtigoId=@revistaArtigoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@revistaArtigoId", DbType.Int32, entidade.RevistaArtigoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Arquivo entidadeRetorno = new Arquivo();
                PopulaArquivo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Arquivo.
        /// </summary>
        /// <param name="entidade">RevistaGrupoAEdicao relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Arquivo.</returns>
		public IEnumerable<Arquivo> Carregar(RevistaGrupoAEdicao entidade)
		{		
			List<Arquivo> entidadesRetorno = new List<Arquivo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Arquivo.* FROM Arquivo INNER JOIN RevistaGrupoAEdicao ON Arquivo.arquivoId=RevistaGrupoAEdicao.arquivoId WHERE RevistaGrupoAEdicao.revistaGrupoAEdicaoId=@revistaGrupoAEdicaoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@revistaGrupoAEdicaoId", DbType.Int32, entidade.RevistaGrupoAEdicaoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Arquivo entidadeRetorno = new Arquivo();
                PopulaArquivo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Arquivo.
        /// </summary>
        /// <param name="entidade">TituloConteudoExtraArquivo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Arquivo.</returns>
		public IEnumerable<Arquivo> Carregar(TituloConteudoExtraArquivo entidade)
		{		
			List<Arquivo> entidadesRetorno = new List<Arquivo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Arquivo.* FROM Arquivo INNER JOIN TituloConteudoExtraArquivo ON Arquivo.arquivoId=TituloConteudoExtraArquivo.arquivoId WHERE TituloConteudoExtraArquivo.tituloConteudoExtraArquivoId=@tituloConteudoExtraArquivoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@tituloConteudoExtraArquivoId", DbType.Int32, entidade.TituloConteudoExtraArquivoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Arquivo entidadeRetorno = new Arquivo();
                PopulaArquivo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Arquivo.
        /// </summary>
        /// <param name="entidade">TituloImagemResumo relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Arquivo.</returns>
		public IEnumerable<Arquivo> Carregar(TituloImagemResumo entidade)
		{		
			List<Arquivo> entidadesRetorno = new List<Arquivo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Arquivo.* FROM Arquivo INNER JOIN TituloImagemResumo ON Arquivo.arquivoId=TituloImagemResumo.arquivoId WHERE TituloImagemResumo.tituloImagemResumoId=@tituloImagemResumoId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@tituloImagemResumoId", DbType.Int32, entidade.TituloImagemResumoId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Arquivo entidadeRetorno = new Arquivo();
                PopulaArquivo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Arquivo.
        /// </summary>
        /// <param name="entidade">TituloInformacaoComentarioEspecialista relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Arquivo.</returns>
		public IEnumerable<Arquivo> Carregar(TituloInformacaoComentarioEspecialista entidade)
		{		
			List<Arquivo> entidadesRetorno = new List<Arquivo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Arquivo.* FROM Arquivo INNER JOIN TituloInformacaoComentarioEspecialista ON Arquivo.arquivoId=TituloInformacaoComentarioEspecialista.arquivoId WHERE TituloInformacaoComentarioEspecialista.tituloInformacaoComentarioEspecialistaId=@tituloInformacaoComentarioEspecialistaId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@tituloInformacaoComentarioEspecialistaId", DbType.Int32, entidade.TituloInformacaoComentarioEspecialistaId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Arquivo entidadeRetorno = new Arquivo();
                PopulaArquivo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Arquivo.
        /// </summary>
        /// <param name="entidade">TituloInformacaoSobreAutor relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Arquivo.</returns>
		public IEnumerable<Arquivo> Carregar(TituloInformacaoSobreAutor entidade)
		{		
			List<Arquivo> entidadesRetorno = new List<Arquivo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Arquivo.* FROM Arquivo INNER JOIN TituloInformacaoSobreAutor ON Arquivo.arquivoId=TituloInformacaoSobreAutor.arquivoId WHERE TituloInformacaoSobreAutor.tituloInformacaoSobreAutorId=@tituloInformacaoSobreAutorId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@tituloInformacaoSobreAutorId", DbType.Int32, entidade.TituloInformacaoSobreAutorId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Arquivo entidadeRetorno = new Arquivo();
                PopulaArquivo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}

		/// <summary>
        /// Método que retorna uma coleção de Arquivo.
        /// </summary>
        /// <param name="entidade">TituloInformacaoSumario relacionado(a) (somente o identificador é necessário).</param>		
		/// <returns>Retorna uma coleção de Arquivo.</returns>
		public IEnumerable<Arquivo> Carregar(TituloInformacaoSumario entidade)
		{		
			List<Arquivo> entidadesRetorno = new List<Arquivo>();
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT Arquivo.* FROM Arquivo INNER JOIN TituloInformacaoSumario ON Arquivo.arquivoId=TituloInformacaoSumario.arquivoId WHERE TituloInformacaoSumario.tituloInformacaoSumarioId=@tituloInformacaoSumarioId");

			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			_db.AddInParameter(command, "@tituloInformacaoSumarioId", DbType.Int32, entidade.TituloInformacaoSumarioId);
								
			IDataReader reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Arquivo entidadeRetorno = new Arquivo();
                PopulaArquivo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
			
		}
		
		
		/// <summary>
        /// Método que retorna uma coleção de Arquivo.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
		///  <returns>Retorna um List contendos Arquivo.</returns>
		public IEnumerable<Arquivo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro) {
		
			List<Arquivo> entidadesRetorno = new List<Arquivo>();
			
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
				sbOrder.Append( " ORDER BY arquivoId" );
			}
				
			
			if (registrosPagina>0) {
				
				//sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Arquivo");
				//if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Arquivo WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
				//} else {
				//	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Arquivo ORDER BY " + orderBy + ")");				
			    //}	
				sbSQL.Append("SELECT * FROM ( ");				
				sbSQL.Append("SELECT Arquivo.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Arquivo ");				
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina-1)*registrosPagina)+1).ToString() + " AND " + ((numeroPagina)*registrosPagina).ToString());				
								
			} else {
				sbSQL.Append("SELECT Arquivo.* FROM Arquivo ");
				if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) { sbSQL.Append( "WHERE" + filtro.GetWhereString() + " " ); }
				if ( sbOrder.Length > 0 ) { sbSQL.Append(sbOrder.ToString()); }
			}
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());
			reader = _db.ExecuteReader(command);
			
            while (reader.Read())
            {
                Arquivo entidadeRetorno = new Arquivo();
                PopulaArquivo(reader, entidadeRetorno);
				entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;					
					
		}	
		
		/// <summary>
        /// Método que retorna todas os Arquivo existentes na base de dados.
        /// </summary>
		public IEnumerable<Arquivo> CarregarTodos() {			
			return CarregarTodos(0, 0, null, null, null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Arquivo na base de dados.
        /// </summary>
        /// <returns></returns>
		public int TotalRegistros() 
		{
			return TotalRegistros(null);
		}	
		
        /// <summary>
        /// Método que retorna o total de Arquivo na base de dados, aceita filtro.
        /// </summary>
		/// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
		/// <returns></returns>
		public int TotalRegistros(IFilterHelper filtro) 
		{		
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT COUNT(*) AS Total FROM Arquivo");
			
			if (filtro!=null && !filtro.GetWhereString().Equals(String.Empty))
					sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");			
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
								
			// Executa a query.
			
			int resultado = (int) _db.ExecuteScalar(command);
			
			
			return resultado;	
		}
		
		/// <summary>
        /// Método que retorna popula um Arquivo baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Arquivo a ser populado(.</param>
		public static void PopulaArquivo(IDataReader reader, Arquivo entidade) 
		{						
			if (reader["arquivoId"] != DBNull.Value)
				entidade.ArquivoId = Convert.ToInt32(reader["arquivoId"].ToString());
			
			if (reader["tamanhoArquivo"] != DBNull.Value)
				entidade.TamanhoArquivo = Convert.ToInt32(reader["tamanhoArquivo"].ToString());
			
			if (reader["dataHoraUpload"] != DBNull.Value)
				entidade.DataHoraUpload = Convert.ToDateTime(reader["dataHoraUpload"].ToString());
			
			if (reader["nomeArquivo"] != DBNull.Value)
				entidade.NomeArquivo = reader["nomeArquivo"].ToString();
			
			if (reader["nomeArquivoOriginal"] != DBNull.Value)
				entidade.NomeArquivoOriginal = reader["nomeArquivoOriginal"].ToString();
			

		}		
		
	}
}
		