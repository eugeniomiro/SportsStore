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
        private IProductRepository  _repository;
        private IOrderProcessor     _orderProcessor;

        public CartController(IProductRepository repo, IOrderProcessor proc)
        {
            _repository = repo;
            _orderProcessor = proc;
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

        [ChildActionOnly]
        public ViewResult Summary(Cart cart)
        {
            return View(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0) {
                ModelState.AddModelError("", "Sorry, your cart is empty");
            }
            if (ModelState.IsValid) {
                _orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("completed");
            }
            return View(shippingDetails);
        }
    }
}
