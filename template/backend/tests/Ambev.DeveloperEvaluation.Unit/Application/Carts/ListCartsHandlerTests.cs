using Ambev.DeveloperEvaluation.Application.Carts.ListCarts;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Carts;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

public class ListCartsHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly ListCartsHandler _handler;

    public ListCartsHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new ListCartsHandler(_cartRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid query When listing carts Then returns list response")]
    public async Task Handle_ValidRequest_ReturnsListResponse()
    {
        // Given
        var command = ListCartsHandlerTestData.GenerateValidCommand();
        var apiQuery = new ApiQueryRequestDomain();
        var response = ListCartsHandlerTestData.GenerateValidResponse();

        _mapper.Map<ApiQueryRequestDomain>(command).Returns(apiQuery);
        _cartRepository.GetAllCartsAsync(apiQuery, Arg.Any<CancellationToken>()).Returns(response);
        _mapper.Map<ListCartsResult>(response).Returns(new ListCartsResult());

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        await _cartRepository.Received(1).GetAllCartsAsync(apiQuery, Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid query When listing carts Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new ListCartsCommand();
        command.Size = 0;

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact(DisplayName = "Given valid query When handling Then maps command to API query")]
    public async Task Handle_ValidRequest_MapsCommandToApiQuery()
    {
        // Given
        var command = ListCartsHandlerTestData.GenerateValidCommand();
        var apiQuery = new ApiQueryRequestDomain();
        _mapper.Map<ApiQueryRequestDomain>(command).Returns(apiQuery);

        _cartRepository.GetAllCartsAsync(apiQuery, Arg.Any<CancellationToken>()).Returns(ListCartsHandlerTestData.GenerateValidResponse());

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<ApiQueryRequestDomain>(command);
    }
}
