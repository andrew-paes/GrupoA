using System.Collections.Generic;
using System;

namespace GrupoA.BusinessObject
{

  	[Serializable]
    public partial class PublicacaoUsuarioVH
	{
        private int _produtoId;
        private string _arquivoNome;
        private string _titulo;
        private string _autores;
        private string _tipoProduto;

        public int ProdutoId
        {
            get { return _produtoId; }
            set { _produtoId = value; }
        }
        
        public string ArquivoNome
        {
            get { return _arquivoNome; }
            set { _arquivoNome = value; }
        }

        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value; }
        }

        public string Autores
        {
            get { return _autores; }
            set { _autores = value; }
        }

        public string TipoProduto
        {
            get { return _tipoProduto; }
            set { _tipoProduto = value; }
        }


        public struct PublicacaoColunas
        {
            public static string DataLancamanto = @"dataLancamanto";
            public static string Titulo = @"titulo";
            public static string Autor = @"autor";
            public static string TipoProduto = @"tipoProduto";
        }


	}
}
