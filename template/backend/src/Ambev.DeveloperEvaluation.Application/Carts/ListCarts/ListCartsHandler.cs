using Ambev.DeveloperEvaluation.Domain.Common;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCarts;

public class ListCartsHandler : IRequestHandler<ListCartsCommand, ListCartsResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;

    public ListCartsHandler(
        ICartRepository cartRepository,
        IMapper mapper)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
    }

    public async Task<ListCartsResult> Handle(ListCartsCommand command, CancellationToken cancellationToken)
    {
        var validator = new ListCartsValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var apiQuery = _mapper.Map<ApiQueryRequestDomain>(command);

        var response = await _cartRepository.GetAllCartsAsync(apiQuery, cancellationToken);

        return _mapper.Map<ListCartsResult>(response);
    }
}
