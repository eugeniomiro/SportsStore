using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
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

        public ViewResult List(Int32 page = 1)
        {
            ProductsListViewModel viewModel = new ProductsListViewModel {
                Products = _repository.Products
                                    .OrderBy(p => p.ProductID)
                                    .Skip((page - 1) * _pageSize)
                                    .Take(_pageSize),
                PagingInfo = new PagingInfo {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = _repository.Products.Count()
                }
            };
            return View(viewModel);
        }
    }
}
