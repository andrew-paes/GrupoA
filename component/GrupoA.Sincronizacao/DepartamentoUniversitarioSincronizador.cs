using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Prospect = GrupoA.Sincronizacao.ServicoProspect;
//using Customer = GrupoA.Sincronizacao.ServicoCustomer;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using GrupoA.BusinessObject.Enumerator;
using System.Xml;

namespace GrupoA.Sincronizacao
{
	public class DepartamentoUniversitarioSincronizador
	{
		#region [ Propriedades ]

		LogBLL _logBLL;
		protected LogBLL logBLL
		{
			get
			{
				if (_logBLL == null)
				{
					_logBLL = new LogBLL();
				}
				return _logBLL;
			}
		}

		ImportacaoBLL _importacaoBLL;
		protected ImportacaoBLL importacaoBLL
		{
			get
			{
				if (_importacaoBLL == null)
				{
					_importacaoBLL = new ImportacaoBLL();
				}
				return _importacaoBLL;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public enum TabelaImportacao
		{
			Produto = 1,
			Grupo = 2,
			Autor = 3,
			Instituicao = 4,
			Curso = 5,
			Disciplina = 6
		}

		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="enumLogCategoria"></param>
		/// <param name="logEventos"></param>
		/// <param name="usuario"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		public LogDados CriarLogDados(EnumLogCategoria enumLogCategoria, LogEventos logEventos, Usuario usuario, string message)
		{
			LogDados logDados = new LogDados();
			logDados.Adicionar("LogEvento", "LOG_EVENTO", Convert.ToInt32(logEventos).ToString());
			logDados.Adicionar("LogCategoria", "LOG_CATEGORIA", Convert.ToInt32(enumLogCategoria).ToString());
			logDados.Adicionar("DataHora", "DATA_HORA", Convert.ToInt32(enumLogCategoria).ToString());
			logDados.Adicionar("Usuario", "USUARIO_ID", usuario.UsuarioId.ToString());
			logDados.Adicionar("MensagemErro", "MENSAGEM_ERRO", message);
			return logDados;
		}

		/// <summary>
		/// Metodo que executa a leitura e a insercao dos dados.
		/// </summary>
		/// <param name="arquivo">Nome e caminho do arquivo a ser importado.</param>
		/// <param name="tabela">Nome da tabela onde sera inserido os dados.</param>
		public void ImportacaoArquivo(string arquivo, TabelaImportacao tabela)
		{
			string NomeTabela = BuscaNomeTabela(tabela);
			string sql = LerArquivo(arquivo, NomeTabela);
			InserirLote(sql);
		}

		/// <summary>
		/// Metodo que retorna o nome da tabela a ser inserido os dados no banco.
		/// </summary>
		/// <param name="tabela">Identidicador da tabela.</param>
		/// <returns>Nome da tabela.</returns>
		public string BuscaNomeTabela(TabelaImportacao tabela)
		{
			switch (tabela)
			{
				case TabelaImportacao.Produto:
					{
						return "ImportacaoProduto";
					}
				case TabelaImportacao.Grupo:
					{
						return "ImportacaoGrupo";
					}
				case TabelaImportacao.Autor:
					{
						return "ImportacaoAutor";
					}
				case TabelaImportacao.Instituicao:
					{
						return "ImportacaoInstituicao";
					}
				case TabelaImportacao.Curso:
					{
						return "ImportacaoCurso";
					}
				case TabelaImportacao.Disciplina:
					{
						return "ImportacaoDisciplina";
					}
				default:
					{
						return string.Empty;
					}
			}
		}

		/// <summary>
		/// Faz leitura de um arquivo e retorna string sql para ser executada.
		/// </summary>
		/// <param name="arquivo">Nome e caminho do arquivo a ser importado.</param>
		/// <param name="tabela">Nome da tabela onde sera inserido os dados.</param>
		/// <returns>String com o sql a ser executado.</returns>
		private string LerArquivo(string arquivo, string tabela)
		{
			if (!System.IO.File.Exists(arquivo))
			{
				throw (new System.IO.FileNotFoundException("Não foi Possível Localizar o Arquivo Especificado!"));
			}

			string linha = string.Empty;
			StringBuilder sbSQL;
			System.IO.FileInfo fi = new System.IO.FileInfo(arquivo);

			using (System.IO.StreamReader sr = fi.OpenText())
			{
				int contadorlinha = 0;

				sbSQL = new StringBuilder();

				while ((linha = sr.ReadLine()) != null)
				{
					// sbSQL = new StringBuilder();

					if (contadorlinha != 0)
					{
						sbSQL.Append("INSERT INTO " + tabela + " VALUES ( ");
						sbSQL.AppendFormat("'{0}'", linha.Replace("'", "''").Replace(";", "','"));
						sbSQL.Append(",GETDATE()");
						sbSQL.Append(", NULL");
						sbSQL.Append("); ");

						// InserirLote(sbSQL.ToString());
					}

					contadorlinha++;
				}
			}

			return sbSQL.ToString();
		}

		/// <summary>
		/// Metodo que chamda a camada de dados para insercao dos dados.
		/// </summary>
		/// <param name="sql">String com o sql a ser executado.</param>
		public void InserirLote(string sql)
		{
			importacaoBLL.InserirLote(sql);
		}
	}
}