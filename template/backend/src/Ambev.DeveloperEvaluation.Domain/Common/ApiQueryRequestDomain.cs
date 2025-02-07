namespace Ambev.DeveloperEvaluation.Domain.Common
{
    public class ApiQueryRequestDomain
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
        public string Order { get; set; } = string.Empty;
    }
}
