using Microsoft.EntityFrameworkCore;
using CineReview.Data;
using CineReview.Services;
using System.Reflection; // adicionar para XML comments
using System.Text.Json.Serialization; // para JsonStringEnumConverter

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Substituir registro de controllers para converter enums em strings
builder.Services.AddControllersWithViews().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Swagger / OpenAPI services (configuração avançada)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

// Application services
builder.Services.AddScoped<ISerieFilmeService, SerieFilmeService>();

var app = builder.Build();

// Apply migrations automatically (development scenario)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "docs"; // UI ficará em /docs
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CineReview API v1");
        // http://localhost:5279/docs/index.html
    });
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=SerieFilmes}/{action=Index}/{id?}");

app.Run();