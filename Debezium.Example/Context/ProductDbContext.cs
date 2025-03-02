using Debezium.Example.Model;
using Microsoft.EntityFrameworkCore;

namespace Debezium.Example.Context;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {
    }
    public DbSet<ProductModel> Product { get; set; }
}
