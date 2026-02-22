public class CUserSession
{
    public required int id { get; set; } //pk

    public required int user_id { get; set; } //fk

    public required string token { get; set; }

    public required string device { get; set; }

    public required string location { get; set; }

    public DateTime created_at = DateTime.UtcNow;

    public DateTime expires_at = DateTime.UtcNow.AddDays(7);
    

    // For Foreign Key purposes EF Core Navigation property (optional but recommended)
    public CUser? User { get; set; }
}

