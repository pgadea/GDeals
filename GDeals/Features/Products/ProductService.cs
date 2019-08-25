using GDeals.Features.Models;
using GDeals.Web.Domain;

namespace GDeals.Web.Features
{
    public class ProductService
    {
        private readonly StoreContext dbContext;

        public ProductService(StoreContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ProductListModel GetProductList()
        {
            var products = dbContext.Products;

            var model = new ProductListModel();
            foreach(var product in products)
            {
                model.Products.Add(new ProductListModel.ProductListItem
                {
                    Id = product.Id,
                    Description = product.Description,
                    Name = product.Name
                });
            }

            return model;
        }

        public ProductDetailsModel GetDetails(int id)
        {
            var product = dbContext.Products.Find(id);

            return new ProductDetailsModel
            {
                Id = id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }
    }
}