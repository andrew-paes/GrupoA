using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess.ADO
{
	public partial class EnderecoADO : ADOSuper, IEnderecoDAL
    {

        #region Métodos

        public List<Endereco> CarregarEnderecosUsuario(Usuario usuario)
        {

            List<Endereco> entidadeRetorno = new List<Endereco>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" SELECT e.*, et.tipo, m.*, r.* FROM Endereco e ");
            sbSQL.Append(" INNER JOIN EnderecoTipo et ON e.enderecoTipoId = et.enderecoTipoId ");
            sbSQL.Append(" INNER JOIN Municipio m ON m.municipioId = e.municipioId ");
            sbSQL.Append(" INNER JOIN Regiao r ON r.regiaoId = m.regiaoId ");
            sbSQL.Append(" WHERE usuarioId = @usuarioId ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuario.UsuarioId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Endereco end = new Endereco();
                PopulaEndereco(reader, end);
                // Popula o tipo de endereco
                EnderecoTipoADO.PopulaEnderecoTipo(reader, end.EnderecoTipo);
                // Popula o município
                MunicipioADO.PopulaMunicipio(reader, end.Municipio);
                // Popula a região
                RegiaoADO.PopulaRegiao(reader, end.Municipio.Regiao);
                entidadeRetorno.Add(end);
            }
            reader.Close();

            return entidadeRetorno;
        }
        
        public Endereco CarregarEnderecoComDependencias( Endereco endereco )
        {
			//Endereco endereco = null;

			StringBuilder sbSQL = new StringBuilder();

			sbSQL.Append("SELECT * FROM Endereco e ");
			sbSQL.Append("JOIN EnderecoTipo et ON et.enderecoTipoId = e.enderecoTipoId ");
			sbSQL.Append("JOIN Municipio m ON m.municipioId = e.municipioId ");
			sbSQL.Append("JOIN Regiao r ON r.regiaoId = m.regiaoId ");
			sbSQL.Append("WHERE e.enderecoId=@enderecoId");

			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

			_db.AddInParameter(command, "@enderecoId", DbType.Int32, endereco.EnderecoId);

			IDataReader reader = _db.ExecuteReader(command);

			if (reader.Read())
			{
				//entidadeRetorno = new Endereco();
				PopulaEnderecoComDependencias(reader, endereco);
			}
			reader.Close();

			return endereco;
        }

		public static void PopulaEnderecoComDependencias(IDataReader reader, Endereco entidade)
		{
			if (reader["enderecoId"] != DBNull.Value)
				entidade.EnderecoId = Convert.ToInt32(reader["enderecoId"].ToString());

			if (reader["preferencialParaEntrega"] != DBNull.Value)
				entidade.PreferencialParaEntrega = Convert.ToBoolean(reader["preferencialParaEntrega"].ToString());

			if (reader["logradouro"] != DBNull.Value)
				entidade.Logradouro = reader["logradouro"].ToString();

			if (reader["bairro"] != DBNull.Value)
				entidade.Bairro = reader["bairro"].ToString();

			if (reader["cep"] != DBNull.Value)
				entidade.Cep = reader["cep"].ToString();

			if (reader["complemento"] != DBNull.Value)
				entidade.Complemento = reader["complemento"].ToString();

			if (reader["numero"] != DBNull.Value)
				entidade.Numero = reader["numero"].ToString();

			if (reader["nomeEndereco"] != DBNull.Value)
				entidade.NomeEndereco = reader["nomeEndereco"].ToString();

			if (reader["municipioId"] != DBNull.Value)
			{
				entidade.Municipio = new Municipio();
				//entidade.Municipio.MunicipioId = Convert.ToInt32(reader["municipioId"].ToString());
				 MunicipioADO.PopulaMunicipio( reader, entidade.Municipio );
				 entidade.Municipio.Regiao = new Regiao();
				 RegiaoADO.PopulaRegiao( reader, entidade.Municipio.Regiao );
			}

			if (reader["enderecoTipoId"] != DBNull.Value)
			{
				entidade.EnderecoTipo = new EnderecoTipo();
				//entidade.EnderecoTipo.EnderecoTipoId = Convert.ToInt32(reader["enderecoTipoId"].ToString());
				EnderecoTipoADO.PopulaEnderecoTipo( reader, entidade.EnderecoTipo );
			}

			if (reader["usuarioId"] != DBNull.Value)
			{
				entidade.Usuario = new Usuario();
				entidade.Usuario.UsuarioId = Convert.ToInt32(reader["usuarioId"].ToString());
			}


        }

        public int ExcluiEnderecoPorUsuarioId(int usuarioId)
        {
            try
            {
                StringBuilder sbSQL = new StringBuilder();

                sbSQL.Append("DELETE FROM Endereco ");
                sbSQL.Append("WHERE usuarioId=@usuarioId ");

                DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

                _db.AddInParameter(command, "@usuarioId", DbType.Int64, usuarioId);

                // Executa a query.

                //int resultado = (int) _db.ExecuteScalar(command);
                _db.ExecuteNonQuery(command);

                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }


        #endregion

    }
}
		