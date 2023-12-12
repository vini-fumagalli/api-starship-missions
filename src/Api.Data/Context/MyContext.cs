using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Context;

public class MyContext : DbContext
{
    public DbSet<StarshipEntity> STARSHIPS { get; set; } = default!;
    public DbSet<MissionsEnitity> MISSIONS_STARSHIP { get; set; } = default!;

    public MyContext(DbContextOptions<MyContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StarshipEntity>()
        .HasKey(e => new { e.Name });

        modelBuilder.Entity<MissionsEnitity>()
        .HasKey(e => new { e.Id });
    }
}