using System;
using System.Text;
using System.Collections.Generic;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;

namespace GrupoA.DataAccess
{
    public partial interface IContatoAssuntoDAL
    {

        IEnumerable<ContatoAssunto> CarregarTodosDoSetor(ContatoSetor setor);
        
    }
}