using Ecommerce.Application.Products.Models;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.UI.Models
{
    public class Cart
    {
        private readonly List<CartItem> items = new();

        public void Add(ProductModel product, int quantity)
        {
            CartItem item = items.Where(p => p.Product.Id == product.Id).FirstOrDefault();

            if (item == null)
            {
                items.Add(new CartItem { Product = product, Quantity = quantity });
            }
            else
            {
                item.Quantity += quantity;
            }
        }

        public void Remove(ProductModel product)
        {
            items.RemoveAll(p => p.Product.Id == product.Id);
        }

        public decimal TotalPrice()
        {
            return items.Sum(p => p.Product.Price * p.Quantity);
        }

        public IEnumerable<CartItem> Items => items;

        public void Clear()
        {
            items.Clear();
        }
    }
}
