using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStoreNaj.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using SportsStoreNaj.Domain.Abstract;
using SportsStoreNaj.Domain.Entities;
using System.Web.Mvc;

namespace SportsStoreNaj.WebUI.Controllers.Tests
{
    [TestClass()]
    public class AdminControllerTests
    {

        private Mock<IProductRepository> MockData()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product{ ProductId=1,Name="P1"},
                new Product{ ProductId=2,Name="P2"},
                new Product{ ProductId=3,Name="P3"},
                new Product{ ProductId=4,Name="P4"},
                new Product{ ProductId=5,Name="P5"},
            });

            return mock;
        }

        [TestMethod()]
        public void Index_Contains_All_Products()
        {
            Mock<IProductRepository> mock = MockData();

            AdminController target = new AdminController(mock.Object);

            Product[] result = ((IEnumerable<Product>)target.Index().ViewData.Model).ToArray();
            Product[] result1 = ((IEnumerable<Product>)target.Index().Model).ToArray();

            Assert.AreEqual(result.Length, result1.Length);
            Assert.AreEqual(result[0], result1[0]);

            Assert.AreEqual(result[1].Name, "P2");
            Assert.AreEqual(result1[1].Name, "P2");
        }

        [TestMethod()]
        public void Can_Edit_Product()
        {
            Mock<IProductRepository> mock = MockData();
            AdminController target = new AdminController(mock.Object);

            Product p1 = target.Edit(1).Model as Product;
            Product p5 = target.Edit(5).Model as Product;

            Assert.AreEqual(1, p1.ProductId);
            Assert.AreEqual(5, p5.ProductId);
        }


        [TestMethod()]
        public void Cannot_Edit_NoneExist_Product()
        {
            Mock<IProductRepository> mock = MockData();
            AdminController target = new AdminController(mock.Object);

            Product p = target.Edit(6).Model as Product;
            
            Assert.AreEqual(null, p);

            Assert.IsNull(p, "不存在的物品应该是Null");
        }

        [TestMethod()]
        public void Can_Save_valid_Changes() {
            Mock<IProductRepository> mock = MockData();
            AdminController target = new AdminController(mock.Object);
			Product p=mock.Object.Products.ToArray()[0];
			
			target.ModelState.AddModelError("Error","Error");
			
			ActionResult result=target.Edit(p);
			
			mock.Verify(m=>m.SaveProduct(It.IsAny<Product>()),Times.Never());
            Assert.IsInstanceOfType(result, typeof(ViewResult));



        }

    }
}