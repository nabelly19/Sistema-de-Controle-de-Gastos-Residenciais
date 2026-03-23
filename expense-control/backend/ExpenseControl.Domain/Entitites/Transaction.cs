public class Transaction
{
    public Guid Id { get; private set; }

    public string Description { get; private set; }

    public decimal Amount { get; private set; }

    public TransactionType Type { get; private set; }

    public Guid PersonId { get; private set; }

    public Guid CategoryId { get; private set; }

    public Transaction(string description, decimal amount, TransactionType type, Guid personId, Guid categoryId)
    {
        if (amount <= 0)
            throw new Exception("Valor deve ser positivo");

        Id = Guid.NewGuid();
        Description = description;
        Amount = amount;
        Type = type;
        PersonId = personId;
        CategoryId = categoryId;
    }
}