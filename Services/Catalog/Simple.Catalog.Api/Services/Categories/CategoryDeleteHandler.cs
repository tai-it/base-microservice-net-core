﻿namespace Simple.Catalog.Api.Services.Categories
{
    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Simple.Catalog.Api.Domain.Context;
    using Simple.Core.Models.Common;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class CategoryDeleteHandler : IRequestHandler<CategoryDeleteRequest, ResponseModel>
    {
        private readonly AppCatalogDbContext db;
        private readonly IMapper mapper;

        public CategoryDeleteHandler(
            AppCatalogDbContext db,
            IMapper mapper
        )
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseModel> Handle(CategoryDeleteRequest request, CancellationToken cancellationToken)
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

            this.db.Categories.Remove(category);
            await this.db.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Category deleted successfully"
            };
        }
    }
}