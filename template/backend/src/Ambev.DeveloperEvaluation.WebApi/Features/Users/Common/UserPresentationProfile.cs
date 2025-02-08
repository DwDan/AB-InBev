using Ambev.DeveloperEvaluation.Application.Users.Common;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.Common
{
    public class UserPresentationProfile : Profile
    {
        public UserPresentationProfile()
        {
            CreateMap<GeolocationPresentation, GeolocationApplication>()
                .ReverseMap();

            CreateMap<AddressPresentation, AddressApplication>()
                .ReverseMap();

            CreateMap<NamePresentation, NameApplication>()
                .ReverseMap();

            CreateMap<UserPresentation, UserApplication>()
               .ReverseMap();
        }
    }

}
