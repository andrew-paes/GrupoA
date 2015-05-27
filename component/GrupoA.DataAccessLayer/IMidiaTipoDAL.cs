
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
    public partial interface IMidiaTipoDAL
    {	
        void Inserir(MidiaTipo entidade);
        void Atualizar(MidiaTipo entidade);
        void Excluir(MidiaTipo entidade);
        MidiaTipo Carregar(MidiaTipo entidade);
		
		IEnumerable<MidiaTipo> Carregar(Midia entidade);
				
        IEnumerable<MidiaTipo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<MidiaTipo> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
