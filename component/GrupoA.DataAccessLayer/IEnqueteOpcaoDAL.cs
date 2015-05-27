
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
    public partial interface IEnqueteOpcaoDAL
    {	
        void Inserir(EnqueteOpcao entidade);
        void Atualizar(EnqueteOpcao entidade);
        void Excluir(EnqueteOpcao entidade);
        EnqueteOpcao Carregar(EnqueteOpcao entidade);
		
		IEnumerable<EnqueteOpcao> Carregar(Enquete entidade);
				
        IEnumerable<EnqueteOpcao> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<EnqueteOpcao> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
