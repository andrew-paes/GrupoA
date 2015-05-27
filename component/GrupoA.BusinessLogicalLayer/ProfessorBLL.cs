using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using GrupoA.BusinessObject;
using GrupoA.BusinessObject.ViewHelper;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;

namespace GrupoA.BusinessLogicalLayer
{
    /// <summary>
    /// Classe que abstrai as regras de negócio referentes a usuários.
    /// </summary>    
    public class ProfessorBLL : BaseBLL
    {
        #region Propriedades

        #region professorDAL
        private IProfessorDAL _professorDAL;
        private IProfessorDAL professorDAL
        {
            get
            {
                if (_professorDAL == null)
                    _professorDAL = new ProfessorADO();
                return _professorDAL;

            }
        }
        #endregion

        #region perfilDAL
        private IPerfilDAL _perfilDAL;
        private IPerfilDAL perfilDAL
        {
            get
            {
                if (_perfilDAL == null)
                    _perfilDAL = new PerfilADO();
                return _perfilDAL;

            }
        }
        #endregion

        #region enderecoDAL
        private IEnderecoDAL _enderecoDAL;
        private IEnderecoDAL enderecoDAL
        {
            get
            {
                if (_enderecoDAL == null)
                    _enderecoDAL = new EnderecoADO();
                return _enderecoDAL;
            }
        }
        #endregion

        #region telefoneDAL
        private ITelefoneDAL _telefoneDAL;
        private ITelefoneDAL telefoneDAL
        {
            get
            {
                if (_telefoneDAL == null)
                    _telefoneDAL = new TelefoneADO();
                return _telefoneDAL;
            }
        }
        #endregion

        #region professorADO
        private IProfessorDAL _professorADO;
        private IProfessorDAL professorADO
        {
            get
            {
                if (_professorADO == null)
                    _professorADO = new ProfessorADO();
                return _professorADO;
            }
        }
        #endregion

        #region graduacaoProfessorDAL
        private IGraduacaoProfessorDAL _graduacaoProfessorDAL;
        private IGraduacaoProfessorDAL graduacaoProfessorDAL
        {
            get
            {
                if (_graduacaoProfessorDAL == null)
                    _graduacaoProfessorDAL = new GraduacaoProfessorADO();
                return _graduacaoProfessorDAL;
            }
        }
        #endregion

        #region tituloSolicitacaoDAL
        private ITituloSolicitacaoDAL _tituloSolicitacaoDAL;
        private ITituloSolicitacaoDAL tituloSolicitacaoDAL
        {
            get
            {
                if (_tituloSolicitacaoDAL == null)
                    _tituloSolicitacaoDAL = new TituloSolicitacaoADO();
                return _tituloSolicitacaoDAL;
            }
        }
        #endregion

        #region tituloDAL
        private ITituloDAL _tituloDAL;
        private ITituloDAL tituloDAL
        {
            get
            {
                if (_tituloDAL == null)
                    _tituloDAL = new TituloADO();
                return _tituloDAL;

            }
        }
        #endregion

        #region tituloSolicitacaoStatusDAL;
        private ITituloSolicitacaoStatusDAL _tituloSolicitacaoStatusDAL;
        private ITituloSolicitacaoStatusDAL tituloSolicitacaoStatusDAL
        {
            get
            {
                if (_tituloSolicitacaoStatusDAL == null)
                    _tituloSolicitacaoStatusDAL = new TituloSolicitacaoStatusADO();
                return _tituloSolicitacaoStatusDAL;

            }
        }
        #endregion

        #region produtoDAL
        private IProdutoDAL _produtoDAL;
        private IProdutoDAL produtoDAL
        {
            get
            {
                if (_produtoDAL == null)
                    _produtoDAL = new ProdutoADO();
                return _produtoDAL;

            }
        }
        #endregion

        #region disciplinaDAL
        private IDisciplinaDAL _disciplinaDAL;
        private IDisciplinaDAL disciplinaDAL
        {
            get
            {
                if (_disciplinaDAL == null)
                    _disciplinaDAL = new DisciplinaADO();
                return _disciplinaDAL;

            }
        }
        #endregion

        #region cursoDAL
        private ICursoDAL _cursoDAL;
        private ICursoDAL cursoDAL
        {
            get
            {
                if (_cursoDAL == null)
                    _cursoDAL = new CursoADO();
                return _cursoDAL;

            }
        }
        #endregion

        #region professorCursoDAL
        private IProfessorCursoDAL _professorCursoDAL;
        private IProfessorCursoDAL professorCursoDAL
        {
            get
            {
                if (_professorCursoDAL == null)
                    _professorCursoDAL = new ProfessorCursoADO();
                return _professorCursoDAL;

            }
        }
        #endregion

        #region cursoNivelDAL
        private ICursoNivelDAL _cursoNivelDAL;
        private ICursoNivelDAL cursoNivelDAL
        {
            get
            {
                if (_cursoNivelDAL == null)
                    _cursoNivelDAL = new CursoNivelADO();
                return _cursoNivelDAL;

            }
        }
        #endregion

        #region instituicaoDAL
        private IInstituicaoDAL _instituicaoDAL;
        private IInstituicaoDAL instituicaoDAL
        {
            get
            {
                if (_instituicaoDAL == null)
                    _instituicaoDAL = new InstituicaoADO();
                return _instituicaoDAL;

            }
        }
        #endregion

        #region professorInstituicaoDAL
        private IProfessorInstituicaoDAL _professorInstituicaoDAL;
        private IProfessorInstituicaoDAL professorInstituicaoDAL
        {
            get
            {
                if (_professorInstituicaoDAL == null)
                    _professorInstituicaoDAL = new ProfessorInstituicaoADO();
                return _professorInstituicaoDAL;

            }
        }
        #endregion

        #region professorDisciplinaDAL
        private IProfessorDisciplinaDAL _professorDisciplinaDAL;
        private IProfessorDisciplinaDAL professorDisciplinaDAL
        {
            get
            {
                if (_professorDisciplinaDAL == null)
                    _professorDisciplinaDAL = new ProfessorDisciplinaADO();
                return _professorDisciplinaDAL;

            }
        }
        #endregion

        #region arquivoDAL
        private IArquivoDAL _arquivoDAL;
        private IArquivoDAL arquivoDAL
        {
            get
            {
                if (_arquivoDAL == null)
                    _arquivoDAL = new ArquivoADO();
                return _arquivoDAL;

            }
        }
        #endregion

        #region professorComprovanteDocenciaDAL
        private IProfessorComprovanteDocenciaDAL _professorComprovanteDocenciaDAL;
        private IProfessorComprovanteDocenciaDAL professorComprovanteDocenciaDAL
        {
            get
            {
                if (_professorComprovanteDocenciaDAL == null)
                    _professorComprovanteDocenciaDAL = new ProfessorComprovanteDocenciaADO();
                return _professorComprovanteDocenciaDAL;

            }
        }
        #endregion

        #endregion

        #region Métodos

        #region public TituloSolicitacao CarregarTituloSolicitacaoComProfessorETitulo(TituloSolicitacao tituloSolicitacao)
        /// <summary>
        /// Carrega uma Solicitação de Título Com Professor, Título (e Produto) e Status encadeados.
        /// </summary>
        /// <param name="tituloSolicitacao">Entidade TituloSolicitação com código identificador para pesquisar.</param>
        /// <returns>Solicitação de título com Professor, Título (e Produto) e Status encadeados.</returns>
        public TituloSolicitacao CarregarTituloSolicitacaoComProfessorETitulo(TituloSolicitacao tituloSolicitacao)
        {
            // Carrega a Solicitacao
            tituloSolicitacao = tituloSolicitacaoDAL.Carregar(tituloSolicitacao);

            // Carrega os dados do professor
            tituloSolicitacao.Professor = professorDAL.CarregarComDependencias(tituloSolicitacao.Professor);

            // Carrega o Titulo
            tituloSolicitacao.Titulo = tituloDAL.CarregarComDependencias(tituloSolicitacao.Titulo);

            // Carrega o Produto
            //tituloSolicitacao.Titulo.Produto = produtoDAL.CarregarAutor(tituloSolicitacao.Titulo.Produto);


            // Carrega Status
            tituloSolicitacao.TituloSolicitacaoStatus = tituloSolicitacaoStatusDAL.Carregar(tituloSolicitacao.TituloSolicitacaoStatus);

            return tituloSolicitacao;
        }
        #endregion

        #region public Professor CarregarProfessorCompleto(Usuario usuario)
        /// <summary>
        /// Carrega os dados do Professor através do código identificador do Usuário
        /// </summary>
        /// <param name="usuario">Objeto Usuário que contém o identificador usuarioId</param>
        /// <returns>Objeto Professor com seus dados populados (incluindo Docências e Graduação).</returns>
        public Professor CarregarProfessorCompleto(Usuario usuario, bool docencia)
        {
            Professor professor = new Professor();
            professor.ProfessorId = usuario.UsuarioId;
            professor = professorDAL.CarregarComDependencias(professor);

            if (professor != null && professor.ProfessorId > 0)
            {
                if (docencia)
                {
                    professor = professorDAL.CarregarInstituicoesProfessor(professor); // Carrega Instituições
                }

                professor.GraduacaoProfessor = graduacaoProfessorDAL.CarregarGraduacaoCompletaPorProfessor(professor); // Carrega Graduação
            }

            return professor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Professor CarregarProfessorCompleto(Usuario usuario)
        {
            return this.CarregarProfessorCompleto(usuario, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public List<DisciplinaCursoVH> CarregarDisciplinasComCursosPorInstituicoesProfessor(ProfessorInstituicao entidade)
        {
            return disciplinaDAL.CarregarDisciplinasComCursosPorInstituicoesProfessor(entidade);
        }

        #endregion

        #region public Professor CarregarComDependencia(Professor professor)
        public Professor CarregarCompleto(Professor professor)
        {
            professor = professorDAL.Carregar(professor);
            //professor.Enderecos = enderecoDAL.CarregarEnderecosProfessor(professor);
            //professor.Telefones = telefoneDAL.CarregarTelefonesProfessor(professor);
            //professor.Perfis = perfilDAL.CarregarTodosPorProfessor(professor);
            return professor;
        }
        #endregion

        #region public Professor CarregarCompletoComAvaliacoes(Professor professor)
        public Professor CarregarCompletoComAvaliacoes(Professor professor)
        {
            professor = professorDAL.CarregarAvaliacaoComDependencias(professor);
            return professor;
        }
        #endregion

        #region public Professor CarregarProfessor(Professor professor)
        /// <summary>
        /// Carrega o usuário sem endereço e telefone
        /// </summary>
        /// <param name="professor">Deve ser passado um objeto Professor contendo o identificador ProfessorId</param>
        /// <returns>Usuário populado</returns>
        public Professor CarregarProfessor(Professor professor)
        {
            return professorDAL.Carregar(professor);
        }
        #endregion

        #region public Professor CarregarDocenciaProfessor(Professor professor)
        /// <summary>
        /// Carrega os dados de docência do Professor através do código identificador do Usuário
        /// </summary>
        /// <param name="professor">Objeto Usuário que contém o identificador professorId</param>
        /// <returns>Objeto Professor com seus dados populados (incluindo Docências e Graduação).</returns>
        public Professor CarregarDocenciaProfessor(Professor professor)
        {
            return this.CarregarDocenciaProfessor(professor, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="professor"></param>
        /// <param name="professorInstituicaoIdsRemovidos"></param>
        /// <returns></returns>
        public Professor CarregarDocenciaProfessor(Professor professor, String professorInstituicaoIdsRemovidos)
        {
            Professor professorPopulado = professorDAL.Carregar(professor);
            if (professorPopulado != null)
            {
                professor = professorPopulado;
                professor.ProfessorInstituicoes = professorInstituicaoDAL.CarregarComDependenciasPorProfessor(professor, professorInstituicaoIdsRemovidos).ToList();
            }
            return professor;
        }
        #endregion

        public Professor CarregarDocenciaProfessorPorProfessorInstituicaoIds(Professor professor, String professorInstituicaoIdsRemovidos)
        {
            Professor professorPopulado = professorDAL.Carregar(professor);
            if (professorPopulado != null)
            {
                professor = professorPopulado;
                professor.ProfessorInstituicoes = professorInstituicaoDAL.CarregarComDependenciasPorProfessor(professor, professorInstituicaoIdsRemovidos).ToList();
            }
            return professor;
        }

        #region public void AtualizaStatus(Professor professor, bool ativo )
        /// <summary>
        /// Atualiza somente o status do usuário
        /// </summary>
        /// <param name="professor">Usuário a ser atualizado o status</param>
        /// <param name="ativo">Status</param>
        public void AtualizaStatus(Professor professor, bool ativo)
        {
            professor = professorDAL.Carregar(professor);
            //professor.Ativo = ativo;
            professorDAL.Atualizar(professor);
        }
        #endregion

        //***************************************
        public List<Instituicao> CarregarInstituicoes()
        {
            //string[] teste = new string["nomeInstituicao",""]();
            return instituicaoDAL.CarregarTodos(0, 0, new string[] { "nomeInstituicao" }, new string[] { "ASC" }, null).ToList();
        }

        public void InserirProfessorInstituicao(ProfessorInstituicao professorInstituicao)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                telefoneDAL.Inserir(professorInstituicao.Telefone);
                professorInstituicaoDAL.Inserir(professorInstituicao);
                scope.Complete();
            }
        }

        public List<Curso> CarregarCursos()
        {
            return cursoDAL.CarregarTodos().ToList();
        }

        public List<CursoNivel> CarregarCursoNivel()
        {
            return cursoNivelDAL.CarregarTodos().ToList();
        }

        public void InserirProfessorCurso(ProfessorCurso professorCurso)
        {
            professorCursoDAL.Inserir(professorCurso);
        }

        public void InserirProfessorDisciplina(ProfessorDisciplina professorDisciplina)
        {
            professorDisciplinaDAL.Inserir(professorDisciplina);
        }

        public List<Instituicao> CarregarInstituicoesExcetoCadastradosPorProfessor(Professor professor)
        {
            return instituicaoDAL.CarregarExcetoCadastradosPorProfessor(professor).ToList();
        }

        public List<Instituicao> CarregarInstituicoesPorProfessor(Professor professor)
        {
            return instituicaoDAL.CarregarPorProfessor(professor).ToList();
        }

        public List<Disciplina> CarregarDisciplinas()
        {
            return disciplinaDAL.CarregarTodos().ToList();
        }

        public List<CursoNivel> CarregarNiveisCurso()
        {
            return cursoNivelDAL.CarregarTodos().ToList();
        }
        //**************************************
        #endregion

        /// <summary>
        /// Insere um novo arquivo 
        /// </summary>
        /// <param name="arquivo"></param>
        public void InserirArquivo(Arquivo arquivo)
        {
            arquivoDAL.Inserir(arquivo);
        }

        /// <summary>
        /// Método que insere a docência completa (instituições, cursos e disciplinas) refentes ao professor
        /// </summary>
        /// <param name="professor">Objeto Professor que contém as informações de docência a serem inseridas</param>
        public void InserirProfessorComDocencias(Professor professor)
        {
            if (professor.ProfessorInstituicoes != null)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    professorDAL.Inserir(professor);
                    foreach (ProfessorInstituicao professorInstituicao in professor.ProfessorInstituicoes)
                    {
                        // Feito provisoriamente para evitar erro de FK
                        professorInstituicao.Professor = professor;
                        professorInstituicao.Telefone = null;
                        //telefoneDAL.Inserir(professorInstituicao.Telefone);
                        professorInstituicaoDAL.Inserir(professorInstituicao);
                        foreach (ProfessorCurso professorCurso in professorInstituicao.ProfessorCursos)
                        {
                            professorCursoDAL.Inserir(professorCurso);
                            foreach (ProfessorDisciplina professorDisciplina in professorCurso.ProfessorDisciplinas)
                            {
                                professorDisciplinaDAL.Inserir(professorDisciplina);
                            }
                        }
                    }
                    foreach (ProfessorComprovanteDocencia professorComprovanteDocencia in professor.ProfessorComprovanteDocencias)
                    {
                        professorComprovanteDocencia.Professor = professor;
                        professorComprovanteDocenciaDAL.Inserir(professorComprovanteDocencia);
                    }
                    scope.Complete();
                }
            }
        }

        public void AtualizarProfessor(Professor professor)
        {
            professorDAL.Atualizar(professor);
        }

        /// <summary>
        /// Insere uma nova docência
        /// </summary>
        /// <param name="professorBO">Dados do prefessor que deve conter a instituição, curso e disciplina</param>
        /// <param name="deveInserirInstituicao">Deve receber "True" se a instituição é nova</param>
        /// <param name="deveInserirCurso">Deve receber "True" se o curso é novo</param>
        /// <param name="deveInserirComprovante">Deve receber "True" se a disciplina é nova</param>
        /// <returns></returns>
        public Professor InserirDocenciaPorProfessor(Professor professorBO, ProfessorInstituicao professorInstituicaoBO, bool deveInserirInstituicao, bool deveInserirCurso, bool deveInserirComprovante)
        {
            //bool deveInserirInstituicao = false;
            //bool deveInserirCurso = false;
            //bool deveInserirComprovante = false;

            using (TransactionScope scope = new TransactionScope())
            {
                if (professorADO.Carregar(new Professor() { ProfessorId = professorBO.Usuario.UsuarioId }) == null)
                {
                    professorBO.ProfessorId = professorBO.Usuario.UsuarioId;
                    professorBO.GraduacaoProfessor = new GraduacaoProfessor(3);
                    professorBO.AutorGrupoa = false;
                    professorBO.ColaboradorGrupoa = false;
                    professorBO.PossuiPublicacao = false;

                    professorADO.Inserir(professorBO);
                }

                //if (professorInstituicaoDAL.ValidarProfessorInstituicaoUnico(professorBO, professorInstituicaoBO.Instituicao))
                if (deveInserirInstituicao)
                {
                    if (professorInstituicaoBO.Telefone != null)
                    {
                        telefoneDAL.Inserir(professorInstituicaoBO.Telefone);
                    }

                    professorInstituicaoDAL.Inserir(professorInstituicaoBO);
                }

                if (deveInserirCurso)
                {
                    professorCursoDAL.Inserir(professorInstituicaoBO.ProfessorCursos.Last());
                }

                professorDisciplinaDAL.Inserir(professorInstituicaoBO.ProfessorCursos.Last().ProfessorDisciplinas.Last());

                if (deveInserirComprovante && professorBO.ProfessorComprovanteDocencias.Any())
                {
                    ProfessorComprovanteDocencia professorComprovanteDocencia = professorBO.ProfessorComprovanteDocencias.Last();
                    professorComprovanteDocencia.Professor = professorBO;
                    professorComprovanteDocenciaDAL.Inserir(professorComprovanteDocencia);
                }

                scope.Complete();
            }

            return professorBO;
        }

        /// <summary>
        /// Insere um novo registro de professor
        /// </summary>
        /// <param name="professor"></param>
        public void InserirProfessor(Professor professor)
        {
            professorADO.Inserir(professor);
        }

        public void RemoverDisciplinaCursoPorProfessor(Professor professor, int numeroCursos, int numeroDisciplinas)
        {
            if (professor != null)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    professorDisciplinaDAL.Excluir(professor.ProfessorInstituicoes.First().ProfessorCursos.First().ProfessorDisciplinas.First());
                    // Se o número de disciplinas for = 1, deve excluir também o curso
                    if (numeroDisciplinas == 1)
                    {
                        professorCursoDAL.Excluir(professor.ProfessorInstituicoes.First().ProfessorCursos.First());
                        // Se o número de cursos for = 1, deve excluir também a instituição e comprovante de docencia
                        if (numeroCursos == 1)
                        {
                            ProfessorInstituicao professorInstituicao = professor.ProfessorInstituicoes.First();
                            professorInstituicao = professorInstituicaoDAL.Carregar(professorInstituicao);
                            //excluir o comprovante de docencia
                            professorComprovanteDocenciaDAL.ExcluirPorProfessorEInstituicao(professor.ProfessorId, professorInstituicao.Instituicao.InstituicaoId);
                            //excluir instituicao
                            professorInstituicaoDAL.Excluir(professorInstituicao);
                            //excluir telefone da instituicao
                            telefoneDAL.ExcluirPorProfessorInstituicao(professorInstituicao);
                        }
                    }
                    scope.Complete();
                }
            }
        }

        public void RemoverInstituicaoCompletaPorProfessor(Professor professor, String professorInstituicoesIdsDeletar)
        {
            if (professor != null)
            {
                if (professorInstituicoesIdsDeletar == "T")
                {
                    professor = this.CarregarDocenciaProfessor(new Professor() { ProfessorId = professor.ProfessorId });
                }
                else
                {
                    //Buscar professorInstituicoesIdsDeletar
                    professor.ProfessorInstituicoes = professorInstituicaoDAL.CarregarComDependenciasPorProfessorInstituicoesIds(professor, professorInstituicoesIdsDeletar).ToList();
                }

                foreach (ProfessorInstituicao professorInstituicaoAux in professor.ProfessorInstituicoes)
                {
                    ProfessorInstituicao professorInstituicao = professorInstituicaoDAL.Carregar(professorInstituicaoAux);
                    Telefone tel = telefoneDAL.CarregarTelefonePorProfessorInstituicao(professorInstituicao);

                    using (TransactionScope scope = new TransactionScope())
                    {
                        // Exclui as disciplinas da Instituição
                        professorDisciplinaDAL.ExcluirPorProfessorInstituicao(professorInstituicao);
                        // Exclui os Cursos
                        professorCursoDAL.ExcluirPorProfessorInstituicao(professorInstituicao);
                        //excluir o comprovante de docencia
                        professorComprovanteDocenciaDAL.ExcluirPorProfessorEInstituicao(professor.ProfessorId, professorInstituicao.Instituicao.InstituicaoId);
                        // Exclui Instituição
                        professorInstituicaoDAL.Excluir(professorInstituicao);
                        // Exclui o telefone da Instituição
                        if (tel != null)
                        {
                            telefoneDAL.Excluir(tel);
                        }
                        scope.Complete();
                    }
                }
            }
        }
    }
}
