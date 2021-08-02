using Microsoft.EntityFrameworkCore;
using ProjectsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<User> Users { get; set; }

        public async Task<bool> IsUserIdValid(int Id)
        {
            var isValid = await Users.AnyAsync(x => x.Id == Id);
            return isValid;  
        }

        public async Task<bool> IsProgrammeIdValid(int Id)
        {
            var isValid = await Programmes.AnyAsync(x => x.Id == Id);
            return isValid;
        }
    }
}
