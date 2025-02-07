using Ambev.DeveloperEvaluation.Domain.Common;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProductsByCategory;

public class ListProductsByCategoryHandler : IRequestHandler<ListProductsByCategoryCommand, ListProductsByCategoryResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ListProductsByCategoryHandler(
        IProductRepository productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ListProductsByCategoryResult> Handle(ListProductsByCategoryCommand command, CancellationToken cancellationToken)
    {
        var validator = new ListProductsByCategoryValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var apiQuery = _mapper.Map<ApiQueryRequestDomain>(command);

        var result = await _productRepository.GetAllProductsByCategoryAsync(apiQuery, command.Category, cancellationToken);

        return _mapper.Map<ListProductsByCategoryResult>(result);
    }
}
