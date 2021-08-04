using System.ComponentModel.DataAnnotations;

namespace ProjectsAPI.DTOs
{
    public class ProjectCreationDTO
    {
        [Required(ErrorMessage = "Title is required with max length of 30 characters")]
        [StringLength(30)]
        public string Title { get; set; } // Project Title

        [Required(ErrorMessage = "Description is required with max length of 500 characters")]
        [StringLength(500)]
        public string Description { get; set; } // Project description

        [Required]
        [StringLength(30)]
        public string Year { get; set; }
    }
}