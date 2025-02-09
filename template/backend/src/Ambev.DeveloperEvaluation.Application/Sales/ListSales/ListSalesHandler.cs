using Ambev.DeveloperEvaluation.Domain.Common;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

public class ListSalesHandler : IRequestHandler<ListSalesCommand, ListSalesResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public ListSalesHandler(
        ISaleRepository saleRepository,
        IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<ListSalesResult> Handle(ListSalesCommand command, CancellationToken cancellationToken)
    {
        var validator = new ListSalesValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var apiQuery = _mapper.Map<ApiQueryRequestDomain>(command);

        var response = await _saleRepository.GetAllSalesAsync(apiQuery, cancellationToken);

        return _mapper.Map<ListSalesResult>(response);
    }
}
