using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GDeals.Web.Features.Checkout
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly CheckoutService service;

        public CheckoutController(CheckoutService service)
        {
            this.service = service;
        }

        [HttpPost]
        public IActionResult Checkout(CheckoutCommand command)
        {
            var userIdentifier = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            service.PlaceOrder(command, userIdentifier);
            return Ok();
        }
    }
}
