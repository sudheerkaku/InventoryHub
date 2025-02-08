var builder = WebApplication.CreateBuilder(args);

// ✅ Add services for Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Register CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// ✅ Enable Swagger & Swagger UI
if (app.Environment.IsDevelopment()) // Enable only in development mode
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ✅ Apply the CORS policy before routing
app.UseCors("AllowAll");

app.MapGet("/api/products", () =>
{
    return new[]
    {
        new { Id = 1, Name = "Laptop", Price = 1200.50, Stock = 25, Category = new { Id = 101, Name = "Electronics" } },
        new { Id = 2, Name = "Headphones", Price = 50.00, Stock = 100, Category = new { Id = 102, Name = "Accessories" } }
    };
})
.WithName("GetProducts"); // Adds metadata for Swagger

app.Run();