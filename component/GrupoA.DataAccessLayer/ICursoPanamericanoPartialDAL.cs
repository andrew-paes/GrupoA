using System;
using System.Text;
using System.Collections.Generic;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;

namespace GrupoA.DataAccess
{
    public partial interface ICursoPanamericanoDAL
    {
        void InserirLocalizacaoCursoPanamericano(int cursoPanamericanoId, int categoriaId);
        List<Categoria> CarregarCategoriasDoCursoPanamericano(int cursoPanamericanoId);
        void ExcluiCursoPanamericanoCategoria(int cursoPanamericanoId);
        IEnumerable<CursoPanamericano> CarregarCursosPanamericano(Usuario entidade);
        CursoPanamericano CarregarCursoPanamericanoPorInteresseUsuario(Int32? usuarioId);
        List<CursoPanamericano> CarregarCursosPanamericanoParaRevistas(Int32? usuarioId);
	}
}
