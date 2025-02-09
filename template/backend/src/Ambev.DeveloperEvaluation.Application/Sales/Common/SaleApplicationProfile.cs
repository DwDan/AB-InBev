using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.Common
{
    public class SaleApplicationProfile : Profile
    {
        public SaleApplicationProfile()
        {
            CreateMap<SaleProductApplication, SaleProduct>()
                .ReverseMap();

            CreateMap<Sale, SaleApplication>()
               .ReverseMap();
        }
    }
}
