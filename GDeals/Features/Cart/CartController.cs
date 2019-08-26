using Microsoft.AspNetCore.Mvc;

namespace GDeals.Web.Features.Cart
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService cartService;

        public CartController(CartService cartService)
        {
            this.cartService = cartService;
        }

        public IActionResult AddItem(AddItemCommand command)
        {
            var result = cartService.AddItem(command);
            return Ok(result);
        }
    }
}