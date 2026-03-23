public class Person
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public int Age { get; private set; }

    public List<Transaction> Transactions { get; private set; } = new();

    public Person(string name, int age)
    {
        Id = Guid.NewGuid();
        Name = name;
        Age = age;
    }
}