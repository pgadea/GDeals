using System;

namespace GDeals.Web.Features.Cart
{
    public partial class CartController
    {
        public class RemoveItemCommand
        {
            public Guid SessionId { get; set; }
            public int ProductId { get; set; }
        }
    }
}