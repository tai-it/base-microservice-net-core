namespace Simple.Catalog.Api
{
    using AutoMapper;
    using Simple.Catalog.Api.Domain.Entities;
    using Simple.Catalog.Api.Services.Categories;
    using Simple.Catalog.Api.Services.Products;

    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<CategoryCreateRequest, Category>();
            CreateMap<ProductCreateRequest, Product>();
            //CreateMap<CategoryEditRequest, Category>()
            //    .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
