using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CreateSaleHandler(ISaleRepository saleRepository, 
        IUserRepository userRepository, IProductRepository productRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _productRepository = productRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = _mapper.Map<Sale>(command);

        var user = await _userRepository.GetByIdAsync(sale.UserId);
        if (user == null)
            throw new ValidationException("User not found.");

        sale.User = user;

        foreach (var saleProduct in sale.Products)
        {
            var product = await _productRepository.GetByIdAsync(saleProduct.ProductId);
            if (product == null)
                throw new ValidationException($"Product with ID {saleProduct.ProductId} not found.");

            saleProduct.Product = product;
        }

        var createdUser = await _saleRepository.CreateAsync(sale, cancellationToken);
        var result = _mapper.Map<CreateSaleResult>(createdUser);
        return result;
    }
}
