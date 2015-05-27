
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Text;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace GrupoA.BusinessObject
{
    public partial class Usuario
    {
        private string _codigoLegado;
        private Int32? _pontuacaoFidelidade;
        private Int32? _bonusFidelidade;
        private Boolean _assinanteRevistaBmj = false;
        private Boolean _assinanteRevistaPatioEnsFundamental = false;
        private Boolean _assinanteRevistaPatioEnsMedio = false;
        private Boolean _assinanteRevistaPatioPedagogica = false;

        public string CodigoLegado
        {
            get { return _codigoLegado; }
            set { _codigoLegado = value; }
        }

        public Int32? PontuacaoFidelidade
        {
            get { return _pontuacaoFidelidade; }
            set { _pontuacaoFidelidade = value; }
        }

        public Int32? BonusFidelidade
        {
            get { return _bonusFidelidade; }
            set { _bonusFidelidade = value; }
        }

        public Boolean AssinanteRevistaBmj
        {
            get { return _assinanteRevistaBmj; }
            set { _assinanteRevistaBmj = value; }
        }

        public Boolean AssinanteRevistaPatioEnsFundamental
        {
            get { return _assinanteRevistaPatioEnsFundamental; }
            set { _assinanteRevistaPatioEnsFundamental = value; }
        }

        public Boolean AssinanteRevistaPatioEnsMedio
        {
            get { return _assinanteRevistaPatioEnsMedio; }
            set { _assinanteRevistaPatioEnsMedio = value; }
        }

        public Boolean AssinanteRevistaPatioPedagogica
        {
            get { return _assinanteRevistaPatioPedagogica; }
            set { _assinanteRevistaPatioPedagogica = value; }
        }
    }
}