using GDeals.Features.Models;
using Microsoft.AspNetCore.Mvc;

namespace GDeals.Web.Features
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService productService;

        public ProductController(ProductService productService)
        {
            this.productService = productService;
        }

        public IActionResult List()
        {
            ProductListModel model = productService.GetProductList();
            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            return Ok(productService.GetDetails(id));
        }
    }
}