using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<AppUser> AppUsers { get; set; } = null!;
    public DbSet<Student> Students { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>()
            .HasIndex(e => e.Email)
            .IsUnique();
    }
}