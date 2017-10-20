using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStoreNaj.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using SportsStoreNaj.Domain.Abstract;
using SportsStoreNaj.WebUI.Controllers;
using SportsStoreNaj.WebUI.Models;
namespace SportsStoreNaj.UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            Product P1 = new Product { ProductId = 1, Name = "P1" };
            Product P2 = new Product { ProductId = 2, Name = "P2" };
            Product P3 = new Product { ProductId = 3, Name = "P3" };

            Cart cart = new Cart();
            cart.AddItem(P1, 1);
            cart.AddItem(P3, 1);
            var clines = cart.Lines.ToArray();
            Assert.AreEqual(clines.Length, 2);

            Assert.AreEqual(clines[0].Product.Name, "P1");
            Assert.AreEqual(clines[1].Product.Name, "P3");
        }


        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            Product P1 = new Product { ProductId = 1, Name = "P1" };
            Product P2 = new Product { ProductId = 2, Name = "P2" };
            Product P3 = new Product { ProductId = 3, Name = "P3" };

            Cart cart = new Cart();
            cart.AddItem(P1, 7);
            cart.AddItem(P3, 1);
            cart.AddItem(P2, 14);
            cart.AddItem(P1, 10);
            var clines = cart.Lines.OrderBy(c => c.Product.ProductId).ToArray();
            Assert.AreEqual(clines.Length, 3);

            Assert.AreEqual(clines[0].Quantity, 17);
            Assert.AreEqual(clines[1].Quantity, 14);

            Assert.AreEqual(clines[2].Quantity, 1);
        }

        [TestMethod]
        public void Can_Add_Remove_Lines()
        {
            Product P1 = new Product { ProductId = 1, Name = "P1" };
            Product P2 = new Product { ProductId = 2, Name = "P2" };
            Product P3 = new Product { ProductId = 3, Name = "P3" };

            Cart cart = new Cart();
            cart.AddItem(P1, 7);
            cart.AddItem(P3, 3);
            cart.AddItem(P2, 14);
            cart.AddItem(P1, 10);
            cart.RemoveItem(P2);
            var clines = cart.Lines.OrderBy(c => c.Product.ProductId).ToArray();



            Assert.AreEqual(clines.Length, 2);

            Assert.AreEqual(clines[0].Quantity, 17);
            Assert.AreEqual(clines[1].Quantity, 3);

        }

        [TestMethod]
        public void Can_Clear_Lines()
        {
            Product P1 = new Product { ProductId = 1, Name = "P1" };
            Product P2 = new Product { ProductId = 2, Name = "P2" };
            Product P3 = new Product { ProductId = 3, Name = "P3" };

            Cart cart = new Cart();
            cart.AddItem(P1, 7);
            cart.AddItem(P3, 3);
            cart.AddItem(P2, 14);
            cart.AddItem(P1, 10);
            cart.Clear();
            Assert.AreEqual(cart.Lines.Count(), 0);

        }

        [TestMethod]
        public void Can_Compute_Total_Value()
        {
            Product P1 = new Product { ProductId = 1, Name = "P1", Price = 30.25m };
            Product P2 = new Product { ProductId = 2, Name = "P2", Price = 47.37m };
            Product P3 = new Product { ProductId = 3, Name = "P3", Price = 48.00m };

            Cart cart = new Cart();
            cart.AddItem(P1, 1);
            cart.AddItem(P1, 3);
            cart.AddItem(P2, 2);
            cart.AddItem(P3, 11);

            Assert.AreEqual(cart.ComputeValue(), 30.25m * 4 + 47.37m * 2 + 48.00m * 11);

        }

        [TestMethod]
        public void Can_Add_To_Cart()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product{ ProductId=1,Name="P1",Category="Apples"}
            }.AsQueryable()
            );

            Cart cart = new Cart();
            CartController cartController = new CartController(mock.Object,null);

            cartController.AddToCart(cart, 1, null);

            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToArray()[0].Product.ProductId, 1);

        }

        [TestMethod]
        public void Adding_Product_To_Cart_Goes_To_Cart_Screen()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product{ ProductId=1,Name="P1",Category="Apples"}
            }.AsQueryable()
            );

            Cart cart = new Cart();
            CartController cartController = new CartController(mock.Object,null);

            var result=cartController.AddToCart(cart, 2, "myUrl");

            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }


    }
}
