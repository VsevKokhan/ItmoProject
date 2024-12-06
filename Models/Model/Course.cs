using System.ComponentModel.DataAnnotations;

namespace Models.Model;

public class Course
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Mail { get; set; }
    [Required]
    public DateTime Duration { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Link_For_Source { get; set; }
}