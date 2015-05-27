
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
    public partial interface ITituloEletronicoDAL
    {
        TituloEletronico CarregarPorProduto(Int32 produtoId);
        TituloEletronico CarregarPorIsbn13(String Isbn13);
        void AtualizarISBN(TituloEletronico entidade);
        TituloEletronico CarregarPorTitulo(Titulo tituloBO);
    }
}