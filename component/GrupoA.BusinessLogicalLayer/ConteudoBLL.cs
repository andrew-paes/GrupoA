using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;
using GrupoA.BusinessObject.Enumerator;
using GrupoA.FilterHelper;
using System.Transactions;

namespace GrupoA.BusinessLogicalLayer
{

    public class ConteudoBLL : BaseBLL
    {

        
        private ICategoriaDAL _categoriaDAL;
        private ICategoriaDAL categoriaDAL
        {
            get
            {
                if (_categoriaDAL == null)
                    _categoriaDAL = new CategoriaADO();
                return _categoriaDAL;
            }
        }   

        
        private IConteudoDAL _conteudoDAL;
        private IConteudoDAL conteudoDAL
        {
            get
            {
                if (_conteudoDAL == null)
                    _conteudoDAL = new ConteudoADO();
                return _conteudoDAL;
            }
        }
       

       
        private IConteudoHitsDAL _conteudoHitsDAL;
        private IConteudoHitsDAL ConteudoHitsDAL
        {
            get
            {
                if (_conteudoHitsDAL == null)
                    _conteudoHitsDAL = new ConteudoHitsADO();
                return _conteudoHitsDAL;
            }
        }

        private IConteudoAvaliacaoDAL _conteudoAvaliacaoDAL;
        private IConteudoAvaliacaoDAL ConteudoAvaliacaoDAL
        {
            get 
            {
                if (_conteudoAvaliacaoDAL == null)
                    _conteudoAvaliacaoDAL = new ConteudoAvaliacaoADO();
                return _conteudoAvaliacaoDAL;
            }
        }

        public ConteudoHits CarregarConteudoHits(ConteudoHits conteudo)
        {
            return ConteudoHitsDAL.CarregarComDependencias(conteudo);
        }

        public ConteudoAvaliacao CarregarConteudoAvaliacao(ConteudoAvaliacao conteudo)
        {
            return ConteudoAvaliacaoDAL.CarregarComDependencias(conteudo);
        }

        public void InserirConteudoHit(ConteudoHits conteudoHits)
        {
            ConteudoHitsDAL.Inserir(conteudoHits);
        }

        public void InserirConteudo(Conteudo conteudo)
        {
            conteudoDAL.Inserir(conteudo);
        }


        public void AtualizarConteudoHits(ConteudoHits conteudoHits)
        {
            ConteudoHitsDAL.Atualizar(conteudoHits); 
        }

        public void AtualizarConteudoAvaliacao(ConteudoAvaliacao conteudoAvaliacao)
        {
            ConteudoAvaliacaoDAL.Atualizar(conteudoAvaliacao);
        }

        public void InserirConteudoAvaliacao(ConteudoAvaliacao conteudoAvaliacao)
        {
            ConteudoAvaliacaoDAL.Inserir(conteudoAvaliacao);
        }

        public Conteudo CarregarConteudo(Conteudo entidade)
        {
            Conteudo conteudo = conteudoDAL.Carregar(entidade);
            conteudo.ConteudoAvaliacao = ConteudoAvaliacaoDAL.CarregarComDependencias(conteudo.ConteudoAvaliacao);
            return conteudo;
        }       

        public List<Categoria> CarregarTodasAreasConhecimentoCategoria(Conteudo entidade)
        {
            return (List<Categoria>)conteudoDAL.CarregarTodasAreasConhecimentoCategoria(entidade);
        }
       
        /// <summary>
        /// Exclui um conteúdo e seus relacionamentos
        /// </summary>
        /// <param name="conteudo"></param>
        public void ExcluirConteudo(Conteudo conteudo)
        { 
                // EXCLUSÃO DO CONTEUDO
                // 1. Excluir Conteudo Área Conhecimento
                categoriaDAL.ExcluirPorConteudo(conteudo);
                // 2. Excluir Conteúdo hits
                ConteudoHitsDAL.Excluir(conteudo);
                // 3. Exculir conteúdo relacionado
                conteudoDAL.ExcluirConteudoRelacionado(conteudo);
                // 4. Excluir Conteudo
                conteudoDAL.Excluir(conteudo);
                //scope.Complete();
        }
       

    }

}
