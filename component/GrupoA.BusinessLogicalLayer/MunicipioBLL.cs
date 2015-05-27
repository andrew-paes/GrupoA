using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;

namespace GrupoA.BusinessLogicalLayer
{
    /// <summary>
    /// Classe que abstrai as regras de negócio referentes a usuários.
    /// </summary>
    public class MunicipioBLL : BaseBLL
    {
        private IMunicipioDAL _municipioDAL;
        private IRegiaoDAL _regiaoDAL;

        private IMunicipioDAL MunicipioDAL
        {
            get
            {
                if (_municipioDAL == null)
                    _municipioDAL = new MunicipioADO();
                return _municipioDAL;

            }
        }

        private IRegiaoDAL RegiaoDAL
        {
            get
            {
                if (_regiaoDAL == null)
                    _regiaoDAL = new RegiaoADO();
                return _regiaoDAL;

            }
        }

        public List<Municipio> CarregarTodosMunicipios()
        {
            return MunicipioDAL.CarregarTodos().ToList();
        }

        public List<Municipio> CarregarTodasCidades(int regiaoId)
        {
            return MunicipioDAL.CarregarTodasCidades(regiaoId).OrderBy(ci => ci.NomeMunicipio).ToList();
        }

        public List<Regiao> CarregarTodasRegioes()
        {
            string[] ordem = { "nomeRegiao" };
            string[] sentido = { "Asc" };
            return RegiaoDAL.CarregarTodos(0, 0, ordem, sentido, null).OrderBy(re => re.NomeRegiao).ToList();
        }


        public Municipio CarregarTodos(Municipio entidade)
        {
            entidade = MunicipioDAL.Carregar(entidade);
            if (entidade.Regiao != null && entidade.Regiao.RegiaoId > 0)
            {
                entidade.Regiao = RegiaoDAL.Carregar(entidade.Regiao);
            }
            return entidade;
        }

        public Municipio CarregarPorCodigoIbge(Municipio entidade)
        {
            return MunicipioDAL.CarregarPorCodigoIbge(entidade);
        }

        public Municipio Carregar(Municipio entidade)
        {
            return MunicipioDAL.Carregar(entidade);
        }
    }
}