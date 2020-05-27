using Simple.Catalog.Api.Domain.Entities;

namespace Simple.Catalog.Api.Services.Products
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {

        }

        public ProductViewModel(Product product)
        {
            if (product != null)
            {
                Name = product.Name;
                Price = product.Price;
                Thumbnail = product.Thumbnail;
            }
        }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Thumbnail { get; set; }
    }
}
