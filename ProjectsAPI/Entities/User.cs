using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectsAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        [StringLength(70)]
        public string Name { get; set; }
        [Required]
        public int ProgrammeId { get; set; }
        public Programme Programme { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}