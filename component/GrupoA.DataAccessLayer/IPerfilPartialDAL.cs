
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
        List<Perfil> CarregarPerfisDoUsuario(Usuario entidade);
        List<Perfil> CarregarTodosPorPromocao(Promocao promocao);
        List<Perfil> CarregarTodosExcetoPerfis(List<Perfil> perfis);
	}
}
