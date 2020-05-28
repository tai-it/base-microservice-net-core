namespace Simple.Catalog.Api.Services.Categories
{
    using Simple.Catalog.Api.Domain.Entities;
    using System;

    public class CategoryViewModel
    {
        public CategoryViewModel()
        {

        }

        public CategoryViewModel(Category category) : this()
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
