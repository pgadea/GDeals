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

            cart.Add(command.ProductId, command.Quantity);
            dbContext.SaveChanges();

            return new AddItemResult { SessionId = cart.SessionId };
        }

        public DeleteItemResult Delete(CartController.RemoveItemCommand command)
        {
            var cart = dbContext.ShoppingCart.Include(x => x.Items).SingleOrDefault(x => x.SessionId == command.SessionId);
            var cartItemToRemove = cart.Items.Where(x => x.ProductId == command.ProductId).FirstOrDefault();
            dbContext.Remove(cartItemToRemove);
            dbContext.SaveChanges();

            return new DeleteItemResult { SessionId = cart.SessionId, ProductId = cartItemToRemove.ProductId };
        }

        public GetCartResponse Get(Guid? sessionId)
        {
            if (!sessionId.HasValue)
            {
                return new GetCartResponse();
            }

            var model = new GetCartResponse();
            var cart = dbContext
                .ShoppingCart
                .Include(x => x.Items)
                .SingleOrDefault(x => x.SessionId == sessionId);

            foreach(var item in cart.Items)
            {
                var itemDetails = dbContext.Products.Find(item.ProductId);

                model.Items.Add(new GetCartResponse.ItemDetails
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Name = itemDetails.Name,
                    Price = itemDetails.Price
                });
            }

            return model;
        }
    }

    public class DeleteItemResult
    {
        public Guid SessionId { get; set; }
        public int ProductId { get; set; }
    }

    public class AddItemResult
    {
        public Guid? SessionId { get; set; }
    }
}