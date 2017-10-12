using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Moq;
using SportsStoreNaj.Domain.Abstract;
using SportsStoreNaj.Domain.Entities;
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
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product> {
                new Product{ Name="FootBall",Price=25},
                new Product{ Name="Surf Board",Price=179},
                new Product{ Name="Running Shoes",Price=95},
                new Product{ Name="T-Shirt",Price=35.75m}
            });

            _Kernel.Bind<IProductsRepository>().ToConstant(mock.Object);
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