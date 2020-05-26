namespace Simple.Catalog.Api.Services.Categories
{
    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Simple.Catalog.Api.Domain.Context;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class CategoryGetByIdHandler : IRequestHandler<CategoryGetByIdRequest, CategoryViewModel>
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

        public async Task<CategoryViewModel> Handle(CategoryGetByIdRequest request, CancellationToken cancellationToken)
        {
            var category = await this.db.Categories.FirstOrDefaultAsync(x => x.Id == request.Id);

            return new CategoryViewModel(category);
        }
    }
}
