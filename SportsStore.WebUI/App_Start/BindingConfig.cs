using System.Web.Mvc;

namespace SportsStore.WebUI
{
    using Binders;
    using Domain.Entities;
    using Infrastructure;

    public class BindingConfig
    {
        internal static void RegisterBindings()
        {
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }
    }
}
