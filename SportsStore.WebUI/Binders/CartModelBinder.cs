using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const String sessionKey = "Cart";

        public object BindModel(ControllerContext controllerContext, 
                                ModelBindingContext bindingContext)
        {
            // get the cart from the session
            Cart cart = (Cart) controllerContext.HttpContext.Session[sessionKey];

            // create the cart if ther wasn't one in the session data
            if (cart == null) {
                cart = new Cart();
                controllerContext.HttpContext.Session[sessionKey] = cart;
            }

            return cart;
        }
    }
}