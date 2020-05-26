namespace Simple.Catalog.Api.Services.Categories
{
    using MediatR;
    using Simple.Core.Models.Common;

    public class CategoryPageListRequest : BaseRequestModel, IRequest<PagedList<CategoryViewModel>>
    {
    }
}
