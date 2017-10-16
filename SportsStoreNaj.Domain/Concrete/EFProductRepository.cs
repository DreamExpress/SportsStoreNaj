using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStoreNaj.Domain.Abstract;
using SportsStoreNaj.Domain.Entities;
namespace SportsStoreNaj.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<Product> GetProducts()
        {
            return context.Products;
        }
    }
}
