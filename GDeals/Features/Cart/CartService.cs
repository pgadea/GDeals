using GDeals.Web.Domain;
using Microsoft.EntityFrameworkCore;
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
                cart = dbContext.ShoppingCart.Include(x => x.Items).SingleOrDefault(x => x.SessionId == command.SessionId);
            }
            else
            {
                Guid newCartSessionId = Guid.NewGuid();
                cart = new ShoppingCart { SessionId = newCartSessionId, CreatedOn = DateTime.Now };
                dbContext.ShoppingCart.Add(cart);
            }

            cart.Add(command.ProductId);
            dbContext.SaveChanges();

            return new AddItemResult { SessionId = cart.SessionId };
        }
    }

    public class AddItemResult
    {
        public Guid? SessionId { get; set; }
    }
}