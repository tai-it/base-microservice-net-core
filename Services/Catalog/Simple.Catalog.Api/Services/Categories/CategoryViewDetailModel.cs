namespace Simple.Catalog.Api.Services.Categories
{
    using Simple.Catalog.Api.Domain.Entities;
    using Simple.Catalog.Api.Services.Products;
    using System.Collections.Generic;
    using System.Linq;

    public class CategoryViewDetailModel : CategoryViewBaseModel
    {
        public CategoryViewDetailModel() : base()
        {

        }

        public CategoryViewDetailModel(Category category) : base(category)
        {
            Products = category.ProductInCategories
                .Where(x => !x.Product.IsDeleted)
                    .Select(x => new ProductViewModel(x.Product)).ToList();
        }

        public List<ProductViewModel> Products { get; set; }
    }
}
