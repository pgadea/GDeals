using System;
using System.Collections.Generic;

namespace GDeals.Web.Domain
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public Guid SessionId { get; set; } 
        public DateTime CreatedOn { get; set; }
        public List<CartLineItem> Items { get; set; } = new List<CartLineItem>();
    }

    public class CartLineItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
