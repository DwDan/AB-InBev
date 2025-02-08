using Ambev.DeveloperEvaluation.Application.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

public record ListUsersCommand : ApiQueryRequestApplication, IRequest<ListUsersResult> { }
