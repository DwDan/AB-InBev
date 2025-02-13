using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public UpdateProductHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateProductCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingUser = await _productRepository.GetByAsync((product) => product.Title == command.Title && product.Id != command.Id, cancellationToken);
        if (existingUser != null)
            throw new InvalidOperationException($"User with email {command.Title} already exists");

        var user = _mapper.Map<Product>(command);

        var createdUser = await _productRepository.UpdateAsync(user, cancellationToken);
        var result = _mapper.Map<UpdateProductResult>(createdUser);
        return result;
    }
}
