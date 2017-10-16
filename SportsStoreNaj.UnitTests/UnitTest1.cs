using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Moq;
using System.Web;
using System.Web.Mvc;
using SportsStoreNaj.Domain.Abstract;
using SportsStoreNaj.Domain.Entities;
using SportsStoreNaj.WebUI.Controllers;
using SportsStoreNaj.WebUI.Models;
using SportsStoreNaj.WebUI.HtmlHelpers;
namespace SportsStoreNaj.UnitTests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void Can_Paginate()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.GetProducts()).Returns(
                new Product[] {
                    new Product { ProductId=1,Name="P1"},
                    new Product { ProductId=2,Name="P2"},
                    new Product { ProductId=3,Name="P3"},
                    new Product { ProductId=4,Name="P4"},
                    new Product { ProductId=5,Name="P5"},
                    new Product { ProductId=6,Name="P6"}
                }
                );

            ProductController controller = new ProductController(mock.Object)
            {
                PageSize = 4
            };

            ProductsListViewModel result = (ProductsListViewModel)(controller.List(null, 2)).Model;
            Product[] productArray = result.Products.ToArray();
            Assert.IsTrue(productArray.Length == 2);
            Assert.AreEqual(productArray[0].Name, "P5");
            Assert.AreEqual(productArray[1].Name, "P6");
        }
        [TestMethod]
        public void Test_PageLinks()
        {
            HtmlHelper myhelper = null;
            var pageInfo = new PagingInfo
            {
                TotalItems = 28,
                CurrentPage = 2,
                ItemsPerPage = 10
            };

            Func<int, string> pageUrlDel = i => "Page" + i;

            var result = myhelper.PageLinks(pageInfo, pageUrlDel);

            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
            + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
            + @"<a class=""btn btn-default"" href=""Page3"">3</a>", result.ToString());
        }

        [TestMethod]
        public void Can_Send_Pagination_view_Model()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.GetProducts()).Returns(
                new Product[] {
                    new Product { ProductId=1,Name="P1"},
                    new Product { ProductId=2,Name="P2"},
                    new Product { ProductId=3,Name="P3"},
                    new Product { ProductId=4,Name="P4"},
                    new Product { ProductId=5,Name="P5"},
                    new Product { ProductId=6,Name="P6"}
                }
                );

            ProductController controller = new ProductController(mock.Object)
            {
                PageSize = 4
            };

            var result = (ProductsListViewModel)controller.List(null, 2).Model;

            var pageinfo = result.PageInfo;

            Assert.AreEqual(pageinfo.CurrentPage, 2);
            Assert.AreEqual(pageinfo.ItemsPerPage, 4);
            Assert.AreEqual(pageinfo.TotalItems, 6);
            Assert.AreEqual(pageinfo.TotalPages, 2);

        }

        [TestMethod]
        public void Can_Filter_Products()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.GetProducts()).Returns(
                new Product[] {
                    new Product { ProductId=1,Name="P1",Category="Cat1"},
                    new Product { ProductId=2,Name="P2",Category="Cat2"},
                    new Product { ProductId=3,Name="P3",Category="Cat1"},
                    new Product { ProductId=4,Name="P4",Category="Cat2"},
                    new Product { ProductId=5,Name="P5",Category="Cat1"},
                    new Product { ProductId=6,Name="P6",Category="Cat3"}
                }
                );

            ProductController controller = new ProductController(mock.Object)
            {
                PageSize = 4
            };

            var result=((ProductsListViewModel)controller.List("Cat2", 1).Model).Products.ToArray();

            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual(result[0].Name,"P2" );
            Assert.IsTrue(result[1].Name == "P4" && result[1].Category == "Cat2");
        }

        [TestMethod]
        public void Can_Create_Categories()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.GetProducts()).Returns(
                new Product[] {
                    new Product{ ProductId=1,Name="P1",Category="Apples"},
                    new Product{ ProductId=1,Name="P2",Category="Apples"},
                    new Product{ ProductId=1,Name="P3",Category="Plums"},
                    new Product{ ProductId=1,Name="P4",Category="Oranges"},
                    new Product{ ProductId=1,Name="P5",Category="Plums"},
                });

            NavController navController = new NavController(mock.Object);

            string[] result= ((IEnumerable<string>)navController.Menu().Model).ToArray();

            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual(result[0], "Apples");
            Assert.AreEqual(result[1], "Oranges");
            Assert.AreEqual(result[2], "Plums");
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.GetProducts()).Returns(new Product[]
            {
                new Product { ProductId=1,Name="P1",Category="Apples"},
                new Product { ProductId=2,Name="P2",Category="Oranges"},
            }
                
                );


            var target = new NavController(mock.Object);

            string cateSelected = "Apples";
            string result = target.Menu(cateSelected).ViewBag.SelectedCategory;

            Assert.AreEqual(cateSelected,result);
        }

        [TestMethod]
        public void Generate_Category_Specific_Product_Count()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.GetProducts()).Returns(new Product[] {
                new Product{ ProductId=1,Name="P1",Category="Cat1"},
                new Product{ ProductId=1,Name="P2",Category="Cat2"},
                new Product{ ProductId=1,Name="P3",Category="Cat1"},
                new Product{ ProductId=1,Name="P4",Category="Cat2"},
                new Product{ ProductId=1,Name="P5",Category="Cat3"},
            });

            var target = new ProductController(mock.Object);

            int res1=((ProductsListViewModel)target.List("Cat1").Model).PageInfo.TotalItems;

            int res2 = ((ProductsListViewModel)target.List("Cat2").Model).PageInfo.TotalItems;

            int res3 = ((ProductsListViewModel)target.List("Cat3").Model).PageInfo.TotalItems;

            int res4 = ((ProductsListViewModel)target.List(null).Model).PageInfo.TotalItems;

            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(res4, 5);
        }
    }
}
