﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace GDeals.Web.Domain
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public Guid SessionId { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<CartLineItem> Items { get; set; } = new List<CartLineItem>();

        internal void Add(int productId, int quantity)
        {
            var existing = Items.SingleOrDefault(x => x.ProductId == productId);
            if (existing == null)
            {
                Items.Add(new CartLineItem { ProductId = productId, Quantity = quantity });
            }
            else
            {
                existing.Quantity += quantity;
            }
        }

        internal void Empty()
        {
            Items.Clear();
        }
    }

    public class CartLineItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
