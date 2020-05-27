namespace Simple.Catalog.Api.Services.Categories
{
    using MediatR;
    using Simple.Core.Models.Common;
    using System;

    public class CategoryGetByIdRequest : IRequest<ResponseModel>
    {
        public Guid Id { get; set; }
    }
}
