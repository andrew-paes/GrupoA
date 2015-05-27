using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;
using GrupoA.FilterHelper;

namespace GrupoA.BusinessLogicalLayer
{
    /// <summary>
    /// Classe que abstrai as regras de negócio referentes a usuários.
    /// </summary>
    public class TituloInformacaoComentarioEspecialistaBLL : BaseBLL
    {
        private ITituloInformacaoComentarioEspecialistaDAL _TituloInformacaoComentarioEspecialistaDAL;

        private ITituloInformacaoComentarioEspecialistaDAL TituloInformacaoComentarioEspecialistaDAL
        {
            get
            {
                if (_TituloInformacaoComentarioEspecialistaDAL == null)
                    _TituloInformacaoComentarioEspecialistaDAL = new TituloInformacaoComentarioEspecialistaADO();
                return _TituloInformacaoComentarioEspecialistaDAL;
            }
        }

        public TituloInformacaoComentarioEspecialista Carregar(TituloInformacaoComentarioEspecialista entidade)
        {
            return TituloInformacaoComentarioEspecialistaDAL.Carregar(entidade);
        }

        public TituloInformacaoComentarioEspecialista Carregar(Titulo entidade)
        {
            return TituloInformacaoComentarioEspecialistaDAL.Carregar(entidade);
        }

        public void Atualizar(TituloInformacaoComentarioEspecialista entidade)
        {
            TituloInformacaoComentarioEspecialistaDAL.Atualizar(entidade);
        }

        public void Inserir(TituloInformacaoComentarioEspecialista entidade)
        {
            TituloInformacaoComentarioEspecialistaDAL.Inserir(entidade);
        }
    }
}