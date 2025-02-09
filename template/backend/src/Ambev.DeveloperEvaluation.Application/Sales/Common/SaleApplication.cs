namespace Ambev.DeveloperEvaluation.Application.Sales.Common
{
    public class SaleApplication
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int BranchId { get; set; }

        public int SaleId { get; set; }

        public DateTime Date { get; set; }

        public decimal TotalAmount { get; set; }

        public bool IsCancelled { get; set; } = false;

        public virtual List<SaleProductApplication> Products { get; set; } = new();
    }
}
