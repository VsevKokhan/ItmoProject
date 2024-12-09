using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Model;

public class User_Modules
{
    [Key, Column(Order = 0)]
    public int User_Id { get; set; }

    [Key, Column(Order = 1)]
    public int Module_Id { get; set; }
    [DefaultValue(false)]
    public bool Is_Passed { get; set; }
    [ForeignKey(nameof(User_Id))]
    public UserEntity User { get; set; }

    [ForeignKey(nameof(Module_Id))]
    public Module Course { get; set; }
}