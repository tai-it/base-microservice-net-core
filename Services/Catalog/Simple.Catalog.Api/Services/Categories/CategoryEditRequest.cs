namespace Simple.Catalog.Api.Services.Categories
{
    using MediatR;
    using Newtonsoft.Json;
    using Simple.Catalog.Api.Domain.Context;
    using Simple.Core.Models.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class CategoryEditRequest : IValidatableObject, IRequest<ResponseModel>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name can not be blank")]
        [MinLength(3, ErrorMessage = "Name must be greater than 3 characters")]
        [MaxLength(100, ErrorMessage = "Name must be less than 100 characters")]
        public string Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var db = (AppCatalogDbContext)validationContext.GetService(typeof(AppCatalogDbContext));

            var category = db.Categories.FirstOrDefault(x => x.Name == this.Name);

            if (category != null)
            {
                yield return new ValidationResult("Category name '" + category.Name + "' was existed", new string[] { "Name" });
            }
        }
    }
}
