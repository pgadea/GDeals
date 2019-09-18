using System.Collections.Generic;
using System.Linq;
using GDeals.Web.Features.Checkout;

namespace GDeals.Web.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerEmail { get; private set; }
        public List<OrderLine> Lines { get; private set; }
        public string UserIdentifier { get; private set; }

        internal static Order FromCheckout(CheckoutCommand command, string userIdentifier)
        {
            return new Order
            {
                UserIdentifier = userIdentifier,
                CustomerEmail = command.Customer.Email,
                Lines = new List<OrderLine>()
            };
        }

        internal void AddItem(Product product, int quantity)
        {
            Lines.Add(new OrderLine
            {
                Description = product.Description,
                Price = product.Price,
                ProductId = product.Id,
                Quantity = quantity
            });
        }

        public decimal GetTotal()
        {
            return Lines.Sum(x => x.Quantity * x.Price);
        }
    }

    public class OrderLine
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
