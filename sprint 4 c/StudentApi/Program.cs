using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.Models;
using StudentApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the dependency injection container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Entity Framework Core with an in‑memory database.  In a real
// application you could switch to a relational provider (e.g. SQL Server)
// without changing the rest of the code.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("ProductsDb"));

// Register application services.  ProductService is registered as scoped so
// each HTTP request gets its own instance.  JokeService is registered as a
// typed HttpClient which will be configured automatically by the factory.
builder.Services.AddScoped<ProductService>();
builder.Services.AddHttpClient<JokeService>();

var app = builder.Build();

// Seed the database with some initial products.  When the app is first
// started the in‑memory database is empty; adding sample data makes it
// easier to test the API right away.
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    if (!context.Products.Any())
    {
        context.Products.AddRange(
            new Product { Name = "Lápis", Description = "Lápis de escrever", Price = 1.50M, Quantity = 100 },
            new Product { Name = "Caderno", Description = "Caderno universitário", Price = 15.00M, Quantity = 50 },
            new Product { Name = "Borracha", Description = "Borracha branca", Price = 0.80M, Quantity = 200 }
        );
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.  Swagger UI is exposed at /swagger and
// the JSON specification at /swagger/v1/swagger.json.  We don't wrap the
// middleware in an environment check so that Swagger is always available as
// recommended when publishing to Azure【607809691539310†L144-L178】.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();