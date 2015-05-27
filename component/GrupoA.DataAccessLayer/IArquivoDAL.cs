
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
using System.Text;
using System.Collections.Generic;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;

namespace GrupoA.DataAccess
{
    public partial interface IArquivoDAL
    {	
        void Inserir(Arquivo entidade);
        void Atualizar(Arquivo entidade);
        void Excluir(Arquivo entidade);
        Arquivo Carregar(Arquivo entidade);
		
		IEnumerable<Arquivo> Carregar(Autor entidade);
		
		IEnumerable<Arquivo> Carregar(Banner entidade);
		
		IEnumerable<Arquivo> Carregar(ClippingImagem entidade);
		
		IEnumerable<Arquivo> Carregar(CursoPanamericano entidade);
		
		IEnumerable<Arquivo> Carregar(Evento entidade);
		
		IEnumerable<Arquivo> Carregar(EventoImagem entidade);
		
		IEnumerable<Arquivo> Carregar(Midia entidade);
		
		IEnumerable<Arquivo> Carregar(Noticia entidade);
		
		IEnumerable<Arquivo> Carregar(NoticiaImagem entidade);
		
		IEnumerable<Arquivo> Carregar(PaginaPromocional entidade);
		
		IEnumerable<Arquivo> Carregar(ProdutoImagem entidade);
		
		IEnumerable<Arquivo> Carregar(ProfessorComprovanteDocencia entidade);
		
		IEnumerable<Arquivo> Carregar(ProgramaAtualizacaoChamada entidade);
		
		IEnumerable<Arquivo> Carregar(RevistaArtigo entidade);
		
		IEnumerable<Arquivo> Carregar(RevistaGrupoAEdicao entidade);
		
		IEnumerable<Arquivo> Carregar(TituloConteudoExtraArquivo entidade);
		
		IEnumerable<Arquivo> Carregar(TituloImagemResumo entidade);
		
		IEnumerable<Arquivo> Carregar(TituloInformacaoComentarioEspecialista entidade);
		
		IEnumerable<Arquivo> Carregar(TituloInformacaoSobreAutor entidade);
		
		IEnumerable<Arquivo> Carregar(TituloInformacaoSumario entidade);
				
        IEnumerable<Arquivo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<Arquivo> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
