using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;

namespace Models.DTO;

public class UserDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Mail { get; set; }
    [Required]
    public string Password_HK { get; set; }
}