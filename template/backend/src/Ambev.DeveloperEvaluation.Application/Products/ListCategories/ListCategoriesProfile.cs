using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.ListCategories;

public class ListCategoriesProfile : Profile
{
    public ListCategoriesProfile()
    {
        CreateMap<List<string>, ListCategoriesResult>()
            .ForMember(dest => dest.Categories, src => src.MapFrom(x => x));
    }
}