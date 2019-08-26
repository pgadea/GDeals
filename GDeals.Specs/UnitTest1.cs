using System;
using GDeals.Web.Domain;
using GDeals.Web.Features.Cart;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GDeals.Specs
{
    public class AddItemToCartShould
    {
        private readonly DbContextOptions<StoreContext> options;

        public AddItemToCartShould()
        {
            options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: "Cart")
                .Options;
        }
        [Fact]
        public void CreateCartIfNotFound()
        {
            using (var context = new StoreContext(options))
            {
                var cartService = new CartService(context);
                var result = cartService.AddItem(new AddItemCommand { ProductId = 1, SessionId = null });
                Assert.NotEqual(Guid.Empty, result.SessionId);
            }
        }

        [Fact]
        public void NotCreateCartIfExistingOneFound()
        {
            Guid sessionId = Guid.NewGuid();

            using (var context = new StoreContext(options))
            {
                context.ShoppingCart.Add(new ShoppingCart { SessionId = sessionId });
                context.SaveChanges();
            }

            using (var context = new StoreContext(options))
            {
                var cartService = new CartService(context);
                var result = cartService.AddItem(new AddItemCommand { ProductId = 1, SessionId = sessionId });
                Assert.Equal(sessionId, result.SessionId);
            }
        }
    }
}
