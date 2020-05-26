namespace Simple.Catalog.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Simple.Catalog.Api.Services.Categories;
    using Simple.Core.Models.Common;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    [Route("api/categories")]
    [Consumes("application/json")]
    [Produces("application/json")]
    //[Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator mediator;

        public CategoryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<PagedList<CategoryViewModel>> GetAsync([FromQuery] CategoryPageListRequest request, CancellationToken cancellationToken)
        {
            return await this.mediator.Send(request, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<CategoryViewModel> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var request = new CategoryGetByIdRequest { Id = id };
            return await this.mediator.Send(request, cancellationToken);
        }

        [HttpPost]
        public async Task<ResponseModel> PostAsync([FromBody] CategoryCreateRequest request, CancellationToken cancellationToken)
        {
            return await this.mediator.Send(request, cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task<ResponseModel> PutAsync(Guid id, [FromBody] CategoryEditRequest request, CancellationToken cancellationToken)
        {
            request.Id = id;
            return await this.mediator.Send(request, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task<ResponseModel> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var request = new CategoryDeleteRequest()
            {
                Id = id
            };
            return await this.mediator.Send(request, cancellationToken);
        }
    }
}
