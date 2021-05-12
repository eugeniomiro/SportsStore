using System.Linq;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    using Domain.Abstract;
    using Models;

    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;
        public int _pageSize = 4;

        public ProductController(IProductRepository productRepository)
        {
            _repository = productRepository;
        }

        public ViewResult List(string category, int page = 1)
        {
            ViewBag.EmptyStore = _repository.Products.Count() == 0;

            var selectedProducts = _repository.Products.Where(p => category == null || p.Category == category);

            var viewModel = new ProductsListViewModel
            {
                Products = selectedProducts.OrderBy(p => p.ProductID)
                                                .Skip((page - 1) * _pageSize)
                                                .Take(_pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = selectedProducts.Count()
                },
                CurrentCategory = category
            };
            return View(viewModel);
        }

        public FileContentResult GetImage(int productId)
        {
            var prod = _repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}
