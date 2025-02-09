namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Common
{
    public class SalePresentation
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public virtual List<SaleProductPresentation> Products { get; set; } = new();
    }
}
