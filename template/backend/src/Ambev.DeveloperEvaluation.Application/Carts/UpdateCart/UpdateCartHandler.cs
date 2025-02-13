using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

public class UpdateCartHandler : IRequestHandler<UpdateCartCommand, UpdateCartResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly ICartProductRepository _cartProductRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public UpdateCartHandler(ICartRepository cartRepository, ICartProductRepository cartProductRepository,
        IUserRepository userRepository, IProductRepository productRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _cartProductRepository = cartProductRepository;
        _productRepository = productRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UpdateCartResult> Handle(UpdateCartCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateCartCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        if (await _cartRepository.GetByIdAsync(command.Id, cancellationToken) == null)
            throw new KeyNotFoundException($"Cart with ID {command.Id} not found");

        var cart = _mapper.Map<Cart>(command);

        cart = await CalculateCartTotalWithDiscounts(cart, cancellationToken);

        await _cartRepository.UpdateAsync(cart, cancellationToken);

        var result = _mapper.Map<UpdateCartResult>(cart);

        return result;
    }

    private async Task<Cart> CalculateCartTotalWithDiscounts(Cart cart, CancellationToken cancellationToken)
    {
        decimal totalCartPrice = 0;
        decimal totalCartPriceWithDiscount = 0;

        foreach (var cartProduct in cart.Products)
        {
            var product = await _productRepository.GetByIdAsync(cartProduct.ProductId, cancellationToken);

            if (product == null)
                throw new ValidationException($"Product with ID {cartProduct.ProductId} not found.");

            if (cartProduct.Quantity > 20)
                throw new ValidationException($"Cannot purchase more than 20 units of product ID {cartProduct.ProductId}.");

            cartProduct.Product = product;
            cartProduct.UnityPrice = product.Price;

            if (cartProduct.Quantity >= 10)
                cartProduct.Discount = 0.20m; 
            else if (cartProduct.Quantity >= 4)
                cartProduct.Discount = 0.10m; 
            else
                cartProduct.Discount = 0.00m; 

            decimal originalTotalPrice = cartProduct.UnityPrice * cartProduct.Quantity;
            decimal discountValue = originalTotalPrice * cartProduct.Discount;
            cartProduct.TotalPrice = originalTotalPrice - discountValue;

            totalCartPrice += originalTotalPrice;
            totalCartPriceWithDiscount += cartProduct.TotalPrice;
        }

        cart.Price = totalCartPrice;
        cart.TotalPrice = totalCartPriceWithDiscount;
        return cart;
    }
}
