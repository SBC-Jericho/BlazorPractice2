using System.ComponentModel.DataAnnotations;

namespace BlazorPlayGround.Shared.Models
{
    public class Team
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please, give this team a name: ")]
        public string Name { get; set; } = string.Empty;
    }
}
