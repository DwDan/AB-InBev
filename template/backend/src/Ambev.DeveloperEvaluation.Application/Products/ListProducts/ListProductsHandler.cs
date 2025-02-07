using Ambev.DeveloperEvaluation.Domain.Common;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

public class ListProductsHandler : IRequestHandler<ListProductsCommand, ListProductsResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ListProductsHandler(
        IProductRepository productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ListProductsResult> Handle(ListProductsCommand command, CancellationToken cancellationToken)
    {
        var validator = new ListProductsValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var apiQuery = _mapper.Map<ApiQueryRequestDomain>(command);

        var response = await _productRepository.GetAllProductsAsync(apiQuery, cancellationToken);

        return _mapper.Map<ListProductsResult>(response);
    }
}
