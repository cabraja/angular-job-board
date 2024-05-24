using System.ComponentModel.DataAnnotations;

namespace API.Helpers.DTO.Auth
{
    public abstract class RegisterBaseDTO
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

    }

    public class RegisterDTO : RegisterBaseDTO
    {
        [Required]
        public string? Username { get; set; }
    }

    public class RegisterEmployerDTO : RegisterBaseDTO
    {
        [Required]
        public string? EmployerName { get; set; }
    }
}
