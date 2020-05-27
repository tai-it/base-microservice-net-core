using MediatR;
using Simple.Core.Models.Common;

namespace Simple.Catalog.Api.Services.Products
{
    public class ProductPagedListRequest : BaseRequestModel, IRequest<PagedList<ProductViewModel>>
    {

    }
}
