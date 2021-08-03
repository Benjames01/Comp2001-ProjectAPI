using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectsAPI.Entities;
using ProjectsAPI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace ProjectsAPI
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        internal Task<ActionResult<List<Programme>>> GetAllProgrammes()
        {
            throw new NotImplementedException();
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<ApplicationUser> Students { get; set; }

        public async Task<Project> GetProjectById(int Id)
        {
            var project = await Projects.FirstAsync(x => x.Id == Id);
            return project;
        }

        public async Task<bool> IsStudentIdValid(int Id)
        {
            var isValid = await Students.AnyAsync(x => x.Id == Id);
            return isValid;
        }

        public async Task<bool> IsProgrammeIdValid(int Id)
        {
            var isValid = await Programmes.AnyAsync(x => x.Id == Id);
            return isValid;
        }

        public async Task<bool> IsProjectIdValid(int Id)
        {
            var isValid = await Projects.AnyAsync(x => x.Id == Id);
            return isValid;
        }
    }
}