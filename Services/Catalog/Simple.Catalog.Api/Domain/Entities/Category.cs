namespace Simple.Catalog.Api.Domain.Entities
{
    using Simple.Core.Models.Entity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Category")]
    public class Category : BaseEntity
    {
        public Category() : base()
        {

        }

        public string Name { get; set; }

        public List<ProductInCategory> ProductInCategories { get; set; }
    }
}
