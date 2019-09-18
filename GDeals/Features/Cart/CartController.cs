using Microsoft.AspNetCore.Mvc;
using System;

namespace GDeals.Web.Features.Cart
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class CartController : ControllerBase
    {
        private readonly CartService cartService;

        public CartController(CartService cartService)
        {
            this.cartService = cartService;
        }

        [HttpPost]
        public IActionResult AddItem(AddItemCommand command)
        {
            var result = cartService.AddItem(command);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult Get(Guid? sessionId)
        {
            var result = cartService.Get(sessionId);
            return Ok(result);
        }

        [HttpDelete("{sessionId}/lines/{productId}")]
        public IActionResult RemoveItem([FromRoute]RemoveItemCommand command)
        {
            var result = cartService.Delete(command);
            return Ok(new { command.SessionId, command.ProductId });
        }
    }
}