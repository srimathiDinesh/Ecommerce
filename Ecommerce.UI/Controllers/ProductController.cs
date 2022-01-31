using Ecommerce.Application.Products.Commands;
using Ecommerce.Application.Products.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Ecommerce.UI.Controllers
{
    [Authorize]
    public class ProductController : BaseController
    {
        public async Task<ActionResult> Index()
        {
            return View(await Mediator.Send(new GetAllProductsQuery()));
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var product = await Mediator.Send(new GetProductByIdQuery(id));

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateProductCommand command)
        {
            if (ModelState.IsValid)
            {
                await Mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }

            return View(command);
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            var product = await Mediator.Send(new GetProductByIdQuery(id));

            if (product == null)
            {
                return NotFound();
            }

            UpdateProductCommand command = new();
            command.Id = product.Id;
            command.Name = product.Name;
            command.Price = product.Price;
            command.ExistingImage = product.ImagePath;

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateProductCommand command)
        {
            if (ModelState.IsValid)
            {
                await Mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }

            return View(command);
        }

        public async Task<ActionResult> Delete(Guid? id)
        {
            if(id is null)
            {
                return NotFound();
            }

            var product = await Mediator.Send(new GetProductByIdQuery(id.Value));

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteProductCommand(id));
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Search()
        {
            return View(await Mediator.Send(new GetAllProductsQuery()));
        }
    }
}
