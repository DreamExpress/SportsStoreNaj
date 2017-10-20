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

            Assert.AreEqual(true, result.ViewData.ModelState.ContainsKey("emptyCart"));
        }

        [TestMethod]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);

            CartController target = new CartController(null, mock.Object);

            target.ModelState.AddModelError("error", "error");

            var result = target.Checkout(cart, new ShippingDetails());

            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());

            Assert.AreEqual("",result.ViewName);

            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);

            Assert.AreEqual(true, result.ViewData.ModelState.ContainsKey("error"));
        }

        [TestMethod]
        public void Can_Checkout_And_Submit_Order() {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);

            CartController target = new CartController(null, mock.Object);
            var result = target.Checkout(cart, new ShippingDetails());

            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Once());

            Assert.AreEqual("Completed", result.ViewName);
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);

        }
    }
}