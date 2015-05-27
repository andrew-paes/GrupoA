using System;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess
{
    public partial interface IPromocaoCupomDAL
    {
        void Inserir(Promocao promocao);
        PromocaoCupom CarregarPorPromocao(Promocao entidade);
        PromocaoCupom CarregarPorCodigoCupom(String codigoCupom);
        PromocaoCupom CarregarPorCodigoAmigavel(Int32? promocaoCupomId, String codigoAmigavel);
    }
}
