using Microsoft.EntityFrameworkCore;

namespace ConsoleWinTasks
{
    public partial class GameContext : DbContext
    {
        public GameContext()
        {
        }

        public GameContext(DbContextOptions<GameContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Studio> Studios { get; set; }
        public virtual DbSet<StudioBranch> StudioBranches { get; set; }
        public virtual DbSet<StudioCountry> StudioCountries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Game;Trusted_Connection=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка модели Game
            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Game__3214EC073EE53829");

                entity.ToTable("Game");

                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.StuleGame).HasMaxLength(50);
                entity.Property(e => e.GameMode).HasMaxLength(50);

                // Связь с Studio
                entity.HasOne(d => d.Studio)
                      .WithMany(p => p.Games)
                      .HasForeignKey(d => d.StudioId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_Game_Studio");
            });

            // Настройка модели Studio
            modelBuilder.Entity<Studio>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Studio");

                entity.ToTable("Studio");

                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();

                // Связь с филиалами
                entity.HasMany(e => e.StudioBranches)
                      .WithOne(d => d.Studio)
                      .HasForeignKey(d => d.StudioId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Связь с странами
                entity.HasMany(e => e.StudioCountries)
                      .WithOne(d => d.Studio)
                      .HasForeignKey(d => d.StudioId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Настройка модели StudioBranch
            modelBuilder.Entity<StudioBranch>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_StudioBranch");

                entity.ToTable("StudioBranch");

                entity.Property(e => e.CityName).HasMaxLength(100).IsRequired();
            });

            // Настройка модели StudioCountry
            modelBuilder.Entity<StudioCountry>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_StudioCountry");

                entity.ToTable("StudioCountry");

                entity.Property(e => e.CountryName).HasMaxLength(100).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
