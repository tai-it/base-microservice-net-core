namespace Simple.Catalog.Api.Services.Categories
{
    using AutoMapper;
    using MediatR;
    using Simple.Catalog.Api.Domain.Context;
    using Simple.Catalog.Api.Domain.Entities;
    using Simple.Core.Models.Common;
    using Simple.EventBus.Abstractions;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class CategoryCreateHandler : IRequestHandler<CategoryCreateRequest, ResponseModel>
    {
        private readonly AppCatalogDbContext db;
        private readonly IMapper mapper;
        //private readonly IEventBus eventBus;

        public CategoryCreateHandler(
            AppCatalogDbContext db,
            IMapper mapper)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseModel> Handle(CategoryCreateRequest request, CancellationToken cancellationToken)
        {
            var category = this.mapper.Map<Category>(request);

            this.db.Categories.Add(category);

            await this.db.SaveChangesAsync(cancellationToken);

            //var eventPost = new CategoryCreatedEvent(request.Name);

            //this.eventBus.Publish(eventPost);

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "The category was created successfully"
            };
        }
    }
}
