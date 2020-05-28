namespace Simple.Catalog.Api.Services.Products
{
    using AutoMapper;
    using MediatR;
    using Simple.Catalog.Api.Domain.Context;
    using Simple.Catalog.Api.Domain.Entities;
    using Simple.Core.Models.Common;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class ProductCreateHandler : IRequestHandler<ProductCreateRequest, ResponseModel>
    {
        private readonly AppCatalogDbContext db;
        private readonly IMapper mapper;

        public ProductCreateHandler(AppCatalogDbContext db, IMapper mapper)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseModel> Handle(ProductCreateRequest request, CancellationToken cancellationToken)
        {
            var product = this.mapper.Map<Product>(request);

            product.ProductInCategories.Add(new ProductInCategory() {
                    ProductId = product.Id,
                    CategoryId = request.CategoryId
            });

            this.db.Products.Add(product);

            await this.db.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = new ProductViewModel(product)
            };
        }
    }
}
