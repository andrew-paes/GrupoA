using System.Collections.Generic;
using GrupoA.DataAccess;
using GrupoA.BusinessObject;
using GrupoA.DataAccess.ADO;
using GrupoA.FilterHelper;

namespace GrupoA.BusinessLogicalLayer
{
    public class AutorBLL : BaseBLL
    {
        #region Declarações DAL
        
        private IAutorDAL _autorDal;
        private IAutorDAL AutorDal
        {
            get { return _autorDal ?? (_autorDal = new AutorADO()); }
        }

        #endregion

        #region Métodos: Autor

        /// <summary>
        /// Método que carrega um nomeDoAutor.
        /// </summary>
        /// <param name="autor">Objeto Autor com seu identificador configurado.</param>
        /// <returns>Objeto Autor com seus dados configurados.</returns>
        public Autor CarregarAutor(Autor autor)
        {
            return AutorDal.Carregar(autor);
        }

        /// <summary>
        /// Método que persiste um Autor.
        /// </summary>
        /// <param name="autor">Objeto Autor com seus dados configurados.</param>
        /// <returns>Objeto Autor passado no método com o identificador configurado.</returns>
        public Autor InserirNovoAutor(Autor autor)
        {
            AutorDal.Inserir(autor);
            return autor;
        }
        
        /// <summary>
        /// Método que atualiza um Autor.
        /// </summary>
        /// <param name="autor">Objeto Autor com seus dados configurados.</param>
        public void AtualizarAutor(Autor autor)
        {
            AutorDal.Atualizar(autor);
        }

        /// <summary>
        /// Método que exclui um Autor.
        /// </summary>
        /// <param name="autor">Objeto Autor com seus dados configurados.</param>
        public void Excluir(Autor autor)
        {
            AutorDal.Excluir(autor);
        }

        /// <summary>
        /// Método que carrega uma coleção de Autor conforme o nome do do Autor.
        /// </summary>
        /// <param name="nomeDoAutor">string contendo o nome ou parte do nome do autor.</param>
        /// <returns>Coleção de autores.</returns>
        public IEnumerable<Autor> CarregarAutores(string nomeDoAutor)
        {
            var filtro = new AutorFH() { NomeAutor = nomeDoAutor };
            IEnumerable<Autor> autores = AutorDal.CarregarTodos(0, 0, null, null, filtro);
            return autores;
        }

        public Autor CarregarAutorCodigoLegado(Autor entidade)
        {
            return AutorDal.CarregarAutorCodigoLegado(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="autor"></param>
        /// <returns></returns>
        public Autor CarregarAutorComDependencia(Autor autor)
        {
            return AutorDal.CarregarAutorComDependencia(autor);
        }

        /// <summary>
        /// Busca por autores que não estão relacionado ao Titulo
        /// </summary>
        /// <param name="entidade"></param>
        /// <param name="titulo"></param>
        /// <returns></returns>
        public List<Autor> CarregarAutoresPorNome(Autor entidade, Titulo titulo)
        {
            return AutorDal.CarregarAutoresPorNome(entidade, titulo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public Autor CarregarAutorPorNome(Autor entidade)
        {
            return AutorDal.CarregarAutorPorNome(entidade);
        }

        #endregion
    }
}