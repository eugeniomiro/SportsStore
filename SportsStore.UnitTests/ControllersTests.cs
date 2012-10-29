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
    }
}
