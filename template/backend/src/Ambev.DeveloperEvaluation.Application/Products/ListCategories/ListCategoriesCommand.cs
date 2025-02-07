using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListCategories;

public record ListCategoriesCommand : IRequest<ListCategoriesResult> { }
