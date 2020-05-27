namespace Simple.Catalog.Api.Services.Categories
{
    using MediatR;
    using Simple.Core.Models.Common;

    public class CategoryPagedListRequest : BaseRequestModel, IRequest<PagedList<CategoryViewModel>>
    {
    }
}
