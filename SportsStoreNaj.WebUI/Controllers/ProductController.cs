using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStoreNaj.Domain.Abstract;
using SportsStoreNaj.Domain.Entities;
namespace SportsStoreNaj.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductsRepository repository;
        public ProductController(IProductsRepository productsRepository)
        {
            repository = productsRepository;
        }

        // GET: Product
        public ActionResult List()
        {
            return View(repository.Products);
        }
    }
}