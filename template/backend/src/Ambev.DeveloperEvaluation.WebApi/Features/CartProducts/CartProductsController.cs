using Ambev.DeveloperEvaluation.Application.CartProducts.CreateCartProduct;
using Ambev.DeveloperEvaluation.Application.CartProducts.DeleteCartProduct;
using Ambev.DeveloperEvaluation.Application.CartProducts.GetCartProduct;
using Ambev.DeveloperEvaluation.Application.CartProducts.UpdateCartProduct;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.CartProducts.CreateCartProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.CartProducts.DeleteCartProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.CartProducts.GetCartProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.CartProducts.UpdateCartProduct;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.CartProducts;

/// <summary>
/// Controller for managing cartProduct operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CartProductsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CartProductsController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CartProductsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new cartProduct
    /// </summary>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateCartProductResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCartProduct([FromBody] CreateCartProductRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateCartProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateCartProductCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(_mapper.Map<CreateCartProductResponse>(response));
    }

    /// <summary>
    /// Retrieves a cartProduct by ID
    /// </summary>
    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponseWithData<GetCartProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCartProduct([FromRoute] int id, CancellationToken cancellationToken)
    {
        var request = new GetCartProductRequest { Id = id };
        var validator = new GetCartProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetCartProductCommand>(request.Id);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(_mapper.Map<GetCartProductResponse>(response));
    }

    /// <summary>
    /// Updates a cartProduct by ID
    /// </summary>
    [HttpPut("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateCartProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCartProduct([FromRoute] int id, [FromBody] UpdateCartProductRequest request, CancellationToken cancellationToken)
    {
        request.Id = id;
        var validator = new UpdateCartProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<UpdateCartProductCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(_mapper.Map<UpdateCartProductResponse>(response));
    }

    /// <summary>
    /// Deletes a cartProduct by ID
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCartProduct([FromRoute] int id, CancellationToken cancellationToken)
    {
        var request = new DeleteCartProductRequest { Id = id };
        var validator = new DeleteCartProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteCartProductCommand>(request.Id);
        await _mediator.Send(command, cancellationToken);

        return Ok(true);
    }
}
