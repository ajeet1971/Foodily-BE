using System.ComponentModel.DataAnnotations;

namespace Foodily.ViewModels
{
    public class UserVM
    {
        [Key]
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Bio { get; set; } 

    }
}
