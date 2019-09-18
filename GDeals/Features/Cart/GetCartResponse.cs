using System.Collections.Generic;

namespace GDeals.Web.Features.Cart
{
    public class GetCartResponse
    {
        public List<ItemDetails> Items { get; set; } = new List<ItemDetails>();

        public class ItemDetails
        {
            public int ProductId { get; set; }
            public string Name { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
        }
    }
}
