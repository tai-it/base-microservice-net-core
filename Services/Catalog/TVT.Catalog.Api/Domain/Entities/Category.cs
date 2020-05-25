namespace TVT.Catalog.Api.Domain.Entities
{
    using TVT.Core.Models.Entity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Category")]
    public class Category : BaseEntity
    {
        public Category() : base()
        {

        }

        public string Name { get; set; }

        List<ProductInCategory> ProductInCategories { get; set; }
    }
}
