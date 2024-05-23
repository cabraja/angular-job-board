using System.ComponentModel.DataAnnotations;

namespace API.Helpers.DTO.Auth
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

    }

    public class RegisterEmployerDTO : RegisterDTO
    {
        [Required]
        public string? EmployerName { get; set; }
    }
}
