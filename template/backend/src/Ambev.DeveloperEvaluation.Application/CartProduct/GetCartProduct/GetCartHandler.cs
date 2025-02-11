using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.CartProducts.GetCartProduct;

public class GetCartProductHandler : IRequestHandler<GetCartProductCommand, GetCartProductResult>
{
    private readonly ICartProductRepository _cartProductRepository;
    private readonly IMapper _mapper;

    public GetCartProductHandler(
        ICartProductRepository cartProductRepository,
        IMapper mapper)
    {
        _cartProductRepository = cartProductRepository;
        _mapper = mapper;
    }

    public async Task<GetCartProductResult> Handle(GetCartProductCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetCartProductValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var cartProduct = await _cartProductRepository.GetByIdAsync(request.Id, cancellationToken);
        if (cartProduct == null)
            throw new KeyNotFoundException($"CartProduct with ID {request.Id} not found");

        return _mapper.Map<GetCartProductResult>(cartProduct);
    }
}
