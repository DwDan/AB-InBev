using Ambev.DeveloperEvaluation.Application.Sales.Common;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Common
{
    public class SalePresentationProfile : Profile
    {
        public SalePresentationProfile()
        {
            CreateMap<SaleProductPresentation, SaleProductApplication>()
                .ReverseMap();

            CreateMap<SalePresentation, SaleApplication>()
                .ReverseMap();
        }
    }
}
