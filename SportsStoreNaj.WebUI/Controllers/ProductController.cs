using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStoreNaj.Domain.Abstract;
using SportsStoreNaj.Domain.Entities;
using SportsStoreNaj.WebUI.Models;
namespace SportsStoreNaj.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 2;
        public ProductController(IProductRepository productsRepository)
        {
            repository = productsRepository;
        }

        // GET: Product
        public ViewResult List(string category,int page=1)
        {
            ProductsListViewModel model = new ProductsListViewModel {
                Products = repository.GetProducts().Where(p=>category==null||p.Category==category).Skip((page - 1) * PageSize).Take(PageSize),

                PageInfo=new PagingInfo {
                    CurrentPage=page,
                    ItemsPerPage=PageSize,
                    TotalItems= 
                    category==null?
                    repository.GetProducts().Count():
                    repository.GetProducts().Where(p=>p.Category==category).Count()
                },
                CurrentCategory=category

                
            };

            return View(model);
        }
    }
}