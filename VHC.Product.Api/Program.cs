using Microsoft.EntityFrameworkCore;
using VHC.Product.Helpers.Repository;
using VHC.Product.Infrastructure;
using VHC.Product.Infrastructure.Data;
using VHC.Product.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseInMemoryDatabase("VHCProduct");
    options.EnableSensitiveDataLogging();
});

builder.Services.AddControllers();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IRepository<VHC.Product.Domain.Product, VHC.Product.Domain.ProductFilter>, ProductRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
