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
    public partial class Carrinho
    {
        // Construtor
        public Carrinho() { }

        // Construtor com identificador
        public Carrinho(int carrinhoId)
        {
            _carrinhoId = carrinhoId;
        }

        private int _carrinhoId;
        private DateTime _dataHoraCriacao;
        private List<CarrinhoItem> _carrinhoItens;
        private List<Pedido> _pedidos;
        private CarrinhoStatus _carrinhoStatus;
        private Usuario _usuario;

        public int CarrinhoId
        {
            get { return _carrinhoId; }
            set { _carrinhoId = value; }
        }

        [NotNullValidator]
        public DateTime DataHoraCriacao
        {
            get { return _dataHoraCriacao; }
            set { _dataHoraCriacao = value; }
        }

        public List<CarrinhoItem> CarrinhoItens
        {
            get { return _carrinhoItens; }
            set { _carrinhoItens = value; }
        }

        public List<Pedido> Pedidos
        {
            get { return _pedidos; }
            set { _pedidos = value; }
        }

        [NotNullValidator]
        public CarrinhoStatus CarrinhoStatus
        {
            get { return _carrinhoStatus; }
            set { _carrinhoStatus = value; }
        }

        [NotNullValidator]
        public Usuario Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Carrinho>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Carrinho>(this);
        }
    }

    public struct CarrinhoColunas
    {
        public static string CarrinhoId = @"carrinhoId";
        public static string UsuarioId = @"usuarioId";
        public static string DataHoraCriacao = @"dataHoraCriacao";
        public static string CarrinhoStatusId = @"carrinhoStatusId";
    }
}
