using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Carts;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

public class UpdateCartHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly UpdateCartHandler _handler;

    public UpdateCartHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new UpdateCartHandler(_cartRepository, _productRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid cart data When updating cart Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        var command = UpdateCartHandlerTestData.GenerateValidCommand();
        var cart = UpdateCartHandlerTestData.GenerateValidCommand(command);
        var result = new UpdateCartResult { Id = cart.Id };

        _mapper.Map<Cart>(command).Returns(cart);
        _mapper.Map<UpdateCartResult>(cart).Returns(result);
        _cartRepository.UpdateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>()).Returns(cart);
        _productRepository.GetByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(cart.Products.First().Product);

        var updateCartResult = await _handler.Handle(command, CancellationToken.None);

        updateCartResult.Should().NotBeNull();
        updateCartResult.Id.Should().Be(cart.Id);
        await _cartRepository.Received(1).UpdateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid cart data When updating cart Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        var command = new UpdateCartCommand();

        var act = () => _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact(DisplayName = "Given non-existent product When updating cart Then throws validation exception")]
    public async Task Handle_NonExistentProduct_ThrowsValidationException()
    {
        var command = UpdateCartHandlerTestData.GenerateValidCommand();
        var cart = UpdateCartHandlerTestData.GenerateValidCommand(command);

        _mapper.Map<Cart>(command).Returns(cart);
        _productRepository.GetByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(default(Product));

        var act = () => _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<ValidationException>().WithMessage($"Product with ID {cart.Products.First().ProductId} not found.");
    }

    [Fact(DisplayName = "Given valid command When handling Then maps command to cart entity")]
    public async Task Handle_ValidRequest_MapsCommandToCart()
    {
        var command = UpdateCartHandlerTestData.GenerateValidCommand();
        var cart = UpdateCartHandlerTestData.GenerateValidCommand(command);

        _mapper.Map<Cart>(command).Returns(cart);
        _cartRepository.UpdateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>()).Returns(cart);
        _productRepository.GetByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(cart.Products.First().Product);

        await _handler.Handle(command, CancellationToken.None);

        _mapper.Received(1).Map<Cart>(command);
    }
}
