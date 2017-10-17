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
    public class CartControllerTests
    {
        [TestMethod()]
        public void Cannot_Checkout_Empty_Cart()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            Cart cart = new Cart();
            CartController target = new CartController(null, mock.Object);

            var details = new ShippingDetails();
            ViewResult result = target.Checkout(cart, details);

            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());

            Assert.AreEqual("",result.ViewName);

            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }
    }
}