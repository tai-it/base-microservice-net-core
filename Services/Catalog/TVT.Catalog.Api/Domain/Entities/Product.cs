namespace TVT.Catalog.Api.Domain.Entities
{
    using TVT.Core.Models.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;

    [Table("Product")]
    public class Product : BaseEntity
    {
        public Product() : base()
        {

        }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Thumbnail { get; set; }

        List<ProductInCategory> ProductInCategories { get; set; }
    }
}
