using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.DataAccess;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;
using GrupoA.DataAccess.ADO;
using GrupoA.BusinessObject.Enumerator;
using System.Collections;
using System.Transactions;

namespace GrupoA.BusinessLogicalLayer
{
	public class ClippingBLL : BaseBLL
	{
		#region Declarações DAL

		private IClippingDAL _clippingDAL;
		private IClippingImagemDAL _clippingImagemDAL;
		private IConteudoDAL _conteudoDAL;
		private IArquivoDAL _arquivoDAL;

		private IClippingDAL ClippingDAL
		{
			get
			{
				if (_clippingDAL == null)
					_clippingDAL = new ClippingADO();
				return _clippingDAL;
			}
		}
		private IConteudoDAL ConteudoDAL
		{
			get
			{
				if (_conteudoDAL == null)
					_conteudoDAL = new ConteudoADO();
				return _conteudoDAL;
			}
		}
		private IArquivoDAL ArquivoDAL
		{
			get
			{
				if (_arquivoDAL == null)
					_arquivoDAL = new ArquivoADO();
				return _arquivoDAL;
			}
		}
		private IClippingImagemDAL ClippingImagemDAL
		{
			get
			{
				if (_clippingImagemDAL == null)
					_clippingImagemDAL = new ClippingImagemADO();
				return _clippingImagemDAL;
			}
		}
		private IConteudoImprensaDAL _conteudoImprensaDAL;
		private IConteudoImprensaDAL ConteudoImprensaDAL
		{
			get
			{
				if (_conteudoImprensaDAL == null)
					_conteudoImprensaDAL = new ConteudoImprensaADO();
				return _conteudoImprensaDAL;
			}
		}
		#endregion

		#region Métodos: Clipping

		public int ContarTodosClippingValidosComDependencias()
		{
			return ClippingDAL.ContarTodosValidosComDependencias();
		}

		public Clipping Carregar(Clipping entidade)
		{
			entidade = ClippingDAL.CarregarComDependencias(entidade);
			return entidade;
		}

		//public Clipping CarregarAntigo(Clipping entidade)
		//{
		//    entidade = ClippingDAL.CarregarAutor(entidade);
		//    return entidade;
		//}

		public IEnumerable<Clipping> CarregarClippingValidosComDependencias(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
		{
			return ClippingDAL.CarregarTodosValidosComDependencias(registrosPagina, numeroPagina, ordemColunas, ordemSentidos, filtro);
		}

		public Clipping Inserir(Clipping entidade,List<Categoria> categorias)
		{
			using (TransactionScope scope = new TransactionScope())
			{
				var conteudo = new Conteudo();
				conteudo.ConteudoTipo = new ConteudoTipo();
				conteudo.ConteudoTipo.ConteudoTipoId = (int)TipoDeConteudo.Clipping;
				conteudo.DataHoraCadastro = DateTime.Now;
				ConteudoDAL.Inserir(conteudo);
				// Inserção de Conteúdo Imprensa
				ConteudoImprensa conteudoImprensa = entidade.ConteudoImprensa;
				conteudoImprensa.ConteudoImprensaId = conteudo.ConteudoId;
				ConteudoImprensaDAL.Inserir(conteudoImprensa);
				entidade.ConteudoImprensa = conteudoImprensa;
				entidade.ClippingId = conteudo.ConteudoId;
                // Inserção de Categorias
                foreach (Categoria categoria in categorias)
                    if (ConteudoDAL != null) ConteudoDAL.InserirRelacionamentoAreaConhecimento(conteudo, categoria);
				ClippingDAL.Inserir(entidade);

				scope.Complete();
				return entidade;
			}
		}

		//public Clipping InserirAntigo(Clipping entidade)
		//{    
		//    var conteudo = new Conteudo();
		//    conteudo.ConteudoTipo = new ConteudoTipo();
		//    conteudo.ConteudoTipo.ConteudoTipoId = (int)TipoDeConteudo.Clipping;
		//    conteudo.DataHoraCadastro = DateTime.Now;
		//    ConteudoDAL.InserirNovoAutor(conteudo);

		//    entidade.ClippingId = conteudo.ConteudoId;
		//    ClippingDAL.InserirNovoAutor(entidade);

		//    return entidade;
		//}

		public void Atualizar(Clipping clipping, List<Categoria> categorias)
		{
			using (TransactionScope scope = new TransactionScope())
			{
				// Atualização dos Ids
				clipping.ConteudoImprensa.ConteudoImprensaId = clipping.ClippingId;
				clipping.ConteudoImprensa.Conteudo = new Conteudo() { ConteudoId = clipping.ClippingId };

				// Atualização de Conteúdo Imprensa
				ConteudoImprensa conteudoImprensa = clipping.ConteudoImprensa;
				conteudoImprensa.ConteudoImprensaId = clipping.ClippingId;
				ConteudoImprensaDAL.Atualizar(conteudoImprensa);

                // Atualização de Categorias
                // a. Exclui todos os relacionamentos com áreas de conhecimento
                ConteudoDAL.ExcluirTodasAreasConhecimento(clipping.ConteudoImprensa.Conteudo);
                // b. Inclui os novos relacionamentos
                foreach (Categoria categoria in categorias)
                    ConteudoDAL.InserirRelacionamentoAreaConhecimento(clipping.ConteudoImprensa.Conteudo, categoria);

				// Atualização de Evento com mesmo código identificador (Id)
				ClippingDAL.Atualizar(clipping);

				scope.Complete();

			}
		}

		//public void AtualizarAntigo(Clipping entidade)
		//{
		//    ClippingDAL.AtualizarAutor(entidade);
		//}

		#endregion

		#region Métodos: Clipping Imagem

		public IEnumerable<ClippingImagem> CarregarTodosClippingImagem(ClippingImagem entidade)
		{
			var clippingImagemFH = new ClippingImagemFH() { ClippingId = entidade.Clipping.ClippingId.ToString() };
			if (entidade.Arquivo != null)
				clippingImagemFH.ArquivoId = entidade.Arquivo.ArquivoId.ToString();
			IEnumerable<ClippingImagem> imagens = ClippingImagemDAL.CarregarTodosArquivos(0, 0, null, null, clippingImagemFH);
			return imagens;
		}

		public ClippingImagem InserirClippingImagem(ClippingImagem entidade)
		{
			if (entidade.Arquivo != null && entidade.Arquivo.ArquivoId == 0)
			{
				var arquivo = new Arquivo();
				arquivo.NomeArquivo = entidade.Arquivo.NomeArquivo;
				arquivo.NomeArquivoOriginal = entidade.Arquivo.NomeArquivoOriginal;
				arquivo.TamanhoArquivo = entidade.Arquivo.TamanhoArquivo;
				arquivo.DataHoraUpload = entidade.Arquivo.DataHoraUpload;
				ArquivoDAL.Inserir(arquivo);
				entidade.Arquivo.ArquivoId = arquivo.ArquivoId;
				ClippingImagemDAL.Inserir(entidade);
			}
			else if ((entidade.Arquivo != null && entidade.Arquivo.ArquivoId > 0) && entidade.Clipping != null && entidade.Clipping.ClippingId > 0)
			{
				ClippingImagemDAL.Inserir(entidade);
			}

			return entidade;
		}

		public ClippingImagem ExcluirClippingImagem(ClippingImagem entidade)
		{
			ClippingImagemDAL.ExcluirImagem(entidade);
			return entidade;
		}

		public ClippingImagem CarregarClippingImagem(ClippingImagem entidade)
		{
			return ClippingImagemDAL.CarregarClippingImagem(entidade);
		}

        public ClippingImagem CarregarClippingImagem(Clipping entidade)
        {
            return ClippingImagemDAL.CarregarClippingImagem(entidade);
        }

		public void AtualizarClippingImagem(ClippingImagem entidade)
		{
			ClippingImagemDAL.Atualizar(entidade);
		}

		#endregion

		#region Métodos: Arquivo

		public Arquivo CarregarArquivo(Arquivo entidade)
		{
			return ArquivoDAL.Carregar(entidade);
		}

		#endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public List<Clipping> CarregarClippingsPorCategoria(Categoria categoria)
        {
            return ClippingDAL.CarregarClippingsPorCategoria(categoria);
        }
	}
}
