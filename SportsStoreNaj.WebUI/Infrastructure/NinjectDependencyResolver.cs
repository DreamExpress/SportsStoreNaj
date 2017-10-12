using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;

namespace SportsStoreNaj.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel _Kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            _Kernel = kernel;
            AddBindings();
        }

        private void AddBindings()
        {
            //绑定代码
        }

        public object GetService(Type serviceType)
        {
            return _Kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _Kernel.GetAll(serviceType);
        }
    }
}