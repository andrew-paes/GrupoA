
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
    public partial interface ITelefoneDAL
    {	
        void Inserir(Telefone entidade);
        void Atualizar(Telefone entidade);
        void Excluir(Telefone entidade);
        Telefone Carregar(Telefone entidade);
		
		IEnumerable<Telefone> Carregar(TelefoneTipo entidade);
		
		IEnumerable<Telefone> Carregar(Usuario entidade);
		
		Telefone Carregar(ProfessorInstituicao entidade);
				
        IEnumerable<Telefone> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<Telefone> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
