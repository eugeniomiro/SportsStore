using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private  IProductRepository _repository;
        public   Int32 _pageSize = 10;

        public AdminController(IProductRepository repo)
        {
            _repository = repo;
        }

        public ViewResult Index(Int32 page = 1)
        {
            IQueryable<Product> selectedProducts = _repository.Products.OrderBy(p => p.Name)
                                                    .Skip((page - 1) * _pageSize).Take(_pageSize);
            ProductsListViewModel model = new ProductsListViewModel {
                Products = selectedProducts,
                PagingInfo = new PagingInfo {
                    ItemsPerPage = _pageSize,
                    CurrentPage = page,
                    TotalItems = _repository.Products.Count()
                }
            };
            return View(model);
        }

        public ViewResult Edit(int productId)
        {
            Product product = _repository.Products.FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Save(Product product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid) {
                if (image != null) {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new Byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                _repository.SaveProduct(product);
                TempData["message"] = String.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            } else {
                return View(product);
            }
        }

        public ActionResult Create()
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(Int32 productId)
        {
            Product prod = _repository.Products.FirstOrDefault(p => p.ProductID == productId);

            if (prod != null) {
                _repository.DeleteProduct(prod);
                TempData["message"] = string.Format("{0} was deleted", prod.Name);
            }
            return RedirectToAction("Index");
        }
    }
}
