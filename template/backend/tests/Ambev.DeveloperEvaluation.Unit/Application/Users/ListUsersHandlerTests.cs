using Ambev.DeveloperEvaluation.Application.Users.ListUsers;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Users;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users;

/// <summary>
/// Contains unit tests for the <see cref="ListUsersHandler"/> class.
/// </summary>
public class ListUsersHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ListUsersHandler _handler;

    public ListUsersHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new ListUsersHandler(_userRepository, _mapper);
    }

    [Fact(DisplayName = "Should return user when users exists")]
    public async Task ListUsersHandler_ShouldReturnUser_WhenUserExists()
    {
        var command = ListUsersHandlerTestData.GenerateValidCommand();
        var apiQueryRequest = ListUsersHandlerTestData.GenerateValidCommand(command);
        var apiQueryResponse = ListUsersHandlerTestData.GenerateValidResponse();
        var result = ListUsersHandlerTestData.GenerateValidResponse(apiQueryResponse);

        _mapper.Map<ApiQueryRequestDomain>(command).Returns(apiQueryRequest);
        _mapper.Map<ListUsersResult>(apiQueryResponse).Returns(result);

        _userRepository.GetAllUsersAsync(apiQueryRequest, CancellationToken.None).Returns(apiQueryResponse);

        var listUsersResult = await _handler.Handle(command, CancellationToken.None);

        listUsersResult.Should().NotBeNull();
        listUsersResult.Data.Should().BeSameAs(result.Data);
        await _userRepository.Received(1).GetAllUsersAsync(apiQueryRequest, CancellationToken.None);
    }

    [Fact(DisplayName = "Should throws ValidationException when page was not provide")]
    public async Task ListUsersHandler_Throw_ValidationException_WhenPageWasNotProvided()
    {
        var command = new ListUsersCommand();
        command.Page = 0;

        var exception = await Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(command, CancellationToken.None));
        Assert.Equal("Validation failed: \r\n -- Page: Page must be greater than 0 Severity: Error", exception.Message);
    }

    [Fact(DisplayName = "Should throws ValidationException when page size was not provide")]
    public async Task ListUsersHandler_Throw_ValidationException_WhenPageSizeWasNotProvided()
    {
        var command = new ListUsersCommand();
        command.Size = 0;

        var exception = await Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(command, CancellationToken.None));
        Assert.Equal("Validation failed: \r\n -- Size: Page size must be greater than 0 Severity: Error", exception.Message);
    }
}
