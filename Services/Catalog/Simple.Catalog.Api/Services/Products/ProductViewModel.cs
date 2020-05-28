using Simple.Catalog.Api.Domain.Entities;
using System;

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
                Id = product.Id;
                Name = product.Name;
                Price = product.Price;
                Thumbnail = product.Thumbnail;
            }
        }

        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Thumbnail { get; set; }
    }
}
