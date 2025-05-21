using System.ComponentModel.DataAnnotations;

namespace ToolHub.InfraStrcuture
{
    public class CreateToolViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public List<Tool> ExistingTools { get; set; } = new();
    }
}
