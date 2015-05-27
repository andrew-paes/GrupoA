using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;
using GrupoA.FilterHelper;
using System.Transactions;

namespace GrupoA.BusinessLogicalLayer
{
    /// <summary>
    /// Classe que abstrai as regras de negócio referentes a usuários.
    /// </summary>    
    public class ProgramaAtualizacaoChamadaBLL : BaseBLL
    {

        #region Propriedades

        #region programaAtualizacaoChamadaDAL
        private IProgramaAtualizacaoChamadaDAL _programaAtualizacaoChamadaDAL;
        private IProgramaAtualizacaoChamadaDAL programaAtualizacaoChamadaDAL
        {
            get
            {
                if (_programaAtualizacaoChamadaDAL == null)
                    _programaAtualizacaoChamadaDAL = new ProgramaAtualizacaoChamadaADO();
                return _programaAtualizacaoChamadaDAL;

            }
        }
        #endregion

        #region programaAtualizacaoPaginaDAL
        private IProgramaAtualizacaoPaginaDAL _programaAtualizacaoPaginaDAL;
        private IProgramaAtualizacaoPaginaDAL programaAtualizacaoPaginaDAL
        {
            get
            {
                if (_programaAtualizacaoPaginaDAL == null)
                    _programaAtualizacaoPaginaDAL = new ProgramaAtualizacaoPaginaADO();
                return _programaAtualizacaoPaginaDAL;

            }
        }
        #endregion

        #endregion

        #region Métodos

        public void InserirProgramaAtualizacaoChamada(ProgramaAtualizacaoChamada programaAtualizacaoChamada)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // 1. Inserção do ProgramaAtualizacaoChamada
                programaAtualizacaoChamadaDAL.Inserir(programaAtualizacaoChamada);

                foreach (ProgramaAtualizacaoPagina programaAtualizacaoPagina in programaAtualizacaoChamada.ProgramaAtualizacaoPaginas)
                    programaAtualizacaoChamadaDAL.InserirLocalizacao(programaAtualizacaoChamada, programaAtualizacaoPagina);

                scope.Complete();
            }

        }

        public IEnumerable<ProgramaAtualizacaoPagina> CarregarPaginas()
        {
            return programaAtualizacaoPaginaDAL.CarregarTodos();
        }

        public void AtualizarProgramaAtualizacaoChamada(ProgramaAtualizacaoChamada programaAtualizacaoChamada)
        {

            using (TransactionScope scope = new TransactionScope())
            {
                programaAtualizacaoChamadaDAL.ExcluirLocalizacoesPorProgramaAtualizacao(programaAtualizacaoChamada);
                foreach (ProgramaAtualizacaoPagina programaAtualizacaoPagina in programaAtualizacaoChamada.ProgramaAtualizacaoPaginas)
                    programaAtualizacaoChamadaDAL.InserirLocalizacao(programaAtualizacaoChamada, programaAtualizacaoPagina);
                programaAtualizacaoChamadaDAL.Atualizar(programaAtualizacaoChamada);
                scope.Complete();
            }



        }

        public ProgramaAtualizacaoChamada CarregarCompleto(ProgramaAtualizacaoChamada programaAtualizacaoChamada)
        {
            programaAtualizacaoChamada = programaAtualizacaoChamadaDAL.Carregar(programaAtualizacaoChamada);
            programaAtualizacaoChamada.ProgramaAtualizacaoPaginas = CarregarPaginasProgramaAtualizacao(programaAtualizacaoChamada);
            return programaAtualizacaoChamada;
        }

        public List<ProgramaAtualizacaoChamada> CarregarChamadasRandomicas(int numeroMaximoRegistros)
        {
            
            return programaAtualizacaoChamadaDAL.CarregarChamadasRandomicas(numeroMaximoRegistros);

        }

        public List<ProgramaAtualizacaoPagina> CarregarPaginasProgramaAtualizacao(ProgramaAtualizacaoChamada programaAtualizacaoChamada)
        {
            List<ProgramaAtualizacaoPagina> retorno = new List<ProgramaAtualizacaoPagina>();
            foreach (ProgramaAtualizacaoPagina programaAtualizacaoPagina in programaAtualizacaoChamadaDAL.CarregarLocalizacoesPorProgramaAtualizacao(programaAtualizacaoChamada))
            {
                retorno.Add(programaAtualizacaoPagina);
            }
            return retorno;
        }

        public void ExcluirProgramaAtualizacaoChamada( ProgramaAtualizacaoChamada programaAtualizacaoChamada )
        {
            programaAtualizacaoChamada = this.CarregarCompleto(programaAtualizacaoChamada);
            using (TransactionScope scope = new TransactionScope())
            {
                // Exclusão das Páginas
                //foreach (ProgramaAtualizacaoPagina programaAtualizacaoPagina in programaAtualizacaoChamada.ProgramaAtualizacaoPaginas)
                //{
                //    programaAtualizacaoPaginaDAL.Excluir(programaAtualizacaoPagina);
                //}

                programaAtualizacaoChamadaDAL.ExcluirLocalizacoesPorProgramaAtualizacao(programaAtualizacaoChamada);
                programaAtualizacaoChamadaDAL.Excluir(programaAtualizacaoChamada);
                scope.Complete();
            }
        }

        public ProgramaAtualizacaoChamada CarregarProgramaAtualizacaoChamadaPorPagina(ProgramaAtualizacaoPagina programaAtualizacaoPagina)
        {
            return programaAtualizacaoChamadaDAL.CarregarProgramaAtualizacaoChamadaPorPagina(programaAtualizacaoPagina);
        }

        #endregion

    }
}
