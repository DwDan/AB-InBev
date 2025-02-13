using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.CartProducts.UpdateCartProduct;

public class UpdateCartProductHandler : IRequestHandler<UpdateCartProductCommand, UpdateCartProductResult>
{
    private readonly ICartProductRepository _cartProductRepository;
    private readonly IMapper _mapper;

    public UpdateCartProductHandler(ICartProductRepository cartProductRepository, IMapper mapper)
    {
        _cartProductRepository = cartProductRepository;
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
