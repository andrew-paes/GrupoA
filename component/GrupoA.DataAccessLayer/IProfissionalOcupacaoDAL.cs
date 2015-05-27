
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
    public partial interface IProfissionalOcupacaoDAL
    {	
        void Inserir(ProfissionalOcupacao entidade);
        void Atualizar(ProfissionalOcupacao entidade);
        void Excluir(ProfissionalOcupacao entidade);
        ProfissionalOcupacao Carregar(ProfissionalOcupacao entidade);
		IEnumerable<ProfissionalOcupacao> Carregar(Usuario entidade);
        IEnumerable<ProfissionalOcupacao> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<ProfissionalOcupacao> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
