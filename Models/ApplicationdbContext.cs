using Microsoft.EntityFrameworkCore;

namespace TP2.Models;

public class ApplicationdbContext : DbContext
{
    public ApplicationdbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movie>? Movies { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Movies)
            .WithMany(m => m.Customers)
            .UsingEntity(j => j.ToTable("CustomerMovie"));
        modelBuilder.Entity<BaseClass>()
            .HasIndex(b => b.Name)
            .IsUnique(true);
    }
}