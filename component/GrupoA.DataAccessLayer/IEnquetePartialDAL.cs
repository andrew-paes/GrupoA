using System;
using System.Collections.Generic;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess
{
    public partial interface IEnqueteDAL
    {	
        void InserirLocalizacao(Enquete enquete, EnquetePagina enquetePagina);
        IEnumerable<EnquetePagina> CarregarLocalizacoesPorEnquete(Enquete enquete);
        void ExcluirLocalizacoesPorEnquete(Enquete enquete);
        void AtualizaStatus(Enquete enquete);
        List<Enquete> CarregarEnquetePorAreas(Int32 enquetePaginaId, Usuario usuario);
        Enquete VotarNaEnquete(Enquete enquete);
	}
}
