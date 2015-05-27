using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess.ADO
{
    public partial class EventoAlertaADO : ADOSuper, IEventoAlertaDAL
    {
        /// <summary>
        /// Método que carrega um EventoAlerta.
        /// </summary>
        /// <param name="entidade">EventoAlerta a ser carregado (somente o identificador é necessário).</param>
        /// <returns>EventoAlerta</returns>
        public void Excluir(Evento entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM EventoAlerta ");
            sbSQL.Append("WHERE eventoId=@eventoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@eventoId", DbType.Int32, entidade.EventoId);


            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que atualiza todas as datas de cancelamento de um Alerta conforme o identificador do Evento
        /// </summary>
        /// <param name="entidade">Evento que devem ser cancelados os alertas</param>
        public void CancelarAlertas(Evento entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE EventoAlerta SET ");
            sbSQL.Append("  dataHoraCancelamento=getdate() ");
            sbSQL.Append(" WHERE eventoId=@eventoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@eventoId", DbType.Int32, entidade.EventoId);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que carrega um EventoAlerta.
        /// </summary>
        /// <returns>EventoAlerta</returns>
        public EventoAlerta CarregarEventoAlertaPorQtdDias(int usuarioId, int eventoId, int qtdDias)
        {

            EventoAlerta entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM EventoAlerta ");
            sbSQL.Append("WHERE eventoId=@eventoId ");
            sbSQL.Append("AND usuarioId=@usuarioId ");
            sbSQL.Append("AND dias=@dias");
            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@eventoId", DbType.Int32, eventoId);
            _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuarioId);
            _db.AddInParameter(command, "@dias", DbType.Int32, qtdDias);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new EventoAlerta();
                PopulaEventoAlerta(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Método que carrega um EventoAlerta.
        /// </summary>
        /// <returns>EventoAlerta</returns>
        public List<EventoAlerta> CarregarEventoAlertasPorUsuario(Int32 usuarioId, Int32 eventoId)
        {
            List<EventoAlerta> entidadesRetorno = new List<EventoAlerta>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM EventoAlerta ");
            sbSQL.Append("WHERE eventoId=@eventoId ");
            sbSQL.Append("AND usuarioId=@usuarioId ");
            sbSQL.Append("ORDER BY dias ");
            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@eventoId", DbType.Int32, eventoId);
            _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuarioId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                EventoAlerta eventoAlerta = new EventoAlerta();
                PopulaEventoAlerta(reader, eventoAlerta);
                entidadesRetorno.Add(eventoAlerta);
            }
            reader.Close();

            return entidadesRetorno;
        }

        public List<Evento> CarregarAlertasAtivosPorUsuario(int registrosPagina, int numeroPagina, Usuario usuario)
        {
            List<Evento> eventos = new List<Evento>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM ( ");
            sbSQL.Append("SELECT Evento.eventoId, ");
            sbSQL.Append("    Evento.dataEventoInicio, ");
	        sbSQL.Append("    Evento.dataEventoFim, ");
	        sbSQL.Append("    Evento.arquivoIdThumb, ");
	        sbSQL.Append("    Evento.exibeFormularioContato, ");
	        sbSQL.Append("    Evento.local, ");
	        sbSQL.Append("    ConteudoImprensa.Titulo, ");
	        sbSQL.Append("    ConteudoImprensa.Resumo, ");
	        sbSQL.Append("    ConteudoImprensa.conteudoImprensaId, ");
	        sbSQL.Append("    ConteudoImprensa.fonte, ");
	        sbSQL.Append("    ConteudoImprensa.fonteUrl, ");
	        sbSQL.Append("    ConteudoImprensa.dataExibicaoInicio, ");
	        sbSQL.Append("    ConteudoImprensa.dataExibicaoFim, ");
	        sbSQL.Append("    ConteudoImprensa.texto, ");
	        sbSQL.Append("    ConteudoImprensa.destaque, ");
            sbSQL.Append("    ConteudoImprensa.ativo, ");
	        sbSQL.Append("    Arquivo.*, ");
	        sbSQL.Append("    ROW_NUMBER() OVER (ORDER BY Evento.dataEventoInicio) R ");
            sbSQL.Append("FROM Evento ");
            sbSQL.Append("JOIN ConteudoImprensa ON ConteudoImprensa.ConteudoImprensaId = Evento.EventoId ");
            sbSQL.Append("LEFT JOIN Arquivo ON Arquivo.ArquivoId = ArquivoIdThumb ");
            sbSQL.Append("WHERE EXISTS (SELECT * FROM EventoAlerta ");
			sbSQL.Append("              WHERE EventoAlerta.Ativo = 1 ");
			sbSQL.Append("	            AND EventoAlerta.dataHoraCancelamento IS NULL ");
			sbSQL.Append("	            AND EventoAlerta.dataHoraEncaminhamento IS NULL ");
            sbSQL.Append("	            AND EventoAlerta.UsuarioId = @usuarioId ");
			sbSQL.Append("	            AND EventoAlerta.eventoId = Evento.eventoId) ");
            sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());
            sbSQL.Append(" ORDER BY dataEventoInicio ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuario.UsuarioId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Evento evento = new Evento();
                EventoADO.PopulaEvento(reader, evento);
                evento.ConteudoImprensa = new ConteudoImprensa();
                ConteudoImprensaADO.PopulaConteudoImprensa(reader, evento.ConteudoImprensa);
                evento.ArquivoThumb = new Arquivo();
                ArquivoADO.PopulaArquivo(reader, evento.ArquivoThumb);
                eventos.Add(evento);
            }
            reader.Close();

            return eventos;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Int32 ContarAlertasAtivosPorUsuario(Usuario usuario)
        {
            Int32 retorno = 0;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total ");
            sbSQL.Append("FROM Evento ");
            sbSQL.Append("JOIN ConteudoImprensa ON ConteudoImprensa.ConteudoImprensaId = Evento.EventoId ");
            sbSQL.Append("WHERE EXISTS (SELECT * FROM EventoAlerta ");
			sbSQL.Append("              WHERE EventoAlerta.Ativo = 1 ");
			sbSQL.Append("	            AND EventoAlerta.dataHoraCancelamento IS NULL ");
			sbSQL.Append("	            AND EventoAlerta.dataHoraEncaminhamento IS NULL ");
			sbSQL.Append("	            AND EventoAlerta.UsuarioId = @usuarioId ");
			sbSQL.Append("	            AND EventoAlerta.eventoId = Evento.eventoId) ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuario.UsuarioId);

            IDataReader reader = _db.ExecuteReader(command);

            if ((reader.Read()) && ((reader["Total"] != DBNull.Value)))
            {
                retorno = (int)reader["Total"];
            }
            reader.Close();

            return retorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <param name="eventoId"></param>
        /// <param name="dias"></param>
        /// <returns></returns>
        public Int32 ValidarDiasEventoParaUsuario(Int32 usuarioId, Int32 eventoId, Int32 dias)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM EventoAlerta ");
            sbSQL.Append("WHERE eventoId=@eventoId ");
            sbSQL.Append("AND usuarioId=@usuarioId ");
            sbSQL.Append("AND dias=@dias ");
            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@eventoId", DbType.Int32, eventoId);
            _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuarioId);
            _db.AddInParameter(command, "@dias", DbType.Int32, dias);

            Int32 resultado = (Int32)_db.ExecuteScalar(command);

            return resultado;
        }
    }
}
