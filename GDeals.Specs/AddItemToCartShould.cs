using System;
using System.Linq;
using GDeals.Web.Domain;
using GDeals.Web.Features.Cart;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace GDeals.Specs
{
    public class AddItemToCartShould
    {
        private DbContextOptions<StoreContext> options;

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

        [Fact]
        public void UpdateCartLineQuantityWhenAddedMoreThanOnce()
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
                cartService.AddItem(new AddItemCommand { ProductId = 1, SessionId = sessionId });
            }

            using (var context = new StoreContext(options))
            {
                var cartService = new CartService(context);
                cartService.AddItem(new AddItemCommand { ProductId = 1, SessionId = sessionId });
            }

            using (var context = new StoreContext(options))
            {
                var cart = context
                    .ShoppingCart
                    .Include(x => x.Items)
                    .SingleOrDefault(x => x.SessionId == sessionId);

                cart.Items.Count(x => x.ProductId == 1).ShouldBe(1);
                cart.Items.SingleOrDefault(x => x.ProductId == 1).Quantity.ShouldBe(2);
            }
        }

    }

}
