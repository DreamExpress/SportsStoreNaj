using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStoreNaj.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lines = new List<CartLine>();

        public void AddItem(Product product, int quantity)
        {
            var cartLine = lines.Where(c => c.Product.ProductId == product.ProductId).FirstOrDefault();

            if (cartLine == null)
            {
                lines.Add(new CartLine { Product = product, Quantity = quantity });
            }
            else
            {
                cartLine.Quantity += quantity;
            }
        }

        public void RemoveItem(Product product)
        {
            lines.RemoveAll(l => l.Product.ProductId == product.ProductId);
        }

        public decimal ComputeValue()
        {
            return lines.Sum(e => e.Product.Price * e.Quantity);
        }

        public void Clear()
        {
            lines.Clear();
        }

        public IEnumerable<CartLine> Lines => lines;

    }

    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

    }
}
