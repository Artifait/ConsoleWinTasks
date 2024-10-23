
using Microsoft.EntityFrameworkCore;

namespace ConsoleWinTasks;

public class BookStoreContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .Property(b => b.Title)
            .HasMaxLength(255); 

        modelBuilder.Entity<Author>()
            .Property(a => a.FirstName)
            .HasMaxLength(100); 

        modelBuilder.Entity<Author>()
            .Property(a => a.LastName)
            .HasMaxLength(100);

        modelBuilder.Entity<Author>()
            .Property(a => a.MiddleName)
            .HasMaxLength(100); 

        modelBuilder.Entity<Discount>()
            .Property(d => d.Name)
            .HasMaxLength(50); 

        modelBuilder.Entity<Genre>()
            .Property(g => g.Name)
            .HasMaxLength(50); 

        modelBuilder.Entity<Publisher>()
            .Property(p => p.Name)
            .HasMaxLength(150); 

        modelBuilder.Entity<User>()
            .Property(u => u.Username)
            .HasMaxLength(100); 

        modelBuilder.Entity<User>()
            .Property(u => u.HashPassword)
            .HasMaxLength(100); 
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BookStore;Trusted_Connection=True;");
}
