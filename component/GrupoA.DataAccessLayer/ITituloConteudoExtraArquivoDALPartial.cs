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
    public partial interface ITituloConteudoExtraArquivoDAL
    {
        List<Titulo> CarregarSalaAulaPorCategoria(Usuario usuario);
        TituloConteudoExtraArquivo CarregarComDependencia(TituloConteudoExtraArquivo entidade);
        List<TituloConteudoExtraArquivo> CarregarTodosComDependenciaPorTitulo(Int32 tituloId);
        void AtualizarNomeConteudo(TituloConteudoExtraArquivo entidade);
    }
}