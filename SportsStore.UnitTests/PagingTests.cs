using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SportsStore.UnitTests
{
    using WebUI.HtmlHelpers;
    using WebUI.Models;

    [TestClass]
    public class PagingTests
    {
        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            // Arrange
            HtmlHelper myHelper = null;

            var pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };
            string pageUrlDelegate(int i) => "Page" + i;

            // Act
            var result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Assert
            Assert.AreEqual(@"<a href=""Page1"">1</a><a class=""selected"" href=""Page2"">2</a><a href=""Page3"">3</a>",
                            result.ToString());
        }

    }
}
