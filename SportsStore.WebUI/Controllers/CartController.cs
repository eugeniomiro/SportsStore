using System;
using System.Linq;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository _repository;

        public CartController(IProductRepository repo)
        {
            _repository = repo;
        }

        public RedirectToRouteResult AddToCart(Cart cart, Int32 productId, String returnUrl)
        {
            Product product = _repository.Products
                                            .FirstOrDefault(p => p.ProductID == productId);

            if (product != default(Product)) {
                cart.AddItem(product, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, Int32 productId, String returnUrl)
        {
            Product product = _repository.Products
                                            .FirstOrDefault(p => p.ProductID == productId);

            if (product != default(Product)) {
                cart.RemoveLine(product);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Index(Cart cart, String returnUrl)
        {
            return View(new CartIndexViewModel {
                Cart        = cart,
                ReturnUrl   = returnUrl
            });
        }
    }
}
