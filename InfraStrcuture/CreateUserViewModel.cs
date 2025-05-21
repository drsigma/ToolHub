using System.ComponentModel.DataAnnotations;

public class CreateUserViewModel
{
    [Required]
    [Display(Name = "Name")]
    public string Name { get; set; }

    public List<User> ExistingUsers { get; set; } = new();
}
