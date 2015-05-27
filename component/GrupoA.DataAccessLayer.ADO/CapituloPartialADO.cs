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
    public partial class CapituloADO : ADOSuper, ICapituloDAL
    {
        /// <summary>
        /// Método que carrega os Autores do Capitulo
        /// </summary>
        /// <param name="captiulo">Capitulo com identificador configurado.</param>
        /// <returns>Coleção de Autor associado ao Capitulo.</returns>
        public List<Autor> CarregarAutoresDoCapitulo(Capitulo captiulo)
        {
            List<Autor> autores = new List<Autor>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Autor.* FROM CapituloAutor ");
            sbSQL.Append("JOIN Autor ON Autor.autorId = CapituloAutor.autorId ");
            sbSQL.Append("WHERE capituloId=@capituloId ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@capituloId", DbType.Int32, captiulo.CapituloId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Autor entidadeRetorno = new Autor();
                AutorADO.PopulaAutor(reader, entidadeRetorno);
                autores.Add(entidadeRetorno);
            }

            reader.Close();

            return autores;
        }

        /// <summary>
        /// Método que exclui um Capitulo da base de dados.
        /// </summary>
        /// <param name="capitulo">Capitulo com identificador configurado.</param>		
        public void ExcluirCapituloAutor(Capitulo capitulo)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("DELETE FROM CapituloAutor ");
            sbSQL.Append("WHERE CapituloId=@CapituloId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@CapituloId", DbType.Int32, capitulo.CapituloId);

            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que persiste os relacionamentos entre Capitulo e Autor.
        /// </summary>
        /// <param name="capitulo">Capitulo com seu identificador configurado.</param>	
        /// <param name="autores">Coleção de Autor com seus identificadores configurados.</param>	
        public void InserirAutoresDeCapitulo(Capitulo capitulo, List<Autor> autores)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" INSERT INTO CapituloAutor ");
            sbSQL.Append(" (capituloId, autorId) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@capituloId, @autorId) ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            foreach (var autor in autores)
            {
                _db.AddInParameter(command, "@capituloId", DbType.Int32, capitulo.CapituloId);
                _db.AddInParameter(command, "@autorId", DbType.Int32, autor.AutorId);

                _db.ExecuteNonQuery(command);

            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public Capitulo CarregarPorCodigoLegado(Capitulo entidade)
        {
            Capitulo entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM Capitulo WHERE codigoLegado=@codigoLegado");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@codigoLegado", DbType.String, entidade.CodigoLegado);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Capitulo();
                PopulaCapitulo(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void AtualizarMenosNomeResumo(Capitulo entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE Capitulo SET ");
            sbSQL.Append(" numeroPaginaCapitulo=@numeroPaginaCapitulo, tituloId=@tituloId, codigoLegado=@codigoLegado ");
            sbSQL.Append(" WHERE capituloId=@capituloId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@capituloId", DbType.Int32, entidade.CapituloId);
            _db.AddInParameter(command, "@numeroPaginaCapitulo", DbType.Int32, entidade.NumeroPaginaCapitulo);
            _db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.Titulo.TituloId);

            if (entidade.CodigoLegado != null)
                _db.AddInParameter(command, "@codigoLegado", DbType.String, entidade.CodigoLegado);
            else
                _db.AddInParameter(command, "@codigoLegado", DbType.String, null);

            // Executa a query.
            _db.ExecuteNonQuery(command);
        }
    }
}