using GDeals.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Linq;

namespace GDeals.Web.Features.Checkout
{
    public class CheckoutService
    {
        private readonly StoreContext dbContext;
        private readonly IConfiguration configuration;

        public CheckoutService(StoreContext storeContext, IConfiguration configuration)
        {
            this.dbContext = storeContext;
            this.configuration = configuration;
        }

        public void PlaceOrder(CheckoutCommand command, string userIdentifier)
        {
            var order = Domain.Order.FromCheckout(command, userIdentifier);

            var cart = dbContext
                .ShoppingCart
                .Include(x => x.Items)
                .SingleOrDefault(x => x.SessionId == command.SessionId);

            foreach (var item in cart.Items)
            {
                var product = dbContext.Products.SingleOrDefault(x => x.Id == item.ProductId);
                order.AddItem(product, item.Quantity);
            }

            ChargeCustomer(command.PaymentToken, order.GetTotal(), "Order for " + command.Customer.Email);

            dbContext.Orders.Add(order);

            cart.Empty();

            dbContext.SaveChanges();
        }

        private void ChargeCustomer(string paymentToken, decimal total, string description)
        {
            var requestOptions = new RequestOptions
            {
                ApiKey = configuration.GetValue<string>("Stripe:ApiKey")
            };

            var options = new ChargeCreateOptions
            {
                Amount = Convert.ToInt64(total * 100),
                Currency = "usd",
                Description = description,
                Source = paymentToken
            };

            var service = new ChargeService();
            service.Create(options, requestOptions);
        }
    }
}
