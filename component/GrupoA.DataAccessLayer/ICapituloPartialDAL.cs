
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
    public partial interface ICapituloDAL
    {
        List<Autor> CarregarAutoresDoCapitulo(Capitulo captiulo);
        void ExcluirCapituloAutor(Capitulo capitulo);
        void InserirAutoresDeCapitulo(Capitulo capitulo, List<Autor> autores);
        Capitulo CarregarPorCodigoLegado(Capitulo entidade);
        void AtualizarMenosNomeResumo(Capitulo entidade);
	}
}
