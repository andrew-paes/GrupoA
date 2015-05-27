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
    public partial class ArquivoADO : ADOSuper, IArquivoDAL
    {
        /// <summary>
        /// Método que retorna uma coleção de Arquivo.
        /// </summary>
        /// <param name="entidade">ProdutoImagem relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Arquivo.</returns>
        public List<Arquivo> Carregar(Produto entidade)
        {
            List<Arquivo> entidadesRetorno = new List<Arquivo>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Arquivo.* FROM Arquivo INNER JOIN ProdutoImagem ON Arquivo.arquivoId=ProdutoImagem.arquivoId WHERE ProdutoImagem.produtoId=@produtoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);

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
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public Arquivo CarregarArquivoPorNome(Arquivo entidade)
        {
            Arquivo entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM Arquivo WHERE nomeArquivo=@nomeArquivo");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@nomeArquivo", DbType.String, entidade.NomeArquivo);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Arquivo();
                PopulaArquivo(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }
    }
}
