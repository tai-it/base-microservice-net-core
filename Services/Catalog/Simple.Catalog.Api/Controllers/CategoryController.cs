namespace Simple.Catalog.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Simple.Catalog.Api.Infrastructure.Filters;
    using Simple.Catalog.Api.Services.Categories;
    using Simple.Core.Models.Common;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    [Route("api/categories")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ValidateModel]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator mediator;

        public CategoryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<PagedList<CategoryViewModel>> GetAsync([FromQuery] CategoryPagedListRequest request, CancellationToken cancellationToken)
        {
            return await this.mediator.Send(request, cancellationToken);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var request = new CategoryGetByIdRequest { Id = id };
            var responseModel = await this.mediator.Send(request, cancellationToken);
            return new CustomActionResult(responseModel);
        }

        [HttpPost]
        [Authorize(Roles = CommonConstants.Roles.ADMIN)]
        public async Task<IActionResult> PostAsync([FromBody] CategoryCreateRequest request, CancellationToken cancellationToken)
        {
            var responseModel = await this.mediator.Send(request, cancellationToken);
            return new CustomActionResult(responseModel);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = CommonConstants.Roles.ADMIN)]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] CategoryEditRequest request, CancellationToken cancellationToken)
        {
            request.Id = id;
            var responseModel = await this.mediator.Send(request, cancellationToken);
            return new CustomActionResult(responseModel);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = CommonConstants.Roles.ADMIN)]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var request = new CategoryDeleteRequest()
            {
                Id = id
            };
            var responseModel = await this.mediator.Send(request, cancellationToken);
            return new CustomActionResult(responseModel);
        }
    }
}
