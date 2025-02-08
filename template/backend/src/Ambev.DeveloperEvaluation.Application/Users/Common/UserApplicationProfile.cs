using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Users.Common
{
    public class UserApplicationProfile : Profile
    {
        public UserApplicationProfile()
        {
            CreateMap<Geolocation, GeolocationApplication>()
            .ReverseMap();

            CreateMap<Address, AddressApplication>()
                .ReverseMap();

            CreateMap<Name, NameApplication>()
                .ReverseMap();

            CreateMap<User, UserApplication>()
               .ReverseMap();
        }
    }
}
