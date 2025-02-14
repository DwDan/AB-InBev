using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Common;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

/// <summary>
/// Contains unit tests for the <see cref="GetCartHandler"/> class.
/// </summary>
public class GetCartHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly GetCartHandler _handler;

    public GetCartHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetCartHandler(_cartRepository, _mapper);
    }

    [Fact(DisplayName = "Should return user when user exists")]
    public async Task GetCartHandler_ShouldReturnCart_WhenCartExists()
    {
        var command = GetCartHandlerTestData.GenerateValidCommand();
        var cart = CartHandlerTestData.GenerateValidEntity();

        var result = new GetCartResult
        {
            Id = cart.Id,
        };

        _mapper.Map<Cart>(command).Returns(cart);
        _mapper.Map<GetCartResult>(cart).Returns(result);

        _cartRepository.GetFullByIdAsync(command.Id, CancellationToken.None).Returns(cart);

        var getCartResult = await _handler.Handle(command, CancellationToken.None);

        getCartResult.Should().NotBeNull();
        getCartResult.Id.Should().Be(cart.Id);
        await _cartRepository.Received(1).GetFullByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Should throw KeyNotFoundException when user does not exist")]
    public async Task GetCartHandler_Throw_KeyNotFoundException_WhenCartDoesNotExist()
    {
        var command = GetCartHandlerTestData.GenerateValidCommand();

        _cartRepository.GetFullByIdAsync(command.Id, CancellationToken.None).Returns(default(Cart));

        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(async () => await _handler.Handle(command, CancellationToken.None));
        Assert.Equal($"Cart with ID {command.Id} not found", exception.Message);

        await _cartRepository.Received(1).GetFullByIdAsync(command.Id, CancellationToken.None);
    }


    [Fact(DisplayName = "Should throws ValidationException when id was not provide")]
    public async Task GetCartHandler_Throw_ValidationException_WhenCartDoesNotExist()
    {
        var command = new GetCartCommand(0);

        var exception = await Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(command, CancellationToken.None));
        Assert.Equal("Validation failed: \r\n -- Id: Cart ID is required Severity: Error", exception.Message);
    }
}
