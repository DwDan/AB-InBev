using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.CartProducts.CreateCartProduct;

public class CreateCartProductHandler : IRequestHandler<CreateCartProductCommand, CreateCartProductResult>
{
    private readonly ICartProductRepository _cartProductRepository;
    private readonly IMapper _mapper;

    public CreateCartProductHandler(ICartProductRepository cartProductRepository, IMapper mapper)
    {
        _cartProductRepository = cartProductRepository;
        _mapper = mapper;
    }

    public async Task<CreateCartProductResult> Handle(CreateCartProductCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateCartProductCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var cartProduct = _mapper.Map<CartProduct>(command);

        var createdUser = await _cartProductRepository.CreateAsync(cartProduct, cancellationToken);
        var result = _mapper.Map<CreateCartProductResult>(createdUser);
        return result;
    }
}
