using System;
using System.Collections.Generic;
using System.Web;

namespace Ag2.Manager.Entity
{
    /// <summary>
    /// Summary description for OrdenacaoColunas
    /// </summary>
    public class OrdenacaoColunas
    {
        private string _dataFieldName = string.Empty;
        private DirectionOrder _direction;

        public enum DirectionOrder
        {
            ASC,
            DESC
        }

        public OrdenacaoColunas()
        {
            _direction = DirectionOrder.ASC;
        }

        public string DataFieldName
        {
            get { return _dataFieldName; }
            set { _dataFieldName = value; }
        }

        public DirectionOrder Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }
    }
}