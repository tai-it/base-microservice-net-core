namespace Simple.Catalog.Api.Services.Products
{
    using MediatR;
    using Simple.Core.Models.Common;
    using System;

    public class ProductGetByIdRequest : IRequest<ResponseModel>
    {
        public Guid Id { get; set; }
    }
}
