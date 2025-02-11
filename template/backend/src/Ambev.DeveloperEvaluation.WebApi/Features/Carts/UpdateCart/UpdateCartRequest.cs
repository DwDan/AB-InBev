using Ambev.DeveloperEvaluation.WebApi.Features.Carts.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

public class UpdateCartRequest : CartPresentation 
{
    public bool IsFinished { get; set; }
    public bool IsCancelled { get; set; }
}
