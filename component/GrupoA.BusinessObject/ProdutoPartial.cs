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
    public partial class Produto
    {
        private string _isbn13Relacionado; // Usado na importação de produto em ProdutoSincronizador
        private string _codigoLegadoCategoria; // Usado na importação de produto em ProdutoSincronizador
        private string _nomeLegadoCategoria; // Usado na importação de produto em ProdutoSincronizador
        private Int32 _parcelas;
        private Decimal _taxaJuros;

        [StringLengthValidator(0, 20)]
        public string Isbn13Relacionado
        {
            get { return _isbn13Relacionado; }
            set { _isbn13Relacionado = value; }
        }

        public string CodigoLegadoCategoria
        {
            get { return _codigoLegadoCategoria; }
            set { _codigoLegadoCategoria = value; }
        }

        public string NomeLegadoCategoria
        {
            get { return _nomeLegadoCategoria; }
            set { _nomeLegadoCategoria = value; }
        }

        public Int32 Parcelas
        {
            get { return _parcelas; }
            set { _parcelas = value; }
        }
        
        public Decimal TaxaJuros
        {
            get { return _taxaJuros; }
            set { _taxaJuros = value; }
        }
    }
}