using GDeals.Web.Domain;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GDeals.Web.Features.Cart
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly StoreContext dbContext;

        public CartController(StoreContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult AddItem(AddItemCommand command)
        {
            var cart = new ShoppingCart { SessionId = Guid.NewGuid(), CreatedOn = DateTime.Now };
            cart.Items.Add(new CartLineItem { ProductId = command.ProductId, Quantity = 1 });

            dbContext.ShoppingCart.Add(cart);
            dbContext.SaveChanges();

            return Ok(command.ProductId);
        }
    }

    public class AddItemCommand
    {
        public int ProductId { get; set; }
    }
}