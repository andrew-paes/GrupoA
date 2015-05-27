using System.Collections.Generic;
using GrupoA.DataAccess;
using GrupoA.BusinessObject;
using GrupoA.DataAccess.ADO;
using GrupoA.FilterHelper;
using System.Transactions;

namespace GrupoA.BusinessLogicalLayer
{
    public class ConfiguracaoBLL : BaseBLL
	{
		#region Declarações DAL

		private IConfiguracaoDAL _configuracaoDAL;
		private IConfiguracaoDAL ConfiguracaoDAL
		{
			get
			{
				if (_configuracaoDAL == null)
					_configuracaoDAL = new ConfiguracaoADO();
				return _configuracaoDAL;
			}
		}

		#endregion

		#region Métodos: Configuracao

		public Configuracao Carregar(Configuracao entidade)
		{
			entidade = ConfiguracaoDAL.Carregar(entidade);
			return entidade;
		}

		public Configuracao Inserir(Configuracao entidade)
		{
			ConfiguracaoDAL.Inserir(entidade);
			return entidade;
		}

		public void Atualizar(Configuracao entidade)
		{
			ConfiguracaoDAL.Atualizar(entidade);
		}

		public IEnumerable<Configuracao> CarregarTodos(Configuracao entidade)
		{
			var configuracaoFH = new ConfiguracaoFH() { Chave = entidade.Chave };
			IEnumerable<Configuracao> configuracao = ConfiguracaoDAL.CarregarTodos(0, 0, null, null, configuracaoFH);
			return configuracao;
		}

        public Configuracao CarregarCompleto(Configuracao entidade)
        {
            entidade = ConfiguracaoDAL.CarregarCompleto(entidade);
            return entidade;
        }
		#endregion
	}
}
