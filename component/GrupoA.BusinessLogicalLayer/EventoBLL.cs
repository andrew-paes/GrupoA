using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;
using GrupoA.BusinessLogicalLayer.Helper;
using GrupoA.BusinessObject;
using GrupoA.BusinessObject.Enumerator;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;
using GrupoA.FilterHelper;

namespace GrupoA.BusinessLogicalLayer
{
    /// <summary>
    /// Classe que contém os métodos de negócio para utilização de Evento e Categorias de Evento
    /// </summary>
    public class EventoBLL : BaseBLL
    {

        #region Declarações DAL

        private ICategoriaDAL _categoriaDal;
        private ICategoriaDAL CategoriaDal
        {
            get { return _categoriaDal ?? (_categoriaDal = new CategoriaADO()); }
        }

        private IConteudoDAL _conteudoDal;
        private IConteudoDAL ConteudoDal
        {
            get { return _conteudoDal ?? (_conteudoDal = new ConteudoADO()); }
        }

        private IConteudoHitsDAL _conteudoHitsDal;
        private IConteudoHitsDAL ConteudoHitsDal
        {
            get { return _conteudoHitsDal ?? (_conteudoHitsDal = new ConteudoHitsADO()); }
        }

        private IEventoDAL _eventoDal;
        private IEventoDAL EventoDal
        {
            get { return _eventoDal ?? (_eventoDal = new EventoADO()); }
        }

        private IEventoAlertaDAL _eventoAlertaDal;
        private IEventoAlertaDAL EventoAlertaDal
        {
            get { return _eventoAlertaDal ?? (_eventoAlertaDal = new EventoAlertaADO()); }
        }

        private IEventoImagemDAL _eventoImagemDal;
        private IEventoImagemDAL EventoImagemDal
        {
            get { return _eventoImagemDal ?? (_eventoImagemDal = new EventoImagemADO()); }
        }

        private IArquivoDAL _arquivoDal;
        private IArquivoDAL ArquivoDal
        {
            get { return _arquivoDal ?? (_arquivoDal = new ArquivoADO()); }
        }

        private IUsuarioDAL _usuarioDal;
        private IUsuarioDAL UsuarioDal
        {
            get { return _usuarioDal ?? (_usuarioDal = new UsuarioADO()); }
        }

        private IConteudoImprensaDAL _conteudoImprensaDal;
        private IConteudoImprensaDAL ConteudoImprensaDal
        {
            get { return _conteudoImprensaDal ?? (_conteudoImprensaDal = new ConteudoImprensaADO()); }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="quantidadeRegistros"></param>
        /// <returns></returns>
        public IEnumerable<Evento> CarregarPorAreaInteresse(Usuario usuario, Int32 quantidadeRegistros)
        {
            IEnumerable<Evento> eventos = EventoDal.CarregarPorAreaInteresse(usuario, quantidadeRegistros);

            foreach (var item in eventos)
            {
                item.ConteudoImprensa = ConteudoImprensaDal.Carregar(new ConteudoImprensa() { ConteudoImprensaId = item.EventoId });
            }

            return eventos;
        }

        /// <summary>
        /// Insere um novo Evento
        /// </summary>
        /// <param name="evento">Evento a ser inserido</param>
        /// <param name="categorias">Categorias a serem inseridas no conteúdo específico</param>
        public void InserirEvento(Evento evento, IEnumerable<Categoria> categorias)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Inserção de Conteúdo
                Conteudo conteudo = new Conteudo();
                conteudo.DataHoraCadastro = DateTime.Now;
                conteudo.ConteudoTipo = new ConteudoTipo((int)TipoDeConteudo.Evento);
                ConteudoDal.Inserir(conteudo);
                // Inserção de Conteúdo Imprensa
                ConteudoImprensa conteudoImprensa = evento.ConteudoImprensa;
                conteudoImprensa.ConteudoImprensaId = conteudo.ConteudoId;
                //conteudoImprensa.Conteudo = conteudo;
                ConteudoImprensaDal.Inserir(conteudoImprensa);
                // Inserção de Categorias
                foreach (Categoria categoria in categorias)
                    if (ConteudoDal != null) ConteudoDal.InserirRelacionamentoAreaConhecimento(conteudo, categoria);
                // Inserção de Evento com mesmo código identificador (Id)
                evento.EventoId = conteudo.ConteudoId;
                evento.ConteudoImprensa = conteudoImprensa;
                EventoDal.Inserir(evento);
                scope.Complete();

            }
        }

        /// <summary>
        /// Carrega um evento a partir do identificador EventoId recebido
        /// </summary>
        /// <param name="evento">Evento que conterá o identificador EventoId necessário para busca</param>
        /// <returns>Evento populado</returns>
        public Evento CarregarEvento(Evento evento)
        {
            return EventoDal.CarregarComDependencias(evento);
        }

        /// <summary>
        /// Carrega um Evento com todas as dependências de Conteúdo e Imagens
        /// </summary>
        /// <param name="evento">Evento a ser carregado (deve conter o código identificador)</param>
        /// <returns>Objeto Evento carregado</returns>
        public Evento CarregarEventoComDependencias(Evento evento)
        {
            evento = EventoDal.CarregarComDependencias(evento);
            evento.EventoImagens = this.CarregarTodosEventoImagem(evento);
            evento.EventoAlertas = this.CarregarTodosEventoAlerta(evento);
            if (evento.ConteudoImprensa != null && (evento.ConteudoImprensa.Conteudo != null && evento.ConteudoImprensa.Conteudo.ConteudoId > 0))
            {
                evento.ConteudoImprensa.Conteudo.Categorias = (List<Categoria>)CarregarCategoriasConteudo(evento.ConteudoImprensa.Conteudo);
            }
            if (evento.ArquivoThumb != null && evento.ArquivoThumb.ArquivoId > 0)
            {
                evento.ArquivoThumb = CarregarArquivo(evento.ArquivoThumb);
            }
            return evento;
        }

        /// <summary>
        /// Atualiza as informações referentes ao Evento
        /// </summary>
        /// <param name="evento">Objeto Evento que conterá as informações</param>
        /// <param name="categorias">Categorias a serem inseridas no conteúdo específico</param>
        public void AtualizarEvento(Evento evento, IEnumerable<Categoria> categorias)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Atualização dos Ids
                evento.ConteudoImprensa.ConteudoImprensaId = evento.EventoId;
                evento.ConteudoImprensa.Conteudo = new Conteudo() { ConteudoId = evento.EventoId };

                // Atualização de Conteúdo Imprensa
                ConteudoImprensa conteudoImprensa = evento.ConteudoImprensa;
                conteudoImprensa.ConteudoImprensaId = evento.EventoId;
                ConteudoImprensaDal.Atualizar(conteudoImprensa);

                // Atualização de Categorias
                // a. Exclui todos os relacionamentos com áreas de conhecimento
                ConteudoDal.ExcluirTodasAreasConhecimento(evento.ConteudoImprensa.Conteudo);
                // b. Inclui os novos relacionamentos
                foreach (Categoria categoria in categorias)
                    ConteudoDal.InserirRelacionamentoAreaConhecimento(evento.ConteudoImprensa.Conteudo, categoria);

                // Atualização de Evento com mesmo código identificador (Id)
                EventoDal.Atualizar(evento);

                scope.Complete();

            }
        }

        /// <summary>
        /// Exclusão de um Evento conforme o identificador recebido.
        /// </summary>
        /// <param name="evento">Objeto Evento que deverá conter o código identificador "EventoId"
        /// a ser excluído.</param>
        /// <returns>Listagem de </returns>
        public Evento ExcluirEvento(Evento evento)
        {
            Evento retorno = null;
            // 1. Verifica se existe Alertas de Evento em aberto
            // Caso exista, deve somente atualizar o status para inativo
            // Caso contrário, exclui o registro e suas dependências
            if (evento.EventoAlertas.Count > 0)
            {
                // 1.1. Atualiza o status
                //EventoDal.AtualizarStatus(evento);
                evento.ConteudoImprensa.Ativo = false;
                ConteudoImprensaDal.Atualizar(evento.ConteudoImprensa);
                // 1.2. Atualiza a data de cancelamento dos alertas
                EventoAlertaDal.CancelarAlertas(evento);
            }
            else
            {
                // Atribui a variável de retorno alertas conforme os alertas existentes no evento.
                // Os usuários desses alertas receberão (pelo controller) um e-mail de alteração de datas
                // e/ou status do evento
                retorno = evento;
                // 1.1. Exclusão em EventoImagem
                foreach (EventoImagem eventoimagem in evento.EventoImagens)
                {
                    // Exclui Imagem do Evento
                    EventoImagemDal.Excluir(eventoimagem);
                    // Exclui Arquivo
                    ArquivoDal.Excluir(new Arquivo() { ArquivoId = eventoimagem.Arquivo.ArquivoId });
                }
                // 1.2. Exclusão do Evento
                EventoDal.Excluir(evento);
                // 1.3. Exclusão do Conteúdo Imprensa
                ConteudoImprensaDal.Excluir(new ConteudoImprensa() { ConteudoImprensaId = evento.EventoId });
                // 1.4. Exclusão do Conteúdo
                new ConteudoBLL().ExcluirConteudo(evento.ConteudoImprensa.Conteudo);
            }
            // 2. Notifica os usuário, por email, do cancelamento do "alerta" do Evento;
            return retorno;

        }

        /// <summary>
        /// Carrega eventos com suas dependências.
        /// </summary>
        /// <param name="registrosPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="ordemColunas"></param>
        /// <param name="ordemSentidos"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public IEnumerable<Evento> CarregarEventosComDependencias(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {
            IEnumerable<Evento> eventos = EventoDal.CarregarTodosValidosComDependencias(registrosPagina, numeroPagina, ordemColunas, ordemSentidos, filtro);
            return eventos;
        }

        /// <summary>
        /// Carrega eventos ativos e não expirados com suas dependências.
        /// </summary>
        /// <param name="registrosPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="ordemColunas"></param>
        /// <param name="ordemSentidos"></param>
        /// <returns></returns>
        public IEnumerable<Evento> CarregarEventosValidosComDependencias(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos)
        {
            return EventoDal.CarregarTodosValidosComDependencias(registrosPagina, numeroPagina, ordemColunas, ordemSentidos, null);
        }

        public int ContarTodosValidosComDependencias()
        {
            return EventoDal.ContarTodosValidosComDependencias();
        }

        /// <summary>
        /// Carrega todas as Categorias (Áreas de Conhecimento) de um Conteúdo.
        /// </summary>
        /// <param name="conteudo">Conteúdo a serem carregadas as Categorias(Áreas de Conhecimento).</param>
        /// <returns>Coleção de Categorias (Áreas de Conhecimento).</returns>
        public IEnumerable<Categoria> CarregarCategoriasConteudo(Conteudo conteudo)
        {
            return ConteudoDal.CarregarTodasAreasConhecimentoCategoria(conteudo);
        }

        /// <summary>
        /// Carrega todas as Imagens de um Evento conforme código identificador recebido
        /// </summary>
        /// <param name="entidade">Objeto Entidade que possui o identificador eventoId</param>
        /// <returns>Lista de Imagens do Evento</returns>
        public List<EventoImagem> CarregarTodosEventoImagem(Evento entidade)
        {
            var eventoImagemFh = new EventoImagemFH() { EventoId = entidade.EventoId.ToString() };

            return EventoImagemDal.CarregarTodosArquivos(0, 0, null, null, eventoImagemFh).ToList();
        }

        /// <summary>
        /// Carrega um Arquivo conforme o código identificador recebido
        /// </summary>
        /// <param name="entidade">Objeto Arquivo que contém o identificador arquivoId</param>
        /// <returns>Objeto Arquivo populado</returns>
        public Arquivo CarregarArquivo(Arquivo entidade)
        {
            return ArquivoDal.Carregar(entidade);
        }

        /// <summary>
        /// Insere uma nova Imagem ligada ao Evento
        /// </summary>
        /// <param name="entidade">Objeto EventoImagem a ser inserido</param>
        /// <returns>Objeto EventoImagem contendo o código inserido e demais informações</returns>
        public EventoImagem InserirEventoImagem(EventoImagem entidade)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (entidade.Arquivo.ArquivoId == 0)
                {
                    var arquivo = new Arquivo
                                      {
                                          NomeArquivo = entidade.Arquivo.NomeArquivo,
                                          NomeArquivoOriginal = entidade.Arquivo.NomeArquivoOriginal,
                                          TamanhoArquivo = entidade.Arquivo.TamanhoArquivo,
                                          DataHoraUpload = entidade.Arquivo.DataHoraUpload
                                      };
                    ArquivoDal.Inserir(arquivo);
                    entidade.Arquivo.ArquivoId = arquivo.ArquivoId;
                    EventoImagemDal.Inserir(entidade);
                }
                scope.Complete();

            }
            return entidade;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public EventoImagem CarregarEventoImagem(EventoImagem entidade)
        {
            return EventoImagemDal.CarregarEventoImagem(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void ExcluirEventoImagem(Arquivo entidade)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                EventoImagemDal.Excluir(entidade);
                ArquivoDal.Excluir(entidade);
                scope.Complete();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void AtualizarEventoImagem(EventoImagem entidade)
        {
            EventoImagemDal.Atualizar(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public List<EventoAlerta> CarregarTodosEventoAlerta(Evento entidade)
        {
            var eventoAlertaFh = new EventoAlertaFH() { EventoId = entidade.EventoId.ToString() };
            return (List<EventoAlerta>)EventoAlertaDal.CarregarTodos(0, 0, null, null, eventoAlertaFh);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <param name="eventoId"></param>
        /// <returns></returns>
        public List<EventoAlerta> CarregarEventoAlertasPorUsuario(Int32 usuarioId, Int32 eventoId)
        {
            return EventoAlertaDal.CarregarEventoAlertasPorUsuario(usuarioId, eventoId);
        }

        /// <summary>
        /// Insere Evento Alerta
        /// </summary>
        /// <param name="entidade">Objeto EventoAlerta a ser inserido</param>
        /// <returns>Objeto EventoAlerta contendo o código inserido e demais informações</returns>
        public void InserirEventoAlerta(EventoAlerta entidade)
        {
            
            EventoAlertaDal.Inserir(entidade);
        }

        /// <summary>
        /// Remover Evento Alerta
        /// </summary>
        /// <param name="entidade">Objeto EventoAlerta a ser inserido</param>
        /// <returns>Objeto EventoAlerta contendo o código inserido e demais informações</returns>
        public void RemoverEventoAlerta(EventoAlerta entidade)
        {
            EventoAlertaDal.Excluir(entidade);
        }

        public void RemoverEventoAlerta(Int32 eventoAlertaId)
        {
            EventoAlertaDal.Excluir(new EventoAlerta() { EventoAlertaId = eventoAlertaId });
        }

        #region Métodos de Alertas de Evento

        /// <returns>Lista de Alertas do Evento</returns>
        public List<EventoAlerta> carregarTodosEventoAlerta(Evento entidade)
        {
            //var eventoAlertaFH = new EventoAlertaFH() { EventoId = entidade.EventoId.ToString() };

            //return (List<EventoAlerta>)eventoAlertaADO.CarregarAutores(0, 0, null, null, eventoAlertaFH);
            return new List<EventoAlerta>();
        }

        /// <summary>
        /// Insere Evento Alerta
        /// </summary>
        /// <param name="entidade">Objeto EventoAlerta a ser inserido</param>
        /// <returns>Objeto EventoAlerta contendo o código inserido e demais informações</returns>
        //public void InserirEventoAlerta(EventoAlerta entidade)
        //{
        //    this.RemoverEventoAlerta(entidade);
        //    eventoAlertaADO.InserirNovoAutor(entidade);
        //}
        /// <summary>
        /// Remover Evento Alerta
        /// </summary>
        /// <param name="entidade">Objeto EventoAlerta a ser inserido</param>
        /// <returns>Objeto EventoAlerta contendo o código inserido e demais informações</returns>
        //public void RemoverEventoAlerta(EventoAlerta entidade)
        //{
        //    EventoAlerta eventoAlerta = eventoAlertaADO.CarregarEventoAlertaPorUsuario(entidade.Usuario.UsuarioId, entidade.Evento.EventoId);
        //    if (eventoAlerta != null)
        //    {
        //        eventoAlertaADO.Excluir(eventoAlerta);
        //    }
        //}
        #endregion

        #region Métodos de Categoria

        #region CarregarTodasCategoriasBase
        /// <summary>
        /// Carrega todas as categorias base (Áreas de Conhecimento)
        /// </summary>
        /// <returns>Coleção de Categorias Base</returns>
        public IEnumerable<Categoria> CarregarTodasCategoriasBase()
        {
            return CategoriaDal.CarregarTodosBase();
        }
        #endregion

        #endregion

        #region Métodos para Formulario de Contato

        #endregion

        public List<Evento> CarregarAlertasEventoAtivosPorUsuario(int registrosPagina, int numeroPagina, Usuario usuario)
        {
            return EventoAlertaDal.CarregarAlertasAtivosPorUsuario(registrosPagina, numeroPagina, usuario);
        }

        public Int32 ContarAlertasEventoAtivosPorUsuario(Usuario usuario)
        {
            return EventoAlertaDal.ContarAlertasAtivosPorUsuario(usuario);
        }
        

        /// <summary>
        /// Carrega todas as categorias base (Áreas de Conhecimento)
        /// </summary>
        /// <returns>Coleção de Categorias Base</returns>
        //public IEnumerable<Categoria> CarregarTodasCategoriasBase()
        //{
        //    return CategoriaDal.CarregarTodosBase();
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="palavra"></param>
        /// <returns></returns>
        private String FormataPalavraFiltro(String palavra)
        {
            if (!String.IsNullOrEmpty(palavra))
            {
                String[] arrPalavra = BuscaHelper.RemoveStopWords(palavra);

                if (arrPalavra.Count() > 0)
                {
                    var res = arrPalavra.Aggregate((current, next) => current + " AND " + next);
                    palavra = res.ToString();
                    palavra = palavra.Replace("'", "");
                    palavra = palavra.Replace(" AND ", "*\" AND \"");
                    palavra = "\"" + palavra + "*\"";
                    return palavra;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return palavra;
            }
        }

        /// <summary>
        /// Método que retorna quantidade total de evento para busca por palavra
        /// </summary>
        /// <param name="palavra"></param>
        /// <returns></returns>
        public int ContarEventoBusca(String palavra)
        {
            palavra = FormataPalavraFiltro(palavra);

            return EventoDal.ContarEventoBusca(palavra);
        }

        /// <summary>
        /// Método para fazer a busca de evento através do filtro
        /// </summary>
        /// <param name="registrosPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="palavra"></param>
        /// <returns></returns>
        public List<Evento> CarregarEventoBusca(int registrosPagina, int numeroPagina, String[] ordenacao, String[] ordenacaoSentido, String palavra)
        {
            palavra = FormataPalavraFiltro(palavra);

            return EventoDal.CarregarEventoBusca(registrosPagina, numeroPagina, ordenacao, ordenacaoSentido, palavra);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public List<Evento> CarregarEventosPorCategoria(Categoria categoria)
        {
            return EventoDal.CarregarEventosPorCategoria(categoria);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoriaBOList"></param>
        /// <returns></returns>
        public List<Evento> CarregarEventosPorCategoria(List<Categoria> categoriaBOList)
        {
            return EventoDal.CarregarEventosPorCategoria(categoriaBOList);
        }

        public int ContarTodosEventosValidosComDependencias(int areaId)
        {
            return EventoDal.ContarTodosEventosValidosComDependencias(areaId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        public Int32 ContarTodosEventosPorInteresseUsuarioId(Int32 usuarioId)
        {
            return EventoDal.ContarTodosEventosPorInteresseUsuarioId(usuarioId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrosPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="ordemColunas"></param>
        /// <param name="ordemSentidos"></param>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        public IEnumerable<Evento> CarregarTodosEventosPorInteresseUsuarioId(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, Int32 usuarioId)
        {
            return EventoDal.CarregarTodosEventosPorInteresseUsuarioId(registrosPagina, numeroPagina, ordemColunas, ordemSentidos, usuarioId);
        }

        public void DispararAlertas()
        {
            String caminhoTemplateEmail = ConfigurationManager.AppSettings["CaminhoEmailLembreteEvento"].ToString();
            List<Evento> eventos = EventoDal.CarregarEventosParaEnviarAlerta();

            foreach (Evento evento in eventos)
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        this.EnviarEmail(evento.EventoAlertas[0].Usuario.EmailUsuario, caminhoTemplateEmail, evento);

                        evento.EventoAlertas[0].Evento = new Evento();
                        evento.EventoAlertas[0].Evento.EventoId = evento.EventoId;
                        evento.EventoAlertas[0].DataHoraEncaminhamento = DateTime.Now;

                        EventoAlertaDal.Atualizar(evento.EventoAlertas[0]);

                        scope.Complete();
                    }
                }
                catch(Exception ex) 
                {
                    throw ex;
                }
            }
        }

        private Boolean EnviarEmail(String emailDestino, String caminhoTemplate, Evento evento)
        {
            String emailEmitente = String.Empty;
            String assuntoEmail = String.Concat("Grupo A | Lembrete de evento ", evento.ConteudoImprensa.Titulo);

            emailEmitente = GrupoA.GlobalResources.GrupoA_Resource.EmailSAC;
            
            Dictionary<string, string> dicionarioDados = new Dictionary<string, string>();
            dicionarioDados.Add("Nome", evento.EventoAlertas[0].Usuario.NomeUsuario);
            dicionarioDados.Add("Titulo", evento.ConteudoImprensa.Titulo);
            dicionarioDados.Add("Data", String.Concat(evento.DataEventoInicio.Value.ToShortDateString(), " a ", evento.DataEventoFim.Value.ToShortDateString()));
            dicionarioDados.Add("Local", evento.Local);
            dicionarioDados.Add("Veiculo", evento.ConteudoImprensa.Fonte);
            dicionarioDados.Add("CaminhoSite", ConfigurationManager.AppSettings["sitePath"].ToString());

            try // Tenta enviar e-mail
            {
                StringBuilder templateEmail = new EmailHelper().PopulaTemplateEmail(dicionarioDados, caminhoTemplate);

                new EmailHelper().EnviarEmail(emailEmitente, emailDestino, assuntoEmail, templateEmail);
            }
            catch
            {
                return false; // Mensagem NÃO enviada
            }

            return true;
        }

        public Int32 ValidarDiasEventoParaUsuario(Int32 usuarioId, Int32 eventoId, Int32 dias)
        {
            return EventoAlertaDal.ValidarDiasEventoParaUsuario(usuarioId, eventoId, dias);
        }
    }
}