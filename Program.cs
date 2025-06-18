using Microsoft.EntityFrameworkCore;
using PortfolioApi.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
        Console.WriteLine("Database migrato con successo!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Si è verificato un errore durante la migration del database: {ex.Message}");
        throw;
    }
}

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

app.UseCors("AllowAll");

// Mappa le API controllers
app.MapControllers();

app.Run();