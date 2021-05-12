using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Ninject;

namespace SportsStore.WebUI.Infrastructure
{
    using Abstract;
    using Concrete;
    using DataAccess.EntityFramework.Concrete;
    using DataAccess.EntityFramework.Models;
    using Domain.Abstract;
    using Domain.Concrete;

    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _ninjectKernel;

        public NinjectControllerFactory()
        {
            _ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null
                                          : (IController)_ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            _ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();

            var settings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };

            _ninjectKernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("settings", settings);
            _ninjectKernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
            _ninjectKernel.Bind<IUserStore<ApplicationUser>>().To<ApplicationUserStore>()
                                                               .WithConstructorArgument("context", new EfDbContext());
            _ninjectKernel.Bind<UserManager<ApplicationUser>>().To<ApplicationUserManager>()
                                                               .InSingletonScope();

            // put additional bindings here


        }
    }
}
