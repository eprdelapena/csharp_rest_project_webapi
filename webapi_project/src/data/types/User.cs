using System.Runtime.Serialization;

namespace Schema.DbTable;
public class CUser
{
    public required int id { get; set; } //pk

    public required string username { get; set; }

    public required string password { get; set; } //hashed password

    public required EUserType user_type { get; set; }

    public required string full_name { get; set; }

    public required string email { get; set; }

    public required string mobile { get; set; }

    public required string company { get; set; }

    public DateTime last_login { get; set; } = DateTime.UtcNow;

    public DateTime created_at { get; set; } = DateTime.UtcNow;

    //for with many relationships must be I Collection
    public ICollection<CUserSession> UserSessions { get; set; } = new List<CUserSession>();

}

public enum EUserType
{
    [EnumMember(Value = "buyer")]
    Buyer,

    [EnumMember(Value = "seller")]
    Seller,
}