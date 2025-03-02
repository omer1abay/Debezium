namespace Debezium.Example.Model;

public record ProductModel
{
    public int Id { get; init; }
    public Guid Code { get; init; }
    public string Title { get; init; }
    public decimal Price { get; init; }
    public DateTime CreatedOnUtc { get; init; }
}