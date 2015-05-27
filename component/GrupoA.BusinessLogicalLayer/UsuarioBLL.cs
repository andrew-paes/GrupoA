using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using GrupoA.BusinessLogicalLayer.Helper;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;

namespace GrupoA.BusinessLogicalLayer
{
    /// <summary>
    /// Classe que abstrai as regras de negócio referentes a usuários.
    /// </summary>    
    public class UsuarioBLL : BaseBLL
    {
        #region Propriedades

        private IUsuarioDAL _usuarioDal;
        private IUsuarioDAL UsuarioDal
        {
            get { return _usuarioDal ?? (_usuarioDal = new UsuarioADO()); }
        }

        private IUsuarioControleDAL _usuarioControleDal;
        private IUsuarioControleDAL UsuarioControleDal
        {
            get { return _usuarioControleDal ?? (_usuarioControleDal = new UsuarioControleADO()); }
        }

        private IPerfilDAL _perfilDal;
        private IPerfilDAL PerfilDal
        {
            get { return _perfilDal ?? (_perfilDal = new PerfilADO()); }
        }

        private IEnderecoDAL _enderecoDal;
        private IEnderecoDAL EnderecoDal
        {
            get { return _enderecoDal ?? (_enderecoDal = new EnderecoADO()); }
        }

        private ITelefoneDAL _telefoneDal;
        private ITelefoneDAL TelefoneDal
        {
            get { return _telefoneDal ?? (_telefoneDal = new TelefoneADO()); }
        }

        private IProfessorDAL _professorDal;
        private IProfessorDAL ProfessorDal
        {
            get { return _professorDal ?? (_professorDal = new ProfessorADO()); }
        }

        private IGraduacaoProfessorDAL _graduacaoProfessorDal;
        private IGraduacaoProfessorDAL GraduacaoProfessorDal
        {
            get { return _graduacaoProfessorDal ?? (_graduacaoProfessorDal = new GraduacaoProfessorADO()); }
        }

        private IProfissionalOcupacaoDAL _profissionalOcupacaoDal;
        private IProfissionalOcupacaoDAL ProfissionalOcupacaoDal
        {
            get { return _profissionalOcupacaoDal ?? (_profissionalOcupacaoDal = new ProfissionalOcupacaoADO()); }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Carrega o usuario com endereços, perfis, telefones, professor e docência.
        /// </summary>
        /// <param name="usuario">Objeto usuario que deverá conter o código identificador UsuarioId a ser pesquisado</param>
        /// <returns>Objeto usuario populado</returns>
        public Usuario CarregarComDependenciasEDocencias(Usuario usuario)
        {
            usuario = this.CarregarComDependencia(usuario);
            if ((usuario.Professor != null) && (usuario.Professor.ProfessorId > 0))
            {
                usuario.Professor = new ProfessorBLL().CarregarDocenciaProfessor(usuario.Professor);
            }
            return usuario;
        }

        /// <summary>
        /// Carrega o usuário com seus endereços e seus telefones
        /// </summary>
        /// <param name="usuario">Deve ser passado um objeto Usuario contendo o identificador UsuarioId</param>
        /// <returns>Usuário populado</returns>
        public Usuario CarregarComDependencia(Usuario usuario)
        {
            usuario = UsuarioDal.Carregar(usuario);
            usuario.Enderecos = EnderecoDal.CarregarEnderecosUsuario(usuario);
            usuario.Telefones = TelefoneDal.CarregarTelefonesUsuario(usuario);
            usuario.Perfis = PerfilDal.CarregarPerfisDoUsuario(usuario);
            usuario.Professor = ProfessorDal.Carregar(new Professor() { ProfessorId = usuario.UsuarioId });
            return usuario;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tituloAvaliacaoId"></param>
        /// <returns></returns>
        public int CarregarIdUsuarioByAvaliacao(int tituloAvaliacaoId)
        {
            int usuarioId = 0;
            usuarioId = UsuarioDal.CarregarIdUsuarioByAvaliacao(tituloAvaliacaoId);

            return usuarioId;
        }

        /// <summary>
        /// Carrega o usuário sem endereço e telefone
        /// </summary>
        /// <param name="usuario">Deve ser passado um objeto Usuario contendo o identificador UsuarioId</param>
        /// <returns>Usuário populado</returns>
        public Usuario CarregarUsuario(Usuario usuario)
        {
            return UsuarioDal.Carregar(usuario);
        }

        /// <summary>
        /// Carrega o usuário por CPF ou CNPJ
        /// </summary>
        /// <param name="usuario">Deve ser passado um objeto Usuario</param>
        /// <returns>Usuário populado</returns>
        public Usuario CarregarUsuarioEsqueciMinhaSenha(Usuario usuario, String chave)
        {
            return UsuarioDal.CarregarUsuarioEsqueciMinhaSenha(usuario, chave);
        }

        /// <summary>
        /// Carrega os dados do Professor através do código identificador do Usuário
        /// </summary>
        /// <param name="usuario">Objeto Usuário que contém o identificador usuarioId</param>
        /// <returns>Objeto Professor com seus dados populados (incluindo Docências e Graduação).</returns>
        public Professor CarregarProfessorCompleto(Usuario usuario)
        {
            return new ProfessorBLL().CarregarProfessorCompleto(usuario, false);
        }

        public Professor CarregarProfessorCompleto(Usuario usuario, bool docencia)
        {
            return new ProfessorBLL().CarregarProfessorCompleto(usuario, docencia);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public ProfissionalOcupacao CarregarOcupacao(Usuario usuario)
        {
            throw new Exception("Profissional não existe mais!");

            //ProfissionalOcupacao profissionalOcupacao = new ProfissionalOcupacao();

            //Profissional profissional = new Profissional();
            //profissional.ProfissionalId = usuario.UsuarioId;
            //profissional = profissionalDAL.CarregarAutor(profissional);

            //if (profissional != null)
            //{
            //    profissionalOcupacao.ProfissionalOcupacaoId = profissional.ProfissionalOcupacao.ProfissionalOcupacaoId;
            //    profissionalOcupacao = ProfissionalOcupacaoDal.CarregarAutor(profissionalOcupacao);
            //}

            //return profissionalOcupacao;
            return null;
        }

        /// <summary>
        /// Atualiza somente o status do usuário
        /// </summary>
        /// <param name="usuario">Usuário a ser atualizado o status</param>
        /// <param name="ativo">Status</param>
        public void AtualizaStatus(Usuario usuario, bool ativo, String chave)
        {
            usuario = UsuarioDal.Carregar(usuario);
            usuario.Ativo = ativo;
            UsuarioDal.AtualizarStatus(usuario);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="chave"></param>
        /// <param name="caminhoTemplateProfessorRecebido"></param>
        /// <param name="perfilIdAnterior"></param>
        public void Atualizar(Usuario usuario, String chave, String caminhoTemplateProfessorRecebido, Int32 perfilIdAnterior)
        {
            this.Atualizar(usuario, chave, caminhoTemplateProfessorRecebido, perfilIdAnterior, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="chave"></param>
        /// <param name="caminhoTemplateProfessorRecebido"></param>
        /// <param name="perfilIdAnterior"></param>
        /// <param name="deletarHistoricoProfessor"></param>
        public void Atualizar(Usuario usuario, String chave, String caminhoTemplateProfessorRecebido, Int32 perfilIdAnterior, Boolean deletarHistoricoProfessor)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //if (deletarHistoricoProfessor)
                //{
                //    Professor professor = new ProfessorBLL().CarregarProfessorCompleto(usuario);

                //    if (professor != null)
                //    {

                //    }
                //}

                UsuarioDal.Atualizar(usuario, chave);

                UsuarioControle usuarioControle = new UsuarioControle();
                usuarioControle.Usuario = usuario;
                usuarioControle.UsuarioId = usuario.UsuarioId;
                usuarioControle.RealizarSincronizacao = true;

                if (UsuarioControleDal.Carregar(usuarioControle) != null)
                {
                    UsuarioControleDal.AtualizarStatusSincronizacao(usuarioControle);
                }
                else
                {
                    UsuarioControleDal.Inserir(usuarioControle);
                }

                // Exclui os enderecos antigos
                this.ExcluiEnderecoPorUsuarioId(usuario.UsuarioId);
                // Insere os novos endereços
                this.InserirEnderecosUsuario(usuario);

                // Exclui os telefones antigos
                this.ExcluiTelefonePorUsuarioId(usuario.UsuarioId);
                // Insere os novos telefones
                this.InserirTelefonesUsuario(usuario);

                // Exclui perfis anteriores
                this.ExcluiUsuarioPerfil(usuario.UsuarioId);
                // Insere novo perfil
                this.InserePerfisUsuario(usuario);

                // Professor
                if (usuario.Professor != null)
                {
                    if (new ProfessorBLL().CarregarProfessor(usuario.Professor) == null)
                    {
                        new ProfessorBLL().InserirProfessor(usuario.Professor);

                        this.EnviarEmailProfessor(usuario.EmailUsuario, "Cadastro Recebido — Grupo A", caminhoTemplateProfessorRecebido, null);
                    }
                    else
                    {
                        if (perfilIdAnterior == 1)
                        {
                            this.EnviarEmailProfessor(usuario.EmailUsuario, "Cadastro Recebido — Grupo A", caminhoTemplateProfessorRecebido, null);
                        }

                        new ProfessorBLL().AtualizarProfessor(usuario.Professor);
                    }
                }

                // Áreas de Interesse
                this.ExcluiAreaInteressePorUsuarioId(usuario.UsuarioId);
                // Inserir novas Áreas de Interesse
                this.InsereUsuarioCategorias(usuario);

                scope.Complete();
            }
        }

        public void InserirEnderecosUsuario(Usuario usuario)
        {
            if (usuario.Enderecos != null)
            {
                foreach (Endereco endereco in usuario.Enderecos)
                {
                    endereco.Usuario = usuario;
                    this.InsereEndereco(endereco);
                }
            }
        }

        public void InserirTelefonesUsuario(Usuario usuario)
        {
            if (usuario.Telefones != null)
            {
                foreach (Telefone telefone in usuario.Telefones)
                {
                    telefone.Usuario = usuario;
                    this.InsereTelefone(telefone);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="chave"></param>
        /// <param name="caminhoTemplateProfessorRecebido"></param>
        public void Insere(Usuario usuario, String chave, String caminhoTemplateProfessorRecebido)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                UsuarioDal.Inserir(usuario, chave);
                UsuarioControle usuarioControle = new UsuarioControle();
                usuarioControle.Usuario = usuario;
                usuarioControle.RealizarSincronizacao = true;
                usuarioControle.UsuarioId = usuario.UsuarioId;
                UsuarioControleDal.InserirOuAtualizar(usuarioControle);
                // Insere enderecos 
                this.InserirEnderecosUsuario(usuario);
                // Insere os perfis
                this.InserePerfisUsuario(usuario);
                // Professor
                if (usuario.Professor != null)
                {
                    usuario.Professor.ProfessorId = usuario.UsuarioId;
                    new ProfessorBLL().InserirProfessorComDocencias(usuario.Professor);

                    this.EnviarEmailProfessor(usuario.EmailUsuario, "Cadastro Recebido — Grupo A", caminhoTemplateProfessorRecebido, null);
                }
                // Inserir novas Áreas de Interesse
                this.InsereUsuarioCategorias(usuario);
                scope.Complete();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailProfessor"></param>
        /// <param name="assunto"></param>
        /// <param name="caminhotemplate"></param>
        private void EnviarEmailProfessor(String emailProfessor, String assunto, String caminhotemplateRecebido, String caminhotemplateConfirmado)
        {
            Dictionary<String, String> dicionarioDados = new Dictionary<String, String>();
            StringBuilder templateEmail = new StringBuilder();

            if (!String.IsNullOrEmpty(caminhotemplateRecebido))
            {
                templateEmail = new EmailHelper().PopulaTemplateEmail(dicionarioDados, caminhotemplateRecebido);
            }
            else
            {
                templateEmail = new EmailHelper().PopulaTemplateEmail(dicionarioDados, caminhotemplateConfirmado);
            }

            new EmailHelper().EnviarEmail(GrupoA.GlobalResources.GrupoA_Resource.EmailSAC, GrupoA.GlobalResources.GrupoA_Resource.EmailDivulgacao,
                                          emailProfessor,
                                          assunto,
                                          templateEmail);
        }

        public void InsereEndereco(Endereco entidade)
        {
            EnderecoDal.Inserir(entidade);

            Usuario usuario = this.CarregarComDependencia(entidade.Usuario);
            UsuarioControle usuarioControle = new UsuarioControle();
            usuarioControle.Usuario = usuario;
            usuarioControle.RealizarSincronizacao = true;
            usuarioControle.UsuarioId = usuario.UsuarioId;

            UsuarioControleDal.InserirOuAtualizar(usuarioControle);
        }

        public void AtualizarEndereco(Endereco endereco)
        {
            EnderecoDal.Atualizar(endereco);
            Usuario usuario = this.CarregarComDependencia(endereco.Usuario);
            UsuarioControle usuarioControle = new UsuarioControle();
            usuarioControle.Usuario = usuario;
            usuarioControle.RealizarSincronizacao = true;
            usuarioControle.UsuarioId = usuario.UsuarioId;

            UsuarioControleDal.InserirOuAtualizar(usuarioControle);

        }

        public void InsereUsuarioInteresse(int usuarioId, int categoriaId)
        {
            UsuarioDal.InsereUsuarioInteresse(usuarioId, categoriaId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        public void InserePerfisUsuario(Usuario usuario)
        {
            this.InserePerfisUsuario(usuario, true, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="PerfilAnteriorDeProfessor"></param>
        public void InserePerfisUsuario(Usuario usuario, Boolean PerfilAnteriorDeProfessor, String caminhoTemplateProfessorConfirmado)
        {
            if (usuario.Perfis != null)
            {
                foreach (Perfil perfil in usuario.Perfis)
                {
                    UsuarioDal.InsereUsuarioPerfil(usuario.UsuarioId, perfil.PerfilId);

                    if (!PerfilAnteriorDeProfessor && perfil.PerfilId == 2)
                    {
                        this.EnviarEmailProfessor(usuario.EmailUsuario, "Cadastro Confirmado — Grupo A", null, caminhoTemplateProfessorConfirmado);
                    }
                }
            }
        }

        public void InsereTelefone(Telefone entidade)
        {
            TelefoneDal.Inserir(entidade);
        }

        public void InsereUsuarioCategorias(Usuario usuario)
        {
            if (usuario.Categorias != null)
            {
                foreach (Categoria categoria in usuario.Categorias)
                {
                    this.InsereUsuarioInteresse(usuario.UsuarioId, categoria.CategoriaId);
                }
            }
        }

        public bool ValidarCadastroPessoaUnicoUsuario(Usuario usuario)
        {
            return UsuarioDal.ValidarCadastroPessoaUnico(usuario);
        }

        public bool ValidarEmailUnicoUsuario(Usuario usuario)
        {
            return UsuarioDal.ValidarEmailUnico(usuario);
        }

        /// <summary>Valida email e senha do usuário</summary>
        /// <param name="cadastroPessoa">CadastroPessoa do usuário</param>
        /// <param name="senha">Senha não criptografada do usuário</param>
        public Usuario AutenticaUsuario(String identificador, String senha, String chave)
        {
            Usuario busca = new Usuario();
            busca.CadastroPessoa = identificador;
            busca.Senha = senha;
            Usuario usuario = UsuarioDal.LoginUsuario(busca, chave);

            return usuario;
        }

        public void ExcluiUsuarioPerfil(int usuarioId)
        {
            UsuarioDal.ExcluiUsuarioPerfil(usuarioId);
        }

        public void ExcluiAreaInteressePorUsuarioId(int usuarioId)
        {
            UsuarioDal.ExcluiAreaInteressePorUsuarioId(usuarioId);
        }

        public int ExcluiEnderecoPorUsuarioId(int usuarioId)
        {
            return EnderecoDal.ExcluiEnderecoPorUsuarioId(usuarioId);
        }

        public int ExcluiTelefonePorUsuarioId(int usuarioId)
        {
            return TelefoneDal.ExcluiTelefonePorUsuarioId(usuarioId);
        }

        public List<ProfissionalOcupacao> CarregarProfissionalOcupacao()
        {
            return ProfissionalOcupacaoDal.CarregarTodos().OrderBy(po => po.Ocupacao).ToList();
        }

        public List<Categoria> CarregarUsuarioInteresse(int usuarioId)
        {
            return UsuarioDal.CarregarUsuarioInteresse(usuarioId).ToList();
        }

        /// <summary>
        /// Carrega todos os usuários que tem sincronização pendente (UsuarioControle.realizarSincronizacao = 1).
        /// </summary>
        /// <returns>Listagem de Usuarios contendo Endereços, Tipos de Endereço, Telefones, Tipos de Telefone e Controle do Usuário</returns>
        public List<Usuario> CarregarUsuariosComSincronizacaoPendente()
        {
            return UsuarioDal.CarregarUsuariosComSincronizacaoPendente();
        }

        /// <summary>
        /// Atualiza os dados de controle recebidos. ATENÇÃO: Somente serão atualizadas as informações recebidas.
        /// </summary>
        /// <param name="usuario">usuario que contém as informações a serem atualizadosos dados de controle</param>
        public void AtualizarDadosControle(Usuario usuario)
        {
            UsuarioControleDal.AtualizarSomenteCamposRecebidos(usuario.UsuarioControle);
        }

        /// <summary>
        /// Método que persiste somente Usuario.
        /// </summary>
        /// <param name="entidade"></param>
        /// <param name="chave"></param>
        public void Inserir(Usuario entidade, String chave)
        {
            UsuarioDal.Inserir(entidade, chave);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public Usuario CarregarPorCadastroPessoa(Usuario entidade)
        {
            return UsuarioDal.CarregarPorCadastroPessoa(entidade);
        }

        public void AtualizarUsuario(Usuario usuario, String chave)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                UsuarioControle usuarioControle = new UsuarioControle();
                usuarioControle.Usuario = usuario;
                usuarioControle.UsuarioId = usuario.UsuarioId;
                usuarioControle.RealizarSincronizacao = true;

                if (UsuarioControleDal.Carregar(usuarioControle) != null)
                {
                    UsuarioControleDal.AtualizarStatusSincronizacao(usuarioControle);
                }
                else
                {
                    UsuarioControleDal.Inserir(usuarioControle);
                }

                UsuarioDal.Atualizar(usuario, chave);

                scope.Complete();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public Usuario CarregarPorCadastroPessoaEmail(Usuario entidade)
        {
            return UsuarioDal.CarregarPorCadastroPessoaEmail(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public List<Categoria> CarregarUsuarioAreaInteresse(Usuario entidade)
        {
            return UsuarioDal.CarregarUsuarioAreaInteresse(entidade).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cadastroPessoa"></param>
        /// <param name="nomeUsuario"></param>
        /// <returns></returns>
        public List<Usuario> CarregarUsuarioParaAssinatura(String cadastroPessoa, String nomeUsuario)
        {
            return UsuarioDal.CarregarUsuarioParaAssinatura(cadastroPessoa, nomeUsuario);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public List<Usuario> Carregar(CompraConjunta entidade)
        {
            return UsuarioDal.Carregar(entidade);
        }

        #endregion
    }
}