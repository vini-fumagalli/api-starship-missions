using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Context;

//classe para representar um contexto do Banco de Dados
//que será mapeado pelo EntityFramework 
public class MyContext : DbContext
{
    //cada DbSet representa uma tabela no BD 
    public DbSet<StarshipEntity> STARSHIPS { get; set; } = default!;
    public DbSet<MissionsEnitity> MISSIONS_STARSHIP { get; set; } = default!;

    public MyContext(DbContextOptions<MyContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //defição de primary key de cada tabela
        modelBuilder.Entity<StarshipEntity>()
        .HasKey(e => new { e.Name });

        modelBuilder.Entity<MissionsEnitity>()
        .HasKey(e => new { e.Id });
    }
}