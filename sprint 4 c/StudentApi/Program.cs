using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.Models;
using StudentApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("ProductsDb"));

.
builder.Services.AddScoped<ProductService>();
builder.Services.AddHttpClient<JokeService>();

var app = builder.Build();


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


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
