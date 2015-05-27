using System;
using System.Text;
using System.Collections.Generic;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;

namespace GrupoA.DataAccess
{
    public partial interface ILogPaymentGatewayDAL
    {
        void Inserir(LogPaymentGateway entidade);
        void Atualizar(LogPaymentGateway entidade);
        void Excluir(LogPaymentGateway entidade);
        LogPaymentGateway Carregar(LogPaymentGateway entidade);
        IEnumerable<LogPaymentGateway> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<LogPaymentGateway> CarregarTodos();
        int TotalRegistros();
        int TotalRegistros(IFilterHelper filtro);
    }
}