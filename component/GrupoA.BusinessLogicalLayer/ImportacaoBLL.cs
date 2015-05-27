using System.Collections.Generic;
using GrupoA.DataAccess;
using GrupoA.BusinessObject;
using GrupoA.DataAccess.ADO;
using GrupoA.FilterHelper;
using System.Transactions;
using System.Text;
using System.IO;

namespace GrupoA.BusinessLogicalLayer
{
    public class ImportacaoBLL : BaseBLL
    {
        #region Declarações DAL

        private IImportacaoDAL _importacaoDAL;
        private IImportacaoDAL ImportacaoDAL
        {
            get
            {
                if (_importacaoDAL == null)
                    _importacaoDAL = new ImportacaoADO();
                return _importacaoDAL;
            }
        }

        #endregion

		/// <summary>
		/// Metodo que chamda a camada de dados para insercao dos dados.
		/// </summary>
		/// <param name="sql">String com o sql a ser executado.</param>
		public void InserirLote(string sql)
		{
			ImportacaoDAL.InserirLote(sql);
		}
    }
}