using System;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Collections.Generic;
using Ag2.Manager.BusinessObject;

namespace Ag2.Manager.DAL
{
    public interface ISecaoConteudo
    {

        /// <summary>
        /// Carrega Seções
        /// </summary>
        /// <returns></returns>
        List<Ag2.Manager.Entity.ItemSecao> CarregaSecoes();
        List<Ag2.Manager.Entity.Secao> CarregaSecoes(Manager.Entity.Secao secao);
        Manager.Entity.Secao Salvar(Manager.Entity.Secao secao);
        Manager.Entity.Secao CarregarSecao(Manager.Entity.Secao secao);
        List<Ag2.Manager.Entity.Modelo> CarregaModelos();
        void AlteraOrdem(Int32 secaoId, string direcao);
        void DeletaSessao(Manager.Entity.Secao secao);
        void DesativarAtivarSecao(Manager.Entity.Secao secao);

    }
}
