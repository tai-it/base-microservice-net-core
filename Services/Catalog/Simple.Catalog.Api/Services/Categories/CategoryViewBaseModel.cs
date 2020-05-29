namespace Simple.Catalog.Api.Services.Categories
{
    using Simple.Catalog.Api.Domain.Entities;
    using System;

    public class CategoryViewBaseModel
    {
        public CategoryViewBaseModel()
        {

        }

        public CategoryViewBaseModel(Category category) : this()
        {
            if (category != null)
            {
                Id = category.Id;
                Name = category.Name;
            }
        }

        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
