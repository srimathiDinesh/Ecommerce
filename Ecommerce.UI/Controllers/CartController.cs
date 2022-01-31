using Ecommerce.Application.Products.Queries;
using Ecommerce.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Ecommerce.UI.Controllers
{
    public class CartController : BaseController
    {
        public ActionResult Index()
        {
            var cart = SessionHelper.Get<Cart>(HttpContext.Session, "cart") ?? new Cart();
            return View(cart);
        }

        public async Task<ActionResult> AddToCart(Guid productId)
        {
            var cart = SessionHelper.Get<Cart>(HttpContext.Session, "cart") ?? new Cart();
            var product = await Mediator.Send(new GetProductByIdQuery(productId));

            if (product != null)
            {
                cart.Add(product, 1);
            }
            SessionHelper.Set<Cart>(HttpContext.Session, "cart", cart);

            return RedirectToAction("Index", cart);
        }

        public async Task<ActionResult> RemoveFromCart(Guid productId)
        {
            var cart = SessionHelper.Get<Cart>(HttpContext.Session, "cart") ?? new Cart();
            var product = await Mediator.Send(new GetProductByIdQuery(productId));

            if (product != null)
            {
                cart.Remove(product);
            }
            SessionHelper.Set<Cart>(HttpContext.Session, "cart", cart);

            return RedirectToAction("Index", cart);
        }
    }
}
