using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetActiveCart;

public class GetActiveCartHandler : IRequestHandler<GetActiveCartCommand, GetActiveCartResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetActiveCartHandler(
        ICartRepository cartRepository,
        IUserService userService,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _cartRepository = cartRepository;
        _userService = userService;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<GetActiveCartResult> Handle(GetActiveCartCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetActiveCartValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var cart = await _cartRepository.GetByAsync((cart) => !cart.Inactive && cart.UserId == _userService.GetCurrentUserId());
        if (cart == null)
        {
            var userId = _userService.GetCurrentUserId();
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
            
            if (user == null)
                throw new ValidationException($"User with ID {userId} not found.");

            cart = new Cart { UserId = userId, Date = DateTime.UtcNow, User = user };
            cart = await _cartRepository.CreateAsync(cart, cancellationToken);
        }

        return _mapper.Map<GetActiveCartResult>(cart);
    }
}
