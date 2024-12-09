using System.ComponentModel.DataAnnotations;

namespace Models.Model;

public class Course
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Mail { get; set; }
    public DateTime Duration { get; set; }
    public string Description { get; set; }
}