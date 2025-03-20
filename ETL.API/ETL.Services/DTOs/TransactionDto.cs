namespace ETL.Services.DTOs
{
    public class TransactionDto
    {
        public int CustomerID { get; set; }

        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
