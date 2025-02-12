using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Common;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the <see cref="GetUserHandler"/> class.
/// </summary>
public class GetUserHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly GetUserHandler _handler;

    public GetUserHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetUserHandler(_userRepository, _mapper);
    }

    [Fact(DisplayName = "Should return user when user exists")]
    public async Task GetUserHandler_ShouldReturnUser_WhenUserExists()
    {
        var command = GetUserHandlerTestData.GenerateValidCommand();
        var user = UserHandlerTestData.GenerateValidEntity();

        var result = new GetUserResult
        {
            Id = user.Id,
        };

        _mapper.Map<User>(command).Returns(user);
        _mapper.Map<GetUserResult>(user).Returns(result);

        _userRepository.GetByIdAsync(command.Id, CancellationToken.None).Returns(user);

        var getUserResult = await _handler.Handle(command, CancellationToken.None);

        getUserResult.Should().NotBeNull();
        getUserResult.Id.Should().Be(user.Id);
        await _userRepository.Received(1).GetByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Should throw KeyNotFoundException when user does not exist")]
    public async Task GetUserHandler_Throw_KeyNotFoundException_WhenUserDoesNotExist()
    {
        var command = GetUserHandlerTestData.GenerateValidCommand();

        _userRepository.GetByIdAsync(command.Id, CancellationToken.None).Returns(default(User));

        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(async () => await _handler.Handle(command, CancellationToken.None));
        Assert.Equal($"User with ID {command.Id} not found", exception.Message);

        await _userRepository.Received(1).GetByIdAsync(command.Id, CancellationToken.None);
    }


    [Fact(DisplayName = "Should throws ValidationException when id was not provide")]
    public async Task GetUserHandler_Throw_ValidationException_WhenUserDoesNotExist()
    {
        var command = new GetUserCommand(0);

        var exception = await Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(command, CancellationToken.None));
        Assert.Equal("Validation failed: \r\n -- Id: User ID is required Severity: Error", exception.Message);
    }
}
