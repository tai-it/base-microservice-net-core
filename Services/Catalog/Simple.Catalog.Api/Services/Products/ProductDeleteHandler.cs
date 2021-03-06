﻿namespace Simple.Catalog.Api.Services.Products
{
    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Simple.Catalog.Api.Domain.Context;
    using Simple.Core.Models.Common;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class ProductDeleteHandler : IRequestHandler<ProductDeleteRequest, ResponseModel>
    {
        private readonly AppCatalogDbContext db;
        private readonly IMapper mapper;

        public ProductDeleteHandler(AppCatalogDbContext db, IMapper mapper)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseModel> Handle(ProductDeleteRequest request, CancellationToken cancellationToken)
        {
            var product = await this.db.Products.FirstOrDefaultAsync(x => x.Id == request.Id);
            
            if (product == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Product not found"
                };
            }

            product.IsDeleted = true;
            product.DeletedOn = DateTime.Now;

            await this.db.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = new ProductViewModel(product)
            };
        }
    }
}
