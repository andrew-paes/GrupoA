/*
'===============================================================================
'
'  Template: Gerador Código C#.csgen
'  Script versão: 0.96
'  Script criado por: Leonardo Alves Lindermann (lindermannla@ag2.com.br)
'  Gerado pelo MyGeneration versão # (???)
'
'===============================================================================
*/
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Text;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace GrupoA.BusinessObject
{

    [Serializable]
    public partial class CategoriaVH
    {
        private int _categoriaId;
        private string _nomeCategoria;
        private string _codigoCategoria;
        private Categoria _categoriaPai;
        private List<Conteudo> _conteudos;
        private List<Produto> _produtos;
        private List<Promocao> _promocoes;
        private List<Usuario> _usuarios;
        private List<CategoriaVH> _categoriaFilha;
        private int _nivel;

        public int CategoriaId
        {
            get { return _categoriaId; }
            set { _categoriaId = value; }
        }

        [NotNullValidator]
        [StringLengthValidator(0, 1000)]
        public string NomeCategoria
        {
            get { return _nomeCategoria; }
            set { _nomeCategoria = value; }
        }

        public string CodigoCategoria
        {
            get { return _codigoCategoria; }
            set { _codigoCategoria = value; }
        }

        public Categoria CategoriaPai
        {
            get { return _categoriaPai; }
            set { _categoriaPai = value; }
        }

        public List<Conteudo> Conteudos
        {
            get { return _conteudos; }
            set { _conteudos = value; }
        }

        public List<Produto> Produtos
        {
            get { return _produtos; }
            set { _produtos = value; }
        }

        public List<Promocao> Promocoes
        {
            get { return _promocoes; }
            set { _promocoes = value; }
        }

        public List<Usuario> Usuarios
        {
            get { return _usuarios; }
            set { _usuarios = value; }
        }

        public List<CategoriaVH> CategoriaFilha
        {
            get { return _categoriaFilha; }
            set { _categoriaFilha = value; }
        }

        public int Nivel
        {
            get { return _nivel; }
            set { _nivel = value; }
        }

        /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<CategoriaVH>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<CategoriaVH>(this);
        }
    }

    //public struct CategoriaColunas
    //{
    //    public static string CategoriaId = @"categoriaId";
    //    public static string NomeCategoria = @"nomeCategoria";
    //    public static string CategoriaIdPai = @"categoriaIdPai";
    //    public static string CodigoCategoria = @"codigoCategoria";
    //}
}
