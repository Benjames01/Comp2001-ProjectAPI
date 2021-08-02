using ProjectsAPI.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsAPI.DTOs
{
    public class ProjectUpdateDTO
    {
        [StringLength(30)]
        public string Title { get; set; } // Project Title

        [StringLength(500)]
        public string Description { get; set; } // Project description

        [StringLength(30)]
        public string Year { get; set; }
    }
}
