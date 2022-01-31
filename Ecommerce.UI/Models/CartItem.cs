using Ecommerce.Application.Products.Models;

namespace Ecommerce.UI.Models
{
    public class CartItem
    {
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }
    }
}
