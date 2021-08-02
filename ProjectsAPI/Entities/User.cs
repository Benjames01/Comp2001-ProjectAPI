using System.ComponentModel.DataAnnotations;

namespace ProjectsAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        [StringLength(70)]
        public string Name { get; set; }
        [Required]
        public int ProgrammeID { get; set; }
    }
}