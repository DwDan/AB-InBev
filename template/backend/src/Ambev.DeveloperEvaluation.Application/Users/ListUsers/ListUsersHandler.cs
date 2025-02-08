using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

public class ListUsersHandler : IRequestHandler<ListUsersCommand, ListUsersResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public ListUsersHandler(
        IUserRepository userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ListUsersResult> Handle(ListUsersCommand command, CancellationToken cancellationToken)
    {
        var validator = new ListUsersValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var apiQuery = _mapper.Map<ApiQueryRequestDomain>(command);

        var response = await _userRepository.GetAllUsersAsync(apiQuery, cancellationToken);

        return _mapper.Map<ListUsersResult>(response);
    }
}
