using System.ComponentModel.DataAnnotations;

namespace ETL.Data.Models
{
    public class Transaction
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
