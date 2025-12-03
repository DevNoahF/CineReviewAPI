using CineReview.Data;
using CineReview.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization; // para Conversão de enum em string

var builder = WebApplication.CreateBuilder(args);


// Substituir registro de controllers para converter enums em strings
builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Swagger / OpenAPI services (configuração avançada)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<ISerieFilmeService, SerieFilmeService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = string.Empty; // Swagger UI disponível em "/"
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CineReview API v1");
});

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();