
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
    public partial interface ITelefoneTipoDAL
    {	
        void Inserir(TelefoneTipo entidade);
        void Atualizar(TelefoneTipo entidade);
        void Excluir(TelefoneTipo entidade);
        TelefoneTipo Carregar(TelefoneTipo entidade);
		
		IEnumerable<TelefoneTipo> Carregar(Telefone entidade);
				
        IEnumerable<TelefoneTipo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<TelefoneTipo> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
