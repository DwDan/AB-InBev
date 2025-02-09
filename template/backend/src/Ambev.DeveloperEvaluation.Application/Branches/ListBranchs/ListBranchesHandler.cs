using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branches.ListBranches;

public class ListBranchesHandler : IRequestHandler<ListBranchesCommand, ListBranchesResult>
{
    private readonly IBranchRepository _branchRepository;
    private readonly IMapper _mapper;

    public ListBranchesHandler(
        IBranchRepository branchRepository,
        IMapper mapper)
    {
        _branchRepository = branchRepository;
        _mapper = mapper;
    }

    public async Task<ListBranchesResult> Handle(ListBranchesCommand command, CancellationToken cancellationToken)
    {
        var validator = new ListBranchesValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var apiQuery = _mapper.Map<ApiQueryRequestDomain>(command);

        var response = await _branchRepository.GetAllBranchesAsync(apiQuery, cancellationToken);

        return _mapper.Map<ListBranchesResult>(response);
    }
}
