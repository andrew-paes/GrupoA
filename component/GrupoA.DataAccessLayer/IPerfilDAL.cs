
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
    public partial interface IPerfilDAL
    {	
        void Inserir(Perfil entidade);
        void Atualizar(Perfil entidade);
        void Excluir(Perfil entidade);
        Perfil Carregar(Perfil entidade);
		
		IEnumerable<Perfil> Carregar(Promocao entidade);
		
		IEnumerable<Perfil> Carregar(Usuario entidade);
				
        IEnumerable<Perfil> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<Perfil> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
