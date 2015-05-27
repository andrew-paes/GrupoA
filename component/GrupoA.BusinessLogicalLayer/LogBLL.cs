using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;

namespace GrupoA.BusinessLogicalLayer
{
    public class LogBLL : BaseBLL
    {
        private ILogOcorrenciaDAL _LogOcorrenciaDAL;
        private IUsuarioDAL _usuarioDAL;
        private ILogEventoDAL _logEventoDAL;
        private ILogCategoriaDAL _logCategoriaDAL;

        public ILogOcorrenciaDAL LogOcorrenciaDAL
        {
            get
            {
                if (_LogOcorrenciaDAL == null)
                    _LogOcorrenciaDAL = new LogOcorrenciaADO();
                return _LogOcorrenciaDAL;
            }            
        }
        private IUsuarioDAL UsuarioDAL
        {
            get
            {
                if (_usuarioDAL == null)
                    _usuarioDAL = new UsuarioADO();
                return _usuarioDAL;

            }
        }
        private ILogEventoDAL LogEventoDAL
        {
            get
            {
                if (_logEventoDAL == null)
                    _logEventoDAL = new LogEventoADO();
                return _logEventoDAL;

            }
        }
        private ILogCategoriaDAL LogCategoriaDAL
        {
            get
            {
                if (_logCategoriaDAL == null)
                    _logCategoriaDAL = new LogCategoriaADO();
                return _logCategoriaDAL;

            }
        }

        /// <summary>
        /// Método que registra uma ocorrencia no log
        /// </summary>
        /// <param name="logOcorrencia">Evento ocorrido</param>
        /// <param name="dados">Dados do evento</param>
        /// <param name="usuario">Usuario que disparou o evento </param>
        public void RegistrarOcorrenciaLog(LogOcorrencia logOcorrencia)
        {
            //LogOcorrencia ocorrencia = new LogOcorrencia();
            //ocorrencia.DataHoraOcorrencia = DateTime.Now;
            //ocorrencia.Usuario = usuario;
            //ocorrencia.Dados = dados.ToXml();
            //ocorrencia.LogEvento = logOcorrencia.LogEvento;
            logOcorrencia.DataHoraOcorrencia = DateTime.Now;
            LogOcorrenciaDAL.Inserir(logOcorrencia);
        }

        /// <summary>
        /// Método que registra uma ocorrencia no log
        /// </summary>
        /// <param name="entidade">Entidade LogOcorrencia</param>
        public LogOcorrencia CarregarLogOcorrencia(LogOcorrencia entidade)
        {
            entidade = LogOcorrenciaDAL.Carregar(entidade);
            if (entidade.LogEvento != null && entidade.LogEvento.LogEventoId > 0)
            {                
                entidade.LogEvento = LogEventoDAL.Carregar(entidade.LogEvento);
                if (entidade.LogEvento.LogCategoria != null && entidade.LogEvento.LogCategoria.LogCategoriaId > 0)
                {
                    entidade.LogEvento.LogCategoria = LogCategoriaDAL.Carregar(entidade.LogEvento.LogCategoria);
                }
            }
            if (entidade.Usuario != null && entidade.Usuario.UsuarioId > 0)
            {
                entidade.Usuario = UsuarioDAL.Carregar(entidade.Usuario);
            }
            return entidade;
        }
    }
}
