using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Server.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<AppUser> AppUsers { get; set; } = null!;
    public DbSet<Student> Students { get; set; } = null!;
}