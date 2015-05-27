
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
    public partial interface IMidiaDAL
    {	
        void Inserir(Midia entidade);
        void Atualizar(Midia entidade);
        void Excluir(Midia entidade);
        Midia Carregar(Midia entidade);
				
		Midia CarregarComDependencias(Midia entidade);	
		
		IEnumerable<Midia> Carregar(Arquivo entidade);
		
		IEnumerable<Midia> Carregar(MidiaTipo entidade);
				
        IEnumerable<Midia> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<Midia> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
