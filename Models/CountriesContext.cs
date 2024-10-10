using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ConsoleWinTasks;

public partial class CountriesContext : DbContext
{
    public CountriesContext()
    {
    }

    public CountriesContext(DbContextOptions<CountriesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Continent> Continents { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Countries;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cities__3214EC0700CC6543");

            entity.Property(e => e.CityName).HasMaxLength(100);

            entity.HasOne(d => d.IdCountryNavigation).WithMany(p => p.Cities)
                .HasForeignKey(d => d.IdCountry)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cities__IdCountr__3B75D760");
        });

        modelBuilder.Entity<Continent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Continen__3214EC07B496202A");

            entity.Property(e => e.ContinentName).HasMaxLength(100);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Countrie__3214EC07271DBE9D");

            entity.Property(e => e.Area).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CapitalName).HasMaxLength(100);
            entity.Property(e => e.CountryName).HasMaxLength(100);

            entity.HasOne(d => d.IdCapitalCityNavigation).WithMany(p => p.Countries)
                .HasForeignKey(d => d.IdCapitalCity)
                .HasConstraintName("FK__Countries__IdCap__3C69FB99");

            entity.HasOne(d => d.IdContinentNavigation).WithMany(p => p.Countries)
                .HasForeignKey(d => d.IdContinent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Countries__IdCon__38996AB5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
