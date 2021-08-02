﻿using ProjectsAPI.Validations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectsAPI.Entities
{
    public class Project
    {
        public int Id { get; set; } // Project ID

        [Required(ErrorMessage = "A user Id must be provided")]
        [UserIdExists]
        public int UserId { get; set; } // Student ID
        public User User { get; set; }

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