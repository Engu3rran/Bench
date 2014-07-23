using System;
using Ninject;
using Ninject.Modules;

namespace Bench
{
    public class FabriqueGenerique
    {
        private static IKernel _configuration;

        public static T constuire<T>()
        {
            try
            {
                return _configuration.Get<T>();
            }
            catch(Exception e)
            {
                throw new FabriqueException(e);
            }
        }

        public static void configurer(NinjectModule module)
        {
            _configuration = new StandardKernel(module);
        }
    }
}
