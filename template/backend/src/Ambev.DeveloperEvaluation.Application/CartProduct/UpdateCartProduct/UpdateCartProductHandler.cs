using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.CartProducts.UpdateCartProduct;

public class UpdateCartProductHandler : IRequestHandler<UpdateCartProductCommand, UpdateCartProductResult>
{
    private readonly ICartProductRepository _cartProductRepository;
    private readonly ICartProductRepository _cartProductProductRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public UpdateCartProductHandler(ICartProductRepository cartProductRepository, ICartProductRepository cartProductProductRepository,
        IUserRepository userRepository, IProductRepository productRepository, IMapper mapper)
    {
        _cartProductRepository = cartProductRepository;
        _cartProductProductRepository = cartProductProductRepository;
        _productRepository = productRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UpdateCartProductResult> Handle(UpdateCartProductCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateCartProductCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var cartProduct = _mapper.Map<CartProduct>(command);

        await _cartProductRepository.UpdateAsync(cartProduct, cancellationToken);

        var result = _mapper.Map<UpdateCartProductResult>(cartProduct);

        return result;
    }
}
