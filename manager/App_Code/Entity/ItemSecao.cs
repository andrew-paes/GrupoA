using System;
using System.Collections.Generic;
using System.Web;

namespace Ag2.Manager.Entity
{
    /// <summary>
    /// Summary description for ItemSessao
    /// </summary>
    public class ItemSecao
    {
        private int _secaoId = 0;
        private string _descricao = string.Empty;
        private bool _ativo;
        private List<ItemSecao> _itensSecao;

        public ItemSecao()
        {
            _itensSecao = new List<ItemSecao>();
        }

        public int SecaoId
        {
            get { return _secaoId; }
            set { _secaoId = value; }
        }

        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }

        public bool Ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }

        public List<ItemSecao> ItensSecao
        {
            get { return _itensSecao; }
            set { _itensSecao = value; }
        }
    }
}
