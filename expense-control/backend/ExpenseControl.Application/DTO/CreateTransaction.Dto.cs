using ExpenseControl.Domain.Enums;

namespace ExpenseControl.Application.DTOs
{
    public class CreateTransactionDto
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public Guid PersonId { get; set; }
        public Guid CategoryId { get; set; }
    }
}