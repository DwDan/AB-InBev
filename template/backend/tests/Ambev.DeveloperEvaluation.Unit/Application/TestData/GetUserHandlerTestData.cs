using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Bogus;

public static class GetUserHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid GetUserCommand instances.
    /// The generated commands will have valid IDs.
    /// </summary>
    private static readonly Faker<GetUserCommand> getUserHandlerFaker = new Faker<GetUserCommand>()
        .CustomInstantiator(f => new GetUserCommand(f.Random.Int(1, 99)));

    /// <summary>
    /// Generates a valid GetUserCommand with randomized data.
    /// </summary>
    /// <returns>A valid GetUserCommand instance.</returns>
    public static GetUserCommand GenerateValidCommand()
    {
        return getUserHandlerFaker.Generate();
    }
}
