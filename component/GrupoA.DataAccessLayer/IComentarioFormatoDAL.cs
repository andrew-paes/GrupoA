
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
    public partial interface IComentarioFormatoDAL
    {	
        void Inserir(ComentarioFormato entidade);
        void Atualizar(ComentarioFormato entidade);
        void Excluir(ComentarioFormato entidade);
        ComentarioFormato Carregar(ComentarioFormato entidade);
		
		IEnumerable<ComentarioFormato> Carregar(TituloInformacaoComentarioEspecialista entidade);
				
        IEnumerable<ComentarioFormato> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<ComentarioFormato> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
