using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<CUser> User { get; set; } = null!;
    public DbSet<CUserSession> UserSession { get; set; } = null!;


    // this is where you define your db table constraints
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CUser>().HasKey(item => item.id); //creates primary key on column id
        modelBuilder.Entity<CUserSession>().HasKey(item => item.id); //creates primary key on column id

        // in the type CUserSession you must put this public CUser? User { get; set; } for navigational purposes 
        // in order for EF Core to detect CUser Properties
        // EF Core automatically uses the primary key of CUser, which is CUser.Id, as the referenced column.

        modelBuilder.Entity<CUserSession>()
        .HasOne(item => item.User) // 1️⃣ Each CUserSession "has one" CUser associated with it
        .WithMany()  // 2️⃣ A CUser can have many CUserSessions
        .HasForeignKey(item => item.user_id)
        .OnDelete(DeleteBehavior.Cascade);
    }
}