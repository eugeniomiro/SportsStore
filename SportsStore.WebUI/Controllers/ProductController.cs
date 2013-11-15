using System;
using System.Linq;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository  _repository;
        public  Int32               _pageSize = 4;

        public ProductController(IProductRepository productRepository)
        {
            _repository = productRepository;
        }

        public ViewResult List(String category, Int32 page = 1)
        {
            var selectedProducts = _repository.Products.Where(p => category == null || p.Category == category);

            ProductsListViewModel viewModel = new ProductsListViewModel {
                Products = selectedProducts.OrderBy(p => p.ProductID)
                                                .Skip((page - 1) * _pageSize)
                                                .Take(_pageSize),
                PagingInfo = new PagingInfo {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = selectedProducts.Count()
                },
                CurrentCategory = category
            };
            return View(viewModel);
        }

        public FileContentResult GetImage(Int32 productId)
        {
            Product prod = _repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (prod != null) {
                return File(prod.ImageData, prod.ImageMimeType);
            } else {
                return null;
            }
        }
    }
}
