using Microsoft.EntityFrameworkCore;
using CineReview.Models;

namespace CineReview.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<SerieFilmeModel> SerieFilmes { get; set; } = null!;
}
