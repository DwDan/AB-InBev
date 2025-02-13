using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branches.UpdateBranch;

public class UpdateBranchHandler : IRequestHandler<UpdateBranchCommand, UpdateBranchResult>
{
    private readonly IBranchRepository _branchRepository;
    private readonly IMapper _mapper;

    public UpdateBranchHandler(IBranchRepository branchRepository, IMapper mapper)
    {
        _branchRepository = branchRepository;
        _mapper = mapper;
    }

    public async Task<UpdateBranchResult> Handle(UpdateBranchCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateBranchCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        if (await _branchRepository.GetByIdAsync(command.Id, cancellationToken) == null)
            throw new KeyNotFoundException($"Branch with ID {command.Id} not found");

        var existingUser = await _branchRepository.GetByAsync((branch)=> branch.Name == command.Name && branch.Id != command.Id, cancellationToken);
        if (existingUser != null)
            throw new InvalidOperationException($"Branch with name {command.Name} already exists");

        var branch = _mapper.Map<Branch>(command);

        var createdUser = await _branchRepository.UpdateAsync(branch, cancellationToken);
        var result = _mapper.Map<UpdateBranchResult>(createdUser);
        return result;
    }
}
