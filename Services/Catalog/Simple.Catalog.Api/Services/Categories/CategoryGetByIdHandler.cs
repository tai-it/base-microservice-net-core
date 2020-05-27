namespace Simple.Catalog.Api.Services.Categories
{
    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Simple.Catalog.Api.Domain.Context;
    using Simple.Core.Models.Common;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class CategoryGetByIdHandler : IRequestHandler<CategoryGetByIdRequest, ResponseModel>
    {
        private readonly AppCatalogDbContext db;
        private readonly IMapper mapper;

        public CategoryGetByIdHandler(
            AppCatalogDbContext db,
            IMapper mapper
        )
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseModel> Handle(CategoryGetByIdRequest request, CancellationToken cancellationToken)
        {
            var category = await this.db.Categories.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (category == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Category not found"
                };
            }

            return new ResponseModel()
            { 
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = new CategoryViewModel(category)
            };
        }
    }
}
