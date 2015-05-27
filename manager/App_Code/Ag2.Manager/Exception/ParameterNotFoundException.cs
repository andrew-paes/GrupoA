using System;
using System.Collections.Generic;
using System.Text;

namespace Ag2.Manager.Exception
{
    public class ParameterNotFoundException : System.Exception
    {
        public ParameterNotFoundException(string parameter, string moduleName)
            : base(string.Format("O parâmetro '{0}' utilizado na consulta sql do arquivo xml do módulo '{1}' é inválido.", parameter, moduleName))
        {

        }
    }
}
