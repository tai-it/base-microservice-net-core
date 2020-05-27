namespace Simple.Catalog.Api.Services.Products
{
    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Simple.Catalog.Api.Domain.Context;
    using Simple.Core;
    using Simple.Core.Models.Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class ProductPagedListHandler : IRequestHandler<ProductPagedListRequest, PagedList<ProductViewModel>>
    {
        private readonly AppCatalogDbContext db;
        private readonly IMapper mapper;

        public ProductPagedListHandler(AppCatalogDbContext db, IMapper mapper)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PagedList<ProductViewModel>> Handle(ProductPagedListRequest request, CancellationToken cancellationToken)
        {
            var list = await this.db.Products.Where(
                     x => (string.IsNullOrEmpty(request.Query)) || (x.Name.Contains(request.Query))
                     ).Select(x => new ProductViewModel(x)).ToListAsync();

            var viewModelProperties = this.GetAllPropertyNameOfViewModel();
            var sortPropertyName = !(string.IsNullOrEmpty(request.SortName)) ? request.SortName.ToLower() : string.Empty;
            var matchedPropertyName = viewModelProperties.FirstOrDefault(x => x == sortPropertyName);
            
            if (string.IsNullOrEmpty(matchedPropertyName))
            {
                matchedPropertyName = "Name";
            }

            var viewModelType = typeof(ProductViewModel);
            var sortProperty = viewModelType.GetProperty(matchedPropertyName);

            list = request.IsDesc ? list.OrderByDescending(x => sortProperty.GetValue(x, null)).ToList() : list.OrderBy(x => sortProperty.GetValue(x, null)).ToList();
            return new PagedList<ProductViewModel>(list, request.Offset ?? CommonConstants.Config.DEFAULT_SKIP, request.Limit ?? CommonConstants.Config.DEFAULT_TAKE);
        }

        private List<string> GetAllPropertyNameOfViewModel()
        {
            var productViewModel = new ProductViewModel();
            var type = productViewModel.GetType();

            return ReflectionUtilities.GetAllPropertyNamesOfType(type);
        }
    }
}
