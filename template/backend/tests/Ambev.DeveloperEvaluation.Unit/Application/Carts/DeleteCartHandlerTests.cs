using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Carts;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Common;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

/// <summary>
/// Contains unit tests for the <see cref="DeleteCartHandler"/> class.
/// </summary>
public class DeleteCartHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly DeleteCartHandler _handler;

    public DeleteCartHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new DeleteCartHandler(_cartRepository);
    }

    [Fact(DisplayName = "Should return true when user deleted")]
    public async Task DeleteCartHandler_ShouldReturnCart_WhenCartExists()
    {
        var command = DeleteCartHandlerTestData.GenerateValidCommand();
        var user = CartHandlerTestData.GenerateValidEntity();

        _cartRepository.DeleteAsync(command.Id, CancellationToken.None).Returns(true);

        var deleteCartResponse = await _handler.Handle(command, CancellationToken.None);

        deleteCartResponse.Should().NotBeNull();
        deleteCartResponse.Success.Should().Be(true);
        await _cartRepository.Received(1).DeleteAsync(command.Id, CancellationToken.None);
    }

    [Fact(DisplayName = "Should throw KeyNotFoundException when user does not exist")]
    public async Task DeleteCartHandler_Throw_KeyNotFoundException_WhenCartDoesNotExist()
    {
        var command = DeleteCartHandlerTestData.GenerateValidCommand();
        var user = CartHandlerTestData.GenerateValidEntity();

        _cartRepository.DeleteAsync(command.Id, CancellationToken.None).Returns(false);

        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(async () => await _handler.Handle(command, CancellationToken.None));
        Assert.Equal($"Cart with ID {command.Id} not found", exception.Message);

        await _cartRepository.Received(1).DeleteAsync(command.Id, CancellationToken.None);
    }

    [Fact(DisplayName = "Should throws ValidationException when id was not provide")]
    public async Task DeleteCartHandler_Throw_ValidationException_WhenCartDoesNotExist()
    {
        var command = new DeleteCartCommand(0);
        var user = CartHandlerTestData.GenerateValidEntity();

        var exception = await Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(command, CancellationToken.None));
        Assert.Equal("Validation failed: \r\n -- Id: Cart ID is required Severity: Error", exception.Message);
    }
}
