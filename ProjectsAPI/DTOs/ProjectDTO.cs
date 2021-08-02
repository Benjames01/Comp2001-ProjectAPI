using ProjectsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsAPI.DTOs
{
    public class ProjectDTO
    {
        public int Id { get; set; } // Project ID
        public int UserId { get; set; } // Student ID

        public string Title { get; set; } // Project Title

        public string Description { get; set; } // Project description

        public string Year { get; set; }
    }
}
