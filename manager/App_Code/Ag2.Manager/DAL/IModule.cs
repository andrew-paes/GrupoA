using System;
using Ag2.Manager.BusinessObject;

namespace Ag2.Manager.DAL
{
    interface IModule
    {
        ConfigTable ConfigTable { get; set; }
        System.Collections.Generic.List<ConfigWebControl> Fields { get; set; }
        System.Collections.Generic.List<Filter> Filters { get; set; }
        System.Collections.Generic.List<FormsType> Forms { get; set; }
        string ModuleName { get; set; }
        System.Collections.Generic.List<Option> Options { get; set; }
        System.Collections.Generic.List<Query> Queries { get; set; }
        Settings Settings { get; set; }
    }
}
