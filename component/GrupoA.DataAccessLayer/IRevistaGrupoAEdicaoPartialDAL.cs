
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
    public partial interface IRevistaGrupoAEdicaoDAL
    {
        int ValidarAtualizacao(RevistaGrupoAEdicao revista);
        RevistaGrupoAEdicao CarregarComArquivos(RevistaGrupoAEdicao entidade);
        List<RevistaGrupoAEdicao> CarregarTodosComArquivos();
        List<RevistaGrupoAEdicao> CarregarTodosComArquivos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
	}
}
