namespace Console.Consumer;

public record ProductModel
{
    public int Id { get; init; }
    public Guid Code { get; init; }
    public string Title { get; init; }
    public decimal Price { get; init; }
    public int CreatedOnUtc { get; init; }
}
