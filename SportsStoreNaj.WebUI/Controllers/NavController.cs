using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStoreNaj.Domain.Abstract;
namespace SportsStoreNaj.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository repository;

        public NavController(IProductRepository repository)
        {
            this.repository = repository;
        }

        public PartialViewResult Menu(string category=null)
        {
            ViewBag.SelectedCategory = category;
            var categories= repository.GetProducts().Select(x => x.Category).Distinct().OrderBy(x => x);
            return PartialView(categories);
        }
    }
}