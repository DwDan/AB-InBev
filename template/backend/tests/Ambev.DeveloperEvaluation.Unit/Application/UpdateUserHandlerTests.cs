using System.Linq.Expressions;
using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class UpdateUserHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly UpdateUserHandler _handler;

    public UpdateUserHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _mapper = Substitute.For<IMapper>();
        _passwordHasher = Substitute.For<IPasswordHasher>();
        _handler = new UpdateUserHandler(_userRepository, _mapper, _passwordHasher);
    }

    [Fact(DisplayName = "Given valid user data When updating user Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        var command = UpdateUserHandlerTestData.GenerateValidCommand();
        var user = UpdateUserHandlerTestData.GenerateValidCommand(command);

        var result = new UpdateUserResult
        {
            Id = user.Id,
        };

        _mapper.Map<User>(command).Returns(user);
        _mapper.Map<UpdateUserResult>(user).Returns(result);
        _userRepository.UpdateAsync(Arg.Any<User>(), Arg.Any<CancellationToken>()).Returns(user);
        _userRepository.GetByAsync(Arg.Any<Expression<Func<User, bool>>>(), Arg.Any<CancellationToken>()).Returns((User?)null);
        _passwordHasher.HashPassword(Arg.Any<string>()).Returns("hashedPassword");

        var updateUserResult = await _handler.Handle(command, CancellationToken.None);

        updateUserResult.Should().NotBeNull();
        updateUserResult.Id.Should().Be(user.Id);
        await _userRepository.Received(1).UpdateAsync(Arg.Any<User>(), Arg.Any<CancellationToken>());
        _passwordHasher.Received(1).HashPassword(command.Password);
    }

    [Fact(DisplayName = "Given invalid user data When updating user Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        var command = new UpdateUserCommand();

        var act = () => _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    [Fact(DisplayName = "Given duplicate email When updating user Then throws InvalidOperationException")]
    public async Task Handle_DuplicateEmail_ThrowsInvalidOperationException()
    {
        var command = UpdateUserHandlerTestData.GenerateValidCommand();
        var existingUser = new User { Email = command.Email, Id = command.Id + 1 };

        _userRepository.GetByAsync(Arg.Any<Expression<Func<User, bool>>>(), Arg.Any<CancellationToken>()).Returns(existingUser);

        var act = () => _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"User with email {command.Email} already exists");
    }

    [Fact(DisplayName = "Given user update request When handling Then password is hashed")]
    public async Task Handle_ValidRequest_HashesPassword()
    {
        var command = UpdateUserHandlerTestData.GenerateValidCommand();
        const string hashedPassword = "hashedPassword";
        var user = UpdateUserHandlerTestData.GenerateValidCommand(command);

        _mapper.Map<User>(command).Returns(user);
        _userRepository.UpdateAsync(Arg.Any<User>(), Arg.Any<CancellationToken>()).Returns(user);
        _userRepository.GetByAsync(Arg.Any<Expression<Func<User, bool>>>(), Arg.Any<CancellationToken>()).Returns(default(User));
        _passwordHasher.HashPassword(command.Password).Returns(hashedPassword);

        await _handler.Handle(command, CancellationToken.None);

        _passwordHasher.Received(1).HashPassword(command.Password);
        await _userRepository.Received(1).UpdateAsync(
            Arg.Is<User>(u => u.Password == hashedPassword),
            Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given valid command When handling Then maps command to user entity")]
    public async Task Handle_ValidRequest_MapsCommandToUser()
    {
        var command = UpdateUserHandlerTestData.GenerateValidCommand();
        var user = UpdateUserHandlerTestData.GenerateValidCommand(command);

        _mapper.Map<User>(command).Returns(user);
        _userRepository.UpdateAsync(Arg.Any<User>(), Arg.Any<CancellationToken>()).Returns(user);
        _userRepository.GetByAsync(Arg.Any<Expression<Func<User, bool>>>(), Arg.Any<CancellationToken>()).Returns(default(User));
        _passwordHasher.HashPassword(Arg.Any<string>()).Returns("hashedPassword");

        await _handler.Handle(command, CancellationToken.None);

        _mapper.Received(1).Map<User>(Arg.Is<UpdateUserCommand>(c =>
            c.Username == command.Username &&
            c.Email == command.Email &&
            c.Phone == command.Phone &&
            c.Status == command.Status &&
            c.Role == command.Role));
    }
}
