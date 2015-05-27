using System.Collections.Generic;

namespace GrupoA.BusinessObject
{
    public class EstanteTituloVH
    {
        #region Properties

        public int IdTitulo { get; set; }
        public int ProdutoId { get; set; }
        public bool Disponivel { get; set; }
        public string DisponivelStr { get { return (this.Disponivel ? "1" : "0"); } }
        public string Titulo { get; set; }
        public string SubTitulo { get; set; }
        public double Valor { get; set; }
        public string Arquivo { get; set; }
        public bool eCompraConjunta { get; set; }
        public string CategoriaId { get; set; }
        public List<Autor> Autores { get; set; }
        public string ECompraConjuntaStr { get { return (this.eCompraConjunta ? "1" : "0"); } }
        public string AutoresEmTexto { get { return CarregaListaDeAutoresEmStringUnica(this.Autores); } }

        #endregion

        #region Methods

        public static string CarregaListaDeAutoresEmStringUnica(IList<Autor> autores)
        {
            string retorno = string.Empty;
            if (autores != null && autores.Count > 0)
            {
                foreach (var item in autores)
                {
                    retorno = string.Concat(retorno, "; ", item);
                }
                retorno = (retorno.Length > 2) ? retorno.Substring(2) : string.Empty;
            }
            return retorno;
        }

        #endregion
    }
}
