using System.Web.Mvc;

namespace SportsStore.WebUI.Binders
{
    using Domain.Entities;

    public class CartModelBinder : IModelBinder
    {
        private const string SessionKey = "Cart";

        public object BindModel(ControllerContext controllerContext,
                                ModelBindingContext bindingContext)
        {
            // get the cart from the session
            var cart = (Cart)controllerContext.HttpContext.Session[SessionKey];

            // create the cart if ther wasn't one in the session data
            if (cart == null)
            {
                cart = new Cart();
                controllerContext.HttpContext.Session[SessionKey] = cart;
            }

            return cart;
        }
    }
}
