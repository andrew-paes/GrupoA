using System;
using System.Collections.Generic;

namespace Ag2.Manager.DAL
{
    public interface ISubformADO
    {
        System.Data.DataSet CarregaDados(Ag2.Manager.Entity.QueryCommand entity);
    }
}
