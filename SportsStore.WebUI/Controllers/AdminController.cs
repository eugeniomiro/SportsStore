using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    using Domain.Abstract;
    using Domain.Entities;
    using Models;

    [Authorize]
    public class AdminController : Controller
    {
        private readonly IProductRepository _repository;
        public int _pageSize = 10;

        public AdminController(IProductRepository repo)
        {
            _repository = repo;
        }

        public ViewResult Index(int page = 1)
        {
            var selectedProducts = _repository.Products.OrderBy(p => p.Name)
                                                    .Skip((page - 1) * _pageSize).Take(_pageSize);
            var model = new ProductsListViewModel
            {
                Products = selectedProducts,
                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = _pageSize,
                    CurrentPage = page,
                    TotalItems = _repository.Products.Count()
                }
            };
            return View(model);
        }

        public ViewResult Edit(int productId)
        {
            var product = _repository.Products.FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Save(Product product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                _repository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public ActionResult Create()
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            var prod = _repository.Products.FirstOrDefault(p => p.ProductID == productId);

            if (prod != null)
            {
                _repository.DeleteProduct(prod);
                TempData["message"] = string.Format("{0} was deleted", prod.Name);
            }
            return RedirectToAction("Index");
        }
    }
}
