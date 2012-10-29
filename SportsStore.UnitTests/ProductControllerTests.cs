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
    public class ProductControllerTests
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
            ProductsListViewModel result = (ProductsListViewModel) controller.List(2).Model;

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
            ProductsListViewModel result = (ProductsListViewModel) controller.List(2).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;

            Assert.AreEqual(2, pageInfo.CurrentPage);
            Assert.AreEqual(3, pageInfo.ItemsPerPage);
            Assert.AreEqual(5, pageInfo.TotalItems);
            Assert.AreEqual(2, pageInfo.TotalPages);
        }
    }

}
