using System;
using System.Collections.Generic;
using GrupoA.BusinessObject;
using GrupoA.BusinessObject.ViewHelper;

namespace GrupoA.DataAccess
{
    public partial interface ITituloInformacaoComentarioEspecialistaDAL
    {
        IEnumerable<TituloInformacaoComentarioEspecialista> CarregarAvaliacoesEspecialistas(Usuario usuario, Int32 quantidadeRegistros);
        IEnumerable<ComentarioEspecialistaDestaqueVH> CarregarAvaliacoesEspecialistasDestaque(Usuario usuario, Int32 quantidadeRegistros);
        TituloInformacaoComentarioEspecialista CarregarComentarioEspecialistaPorCategoria(Categoria categoria, Usuario usuario);
        TituloInformacaoComentarioEspecialista CarregarComentarioEspecialistaPorCategoriaParaRevista(Categoria categoria);
        TituloInformacaoComentarioEspecialista Carregar(Titulo entidade);
    }
}
