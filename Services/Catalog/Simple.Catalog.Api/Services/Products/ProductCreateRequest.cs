namespace Simple.Catalog.Api.Services.Products
{
    using MediatR;
    using Simple.Catalog.Api.Domain.Context;
    using Simple.Core.Models.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class ProductCreateRequest : IValidatableObject, IRequest<ResponseModel>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Thumbnail { get; set; }

        public Guid CategoryId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var db = (AppCatalogDbContext)validationContext.GetService(typeof(AppCatalogDbContext));

            var category = db.Categories.FirstOrDefault(x => x.Id == this.CategoryId);

            if (category == null)
            {
                yield return new ValidationResult("CategoryId not found", new string[] { "CategoryId" });
            }

            var product = db.Products.FirstOrDefault(x => x.Name == this.Name);

            if (product != null)
            {
                yield return new ValidationResult("This product name has already existed. You can try to update", new string[] { "Name" });
            }
        }
    }
}
