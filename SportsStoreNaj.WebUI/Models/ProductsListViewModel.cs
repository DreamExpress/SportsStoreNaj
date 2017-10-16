using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsStoreNaj.Domain.Entities;
namespace SportsStoreNaj.WebUI.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PageInfo { get; set; }

        public string CurrentCategory { get; set; }
    }
}