using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.DataAccess;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;
using GrupoA.DataAccess.ADO;
using GrupoA.BusinessObject.Enumerator;
using System.Collections;

namespace GrupoA.BusinessLogicalLayer
{
    public class ArquivoBLL : BaseBLL
    {
        #region Declarações DAL

        private IArquivoDAL _arquivoDal;
        private IArquivoDAL ArquivoDal
        {
            get { return _arquivoDal ?? (_arquivoDal = new ArquivoADO()); }
        }

        #endregion

        #region Métodos: Arquivo

        /// <summary>
        /// Método que carrega um Arquivo.
        /// </summary>
        /// <param name="arquivo">Objeto Arquivo com seu identificador configurado.</param>
        /// <returns></returns>
        public virtual Arquivo CarregarArquivo(Arquivo arquivo)
        {
            return ArquivoDal.Carregar(arquivo);
        }

        /// <summary>
        /// Método que carrega uma lista de Arquivos por produto.
        /// </summary>
        /// <param name="arquivo">Objeto Arquivo com seu identificador configurado.</param>
        /// <returns></returns>
        public virtual List<Arquivo> CarregarArquivo(Produto entidade)
        {
            return ArquivoDal.Carregar(entidade);
        }

        /// <summary>
        /// Método que persiste um novo Arquivo.
        /// </summary>
        /// <param name="arquivo">Objeto Arquivo com seus dados configurados.</param>
        public virtual void InserirNovoArquivo(Arquivo arquivo)
        {
            ArquivoDal.Inserir(arquivo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arquivo"></param>
        public virtual void Atualizar(Arquivo arquivo)
        {
            ArquivoDal.Atualizar(arquivo);
        }

        /// <summary>
        /// Método que exclui um arquivo.
        /// </summary>
        /// <param name="arquivo">Objeto Arquivo com seu identificador configurado.</param>
        public virtual void ExcluirArquivo(Arquivo arquivo)
        {
            ArquivoDal.Excluir(arquivo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public Arquivo CarregarArquivoPorNome(Arquivo entidade)
        {
            return ArquivoDal.CarregarArquivoPorNome(entidade);
        }

        #endregion
    }
}
