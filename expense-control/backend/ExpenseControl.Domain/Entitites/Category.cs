public class Category
{
    public Guid Id { get; private set; }

    public string Description { get; private set; }

    public CategoryPurpose Purpose { get; private set; }

    public Category(string description, CategoryPurpose purpose)
    {
        Id = Guid.NewGuid();
        Description = description;
        Purpose = purpose;
    }
}