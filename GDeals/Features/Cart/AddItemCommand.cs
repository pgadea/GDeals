using System;

namespace GDeals.Web.Features.Cart
{
    public class AddItemCommand
    {
        public int ProductId { get; set; }
        public Guid? SessionId { get; set; }
    }
}