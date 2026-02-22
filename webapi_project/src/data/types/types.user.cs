using System.Runtime.Serialization;

public class TUser
{
    public required int id { get; set; } //pk

    public required string username { get; set; }

    public required string password { get; set; } //hashed password

    public required E_User_TUserType user_type { get; set; }

    public required string full_name { get; set; }

    public required string email { get; set; }

    public required string mobile { get; set; }

    public required string company { get; set; }

    public DateTime lastLogin = DateTime.UtcNow;

    public DateTime createdAt = DateTime.UtcNow;

}

public enum E_User_TUserType
{
    [EnumMember(Value = "buyer")]
    Buyer,

    [EnumMember(Value = "Seller")]
    Seller,
}