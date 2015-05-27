using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Text;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace GrupoA.BusinessObject
{
    public partial class AvisoDisponibilidade
    {
        private Titulo _titulo;

        public Titulo Titulo
        {
            get { return _titulo; }
            set { _titulo = value; }
        }
    }
}
