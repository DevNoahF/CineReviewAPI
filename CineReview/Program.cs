using CineReview.Data;
using CineReview.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization; // para Conversão de enum em string

var builder = WebApplication.CreateBuilder(args);


// Enums to String json reponses configuration
builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// database connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.
    UseSqlServer(connectionString));

builder.Services.AddScoped<ISerieFilmeService, SerieFilmeService>();

var app = builder.Build();

app.UseHttpsRedirection();

// Swagger configuration --
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = string.Empty; 
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CineReview API v1");
});

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();