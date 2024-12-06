using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Model;

namespace Models;

public class User_Course
{
    [Key, Column(Order = 0)]
    public int User_Id { get; set; }

    [Key, Column(Order = 1)]
    public int Course_Id { get; set; }

    public int Progress { get; set; }

    [ForeignKey(nameof(User_Id))]
    public UserEntity User { get; set; }

    [ForeignKey(nameof(Course_Id))]
    public Course Course { get; set; }
}
