namespace Simple.Catalog.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Simple.Catalog.Api.Infrastructure.Filters;
    using Simple.Catalog.Api.Services.Products;
    using Simple.Core.Models.Common;
    using System;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var request = new ProductGetByIdRequest()
            {
                Id = id
            };
            var responseModel = await this.mediator.Send(request, cancellationToken);
            return new CustomActionResult(responseModel);
        }

        [HttpPost]
        [Authorize(Roles = CommonConstants.Roles.ADMIN)]
        public async Task<IActionResult> PostAsync([FromBody] ProductCreateRequest request, CancellationToken cancellationToken)
        {
            var responseModel = await this.mediator.Send(request, cancellationToken);
            return new CustomActionResult(responseModel);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = CommonConstants.Roles.ADMIN)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] ProductEditRequest request, CancellationToken cancellationToken)
        {
            request.Id = id;
            var responseModel = await this.mediator.Send(request, cancellationToken);
            return new CustomActionResult(responseModel);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = CommonConstants.Roles.ADMIN)]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var request = new ProductDeleteRequest()
            {
                Id = id
            };
            var responseModel = await this.mediator.Send(request, cancellationToken);
            return new CustomActionResult(responseModel);
        }
    }
}
