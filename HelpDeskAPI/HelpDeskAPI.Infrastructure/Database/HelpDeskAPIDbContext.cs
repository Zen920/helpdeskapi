using Microsoft.EntityFrameworkCore;
using HelpDeskAPI.Core.Models;

namespace HelpDeskAPI.Infrastructure.Database;

public class HelpDeskAPIDbContext : DbContext
{
    public DbSet<Utente> Utenti { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Commento> Commenti { get; set; }
    public DbSet<Assegnazione> Assegnazioni { get; set; }
    public HelpDeskAPIDbContext(DbContextOptions<HelpDeskAPIDbContext> options)
        : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Assegnazione>()
                .HasOne(a => a.Utente)
                .WithMany()
                .HasForeignKey(a => a.UtenteId)
                .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Commento>()
        .HasOne(c => c.Autore)
        .WithMany()
        .HasForeignKey(c => c.UtenteId)
        .OnDelete(DeleteBehavior.Restrict);
    }
}
