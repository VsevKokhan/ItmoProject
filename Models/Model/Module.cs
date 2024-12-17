using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Model;

public class Module
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Video_Name { get; set; }
    public int Course_Id { get; set; }
    [ForeignKey(nameof(Course_Id))]
    public Course Course { get; set; }
}