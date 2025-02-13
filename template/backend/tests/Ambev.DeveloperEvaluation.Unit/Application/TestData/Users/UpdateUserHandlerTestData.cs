using Ambev.DeveloperEvaluation.Application.Users.Common;
using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Users;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class UpdateUserHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid User entities.
    /// The generated users will have valid:
    /// - Id (using random numbers)
    /// - Username (using internet usernames)
    /// - Password (meeting complexity requirements)
    /// - Email (valid format)
    /// - Phone (Brazilian format)
    /// - Status (Active or Suspended)
    /// - Role (Customer or Admin)
    /// </summary>
    private static readonly Faker<UpdateUserCommand> updateUserHandlerFaker = new Faker<UpdateUserCommand>()
        .RuleFor(u => u.Id, f => f.Random.Number(1, 99))
        .RuleFor(u => u.Username, f => f.Internet.UserName())
        .RuleFor(u => u.Password, f => $"Test@{f.Random.Number(100, 999)}")
        .RuleFor(u => u.Email, f => f.Internet.Email())
        .RuleFor(u => u.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}")
        .RuleFor(u => u.Status, f => f.PickRandom(UserStatus.Active, UserStatus.Suspended))
        .RuleFor(u => u.Role, f => f.PickRandom(UserRole.Customer, UserRole.Admin))
        .RuleFor(u => u.Name, f => new NameApplication
        {
            Firstname = f.Name.FirstName(),
            Lastname = f.Name.LastName()
        })
        .RuleFor(u => u.Address, f => new AddressApplication
        {
            Street = f.Address.StreetName(),
            City = f.Address.City(),
            Zipcode = f.Address.ZipCode(),
            Number = int.Parse(f.Address.BuildingNumber()),
            Geolocation = new GeolocationApplication
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
    public static UpdateUserCommand GenerateValidCommand()
    {
        return updateUserHandlerFaker.Generate();
    }

    public static User GenerateValidCommand(UpdateUserCommand command)
    {
        return new User
        {
            Id = int.MaxValue,
            Username = command.Username,
            Password = command.Password,
            Email = command.Email,
            Phone = command.Phone,
            Status = command.Status,
            Role = command.Role,
            Name = new Name
            {
                Firstname = command.Name.Firstname,
                Lastname = command.Name.Lastname,
            },
            Address = new Address
            {
                Street = command.Address.Street,
                City = command.Address.City,
                Zipcode = command.Address.Zipcode,
                Number = command.Address.Number,
                Geolocation = new Geolocation
                {
                    Latitude = command.Address.Geolocation.Latitude,
                    Longitude = command.Address.Geolocation.Longitude,
                }
            }
        };
    }
}
