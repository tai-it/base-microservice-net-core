namespace Simple.Catalog.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Simple.Catalog.Api.Infrastructure.Filters;
    using Simple.Catalog.Api.Services.Products;
    using Simple.Core.Models.Common;
    using System.Threading;
    using System.Threading.Tasks;

    [Route("api/products")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ValidateModel]
    public class ProductController : Controller
    {
        private readonly IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<PagedList<ProductViewModel>> GetAsync([FromQuery] ProductPagedListRequest request, CancellationToken cancellationToken)
        {
            return await this.mediator.Send(request, cancellationToken);
        }
    }
}
