using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;
using System.Web.Mvc;
using SportsStore.WebUI.Infrastructure.Abstract;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class ControllersTests
    {
        [TestMethod]
        public void Can_Paginate()
        {
            // Arrange
            // - Create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[] {
                    new Product { ProductID = 1, Name = "P1" },
                    new Product { ProductID = 2, Name = "P2" },
                    new Product { ProductID = 3, Name = "P3" },
                    new Product { ProductID = 4, Name = "P4" },
                    new Product { ProductID = 5, Name = "P5" },
                }.AsQueryable());
            //  create a controller and make the page size = 3 items
            ProductController controller = new ProductController(mock.Object);
            controller._pageSize = 3;

            // Action
            ProductsListViewModel result = (ProductsListViewModel) controller.List(null, 2).Model;

            // Assert
            Product[] productArray = result.Products.ToArray();
            Assert.IsTrue(productArray.Length == 2);
            Assert.AreEqual("P4", productArray[0].Name);
            Assert.AreEqual("P5", productArray[1].Name);
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            // - Create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[] {
                    new Product { ProductID = 1, Name = "P1" },
                    new Product { ProductID = 2, Name = "P2" },
                    new Product { ProductID = 3, Name = "P3" },
                    new Product { ProductID = 4, Name = "P4" },
                    new Product { ProductID = 5, Name = "P5" },
                }.AsQueryable());

            //  create a controller and make the page size = 3 items
            ProductController controller = new ProductController(mock.Object);
            controller._pageSize = 3;

            // Act
            ProductsListViewModel result = (ProductsListViewModel) controller.List(null, 2).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;

            Assert.AreEqual(2, pageInfo.CurrentPage);
            Assert.AreEqual(3, pageInfo.ItemsPerPage);
            Assert.AreEqual(5, pageInfo.TotalItems);
            Assert.AreEqual(2, pageInfo.TotalPages);
        }

        [TestMethod]
        public void Can_Filter_Products()
        {
            // Arrange
            // - Create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[] {
                    new Product { ProductID = 1, Name = "P1", Category = "Cat1" },
                    new Product { ProductID = 2, Name = "P2", Category = "Cat2" },
                    new Product { ProductID = 3, Name = "P3", Category = "Cat1" },
                    new Product { ProductID = 4, Name = "P4", Category = "Cat2" },
                    new Product { ProductID = 5, Name = "P5", Category = "Cat3" },
                }.AsQueryable());

            //  create a controller and make the page size = 3 items
            ProductController controller = new ProductController(mock.Object);
            controller._pageSize = 3;

            // Action
            Product[] result = ((ProductsListViewModel) controller.List("Cat2", 1).Model).Products.ToArray();

            // Assert
            Assert.AreEqual(2, result.Length);
            Assert.IsTrue(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "P4" && result[1].Category == "Cat2");
        }

        [TestMethod]
        public void Can_Create_Categories()
        {
            // Arrange
            // - Create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[] {
                    new Product { ProductID = 1, Name = "P1", Category = "Apples" },
                    new Product { ProductID = 2, Name = "P2", Category = "Apples" },
                    new Product { ProductID = 3, Name = "P3", Category = "Plums" },
                    new Product { ProductID = 4, Name = "P4", Category = "Oranges" },
                }.AsQueryable());

            NavController target = new NavController(mock.Object);

            // Act
            String[] results = ((IEnumerable<String>) target.Menu().Model).ToArray();

            // Assert
            Assert.AreEqual(3, results.Length);
            Assert.AreEqual("Apples", results[0]);
            Assert.AreEqual("Oranges", results[1]);
            Assert.AreEqual("Plums", results[2]);
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            // Arrange
            // - Create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[] {
                    new Product { ProductID = 1, Name = "P1", Category = "Apples" },
                    new Product { ProductID = 4, Name = "P2", Category = "Oranges" },
                }.AsQueryable());

            NavController target = new NavController(mock.Object);

            String categoryToSelect = "Apples";

            // Act
            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            // Assert
            Assert.AreEqual(categoryToSelect, result);
        }

        [TestMethod]
        public void Generate_Category_Specific_Product_Count()
        {
            // Arrange
            // - Create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[] {
                    new Product { ProductID = 1, Name = "P1", Category = "Cat1" },
                    new Product { ProductID = 2, Name = "P2", Category = "Cat2" },
                    new Product { ProductID = 3, Name = "P3", Category = "Cat1" },
                    new Product { ProductID = 4, Name = "P4", Category = "Cat2" },
                    new Product { ProductID = 5, Name = "P5", Category = "Cat3" },
                }.AsQueryable());

            ProductController target = new ProductController(mock.Object);
            target._pageSize = 3;

            // Act
            Int32 res1 = ((ProductsListViewModel) target.List("Cat1").Model).PagingInfo.TotalItems;
            Int32 res2 = ((ProductsListViewModel) target.List("Cat2").Model).PagingInfo.TotalItems;
            Int32 res3 = ((ProductsListViewModel) target.List("Cat3").Model).PagingInfo.TotalItems;
            Int32 resAll = ((ProductsListViewModel) target.List(null).Model).PagingInfo.TotalItems;

            // Assert
            Assert.AreEqual(2, res1);
            Assert.AreEqual(2, res2);
            Assert.AreEqual(1, res3);
            Assert.AreEqual(5, resAll);
        }

        [TestMethod]
        public void Cannot_Checkout_Empty_Cart()
        {
            // Arrange
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            ShippingDetails shippingDetails = new ShippingDetails();
            CartController  target = new CartController(null, mock.Object);

            // Act
            ViewResult result = target.Checkout(cart, shippingDetails);

            // Assert
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(),
                                            It.IsAny<ShippingDetails>()),
                        Times.Never());
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            // Arrange
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);

            ShippingDetails shippingDetails = new ShippingDetails();
            CartController  target = new CartController(null, mock.Object);

            target.ModelState.AddModelError("error", "error");

            // Act
            ViewResult result = target.Checkout(cart, shippingDetails);

            // Assert
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(),
                                            It.IsAny<ShippingDetails>()),
                        Times.Never());
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Can_Checkout_And_Submit_Order()
        {
            // Arrange
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);

            ShippingDetails shippingDetails = new ShippingDetails();
            CartController  target = new CartController(null, mock.Object);

            // Act
            ViewResult result = target.Checkout(cart, shippingDetails);

            // Assert
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(),
                                            It.IsAny<ShippingDetails>()),
                        Times.Once());
            Assert.AreEqual("completed", result.ViewName);
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Index_Contains_All_Products()
        {
            // Arrange
            // - Create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[] {
                    new Product { ProductID = 1, Name = "P1" },
                    new Product { ProductID = 2, Name = "P2" },
                    new Product { ProductID = 3, Name = "P3" },
                }.AsQueryable());

            AdminController target = new AdminController(mock.Object);

            // Act
            Product[] result = ((ProductsListViewModel)target.Index().ViewData.Model).Products.ToArray();

            // Assert
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual("P1", result[0].Name);
            Assert.AreEqual("P2", result[1].Name);
            Assert.AreEqual("P3", result[2].Name);
        }

        [TestMethod]
        public void Can_Edit_Product()
        {
            // Arrange
            // - Create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[] {
                    new Product { ProductID = 1, Name = "P1" },
                    new Product { ProductID = 2, Name = "P2" },
                    new Product { ProductID = 3, Name = "P3" },
                }.AsQueryable());

            AdminController target = new AdminController(mock.Object);

            // Act
            Product p1 = target.Edit(1).ViewData.Model as Product;
            Product p2 = target.Edit(2).ViewData.Model as Product;
            Product p3 = target.Edit(3).ViewData.Model as Product;

            // Assert
            Assert.AreEqual(1, p1.ProductID);
            Assert.AreEqual(2, p2.ProductID);
            Assert.AreEqual(3, p3.ProductID);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Product()
        {
            // Arrange
            // - Create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[] {
                    new Product { ProductID = 1, Name = "P1" },
                    new Product { ProductID = 2, Name = "P2" },
                    new Product { ProductID = 3, Name = "P3" },
                }.AsQueryable());

            AdminController target = new AdminController(mock.Object);

            // Act
            Product result = target.Edit(4).ViewData.Model as Product;
            
            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            // Arrange
            // - Create the mock repository
            Mock<IProductRepository>    mock    = new Mock<IProductRepository>();
            AdminController             target  = new AdminController(mock.Object);
            Product                     product = new Product { Name = "Test" };
            
            // Act
            ActionResult    result = target.Save(product, null);

            // Assert
            mock.Verify(m => m.SaveProduct(product));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            // Arrange
            // - Create the mock repository
            Mock<IProductRepository>    mock    = new Mock<IProductRepository>();
            AdminController             target  = new AdminController(mock.Object);
            Product                     product = new Product { Name = "Test" };
            target.ModelState.AddModelError("error", "error");

            // Act
            ActionResult    result = target.Save(product, null);

            // Assert
            mock.Verify(m => m.SaveProduct(product), Times.Never());
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Delete_Valid_Product()
        {
            // Arrange
            // - Create the mock repository
            Mock<IProductRepository>    mock    = new Mock<IProductRepository>();
            AdminController             target  = new AdminController(mock.Object);
            Product                     product = new Product { ProductID = 2, Name = "Test" };

            mock.Setup(m => m.Products).Returns(new Product[] { 
                new Product { ProductID = 1, Name = "P1" },
                product, 
                new Product { ProductID = 3, Name = "P3" }
            }.AsQueryable());

            // Act
            ActionResult    result = target.Delete(product.ProductID);

            // Assert
            mock.Verify(m => m.DeleteProduct(product));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Delete_Invalid_Products()
        {
            // Arrange
            // - Create the mock repository
            Mock<IProductRepository>    mock    = new Mock<IProductRepository>();
            AdminController             target  = new AdminController(mock.Object);

            mock.Setup(m => m.Products).Returns(new Product[] { 
                new Product { ProductID = 1, Name = "P1" },
                new Product { ProductID = 2, Name = "P2" },
                new Product { ProductID = 3, Name = "P3" }
            }.AsQueryable());

            // Act
            ActionResult    result = target.Delete(100);

            // Assert
            mock.Verify(m => m.DeleteProduct(It.IsAny<Product>()), Times.Never());
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Login_With_Valid_Credentials()
        {
            // Arrange
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "secret")).Returns(true);
            LogOnViewModel model = new LogOnViewModel { 
                UserName = "admin",
                Password = "secret"
            };
            AccountController target = new AccountController(mock.Object);

            // Act
            ActionResult result = target.LogOn(model, "/MyUrl");

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("/MyUrl", ((RedirectResult) result).Url);
        }

        [TestMethod]
        public void Cannot_Login_With_Invalid_Credentials()
        {
            // Arrange
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("badUser", "badPassword")).Returns(false);
            LogOnViewModel model = new LogOnViewModel {
                UserName = "badUser",
                Password = "badPassword"
            };
            AccountController target = new AccountController(mock.Object);

            // Act
            ActionResult result = target.LogOn(model, "/MyUrl");

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult) result).ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Can_Retrieve_Image_Data()
        {
            // Arrange
            Product prod = new Product {
                ProductID = 2,
                Name = "Test",
                ImageData = new Byte[] { },
                ImageMimeType = "image/png"
            };
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] { 
                new Product { ProductID = 1, Name = "P1" },
                prod,
                new Product { ProductID = 3, Name = "P3" },
            }.AsQueryable());
            ProductController target = new ProductController(mock.Object);

            // Act
            ActionResult result = target.GetImage(2);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(prod.ImageMimeType, ((FileResult) result).ContentType);
        }

        [TestMethod]
        public void Cannot_Retrieve_Image_Data_For_Invalid_ID()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] { 
                new Product { ProductID = 1, Name = "P1" },
                new Product { ProductID = 2, Name = "P2" },
            }.AsQueryable());
            ProductController target = new ProductController(mock.Object);

            // Act
            ActionResult result = target.GetImage(100);

            // Assert
            Assert.IsNull(result);
        }
    }
}
