using GrupoA.BusinessLogicalLayer.Cache;
using GrupoA.BusinessLogicalLayer.Helper;
using Microsoft.Practices.Unity;

namespace GrupoA.BusinessLogicalLayer
{
    public class BaseBLL
    {
        private ICacheStorage _cache;

        protected ICacheStorage Cache
        {
            get { return _cache ?? (_cache = InjectionContainerHelper.GetContainer().Resolve<ICacheStorage>()); }
        }
    }
}