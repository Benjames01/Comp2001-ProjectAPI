using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using ProjectsAPI.Entities;
using ProjectsAPI.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
