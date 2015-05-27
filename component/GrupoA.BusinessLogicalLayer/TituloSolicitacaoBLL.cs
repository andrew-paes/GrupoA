using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;
using GrupoA.BusinessLogicalLayer.Helper;
using GrupoA.BusinessObject;
using GrupoA.BusinessObject.Enumerator;
using GrupoA.BusinessObject.ViewHelper;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;
using GrupoA.FilterHelper;

namespace GrupoA.BusinessLogicalLayer
{
    /// <summary>
    /// Classe que contém métodos de negócio para utilização de minhas solicitações.
    /// </summary>
    public class TituloSolicitacaoBLL : BaseBLL
    {
        #region Declarações DAL

        private ITituloSolicitacaoDAL _tituloSolicitacaoDal;
        private ITituloSolicitacaoDAL TituloSolicitacaoDal
        {
            get { return _tituloSolicitacaoDal ?? (_tituloSolicitacaoDal = new TituloSolicitacaoADO()); }
        }

        private ITituloSolicitacaoStatusDAL _tituloSolicitacaoStatusDal;
        private ITituloSolicitacaoStatusDAL TituloSolicitacaoStatusDal
        {
            get { return _tituloSolicitacaoStatusDal ?? (_tituloSolicitacaoStatusDal = new TituloSolicitacaoStatusADO()); }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Inserir um novo título solicitação
        /// </summary>
        /// <param name="tituloSolicitacao">Título solicitação a ser inserido</param>
        /// <param name="produto">Produto solicitado</param>
        public StatusSolicitacaoDeTitulo Inserir(TituloSolicitacao tituloSolicitacao, Produto produto, String caminhotemplateGrupoA, String caminhotemplateProfessor)
        {
            // Valida se o título selecionado existe
            TituloImpresso tituloImpresso = new TituloImpressoBLL().CarregarPorProduto(produto.ProdutoId);
            if (tituloImpresso == null)
            {
                // Retorna erro, título inexistente
                return StatusSolicitacaoDeTitulo.TituloInexistente;
            }
            else
            {
                tituloImpresso.Titulo = new TituloBLL().Carregar(tituloImpresso.Titulo);
            }

            tituloSolicitacao.Titulo = new Titulo();
            tituloSolicitacao.Titulo = tituloImpresso.Titulo;

            // Valida se o título já foi solicitado pelo usuário
            if (TituloSolicitacaoDal.CarregarTituloSolicitacaoPorProfessorTitulo(tituloSolicitacao.Professor.ProfessorId, tituloSolicitacao.Titulo.TituloId) != null)
            {
                // Retorna erro, título já foi solicitado
                return StatusSolicitacaoDeTitulo.TituloJaSolicitado;
            }

            if (!this.ValidarSolicitacao(tituloSolicitacao.Professor.ProfessorId))
            {
                return StatusSolicitacaoDeTitulo.LimiteExcedido;
            }

            using (TransactionScope scope = new TransactionScope())
            {
                // Inserção de título solicitação
                TituloSolicitacaoDal.Inserir(tituloSolicitacao);

                // Enviar email para Grupo A
                this.EnviarEmailAvisoSolicitacaoGrupoA(tituloSolicitacao, tituloImpresso.Isbn13, caminhotemplateGrupoA);
                this.EnviarEmailAvisoSolicitacaoProfessor(tituloSolicitacao, caminhotemplateProfessor);

                scope.Complete();
            }

            return StatusSolicitacaoDeTitulo.SolicitacaoOk;
        }

        /// <summary>
        /// Inserir um novo título solicitação
        /// </summary>
        /// <param name="tituloSolicitacao">Título solicitação a ser inserido</param>
        /// <param name="produto">Produto solicitado</param>
        public void AtualizarStatus(TituloSolicitacao tituloSolicitacao, String caminhotemplateSim, String caminhotemplateNao)
        {
            // Atualização de título solicitação
            TituloSolicitacaoDal.AtualizarStatus(tituloSolicitacao);

            tituloSolicitacao = TituloSolicitacaoDal.Carregar(tituloSolicitacao);
            tituloSolicitacao.Professor.Usuario = new UsuarioBLL().CarregarUsuario(new Usuario(tituloSolicitacao.Professor.ProfessorId));
            tituloSolicitacao.Titulo = new TituloBLL().Carregar(tituloSolicitacao.Titulo);

            if (tituloSolicitacao.TituloSolicitacaoStatus.TituloSolicitacaoStatusId != 1 && tituloSolicitacao.TituloSolicitacaoStatus.TituloSolicitacaoStatusId != 4)
            {
                if (tituloSolicitacao.TituloSolicitacaoStatus.TituloSolicitacaoStatusId == 2)
                {
                    this.EnviarEmailAvisoLiberacaoSolicitacaoAprovado(tituloSolicitacao, caminhotemplateSim);
                }
                else
                {
                    this.EnviarEmailAvisoLiberacaoSolicitacaoCancelado(tituloSolicitacao, caminhotemplateNao);
                }
            }
        }

        /// <summary>
        /// Métodos para retorna a lista de títulos solicitados por um professor
        /// </summary>
        /// <param name="tituloSolicitacao"></param>
        /// <param name="registrosPagina">Número de registros por páginas</param>
        /// <param name="numeroPagina">Número da página atual</param>
        /// <param name="ordemColunas">Ordem das colunas</param>
        /// <param name="ordemSentidos">Ordem dos sentidos</param>
        /// <returns>Lista de títulos solicitados</returns>
        public IEnumerable<TituloSolicitacaoVH> CarregarTodosTitulosSolicitados(TituloSolicitacao tituloSolicitacao, int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos)
        {
            var tituloSolicitacaoFh = new TituloSolicitacaoFH() { ProfessorId = tituloSolicitacao.Professor.ProfessorId.ToString() };
            return TituloSolicitacaoDal.CarregarTodosCompleto(registrosPagina, numeroPagina, null, null, tituloSolicitacaoFh);
        }

        /// <summary>
        /// Métodos para retorna a contagem de títulos solicitados por um professor
        /// </summary>
        /// <param name="tituloSolicitacao"></param>
        /// <returns>Contagem de títulos solicitados</returns>
        public int ContarTodosCompleto(TituloSolicitacao tituloSolicitacao)
        {
            var tituloSolicitacaoFh = new TituloSolicitacaoFH() { ProfessorId = tituloSolicitacao.Professor.ProfessorId.ToString() };
            return TituloSolicitacaoDal.ContarTodosCompleto(tituloSolicitacaoFh);
        }

        public List<TituloSolicitacaoStatus> CarregarTodosTituloSolicitacaoStatusParaLiberacao()
        {
            return TituloSolicitacaoStatusDal.CarregarTodos().ToList();
        }

        public Boolean EnviarEmailAvisoSolicitacaoGrupoA(TituloSolicitacao tituloSolicitacao, String isbn13, String caminhotemplate)
        {
            try
            {
                Usuario usuario = tituloSolicitacao.Professor.Usuario;
                Endereco endereco = usuario.Enderecos[0];
                Professor professor = new ProfessorBLL().CarregarProfessorCompleto(usuario, true);
                List<Instituicao> instituicoes = new ProfessorBLL().CarregarInstituicoesPorProfessor(professor);

                List<StringBuilder> sbInformacoes = this.CarregarInstituicoes(usuario.UsuarioId);

                Dictionary<String, String> dicionarioDados = new Dictionary<String, String>();
                dicionarioDados.Add("NomeTitulo", tituloSolicitacao.Titulo.NomeTitulo);
                dicionarioDados.Add("NomeAutores", new TituloAvaliacaoBLL().CarregarAutores(tituloSolicitacao.Titulo));
                dicionarioDados.Add("NumeroEdicao", tituloSolicitacao.Titulo.Edicao != null ? String.Concat(tituloSolicitacao.Titulo.Edicao, " ª") : String.Empty);
                dicionarioDados.Add("Isbn", isbn13);
                dicionarioDados.Add("NomeProfessor", usuario.NomeUsuario);
                dicionarioDados.Add("Email", usuario.EmailUsuario);
                dicionarioDados.Add("Cpf", usuario.CadastroPessoa);
                dicionarioDados.Add("Graduacao", professor.GraduacaoProfessor.Graduacao);
                dicionarioDados.Add("Instituicoes", sbInformacoes[0].ToString());
                dicionarioDados.Add("Cursos", sbInformacoes[1].ToString());
                dicionarioDados.Add("Disciplinas", sbInformacoes[2].ToString());
                dicionarioDados.Add("NumeroAlunos", sbInformacoes[3].ToString());
                dicionarioDados.Add("Logradouro", endereco.Logradouro);
                dicionarioDados.Add("Numero", endereco.Numero);
                dicionarioDados.Add("Complemento", endereco.Complemento);
                dicionarioDados.Add("Bairro", endereco.Bairro);
                dicionarioDados.Add("Cep", endereco.Cep);
                dicionarioDados.Add("Municipio", endereco.Municipio.NomeMunicipio);
                dicionarioDados.Add("Estado", endereco.Municipio.Regiao.NomeRegiao);
                dicionarioDados.Add("CaminhoSite", ConfigurationManager.AppSettings["sitePath"].ToString());

                StringBuilder templateEmail = new EmailHelper().PopulaTemplateEmail(dicionarioDados, caminhotemplate);

                new EmailHelper().EnviarEmail(usuario.EmailUsuario, GrupoA.GlobalResources.GrupoA_Resource.EmailDivulgacao.ToString(), "Grupo A | Nova Solicitação de Título", templateEmail);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<StringBuilder> CarregarInstituicoes(Int32 professorId)
        {
            var professorInstituicoes = new ProfessorInstituicaoBLL().Carregar(new Professor() { ProfessorId = professorId });
            string strInstituicoes = string.Empty;
            List<StringBuilder> retorno = new List<StringBuilder>();
            StringBuilder sbInstituicoes = new StringBuilder();
            StringBuilder sbCursos = new StringBuilder();
            StringBuilder sbNumeroAlunos = new StringBuilder();
            StringBuilder sbDisciplinas = new StringBuilder();

            int x = 1;

            if (professorInstituicoes != null && professorInstituicoes.Count() > 0)
            {
                foreach (var professorInstituicao in professorInstituicoes)
                {
                    Instituicao instituicao = new InstituicaoBLL().Carregar(professorInstituicao.Instituicao);

                    sbInstituicoes.Append(instituicao.NomeInstituicao);

                    ProfessorCurso professorCurso = new ProfessorCursoBLL().Carregar(professorInstituicao).FirstOrDefault();

                    Curso curso = new CursoBLL().Carregar(professorCurso.Curso);

                    sbCursos.Append(curso.Nome);

                    ProfessorDisciplina professorDisciplina = new ProfessorDisciplinaBLL().Carregar(professorCurso).FirstOrDefault();

                    sbNumeroAlunos.Append(professorDisciplina.NumeroAlunos);

                    Disciplina disciplina = new DisciplinaBLL().Carregar(professorDisciplina.Disciplina);

                    sbDisciplinas.Append(disciplina.Descricao);

                    if (x < professorInstituicoes.Count())
                    {
                        sbInstituicoes.Append("; ");
                        sbCursos.Append("; ");
                        sbNumeroAlunos.Append("; ");
                        sbDisciplinas.Append("; ");
                    }
                    x++;
                }
            }

            strInstituicoes = professorInstituicoes.ToString();

            retorno.Add(sbInstituicoes);
            retorno.Add(sbCursos);
            retorno.Add(sbDisciplinas);
            retorno.Add(sbNumeroAlunos);

            return retorno;
        }

        public Boolean EnviarEmailAvisoSolicitacaoProfessor(TituloSolicitacao tituloSolicitacao, String caminhotemplate)
        {
            try
            {
                Usuario usuario = tituloSolicitacao.Professor.Usuario;

                Dictionary<String, String> dicionarioDados = new Dictionary<String, String>();
                dicionarioDados.Add("CaminhoSite", ConfigurationManager.AppSettings["sitePath"].ToString());

                StringBuilder templateEmail = new EmailHelper().PopulaTemplateEmail(dicionarioDados, caminhotemplate);

                new EmailHelper().EnviarEmail(GrupoA.GlobalResources.GrupoA_Resource.EmailDivulgacao.ToString(), usuario.EmailUsuario, "Grupo A | Solicitação de livro recebida", templateEmail);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean EnviarEmailAvisoLiberacaoSolicitacaoAprovado(TituloSolicitacao tituloSolicitacao, String caminhotemplate)
        {
            try
            {
                Usuario usuario = tituloSolicitacao.Professor.Usuario;
                usuario = new UsuarioBLL().CarregarComDependencia(usuario);

                Endereco endereco = (from e in usuario.Enderecos.Where(c => c.PreferencialParaEntrega)
                                     select e).FirstOrDefault();

                String endEntrega = String.Concat(endereco.Logradouro, ", ", endereco.Numero, ",");
                if (!String.IsNullOrEmpty(endereco.Complemento))
                {
                    endEntrega = String.Concat(endEntrega, endereco.Complemento, ", ");
                }
                endEntrega = String.Concat(endEntrega, endereco.Bairro, ", ", endereco.Municipio.NomeMunicipio, ", ", endereco.Municipio.Regiao.NomeRegiao);

                Dictionary<String, String> dicionarioDados = new Dictionary<String, String>();
                dicionarioDados.Add("NomeTitulo", tituloSolicitacao.Titulo.NomeTitulo);
                dicionarioDados.Add("Endereco", endEntrega);
                dicionarioDados.Add("CaminhoUrl", String.Concat(ConfigurationManager.AppSettings["CaminhoSite"].ToString(), "perfil-professor/minhas-solicitacoes.aspx"));
                dicionarioDados.Add("CaminhoSite", ConfigurationManager.AppSettings["CaminhoSite"].ToString());

                StringBuilder templateEmail = new EmailHelper().PopulaTemplateEmail(dicionarioDados, caminhotemplate);

                new EmailHelper().EnviarEmail(GrupoA.GlobalResources.GrupoA_Resource.EmailDivulgacao.ToString(), usuario.EmailUsuario, "Grupo A | Solicitação de livro aprovada", templateEmail);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean EnviarEmailAvisoLiberacaoSolicitacaoCancelado(TituloSolicitacao tituloSolicitacao, String caminhotemplate)
        {
            try
            {
                Usuario usuario = tituloSolicitacao.Professor.Usuario;

                Dictionary<String, String> dicionarioDados = new Dictionary<String, String>();
                dicionarioDados.Add("NomeTitulo", tituloSolicitacao.Titulo.NomeTitulo);
                dicionarioDados.Add("CaminhoSite", ConfigurationManager.AppSettings["CaminhoImagem"].ToString());

                StringBuilder templateEmail = new EmailHelper().PopulaTemplateEmail(dicionarioDados, caminhotemplate);

                new EmailHelper().EnviarEmail(GrupoA.GlobalResources.GrupoA_Resource.EmailDivulgacao.ToString(), usuario.EmailUsuario, "Grupo A | Livro indisponível", templateEmail);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private Boolean ValidarSolicitacao(Int32 professorId)
        {
            return (TituloSolicitacaoDal.TotalRegistrosPendentesPorProfessor(professorId) < 3);
        }

        public Int32 ContarAvaliacoesPendencias(Int32 professorId)
        {
            return TituloSolicitacaoDal.ContarAvaliacoesPendencias(professorId);
        }

        public TituloSolicitacao Carregar(TituloSolicitacao tituloSolicitacao)
        {
            return TituloSolicitacaoDal.Carregar(tituloSolicitacao);
        }

        #endregion
    }
}
