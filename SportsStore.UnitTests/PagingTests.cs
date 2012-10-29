using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.WebUI.Models;
using System.Web.Mvc;
using SportsStore.WebUI.HtmlHelpers;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class PagingTests
    {
        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            // Arrange
            HtmlHelper  myHelper = null;

            PagingInfo pagingInfo = new PagingInfo { 
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };
            Func<Int32, String> pageUrlDelegate = i => "Page" + i;
 
            // Act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Assert
            Assert.AreEqual(@"<a href=""Page1"">1</a><a class=""selected"" href=""Page2"">2</a><a href=""Page3"">3</a>",
                            result.ToString());
        }

    }
}
