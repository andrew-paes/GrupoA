using System;
using System.Data;
using System.Collections.Generic;

namespace Ag2.Manager.DAL
{
    public interface IIdiomaDAL
    {
        /// <summary>
        /// Retorna idioma default do manager
        /// </summary>
        /// <returns></returns>
        Ag2.Manager.BusinessObject.Idioma GetIdiomaDefault();

        /// <summary>
        /// Retorna os idiomas ativos
        /// </summary>
        /// <returns></returns>
        List<Ag2.Manager.BusinessObject.Idioma> GetActiveIdiomas();


        /// <summary>
        /// Carrega idioma
        /// </summary>
        /// <param name="idioma"></param>
        /// <returns></returns>
        Ag2.Manager.BusinessObject.Idioma LoadIdioma(Ag2.Manager.BusinessObject.Idioma idioma);

        /// <summary>
        /// Carrega todos do idiomas da base de dados
        /// </summary>
        /// <returns></returns>
        List<Ag2.Manager.Entity.Idioma> GetIdiomas();

        /// <summary>
        /// Atualiza o idioma correspondente
        /// </summary>
        /// <param name="idioma"></param>
        void IdiomaUpdate(Ag2.Manager.Entity.Idioma idioma);
    }
}
