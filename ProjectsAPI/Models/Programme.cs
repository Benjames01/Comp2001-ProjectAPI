using ProjectsAPI.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectsAPI.Entities
{
    public class Programme
    {
        [Required]
        public int Id { get; set; } // Level of authority

        [Required]
        [StringLength(40)]
        public string Name { get; set; } // Name of programme

        public ICollection<ApplicationUser> Students { get; set; }
    }
}