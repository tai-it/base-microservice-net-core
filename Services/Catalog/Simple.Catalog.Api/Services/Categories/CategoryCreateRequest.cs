namespace Simple.Catalog.Api.Services.Categories
{
    using MediatR;
    using Simple.Core.Models.Common;
    using System.ComponentModel.DataAnnotations;

    public class CategoryCreateRequest : IRequest<ResponseModel>
    {
        [Required]
        public string Name { get; set; }
    }
}
