using Debezium.Example.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ProductDbContext>(opts => opts.UseNpgsql(builder.Configuration.GetConnectionString("ProductWriteDb")));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();





app.Run();

