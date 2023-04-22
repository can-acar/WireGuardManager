using Microsoft.EntityFrameworkCore;
using WireGuardManager.Domain.Entities;

namespace WireGuardManager.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Interface> Interfaces { get; set; }
    public DbSet<Peer> Peers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<JwtSettings> JwtSettings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}