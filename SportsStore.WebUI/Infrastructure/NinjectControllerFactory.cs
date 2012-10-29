using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using System.Web.Routing;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Concrete;
using System.Configuration;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Infrastructure.Concrete;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        IKernel _ninjectKernel;
        
        public NinjectControllerFactory()
        {
            _ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null
                                          : (IController) _ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            // Mock implementation of the IProductRepository Interface
            //Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product> {
            //    new Product { Name = "Football", Price = 25 },
            //    new Product { Name = "Surf board", Price = 179 },
            //    new Product { Name = "Running shoes", Price = 95 }
            //}.AsQueryable());
            //_ninjectKernel.Bind<IProductRepository>().ToConstant(mock.Object);

            _ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();

            EmailSettings   settings = new EmailSettings { 
                 WriteAsFile = Boolean.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };
            
            _ninjectKernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("settings", settings);
            _ninjectKernel.Bind<IAuthProvider>().To<FormsAuthProvider>();

            // put additional bindings here


        }
    }
}