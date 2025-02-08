﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

public class UpdateCartHandler : IRequestHandler<UpdateCartCommand, UpdateCartResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public UpdateCartHandler(ICartRepository cartRepository,
        IUserRepository userRepository, IProductRepository productRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
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

        var cart = _mapper.Map<Cart>(command);

        var user = await _userRepository.GetByIdAsync(cart.UserId);
        if (user == null)
            throw new ValidationException("User not found.");

        cart.User = user;

        foreach (var cartProduct in cart.Products)
        {
            var product = await _productRepository.GetByIdAsync(cartProduct.ProductId);
            if (product == null)
                throw new ValidationException($"Product with ID {cartProduct.ProductId} not found.");

            cartProduct.Product = product;
        }

        var createdUser = await _cartRepository.UpdateAsync(cart, cancellationToken);
        var result = _mapper.Map<UpdateCartResult>(createdUser);
        return result;
    }
}
