using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Common;
using Ambev.DeveloperEvaluation.Unit.Domain;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the <see cref="DeleteUserHandler"/> class.
/// </summary>
public class DeleteUserHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly DeleteUserHandler _handler;

    public DeleteUserHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new DeleteUserHandler(_userRepository);
    }

    [Fact(DisplayName = "Should return true when user deleted")]
    public async Task DeleteUserHandler_ShouldReturnUser_WhenUserExists()
    {
        var command = DeleteUserHandlerTestData.GenerateValidCommand();
        var user = UserHandlerTestData.GenerateValidEntity();

        _userRepository.DeleteAsync(command.Id, CancellationToken.None).Returns(true);

        var deleteUserResponse = await _handler.Handle(command, CancellationToken.None);

        deleteUserResponse.Should().NotBeNull();
        deleteUserResponse.Success.Should().Be(true);
        await _userRepository.Received(1).DeleteAsync(command.Id, CancellationToken.None);
    }

    [Fact(DisplayName = "Should throw KeyNotFoundException when user does not exist")]
    public async Task DeleteUserHandler_Throw_KeyNotFoundException_WhenUserDoesNotExist()
    {
        var command = DeleteUserHandlerTestData.GenerateValidCommand();
        var user = UserHandlerTestData.GenerateValidEntity();

        _userRepository.DeleteAsync(command.Id, CancellationToken.None).Returns(false);

        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(async () => await _handler.Handle(command, CancellationToken.None));
        Assert.Equal($"User with ID {command.Id} not found", exception.Message);

        await _userRepository.Received(1).DeleteAsync(command.Id, CancellationToken.None);
    }

    [Fact(DisplayName = "Should throws ValidationException when id was not provide")]
    public async Task DeleteUserHandler_Throw_ValidationException_WhenUserDoesNotExist()
    {
        var command = new DeleteUserCommand(0);
        var user = UserHandlerTestData.GenerateValidEntity();

        var exception = await Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(command, CancellationToken.None));
        Assert.Equal("Validation failed: \r\n -- Id: User ID is required Severity: Error", exception.Message);
    }
}
