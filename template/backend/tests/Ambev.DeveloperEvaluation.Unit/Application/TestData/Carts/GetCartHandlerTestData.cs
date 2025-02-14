using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Bogus;

public static class GetCartHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid GetCartCommand instances.
    /// The generated commands will have valid IDs.
    /// </summary>
    private static readonly Faker<GetCartCommand> getCartHandlerFaker = new Faker<GetCartCommand>()
        .CustomInstantiator(f => new GetCartCommand(f.Random.Int(1, 99)));

    /// <summary>
    /// Generates a valid GetCartCommand with randomized data.
    /// </summary>
    /// <returns>A valid GetCartCommand instance.</returns>
    public static GetCartCommand GenerateValidCommand()
    {
        return getCartHandlerFaker.Generate();
    }
}
