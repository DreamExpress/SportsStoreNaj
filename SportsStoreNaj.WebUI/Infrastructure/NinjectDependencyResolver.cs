using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using System.Configuration;
using SportsStoreNaj.Domain.Abstract;
using SportsStoreNaj.Domain.Concrete;
using SportsStoreNaj.Domain.Entities;
using SportsStoreNaj.WebUI.Infrastructure.Abstract;
using SportsStoreNaj.WebUI.Infrastructure.Concrete;
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
            //Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            //mock.Setup(m => m.GetProducts()).Returns(new List<Product> {
            //    new Product{ Name="FootBall",Price=25},
            //    new Product{ Name="Surf Board",Price=179},
            //    new Product{ Name="Running Shoes",Price=95},
            //    new Product{ Name="T-Shirt",Price=35.75m}
            //});

            //_Kernel.Bind<IProductsRepository>().ToConstant(mock.Object);
            _Kernel.Bind<IProductRepository>().To<EFProductRepository>();
            EmailSettings emailSettings = new EmailSettings {
                WriteAsFile=bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"]??"false")
            };

            _Kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("settings", emailSettings);

            _Kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
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