using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess.ADO
{
    public partial class UsuarioADO : ADOSuper, IUsuarioDAL
    {
        #region Métodos
        /// <summary>
        /// Método que carrega uma colecao de usuarios por cadastro pessoa (CPF ou CNPJ) com exceção da listagem de usuários
        /// recebida por parâmetro.
        /// </summary>
        /// <param name="usuario">Objeto Usuario que contém o campo tipo pessoa a ser pesquisado.</param>
        /// <param name="usuarios">Listagem de Usuários (somente o identificador é necessário) que não deverão ser buscados.</param>
        /// <returns>Coleção de Usuários conforme o campo cadastro pessoa</returns>
        public List<Usuario> CarregarTodosPorCadastroPessoaUsuariosExcetoUsuarios(Usuario usuario, List<Usuario> usuarios)
        {
            List<Usuario> entidadeRetorno = new List<Usuario>();
            String ids = "";

            foreach (Usuario _usuario in usuarios)
            {
                ids += string.Concat(",", _usuario.UsuarioId);
            }

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(string.Concat("SELECT * FROM Usuario WHERE usuarioId NOT IN (0", ids, ") AND cadastroPessoa like '%", usuario.CadastroPessoa, "%'"));

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            //_db.AddInParameter(command, "@Ids", DbType.Int64, ids);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Usuario _usuario = new Usuario();
                PopulaUsuario(reader, _usuario);

                entidadeRetorno.Add(_usuario);
            }

            reader.Close();

            return entidadeRetorno;
        }

        public List<Usuario> CarregarUsuariosPorPromocao(Promocao promocao)
        {
            List<Usuario> entidadeRetorno = new List<Usuario>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" SELECT u.* FROM PromocaoUsuario pu ");
            sbSQL.Append(" INNER JOIN Usuario u ON u.usuarioId = pu.usuarioId ");
            sbSQL.Append(" WHERE pu.promocaoId = @promocaoId ");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@promocaoId", DbType.Int32, promocao.PromocaoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Usuario _usuario = new Usuario();
                PopulaUsuario(reader, _usuario);
                entidadeRetorno.Add(_usuario);
            }

            reader.Close();

            return entidadeRetorno;
        }

        public List<Categoria> CarregarUsuarioInteresse(int usuarioId)
        {
            List<Categoria> entidadeRetorno = new List<Categoria>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" SELECT c.categoriaId, c.nomeCategoria FROM UsuarioInteresse ui ");
            sbSQL.Append(" INNER JOIN Categoria c ");
            sbSQL.Append(" ON ui.categoriaId = c.categoriaId ");
            sbSQL.Append(" WHERE ui.usuarioId = @usuarioId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuarioId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Categoria categoria = new Categoria();
                PopulaCategoriaInteresse(reader, categoria);
                entidadeRetorno.Add(categoria);
            }
            reader.Close();

            return entidadeRetorno;
        }

        public int ValidaUsuarioPorEmailCadastroPessoa(string emailUsuario, string cadastroPessoa, int usuarioId)
        {
            int resultado = 0;
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("select count(*) ");
            sbSQL.Append("from Usuario ");
            sbSQL.Append("WHERE ( Usuario.EmailUsuario = @emailUsuario OR CadastroPessoa = @cadastroPessoa ) ");

            if (usuarioId != 0)
            {
                sbSQL.Append("AND Usuario.UsuarioId != @usuarioId");
            }

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@emailUsuario", DbType.String, emailUsuario);
            _db.AddInParameter(command, "@cadastroPessoa", DbType.String, cadastroPessoa);
            _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuarioId);

            resultado = (int)_db.ExecuteScalar(command);
            return resultado;
        }

        public int CarregarIdUsuarioByAvaliacao(int tituloAvaliacaoId)
        {
            int idUsuario = 0;
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("select Usuario.usuarioId ");
            sbSQL.Append("from Usuario ");
            sbSQL.Append("INNER JOIN Professor          ON Usuario.usuarioId=Professor.professorId ");
            sbSQL.Append("INNER JOIN TituloSolicitacao  ON Professor.professorId=TituloSolicitacao.professorId ");
            sbSQL.Append("INNER JOIN TituloAvaliacao    ON TituloSolicitacao.tituloSolicitacaoId=TituloAvaliacao.tituloSolicitacaoId ");
            sbSQL.Append("WHERE TituloAvaliacao.tituloAvaliacaoId = @tituloAvaliacaoId ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloAvaliacaoId", DbType.Int32, tituloAvaliacaoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                idUsuario = Convert.ToInt32(reader["usuarioId"].ToString());

            }
            reader.Close();

            return idUsuario;

        }

        public void ExcluiUsuarioPerfil(int usuarioId)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM UsuarioPerfil ");
            sbSQL.Append("WHERE usuarioId=@usuarioId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuarioId);

            _db.ExecuteNonQuery(command);

        }

        public void ExcluiAreaInteressePorUsuarioId(int usuarioId)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM UsuarioInteresse ");
            sbSQL.Append("WHERE usuarioId=@usuarioId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuarioId);

            _db.ExecuteNonQuery(command);

        }

        public void InsereUsuarioInteresse(int usuarioId, int categoriaId)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO UsuarioInteresse ");
            sbSQL.Append(" (usuarioId, categoriaId) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@usuarioId, @categoriaId) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuarioId);
            _db.AddInParameter(command, "@categoriaId", DbType.Int32, categoriaId);

            // Executa a query.
            _db.ExecuteNonQuery(command);


        }

        public void InsereUsuarioPerfil(int usuarioId, int perfilId)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO UsuarioPerfil ");
            sbSQL.Append(" (usuarioId, perfilId) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@usuarioId, @perfilId) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuarioId);
            _db.AddInParameter(command, "@perfilId", DbType.Int32, perfilId);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        public static void PopulaCategoriaInteresse(IDataReader reader, Categoria entidade)
        {

            if (reader["categoriaId"] != DBNull.Value)
                entidade.CategoriaId = Convert.ToInt32(reader["categoriaId"].ToString());

            if (reader["nomeCategoria"] != DBNull.Value)
                entidade.NomeCategoria = reader["nomeCategoria"].ToString();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Usuario LoginUsuario(Usuario usuario, String chave)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" select U.*, UP.*, P.*");
            sbSQL.Append(" from Usuario U");
            sbSQL.Append(" INNER JOIN UsuarioPerfil UP ON U.usuarioId=UP.usuarioId");
            sbSQL.Append(" INNER JOIN Perfil P ON UP.perfilId=P.perfilId");
            sbSQL.Append(" Where u.cadastroPessoa = @cadastroPessoa");
            sbSQL.Append("       AND CAST(DecryptByPassPhrase(@chave, u.senha) AS NVARCHAR) = @senha");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@cadastroPessoa", DbType.String, usuario.CadastroPessoa);
            _db.AddInParameter(command, "@chave", DbType.String, chave);
            _db.AddInParameter(command, "@senha", DbType.String, usuario.Senha);

            IDataReader reader = _db.ExecuteReader(command);

            Usuario entidadeRetorno = null;

            if (reader.Read())
            {
                entidadeRetorno = new Usuario();

                PopulaUsuario(reader, entidadeRetorno);
                Perfil perfil = new Perfil();

                entidadeRetorno.Perfis = new List<Perfil>();
                PerfilADO.PopulaPerfil(reader, perfil);
                entidadeRetorno.Perfis.Add(perfil);
            }

            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Carrega lista de usuarios para ser enviado ao web service.
        /// </summary>
        /// <returns>Lista de usuarios para ser exportado</returns>
        public IEnumerable<Usuario> CarregaUsuariosParaExportacao(int sincronizacao)
        {
            List<Usuario> entidadesRetorno = new List<Usuario>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" SELECT Usuario.*, UsuarioControle.*, Endereco.*, Telefone.* ");
            sbSQL.Append(" FROM Usuario ");
            sbSQL.Append(" INNER JOIN UsuarioControle ON Usuario.usuarioId=UsuarioControle.usuarioId ");
            sbSQL.Append(" INNER JOIN Endereco ON Usuario.usuarioId=Endereco.usuarioId ");
            sbSQL.Append(" INNER JOIN Telefone ON Usuario.usuarioId=Telefone.usuarioId ");
            sbSQL.Append(" WHERE UsuarioControle.RealizarSincronizacao=@sincronizacao");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@sincronizacao", DbType.Int32, sincronizacao);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Usuario entidadeRetorno = new Usuario();
                PopulaUsuario(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// Carrega todos os usuários que tem sincronização pendente (UsuarioControle.realizarSincronizacao = 1)
        /// </summary>
        /// <returns></returns>
        public List<Usuario> CarregarUsuariosComSincronizacaoPendente()
        {
            List<Usuario> usuarioBOList = new List<Usuario>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT 
								*
								, (
									SELECT 
										COUNT(PedidoId) 
									FROM 
										Pedido 
									WHERE 
										Pedido.UsuarioId = Usuario.UsuarioId
									) Pedidos 
							FROM 
								Usuario 
								JOIN UsuarioControle ON Usuario.UsuarioId = UsuarioControle.UsuarioId AND UsuarioControle.realizarSincronizacao = 1
							WHERE
								EXISTS(
										SELECT
											Endereco.enderecoId
										FROM
											Endereco
										WHERE
											Endereco.usuarioId = Usuario.usuarioId
										)
						");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            IDataReader reader = _db.ExecuteReader(command);

            Usuario usuarioBO = new Usuario();
            Telefone telefone = new Telefone();

            while (reader.Read())
            {
                try
                {
                    if (reader["usuarioId"] != DBNull.Value && usuarioBO.UsuarioId != Convert.ToInt32(reader["usuarioId"]))
                    {
                        usuarioBO = new Usuario();
                        PopulaUsuario(reader, usuarioBO);

                        // Popula um pedido para não deixar os pedidos nulos e fazerem com que existam compras para esse cliente
                        if (reader["Pedidos"] != DBNull.Value && Convert.ToInt32(reader["Pedidos"]) > 0)
                        {
                            usuarioBO.Pedidos = new List<Pedido>();
                            usuarioBO.Pedidos.Add(new Pedido(-1));
                        }

                        usuarioBO.UsuarioControle = new UsuarioControle();
                        UsuarioControleADO.PopulaUsuarioControle(reader, usuarioBO.UsuarioControle);

                        usuarioBO.Enderecos = new List<Endereco>();
                        usuarioBO.Enderecos.Add(CarregaEnderecoUsuario(usuarioBO));

                        usuarioBO.Telefones = new List<Telefone>();
                        usuarioBO.Telefones.Add(CarregarTelefoneUsuario(usuarioBO));

                        usuarioBOList.Add(usuarioBO);
                    }
                }
                catch { }
            }

            reader.Close();

            return usuarioBOList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioBO"></param>
        /// <returns></returns>
        private Endereco CarregaEnderecoUsuario(Usuario usuarioBO)
        {
            Endereco enderecoBO = new Endereco();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT 
								TOP 1 *
							FROM 
								Usuario 
								JOIN Endereco ON Endereco.UsuarioId = Usuario.UsuarioId 
								JOIN EnderecoTipo ON EnderecoTipo.EnderecoTipoId = Endereco.EnderecoTipoId 
								JOIN Municipio ON Municipio.MunicipioId = Endereco.MunicipioId 
								INNER JOIN Regiao ON Regiao.RegiaoId = Municipio.RegiaoId
							WHERE
								Usuario.usuarioId = @usuarioId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuarioBO.UsuarioId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                EnderecoADO.PopulaEndereco(reader, enderecoBO);
                EnderecoTipoADO.PopulaEnderecoTipo(reader, enderecoBO.EnderecoTipo);
                enderecoBO.Municipio = new Municipio();
                enderecoBO.Municipio.Regiao = new Regiao();
                MunicipioADO.PopulaMunicipio(reader, enderecoBO.Municipio);
                RegiaoADO.PopulaRegiao(reader, enderecoBO.Municipio.Regiao);
            }

            return enderecoBO;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioBO"></param>
        /// <returns></returns>
        private Telefone CarregarTelefoneUsuario(Usuario usuarioBO)
        {
            Telefone telefoneBO = new Telefone();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT 
								TOP 1 *
							FROM 
								Usuario 
								JOIN Telefone ON Telefone.UsuarioId = Usuario.UsuarioId 
								JOIN TelefoneTipo ON Telefone.TelefoneTipoId = TelefoneTipo.TelefoneTipoId 
							WHERE
								Usuario.usuarioId = @usuarioId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuarioBO.UsuarioId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                TelefoneADO.PopulaTelefone(reader, telefoneBO);
                TelefoneTipoADO.PopulaTelefoneTipo(reader, telefoneBO.TelefoneTipo);
            }

            return telefoneBO;
        }

        /*
        /// <summary>
        /// Carrega todos os usuários que tem sincronização pendente (UsuarioControle.realizarSincronizacao = 1).
        /// </summary>
        /// <returns>Listagem de Usuarios contendo Endereços, Tipos de Endereço, Telefones, Tipos de Telefone e Controle do Usuário</returns>
        public List<Usuario> CarregarUsuariosComSincronizacaoPendente()
        {
            List<Usuario> usuarios = new List<Usuario>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" SELECT *, (SELECT COUNT(PedidoId) FROM Pedido WHERE Pedido.UsuarioId = Usuario.UsuarioId) Pedidos FROM Usuario");
            sbSQL.Append(" JOIN UsuarioControle ON Usuario.UsuarioId = UsuarioControle.UsuarioId");
            sbSQL.Append(" AND UsuarioControle.realizarSincronizacao = 1");
            sbSQL.Append(" JOIN Telefone ON Telefone.UsuarioId = Usuario.UsuarioId");
            sbSQL.Append(" JOIN TelefoneTipo ON Telefone.TelefoneTipoId = TelefoneTipo.TelefoneTipoId");
            sbSQL.Append(" JOIN Endereco ON Endereco.UsuarioId = Usuario.UsuarioId");
            sbSQL.Append(" JOIN EnderecoTipo ON EnderecoTipo.EnderecoTipoId = Endereco.EnderecoTipoId");
            sbSQL.Append(" JOIN Municipio ON Municipio.MunicipioId = Endereco.MunicipioId");
            sbSQL.Append(" INNER JOIN Regiao ON Regiao.RegiaoId = Municipio.RegiaoId");
            sbSQL.Append(" ORDER BY dbo.Usuario.usuarioId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            //if (!var)
            //{
            //    _db.ExecuteReader(command).Close();
            //}

            IDataReader reader = _db.ExecuteReader(command);

            Usuario usuarioCorrente = new Usuario();
            Endereco endereco = new Endereco();
            Telefone telefone = new Telefone();

            while (reader.Read())
            {
                if (usuarioCorrente.UsuarioId != Convert.ToInt32(reader["usuarioId"]))
                {
                    usuarioCorrente = new Usuario();
                    PopulaUsuario(reader, usuarioCorrente);

                    // Popula um pedido para não deixar os pedidos nulos e fazerem com que existam compras para esse cliente
                    if (Convert.ToInt32(reader["Pedidos"]) > 0)
                    {
                        usuarioCorrente.Pedidos = new List<Pedido>();
                        usuarioCorrente.Pedidos.Add(new Pedido(-1));
                    }

                    usuarios.Add(usuarioCorrente);

                    usuarioCorrente.UsuarioControle = new UsuarioControle();
                    UsuarioControleADO.PopulaUsuarioControle(reader, usuarioCorrente.UsuarioControle);
                }

                if (usuarioCorrente.Enderecos != null && !usuarioCorrente.Enderecos.Any(e => e.EnderecoId == Convert.ToInt32(reader["enderecoId"])))
                {
                    endereco = new Endereco();
                    EnderecoADO.PopulaEndereco(reader, endereco);
                    EnderecoTipoADO.PopulaEnderecoTipo(reader, endereco.EnderecoTipo);
                    endereco.Municipio = new Municipio();
                    endereco.Municipio.Regiao = new Regiao();
                    MunicipioADO.PopulaMunicipio(reader, endereco.Municipio);
                    RegiaoADO.PopulaRegiao(reader, endereco.Municipio.Regiao);
                    usuarioCorrente.Enderecos.Add(endereco);
                }

                if (usuarioCorrente.Telefones != null && !usuarioCorrente.Telefones.Any(t => t.TelefoneId == Convert.ToInt32(reader["telefoneId"])))
                {
                    telefone = new Telefone();
                    TelefoneADO.PopulaTelefone(reader, telefone);
                    TelefoneTipoADO.PopulaTelefoneTipo(reader, telefone.TelefoneTipo);
                    usuarioCorrente.Telefones.Add(telefone);
                }
            }

            reader.Close();

            return usuarios;
        }
        */

        public bool ValidarCadastroPessoaUnico(Usuario usuario)
        {
            int resultado = 0;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT count(*) ");
            sbSQL.Append("FROM Usuario ");
            sbSQL.Append("WHERE CadastroPessoa = @cadastroPessoa ");

            if (usuario.UsuarioId > 0)
            {
                sbSQL.Append(" AND UsuarioId != @UsuarioId ");
            }

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@cadastroPessoa", DbType.String, usuario.CadastroPessoa);
            _db.AddInParameter(command, "@UsuarioId", DbType.String, usuario.UsuarioId);

            resultado = (int)_db.ExecuteScalar(command);

            return (resultado > 0 ? false : true);
        }

        public bool ValidarEmailUnico(Usuario usuario)
        {
            int resultado = 0;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT count(*) ");
            sbSQL.Append("FROM Usuario ");
            sbSQL.Append("WHERE emailUsuario = @email ");

            if (usuario.UsuarioId > 0)
            {
                sbSQL.Append(" AND UsuarioId != @UsuarioId ");
            }

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@email", DbType.String, usuario.EmailUsuario);
            _db.AddInParameter(command, "@UsuarioId", DbType.String, usuario.UsuarioId);

            resultado = (int)_db.ExecuteScalar(command);

            return (resultado > 0 ? false : true);
        }

        public Usuario CarregarUsuarioEsqueciMinhaSenha(Usuario usuario, String chave)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT cadastroPessoa,");
            sbSQL.Append("       CAST(DecryptByPassPhrase(@chave, senha) AS NVARCHAR) as senha,");
            sbSQL.Append("       emailUsuario");
            sbSQL.Append(" FROM Usuario ");
            sbSQL.Append(" WHERE ");
            if (!String.IsNullOrEmpty(usuario.CadastroPessoa))
            {
                sbSQL.Append(" cadastroPessoa = @cadastroPessoa");
            }
            else
            {
                sbSQL.Append(" emailUsuario = @emailUsuario");
            }

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@chave", DbType.String, chave);
            if (!String.IsNullOrEmpty(usuario.CadastroPessoa))
            {
                _db.AddInParameter(command, "@cadastroPessoa", DbType.String, usuario.CadastroPessoa);
            }
            else
            {
                _db.AddInParameter(command, "@emailUsuario", DbType.String, usuario.EmailUsuario);
            }

            IDataReader reader = _db.ExecuteReader(command);

            Usuario entidadeRetorno = null;

            if (reader.Read())
            {
                entidadeRetorno = new Usuario();

                PopulaUsuarioEsqueciMinhaSenha(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }

        public static void PopulaUsuarioEsqueciMinhaSenha(IDataReader reader, Usuario entidade)
        {
            if (reader["cadastroPessoa"] != DBNull.Value)
                entidade.CadastroPessoa = reader["cadastroPessoa"].ToString();

            if (reader["senha"] != DBNull.Value)
                entidade.Senha = reader["senha"].ToString();

            if (reader["emailUsuario"] != DBNull.Value)
                entidade.EmailUsuario = reader["emailUsuario"].ToString();
        }

        public List<Usuario> CarregarUsuarioPromocaoAniversariantes()
        {
            List<Usuario> entidadeRetorno = new List<Usuario>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT Usuario.*
                            FROM Usuario
                            WHERE
	                            CONVERT(nvarchar, MONTH(dataNascimento)) + '-' + CONVERT(nvarchar, DAY(dataNascimento)) = CONVERT(nvarchar, MONTH(DATEADD (DAY, 1, GETDATE()))) + '-' + CONVERT(nvarchar, DAY(DATEADD (DAY, 1, GETDATE())))
	                            AND ativo = 1
                                AND Usuario.usuarioId NOT IN (
	                                SELECT PU.usuarioId FROM PromocaoUsuario AS PU
	                                INNER JOIN Promocao AS P
	                                ON P.promocaoId = PU.promocaoId
	                                WHERE P.origemSistema = 1 AND YEAR(P.dataHoraInicio) = YEAR(GETDATE())
                                )");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Usuario _usuario = new Usuario();
                PopulaUsuario(reader, _usuario);
                entidadeRetorno.Add(_usuario);
            }

            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public Usuario CarregarPorCadastroPessoa(Usuario entidade)
        {
            Usuario entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM Usuario WHERE CadastroPessoa=@CadastroPessoa");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@CadastroPessoa", DbType.String, entidade.CadastroPessoa);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Usuario();
                PopulaUsuario(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void AtualizarStatus(Usuario entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE Usuario SET ");
            sbSQL.Append(" ativo=@ativo ");
            sbSQL.Append(" WHERE usuarioId=@usuarioId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.UsuarioId);
            _db.AddInParameter(command, "@ativo", DbType.Int32, entidade.Ativo);

            // Executa a query.
            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public Usuario CarregarPorCadastroPessoaEmail(Usuario entidade)
        {
            Usuario entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM Usuario WHERE CadastroPessoa=@CadastroPessoa AND emailUsuario = @emailUsuario");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@CadastroPessoa", DbType.String, entidade.CadastroPessoa);
            _db.AddInParameter(command, "@emailUsuario", DbType.String, entidade.EmailUsuario);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Usuario();
                PopulaUsuario(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public List<Categoria> CarregarUsuarioAreaInteresse(Usuario entidade)
        {
            List<Categoria> entidadeRetorno = new List<Categoria>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT
	                            DISTINCT Categoria.categoriaIdPai AS categoriaId, '' AS nomeCategoria
                            FROM
	                            Usuario
	                            INNER JOIN UsuarioInteresse ON UsuarioInteresse.usuarioId = Usuario.usuarioId
	                            INNER JOIN Categoria ON Categoria.categoriaId = UsuarioInteresse.categoriaId
                            WHERE
	                            Usuario.usuarioId = @usuarioId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.UsuarioId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Categoria categoria = new Categoria();
                PopulaCategoriaInteresse(reader, categoria);
                entidadeRetorno.Add(categoria);
            }

            reader.Close();

            return entidadeRetorno;
        }

        public List<Usuario> CarregarUsuarioParaAssinatura(String cadastroPessoa, String nomeUsuario)
        {
            List<Usuario> entidadeRetorno = new List<Usuario>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT usuarioId, ");
            sbSQL.Append("    cadastroPessoa + ' - ' + nomeUsuario AS nomeUsuario ");
            sbSQL.Append("FROM Usuario ");
            sbSQL.Append("WHERE ativo = 1 ");

            if (!String.IsNullOrEmpty(cadastroPessoa))
            {
                sbSQL.Append("AND cadastroPessoa LIKE @cadastroPessoa ");
            }

            if (!String.IsNullOrEmpty(nomeUsuario))
            {
                sbSQL.Append("AND nomeUsuario LIKE @nomeUsuario ");
            }

            sbSQL.Append("ORDER BY nomeUsuario ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            if (!String.IsNullOrEmpty(cadastroPessoa))
            {
                _db.AddInParameter(command, "@cadastroPessoa", DbType.String, String.Concat("%", cadastroPessoa, "%"));
            }

            if (!String.IsNullOrEmpty(nomeUsuario))
            {
                _db.AddInParameter(command, "@nomeUsuario", DbType.String, String.Concat("%", nomeUsuario, "%"));
            }

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Usuario _usuario = new Usuario();
                PopulaUsuarioAssinatura(reader, _usuario);
                entidadeRetorno.Add(_usuario);
            }

            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public List<Usuario> Carregar(CompraConjunta entidade)
        {
            List<Usuario> entidadeRetorno = new List<Usuario>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT 
	                            DISTINCT Usuario.*
                            FROM
	                            CompraConjunta
	                            INNER JOIN PedidoCompraConjunta ON PedidoCompraConjunta.compraConjuntaId = CompraConjunta.compraConjuntaId
	                            INNER JOIN Pedido ON Pedido.pedidoId = PedidoCompraConjunta.pedidoCompraConjuntaId
	                            INNER JOIN Usuario ON Usuario.usuarioId = Pedido.usuarioId
                            WHERE
	                            CompraConjunta.compraConjuntaId = @compraConjuntaId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@compraConjuntaId", DbType.Int32, entidade.CompraConjuntaId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Usuario _usuario = new Usuario();
                PopulaUsuario(reader, _usuario);
                entidadeRetorno.Add(_usuario);
            }

            reader.Close();

            return entidadeRetorno;
        }

        public static void PopulaUsuarioAssinatura(IDataReader reader, Usuario entidade)
        {
            if (reader["usuarioId"] != DBNull.Value)
                entidade.UsuarioId = Convert.ToInt32(reader["usuarioId"].ToString());

            if (reader["nomeUsuario"] != DBNull.Value)
                entidade.NomeUsuario = reader["nomeUsuario"].ToString();
        }

        #endregion
    }
}