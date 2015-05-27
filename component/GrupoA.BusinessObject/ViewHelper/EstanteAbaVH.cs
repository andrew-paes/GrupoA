using System.Collections.Generic;

namespace GrupoA.BusinessObject
{
    public class EstanteAbaVH
    {

        #region AbaAtiva
        private bool abaAtiva;
        public bool AbaAtiva
        {
            get { return abaAtiva; }
            set { abaAtiva = value; }
        }
        #endregion

        #region TituloAbas
        private string tituloAba;
        public string TituloAba
        {
            get { return tituloAba; }
            set { tituloAba = value; }
        }
        #endregion

        #region ConteudoAba
        private List<EstanteTituloVH> conteudoAba;
        public List<EstanteTituloVH> ConteudoAba
        {
            get { return conteudoAba; }
            set { conteudoAba = value; }
        }
        #endregion

    }
}
