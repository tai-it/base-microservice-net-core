namespace Simple.Catalog.Api.Services.Categories
{
    using MediatR;
    using Newtonsoft.Json;
    using Simple.Core.Models.Common;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CategoryEditRequest : IRequest<ResponseModel>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
