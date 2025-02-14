using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Carts;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

public class CreateCartHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly CreateCartHandler _handler;

    public CreateCartHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _userRepository = Substitute.For<IUserRepository>();
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateCartHandler(_cartRepository, _userRepository, _productRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid cart data When creating cart Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateCartHandlerTestData.GenerateValidCommand();
        var cart = CreateCartHandlerTestData.GenerateValidCommand(command);
        var result = new CreateCartResult { Id = cart.Id };

        _mapper.Map<Cart>(command).Returns(cart);
        _mapper.Map<CreateCartResult>(cart).Returns(result);
        _userRepository.GetByIdAsync(cart.UserId).Returns(new User { Id = cart.UserId });

        foreach (var cartProduct in cart.Products)
        {
            _productRepository.GetByIdAsync(cartProduct.ProductId).Returns(new Product { Id = cartProduct.ProductId });
        }

        _cartRepository.CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>()).Returns(cart);

        // When
        var createCartResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createCartResult.Should().NotBeNull();
        createCartResult.Id.Should().Be(cart.Id);
        await _cartRepository.Received(1).CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid cart data When creating cart Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateCartCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact(DisplayName = "Given non-existent user When creating cart Then throws validation exception")]
    public async Task Handle_NonExistentUser_ThrowsValidationException()
    {
        // Given
        var command = CreateCartHandlerTestData.GenerateValidCommand();
        var cart = CreateCartHandlerTestData.GenerateValidCommand(command);

        _mapper.Map<Cart>(command).Returns(cart);
        _userRepository.GetByIdAsync(cart.UserId).Returns(default(User));

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>().WithMessage("User not found.");
    }

    [Fact(DisplayName = "Given non-existent product When creating cart Then throws validation exception")]
    public async Task Handle_NonExistentProduct_ThrowsValidationException()
    {
        // Given
        var command = CreateCartHandlerTestData.GenerateValidCommand();
        var cart = CreateCartHandlerTestData.GenerateValidCommand(command);

        _mapper.Map<Cart>(command).Returns(cart);
        _userRepository.GetByIdAsync(cart.UserId).Returns(new User { Id = cart.UserId });
        _productRepository.GetByIdAsync(Arg.Any<int>()).Returns(default(Product));

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>().WithMessage("Product with ID * not found.");
    }

    [Fact(DisplayName = "Given valid command When handling Then maps command to cart entity")]
    public async Task Handle_ValidRequest_MapsCommandToCart()
    {
        // Given
        var command = CreateCartHandlerTestData.GenerateValidCommand();
        var cart = CreateCartHandlerTestData.GenerateValidCommand(command);

        _mapper.Map<Cart>(command).Returns(cart);
        _userRepository.GetByIdAsync(cart.UserId).Returns(new User { Id = cart.UserId });
        _cartRepository.CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>()).Returns(cart);

        foreach (var cartProduct in cart.Products)
        {
            _productRepository.GetByIdAsync(cartProduct.ProductId).Returns(new Product { Id = cartProduct.ProductId });
        }

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<Cart>(Arg.Is<CreateCartCommand>(c =>
            c.UserId == command.UserId &&
            c.Products.Count == command.Products.Count));
    }
}
