using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TP2.Models;

public partial class ApplicationdbContext : DbContext
{
    public ApplicationdbContext()
    {
    }

    public ApplicationdbContext(DbContextOptions<ApplicationdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=tp2-net;Username=postgres;Password=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasIndex(e => e.GenreId, "IX_Movies_GenreId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            
            entity.HasOne(d => d.Genre).WithMany(p => p.Movies).HasForeignKey(d => d.GenreId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
