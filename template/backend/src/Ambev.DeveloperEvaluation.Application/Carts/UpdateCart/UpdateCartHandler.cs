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

        var cart = _mapper.Map<Cart>(command);

        //var cartDB = await _cartRepository.GetByIdAsync(command.Id, cancellationToken);

        //if (cartDB == null)
        //    throw new ValidationException($"Cart with ID {cart.Id} not found.");

        //var cartProductsDB = await _cartProductRepository.GetAllByAsync(cp => cp.CartId == cart.Id && cp.Id, cancellationToken);
        //var productIdsInCommand = command.Products.Select(p => p.ProductId).ToList();
        //var productsToRemove = cartProductsDB.Where(cp => !productIdsInCommand.Contains(cp.ProductId)).ToList();

        //foreach (var productToRemove in productsToRemove)
        //    await _cartProductRepository.DeleteAsync(productToRemove.Id, cancellationToken);

        //foreach (var cartProduct in cart.Products)
        //{
        //    cartProduct.CartId = cart.Id;

        //    var product = await _productRepository.GetByIdAsync(cartProduct.ProductId);
        //    if (product == null)
        //        throw new ValidationException($"Product with ID {cartProduct.ProductId} not found.");

        //    cartProduct.Product = product;

        //    if (cartProduct.Id > 0)
        //        await _cartProductRepository.UpdateAsync(cartProduct, cancellationToken);
        //    else
        //        await _cartProductRepository.CreateAsync(cartProduct, cancellationToken);
        //}

        await _cartRepository.UpdateAsync(cart, cancellationToken);

        var result = _mapper.Map<UpdateCartResult>(cart);

        return result;
    }
}
