using Microsoft.AspNetCore.Identity;
using ProjectsAPI.Entities;
using ProjectsAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace ProjectsAPI.Infrastructure
{
    public class ApplicationUser : IdentityUser<int>
    {
        [ProgrammeIdExists]
        [Required]
        public int ProgrammeId { get; set; }

        public Programme Programme { get; set; }
    }
}