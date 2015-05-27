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
    public partial class UsuarioADO : ADOSuper, IUsuarioDAL
    {
        /// <summary>
        /// Método que persiste um Usuario.
        /// </summary>
        /// <param name="entidade">Usuario contendo os dados a serem persistidos.</param>	
        public void Inserir(Usuario entidade, String chave)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO Usuario ");
            sbSQL.Append(" (tipoPessoa, sexo, ativo, nomeUsuario, cadastroPessoa, emailUsuario, login, dataNascimento, dataHoraCadastro, optinSMS, optinNewsletter, codigoUsuario, profissionalOcupacaoId, senha) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@tipoPessoa, @sexo, @ativo, @nomeUsuario, @cadastroPessoa, @emailUsuario, @login, @dataNascimento, @dataHoraCadastro, @optinSMS, @optinNewsletter, @codigoUsuario, @profissionalOcupacaoId, EncryptByPassPhrase(@chave, CAST((@senha) AS NVARCHAR))) ");

            sbSQL.Append(" ; SET @usuarioId = SCOPE_IDENTITY(); ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddOutParameter(command, "@usuarioId", DbType.Int32, 8);

            _db.AddInParameter(command, "@tipoPessoa", DbType.Int32, entidade.TipoPessoa);

            if (entidade.Sexo != null)
                _db.AddInParameter(command, "@sexo", DbType.String, entidade.Sexo);
            else
                _db.AddInParameter(command, "@sexo", DbType.String, null);

            _db.AddInParameter(command, "@ativo", DbType.Int32, entidade.Ativo);

            _db.AddInParameter(command, "@nomeUsuario", DbType.String, entidade.NomeUsuario);

            _db.AddInParameter(command, "@cadastroPessoa", DbType.String, entidade.CadastroPessoa);

            _db.AddInParameter(command, "@emailUsuario", DbType.String, entidade.EmailUsuario);

            if (entidade.Login != null)
                _db.AddInParameter(command, "@login", DbType.String, entidade.Login);
            else
                _db.AddInParameter(command, "@login", DbType.String, null);

            if (entidade.DataNascimento != null && entidade.DataNascimento != DateTime.MinValue)
                _db.AddInParameter(command, "@dataNascimento", DbType.DateTime, entidade.DataNascimento);
            else
                _db.AddInParameter(command, "@dataNascimento", DbType.DateTime, null);

            _db.AddInParameter(command, "@dataHoraCadastro", DbType.DateTime, entidade.DataHoraCadastro);

            _db.AddInParameter(command, "@optinSMS", DbType.Int32, entidade.OptinSMS);

            _db.AddInParameter(command, "@optinNewsletter", DbType.Int32, entidade.OptinNewsletter);

            if (entidade.CodigoUsuario != null)
                _db.AddInParameter(command, "@codigoUsuario", DbType.String, entidade.CodigoUsuario);
            else
                _db.AddInParameter(command, "@codigoUsuario", DbType.String, null);

            if (entidade.ProfissionalOcupacao != null)
                _db.AddInParameter(command, "@profissionalOcupacaoId", DbType.Int32, entidade.ProfissionalOcupacao.ProfissionalOcupacaoId);
            else
                _db.AddInParameter(command, "@profissionalOcupacaoId", DbType.Int32, null);

            _db.AddInParameter(command, "@chave", DbType.String, chave);
            _db.AddInParameter(command, "@senha", DbType.String, entidade.Senha);


            // Executa a query.
            _db.ExecuteNonQuery(command);

            entidade.UsuarioId = Convert.ToInt32(_db.GetParameterValue(command, "@usuarioId"));

        }

        /// <summary>
        /// Método que atualiza os dados de um Usuario.
        /// </summary>
        /// <param name="entidade">Usuario contendo os dados a serem atualizados.</param>
        public void Atualizar(Usuario entidade, String chave)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE Usuario SET ");
            sbSQL.Append(" tipoPessoa=@tipoPessoa, sexo=@sexo, ativo=@ativo, nomeUsuario=@nomeUsuario, cadastroPessoa=@cadastroPessoa, emailUsuario=@emailUsuario, login=@login, dataNascimento=@dataNascimento, dataHoraCadastro=@dataHoraCadastro, optinSMS=@optinSMS, optinNewsletter=@optinNewsletter, codigoUsuario=@codigoUsuario, profissionalOcupacaoId=@profissionalOcupacaoId ");

            if (!String.IsNullOrEmpty(entidade.Senha))
                sbSQL.Append(" , senha=EncryptByPassPhrase(@chave, CAST((@senha) AS NVARCHAR)) ");

            sbSQL.Append(" WHERE usuarioId=@usuarioId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.UsuarioId);
            _db.AddInParameter(command, "@tipoPessoa", DbType.Int32, entidade.TipoPessoa);
            if (entidade.Sexo != null)
                _db.AddInParameter(command, "@sexo", DbType.String, entidade.Sexo);
            else
                _db.AddInParameter(command, "@sexo", DbType.String, null);
            _db.AddInParameter(command, "@ativo", DbType.Int32, entidade.Ativo);
            _db.AddInParameter(command, "@nomeUsuario", DbType.String, entidade.NomeUsuario);
            _db.AddInParameter(command, "@cadastroPessoa", DbType.String, entidade.CadastroPessoa);
            _db.AddInParameter(command, "@emailUsuario", DbType.String, entidade.EmailUsuario);
            if (entidade.Login != null)
                _db.AddInParameter(command, "@login", DbType.String, entidade.Login);
            else
                _db.AddInParameter(command, "@login", DbType.String, null);
            if (entidade.DataNascimento != null && entidade.DataNascimento != DateTime.MinValue)
                _db.AddInParameter(command, "@dataNascimento", DbType.DateTime, entidade.DataNascimento);
            else
                _db.AddInParameter(command, "@dataNascimento", DbType.DateTime, null);
            _db.AddInParameter(command, "@dataHoraCadastro", DbType.DateTime, entidade.DataHoraCadastro);
            _db.AddInParameter(command, "@optinSMS", DbType.Int32, entidade.OptinSMS);
            _db.AddInParameter(command, "@optinNewsletter", DbType.Int32, entidade.OptinNewsletter);
            if (entidade.CodigoUsuario != null)
                _db.AddInParameter(command, "@codigoUsuario", DbType.String, entidade.CodigoUsuario);
            else
                _db.AddInParameter(command, "@codigoUsuario", DbType.String, null);
            if (entidade.ProfissionalOcupacao != null)
                _db.AddInParameter(command, "@profissionalOcupacaoId", DbType.Int32, entidade.ProfissionalOcupacao.ProfissionalOcupacaoId);
            else
                _db.AddInParameter(command, "@profissionalOcupacaoId", DbType.Int32, null);

            if (!String.IsNullOrEmpty(entidade.Senha))
            {
                _db.AddInParameter(command, "@chave", DbType.String, chave);
                _db.AddInParameter(command, "@senha", DbType.String, entidade.Senha);
            }

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que remove um Usuario da base de dados.
        /// </summary>
        /// <param name="entidade">Usuario a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(Usuario entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM Usuario ");
            sbSQL.Append("WHERE usuarioId=@usuarioId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.UsuarioId);


            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que carrega um Usuario.
        /// </summary>
        /// <param name="entidade">Usuario a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Usuario</returns>
        public Usuario Carregar(int usuarioId)
        {
            Usuario entidade = new Usuario();
            entidade.UsuarioId = usuarioId;
            return Carregar(entidade);

        }


        /// <summary>
        /// Método que carrega um Usuario.
        /// </summary>
        /// <param name="entidade">Usuario a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Usuario</returns>
        public Usuario Carregar(Usuario entidade)
        {

            Usuario entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM Usuario WHERE usuarioId=@usuarioId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.UsuarioId);

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
        /// Método que retorna uma coleção de Usuario.
        /// </summary>
        /// <param name="entidade">Carrinho relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Usuario.</returns>
        public IEnumerable<Usuario> Carregar(Carrinho entidade)
        {
            List<Usuario> entidadesRetorno = new List<Usuario>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Usuario.* FROM Usuario INNER JOIN Carrinho ON Usuario.usuarioId=Carrinho.usuarioId WHERE Carrinho.carrinhoId=@carrinhoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@carrinhoId", DbType.Int32, entidade.CarrinhoId);

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
        /// Método que retorna uma coleção de Usuario.
        /// </summary>
        /// <param name="entidade">Endereco relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Usuario.</returns>
        public IEnumerable<Usuario> Carregar(Endereco entidade)
        {
            List<Usuario> entidadesRetorno = new List<Usuario>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Usuario.* FROM Usuario INNER JOIN Endereco ON Usuario.usuarioId=Endereco.usuarioId WHERE Endereco.enderecoId=@enderecoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@enderecoId", DbType.Int32, entidade.EnderecoId);

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
        /// Método que retorna uma coleção de Usuario.
        /// </summary>
        /// <param name="entidade">Enquete relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Usuario.</returns>
        public IEnumerable<Usuario> Carregar(Enquete entidade)
        {
            List<Usuario> entidadesRetorno = new List<Usuario>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Usuario.* FROM Usuario INNER JOIN EnqueteUsuario ON Usuario.usuarioId=EnqueteUsuario.usuarioId WHERE EnqueteUsuario.enqueteId=@enqueteId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@enqueteId", DbType.Int32, entidade.EnqueteId);

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
        /// Método que retorna uma coleção de Usuario.
        /// </summary>
        /// <param name="entidade">EventoAlerta relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Usuario.</returns>
        public IEnumerable<Usuario> Carregar(EventoAlerta entidade)
        {
            List<Usuario> entidadesRetorno = new List<Usuario>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Usuario.* FROM Usuario INNER JOIN EventoAlerta ON Usuario.usuarioId=EventoAlerta.usuarioId WHERE EventoAlerta.eventoAlertaId=@eventoAlertaId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@eventoAlertaId", DbType.Int32, entidade.EventoAlertaId);

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
        /// Método que retorna uma coleção de Usuario.
        /// </summary>
        /// <param name="entidade">LogOcorrencia relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Usuario.</returns>
        public IEnumerable<Usuario> Carregar(LogOcorrencia entidade)
        {
            List<Usuario> entidadesRetorno = new List<Usuario>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Usuario.* FROM Usuario INNER JOIN LogOcorrencia ON Usuario.usuarioId=LogOcorrencia.usuarioId WHERE LogOcorrencia.logOcorrenciaId=@logOcorrenciaId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@logOcorrenciaId", DbType.Int32, entidade.LogOcorrenciaId);

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
        /// Método que retorna uma coleção de Usuario.
        /// </summary>
        /// <param name="entidade">NotificacaoDisponibilidade relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Usuario.</returns>
        public IEnumerable<Usuario> Carregar(NotificacaoDisponibilidade entidade)
        {
            List<Usuario> entidadesRetorno = new List<Usuario>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Usuario.* FROM Usuario INNER JOIN NotificacaoDisponibilidade ON Usuario.usuarioId=NotificacaoDisponibilidade.usuarioId WHERE NotificacaoDisponibilidade.notificacaoDisponibilidadeId=@notificacaoDisponibilidadeId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@notificacaoDisponibilidadeId", DbType.Int32, entidade.NotificacaoDisponibilidadeId);

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
        /// Método que retorna uma coleção de Usuario.
        /// </summary>
        /// <param name="entidade">Pedido relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Usuario.</returns>
        public IEnumerable<Usuario> Carregar(Pedido entidade)
        {
            List<Usuario> entidadesRetorno = new List<Usuario>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Usuario.* FROM Usuario INNER JOIN Pedido ON Usuario.usuarioId=Pedido.usuarioId WHERE Pedido.pedidoId=@pedidoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@pedidoId", DbType.Int32, entidade.PedidoId);

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
        /// Método que retorna uma coleção de Usuario.
        /// </summary>
        /// <param name="entidade">Promocao relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Usuario.</returns>
        public IEnumerable<Usuario> Carregar(Promocao entidade)
        {
            List<Usuario> entidadesRetorno = new List<Usuario>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Usuario.* FROM Usuario INNER JOIN PromocaoUsuario ON Usuario.usuarioId=PromocaoUsuario.usuarioId WHERE PromocaoUsuario.promocaoId=@promocaoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@promocaoId", DbType.Int32, entidade.PromocaoId);

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
        /// Método que retorna uma coleção de Usuario.
        /// </summary>
        /// <param name="entidade">Telefone relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Usuario.</returns>
        public IEnumerable<Usuario> Carregar(Telefone entidade)
        {
            List<Usuario> entidadesRetorno = new List<Usuario>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Usuario.* FROM Usuario INNER JOIN Telefone ON Usuario.usuarioId=Telefone.usuarioId WHERE Telefone.telefoneId=@telefoneId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@telefoneId", DbType.Int32, entidade.TelefoneId);

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
        /// Método que retorna uma coleção de Usuario.
        /// </summary>
        /// <param name="entidade">Categoria relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Usuario.</returns>
        public IEnumerable<Usuario> Carregar(Categoria entidade)
        {
            List<Usuario> entidadesRetorno = new List<Usuario>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Usuario.* FROM Usuario INNER JOIN UsuarioInteresse ON Usuario.usuarioId=UsuarioInteresse.usuarioId WHERE UsuarioInteresse.categoriaId=@categoriaId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@categoriaId", DbType.Int32, entidade.CategoriaId);

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
        /// Método que retorna uma coleção de Usuario.
        /// </summary>
        /// <param name="entidade">Perfil relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Usuario.</returns>
        public IEnumerable<Usuario> Carregar(Perfil entidade)
        {
            List<Usuario> entidadesRetorno = new List<Usuario>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Usuario.* FROM Usuario INNER JOIN UsuarioPerfil ON Usuario.usuarioId=UsuarioPerfil.usuarioId WHERE UsuarioPerfil.perfilId=@perfilId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@perfilId", DbType.Int32, entidade.PerfilId);

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
        /// Método que retorna uma coleção de Usuario.
        /// </summary>
        /// <param name="entidade">ProfissionalOcupacao relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Usuario.</returns>
        public IEnumerable<Usuario> Carregar(ProfissionalOcupacao entidade)
        {
            List<Usuario> entidadesRetorno = new List<Usuario>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Usuario.* FROM Usuario WHERE Usuario.profissionalOcupacaoId=@profissionalOcupacaoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@profissionalOcupacaoId", DbType.Int32, entidade.ProfissionalOcupacaoId);

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
        /// Método que retorna uma coleção de Usuario.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos Usuario.</returns>
        public IEnumerable<Usuario> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {
            List<Usuario> entidadesRetorno = new List<Usuario>();

            StringBuilder sbSQL = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            StringBuilder sbOrder = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            // Monta o "OrderBy"
            if (ordemColunas != null)
            {
                for (int i = 0; i < ordemColunas.Length; i++)
                {
                    if (sbOrder.Length > 0) { sbOrder.Append(", "); }
                    sbOrder.Append(ordemColunas[i] + " " + ordemSentidos[i]);
                }
                if (sbOrder.Length > 0) { sbOrder.Insert(0, " ORDER BY "); }
            }
            else
            {
                sbOrder.Append(" ORDER BY usuarioId");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Usuario");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Usuario WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Usuario ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT Usuario.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Usuario ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT Usuario.* FROM Usuario ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

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
        /// Método que retorna todas os Usuario existentes na base de dados.
        /// </summary>
        public IEnumerable<Usuario> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de Usuario na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de Usuario na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM Usuario");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um Usuario baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Usuario a ser populado(.</param>
        public static void PopulaUsuario(IDataReader reader, Usuario entidade)
        {
            if (reader["usuarioId"] != DBNull.Value)
                entidade.UsuarioId = Convert.ToInt32(reader["usuarioId"].ToString());

            if (reader["tipoPessoa"] != DBNull.Value)
                entidade.TipoPessoa = Convert.ToInt32(reader["tipoPessoa"].ToString());

            if (reader["sexo"] != DBNull.Value)
                entidade.Sexo = reader["sexo"].ToString();

            if (reader["ativo"] != DBNull.Value)
                entidade.Ativo = Convert.ToBoolean(reader["ativo"].ToString());

            if (reader["nomeUsuario"] != DBNull.Value)
                entidade.NomeUsuario = reader["nomeUsuario"].ToString();

            if (reader["cadastroPessoa"] != DBNull.Value)
                entidade.CadastroPessoa = reader["cadastroPessoa"].ToString();

            if (reader["emailUsuario"] != DBNull.Value)
                entidade.EmailUsuario = reader["emailUsuario"].ToString();

            if (reader["login"] != DBNull.Value)
                entidade.Login = reader["login"].ToString();

            if (reader["dataNascimento"] != DBNull.Value)
                entidade.DataNascimento = Convert.ToDateTime(reader["dataNascimento"].ToString());

            if (reader["dataHoraCadastro"] != DBNull.Value)
                entidade.DataHoraCadastro = Convert.ToDateTime(reader["dataHoraCadastro"].ToString());

            if (reader["optinSMS"] != DBNull.Value)
                entidade.OptinSMS = Convert.ToBoolean(reader["optinSMS"].ToString());

            if (reader["optinNewsletter"] != DBNull.Value)
                entidade.OptinNewsletter = Convert.ToBoolean(reader["optinNewsletter"].ToString());

            if (reader["codigoUsuario"] != DBNull.Value)
                entidade.CodigoUsuario = reader["codigoUsuario"].ToString();

            if (reader["profissionalOcupacaoId"] != DBNull.Value)
            {
                entidade.ProfissionalOcupacao = new ProfissionalOcupacao();
                entidade.ProfissionalOcupacao.ProfissionalOcupacaoId = Convert.ToInt32(reader["profissionalOcupacaoId"].ToString());
            }


        }
    }
}