using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<CUser> User { get; set; } = null!;
    public DbSet<CUserSession> UserSession { get; set; } = null!;

    //That method tells EF Core how to connect to your PostgreSQL database.
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string DbConnectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL") ?? "";
        optionsBuilder.UseNpgsql(DbConnectionUrl);
    }

    // this is where you define your db table constraints
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // // -----------------------------
        // // Table names
        // // -----------------------------
        // modelBuilder.Entity<CUser>().ToTable("Users");
        // modelBuilder.Entity<CUserSession>().ToTable("UserSessions");

        // // -----------------------------
        // // Primary Keys
        // // -----------------------------
        // modelBuilder.Entity<CUser>().HasKey(u => u.id);
        // modelBuilder.Entity<CUserSession>().HasKey(s => s.id);

        // // -----------------------------
        // // Column constraints
        // // -----------------------------
        // modelBuilder.Entity<CUser>().Property(u => u.Username).HasMaxLength(100).IsRequired();
        // modelBuilder.Entity<CUser>().Property(u => u.Email).HasMaxLength(150).IsRequired();
        // modelBuilder.Entity<CUserSession>().Property(s => s.Token).HasMaxLength(512).IsRequired();

        // // -----------------------------
        // // Unique indexes
        // // -----------------------------
        // modelBuilder.Entity<CUser>().HasIndex(u => u.Username).IsUnique();
        // modelBuilder.Entity<CUser>().HasIndex(u => u.Email).IsUnique();

        // // -----------------------------
        // // Foreign key relationship
        // // -----------------------------
        // modelBuilder.Entity<CUserSession>()
        //     .HasOne(s => s.User)           // navigation property
        //     .WithMany()                    // no collection in CUser, optional: use .WithMany(u => u.Sessions)
        //     .HasForeignKey(s => s.UserId)  // FK column in UserSessions table
        //     .OnDelete(DeleteBehavior.Cascade);

        // // -----------------------------
        // // Default timestamps at DB level (optional)
        // // -----------------------------
        // modelBuilder.Entity<CUser>().Property(u => u.CreatedAt)
        //     .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");
        // modelBuilder.Entity<CUserSession>().Property(s => s.CreatedAt)
        //     .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");
        // modelBuilder.Entity<CUserSession>().Property(s => s.ExpiresAt)
        //     .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC' + INTERVAL '7 DAYS'");
    }
}