namespace Simple.Catalog.Api.Services.Categories
{
    using MediatR;
    using System;

    public class CategoryGetByIdRequest : IRequest<CategoryViewModel>
    {
        public Guid Id { get; set; }
    }
}
