
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
    public partial interface IClippingImagemDAL
    {	
        void ExcluirImagem(ClippingImagem entidade);
        IEnumerable<ClippingImagem> CarregarTodosArquivos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        ClippingImagem CarregarClippingImagem(ClippingImagem entidade);
        ClippingImagem CarregarClippingImagem(Clipping entidade);
	}
}
