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

        public bool IsEmployer { get; set; } = false;

    }
}
