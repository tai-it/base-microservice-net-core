namespace Simple.Catalog.Api.Services.Categories
{
    using Simple.Catalog.Api.Domain.Entities;

    public class CategoryViewModel
    {
        public CategoryViewModel()
        {

        }

        public CategoryViewModel(Category category) : this()
        {
            if (category != null)
            {
                Name = category.Name;
            }
        }

        public string Name { get; set; } = string.Empty;
    }
}
