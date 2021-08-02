using ProjectsAPI.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsAPI.DTOs
{
    public class ProjectCreationDTO
    {
        [Required(ErrorMessage = "A user Id must be provided")]
        [UserIdExists]
        public int UserId { get; set; } // Student ID

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
