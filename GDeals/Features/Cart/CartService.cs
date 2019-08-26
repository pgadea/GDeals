using GDeals.Web.Domain;
using System;
using System.Linq;

namespace GDeals.Web.Features.Cart
{
    public class CartService
    {
        private readonly StoreContext dbContext;

        public CartService(StoreContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public AddItemResult AddItem(AddItemCommand command)
        {
            ShoppingCart cart;
            if (command.SessionId.HasValue)
            {
                cart = dbContext.ShoppingCart.SingleOrDefault(x => x.SessionId == command.SessionId);
            }
            else
            {
                Guid newCartSessionId = Guid.NewGuid();
                cart = new ShoppingCart { SessionId = newCartSessionId, CreatedOn = DateTime.Now };
                dbContext.ShoppingCart.Add(cart);
            }

            cart.Items.Add(new CartLineItem { ProductId = command.ProductId, Quantity = 1 });
            dbContext.SaveChanges();

            return new AddItemResult { SessionId = cart.SessionId };
        }
    }

    public class AddItemResult
    {
        public Guid? SessionId { get; set; }
    }
}