namespace Ambev.DeveloperEvaluation.Application.Sales.Common
{
    public class SaleProductApplication
    {
        public int Id { get; set; }

        public int SaleId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Discount { get; set; }
    }
}
