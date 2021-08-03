using ProjectsAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace ProjectsAPI.DTOs
{
    public class AccountData
    {
        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
        
        [Required]
        [ProgrammeIdExists]
        public int ProgrammeId { get; set; }
    }
}