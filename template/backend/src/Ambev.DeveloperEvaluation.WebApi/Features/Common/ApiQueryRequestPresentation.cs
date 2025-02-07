namespace Ambev.DeveloperEvaluation.WebApi.Features.Common
{
    public class ApiQueryRequestPresentation
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
        public string Order { get; set; } = string.Empty;
    }
}
