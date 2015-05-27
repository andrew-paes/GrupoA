using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.BusinessObject.ViewHelper;

namespace GrupoA.DataAccess.ADO
{
    public partial class TituloADO : ADOSuper, ITituloDAL
    {
        public bool flagConteudoHits = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="quantidadeRegistros"></param>
        /// <returns></returns>
        public IEnumerable<Titulo> CarregarItensSalaDeAula(Usuario usuario, Int32 quantidadeRegistros)
        {
            List<Titulo> entidadesRetorno = new List<Titulo>();

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            sbSQL.Append(" WITH   Categorias ( categoriaId, nomeCategoria, categoriaIdPai, Nivel ) ");
            sbSQL.Append(" AS ( SELECT   C.categoriaId , ");
            sbSQL.Append(" C.nomeCategoria , ");
            sbSQL.Append(" C.categoriaIdPai , ");
            sbSQL.Append(" 0 AS Nivel ");
            sbSQL.Append(" FROM     Categoria AS C ");
            sbSQL.Append(" WHERE    EXISTS ( SELECT UI.categoriaId ");
            sbSQL.Append("    FROM   dbo.UsuarioInteresse UI ");
            sbSQL.Append(" WHERE  C.categoriaId = UI.categoriaId ");
            sbSQL.Append(" AND usuarioId = @usuarioId ) ");
            sbSQL.Append(" UNION ALL ");
            sbSQL.Append(" SELECT   C.categoriaId , ");
            sbSQL.Append(" C.nomeCategoria , ");
            sbSQL.Append(" C.categoriaIdPai , ");
            sbSQL.Append(" Nivel + 1 ");
            sbSQL.Append(" FROM     Categoria AS C ");
            sbSQL.Append(" INNER JOIN Categorias AS CS ON c.CategoriaIdPai  ");
            sbSQL.Append(" = CS.categoriaId ");
            sbSQL.Append(" ), ");
            sbSQL.Append(" Produtos ( produtoId, tituloId ) ");
            sbSQL.Append(" AS ( SELECT   P.tituloImpressoId, p.tituloId AS ProdutoId ");
            sbSQL.Append(" FROM     dbo.TituloImpresso P INNER JOIN dbo.Produto PO  ");
            sbSQL.Append(" ON P.tituloImpressoId = PO.produtoId AND PO.exibirSite = 1 AND PO.homologado=1");
            sbSQL.Append(" UNION ALL ");
            sbSQL.Append(" SELECT   P.tituloEletronicoId, p.tituloId AS ProdutoId ");
            sbSQL.Append(" FROM     dbo.TituloEletronico P INNER JOIN dbo.Produto  ");
            sbSQL.Append(" PO ON P.tituloEletronicoId = PO.produtoId AND PO.exibirSite = 1 AND PO.homologado=1");
            sbSQL.Append(" ), ");
            sbSQL.Append(" Titulos ( tituloId, nomeTitulo, produtoId ) ");
            sbSQL.AppendFormat(" AS ( SELECT TOP {0} ", quantidadeRegistros);
            sbSQL.Append(" T.tituloId , ");
            sbSQL.Append(" T.nomeTitulo , ");
            sbSQL.Append(" P.produtoId ");
            sbSQL.Append(" FROM     dbo.Titulo T ");
            sbSQL.Append(" INNER JOIN Produtos P ON P.tituloId = T.tituloId ");
            sbSQL.Append(" INNER JOIN dbo.ProdutoCategoria PC ON  ");
            sbSQL.Append(" P.produtoId = PC.produtoId ");
            sbSQL.Append("  WHERE    EXISTS ( SELECT tituloId ");
            sbSQL.Append(" FROM    ");
            sbSQL.Append(" dbo.TituloConteudoExtraArquivo TCE ");
            sbSQL.Append(" WHERE  TCE.tituloId = T.tituloId ");
            sbSQL.Append(" AND TCE.restritoProfessor = 1 ) ");
            sbSQL.Append(" AND EXISTS ( SELECT categoriaId ");
            sbSQL.Append(" FROM   Categorias ");
            sbSQL.Append(" WHERE  Categorias.categoriaId =  ");
            sbSQL.Append(" PC.categoriaId ) ");
            sbSQL.Append("  ORDER BY NEWID() ");
            sbSQL.Append(" ) ");
            sbSQL.Append(" SELECT  T.* , ");
            sbSQL.Append(" PIA.arquivoId AS arquivoIdCapa , ");
            sbSQL.Append(" PIA.nomeArquivo AS nomeArquivoCapa , ");
            sbSQL.Append(" PIA.nomeArquivoOriginal AS nomeArquivoOriginalCapa , ");
            sbSQL.Append(" PIA.dataHoraUpload AS dataHoraUploadCapa , ");
            sbSQL.Append(" PIA.tamanhoArquivo AS tamanhoArquivoCapa , ");
            sbSQL.Append(" TCE.*, ");
            sbSQL.Append(" A.* ");
            sbSQL.Append(" FROM    Titulos T ");
            sbSQL.Append(" INNER JOIN dbo.TituloConteudoExtraArquivo TCE ON T.tituloId  ");
            sbSQL.Append(" = TCE.tituloId ");
            sbSQL.Append(" AND  ");
            sbSQL.Append(" restritoProfessor = 1 ");
            sbSQL.Append(" LEFT  JOIN dbo.ProdutoImagem PI ON PI.produtoId = T.produtoId ");
            sbSQL.Append(" AND  ");
            sbSQL.Append(" pi.produtoImagemTipoId = 1 ");
            sbSQL.Append(" LEFT  JOIN dbo.Arquivo PIA ON PI.arquivoId = PIA.arquivoId ");
            sbSQL.Append(" INNER JOIN dbo.Arquivo A ON TCE.arquivoId = A.arquivoId ");
            sbSQL.Append(" ORDER BY T.tituloId , ");
            sbSQL.Append(" t.nomeTitulo ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuario.UsuarioId);

            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Titulo entidadeRetorno = new Titulo();
                PopulaTituloSalaDeAula(reader, entidadeRetorno);

                if (entidadesRetorno.Count(n => n.TituloId.Equals(entidadeRetorno.TituloId)).Equals(0))
                    entidadesRetorno.Add(entidadeRetorno);

                TituloConteudoExtraArquivo conteudoExtra = new TituloConteudoExtraArquivo();
                TituloConteudoExtraArquivoADO.PopulaTituloConteudoExtraArquivo(reader, conteudoExtra);
                ArquivoADO.PopulaArquivo(reader, conteudoExtra.Arquivo);

                if (reader["nomeArquivoCapa"] != DBNull.Value)
                {
                    ProdutoImagem produtoImagem = new ProdutoImagem();
                    produtoImagem.Arquivo = new Arquivo();

                    produtoImagem.Arquivo.NomeArquivo = reader["nomeArquivoCapa"].ToString();

                    entidadeRetorno.TituloImpresso = new TituloImpresso();
                    entidadeRetorno.TituloImpresso.Produto = new Produto();
                    entidadeRetorno.TituloImpresso.Produto.ProdutoImagens = new List<ProdutoImagem>();
                    entidadeRetorno.TituloImpresso.Produto.ProdutoImagens.Add(produtoImagem);
                }

                var tituloTemp = from t in entidadesRetorno
                                 where t.TituloId == entidadeRetorno.TituloId
                                 select t;

                if (tituloTemp.Count() > 0)
                    tituloTemp.First().TituloConteudoExtraArquivos.Add(conteudoExtra);
            }
            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// Método que carrega um Titulo com suas dependências.
        /// </summary>
        /// <param name="entidade">Titulo a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Titulo</returns>
        public Titulo CarregarComInformacoesComplementares(Titulo entidade)
        {

            Titulo entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT titulo.*");
            sbSQL.Append(" FROM Titulo");
            sbSQL.Append(" WHERE Titulo.tituloId=@tituloId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.TituloId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Titulo();
                PopulaComInformacoesComplementares(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Método que retorna popula um Titulo baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Titulo a ser populado(.</param>
        public static void PopulaComInformacoesComplementares(IDataReader reader, Titulo entidade)
        {
            if (reader["subtituloLivro"] != DBNull.Value)
                entidade.SubtituloLivro = reader["subtituloLivro"].ToString();

            if (reader["numeroPaginas"] != DBNull.Value)
                entidade.NumeroPaginas = Convert.ToInt32(reader["numeroPaginas"].ToString());

            if (reader["edicao"] != DBNull.Value)
                entidade.Edicao = Convert.ToInt32(reader["edicao"].ToString());

            if (reader["dataLancamento"] != DBNull.Value)
                entidade.DataLancamento = Convert.ToDateTime(reader["dataLancamento"].ToString());

            if (reader["dataPublicacao"] != DBNull.Value)
                entidade.DataPublicacao = Convert.ToDateTime(reader["dataPublicacao"].ToString());

            if (reader["maisVendido"] != DBNull.Value)
                entidade.MaisVendido = Convert.ToBoolean(reader["maisVendido"].ToString());

            if (reader["nomeTitulo"] != DBNull.Value)
                entidade.NomeTitulo = reader["nomeTitulo"].ToString();

            if (reader["tituloId"] != DBNull.Value)
            {
                entidade.TituloId = Convert.ToInt32(reader["tituloId"].ToString());
            }

            //carrega info sobre autores e arquivo sobre autores
            TituloInformacaoSobreAutorADO tituloInformacaoSobreAutorADO = new TituloInformacaoSobreAutorADO();
            entidade.TituloInformacaoSobreAutor = tituloInformacaoSobreAutorADO.Carregar(new TituloInformacaoSobreAutor() { TituloInformacaoSobreAutorId = entidade.TituloId });
            if (entidade.TituloInformacaoSobreAutor != null && (entidade.TituloInformacaoSobreAutor.ArquivoImagem != null && entidade.TituloInformacaoSobreAutor.ArquivoImagem.ArquivoId > 0))
            {
                ArquivoADO arquivoADO = new ArquivoADO();
                entidade.TituloInformacaoSobreAutor.ArquivoImagem = arquivoADO.Carregar(entidade.TituloInformacaoSobreAutor.ArquivoImagem);
            }

            //carrega info sobre sumario e arquivo do sumario
            TituloInformacaoSumarioADO tituloInformacaoSumarioADO = new TituloInformacaoSumarioADO();
            entidade.TituloInformacaoSumario = tituloInformacaoSumarioADO.Carregar(new TituloInformacaoSumario() { TituloInformacaoSumarioId = entidade.TituloId });
            if (entidade.TituloInformacaoSumario != null && entidade.TituloInformacaoSumario.ArquivoSumario != null && entidade.TituloInformacaoSumario.ArquivoSumario.ArquivoId > 0)
            {
                ArquivoADO arquivoADO = new ArquivoADO();
                entidade.TituloInformacaoSumario.ArquivoSumario = arquivoADO.Carregar(entidade.TituloInformacaoSumario.ArquivoSumario);
            }

            //carrega info sobre resumo
            TituloInformacaoResumoADO tituloInformacaoResumoADO = new TituloInformacaoResumoADO();
            entidade.TituloInformacaoResumo = tituloInformacaoResumoADO.Carregar(new TituloInformacaoResumo() { TituloInformacaoResumoId = entidade.TituloId });

            TituloImagemResumoADO tituloImagemResumoADO = new TituloImagemResumoADO();
            entidade.TituloImagemResumos = tituloImagemResumoADO.Carregar(entidade).ToList();

            //carrega info sobre ficha técnica
            TituloInformacaoFichaADO tituloInformacaoFichaADO = new TituloInformacaoFichaADO();
            entidade.TituloInformacaoFicha = tituloInformacaoFichaADO.Carregar(new TituloInformacaoFicha() { TituloInformacaoFichaId = entidade.TituloId });

            //carrega info sobre material didatico
            TituloInformacaoMaterialDidaticoADO tituloInformacaoMaterialDidaticoADO = new TituloInformacaoMaterialDidaticoADO();
            entidade.TituloInformacaoMaterialDidatico = tituloInformacaoMaterialDidaticoADO.Carregar(new TituloInformacaoMaterialDidatico() { TituloInformacaoMaterialDidaticoId = entidade.TituloId });
        }

        /// <summary>
        /// Método que carrega um Titulo com comentario especialista.
        /// </summary>
        /// <param name="entidade">Titulo a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Titulo</returns>
        public Titulo CarregaTituloComComentarioDoEspecialista(Titulo entidade)
        {

            Titulo entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT Titulo.*");
            sbSQL.Append(" FROM Titulo");
            sbSQL.Append(" WHERE Titulo.tituloId=@tituloId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.TituloId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Titulo();
                PopulaComentarioEspecialista(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Método que retorna popula um Titulo baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Titulo a ser populado(.</param>
        public static void PopulaComentarioEspecialista(IDataReader reader, Titulo entidade)
        {
            if (reader["subtituloLivro"] != DBNull.Value)
                entidade.SubtituloLivro = reader["subtituloLivro"].ToString();

            if (reader["numeroPaginas"] != DBNull.Value)
                entidade.NumeroPaginas = Convert.ToInt32(reader["numeroPaginas"].ToString());

            if (reader["edicao"] != DBNull.Value)
                entidade.Edicao = Convert.ToInt32(reader["edicao"].ToString());

            if (reader["dataLancamento"] != DBNull.Value)
                entidade.DataLancamento = Convert.ToDateTime(reader["dataLancamento"].ToString());

            if (reader["dataPublicacao"] != DBNull.Value)
                entidade.DataPublicacao = Convert.ToDateTime(reader["dataPublicacao"].ToString());

            if (reader["maisVendido"] != DBNull.Value)
                entidade.MaisVendido = Convert.ToBoolean(reader["maisVendido"].ToString());

            if (reader["nomeTitulo"] != DBNull.Value)
                entidade.NomeTitulo = reader["nomeTitulo"].ToString();

            if (reader["tituloId"] != DBNull.Value)
            {
                entidade.TituloId = Convert.ToInt32(reader["tituloId"].ToString());
            }

            //carrega info sobre autores e arquivo sobre autores
            TituloInformacaoComentarioEspecialistaADO tituloInformacaoComentarioEspecialistaADO = new TituloInformacaoComentarioEspecialistaADO();
            entidade.TituloInformacaoComentarioEspecialista = tituloInformacaoComentarioEspecialistaADO.Carregar(new TituloInformacaoComentarioEspecialista() { TituloInformacaoComentarioEspecialistaId = entidade.TituloId });
            if (entidade.TituloInformacaoComentarioEspecialista != null && (entidade.TituloInformacaoComentarioEspecialista.ArquivoAudio != null && entidade.TituloInformacaoComentarioEspecialista.ArquivoAudio.ArquivoId > 0))
            {
                ArquivoADO arquivoADO = new ArquivoADO();
                entidade.TituloInformacaoComentarioEspecialista.ArquivoAudio = arquivoADO.Carregar(entidade.TituloInformacaoComentarioEspecialista.ArquivoAudio);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="titulo"></param>
        /// <returns></returns>
        public Titulo CarregaConteudoExtraMidiaURL(Titulo titulo)
        {
            Titulo entidadeTituloRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" SELECT * FROM ");
            sbSQL.Append(" TITULO ");
            sbSQL.Append(" LEFT JOIN TituloConteudoExtraMidia ON TituloConteudoExtraMidia.TituloConteudoExtraMidiaId = Titulo.TituloId ");
            sbSQL.Append(" LEFT JOIN TituloConteudoExtraUrl ON TituloConteudoExtraUrl.TituloConteudoExtraUrlId = Titulo.TituloId ");
            sbSQL.Append(" WHERE Titulo.tituloId=@tituloId ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloId", DbType.Int32, titulo.TituloId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeTituloRetorno = new Titulo();
                TituloADO.PopulaTitulo(reader, entidadeTituloRetorno);

                entidadeTituloRetorno.TituloConteudoExtraMidia = new TituloConteudoExtraMidia();
                TituloConteudoExtraMidiaADO.PopulaTituloConteudoExtraMidia(reader, entidadeTituloRetorno.TituloConteudoExtraMidia);

                entidadeTituloRetorno.TituloConteudoExtraUrl = new TituloConteudoExtraUrl();
                TituloConteudoExtraUrlADO.PopulaTituloConteudoExtraUrl(reader, entidadeTituloRetorno.TituloConteudoExtraUrl);
            }
            reader.Close();

            return entidadeTituloRetorno;
        }

        /// <summary>
        /// Método que retorna uma coleção de Titulo por Area de Destaque.
        /// </summary>
        /// <param name="areaDestaque">Identificador da area de destaque</param>
        ///  <returns>Retorna um List de Titulo.</returns>
        public IEnumerable<Titulo> CarregarTodosPorAreaDestaque(int destaqueTituloImpressoId)
        {

            List<Titulo> entidadesRetorno = new List<Titulo>();

            StringBuilder sbSQL = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            StringBuilder sbOrder = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            sbOrder.Append(" ORDER BY tituloId");

            sbSQL.Append("SELECT titulo.* FROM titulo ");
            sbSQL.Append("INNER JOIN dbo.DestaqueTituloImpressoRelacionado destaqueRelacionado ON destaqueRelacionado.tituloId = titulo.tituloId ");
            sbSQL.Append("INNER JOIN dbo.DestaqueTituloImpresso destaque ON destaqueRelacionado.destaqueTituloImpressoId = destaque.destaqueTituloImpressoId ");
            sbSQL.Append(string.Concat("WHERE destaque.destaqueTituloImpressoId = ", destaqueTituloImpressoId.ToString()));
            sbSQL.Append(sbOrder.ToString());


            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Titulo entidadeRetorno = new Titulo();
                PopulaTitulo(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// Método que popula um Titulo com suas dependências.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidadeRetorno"></param>
        private static void PopulaTituloComDependencia(IDataReader reader, Titulo entidadeRetorno)
        {
            PopulaTitulo(reader, entidadeRetorno);

            if (reader["isbn13TituloImpresso"] != DBNull.Value)
                entidadeRetorno.TituloImpresso.Isbn13 = reader["isbn13TituloImpresso"].ToString();
            if (reader["isbn13TituloEletronico"] != DBNull.Value)
                entidadeRetorno.TituloEletronico.Isbn13 = reader["isbn13TituloEletronico"].ToString();

            if (reader["tituloImpressoId"] != DBNull.Value)
                entidadeRetorno.TituloImpresso.TituloImpressoId = Convert.ToInt32(reader["tituloImpressoId"].ToString());

            if (reader["tituloEletronicoId"] != DBNull.Value)
                entidadeRetorno.TituloEletronico.TituloEletronicoId = Convert.ToInt32(reader["tituloEletronicoId"].ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="numeroItensSeremExibidos"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarLancamentosPorCategoria(Categoria categoria, int numeroItensSeremExibidos)
        {
            StringBuilder sbOrder = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();

            //sbOrder.Append(" ORDER BY t.dataLancamento DESC ");
            sbOrder.Append(" ORDER BY dataLancamento DESC, nomeTitulo ");

            sbWhere.Append(" WHERE p.exibirSite = 1 AND p.homologado=1 AND t.dataLancamento BETWEEN (GETDATE() - 45) AND (GETDATE() + 45) ");

            List<Categoria> categorias = null;

            if (categoria != null)
            {
                categorias = new List<Categoria>() { categoria };
            }

            return CarregarParaEstantePorCategoria(categorias, numeroItensSeremExibidos, sbOrder, sbWhere);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="numeroItensSeremExibidos"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarCompraConjuntaPorCategoria(Categoria categoria, int numeroItensSeremExibidos)
        {
            StringBuilder sbOrder = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();

            //sbOrder.Append(" ORDER BY t.dataLancamento DESC ");
            sbOrder.Append(" ORDER BY dataLancamento DESC ");

            sbWhere.Append("WHERE p.produtoTipoId = 1 ");
            sbWhere.Append("AND p.exibirSite = 1 ");
            sbWhere.Append("AND p.homologado=1 ");

            List<Categoria> categorias = null;

            if (categoria != null)
            {
                categorias = new List<Categoria>() { categoria };
            }

            return CarregarParaEstantePorCategoria(categorias, numeroItensSeremExibidos, sbOrder, sbWhere, false, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="numeroItensSeremExibidos"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarEBooksPorCategoria(Categoria categoria, int numeroItensSeremExibidos)
        {
            StringBuilder sbOrder = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();

            //sbOrder.Append(" ORDER BY t.dataLancamento DESC ");
            sbOrder.Append(" ORDER BY dataLancamento DESC ");

            sbWhere.Append("WHERE p.produtoTipoId = 2 ");
            sbWhere.Append("AND p.exibirSite = 1 ");
            sbWhere.Append("AND p.homologado=1 ");

            List<Categoria> categorias = null;

            if (categoria != null)
            {
                categorias = new List<Categoria>() { categoria };
            }

            return CarregarParaEstantePorCategoria(categorias, numeroItensSeremExibidos, sbOrder, sbWhere, true, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="numeroItensSeremExibidos"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarOfertasPorCategoria(Categoria categoria, int numeroItensSeremExibidos)
        {
            StringBuilder sbOrder = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();

            //sbOrder.Append(" ORDER BY t.dataLancamento DESC, t.nomeTitulo ");
            sbOrder.Append(" ORDER BY dataLancamento DESC, nomeTitulo ");

            sbWhere.Append("WHERE p.valorOferta > 0 ");
            sbWhere.Append("AND p.exibirSite = 1 ");
            sbWhere.Append("AND p.homologado=1 ");

            List<Categoria> categorias = null;

            if (categoria != null)
            {
                categorias = new List<Categoria>() { categoria };
            }

            return CarregarParaEstantePorCategoria(categorias, numeroItensSeremExibidos, sbOrder, sbWhere);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="numeroItensSeremExibidos"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarMaisVendidosPorCategoria(Categoria categoria, int numeroItensSeremExibidos)
        {
            StringBuilder sbOrder = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();

            //sbOrder.Append(" ORDER BY t.dataLancamento DESC ");
            sbOrder.Append(" ORDER BY maisVendidoOrdem ");

            sbWhere.Append("WHERE p.exibirSite = 1 ");
            sbWhere.Append("AND p.homologado=1 ");
            sbWhere.Append("AND t.maisVendido = 1 ");

            List<Categoria> categorias = null;

            if (categoria != null)
            {
                categorias = new List<Categoria>() { categoria };
            }

            return CarregarParaEstantePorCategoria(categorias, numeroItensSeremExibidos, sbOrder, sbWhere);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="numeroItensSeremExibidos"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregaTitulosParaEstantePorCategoria(List<Categoria> categorias, int numeroItensSeremExibidos, bool somenteEbook)
        {

            StringBuilder sbOrder = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();

            sbWhere.Append("WHERE p.exibirSite = 1 ");
            sbWhere.Append("AND p.homologado=1 ");

            return CarregarParaEstantePorCategoria(categorias, numeroItensSeremExibidos, sbOrder, sbWhere, somenteEbook, false);
        }

        /// <summary>
        /// Método que retorna as informações de produtos do tipo título para serem exibidos nas estantes.
        /// </summary>
        /// <param name="categorias">Categorias pai do catálogo de produtos.</param>
        /// <returns>Coleção de EstanteTituloVH.</returns>
        public List<EstanteTituloVH> CarregaTitulosParaEstantePorCategoria(List<Categoria> categorias, bool somenteEbook)
        {
            StringBuilder sbOrder = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            sbWhere.Append("WHERE p.exibirSite = 1 ");
            sbWhere.Append("AND p.homologado=1 ");

            return CarregarParaEstantePorCategoria(categorias, 0, sbOrder, sbWhere, somenteEbook, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categorias"></param>
        /// <param name="numeroItensSeremExibidos"></param>
        /// <param name="ordermDaPesquisa"></param>
        /// <param name="whereDaConsulta"></param>
        /// <param name="somenteEbook"></param>
        /// <param name="somenteCompraConjunta"></param>
        /// <returns></returns>
        private List<EstanteTituloVH> CarregarParaEstantePorCategoria(
                                                                    List<Categoria> categorias,
                                                                    int numeroItensSeremExibidos,
                                                                    StringBuilder ordermDaPesquisa,
                                                                    StringBuilder whereDaConsulta,
                                                                    bool somenteEbook = false,
                                                                    bool somenteCompraConjunta = false
                                                                    )
        {
            return this.CarregaTitulosParaEstantePorCategoria(categorias, numeroItensSeremExibidos, ordermDaPesquisa, whereDaConsulta, somenteEbook, somenteCompraConjunta);
        }

        /// <summary>
        /// Carrega os títulos associados a uma categoria incluindo ou não as suas subcategorias.
        /// </summary>
        /// <param name="categorias">Coleção de categoria com identificador configurado.</param>
        /// <param name="numeroMaximoDeRegistros">número de itens a serem retornado na pesquisa.</param>
        /// <param name="ordermDaPesquisa"></param>
        /// <param name="whereDaConsulta"></param>
        /// <param name="somenteEbook">booleano informando se devemo trazer somente Ebooks.</param>
        /// <param name="somenteCompraConjunta">booleano informando se devemos trazer somente compra conjunta.</param>
        /// <returns></returns>
        private List<EstanteTituloVH> CarregaTitulosParaEstantePorCategoria(
                                                                            List<Categoria> categorias,
                                                                            int numeroMaximoDeRegistros,
                                                                            StringBuilder ordermDaPesquisa,
                                                                            StringBuilder whereDaConsulta,
                                                                            bool somenteEbook,
                                                                            bool somenteCompraConjunta
                                                                            )
        {
            List<EstanteTituloVH> titulosParaEstante = new List<EstanteTituloVH>();

            StringBuilder sbSql = new StringBuilder();
            StringBuilder sbOrder = ordermDaPesquisa;
            StringBuilder sbWhere = whereDaConsulta;
            DbCommand command;
            IDataReader reader;

            if (categorias != null && categorias.Count > 0)
            {
                string idsDasCategorias = categorias.Select(categoria => categoria.CategoriaId.ToString()).Aggregate((anterior, proximo) => anterior + ", " + proximo);

                sbSql.Append(" WITH Categorias (categoriaId, nomeCategoria, categoriaIdPai, nivel ) AS ( ");
                sbSql.Append(" SELECT C.categoriaId, C.nomeCategoria, C.categoriaIdPai, 0 AS Nivel ");
                sbSql.Append(" FROM Categoria AS C ");
                sbSql.AppendFormat(" WHERE C.categoriaId IN ({0}) ", idsDasCategorias);
                sbSql.Append(" UNION ALL ");
                sbSql.Append(" SELECT C.categoriaId, C.nomeCategoria, C.categoriaIdPai, Nivel+1 ");
                sbSql.Append(" FROM Categoria AS C ");
                sbSql.Append(" INNER JOIN Categorias AS CS ");
                sbSql.Append(" ON c.CategoriaIdPai = CS.categoriaId ) ");
            }

            if (numeroMaximoDeRegistros > 0)
            {
                sbSql.AppendFormat(" SELECT TOP {0} * FROM (SELECT DISTINCT p.produtoId, p.Disponivel, t.tituloId, t.nomeTitulo, t.subtituloLivro, arq.nomeArquivo, {1} eCompraConjunta,", numeroMaximoDeRegistros, (somenteCompraConjunta ? "1" : "0"));
            }
            else
            {
                sbSql.AppendFormat(" SELECT * FROM (SELECT DISTINCT p.produtoId, p.Disponivel, t.tituloId, t.nomeTitulo, t.subtituloLivro, arq.nomeArquivo, {0} eCompraConjunta,", (somenteCompraConjunta ? "1" : "0"));
            }

            if (flagConteudoHits)
            {
                sbSql.Append(" ch.hits,");
                flagConteudoHits = false;
            }

            sbSql.Append(" CASE ISNULL(p.valorOferta,0) WHEN 0 THEN p.valorUnitario ELSE p.valorOferta END AS valor, PC.CategoriaId, t.dataLancamento, t.maisVendidoOrdem FROM Produto p ");

            // Se for eBook troca o relacionamento
            if (somenteEbook)
            {
                sbSql.Append(" INNER JOIN TituloEletronico ti ON ti.tituloEletronicoId = p.produtoId ");
            }
            else
            {
                sbSql.Append(" INNER JOIN TituloImpresso ti ON ti.tituloImpressoId = p.produtoId ");
            }

            // Se for Compra Conjunta deve inserir mais um relacionamento
            if (somenteCompraConjunta)
            {
                sbSql.Append(" JOIN CompraConjunta cc ON p.produtoId = cc.produtoId AND cc.ativa = 1 AND cc.dataHoraFinalizacao is null AND cc.compraconjuntastatusId = 1 AND (cc.dataInicialCompra <= GETDATE() AND cc.dataFinalCompra >= GETDATE())");
            }

            sbSql.Append(" INNER JOIN Titulo t ON t.tituloId = ti.tituloId ");
            sbSql.Append(" INNER JOIN TituloAutor ON TituloAutor.tituloId = t.tituloId ");
            sbSql.Append(" INNER JOIN Autor ON TituloAutor.autorId = Autor.autorId ");
            sbSql.Append(" INNER JOIN ProdutoSelo ON ProdutoSelo.produtoId = p.produtoId ");
            sbSql.Append(" LEFT JOIN ProdutoImagem tImg ON tImg.produtoId = p.produtoId AND tImg.produtoImagemTipoId = 1 ");
            sbSql.Append(" LEFT JOIN Arquivo arq ON arq.arquivoId = tImg.arquivoId ");
            sbSql.Append(" INNER JOIN dbo.ProdutoCategoria PC ON pc.produtoId = p.produtoId ");

            if (categorias != null && categorias.Count > 0)
            {
                sbSql.Append(" INNER JOIN Categorias CS ON cs.categoriaId = pc.categoriaId ");
            }

            sbSql.Append(sbWhere.ToString());

            sbSql.Append(" ) AS Tabela "); //--------

            sbSql.Append(sbOrder.ToString());

            command = _db.GetSqlStringCommand(sbSql.ToString());

            //@categoriaId
            if (categorias != null && categorias.Count > 0)
            {
                _db.AddInParameter(command, "@categoriaId", DbType.Int32, categorias[0].CategoriaId);
            }

            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                EstanteTituloVH estanteTituloVh = new EstanteTituloVH();
                PopulaEstanteTitulo(reader, estanteTituloVh);
                titulosParaEstante.Add(estanteTituloVh);
            }

            reader.Close();

            return titulosParaEstante;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaArtigo"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarTitulosRelacionadosRevistaArtigo(RevistaArtigo revistaArtigo)
        {
            List<EstanteTituloVH> titulosParaEstante = new List<EstanteTituloVH>();

            StringBuilder sbSql = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            sbSql.Append(@"SELECT TOP 5 * FROM
                            (
	                            SELECT DISTINCT Produto.produtoId,
	                                   Produto.Disponivel,
		                               Titulo.tituloId,
		                               Titulo.nomeTitulo,
		                               Titulo.subtituloLivro,
		                               Arquivo.nomeArquivo,
		                               0 eCompraConjunta,
		                               CASE ISNULL(Produto.valorOferta,0) WHEN 0 THEN Produto.valorUnitario ELSE Produto.valorOferta END AS valor,
		                               ProdutoCategoria.CategoriaId,
		                               Titulo.dataLancamento,
		                               Titulo.maisVendidoOrdem
	                            FROM Produto
	                            INNER JOIN TituloImpresso ON TituloImpresso.tituloImpressoId = Produto.produtoId
	                            INNER JOIN Titulo ON Titulo.tituloId = TituloImpresso.tituloId
	                            INNER JOIN TituloAutor ON TituloAutor.tituloId = Titulo.tituloId
	                            INNER JOIN Autor ON TituloAutor.autorId = Autor.autorId
	                            INNER JOIN ProdutoSelo ON ProdutoSelo.produtoId = Produto.produtoId
	                            INNER JOIN ProdutoCategoria ON ProdutoCategoria.produtoId = Produto.produtoId
	                            INNER JOIN RevistaArtigoProduto ON RevistaArtigoProduto.produtoId = Produto.produtoId
	                            LEFT JOIN ProdutoImagem ON ProdutoImagem.produtoId = Produto.produtoId AND ProdutoImagem.produtoImagemTipoId = 1
	                            LEFT JOIN Arquivo ON Arquivo.arquivoId = ProdutoImagem.arquivoId
	                            WHERE Produto.produtoTipoId = 1
		                              AND Produto.exibirSite = 1
		                              AND Produto.homologado=1
		                              AND RevistaArtigoProduto.revistaArtigoId = @revistaArtigoId
	                            UNION ALL
	                            SELECT DISTINCT Produto.produtoId,
	                                   Produto.Disponivel,
		                               Titulo.tituloId,
		                               Titulo.nomeTitulo,
		                               Titulo.subtituloLivro,
		                               Arquivo.nomeArquivo,
		                               0 eCompraConjunta,
		                               CASE ISNULL(Produto.valorOferta,0) WHEN 0 THEN Produto.valorUnitario ELSE Produto.valorOferta END AS valor,
		                               ProdutoCategoria.CategoriaId,
		                               Titulo.dataLancamento,
		                               Titulo.maisVendidoOrdem
	                            FROM Produto
	                            INNER JOIN TituloEletronico ON TituloEletronico.tituloEletronicoId = Produto.produtoId
	                            INNER JOIN Titulo ON Titulo.tituloId = TituloEletronico.tituloId
	                            INNER JOIN TituloAutor ON TituloAutor.tituloId = Titulo.tituloId
	                            INNER JOIN Autor ON TituloAutor.autorId = Autor.autorId
	                            INNER JOIN ProdutoSelo ON ProdutoSelo.produtoId = Produto.produtoId
	                            INNER JOIN ProdutoCategoria ON ProdutoCategoria.produtoId = Produto.produtoId
	                            INNER JOIN RevistaArtigoProduto ON RevistaArtigoProduto.produtoId = Produto.produtoId
	                            LEFT JOIN ProdutoImagem ON ProdutoImagem.produtoId = Produto.produtoId AND ProdutoImagem.produtoImagemTipoId = 1
	                            LEFT JOIN Arquivo ON Arquivo.arquivoId = ProdutoImagem.arquivoId
	                            WHERE Produto.produtoTipoId = 2
		                              AND Produto.exibirSite = 1
		                              AND Produto.homologado=1
		                              AND RevistaArtigoProduto.revistaArtigoId = @revistaArtigoId
                            ) AS Tabela  ORDER BY NEWID()");

            command = _db.GetSqlStringCommand(sbSql.ToString());

            _db.AddInParameter(command, "@revistaArtigoId", DbType.Int32, revistaArtigo.RevistaArtigoId);

            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                EstanteTituloVH estanteTituloVh = new EstanteTituloVH();
                PopulaEstanteTitulo(reader, estanteTituloVh);
                titulosParaEstante.Add(estanteTituloVh);
            }

            reader.Close();

            return titulosParaEstante;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaConhecimentoId"></param>
        /// <returns></returns>
        private string RetornaSqlCategoriaPorAreaConhecimento(int areaConhecimentoId)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.AppendFormat(" SELECT categoriaid FROM Categoria where categoriaIdPai = {0}", areaConhecimentoId);
            return sbSQL.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="qtdRegistros"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarRecomendadosPorCategoria(Categoria categoria, Int32 qtdRegistros)
        {
            return this.CarregarRecomendadosPorCategoria(categoria, null, qtdRegistros);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="qtdRegistros"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarRecomendadosPorCategoria(Usuario usuario, Int32 qtdRegistros)
        {
            return this.CarregarRecomendadosPorCategoria(null, usuario, qtdRegistros);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarRecomendadosPorCategoria(Categoria categoria, Usuario usuario, Int32 qtdRegistros)
        {
            StringBuilder sbOrder = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            List<Categoria> categorias = null;

            //sbOrder.Append(" ORDER BY t.dataLancamento DESC ");
            sbOrder.Append(" ORDER BY dataLancamento DESC ");

            sbWhere.Append(" INNER JOIN DestaqueTituloImpressoRelacionado dtir ON dtir.tituloId = t.tituloId ");
            sbWhere.Append(" INNER JOIN DestaqueTituloImpresso dti ON dti.destaqueTituloImpressoId = dtir.destaqueTituloImpressoId ");

            if (categoria != null && categoria.CategoriaId > 0)
            {
                if (categoria != null)
                {
                    categorias = new List<Categoria>();
                    categorias.Add(categoria);
                }

                sbWhere.Append("WHERE dti.destaqueTituloImpressoId = @categoriaId "); //DESTAQUE CATEGORIA
            }
            else
            {
                if (usuario == null)
                {
                    sbWhere.Append("WHERE dti.destaqueTituloImpressoId = 6 "); //DESTAQUE HOME
                }
                else
                {
                    categorias = usuario.Categorias;

                    if (usuario.Perfis[0].PerfilId == 1)
                    {
                        sbWhere.Append("WHERE dti.destaqueTituloImpressoId = 5 "); //DESTAQUE HOME
                    }
                    else
                    {
                        sbWhere.Append("WHERE dti.destaqueTituloImpressoId = 4 "); //DESTAQUE HOME
                    }
                }
            }

            sbWhere.Append("AND p.produtoTipoId = 1 ");
            sbWhere.Append("AND p.exibirSite = 1 ");
            sbWhere.Append("AND p.homologado=1 ");

            return CarregarParaEstantePorCategoria(categorias, qtdRegistros, sbOrder, sbWhere);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaDeConhecimentoId"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarRecomendadosPorArea(int areaDeConhecimentoId)
        {
            StringBuilder sbOrder = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();

            //sbOrder.Append(" ORDER BY t.dataLancamento DESC ");
            sbOrder.Append(" ORDER BY dataLancamento DESC ");

            sbWhere.Append(" INNER JOIN DestaqueTituloImpressoRelacionado dtir ON dtir.tituloId = t.tituloId ");
            sbWhere.Append(" INNER JOIN DestaqueTituloImpresso dti ON dti.destaqueTituloImpressoId = dtir.destaqueTituloImpressoId ");

            sbWhere.Append("WHERE dti.destaqueTituloImpressoId = 1 "); //DESTAQUE CATEGORIA
            sbWhere.Append("AND p.produtoTipoId = 1 ");
            sbWhere.Append("AND p.exibirSite = 1 ");
            sbWhere.Append("AND p.homologado=1 ");

            return CarregaTitulosParaEstantePorCategoria(new List<Categoria>() { new Categoria() { CategoriaId = areaDeConhecimentoId } }, 7, sbOrder, sbWhere, false, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="qtdRegistros"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarMaisVistos(Usuario usuario, Int32 qtdRegistros)
        {
            return this.CarregarMaisVistos(null, usuario, qtdRegistros);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="qtdRegistros"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarMaisVistos(Categoria categoria, Int32 qtdRegistros)
        {
            return this.CarregarMaisVistos(categoria, null, qtdRegistros);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarMaisVistos(Categoria categoria, Usuario usuario, Int32 qtdRegistros)
        {
            StringBuilder sbOrder = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();

            //sbOrder.Append(" ORDER BY ch.hits DESC ");
            sbOrder.Append(" ORDER BY hits DESC ");

            sbWhere.Append(" INNER JOIN ConteudoHits ch ON ch.conteudoId = p.produtoId ");

            sbWhere.Append("AND p.produtoTipoId = 1 ");
            sbWhere.Append("AND p.exibirSite = 1 ");
            sbWhere.Append("AND p.homologado=1 ");

            flagConteudoHits = true;

            List<Categoria> categorias = null;

            if (categoria != null)
            {
                categorias = new List<Categoria>();
                categorias.Add(categoria);
            }
            else if (usuario != null)
            {
                categorias = usuario.Categorias;
            }

            return CarregarParaEstantePorCategoria(categorias, qtdRegistros, sbOrder, sbWhere);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaDeConhecimentoId"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarMaisVistosPorArea(int areaDeConhecimentoId)
        {
            StringBuilder sbOrder = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();

            //sbOrder.Append(" ORDER BY ch.hits DESC ");
            sbOrder.Append(" ORDER BY hits DESC ");

            sbWhere.Append(" INNER JOIN ConteudoHits ch ON ch.conteudoId = p.produtoId ");

            sbWhere.Append("AND p.produtoTipoId = 1 ");
            sbWhere.Append("AND p.exibirSite = 1 ");
            sbWhere.Append("AND p.homologado=1 ");

            flagConteudoHits = true;

            return CarregaTitulosParaEstantePorCategoria(new List<Categoria>() { new Categoria() { CategoriaId = areaDeConhecimentoId } }, 7, sbOrder, sbWhere, false, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaDeConhecimentoId"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarTitulosRelacionadosPorArea(int produtoId, Int32 areaDeConhecimentoId, int numeroMaximoDeRegistros)
        {

            if (areaDeConhecimentoId <= 0)
                throw new ArgumentException("Categoria inválida!");

            List<EstanteTituloVH> titulosParaEstante = new List<EstanteTituloVH>();

            StringBuilder sbSQL = new StringBuilder();
            StringBuilder sbOrder = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            sbSQL.Append("WITH ProdutoTitulo (ProdutoId, TituloId) AS ");
            sbSQL.Append("( ");

            //-- Busca os títulos eletrônicos e impressos que estão ativos no site.
            sbSQL.Append("    SELECT P.produtoId, T.tituloId FROM dbo.Produto P ");
            sbSQL.Append("INNER JOIN dbo.TituloEletronico T ON p.produtoId = t.tituloEletronicoId ");
            sbSQL.Append("    WHERE P.exibirSite = 1 AND P.homologado=1");
            sbSQL.Append("    AND P.produtoId <> @produtoId ");
            sbSQL.Append("    UNION ALL ");
            sbSQL.Append("    SELECT P.produtoId, T.tituloId FROM dbo.Produto P ");
            sbSQL.Append("    INNER JOIN dbo.TituloImpresso T ON p.produtoId = t.TituloImpressoId ");
            sbSQL.Append("    WHERE P.exibirSite = 1 AND P.homologado=1");
            sbSQL.Append("    AND P.produtoId <> @produtoId ");
            sbSQL.Append("), ");
            sbSQL.Append("Categorias (categoriaId, nomeCategoria, categoriaIdPai, nivel ) AS ");
            sbSQL.Append("( ");

            //-- Definição do membro ancora.
            sbSQL.Append("    SELECT C.categoriaId, C.nomeCategoria, C.categoriaIdPai, 0 AS Nivel ");
            sbSQL.Append("    FROM Categoria AS C ");
            sbSQL.Append("    WHERE C.categoriaId = @areaDeConhecimentoId");
            sbSQL.Append("    UNION ALL ");

            //-- Definição do membro recursivo.
            sbSQL.Append("    SELECT C.categoriaId, C.nomeCategoria, C.categoriaIdPai, Nivel+1 ");
            sbSQL.Append("    FROM Categoria AS C ");
            sbSQL.Append("    INNER JOIN Categorias AS CS ");
            sbSQL.Append("        ON c.CategoriaIdPai = CS.categoriaId    ");
            sbSQL.Append(") ");

            sbSQL.AppendFormat("SELECT TOP {0} P.produtoId, P.disponivel, T.tituloId, T.nomeTitulo, T.subtituloLivro, ARQ.nomeArquivo, 0 as eCompraConjunta, ", numeroMaximoDeRegistros);
            sbSQL.Append("CASE ISNULL(P.valorOferta,0) WHEN 0 THEN P.valorUnitario ELSE P.valorOferta END AS valor, PC.CategoriaId FROM ");
            sbSQL.Append("Categorias C ");
            sbSQL.Append("INNER JOIN dbo.ProdutoCategoria PC ON c.categoriaId = pc.categoriaId ");
            sbSQL.Append("INNER JOIN dbo.Produto P ON  PC.produtoId = P.produtoId ");
            sbSQL.Append("INNER JOIN ProdutoTitulo PT ON PT.ProdutoId = P.produtoId ");
            sbSQL.Append("INNER JOIN Titulo T ON T.tituloId = PT.TituloId ");
            sbSQL.Append("LEFT JOIN ProdutoImagem TI ON TI.produtoId = P.produtoId AND TI.produtoImagemTipoId = 1 ");
            sbSQL.Append("LEFT JOIN Arquivo ARQ ON ARQ.arquivoId = TI.arquivoId ");

            sbSQL.Append("ORDER BY NEWID() ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@areaDeConhecimentoId", DbType.Int32, areaDeConhecimentoId);
            _db.AddInParameter(command, "@produtoId", DbType.Int32, produtoId);
            reader = _db.ExecuteReader(command);


            while (reader.Read())
            {
                EstanteTituloVH estanteTituloVh = new EstanteTituloVH();
                PopulaEstanteTitulo(reader, estanteTituloVh);
                titulosParaEstante.Add(estanteTituloVh);
            }
            reader.Close();

            return titulosParaEstante;
        }

        /// <summary>
        /// Método que retorna popula um Titulo baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">EstanteTituloVH a ser populado(.</param>
        public static void PopulaEstanteTitulo(IDataReader reader, EstanteTituloVH entidade)
        {
            if (reader["tituloId"] != DBNull.Value)
            {
                entidade.IdTitulo = Convert.ToInt32(reader["tituloId"].ToString());
                // Carrega Autores
                entidade.Autores = new AutorADO().CarregarAutores(new Titulo() { TituloId = entidade.IdTitulo }, 1);
            }
            if (reader["nomeTitulo"] != DBNull.Value)
            {
                entidade.Titulo = reader["nomeTitulo"].ToString();
            }
            if (reader["subtituloLivro"] != DBNull.Value)
            {
                entidade.SubTitulo = reader["subtituloLivro"].ToString();
            }
            if (reader["nomeArquivo"] != DBNull.Value)
            {
                entidade.Arquivo = reader["nomeArquivo"].ToString();
            }
            if (reader["valor"] != DBNull.Value)
            {
                entidade.Valor = Convert.ToDouble(reader["valor"].ToString());
            }
            if (reader["produtoId"] != DBNull.Value)
            {
                entidade.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());
            }

            if (reader["disponivel"] != DBNull.Value)
            {
                entidade.Disponivel = Convert.ToBoolean(reader["disponivel"]);
            }

            if (reader["CategoriaId"] != DBNull.Value)
            {
                entidade.CategoriaId = reader["CategoriaId"].ToString();
            }
            if (reader["eCompraConjunta"] != null
                && reader["eCompraConjunta"] != DBNull.Value)
            {
                entidade.eCompraConjunta = (reader["eCompraConjunta"].ToString().Equals("1") ? true : false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaDeConhecimentoId"></param>
        /// <returns></returns>
        public List<TituloInformacaoComentarioEspecialista> CarregarTitulosPorAreaDeConhecimento(int areaDeConhecimentoId, int numeroMaximoRegistros)
        {
            List<TituloInformacaoComentarioEspecialista> entidadesRetorno = new List<TituloInformacaoComentarioEspecialista>();

            StringBuilder sbSQL = new StringBuilder();
            StringBuilder sbOrder = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            sbSQL.Append("WITH ProdutoTitulo (ProdutoId, TituloId) AS ");
            sbSQL.Append("( ");

            //-- Busca os títulos eletrônicos e impressos que estão ativos no site.
            sbSQL.Append("    SELECT P.produtoId, T.tituloId FROM dbo.Produto P ");
            sbSQL.Append("INNER JOIN dbo.TituloEletronico T ON p.produtoId = t.tituloEletronicoId ");
            sbSQL.Append("    WHERE P.exibirSite = 1 AND P.homologado=1");
            sbSQL.Append("    UNION ALL ");
            sbSQL.Append("    SELECT P.produtoId, T.tituloId FROM dbo.Produto P ");
            sbSQL.Append("    INNER JOIN dbo.TituloImpresso T ON p.produtoId = t.TituloImpressoId ");
            sbSQL.Append("    WHERE P.exibirSite = 1 AND P.homologado=1");
            sbSQL.Append("), ");
            sbSQL.Append("Categorias (categoriaId, nomeCategoria, categoriaIdPai, nivel ) AS ");
            sbSQL.Append("( ");

            //-- Definição do membro ancora.
            sbSQL.Append("    SELECT C.categoriaId, C.nomeCategoria, C.categoriaIdPai, 0 AS Nivel ");
            sbSQL.Append("    FROM Categoria AS C ");
            sbSQL.Append("    WHERE C.categoriaId = @areaDeConhecimentoId");
            sbSQL.Append("    UNION ALL ");

            //-- Definição do membro recursivo.
            sbSQL.Append("    SELECT C.categoriaId, C.nomeCategoria, C.categoriaIdPai, Nivel+1 ");
            sbSQL.Append("    FROM Categoria AS C ");
            sbSQL.Append("    INNER JOIN Categorias AS CS ");
            sbSQL.Append("        ON c.CategoriaIdPai = CS.categoriaId    ");
            sbSQL.Append(") ");

            sbSQL.Append("SELECT TOP " + numeroMaximoRegistros + " * FROM( ");
            sbSQL.Append("SELECT DISTINCT TCE.*, ARQIMA.nomeArquivo as nomeArquivoImagem, ARQSOM.nomeArquivo as nomeArquivoSom FROM ");
            sbSQL.Append("Categorias C ");
            sbSQL.Append("INNER JOIN dbo.ProdutoCategoria PC ON c.categoriaId = pc.categoriaId ");
            sbSQL.Append("INNER JOIN ProdutoTitulo PT ON PT.ProdutoId = PC.produtoId ");
            sbSQL.Append("INNER JOIN Titulo T ON t.tituloId = PT.TituloId ");
            sbSQL.Append("INNER JOIN dbo.TituloInformacaoComentarioEspecialista TCE ON TCE.tituloInformacaoComentarioEspecialistaId = T.TituloId ");
            sbSQL.Append("LEFT JOIN Arquivo ARQIMA  ON TCE.arquivoIdImagem=ARQIMA.arquivoId ");
            sbSQL.Append("LEFT JOIN Arquivo ARQSOM  ON TCE.arquivoIdAudio=ARQSOM.arquivoId ");

            sbSQL.Append("WHERE ");
            sbSQL.Append("tce.destaqueAreaConhecimento = 1 ");
            sbSQL.Append(") as r ORDER BY NEWID() ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@areaDeConhecimentoId", DbType.Int32, areaDeConhecimentoId);
            //_db.AddInParameter(command, "@numeroMaximoRegistros", DbType.Int32, numeroMaximoRegistros);

            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                TituloInformacaoComentarioEspecialista entidadeRetorno = new TituloInformacaoComentarioEspecialista();
                PopulaTituloInformacaoComentarioEspecialista(reader, entidadeRetorno);

                if (reader["nomeArquivoImagem"] != DBNull.Value)
                    entidadeRetorno.ArquivoImagem.NomeArquivo = reader["nomeArquivoImagem"].ToString();

                if (reader["nomeArquivoSom"] != DBNull.Value)
                    entidadeRetorno.ArquivoAudio.NomeArquivo = reader["nomeArquivoSom"].ToString();

                if (reader["comentarioFormatoId"] != DBNull.Value)
                {
                    ComentarioFormatoADO comentarioFormatoADO = new ComentarioFormatoADO();
                    entidadeRetorno.ComentarioFormato = comentarioFormatoADO.Carregar(Convert.ToInt32(reader["comentarioFormatoId"]));
                }


                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        public static void PopulaTituloInformacaoComentarioEspecialista(IDataReader reader, TituloInformacaoComentarioEspecialista entidade)
        {
            if (reader["textoComentario"] != DBNull.Value)
                entidade.TextoComentario = reader["textoComentario"].ToString();

            if (reader["resumoComentario"] != DBNull.Value)
                entidade.ResumoComentario = reader["resumoComentario"].ToString();

            if (reader["tituloComentario"] != DBNull.Value)
                entidade.TituloComentario = reader["tituloComentario"].ToString();

            if (reader["urlMidia"] != DBNull.Value)
                entidade.UrlMidia = reader["urlMidia"].ToString();

            if (reader["destaqueAreaConhecimento"] != DBNull.Value)
                entidade.DestaqueAreaConhecimento = Convert.ToBoolean(reader["destaqueAreaConhecimento"].ToString());

            if (reader["nomeEspecialista"] != DBNull.Value)
                entidade.NomeEspecialista = reader["nomeEspecialista"].ToString();

            if (reader["especialidade"] != DBNull.Value)
                entidade.Especialidade = reader["especialidade"].ToString();

            if (reader["tituloInformacaoComentarioEspecialistaId"] != DBNull.Value)
            {
                entidade.TituloInformacaoComentarioEspecialistaId = Convert.ToInt32(reader["tituloInformacaoComentarioEspecialistaId"].ToString());
            }

            if (reader["arquivoIdAudio"] != DBNull.Value)
            {
                entidade.ArquivoAudio = new Arquivo();
                entidade.ArquivoAudio.ArquivoId = Convert.ToInt32(reader["arquivoIdAudio"].ToString());
            }

            if (reader["arquivoIdImagem"] != DBNull.Value)
            {
                entidade.ArquivoImagem = new Arquivo();
                entidade.ArquivoImagem.ArquivoId = Convert.ToInt32(reader["arquivoIdImagem"].ToString());
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        private static void PopulaTituloSalaDeAula(IDataReader reader, Titulo entidade)
        {
            if (reader["nomeTitulo"] != DBNull.Value)
                entidade.NomeTitulo = reader["nomeTitulo"].ToString();

            if (reader["tituloId"] != DBNull.Value)
            {
                entidade.TituloId = Convert.ToInt32(reader["tituloId"].ToString());
            }

            entidade.TituloConteudoExtraArquivos = new List<TituloConteudoExtraArquivo>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrosPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="ordemColunas"></param>
        /// <param name="ordemSentidos"></param>
        /// <param name="usuario"></param>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public List<TituloVH> CarregarTituloComMaterialExtraArquivo(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, Usuario usuario, Categoria categoria)
        {
            List<TituloVH> entidadesRetorno = new List<TituloVH>();

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
                if (sbOrder.Length > 0) { sbOrder.Insert(0, " order by "); }
            }

            sbSQL.Append("WITH  Produtos ( produtoId, tituloId, tipo, valorUnitario, valorOferta, valor, parcelas, taxaJuros, identificadorArea, categoriaId, disponivel )");
            sbSQL.Append("      AS ( SELECT   P.tituloImpressoId ,");
            sbSQL.Append("                    p.tituloId AS ProdutoId,");
            sbSQL.Append("                    'Livro Impresso',");
            sbSQL.Append("                    PO.valorUnitario,");
            sbSQL.Append("                    PO.valorOferta,");
            sbSQL.Append("                    CASE WHEN PO.valorOferta>0 THEN PO.valorOferta ELSE PO.valorUnitario END valor, ");
            sbSQL.Append("                    CASE WHEN PO.valorOferta IS NOT NULL THEN ( SELECT ISNULL(MAX(MPF.numeroParcelas),0) FROM dbo.MeioPagamentoFaixa MPF INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId WHERE MP.ativo = 1 AND valorMinimo <= PO.valorOferta) ");
            sbSQL.Append("                    ELSE ( SELECT ISNULL(MAX(MPF.numeroParcelas),0) FROM dbo.MeioPagamentoFaixa MPF INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId WHERE MP.ativo = 1 AND valorMinimo <= PO.valorUnitario) ");
            sbSQL.Append("                    END parcelas, ");
            sbSQL.Append("                    (SELECT ISNULL(MAX(taxaJuros),0) FROM dbo.MeioPagamentoFaixa) taxaJuros, ");
            sbSQL.Append("                    dbo.AreaDeConhecimentoDaCategoria(pc.categoriaId) AS identificadorArea,");
            sbSQL.Append("                    PC.categoriaId, ");
            sbSQL.Append("                    PO.disponivel ");
            sbSQL.Append("           FROM     dbo.TituloImpresso P");
            sbSQL.Append("                    INNER JOIN dbo.Produto PO ON P.tituloImpressoId = PO.produtoId");
            sbSQL.Append("                                                 AND PO.exibirSite = 1 AND PO.homologado=1 ");
            sbSQL.Append("                    INNER JOIN dbo.ProdutoCategoria PC ON PC.produtoId = PO.produtoId");
            sbSQL.Append("                    INNER JOIN dbo.UsuarioInteresse ON UsuarioInteresse.categoriaId = PC.categoriaId AND UsuarioInteresse.usuarioId = @usuarioId ");
            //sbSQL.Append("           UNION ALL");
            //sbSQL.Append("           SELECT   P.tituloEletronicoId ,");
            //sbSQL.Append("                    p.tituloId AS ProdutoId,");
            //sbSQL.Append("                    'eBook',");
            //sbSQL.Append("                    PO.valorUnitario,");
            //sbSQL.Append("                    PO.valorOferta,");
            //sbSQL.Append("                    CASE WHEN PO.valorOferta>0 THEN PO.valorOferta ELSE PO.valorUnitario END valor, ");
            //sbSQL.Append("                    CASE WHEN PO.valorOferta IS NOT NULL THEN ( SELECT ISNULL(MAX(MPF.numeroParcelas),0) FROM dbo.MeioPagamentoFaixa MPF INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId WHERE MP.ativo = 1 AND valorMinimo <= PO.valorOferta) ");
            //sbSQL.Append("                    ELSE ( SELECT ISNULL(MAX(MPF.numeroParcelas),0) FROM dbo.MeioPagamentoFaixa MPF INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId WHERE MP.ativo = 1 AND valorMinimo <= PO.valorUnitario) ");
            //sbSQL.Append("                    END parcelas, ");
            //sbSQL.Append("                    (SELECT ISNULL(MAX(taxaJuros),0) FROM dbo.MeioPagamentoFaixa) taxaJuros, ");
            //sbSQL.Append("                    dbo.AreaDeConhecimentoDaCategoria(pc.categoriaId) AS identificadorArea,");
            //sbSQL.Append("                    PC.categoriaId, ");
            //sbSQL.Append("                    PO.disponivel ");
            //sbSQL.Append("           FROM     dbo.TituloEletronico P");
            //sbSQL.Append("                    INNER JOIN dbo.Produto PO ON P.tituloEletronicoId = PO.produtoId");
            //sbSQL.Append("                                                 AND PO.exibirSite = 1 AND PO.homologado=1");
            //sbSQL.Append("                    INNER JOIN dbo.ProdutoCategoria PC ON PC.produtoId = PO.produtoId");
            //sbSQL.Append("                    INNER JOIN dbo.UsuarioInteresse ON UsuarioInteresse.categoriaId = PC.categoriaId AND UsuarioInteresse.usuarioId = @usuarioId ");
            sbSQL.Append("         ),");
            sbSQL.Append("    Titulos ( tituloId, nomeTitulo, produtoId, identificadorArea, categoriaId, tipo, valorUnitario, valorOferta, valor, parcelas, taxaJuros, dataLancamento, disponivel )");
            sbSQL.Append("      AS ( SELECT   T.tituloId ,");
            sbSQL.Append("                    T.nomeTitulo ,");
            sbSQL.Append("                    P.produtoId,");
            sbSQL.Append("                    P.identificadorArea,");
            sbSQL.Append("                    P.categoriaId,");
            sbSQL.Append("                    P.tipo,");
            sbSQL.Append("                    P.valorUnitario,");
            sbSQL.Append("                    P.valorOferta,");
            sbSQL.Append("                    P.valor,");
            sbSQL.Append("                    P.parcelas,");
            sbSQL.Append("                    P.taxaJuros,");
            sbSQL.Append("                    T.dataLancamento,");
            sbSQL.Append("                    P.disponivel");
            sbSQL.Append("           FROM     dbo.Titulo T");
            sbSQL.Append("                    INNER JOIN Produtos P ON P.tituloId = T.tituloId");
            sbSQL.Append("                    INNER JOIN dbo.ProdutoCategoria PC ON P.produtoId = PC.produtoId");
            sbSQL.Append("           WHERE    EXISTS ( SELECT tituloId");
            sbSQL.Append("                             FROM   dbo.TituloConteudoExtraArquivo TCE");
            sbSQL.Append("                             WHERE  TCE.tituloId = T.tituloId");
            sbSQL.Append("                                    AND TCE.restritoProfessor = 1 )");
            if (categoria.CategoriaId > 0)
            {
                sbSQL.Append("                                AND PC.categoriaId = @categoriaId");
            }
            sbSQL.Append("         ) ");
            sbSQL.Append("SELECT  Q.* , ");
            sbSQL.Append("        TCE.* ");
            sbSQL.Append("FROM    ( SELECT    T.* ,");
            sbSQL.Append("                    (SELECT    A.nomeAutor + '; ' AS [text()]");
            sbSQL.Append("                      FROM      Autor A");
            sbSQL.Append("                                INNER JOIN dbo.TituloAutor TA ON TA.autorId = A.autorId");
            sbSQL.Append("                                                          AND TA.tituloId = T.tituloId");
            sbSQL.Append("                      ORDER BY  TA.ordem");
            sbSQL.Append("                    FOR");
            sbSQL.Append("                      XML PATH('')");
            sbSQL.Append("                    ) AS Autores ,");
            sbSQL.Append("                    PIA.arquivoId AS arquivoIdCapa ,");
            sbSQL.Append("                    PIA.nomeArquivo AS nomeArquivoCapa ,");
            sbSQL.Append("                    PIA.nomeArquivoOriginal AS nomeArquivoOriginalCapa ,");
            sbSQL.Append("                    PIA.dataHoraUpload AS dataHoraUploadCapa ,");
            sbSQL.Append("                    PIA.tamanhoArquivo AS tamanhoArquivoCapa ,");
            sbSQL.Append("                    ROW_NUMBER() OVER ( ORDER BY T.nomeTitulo ) AS R");
            sbSQL.Append("          FROM      Titulos T");
            sbSQL.Append("                    LEFT  JOIN dbo.ProdutoImagem PI ON PI.produtoId = T.produtoId");
            sbSQL.Append("                                                       AND pi.produtoImagemTipoId = 1 /*somente capa*/");
            sbSQL.Append("                    LEFT  JOIN dbo.Arquivo PIA ON PI.arquivoId = PIA.arquivoId");
            sbSQL.Append("        ) AS Q");
            sbSQL.Append("        INNER JOIN TituloConteudoExtraArquivo TCE ON TCE.tituloId = Q.tituloId");
            sbSQL.Append("                                                     AND TCE.restritoProfessor = 1");
            sbSQL.Append("        INNER JOIN dbo.Arquivo A ON TCE.arquivoId = A.arquivoId");
            sbSQL.Append(" WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());
            if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuario.UsuarioId);
            if (categoria.CategoriaId > 0)
            {
                _db.AddInParameter(command, "@categoriaId", DbType.Int32, categoria.CategoriaId);
            }

            reader = _db.ExecuteReader(command);

            Int32 tituloId = 0;
            Int32 produtoId = 0;
            TituloVH entidadeRetorno = null;

            while (reader.Read())
            {
                if (produtoId != Convert.ToInt32(reader["produtoId"].ToString()))
                {
                    if (entidadeRetorno != null)
                    {
                        entidadesRetorno.Add(entidadeRetorno);
                    }

                    produtoId = Convert.ToInt32(reader["produtoId"].ToString());

                    entidadeRetorno = new TituloVH();
                    PopulaTituloComMaterialExtraArquivo(reader, entidadeRetorno);
                }
                else
                {
                    if (entidadeRetorno.TituloConteudoExtraArquivos == null) entidadeRetorno.TituloConteudoExtraArquivos = new List<TituloConteudoExtraArquivo>();
                    TituloConteudoExtraArquivo tituloConteudoExtraArquivo = new TituloConteudoExtraArquivo();

                    PopulaTituloApenasMaterialExtraArquivo(reader, tituloConteudoExtraArquivo);

                    entidadeRetorno.TituloConteudoExtraArquivos.Add(tituloConteudoExtraArquivo);
                }
            }

            if (entidadeRetorno != null)
            {
                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public int ContarTituloComMaterialExtraArquivo(Usuario usuario, Categoria categoria)
        {
            int retorno = 0;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("WITH  Produtos ( produtoId, tituloId, tipo, valorUnitario, valorOferta, valor, parcelas )");
            sbSQL.Append("      AS ( SELECT   P.tituloImpressoId ,");
            sbSQL.Append("                    p.tituloId AS ProdutoId,");
            sbSQL.Append("                    'Livro Impresso',");
            sbSQL.Append("                    PO.valorUnitario,");
            sbSQL.Append("                    PO.valorOferta,");
            sbSQL.Append("                    CASE WHEN PO.valorOferta>0 THEN PO.valorOferta ELSE PO.valorUnitario END valor, ");
            sbSQL.Append("		            CASE WHEN PO.valorOferta IS NOT NULL THEN ( SELECT ISNULL(MAX(MPF.numeroParcelas),0) FROM dbo.MeioPagamentoFaixa MPF INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId WHERE MP.ativo = 1 AND valorMinimo <= PO.valorOferta) ");
            sbSQL.Append("		            ELSE ( SELECT ISNULL(MAX(MPF.numeroParcelas),0) FROM dbo.MeioPagamentoFaixa MPF INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId WHERE MP.ativo = 1 AND valorMinimo <= PO.valorUnitario) ");
            sbSQL.Append("		            END parcelas ");
            sbSQL.Append("           FROM     dbo.TituloImpresso P");
            sbSQL.Append("                    INNER JOIN dbo.Produto PO ON P.tituloImpressoId = PO.produtoId");
            sbSQL.Append("                                                 AND PO.exibirSite = 1 AND PO.homologado=1");
            sbSQL.Append("                    INNER JOIN dbo.ProdutoCategoria PC ON PC.produtoId = PO.produtoId");
            sbSQL.Append("                    INNER JOIN dbo.UsuarioInteresse ON UsuarioInteresse.categoriaId = PC.categoriaId AND UsuarioInteresse.usuarioId = @usuarioId ");
            //sbSQL.Append("           UNION ALL");
            //sbSQL.Append("           SELECT   P.tituloEletronicoId ,");
            //sbSQL.Append("                    p.tituloId AS ProdutoId,");
            //sbSQL.Append("                    'eBook',");
            //sbSQL.Append("                    PO.valorUnitario,");
            //sbSQL.Append("                    PO.valorOferta,");
            //sbSQL.Append("                    CASE WHEN PO.valorOferta>0 THEN PO.valorOferta ELSE PO.valorUnitario END valor, ");
            //sbSQL.Append("		            CASE WHEN PO.valorOferta IS NOT NULL THEN ( SELECT ISNULL(MAX(MPF.numeroParcelas),0) FROM dbo.MeioPagamentoFaixa MPF INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId WHERE MP.ativo = 1 AND valorMinimo <= PO.valorOferta) ");
            //sbSQL.Append("		            ELSE ( SELECT ISNULL(MAX(MPF.numeroParcelas),0) FROM dbo.MeioPagamentoFaixa MPF INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId WHERE MP.ativo = 1 AND valorMinimo <= PO.valorUnitario) ");
            //sbSQL.Append("		            END Parcelas ");
            //sbSQL.Append("           FROM     dbo.TituloEletronico P");
            //sbSQL.Append("                    INNER JOIN dbo.Produto PO ON P.tituloEletronicoId = PO.produtoId");
            //sbSQL.Append("                                                 AND PO.exibirSite = 1 AND PO.homologado=1");
            //sbSQL.Append("                    INNER JOIN dbo.ProdutoCategoria PC ON PC.produtoId = PO.produtoId");
            //sbSQL.Append("                    INNER JOIN dbo.UsuarioInteresse ON UsuarioInteresse.categoriaId = PC.categoriaId AND UsuarioInteresse.usuarioId = @usuarioId ");
            sbSQL.Append("         ),");
            sbSQL.Append("    Titulos ( tituloId, nomeTitulo, produtoId, tipo, valorUnitario, valorOferta, valor, parcelas )");
            sbSQL.Append("      AS ( SELECT   T.tituloId ,");
            sbSQL.Append("                    T.nomeTitulo ,");
            sbSQL.Append("                    P.produtoId,");
            sbSQL.Append("                    P.tipo,");
            sbSQL.Append("                    P.valorUnitario,");
            sbSQL.Append("                    P.valorOferta,");
            sbSQL.Append("                    P.valor,");
            sbSQL.Append("                    P.parcelas");
            sbSQL.Append("           FROM     dbo.Titulo T");
            sbSQL.Append("                    INNER JOIN Produtos P ON P.tituloId = T.tituloId");
            sbSQL.Append("                    INNER JOIN dbo.ProdutoCategoria PC ON P.produtoId = PC.produtoId");
            sbSQL.Append("           WHERE    EXISTS ( SELECT tituloId");
            sbSQL.Append("                             FROM   dbo.TituloConteudoExtraArquivo TCE");
            sbSQL.Append("                             WHERE  TCE.tituloId = T.tituloId");
            sbSQL.Append("                                    AND TCE.restritoProfessor = 1 )");
            if (categoria.CategoriaId > 0)
            {
                sbSQL.Append("                                 AND PC.categoriaId = @categoriaId");
            }
            sbSQL.Append("         )");
            sbSQL.Append(" SELECT  Count(distinct Q.R) as Total ");
            sbSQL.Append(" FROM    ( SELECT    T.* ,");
            sbSQL.Append("                    (SELECT    A.nomeAutor + '; ' AS [text()]");
            sbSQL.Append("                      FROM      Autor A");
            sbSQL.Append("                                INNER JOIN dbo.TituloAutor TA ON TA.autorId = A.autorId");
            sbSQL.Append("                                                          AND TA.tituloId = T.tituloId");
            sbSQL.Append("                      ORDER BY  TA.ordem");
            sbSQL.Append("                    FOR");
            sbSQL.Append("                      XML PATH('')");
            sbSQL.Append("                    ) AS Autores ,");
            sbSQL.Append("                    PIA.arquivoId AS arquivoIdCapa ,");
            sbSQL.Append("                    PIA.nomeArquivo AS nomeArquivoCapa ,");
            sbSQL.Append("                    PIA.nomeArquivoOriginal AS nomeArquivoOriginalCapa ,");
            sbSQL.Append("                    PIA.dataHoraUpload AS dataHoraUploadCapa ,");
            sbSQL.Append("                    PIA.tamanhoArquivo AS tamanhoArquivoCapa ,");
            sbSQL.Append("                    ROW_NUMBER() OVER ( ORDER BY T.nomeTitulo ) AS R");
            sbSQL.Append("          FROM      Titulos T");
            sbSQL.Append("                    LEFT  JOIN dbo.ProdutoImagem PI ON PI.produtoId = T.produtoId");
            sbSQL.Append("                                                       AND pi.produtoImagemTipoId = 1 /*somente capa*/");
            sbSQL.Append("                    LEFT  JOIN dbo.Arquivo PIA ON PI.arquivoId = PIA.arquivoId");
            sbSQL.Append("        ) AS Q");
            sbSQL.Append("        INNER JOIN TituloConteudoExtraArquivo TCE ON TCE.tituloId = Q.tituloId");
            sbSQL.Append("                                                     AND TCE.restritoProfessor = 1");
            sbSQL.Append("        INNER JOIN dbo.Arquivo A ON TCE.arquivoId = A.arquivoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuario.UsuarioId);
            if (categoria.CategoriaId > 0)
            {
                _db.AddInParameter(command, "@categoriaId", DbType.Int32, categoria.CategoriaId);
            }

            IDataReader reader = _db.ExecuteReader(command);

            if ((reader.Read()) && ((reader["Total"] != DBNull.Value)))
            {
                retorno = (int)reader["Total"];
            }
            reader.Close();

            return retorno;
        }

        /// <summary>
        /// Método que retorna popula um Titulo baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Titulo a ser populado(.</param>
        public static void PopulaTituloComMaterialExtraArquivo(IDataReader reader, TituloVH entidade)
        {
            if (reader["tituloId"] != DBNull.Value)
            {
                entidade.TituloId = Convert.ToInt32(reader["tituloId"].ToString());
            }

            if (reader["produtoId"] != DBNull.Value)
            {
                entidade.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());
            }

            if (reader["identificadorArea"] != DBNull.Value)
            {
                entidade.AreaId = Convert.ToInt32(reader["identificadorArea"].ToString());
            }

            if (reader["categoriaId"] != DBNull.Value)
            {
                entidade.CategoriaId = Convert.ToInt32(reader["categoriaId"].ToString());
            }

            if (reader["nomeTitulo"] != DBNull.Value)
                entidade.NomeTitulo = reader["nomeTitulo"].ToString();

            if (reader["tipo"] != DBNull.Value)
                entidade.Tipo = reader["tipo"].ToString();

            if (reader["autores"] != DBNull.Value)
                entidade.Autores = reader["autores"].ToString();

            if (reader["arquivoIdCapa"] != DBNull.Value)
            {
                if (entidade.Arquivo == null) entidade.Arquivo = new Arquivo();
                entidade.Arquivo.ArquivoId = Convert.ToInt32(reader["arquivoIdCapa"].ToString());
            }

            if (reader["nomeArquivoCapa"] != DBNull.Value)
            {
                if (entidade.Arquivo == null) entidade.Arquivo = new Arquivo();
                entidade.Arquivo.NomeArquivo = reader["nomeArquivoCapa"].ToString();
            }

            if (reader["nomeArquivoOriginalCapa"] != DBNull.Value)
            {
                if (entidade.Arquivo == null) entidade.Arquivo = new Arquivo();
                entidade.Arquivo.NomeArquivoOriginal = reader["nomeArquivoOriginalCapa"].ToString();
            }

            if (reader["valorUnitario"] != DBNull.Value)
            {
                entidade.ValorUnitario = Convert.ToDecimal(reader["valorUnitario"].ToString());
            }

            if (reader["valorOferta"] != DBNull.Value)
            {
                entidade.ValorOferta = Convert.ToDecimal(reader["valorOferta"].ToString());
            }

            if (reader["valor"] != DBNull.Value)
            {
                entidade.Valor = Convert.ToDecimal(reader["valor"].ToString());
            }

            if (reader["parcelas"] != DBNull.Value)
            {
                entidade.Parcelas = Convert.ToInt32(reader["parcelas"].ToString());
            }

            if (reader["taxaJuros"] != DBNull.Value)
            {
                entidade.TaxaJuros = Convert.ToDecimal(reader["taxaJuros"].ToString());
            }

            if (reader["tituloConteudoExtraArquivoId"] != DBNull.Value)
            {
                if (entidade.TituloConteudoExtraArquivos == null) entidade.TituloConteudoExtraArquivos = new List<TituloConteudoExtraArquivo>();
                TituloConteudoExtraArquivo tituloConteudoExtraArquivo = new TituloConteudoExtraArquivo();

                PopulaTituloApenasMaterialExtraArquivo(reader, tituloConteudoExtraArquivo);

                entidade.TituloConteudoExtraArquivos.Add(tituloConteudoExtraArquivo);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        public static void PopulaTituloApenasMaterialExtraArquivo(IDataReader reader, TituloConteudoExtraArquivo entidade)
        {
            if (reader["tituloConteudoExtraArquivoId"] != DBNull.Value)
            {
                entidade.TituloConteudoExtraArquivoId = Convert.ToInt32(reader["tituloConteudoExtraArquivoId"].ToString());
            }

            if (reader["tituloConteudoExtraArquivoId"] != DBNull.Value)
            {
                entidade.SomenteLogado = Convert.ToBoolean(reader["somenteLogado"].ToString());
            }

            if (reader["tituloConteudoExtraArquivoId"] != DBNull.Value)
            {
                entidade.RestritoProfessor = Convert.ToBoolean(reader["restritoProfessor"].ToString());
            }

            if (reader["tituloConteudoExtraArquivoId"] != DBNull.Value)
            {
                entidade.NomeConteudo = reader["nomeConteudo"].ToString();
            }

            if (reader["tituloConteudoExtraArquivoId"] != DBNull.Value)
            {
                if (entidade.Arquivo == null) entidade.Arquivo = new Arquivo();
                entidade.Arquivo.ArquivoId = Convert.ToInt32(reader["arquivoId"].ToString());
            }
        }

        /// <summary>
        /// Método que desmarcar os dados de um títulos mais vendidos.
        /// </summary>
        public void DesmarcarMaisVendidos()
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE Titulo SET ");
            sbSQL.Append(" maisVendido = @maisVendido, ");
            sbSQL.Append(" maisVendidoOrdem = null ");
            sbSQL.Append(" WHERE maisVendido = @maisVendidoWhere ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@maisVendido", DbType.Int32, 0);
            _db.AddInParameter(command, "@maisVendidoWhere", DbType.Int32, 1);


            // Executa a query.
            _db.ExecuteNonQuery(command);
        }

        #region [ Atualizar ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="titulosMaisVendidos"></param>
        public void AtualizarMaisVendidos(List<Titulo> titulosMaisVendidos)
        {
            DbCommand command;

            foreach (Titulo tituloBOTemp in titulosMaisVendidos)
            {
                StringBuilder sbSQL = new StringBuilder();

                // Monta a string de atualização.
                sbSQL.Append("UPDATE Titulo ");
                sbSQL.Append("SET maisVendido = @maisVendido, maisVendidoOrdem = @maisVendidoOrdem ");
                sbSQL.Append("WHERE tituloId = @tituloId");
                //sbSQL.Append("WHERE tituloId IN ( ");
                //sbSQL.Append("		                SELECT tituloId ");
                //sbSQL.Append("		                FROM TituloImpresso ");
                //sbSQL.Append("		                WHERE isbn13 = @isbn13 ");
                //sbSQL.Append("		                UNION ");
                //sbSQL.Append("		                SELECT tituloId ");
                //sbSQL.Append("		                FROM TituloEletronico ");
                //sbSQL.Append("		                WHERE isbn13 = @isbn13 ");
                //sbSQL.Append("	                ) ");

                command = _db.GetSqlStringCommand(sbSQL.ToString());

                // Parâmetros
                _db.AddInParameter(command, "@maisVendido", DbType.Int32, 1);
                _db.AddInParameter(command, "@maisVendidoOrdem", DbType.Int32, tituloBOTemp.MaisVendidoOrdem);
                _db.AddInParameter(command, "@tituloId", DbType.Int32, tituloBOTemp.TituloId);
                //_db.AddInParameter(command, "@isbn13", DbType.String, tituloBOTemp.TituloImpresso.Isbn13);

                // Executa a query.
                _db.ExecuteNonQuery(command);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="titulo"></param>
        public void AtualizarNomeSubTitulo(Titulo titulo)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append("UPDATE Titulo ");
            sbSQL.Append("SET nomeTitulo = @nomeTitulo, subtituloLivro = @subtituloLivro ");
            sbSQL.Append("WHERE tituloId = @tituloId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@nomeTitulo", DbType.String, titulo.NomeTitulo);
            _db.AddInParameter(command, "@subtituloLivro", DbType.String, titulo.SubtituloLivro);
            _db.AddInParameter(command, "@tituloId", DbType.Int32, titulo.TituloId);

            // Executa a query.
            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void AtualizarMenosNomeSubtitulo(Titulo entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE Titulo SET ");
            //sbSQL.Append(" numeroPaginas=@numeroPaginas, edicao=@edicao, dataLancamento=@dataLancamento, dataPublicacao=@dataPublicacao, maisVendido=@maisVendido, formato=@formato ");
            sbSQL.Append(" numeroPaginas=@numeroPaginas, edicao=@edicao, dataLancamento=@dataLancamento, dataPublicacao=@dataPublicacao, formato=@formato ");
            sbSQL.Append(" WHERE tituloId=@tituloId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.TituloId);

            if (entidade.NumeroPaginas != null)
                _db.AddInParameter(command, "@numeroPaginas", DbType.Int32, entidade.NumeroPaginas);
            else
                _db.AddInParameter(command, "@numeroPaginas", DbType.Int32, null);

            if (entidade.Edicao != null)
                _db.AddInParameter(command, "@edicao", DbType.Int32, entidade.Edicao);
            else
                _db.AddInParameter(command, "@edicao", DbType.Int32, null);

            if (entidade.DataLancamento != null && entidade.DataLancamento != DateTime.MinValue)
                _db.AddInParameter(command, "@dataLancamento", DbType.DateTime, entidade.DataLancamento);
            else
                _db.AddInParameter(command, "@dataLancamento", DbType.DateTime, null);

            if (entidade.DataPublicacao != null && entidade.DataPublicacao != DateTime.MinValue)
                _db.AddInParameter(command, "@dataPublicacao", DbType.DateTime, entidade.DataPublicacao);
            else
                _db.AddInParameter(command, "@dataPublicacao", DbType.DateTime, null);

            //_db.AddInParameter(command, "@maisVendido", DbType.Int32, entidade.MaisVendido);

            if (entidade.Formato != null)
                _db.AddInParameter(command, "@formato", DbType.String, entidade.Formato);
            else
                _db.AddInParameter(command, "@formato", DbType.String, null);

            // Executa a query.
            _db.ExecuteNonQuery(command);
        }

        #endregion

        #region [ Carregar ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="titulosParaEstante"></param>
        /// <param name="categoria"></param>
        /// <param name="numeroMaximoDeRegistros"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarUltimosLancamentosPorCategoria(List<EstanteTituloVH> titulosParaEstante, Categoria categoria, Int32 numeroMaximoDeRegistros)
        {
            StringBuilder sbOrder = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();

            sbOrder.Append(" ORDER BY t.dataLancamento DESC ");

            List<Categoria> categorias = null;

            if (categoria != null)
            {
                categorias = new List<Categoria>();
                categorias.Add(categoria);
            }

            StringBuilder sbSql = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            if (categorias != null && categorias.Count > 0)
            {
                string idsDasCategorias = categorias.Select(c => c.CategoriaId.ToString()).Aggregate((anterior, proximo) => anterior + ", " + proximo);
                sbSql.Append(" WITH Categorias (categoriaId, nomeCategoria, categoriaIdPai, nivel ) AS ( ");
                sbSql.Append(" SELECT C.categoriaId, C.nomeCategoria, C.categoriaIdPai, 0 AS Nivel ");
                sbSql.Append(" FROM Categoria AS C ");
                sbSql.AppendFormat(" WHERE C.categoriaId IN ({0}) ", idsDasCategorias);
                sbSql.Append(" UNION ALL ");
                sbSql.Append(" SELECT C.categoriaId, C.nomeCategoria, C.categoriaIdPai, Nivel+1 ");
                sbSql.Append(" FROM Categoria AS C ");
                sbSql.Append(" INNER JOIN Categorias AS CS ");
                sbSql.Append(" ON c.CategoriaIdPai = CS.categoriaId ) ");
            }

            sbSql.AppendFormat(
                " SELECT TOP {0} p.produtoId, t.tituloId, t.nomeTitulo, t.subtituloLivro,  arq.nomeArquivo, 0 eCompraConjunta, p.disponivel,",
                numeroMaximoDeRegistros);

            sbSql.Append("CASE ISNULL(p.valorOferta,0) WHEN 0 THEN p.valorUnitario ELSE p.valorOferta END AS valor, PC.CategoriaId FROM Produto p ");

            // Se for eBook troca o relacionamento
            sbSql.Append(" INNER JOIN TituloImpresso ti ON ti.tituloImpressoId = p.produtoId ");

            sbSql.Append(" INNER JOIN Titulo t ON t.tituloId = ti.tituloId ");
            sbSql.Append(" LEFT JOIN ProdutoImagem tImg ON tImg.produtoId = p.produtoId AND tImg.produtoImagemTipoId = 1 ");
            sbSql.Append(" LEFT JOIN Arquivo arq ON arq.arquivoId = tImg.arquivoId ");

            sbSql.Append(" INNER JOIN dbo.ProdutoCategoria PC ON pc.produtoId = p.produtoId ");

            if (categorias != null && categorias.Count > 0)
            {
                sbSql.Append(" INNER JOIN Categorias CS ON cs.categoriaId = pc.categoriaId ");
            }

            //Monta clausula WHERE
            //sbWhere.Append(" INNER JOIN DestaqueTituloImpressoRelacionado dtir ON dtir.tituloId = t.tituloId ");
            //sbWhere.Append(" INNER JOIN DestaqueTituloImpresso dti ON dti.destaqueTituloImpressoId = dtir.destaqueTituloImpressoId ");

            //if (categoria != null && categoria.CategoriaId > 0)
            //{
            //    sbWhere.Append("WHERE dti.destaqueTituloImpressoId = 1 "); //DESTAQUE CATEGORIA
            //}
            //else
            //{
            //    sbWhere.Append("WHERE dti.destaqueTituloImpressoId = 2 "); //DESTAQUE HOME
            //}

            sbWhere.Append("WHERE p.produtoTipoId = 1 ");
            sbWhere.Append("AND p.exibirSite = 1 ");
            sbWhere.Append("AND p.homologado=1 ");

            sbSql.Append(sbWhere.ToString());
            sbSql.Append(sbOrder.ToString());

            command = _db.GetSqlStringCommand(sbSql.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                EstanteTituloVH estanteTituloVh = new EstanteTituloVH();
                PopulaEstanteTitulo(reader, estanteTituloVh);
                titulosParaEstante.Add(estanteTituloVh);
            }

            reader.Close();

            return titulosParaEstante;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="titulosParaEstante"></param>
        /// <param name="categoria"></param>
        /// <param name="numeroMaximoDeRegistros"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarLivrosQueDeixaramDeSerLancamentoPorCategoria(List<EstanteTituloVH> titulosParaEstante, Categoria categoria, Int32 numeroMaximoDeRegistros)
        {
            StringBuilder sbOrder = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();

            sbOrder.Append(" ORDER BY t.dataLancamento DESC ");

            List<Categoria> categorias = null;

            if (categoria != null)
            {
                categorias = new List<Categoria>();
                categorias.Add(categoria);
            }

            StringBuilder sbSql = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            if (categorias != null && categorias.Count > 0)
            {
                string idsDasCategorias = categorias.Select(c => c.CategoriaId.ToString()).Aggregate((anterior, proximo) => anterior + ", " + proximo);
                sbSql.Append(" WITH Categorias (categoriaId, nomeCategoria, categoriaIdPai, nivel ) AS ( ");
                sbSql.Append(" SELECT C.categoriaId, C.nomeCategoria, C.categoriaIdPai, 0 AS Nivel ");
                sbSql.Append(" FROM Categoria AS C ");
                sbSql.AppendFormat(" WHERE C.categoriaId IN ({0}) ", idsDasCategorias);
                sbSql.Append(" UNION ALL ");
                sbSql.Append(" SELECT C.categoriaId, C.nomeCategoria, C.categoriaIdPai, Nivel+1 ");
                sbSql.Append(" FROM Categoria AS C ");
                sbSql.Append(" INNER JOIN Categorias AS CS ");
                sbSql.Append(" ON c.CategoriaIdPai = CS.categoriaId ) ");
            }

            sbSql.AppendFormat(
                " SELECT TOP {0} p.produtoId, t.tituloId, t.nomeTitulo, t.subtituloLivro,  arq.nomeArquivo, 0 eCompraConjunta, p.disponivel,",
                numeroMaximoDeRegistros);

            sbSql.Append("CASE ISNULL(p.valorOferta,0) WHEN 0 THEN p.valorUnitario ELSE p.valorOferta END AS valor, PC.CategoriaId FROM Produto p ");

            // Se for eBook troca o relacionamento
            sbSql.Append(" INNER JOIN TituloImpresso ti ON ti.tituloImpressoId = p.produtoId ");

            sbSql.Append(" INNER JOIN Titulo t ON t.tituloId = ti.tituloId ");
            sbSql.Append(" LEFT JOIN ProdutoImagem tImg ON tImg.produtoId = p.produtoId AND tImg.produtoImagemTipoId = 1 ");
            sbSql.Append(" LEFT JOIN Arquivo arq ON arq.arquivoId = tImg.arquivoId ");

            sbSql.Append(" INNER JOIN dbo.ProdutoCategoria PC ON pc.produtoId = p.produtoId ");

            if (categorias != null && categorias.Count > 0)
            {
                sbSql.Append(" INNER JOIN Categorias CS ON cs.categoriaId = pc.categoriaId ");
            }

            sbWhere.Append(" WHERE t.dataLancamento < DATEADD(DAY,-45,GETDATE()) ");
            sbWhere.Append(" AND dataLancamento > DATEADD(DAY,-75,GETDATE()) ");
            sbWhere.Append(" AND p.produtoTipoId = 1 ");
            sbWhere.Append(" AND p.exibirSite = 1 ");
            sbWhere.Append(" AND p.homologado=1 ");

            sbSql.Append(sbWhere.ToString());
            sbSql.Append(sbOrder.ToString());

            command = _db.GetSqlStringCommand(sbSql.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                EstanteTituloVH estanteTituloVh = new EstanteTituloVH();
                PopulaEstanteTitulo(reader, estanteTituloVh);
                titulosParaEstante.Add(estanteTituloVh);
            }

            reader.Close();

            return titulosParaEstante;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="autor"></param>
        /// <returns></returns>
        public List<Titulo> CarregarTitulosPorAutor(Autor autor)
        {
            List<Titulo> titulos = new List<Titulo>();

            StringBuilder sbSql = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            sbSql.Append("SELECT Titulo.nomeTitulo ");
            sbSql.Append("FROM Titulo ");
            sbSql.Append("INNER JOIN TituloAutor ");
            sbSql.Append("    ON Titulo.tituloId = TituloAutor.tituloId ");
            sbSql.Append("WHERE autorId = @autorId ");
            sbSql.Append("ORDER BY Titulo.nomeTitulo ");

            command = _db.GetSqlStringCommand(sbSql.ToString());

            _db.AddInParameter(command, "@autorId", DbType.Int32, autor.AutorId);

            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Titulo titulo = new Titulo();
                titulo.NomeTitulo = reader["nomeTitulo"].ToString();
                titulos.Add(titulo);
            }

            reader.Close();

            return titulos;
        }

        /// <summary>
        /// Método que retorna quais abas podem estar habilitadas na estante
        /// </summary>
        /// <param name="categoriaId"></param>
        /// <returns></returns>
        public AbasEstanteVH CarregaAbasEstante(Int32 categoriaId)
        {
            AbasEstanteVH abasEstanteVH = new AbasEstanteVH();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("WITH    ConteudosBuscaveis ( identificadorArea, lancamento, oferta, compraConjunta, ebook, maisVendido ) ");
            sbSQL.Append("      AS ( SELECT   dbo.AreaDeConhecimentoDaCategoria(PC.categoriaId) AS identificadorArea , ");
            sbSQL.Append("		            CASE WHEN T_TE.dataLancamento IS NOT NULL ");
            sbSQL.Append("			             THEN  ");
            sbSQL.Append("				            CASE WHEN T_TE.dataLancamento BETWEEN ( GETDATE() - 45 ) ");
            sbSQL.Append("								               AND     ( GETDATE() + 45 ) ");
            sbSQL.Append("				            THEN 1 ");
            sbSQL.Append("				            ELSE 0 ");
            sbSQL.Append("				            END ");
            sbSQL.Append("			             ELSE ");
            sbSQL.Append("				            CASE WHEN T_TI.dataLancamento BETWEEN ( GETDATE() - 45 ) ");
            sbSQL.Append("								               AND     ( GETDATE() + 45 ) ");
            sbSQL.Append("				            THEN 1 ");
            sbSQL.Append("				            ELSE 0 ");
            sbSQL.Append("				            END ");
            sbSQL.Append("		            END AS Lancamento , ");
            sbSQL.Append("		            CASE WHEN P.valorOferta IS NOT NULL  ");
            sbSQL.Append("			             THEN 1 ");
            sbSQL.Append("			             ELSE 0 ");
            sbSQL.Append("		            END AS Oferta, ");
            sbSQL.Append("		            CASE WHEN CC.compraConjuntaId IS NOT NULL  ");
            sbSQL.Append("			             THEN 1 ");
            sbSQL.Append("			             ELSE 0 ");
            sbSQL.Append("		            END AS CompraConjunta, ");
            sbSQL.Append("		            CASE WHEN TE.tituloId IS NOT NULL  ");
            sbSQL.Append("			             THEN 1 ");
            sbSQL.Append("			             ELSE 0 ");
            sbSQL.Append("		            END AS Ebooks, ");
            sbSQL.Append("		            CASE WHEN T_TE.dataLancamento IS NOT NULL");
            sbSQL.Append("                       THEN ");
            sbSQL.Append("                        CASE WHEN T_TE.maisVendido = 1 ");
            sbSQL.Append("                          THEN 1");
            sbSQL.Append("	                        ELSE 0");
            sbSQL.Append("	                        END");
            sbSQL.Append("                       ELSE");
            sbSQL.Append("                       CASE WHEN T_TI.maisVendido = 1 ");
            sbSQL.Append("	                        THEN 1");
            sbSQL.Append("	                        ELSE 0");
            sbSQL.Append("                       END");
            sbSQL.Append("		            END AS MaisVendido ");
            sbSQL.Append("           FROM PRODUTO P ");
            sbSQL.Append("           INNER JOIN dbo.ProdutoCategoria PC ON PC.produtoId = P.produtoId ");
            sbSQL.Append("           LEFT JOIN dbo.TituloEletronico TE ON TE.tituloEletronicoId = P.produtoId ");
            sbSQL.Append("           LEFT JOIN dbo.Titulo T_TE ON TE.tituloId = T_TE.tituloId ");
            sbSQL.Append("           LEFT JOIN dbo.TituloImpresso TI ON TI.tituloImpressoId = P.produtoId ");
            sbSQL.Append("           LEFT JOIN dbo.Titulo T_TI ON TI.tituloId = T_TI.tituloId ");
            sbSQL.Append("           LEFT JOIN dbo.CompraConjunta CC ON P.produtoId = CC.produtoId ");
            sbSQL.Append("								            AND CC.dataInicialCompra <= GETDATE() ");
            sbSQL.Append("								            AND CC.dataFinalCompra >= GETDATE() ");
            sbSQL.Append("								            AND CC.ativa = 1 ");
            sbSQL.Append("								            AND CC.compraConjuntaStatusId = 1 ");
            sbSQL.Append("           LEFT JOIN dbo.PedidoItem PDI ON PDI.produtoId = P.produtoId ");
            sbSQL.Append("           WHERE P.exibirSite = 1 ");
            sbSQL.Append("	             AND P.homologado=1 ");
            sbSQL.Append("         )  ");
            sbSQL.Append("SELECT  DISTINCT identificadorArea , ");
            sbSQL.Append("        lancamento , ");
            sbSQL.Append("        oferta , ");
            sbSQL.Append("        CB.compraConjunta , ");
            sbSQL.Append("        ebook , ");
            sbSQL.Append("        maisVendido ");
            sbSQL.Append("FROM    ConteudosBuscaveis CB ");
            if (categoriaId > 0)
            {
                sbSQL.Append("where identificadorArea = @categoriaId ");
            }

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            if (categoriaId > 0)
            {
                _db.AddInParameter(command, "@categoriaId", DbType.Int32, categoriaId);
            }

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                if (Convert.ToInt32(reader["lancamento"].ToString()) > 0)
                {
                    abasEstanteVH.Lancamento = true;
                }

                if (Convert.ToInt32(reader["oferta"].ToString()) > 0)
                {
                    abasEstanteVH.Oferta = true;
                }

                if (Convert.ToInt32(reader["compraConjunta"].ToString()) > 0)
                {
                    abasEstanteVH.CompraColetiva = true;
                }

                if (Convert.ToInt32(reader["ebook"].ToString()) > 0)
                {
                    abasEstanteVH.Ebook = true;
                }

                if (Convert.ToInt32(reader["maisVendido"].ToString()) > 0)
                {
                    abasEstanteVH.MaisVendido = true;
                }
            }

            reader.Close();

            return abasEstanteVH;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="palavra"></param>
        /// <returns></returns>
        public StringBuilder CarregarAutoCompleteBusca(String palavra)
        {
            StringBuilder retorno = new StringBuilder();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT TOP 20 * FROM ( ");
            sbSQL.Append("        SELECT * FROM ( ");
            sbSQL.Append("            SELECT nomeTitulo as texto, '1' as ordem  ");
            sbSQL.Append("            FROM Produto  ");
            sbSQL.Append("            INNER JOIN ProdutoCategoria ON ProdutoCategoria.produtoId = Produto.produtoId ");
            sbSQL.Append("            INNER JOIN ProdutoSelo ON ProdutoSelo.produtoId = Produto.produtoId ");
            sbSQL.Append("            INNER JOIN TituloImpresso ON Produto.produtoId = TituloImpresso.tituloImpressoId ");
            sbSQL.Append("            INNER JOIN Titulo ON Titulo.tituloId = tituloImpresso.tituloId ");
            sbSQL.Append("            INNER JOIN TituloAutor ON TituloAutor.tituloId = tituloImpresso.tituloId ");
            sbSQL.Append("            INNER JOIN Autor ON TituloAutor.autorId = Autor.autorId ");
            sbSQL.Append("            WHERE nomeProduto LIKE @palavra  ");
            sbSQL.Append("	            AND exibirSite = 1 ");
            sbSQL.Append("	            AND homologado = 1 ");
            sbSQL.Append("            Union ");
            sbSQL.Append("            SELECT nomeTitulo as texto, '1' as ordem  ");
            sbSQL.Append("            FROM Produto  ");
            sbSQL.Append("            INNER JOIN ProdutoCategoria ON ProdutoCategoria.produtoId = Produto.produtoId ");
            sbSQL.Append("            INNER JOIN ProdutoSelo ON ProdutoSelo.produtoId = Produto.produtoId ");
            sbSQL.Append("            INNER JOIN TituloEletronico ON Produto.produtoId = TituloEletronico.tituloEletronicoId ");
            sbSQL.Append("            INNER JOIN Titulo ON Titulo.tituloId = TituloEletronico.tituloId ");
            sbSQL.Append("            INNER JOIN TituloAutor ON TituloAutor.tituloId = TituloEletronico.tituloId ");
            sbSQL.Append("            INNER JOIN Autor ON TituloAutor.autorId = Autor.autorId ");
            sbSQL.Append("            WHERE nomeProduto LIKE @palavra  ");
            sbSQL.Append("	            AND exibirSite = 1 ");
            sbSQL.Append("	            AND homologado = 1 ");
            sbSQL.Append("        ) as Q ");
            sbSQL.Append("    UNION ");
            sbSQL.Append("        SELECT nomeAutor as texto, '2' as ordem  ");
            sbSQL.Append("        FROM autor  ");
            sbSQL.Append("        WHERE nomeAutor LIKE @palavra ");
            sbSQL.Append("    UNION ");
            sbSQL.Append("        SELECT isbn13 as texto, '3' as ordem  ");
            sbSQL.Append("        FROM tituloImpresso   ");
            sbSQL.Append("        INNER JOIN Produto ON tituloImpresso.tituloImpressoId = Produto.produtoId ");
            sbSQL.Append("						             AND Produto.exibirSite = 1 AND Produto.homologado=1");
            sbSQL.Append("        INNER JOIN Titulo ON Titulo.tituloId = tituloImpresso.tituloId ");
            sbSQL.Append("        INNER JOIN TituloAutor ON TituloAutor.tituloId = tituloImpresso.tituloId ");
            sbSQL.Append("        INNER JOIN Autor ON TituloAutor.autorId = Autor.autorId ");
            sbSQL.Append("        INNER JOIN ProdutoCategoria PC ON PC.produtoId = Produto.produtoId ");
            sbSQL.Append("        INNER JOIN ProdutoSelo PS ON PS.produtoId = Produto.produtoId ");
            sbSQL.Append("        WHERE isbn13 LIKE @palavra ");
            sbSQL.Append("	            AND exibirSite = 1 ");
            sbSQL.Append("	            AND homologado = 1 ");
            sbSQL.Append("    UNION ");
            sbSQL.Append("        SELECT isbn13 as texto, '4' as ordem  ");
            sbSQL.Append("        FROM tituloEletronico  ");
            sbSQL.Append("        INNER JOIN Produto ON tituloEletronico.tituloEletronicoId = Produto.produtoId ");
            sbSQL.Append("						             AND Produto.exibirSite = 1 AND Produto.homologado=1");
            sbSQL.Append("        INNER JOIN Titulo ON Titulo.tituloId = tituloEletronico.tituloId ");
            sbSQL.Append("        INNER JOIN TituloAutor ON TituloAutor.tituloId = tituloEletronico.tituloId ");
            sbSQL.Append("        INNER JOIN Autor ON TituloAutor.autorId = Autor.autorId ");
            sbSQL.Append("        INNER JOIN ProdutoCategoria PC ON PC.produtoId = Produto.produtoId ");
            sbSQL.Append("        INNER JOIN ProdutoSelo PS ON PS.produtoId = Produto.produtoId ");
            sbSQL.Append("        WHERE isbn13 LIKE @palavra ");
            sbSQL.Append("	            AND exibirSite = 1 ");
            sbSQL.Append("	            AND homologado = 1 ");
            sbSQL.Append(") AS R ");
            sbSQL.Append("ORDER BY R.ordem, R.texto ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@palavra", DbType.String, "%" + palavra + "%");

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                retorno.Append("{\"");
                retorno.Append(reader["texto"].ToString());
                retorno.Append("\":\"");
                retorno.Append(reader["texto"].ToString());
                retorno.Append("\"},");
            }

            reader.Close();

            return retorno;
        }

        /// <summary>
        /// Carrega 7 títulos de sugestão a partir do histórico de compras
        /// </summary>
        /// <param name="usuarioBO"></param>
        /// <param name="categoriaBO"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregaTitulosHistoricoComprasParaEstante(Usuario usuarioBO, Categoria categoriaBO)
        {
            List<EstanteTituloVH> titulosParaEstante = new List<EstanteTituloVH>();

            StringBuilder sbSql = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            #region [ Execução do CTE ]
            sbSql.AppendFormat(@"WITH
									ProdutosComprados(produtoId) AS
																	(
																	SELECT 
																		PR.ProdutoId 
																	FROM 
																		Pedido P
																		INNER JOIN PedidoItem PI ON P.pedidoId = PI.pedidoId
																		INNER JOIN Produto PR ON PR.produtoId = PI.produtoId AND (PR.produtoTipoId=1 OR PR.produtoTipoId=2)
																	WHERE 
																		P.usuarioId = {0}
																	)
									, CategoriasProdutosComprados(categoriaId) AS
																				(
																				SELECT 
																					PC.categoriaId 
																				FROM 
																					ProdutosComprados P
																					INNER JOIN ProdutoCategoria PC ON P.produtoId=PC.produtoId
																				)
									, Categorias (categoriaId, nomeCategoria, categoriaIdPai, Nivel) AS
																									(
																									-- Definição do membro ancora.
																									SELECT 
																										C.categoriaId
																										, C.nomeCategoria
																										, C.categoriaIdPai
																										, 0 AS Nivel
																									FROM 
																										Categoria AS C
																									WHERE 
																										C.categoriaId IN (SELECT * FROM CategoriasProdutosComprados)
																									UNION ALL
																									-- Definição do membro recursivo.
																									SELECT 
																										C.categoriaId
																										, C.nomeCategoria
																										, C.categoriaIdPai
																										, Nivel+1
																									FROM 
																										Categoria AS C
																										INNER JOIN Categorias AS CS ON c.CategoriaIdPai = CS.categoriaId
																									) ", usuarioBO.UsuarioId);

            if (categoriaBO != null && categoriaBO.CategoriaId > 0)
            {
                sbSql.AppendFormat(@", CategoriasPorArea (categoriaId, nomeCategoria, categoriaIdPai, Nivel) AS (
																												-- Definição do membro ancora.
																												SELECT 
																													C.categoriaId
																													, C.nomeCategoria
																													, C.categoriaIdPai
																													, 0 AS Nivel
																												FROM 
																													Categoria AS C
																												WHERE 
																													C.categoriaId = {0}
																												UNION ALL
																												-- Definição do membro recursivo.
																												SELECT 
																													C.categoriaId
																													, C.nomeCategoria
																													, C.categoriaIdPai
																													, Nivel+1
																												FROM 
																													Categoria AS C
																												INNER JOIN 
																													CategoriasPorArea AS CS ON c.CategoriaIdPai = CS.categoriaId
																												) ", categoriaBO.CategoriaId);
            }

            #endregion

            sbSql.Append(@"SELECT 
								TOP 7
								PC.produtoId
                                , Produto.disponivel
								, T.tituloId
								, T.nomeTitulo
								, T.subtituloLivro
								, Arquivo.nomeArquivo
								, 0 AS eCompraConjunta
								, CASE Produto.valorOferta WHEN 0 THEN Produto.valorUnitario ELSE Produto.valorOferta END AS valor
								, CS.categoriaId
							FROM 
								TituloImpresso TI
								INNER JOIN Titulo T ON T.tituloId = TI.tituloId
								INNER JOIN ProdutoCategoria PC ON PC.produtoId = TI.TituloImpressoId
								INNER JOIN Categorias CS ON CS.CategoriaId = PC.CategoriaId
								INNER JOIN Produto ON Produto.produtoId = PC.produtoId
								LEFT JOIN ProdutoImagem ON Produto.produtoId = ProdutoImagem.produtoId AND ProdutoImagem.produtoImagemTipoId = 1
								LEFT JOIN Arquivo ON Arquivo.arquivoId = ProdutoImagem.arquivoId
							WHERE
                                Produto.homologado = 1
								AND NOT EXISTS (
											SELECT 
												* 
											FROM 
												ProdutosComprados PCS 
											WHERE 
												PCS.produtoId = PC.produtoId
											) ");

            if (categoriaBO != null && categoriaBO.CategoriaId > 0)
            {
                sbSql.Append(@"AND EXISTS (
											SELECT 
												* 
											FROM 
												CategoriasPorArea CSA 
											WHERE 
												CSA.categoriaId=CS.categoriaId
											)");
            }

            sbSql.Append(@"ORDER BY
								NEWID()");

            command = _db.GetSqlStringCommand(sbSql.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                EstanteTituloVH estanteTituloVh = new EstanteTituloVH();
                PopulaEstanteTitulo(reader, estanteTituloVh);
                titulosParaEstante.Add(estanteTituloVh);
            }

            reader.Close();

            return titulosParaEstante;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="autor"></param>
        /// <returns></returns>
        public Int32 ContarTituloPorAutor(Autor autor)
        {
            Int32 retorno = 0;

            StringBuilder sbSQL = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total ");
            sbSQL.Append("FROM TituloAutor ");
            sbSQL.Append("WHERE autorId = @autorId ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@autorId", DbType.Int32, autor.AutorId);

            IDataReader reader = _db.ExecuteReader(command);

            if ((reader.Read()) && ((reader["Total"] != DBNull.Value)))
            {
                retorno = (Int32)reader["Total"];
            }

            reader.Close();

            return retorno;
        }

        #region [ Busca ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrosPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="ordemColunas"></param>
        /// <param name="ordemSentidos"></param>
        /// <param name="palavra"></param>
        /// <param name="palavraExata"></param>
        /// <param name="categoriaId"></param>
        /// <param name="tipoId"></param>
        /// <param name="seloId"></param>
        /// <returns></returns>
        public List<TituloVH> CarregarBuscaTituloOrdenadaPaginada(
                                                        int registrosPagina
                                                        , int numeroPagina
                                                        , string[] ordemColunas
                                                        , string[] ordemSentidos
                                                        , String palavra
                                                        , String palavraExata
                                                        , Int32 categoriaId
                                                        , String tipoId
                                                        , Int32 seloId
                                                        )
        {
            List<TituloVH> entidadesRetorno = new List<TituloVH>();

            StringBuilder sbOrder = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            // Monta o "OrderBy"
            if (ordemColunas != null)
            {
                for (int i = 0; i < ordemColunas.Length; i++)
                {
                    if (sbOrder.Length > 0)
                    {
                        sbOrder.Append(", ");
                    }

                    sbOrder.Append(ordemColunas[i] + " " + ordemSentidos[i]);
                }

                if (sbOrder.Length > 0) { sbOrder.Insert(0, " "); }
            }

            int conteudoTipoId = 0;
            bool lancamento = false;
            bool oferta = false;
            bool maisVendido = false;
            bool produtoCompraConjunta = false;

            if (!String.IsNullOrEmpty(tipoId))
            {
                if (tipoId.ToLower() != "lancamentos" && tipoId.ToLower() != "ofertas" && tipoId.ToLower() != "maisvendidos" && tipoId.ToLower() != "compraconjunta")
                {
                    if (tipoId.ToLower() != "ebooks")
                    {
                        conteudoTipoId = Convert.ToInt32(tipoId);
                    }
                    else if (tipoId.ToLower() == "ebooks")
                    {
                        conteudoTipoId = 2;
                    }
                }
                else
                {
                    switch (tipoId)
                    {
                        case "lancamentos":
                            lancamento = true;
                            break;
                        case "ofertas":
                            oferta = true;
                            break;
                        case "maisVendidos":
                            maisVendido = true;
                            sbOrder = new StringBuilder();
                            sbOrder.Append("maisVendidoOrdem ASC");
                            break;
                        case "compraConjunta":
                            produtoCompraConjunta = true;
                            break;
                        default:
                            break;
                    }
                }
            }

            command = _db.GetStoredProcCommand("ProcBuscaGeralOrdenadaPaginada");
            _db.AddInParameter(command, "palavraExata", DbType.String, palavraExata);
            _db.AddInParameter(command, "palavra", DbType.String, palavra);
            _db.AddInParameter(command, "ordem", DbType.String, sbOrder.ToString());
            _db.AddInParameter(command, "conteudoTipoId", DbType.Int32, conteudoTipoId);
            _db.AddInParameter(command, "areaId", DbType.Int32, categoriaId);
            _db.AddInParameter(command, "seloId", DbType.Int32, seloId);
            _db.AddInParameter(command, "maisVendido", DbType.Boolean, maisVendido);
            _db.AddInParameter(command, "lancamento", DbType.Boolean, lancamento);
            _db.AddInParameter(command, "oferta", DbType.Boolean, oferta);
            _db.AddInParameter(command, "produtoCompraConjunta", DbType.Boolean, produtoCompraConjunta);
            _db.AddInParameter(command, "pageIni", DbType.Int32, (((numeroPagina - 1) * registrosPagina) + 1));
            _db.AddInParameter(command, "pageEnd", DbType.Int32, ((numeroPagina) * registrosPagina));

            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                TituloVH entidadeRetorno = new TituloVH();
                PopulaTituloBusca(reader, entidadeRetorno);

                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// Método que retorna quantidade total de registros filtrando por palavra
        /// </summary>
        /// <param name="palavra"></param>
        /// <param name="categoriaId"></param>
        /// <param name="tipoId"></param>
        /// <param name="seloId"></param>
        /// <returns></returns>
        public MenuBuscaVH CarregarMenuBusca(String palavra, String palavraExata, Int32 categoriaId, String tipoId, Int32 seloId, out int totalItem)
        {
            totalItem = 0;

            MenuBuscaVH menu = new MenuBuscaVH();

            menu = this.CarregarMenuBuscaTitulo(palavra, palavraExata, categoriaId, tipoId, seloId, out totalItem);

            if (!string.IsNullOrEmpty(palavra))
            {
                menu.Conteudos = this.CarregarMenuBuscaConteudo(palavra);
            }

            return menu;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="palavra"></param>
        /// <param name="categoriaId"></param>
        /// <param name="tipoId"></param>
        /// <param name="seloId"></param>
        /// <returns></returns>
        public MenuBuscaVH CarregarMenuBusca(String palavra, Int32 categoriaId, String tipoId, Int32 seloId)
        {
            MenuBuscaVH menu = new MenuBuscaVH();

            return menu;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="palavra"></param>
        /// <param name="palavraExata"></param>
        /// <param name="categoriaId"></param>
        /// <param name="tipoId"></param>
        /// <param name="seloId"></param>
        /// <returns></returns>
        public MenuBuscaVH CarregarMenuBuscaTitulo(String palavra, String palavraExata, Int32 categoriaId, String tipoId, Int32 seloId, out int totalItem)
        {
            Boolean menuMaisVendido = false;
            Boolean menuLancamento = false;
            Boolean menuOferta = false;
            Boolean menuCompraConjunta = false;
            Int32 areaId = 0;

            MenuBuscaVH menu = new MenuBuscaVH();
            menu.Areas = new List<ItemMenuBuscaVH>();
            menu.Tipos = new List<ItemMenuBuscaVH>();
            menu.Selos = new List<ItemMenuBuscaVH>();

            ItemMenuBuscaVH itemMenuBuscaVH = new ItemMenuBuscaVH();

            StringBuilder sbOrder = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            int conteudoTipoId = 0;
            bool lancamento = false;
            bool oferta = false;
            bool maisVendido = false;
            bool produtoCompraConjunta = false;

            if (!String.IsNullOrEmpty(tipoId))
            {
                if (tipoId.ToLower() != "lancamentos" && tipoId.ToLower() != "ofertas" && tipoId.ToLower() != "maisvendidos" && tipoId.ToLower() != "compraconjunta")
                {
                    if (tipoId.ToLower() != "ebooks")
                    {
                        conteudoTipoId = Convert.ToInt32(tipoId);
                    }
                    else if (tipoId.ToLower() == "ebooks")
                    {
                        conteudoTipoId = 2;
                    }
                }
                else
                {
                    switch (tipoId)
                    {
                        case "lancamentos":
                            lancamento = true;
                            break;
                        case "ofertas":
                            oferta = true;
                            break;
                        case "maisVendidos":
                            maisVendido = true;
                            sbOrder = new StringBuilder();
                            sbOrder.Append("maisVendidoOrdem ASC");
                            break;
                        case "compraConjunta":
                            produtoCompraConjunta = true;
                            break;
                        default:
                            break;
                    }
                }
            }

            command = _db.GetStoredProcCommand("ProcBuscaGeralMenu");
            _db.AddInParameter(command, "palavraExata", DbType.String, palavraExata);
            _db.AddInParameter(command, "palavra", DbType.String, palavra);
            _db.AddInParameter(command, "conteudoTipoId", DbType.Int32, conteudoTipoId);
            _db.AddInParameter(command, "areaId", DbType.Int32, categoriaId);
            _db.AddInParameter(command, "seloId", DbType.Int32, seloId);
            _db.AddInParameter(command, "maisVendido", DbType.Boolean, maisVendido);
            _db.AddInParameter(command, "lancamento", DbType.Boolean, lancamento);
            _db.AddInParameter(command, "oferta", DbType.Boolean, oferta);
            _db.AddInParameter(command, "produtoCompraConjunta", DbType.Boolean, produtoCompraConjunta);

            reader = _db.ExecuteReader(command);

            totalItem = 0;
            //totalItem = reader.RecordsAffected;

            while (reader.Read())
            {
                totalItem++;

                #region [ Popula Reader ]

                if (reader["conteudoTipoId"] != DBNull.Value && Convert.ToInt32(reader["conteudoTipoId"].ToString()) > 0)
                {
                    conteudoTipoId = Convert.ToInt32(reader["conteudoTipoId"].ToString());

                    if (menu.Tipos != null)
                    {
                        itemMenuBuscaVH = new ItemMenuBuscaVH();
                        PopulaItemMenuBuscaTipo(reader, itemMenuBuscaVH);

                        int index = menu.Tipos.FindIndex(p => p.Id == conteudoTipoId.ToString());

                        if (index < 0)
                        {
                            menu.Tipos.Add(itemMenuBuscaVH);
                        }
                        else
                        {
                            menu.Tipos[index].Total += 1;
                        }
                    }
                }

                if (reader["areaId"] != DBNull.Value && Convert.ToInt32(reader["areaId"].ToString()) > 0)
                {
                    areaId = Convert.ToInt32(reader["areaId"].ToString());

                    if (menu.Areas != null)
                    {
                        itemMenuBuscaVH = new ItemMenuBuscaVH();
                        PopulaItemMenuBusca(reader, itemMenuBuscaVH);

                        int index = menu.Areas.FindIndex(p => p.Id == areaId.ToString());

                        if (index < 0)
                        {
                            menu.Areas.Add(itemMenuBuscaVH);
                        }
                        else
                        {
                            menu.Areas[index].Total += 1;
                        }
                    }
                }
                /*
                if (reader["maisVendido"] != DBNull.Value && Convert.ToBoolean(reader["maisVendido"].ToString()) == true)
                {
                    menuMaisVendido = Convert.ToBoolean(reader["maisVendido"].ToString());

                    if (menu.Tipos != null)
                    {
                        itemMenuBuscaVH = new ItemMenuBuscaVH();
                        PopulaItemMenuBuscaMaisVendido(reader, itemMenuBuscaVH);

                        int index = menu.Tipos.FindIndex(p => p.Id == "M");

                        if (index < 0)
                        {
                            menu.Tipos.Add(itemMenuBuscaVH);
                        }
                        else
                        {
                            menu.Tipos[index].Total += 1;
                        }
                    }
                }

                if (reader["lancamento"] != DBNull.Value && Convert.ToBoolean(reader["lancamento"].ToString()) == true)
                {
                    menuLancamento = Convert.ToBoolean(reader["lancamento"].ToString());

                    if (menu.Tipos != null)
                    {
                        itemMenuBuscaVH = new ItemMenuBuscaVH();
                        PopulaItemMenuBuscaLancamento(reader, itemMenuBuscaVH);

                        int index = menu.Tipos.FindIndex(p => p.Id == "L");

                        if (index < 0)
                        {
                            menu.Tipos.Add(itemMenuBuscaVH);
                        }
                        else
                        {
                            menu.Tipos[index].Total += 1;
                        }
                    }
                }

                if (reader["oferta"] != DBNull.Value && Convert.ToBoolean(reader["oferta"].ToString()) == true)
                {
                    menuOferta = Convert.ToBoolean(reader["oferta"].ToString());

                    if (menu.Tipos != null)
                    {
                        itemMenuBuscaVH = new ItemMenuBuscaVH();
                        PopulaItemMenuBuscaOferta(reader, itemMenuBuscaVH);

                        int index = menu.Tipos.FindIndex(p => p.Id == "O");

                        if (index < 0)
                        {
                            menu.Tipos.Add(itemMenuBuscaVH);
                        }
                        else
                        {
                            menu.Tipos[index].Total += 1;
                        }
                    }
                }
                */

                if (reader["produtoCompraConjunta"] != DBNull.Value && Convert.ToBoolean(reader["produtoCompraConjunta"].ToString()) == true)
                {
                    menuCompraConjunta = Convert.ToBoolean(reader["produtoCompraConjunta"].ToString());

                    if (menu.Tipos != null)
                    {
                        itemMenuBuscaVH = new ItemMenuBuscaVH();
                        PopulaItemMenuBuscaCompraConjunta(reader, itemMenuBuscaVH);

                        int index = menu.Tipos.FindIndex(p => p.Id == "C");

                        if (index < 0)
                        {
                            menu.Tipos.Add(itemMenuBuscaVH);
                        }
                        else
                        {
                            menu.Tipos[index].Total += 1;
                        }
                    }
                }

                if (reader["seloId"] != DBNull.Value && Convert.ToInt32(reader["seloId"].ToString()) > 0)
                {
                    seloId = Convert.ToInt32(reader["seloId"].ToString());

                    if (menu.Selos != null)
                    {
                        itemMenuBuscaVH = new ItemMenuBuscaVH();
                        PopulaItemMenuBuscaSelo(reader, itemMenuBuscaVH);

                        int index = menu.Selos.FindIndex(p => p.Id == seloId.ToString());

                        if (index < 0)
                        {
                            menu.Selos.Add(itemMenuBuscaVH);
                        }
                        else
                        {
                            menu.Selos[index].Total += 1;
                        }
                    }
                }

                #endregion
            }

            reader.Close();

            return menu;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="palavra"></param>
        /// <returns></returns>
        private List<ItemMenuBuscaVH> CarregarMenuBuscaConteudo(String palavra)
        {
            List<ItemMenuBuscaVH> Conteudos = new List<ItemMenuBuscaVH>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT 
								tipoConteudo AS conteudoTipoId
								, DescricaoConteudo AS tipo
								, COUNT(*) as totalPorTipo
							FROM
								(
								-- Revista BMJ
								SELECT 
									RA.revistaArtigoId AS conteudoId
									, 1 AS TipoConteudo
									, 'Revista BMJ' AS DescricaoConteudo
								FROM
									RevistaArtigo RA
									INNER JOIN RevistaEdicao RE ON RA.revistaEdicaoId=RE.revistaEdicaoId");

            if (!String.IsNullOrEmpty(palavra))
            {
                sbSQL.Append(@" INNER JOIN CONTAINSTABLE(RevistaArtigo, *, @palavra) AS R1 ON R1.[KEY] = RA.revistaArtigoId");
            }

            sbSQL.Append(@" WHERE
									RE.RevistaId = 1
                                    AND RA.Ativo = 1
								UNION
								-- Revista Pátio Pedagógica
								SELECT
									RA.revistaArtigoId AS conteudoId
									, 2 AS TipoConteudo
									, 'Revista Pátio Pedagógica' AS DescricaoConteudo
								FROM
									RevistaArtigo RA
									INNER JOIN RevistaEdicao RE ON RA.revistaEdicaoId=RE.revistaEdicaoId");

            if (!String.IsNullOrEmpty(palavra))
            {
                sbSQL.Append(@"     INNER JOIN CONTAINSTABLE(RevistaArtigo, *, @palavra) AS R1 ON R1.[KEY] = RA.revistaArtigoId");
            }

            sbSQL.Append(@" WHERE
									RE.RevistaId = 2
                                    AND RA.Ativo = 1
								UNION
								-- Revista Pátio Fundamental
								SELECT
									RA.revistaArtigoId AS conteudoId
									, 3 AS TipoConteudo
									, 'Revista Pátio Fundamental' AS DescricaoConteudo
								FROM
									RevistaArtigo RA
									INNER JOIN RevistaEdicao RE ON RA.revistaEdicaoId=RE.revistaEdicaoId");

            if (!String.IsNullOrEmpty(palavra))
            {
                sbSQL.Append(@" INNER JOIN CONTAINSTABLE(RevistaArtigo, *, @palavra) AS R1 ON R1.[KEY] = RA.revistaArtigoId");
            }

            sbSQL.Append(@" WHERE
									RE.RevistaId = 3
                                    AND RA.Ativo = 1
								UNION
								-- Revista Pátio Ensino Médio
								SELECT
									RA.revistaArtigoId AS conteudoId
									, 4 AS TipoConteudo
									, 'Revista Pátio Ensino Médio' AS DescricaoConteudo
								FROM
									RevistaArtigo RA
									INNER JOIN RevistaEdicao RE ON RA.revistaEdicaoId=RE.revistaEdicaoId ");

            if (!String.IsNullOrEmpty(palavra))
            {
                sbSQL.Append(@" INNER JOIN CONTAINSTABLE(RevistaArtigo, *, @palavra) AS R1 ON R1.[KEY] = RA.revistaArtigoId");
            }

            sbSQL.Append(@" WHERE
									RE.RevistaId = 4
                                    AND RA.Ativo = 1
								UNION
								-- Midias
								SELECT
									T.midiaId AS conteudoId
									, 5 AS TipoConteudo
									, 'Multimídias' AS DescricaoConteudo
								FROM
									Midia T ");

            if (!String.IsNullOrEmpty(palavra))
            {
                sbSQL.Append(@"     INNER JOIN MidiaTipo ON MidiaTipo.midiaTipoId = T.midiaTipoId
	                                INNER JOIN MidiaRevista ON MidiaRevista.midiaId = T.midiaId
                                    INNER JOIN CONTAINSTABLE(Midia, *, @palavra) AS R1 ON R1.[KEY] = T.midiaId ");
            }

            sbSQL.Append(@" UNION
								-- Eventos
								SELECT
									T.eventoId AS conteudoId
									, 6 AS TipoConteudo
									, 'Eventos' AS DescricaoConteudo
								FROM
									Evento T
									INNER JOIN ConteudoImprensa CI ON CI.conteudoImprensaId = T.eventoId ");

            if (!String.IsNullOrEmpty(palavra))
            {
                sbSQL.Append(@" LEFT JOIN CONTAINSTABLE(ConteudoImprensa, *, @palavra) AS R1 ON R1.[KEY] = CI.conteudoImprensaId 
									LEFT JOIN CONTAINSTABLE(Evento, *, @palavra) AS R2 ON R2.[KEY] = T.eventoId ");
            }

            sbSQL.Append(@" WHERE
									CI.Ativo = 1
									AND GETDATE() BETWEEN ISNULL(dataExibicaoInicio,GETDATE()) and ISNULL(dataExibicaoFim,GETDATE()) ");

            if (!String.IsNullOrEmpty(palavra))
            {
                sbSQL.Append(@" AND ( R1.[RANK] IS NOT NULL OR R2.[RANK] IS NOT NULL) ");
            }

            sbSQL.Append(@" UNION
								-- Noticias
								SELECT
									N.noticiaId AS conteudoId
									, 7 AS TipoConteudo
									, 'Notícias' AS DescricaoConteudo
								FROM
									Noticia N
									INNER JOIN ConteudoImprensa CI ON CI.conteudoImprensaId = N.noticiaId ");

            if (!String.IsNullOrEmpty(palavra))
            {
                sbSQL.Append(@" LEFT JOIN CONTAINSTABLE(ConteudoImprensa, *, @palavra) AS R1 ON R1.[KEY] = CI.conteudoImprensaId
									LEFT JOIN CONTAINSTABLE(Noticia, *, @palavra) AS R2 ON R2.[KEY] = N.noticiaId ");
            }

            sbSQL.Append(@" WHERE
									CI.Ativo = 1
									AND GETDATE() BETWEEN ISNULL(dataExibicaoInicio,GETDATE()) and ISNULL(dataExibicaoFim,GETDATE()) ");

            if (!String.IsNullOrEmpty(palavra))
            {
                sbSQL.Append(@" AND ( R1.[RANK] IS NOT NULL OR R2.[RANK] IS NOT NULL) ");
            }

            sbSQL.Append(@" ) AS R 
							GROUP BY
								R.tipoConteudo, R.DescricaoConteudo
							ORDER BY
								R.tipoConteudo, R.DescricaoConteudo");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            if (!String.IsNullOrEmpty(palavra))
            {
                _db.AddInParameter(command, "@palavra", DbType.String, palavra);
            }

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ItemMenuBuscaVH itemMenuBuscaVH = new ItemMenuBuscaVH();
                PopulaConteudoBusca(reader, itemMenuBuscaVH);
                Conteudos.Add(itemMenuBuscaVH);
            }

            reader.Close();

            return Conteudos;
        }

        #region [ Popula ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        private static void PopulaItemMenuBuscaTipo(IDataReader reader, ItemMenuBuscaVH entidade)
        {
            if (reader["conteudoTipoId"] != DBNull.Value)
            {
                entidade.Id = reader["conteudoTipoId"].ToString();
            }

            if (reader["tipo"] != DBNull.Value)
            {
                entidade.Label = reader["tipo"].ToString();
            }

            entidade.Total = 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        private static void PopulaItemMenuBusca(IDataReader reader, ItemMenuBuscaVH entidade)
        {
            if (reader["areaId"] != DBNull.Value)
            {
                entidade.Id = reader["areaId"].ToString();
            }

            if (reader["nomeArea"] != DBNull.Value)
            {
                entidade.Label = reader["nomeArea"].ToString();
            }

            entidade.Total = 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        private static void PopulaItemMenuBuscaMaisVendido(IDataReader reader, ItemMenuBuscaVH entidade)
        {
            if (reader["conteudoTipoId"] != DBNull.Value)
            {
                entidade.Id = "M";
            }

            if (reader["maisVendido"] != DBNull.Value)
            {
                entidade.Label = "Mais Vendido";
            }

            entidade.Total = 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        private static void PopulaItemMenuBuscaLancamento(IDataReader reader, ItemMenuBuscaVH entidade)
        {
            if (reader["conteudoTipoId"] != DBNull.Value)
            {
                entidade.Id = "L";
            }

            if (reader["lancamento"] != DBNull.Value)
            {
                entidade.Label = "Lançamento";
            }

            entidade.Total = 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        private static void PopulaItemMenuBuscaOferta(IDataReader reader, ItemMenuBuscaVH entidade)
        {
            if (reader["conteudoTipoId"] != DBNull.Value)
            {
                entidade.Id = "O";
            }

            if (reader["oferta"] != DBNull.Value)
            {
                entidade.Label = "Oferta";
            }

            entidade.Total = 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        private static void PopulaItemMenuBuscaCompraConjunta(IDataReader reader, ItemMenuBuscaVH entidade)
        {
            if (reader["conteudoTipoId"] != DBNull.Value)
            {
                entidade.Id = "C";
            }

            if (reader["produtoCompraConjunta"] != DBNull.Value)
            {
                entidade.Label = "Compra Conjunta";
            }

            entidade.Total = 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        private static void PopulaItemMenuBuscaSelo(IDataReader reader, ItemMenuBuscaVH entidade)
        {
            if (reader["seloId"] != DBNull.Value)
            {
                entidade.Id = reader["seloId"].ToString();
            }

            if (reader["nomeSelo"] != DBNull.Value)
            {
                entidade.Label = reader["nomeSelo"].ToString();
            }

            entidade.Total = 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        private static void PopulaTituloBusca(IDataReader reader, TituloVH entidade)
        {
            if (reader["conteudoId"] != DBNull.Value)
            {
                entidade.ProdutoId = Convert.ToInt32(reader["conteudoId"].ToString());
            }

            if (reader["areaId"] != DBNull.Value)
            {
                entidade.AreaId = Convert.ToInt32(reader["areaId"].ToString());
            }

            if (reader["categoriaId"] != DBNull.Value)
            {
                entidade.CategoriaId = Convert.ToInt32(reader["categoriaId"].ToString());
            }

            if (reader["tituloId"] != DBNull.Value)
            {
                entidade.TituloId = Convert.ToInt32(reader["tituloId"].ToString());
            }

            if (reader["nomeTitulo"] != DBNull.Value)
                entidade.NomeTitulo = reader["nomeTitulo"].ToString();

            if (reader["tipo"] != DBNull.Value)
                entidade.Tipo = reader["tipo"].ToString();

            if (reader["autores"] != DBNull.Value)
                entidade.Autores = reader["autores"].ToString();

            if (reader["arquivoIdCapa"] != DBNull.Value)
            {
                if (entidade.Arquivo == null) entidade.Arquivo = new Arquivo();
                entidade.Arquivo.ArquivoId = Convert.ToInt32(reader["arquivoIdCapa"].ToString());
            }

            if (reader["nomeArquivoCapa"] != DBNull.Value)
            {
                if (entidade.Arquivo == null) entidade.Arquivo = new Arquivo();
                entidade.Arquivo.NomeArquivo = reader["nomeArquivoCapa"].ToString();
            }

            if (reader["nomeArquivoOriginalCapa"] != DBNull.Value)
            {
                if (entidade.Arquivo == null) entidade.Arquivo = new Arquivo();
                entidade.Arquivo.NomeArquivoOriginal = reader["nomeArquivoOriginalCapa"].ToString();
            }

            if (reader["valorUnitario"] != DBNull.Value)
            {
                entidade.ValorUnitario = Convert.ToDecimal(reader["valorUnitario"].ToString());
            }

            if (reader["valorOferta"] != DBNull.Value)
            {
                entidade.ValorOferta = Convert.ToDecimal(reader["valorOferta"].ToString());
            }

            if (reader["valor"] != DBNull.Value)
            {
                entidade.Valor = Convert.ToDecimal(reader["valor"].ToString());
            }

            if (reader["parcelas"] != DBNull.Value)
            {
                entidade.Parcelas = Convert.ToInt32(reader["parcelas"].ToString());
            }

            if (reader["taxaJuros"] != DBNull.Value)
            {
                entidade.TaxaJuros = Convert.ToDecimal(reader["taxaJuros"].ToString());
            }

            if (reader["disponivel"] != DBNull.Value)
            {
                entidade.Disponivel = Convert.ToBoolean(reader["disponivel"].ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        private static void PopulaConteudoBusca(IDataReader reader, ItemMenuBuscaVH entidade)
        {
            if (reader["conteudoTipoId"] != DBNull.Value)
            {
                entidade.Id = reader["conteudoTipoId"].ToString();
            }

            if (reader["tipo"] != DBNull.Value)
            {
                entidade.Label = reader["tipo"].ToString();
            }

            if (reader["totalPorTipo"] != DBNull.Value)
            {
                entidade.Total = Convert.ToInt32(reader["totalPorTipo"].ToString());
            }
        }

        #endregion

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrosPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="revista"></param>
        /// <returns></returns>
        public List<TituloVH> CarregarTitulosPorPromocaoRevista(Int32 registrosPagina, Int32 numeroPagina, String ordem, Revista revista)
        {
            List<TituloVH> entidadesRetorno = new List<TituloVH>();

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            sbSQL.Append(@"WITH  ProdutosPromo ( produtoId )
                            AS (
                                SELECT Produto.produtoId
                                FROM PromocaoRevista
                                INNER JOIN Promocao ON Promocao.promocaoId = PromocaoRevista.promocaoId
                                INNER JOIN PromocaoCategoria ON PromocaoCategoria.promocaoId = Promocao.promocaoId
                                INNER JOIN ProdutoCategoria ON ProdutoCategoria.categoriaId = PromocaoCategoria.categoriaId
                                INNER JOIN Produto ON Produto.produtoId = ProdutoCategoria.produtoId
                                WHERE PromocaoRevista.revistaId = @revistaId
                                    AND Promocao.ativa = 1
                                    AND Promocao.aplicaAutomaticamente = 1
                                    AND Promocao.dataHoraInicio <= GETDATE()
                                    AND Promocao.dataHoraFim >= GETDATE()
                                    AND Produto.disponivel = 1
                                    AND Produto.exibirSite = 1
                                    AND Produto.homologado = 1
                                    AND EXISTS (SELECT * FROM PromocaoFaixa WHERE PromocaoFaixa.promocaoId = Promocao.promocaoId)
                                UNION ALL
                                SELECT Produto.produtoId
                                FROM PromocaoRevista
                                INNER JOIN Promocao ON Promocao.promocaoId = PromocaoRevista.promocaoId
                                INNER JOIN PromocaoProdutoTipo ON PromocaoProdutoTipo.promocaoId = Promocao.promocaoId
                                INNER JOIN Produto ON Produto.produtoTipoId = PromocaoProdutoTipo.produtoTipoId
                                WHERE PromocaoRevista.revistaId = @revistaId
                                    AND Promocao.ativa = 1
                                    AND Promocao.aplicaAutomaticamente = 1
                                    AND Promocao.dataHoraInicio <= GETDATE()
                                    AND Promocao.dataHoraFim >= GETDATE()
                                    AND Produto.disponivel = 1
                                    AND Produto.exibirSite = 1
                                    AND Produto.homologado = 1
                                    AND EXISTS (SELECT * FROM PromocaoFaixa WHERE PromocaoFaixa.promocaoId = Promocao.promocaoId)
                                UNION ALL
                                SELECT Produto.produtoId
                                FROM PromocaoRevista
                                INNER JOIN Promocao ON Promocao.promocaoId = PromocaoRevista.promocaoId
                                INNER JOIN PromocaoProduto ON PromocaoProduto.promocaoId = Promocao.promocaoId
                                INNER JOIN Produto ON Produto.produtoId = PromocaoProduto.produtoId
                                WHERE PromocaoRevista.revistaId = @revistaId
                                    AND Promocao.ativa = 1
                                    AND Promocao.aplicaAutomaticamente = 1
                                    AND Promocao.dataHoraInicio <= GETDATE()
                                    AND Promocao.dataHoraFim >= GETDATE()
                                    AND Produto.disponivel = 1
                                    AND Produto.exibirSite = 1
                                    AND Produto.homologado = 1
                                    AND EXISTS (SELECT * FROM PromocaoFaixa WHERE PromocaoFaixa.promocaoId = Promocao.promocaoId)

                            )
                            ,  Produtos ( produtoId, tituloId, conteudoTipoId, isbn13 )
                            AS (
                                SELECT TituloImpresso.tituloImpressoId,
                                    TituloImpresso.tituloId,
                                    1,
                                    TituloImpresso.isbn13
                                FROM TituloImpresso
                                INNER JOIN ProdutosPromo ON ProdutosPromo.produtoId = TituloImpresso.tituloImpressoId
                                UNION ALL
                                SELECT TituloEletronico.tituloEletronicoId,
                                    TituloEletronico.tituloId,
                                    2,
                                    TituloEletronico.isbn13
                                FROM TituloEletronico
                                INNER JOIN ProdutosPromo ON ProdutosPromo.produtoId = TituloEletronico.tituloEletronicoId
                            )
                            SELECT * 
                            FROM (
                                SELECT
                                    conteudoId = Produtos.produtoId
                                    , Produtos.conteudoTipoId
                                    , ConteudoTipo.tipo
                                    , Produtos.isbn13
                                    , Titulo.tituloId
                                    , Titulo.nomeTitulo
                                    , Produto.valorUnitario
                                    , Produto.valorOferta
                                    , dbo.AreaDeConhecimentoDaCategoria(ProdutoCategoria.categoriaId) AS areaId
                                    , ProdutoSelo.seloId
                                    , Titulo.maisVendido
                                    , Titulo.maisVendidoOrdem
                                    , CASE WHEN Titulo.dataLancamento BETWEEN (GETDATE() - 45) AND (GETDATE() + 45) THEN 1 ELSE 0 END AS Lancamento
                                    , CASE WHEN Produto.valorOferta IS NOT NULL THEN 1 ELSE 0 END AS Oferta
                                    , CASE WHEN Produto.valorOferta > 0 THEN Produto.valorOferta ELSE Produto.valorUnitario END valor
                                    , CASE WHEN Produto.valorOferta IS NOT NULL THEN (
								                                                        SELECT
									                                                        ISNULL(MAX(MPF.numeroParcelas),0)
								                                                        FROM
									                                                        dbo.MeioPagamentoFaixa MPF
									                                                        INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId
								                                                        WHERE
									                                                        MP.ativo = 1 AND valorMinimo <= Produto.valorOferta)
							                                                        ELSE (
								                                                        SELECT
									                                                        ISNULL(MAX(MPF.numeroParcelas),0)
								                                                        FROM
									                                                        dbo.MeioPagamentoFaixa MPF
									                                                        INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId
								                                                        WHERE 
									                                                        MP.ativo = 1 AND valorMinimo <= Produto.valorUnitario
								                                                        )
							                                                        END parcelas
                                    , (SELECT ISNULL(MAX(taxaJuros),0) FROM dbo.MeioPagamentoFaixa) taxaJuros
                                    , Titulo.dataLancamento
                                    , ProdutoCategoria.categoriaId
                                    , Produto.disponivel
                                    , Arquivo.arquivoId AS arquivoIdCapa
                                    , Arquivo.nomeArquivo AS nomeArquivoCapa
                                    , Arquivo.nomeArquivoOriginal AS nomeArquivoOriginalCapa
                                    , Arquivo.dataHoraUpload AS dataHoraUploadCapa
                                    , Arquivo.tamanhoArquivo AS tamanhoArquivoCapa
                                    , (
                                        SELECT
                                            Autor.nomeAutor + '; ' AS [text()]
                                        FROM
                                            Autor
                                            INNER JOIN dbo.TituloAutor ON TituloAutor.autorId = Autor.autorId AND TituloAutor.tituloId = Produtos.tituloId
                                        ORDER BY
                                            TituloAutor.ordem FOR XML PATH('')) AS Autores
                                    , ROW_NUMBER() OVER ( ORDER BY " + ordem + @" ) AS R
                                FROM Produtos
                                INNER JOIN dbo.Produto ON Produto.produtoId = Produtos.produtoId
                                INNER JOIN dbo.Titulo ON Titulo.tituloId = Produtos.tituloId
                                INNER JOIN dbo.ProdutoCategoria ON ProdutoCategoria.produtoId = Produto.produtoId
                                INNER JOIN dbo.ProdutoSelo ON ProdutoSelo.produtoId = Produto.produtoId
                                INNER JOIN ConteudoTipo ON ConteudoTipo.conteudoTipoId = Produtos.conteudoTipoId
                                LEFT JOIN dbo.ProdutoImagem ON ProdutoImagem.produtoId = Produtos.produtoId AND ProdutoImagem.produtoImagemTipoId = 1
                                LEFT JOIN dbo.Arquivo ON Arquivo.arquivoId = ProdutoImagem.arquivoId
                            ) P
                            WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaId", DbType.Int32, revista.RevistaId);

            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                TituloVH entidadeRetorno = new TituloVH();
                PopulaTituloBusca(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revista"></param>
        /// <returns></returns>
        public Int32 ContarTitulosPorPromocaoRevista(Revista revista)
        {
            Int32 retorno = 0;

            StringBuilder sbSQL = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();

            sbSQL.Append(@"WITH  ProdutosPromo ( produtoId )
                            AS (
                                SELECT Produto.produtoId
                                FROM PromocaoRevista
                                INNER JOIN Promocao ON Promocao.promocaoId = PromocaoRevista.promocaoId
                                INNER JOIN PromocaoCategoria ON PromocaoCategoria.promocaoId = Promocao.promocaoId
                                INNER JOIN ProdutoCategoria ON ProdutoCategoria.categoriaId = PromocaoCategoria.categoriaId
                                INNER JOIN Produto ON Produto.produtoId = ProdutoCategoria.produtoId
                                WHERE PromocaoRevista.revistaId = @revistaId
                                    AND Promocao.ativa = 1
                                    AND Promocao.aplicaAutomaticamente = 1
                                    AND Promocao.dataHoraInicio <= GETDATE()
                                    AND Promocao.dataHoraFim >= GETDATE()
                                    AND Produto.disponivel = 1
                                    AND Produto.exibirSite = 1
                                    AND Produto.homologado = 1
                                    AND EXISTS (SELECT * FROM PromocaoFaixa WHERE PromocaoFaixa.promocaoId = Promocao.promocaoId)
                                UNION ALL
                                SELECT Produto.produtoId
                                FROM PromocaoRevista
                                INNER JOIN Promocao ON Promocao.promocaoId = PromocaoRevista.promocaoId
                                INNER JOIN PromocaoProdutoTipo ON PromocaoProdutoTipo.promocaoId = Promocao.promocaoId
                                INNER JOIN Produto ON Produto.produtoTipoId = PromocaoProdutoTipo.produtoTipoId
                                WHERE PromocaoRevista.revistaId = @revistaId
                                    AND Promocao.ativa = 1
                                    AND Promocao.aplicaAutomaticamente = 1
                                    AND Promocao.dataHoraInicio <= GETDATE()
                                    AND Promocao.dataHoraFim >= GETDATE()
                                    AND Produto.disponivel = 1
                                    AND Produto.exibirSite = 1
                                    AND Produto.homologado = 1
                                    AND EXISTS (SELECT * FROM PromocaoFaixa WHERE PromocaoFaixa.promocaoId = Promocao.promocaoId)
                                UNION ALL
                                SELECT Produto.produtoId
                                FROM PromocaoRevista
                                INNER JOIN Promocao ON Promocao.promocaoId = PromocaoRevista.promocaoId
                                INNER JOIN PromocaoProduto ON PromocaoProduto.promocaoId = Promocao.promocaoId
                                INNER JOIN Produto ON Produto.produtoId = PromocaoProduto.produtoId
                                WHERE PromocaoRevista.revistaId = @revistaId
                                    AND Promocao.ativa = 1
                                    AND Promocao.aplicaAutomaticamente = 1
                                    AND Promocao.dataHoraInicio <= GETDATE()
                                    AND Promocao.dataHoraFim >= GETDATE()
                                    AND Produto.disponivel = 1
                                    AND Produto.exibirSite = 1
                                    AND Produto.homologado = 1
                                    AND EXISTS (SELECT * FROM PromocaoFaixa WHERE PromocaoFaixa.promocaoId = Promocao.promocaoId)
                            )
                            ,  Produtos ( produtoId, tituloId, conteudoTipoId, isbn13 )
                            AS (
                                select TituloImpresso.tituloImpressoId,
                                    TituloImpresso.tituloId,
                                    1,
                                    TituloImpresso.isbn13
                                from TituloImpresso
                                inner join ProdutosPromo on ProdutosPromo.produtoId = TituloImpresso.tituloImpressoId
                                union all
                                select TituloEletronico.tituloEletronicoId,
                                    TituloEletronico.tituloId,
                                    2,
                                    TituloEletronico.isbn13
                                from TituloEletronico
                                inner join ProdutosPromo on ProdutosPromo.produtoId = TituloEletronico.tituloEletronicoId
                            )
                            SELECT COUNT(Produtos.produtoId) AS Total
                            from Produtos
                            INNER JOIN dbo.Produto ON Produto.produtoId = Produtos.produtoId
                            INNER JOIN dbo.Titulo ON Titulo.tituloId = Produtos.tituloId
                            INNER JOIN dbo.ProdutoCategoria ON ProdutoCategoria.produtoId = Produto.produtoId
                            INNER JOIN dbo.ProdutoSelo ON ProdutoSelo.produtoId = Produto.produtoId
                            INNER JOIN ConteudoTipo ON ConteudoTipo.conteudoTipoId = Produtos.conteudoTipoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaId", DbType.Int32, revista.RevistaId);

            IDataReader reader = _db.ExecuteReader(command);

            if ((reader.Read()) && ((reader["Total"] != DBNull.Value)))
            {
                retorno = (Int32)reader["Total"];
            }

            reader.Close();

            return retorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrosPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="revista"></param>
        /// <returns></returns>
        public List<TituloVH> CarregarTodosTitulosPorPromocaoRevista(Int32 registrosPagina, Int32 numeroPagina, String ordem)
        {
            List<TituloVH> entidadesRetorno = new List<TituloVH>();

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            sbSQL.Append(@"WITH  ProdutosPromo ( produtoId )
                            AS (
	                            SELECT Produto.produtoId
	                            FROM Produto
	                            WHERE Produto.disponivel = 1
		                            AND Produto.exibirSite = 1
		                            AND Produto.homologado = 1
                            )
                            ,  Produtos ( produtoId, tituloId, conteudoTipoId, isbn13 )
                            AS (
	                            SELECT TituloImpresso.tituloImpressoId,
		                            TituloImpresso.tituloId,
		                            1,
		                            TituloImpresso.isbn13
	                            FROM TituloImpresso
	                            INNER JOIN ProdutosPromo ON ProdutosPromo.produtoId = TituloImpresso.tituloImpressoId
	                            UNION ALL
	                            SELECT TituloEletronico.tituloEletronicoId,
		                            TituloEletronico.tituloId,
		                            2,
		                            TituloEletronico.isbn13
	                            FROM TituloEletronico
	                            INNER JOIN ProdutosPromo ON ProdutosPromo.produtoId = TituloEletronico.tituloEletronicoId
                            )
                            SELECT * 
                            FROM (
	                            SELECT
		                            conteudoId = Produtos.produtoId
		                            , Produtos.conteudoTipoId
		                            , ConteudoTipo.tipo
		                            , Produtos.isbn13
		                            , Titulo.tituloId
		                            , Titulo.nomeTitulo
		                            , Produto.valorUnitario
		                            , Produto.valorOferta
		                            , dbo.AreaDeConhecimentoDaCategoria(ProdutoCategoria.categoriaId) AS areaId
		                            , ProdutoSelo.seloId
		                            , Titulo.maisVendido
		                            , Titulo.maisVendidoOrdem
		                            , CASE WHEN Titulo.dataLancamento BETWEEN (GETDATE() - 45) AND (GETDATE() + 45) THEN 1 ELSE 0 END AS Lancamento
		                            , CASE WHEN Produto.valorOferta IS NOT NULL THEN 1 ELSE 0 END AS Oferta
		                            , CASE WHEN Produto.valorOferta > 0 THEN Produto.valorOferta ELSE Produto.valorUnitario END valor
		                            , CASE WHEN Produto.valorOferta IS NOT NULL THEN (
															                            SELECT
																                            ISNULL(MAX(MPF.numeroParcelas),0)
															                            FROM
																                            dbo.MeioPagamentoFaixa MPF
																                            INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId
															                            WHERE
																                            MP.ativo = 1 AND valorMinimo <= Produto.valorOferta)
														                            ELSE (
															                            SELECT
																                            ISNULL(MAX(MPF.numeroParcelas),0)
															                            FROM
																                            dbo.MeioPagamentoFaixa MPF
																                            INNER JOIN MeioPagamento MP ON MP.meioPagamentoId = MPF.meioPagamentoId
															                            WHERE 
																                            MP.ativo = 1 AND valorMinimo <= Produto.valorUnitario
															                            )
														                            END parcelas
		                            , (SELECT ISNULL(MAX(taxaJuros),0) FROM dbo.MeioPagamentoFaixa) taxaJuros
		                            , Titulo.dataLancamento
		                            , ProdutoCategoria.categoriaId
		                            , Produto.disponivel
		                            , Arquivo.arquivoId AS arquivoIdCapa
		                            , Arquivo.nomeArquivo AS nomeArquivoCapa
		                            , Arquivo.nomeArquivoOriginal AS nomeArquivoOriginalCapa
		                            , Arquivo.dataHoraUpload AS dataHoraUploadCapa
		                            , Arquivo.tamanhoArquivo AS tamanhoArquivoCapa
		                            , (
			                            SELECT
				                            Autor.nomeAutor + '; ' AS [text()]
			                            FROM
				                            Autor
				                            INNER JOIN dbo.TituloAutor ON TituloAutor.autorId = Autor.autorId AND TituloAutor.tituloId = Produtos.tituloId
			                            ORDER BY
				                            TituloAutor.ordem FOR XML PATH('')) AS Autores
		                            , ROW_NUMBER() OVER ( ORDER BY " + ordem + @" ) AS R
	                            FROM Produtos
	                            INNER JOIN dbo.Produto ON Produto.produtoId = Produtos.produtoId
	                            INNER JOIN dbo.Titulo ON Titulo.tituloId = Produtos.tituloId
	                            INNER JOIN dbo.ProdutoCategoria ON ProdutoCategoria.produtoId = Produto.produtoId
	                            INNER JOIN dbo.ProdutoSelo ON ProdutoSelo.produtoId = Produto.produtoId
	                            INNER JOIN ConteudoTipo ON ConteudoTipo.conteudoTipoId = Produtos.conteudoTipoId
	                            LEFT JOIN dbo.ProdutoImagem ON ProdutoImagem.produtoId = Produtos.produtoId AND ProdutoImagem.produtoImagemTipoId = 1
	                            LEFT JOIN dbo.Arquivo ON Arquivo.arquivoId = ProdutoImagem.arquivoId
                            ) P
                            WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                TituloVH entidadeRetorno = new TituloVH();
                PopulaTituloBusca(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revista"></param>
        /// <returns></returns>
        public Int32 ContarTodosTitulosPorPromocaoRevista()
        {
            Int32 retorno = 0;

            StringBuilder sbSQL = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();

            sbSQL.Append(@"WITH  ProdutosPromo ( produtoId )
                            AS (
	                            SELECT Produto.produtoId
	                            FROM Produto
	                            WHERE Produto.disponivel = 1
		                            AND Produto.exibirSite = 1
		                            AND Produto.homologado = 1
                            )
                            ,  Produtos ( produtoId, tituloId, conteudoTipoId, isbn13 )
                            AS (
	                            select TituloImpresso.tituloImpressoId,
		                            TituloImpresso.tituloId,
		                            1,
		                            TituloImpresso.isbn13
	                            from TituloImpresso
	                            inner join ProdutosPromo on ProdutosPromo.produtoId = TituloImpresso.tituloImpressoId
	                            union all
	                            select TituloEletronico.tituloEletronicoId,
		                            TituloEletronico.tituloId,
		                            2,
		                            TituloEletronico.isbn13
	                            from TituloEletronico
	                            inner join ProdutosPromo on ProdutosPromo.produtoId = TituloEletronico.tituloEletronicoId
                            )
                            SELECT COUNT(Produtos.produtoId) AS Total
                            from Produtos
                            INNER JOIN dbo.Produto ON Produto.produtoId = Produtos.produtoId
                            INNER JOIN dbo.Titulo ON Titulo.tituloId = Produtos.tituloId
                            INNER JOIN dbo.ProdutoCategoria ON ProdutoCategoria.produtoId = Produto.produtoId
                            INNER JOIN dbo.ProdutoSelo ON ProdutoSelo.produtoId = Produto.produtoId
                            INNER JOIN ConteudoTipo ON ConteudoTipo.conteudoTipoId = Produtos.conteudoTipoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            IDataReader reader = _db.ExecuteReader(command);

            if ((reader.Read()) && ((reader["Total"] != DBNull.Value)))
            {
                retorno = (Int32)reader["Total"];
            }

            reader.Close();

            return retorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tituloId"></param>
        /// <returns></returns>
        public Int32 CarregarCategoriaPorTituloId(Int32 tituloId)
        {
            int retorno = 0;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT TOP 1 categoriaId FROM (
	                        SELECT ProdutoCategoria.categoriaId, 0 AS seq FROM TituloImpresso
	                        INNER JOIN Produto ON Produto.produtoId = TituloImpresso.tituloImpressoId
	                        INNER JOIN ProdutoCategoria ON ProdutoCategoria.produtoId = Produto.produtoId
	                        WHERE tituloId = @tituloId
	                        UNION ALL
	                        SELECT ProdutoCategoria.categoriaId, 1 AS seq FROM TituloEletronico
	                        INNER JOIN Produto ON Produto.produtoId = TituloEletronico.tituloEletronicoId
	                        INNER JOIN ProdutoCategoria ON ProdutoCategoria.produtoId = Produto.produtoId
	                        WHERE tituloId = @tituloId
                        )AS C
                        ORDER BY seq");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloId", DbType.Int32, tituloId);

            IDataReader reader = _db.ExecuteReader(command);

            if ((reader.Read()) && ((reader["categoriaId"] != DBNull.Value)))
            {
                retorno = (Int32)reader["categoriaId"];
            }
            reader.Close();

            return retorno;
        }
    }
}