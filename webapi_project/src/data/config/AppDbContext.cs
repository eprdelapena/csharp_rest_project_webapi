using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    private readonly string? _ConnectionString = null;
    public DbSet<CUser> User { get; set; } = null!;
    public DbSet<CUserSession> UserSession { get; set; } = null!;

    // entity framework must be public and not a static so EF Core can call it and dont pass parameter strings
    public AppDbContext()
    {
        string ConnectionString = "";
        this._ConnectionString = ConnectionString;
    }

    //That method tells EF Core how to connect to your PostgreSQL database.
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (this._ConnectionString != null)
        {
            string DbConnectionUrl = this._ConnectionString;
            optionsBuilder.UseNpgsql(DbConnectionUrl);
            return;
        }

        return;
    }

    // this is where you define your db table constraints
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // -----------------------------
        // Table names
        // -----------------------------
        // sets the table and columns and table name for the postgreSQL
        // /types/User.cs
        modelBuilder.Entity<CUser>().ToTable("user");
        // /types/UserSession.cs
        modelBuilder.Entity<CUserSession>().ToTable("user_session");

        // -----------------------------
        // Primary Keys
        // -----------------------------
        // sets the primary key for the postgreSQL
        modelBuilder.Entity<CUser>().HasKey(user => user.id);
        modelBuilder.Entity<CUserSession>().HasKey(session => session.id);

        // // -----------------------------
        // // Column constraints
        // // -----------------------------

        // EF Core automatically treats non-nullable reference types (string) and value types (int, DateTime) as required/NOT NULL in the database.
        // CUser Table
        modelBuilder.Entity<CUser>().Property(user => user.username).HasMaxLength(50);

        // By default enums are save as integers on the database if you want to save it as string you must convert the enum back to string via hasConversion
        modelBuilder.Entity<CUser>().Property(user => user.user_type).HasConversion<string>();
        modelBuilder.Entity<CUser>().Property(user => user.full_name).HasMaxLength(200);
        modelBuilder.Entity<CUser>().Property(user => user.mobile).HasMaxLength(50);
        modelBuilder.Entity<CUser>().Property(user => user.company).HasMaxLength(200);

        // CUserSession
        //converts string (default: varchar) into text type
        modelBuilder.Entity<CUserSession>().Property(session => session.token).HasColumnType("text");
        modelBuilder.Entity<CUserSession>().Property(session => session.device).HasColumnType("text");
        modelBuilder.Entity<CUserSession>().Property(session => session.location).HasColumnType("text");

        // converts string type to jsonbtype
        // modelBuilder.Entity<CUserSession>()
        //     .Property(s => s.Grades)
        //     .HasColumnType("jsonb")
        //     .HasConversion(
        //         v => System.Text.Json.JsonSerializer.Serialize(v, null),
        //         v => System.Text.Json.JsonSerializer.Deserialize<Grades>(v, null)!
        //     );
        // modelBuilder.Entity<CUser>().Property(u => u.Username).HasMaxLength(100).IsRequired();
        // modelBuilder.Entity<CUser>().Property(u => u.Email).HasMaxLength(150).IsRequired();
        // modelBuilder.Entity<CUserSession>().Property(s => s.Token).HasMaxLength(512).IsRequired();



        // // -----------------------------
        // // Unique indexes
        // // -----------------------------
        modelBuilder.Entity<CUser>().HasIndex(user => user.username).IsUnique();
        modelBuilder.Entity<CUser>().HasIndex(user => user.email).IsUnique();
        modelBuilder.Entity<CUserSession>().HasIndex(session => session.token).IsUnique();
        // modelBuilder.Entity<CUser>().HasIndex(u => u.Username).IsUnique();
        // modelBuilder.Entity<CUser>().HasIndex(u => u.Email).IsUnique();


        // // -----------------------------
        // // Foreign key relationship
        // // -----------------------------
        modelBuilder.Entity<CUserSession>()
        .HasOne(session => session.User) // Navigation property in dependent
        .WithMany(user => user.UserSessions) // Navigation property in principal
        .HasForeignKey(session => session.user_id) // FK in dependent
        .HasPrincipalKey(user => user.id) // PK/unique column in principal explicitly
        .OnDelete(DeleteBehavior.Cascade); // Optional cascade delete

        // modelBuilder.Entity<CUserSession>()
        //     .HasOne(s => s.User)           // navigation property
        //     .WithMany()                    // no collection in CUser, optional: use .WithMany(u => u.Sessions)
        //     .HasForeignKey(s => s.UserId)  // FK column in UserSessions table
        //     .OnDelete(DeleteBehavior.Cascade);

        // // -----------------------------
        // // Default timestamps at DB level (optional)
        // // -----------------------------
        modelBuilder.Entity<CUser>().Property(user => user.created_at)
            .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");
        modelBuilder.Entity<CUserSession>().Property(session => session.created_at)
            .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");
        modelBuilder.Entity<CUserSession>().Property(session => session.expires_at)
            .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC' + INTERVAL '7 DAYS'");
    }
}