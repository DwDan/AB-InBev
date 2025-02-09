using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

public class ListSalesProfile : Profile
{
    public ListSalesProfile()
    {
        CreateMap<ListSalesCommand, ApiQueryRequestDomain>();

        CreateMap<ApiQueryResponseDomain<Sale>, ListSalesResult>();
    }
}
