using System.ComponentModel.DataAnnotations;

namespace Models.Model;

public class UserEntity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Mail { get; set; }
    public long Itmo_Id { get; set; }
    public string Itmo_Password { get; set; }
    public string Password_HK { get; set; }
    public DateTime CreatedAt { get; set; }
}