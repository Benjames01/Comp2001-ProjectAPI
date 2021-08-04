using System.ComponentModel.DataAnnotations;

namespace ProjectsAPI.DTOs
{
    public class AccountLoginDTO
    {
        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}