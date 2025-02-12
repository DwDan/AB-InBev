using Ambev.DeveloperEvaluation.Application.Users.Common;
using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Common;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class UserHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid User entities.
    /// The generated users will have valid:
    /// - Username (using internet usernames)
    /// - Password (meeting complexity requirements)
    /// - Email (valid format)
    /// - Phone (Brazilian format)
    /// - Status (Active or Suspended)
    /// - Role (Customer or Admin)
    /// </summary>
    private static readonly Faker<User> userHandlerFaker = new Faker<User>()
        .RuleFor(u => u.Id, f => f.Random.Number(1, 99))
        .RuleFor(u => u.Username, f => f.Internet.UserName())
        .RuleFor(u => u.Password, f => $"Test@{f.Random.Number(100, 999)}")
        .RuleFor(u => u.Email, f => f.Internet.Email())
        .RuleFor(u => u.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}")
        .RuleFor(u => u.Status, f => f.PickRandom(UserStatus.Active, UserStatus.Suspended))
        .RuleFor(u => u.Role, f => f.PickRandom(UserRole.Customer, UserRole.Admin))
        .RuleFor(u => u.Name, f => new Name
        {
            Firstname = f.Name.FirstName(),
            Lastname = f.Name.LastName()
        })
        .RuleFor(u => u.Address, f => new Address
        {
            Street = f.Address.StreetName(),
            City = f.Address.City(),
            Zipcode = f.Address.ZipCode(),
            Number = int.Parse(f.Address.BuildingNumber()),
            Geolocation = new Geolocation
            {
                Latitude = f.Address.Latitude().ToString(),
                Longitude = f.Address.Longitude().ToString(),
            }
        });

    /// <summary>
    /// Generates a valid User entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid User entity with randomly generated data.</returns>
    public static User GenerateValidEntity()
    {
        return userHandlerFaker.Generate();
    }

    public static UserApplication GenerateValidEntity(User user)
    {
        return new UserApplication
        {
            Id = int.MaxValue,
            Username = user.Username,
            Password = user.Password,
            Email = user.Email,
            Phone = user.Phone,
            Status = user.Status,
            Role = user.Role,
            Name = new NameApplication
            {
                Firstname = user.Name.Firstname,
                Lastname = user.Name.Lastname,
            },
            Address = new AddressApplication
            {
                Street = user.Address.Street,
                City = user.Address.City,
                Zipcode = user.Address.Zipcode,
                Number = user.Address.Number,
                Geolocation = new GeolocationApplication
                {
                    Latitude = user.Address.Geolocation.Latitude,
                    Longitude = user.Address.Geolocation.Longitude,
                }
            }
        };
    }
}