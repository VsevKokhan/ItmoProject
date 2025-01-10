using System.ComponentModel.DataAnnotations;

namespace Models.DTO;

public class UserForLogin
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Pass { get; set; }
}