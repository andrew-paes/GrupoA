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
    public partial interface ITituloImpressoDAL
    {
        TituloImpresso CarregarPorIsbn13(String Isbn13);
        TituloImpresso CarregarPorProduto(Produto produto);
        TituloImpresso CarregarPorProduto(Int32 produtoId);
        TituloImpresso CarregarPorTitulo(Int32 tituloId);
        void AtualizarISBN(TituloImpresso entidade);
    }
}