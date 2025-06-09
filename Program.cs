using Microsoft.EntityFrameworkCore;
using PortfolioApp.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Configura EF Core per PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Aggiungi Swagger per documentazione API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    // Abilita middleware Swagger in sviluppo
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Portfolio API V1");
        c.RoutePrefix = string.Empty; // Swagger UI a radice (es. https://localhost:5001/)
    });
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

// Mappa le API controllers
app.MapControllers();

app.Run();
