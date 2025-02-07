using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListCategories;

public class ListCategoriesHandler : IRequestHandler<ListCategoriesCommand, ListCategoriesResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ListCategoriesHandler(
        IProductRepository productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ListCategoriesResult> Handle(ListCategoriesCommand request, CancellationToken cancellationToken)
    {
        var validator = new ListCategoriesValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var categories = await _productRepository.GetAllCategoriesAsync(cancellationToken);
        if (categories == null)
            throw new KeyNotFoundException($"Categories not found");

        return _mapper.Map<ListCategoriesResult>(categories);
    }
}
