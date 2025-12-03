using Microsoft.EntityFrameworkCore;
using CineReview.Models;

namespace CineReview.Data;

public class ApplicationDbContext : DbContext
{
    // default constructor
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // dbset connect tupla
    public DbSet<SerieFilmeModel> SerieFilmes { get; set; } = null!;
}
