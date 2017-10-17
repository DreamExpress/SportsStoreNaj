using SportsStoreNaj.Domain.Abstract;
using SportsStoreNaj.Domain.Entities;
using SportsStoreNaj.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStoreNaj.WebUI.Controllers
{
    public class CartController : Controller
    {

        private IProductRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController(IProductRepository repository,IOrderProcessor orderProcessor)
        {
            this.repository = repository;
            this.orderProcessor = orderProcessor;
        }

        public ViewResult Index(Cart cart,string returnUrl)
        {
            return View(new CartIndexViewModel {
                Cart=cart,
                ReturnUrl=returnUrl

            });
        }


        public RedirectToRouteResult AddToCart(Cart cart,int productId, string returnUrl)
        {
            Product product = repository.GetProducts().FirstOrDefault(p => p.ProductId == productId);

            if (product!=null)
            {
                cart.AddItem(product, 1);
            }

            return RedirectToAction("Index",new { returnUrl } );

        }



        public RedirectToRouteResult RemoveFromCart(Cart cart,int productId, string returnUrl)
        {
            Product product = repository.GetProducts().FirstOrDefault(p => p.ProductId == productId);

            if (product != null)
            {
                cart.RemoveItem(product);
            }

            return RedirectToAction("Index", new { returnUrl });

        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        //通过模型绑定获取Session中的Cart,此方法可以去除
        //private Cart GetCart()
        //{
        //    var cart = (Cart)Session["Cart"];
        //    if (cart==null)
        //    {
        //        cart = new Cart();
        //        Session["Cart"] = cart;
        //    }            
        //    return cart ;
        //}

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count()==0)
            {
                ModelState.AddModelError("", "Sorry,Your Cart is empty!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}