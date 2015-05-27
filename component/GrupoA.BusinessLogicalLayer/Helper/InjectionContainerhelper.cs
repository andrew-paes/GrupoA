using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace GrupoA.BusinessLogicalLayer.Helper
{
    /// <summary>
    /// Abstracts the access to dependency injection container.
    /// </summary>
    public static class InjectionContainerHelper
    {
        private static readonly IUnityContainer Container;

        /// <summary>
        /// Static contructor.
        /// </summary>
        static InjectionContainerHelper()
        {
            if (Container == null)
            {
                Container = new UnityContainer();
                Container.LoadConfiguration();
            }
        }

        /// <summary>
        /// Get the dependency injection container.
        /// </summary>
        /// <returns>A singleton instance of dependency injection container</returns>
        public static IUnityContainer GetContainer()
        {
            return Container;
        }
    }
}
