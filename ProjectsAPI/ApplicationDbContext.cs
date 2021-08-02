﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsAPI
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<User> Users { get; set; }

        public async Task<Project> GetProjectById(int Id)
        {
            var project = await Projects.FirstAsync(x => x.Id == Id);
            return project;
        }

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

        public async Task<bool> IsProjectIdValid(int Id)
        {
            var isValid = await Projects.AnyAsync(x => x.Id == Id);
            return isValid;
        }
    }
}
