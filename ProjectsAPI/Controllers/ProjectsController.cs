using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectsAPI.DTOs;
using ProjectsAPI.Entities;
using ProjectsAPI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectsAPI.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ApplicationDbContext context; // Database access
        private readonly ILogger<ProjectsController> logger;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ProjectsController(ApplicationDbContext context,
            ILogger<ProjectsController> logger,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor) //repository is injected into constructor
        {
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpPost] // Create a new project
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post([FromBody] ProjectCreationDTO projectCreation)
        {
            int id = (await GetStudent()).Id; // Get the logged in user's id
            var project = mapper.Map<Project>(projectCreation);
            project.ApplicationUserId = id;

            context.Add(project);
            await context.SaveChangesAsync();
            var projectDTO = mapper.Map<ProjectDTO>(project);

            return new CreatedAtRouteResult("getProject", new { Id = projectDTO.Id }, projectDTO);
        }

        [HttpGet("{Id:int}", Name = "getProject")] // [api/projects/{Id}] - get project with {Id}
        public async Task<ActionResult<ProjectDTO>> Get(int Id)
        {
            var project = await context.Projects.FirstOrDefaultAsync(x => x.Id == Id);

            if (project == null)
            {
                return NotFound();  // status code: 404
            }

            var projectDTO = mapper.Map<ProjectDTO>(project);

            return projectDTO;
        }

        [HttpGet] // [api/projects] - Provides view of all projects for a programme
        //[ResponseCache(Duration = 60)] would be used to cache results to prevent spam
        public async Task<ActionResult<List<ProjectDTO>>> Get()
        {
            logger.LogInformation("Get projects executed.");
            var projects = await context.Projects.AsNoTracking().ToListAsync();
            var projectsDTOs = mapper.Map<List<ProjectDTO>>(projects);

            return projectsDTOs;
        }

        /*
         * Endpoint: [api/projects/{Id}]
         * Update an existing project with {Id}
         * Can only be done by a logged in user
         */
        [HttpPut("{Id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(int Id, [FromBody] ProjectUpdateDTO projectUpdate)
        {
           
            // Only update if this project belongs to logged in user
            if(Id != (await GetStudent()).Id)
            {
                return BadRequest();
            }

            Project original = await context.Projects.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
            if (original == null) { return NotFound(); } // status code: 404

            var project = mapper.Map<Project>(projectUpdate);
            project.Id = Id;
            project.ApplicationUserId = original.ApplicationUserId;

            if (string.IsNullOrEmpty(project.Title))
            {
                logger.LogInformation("Using original title for project.");
                project.Title = original.Title;
            }
              
            if (string.IsNullOrEmpty(project.Description))
            {
                logger.LogInformation("Using original description for project.");
                project.Description = original.Description;
            }

            if (string.IsNullOrEmpty(project.Year))
            {
                logger.LogInformation("Using original year for project.");
                project.Year = original.Year;
            }

            context.Entry(project).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent(); // status code: 204
        }

        /*
         * Endpoint:[api/projects/{Id}]
         * Delete project with {Id}
         * Can only be done by a logged in user
        */
        [HttpDelete("{Id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(int Id)
        {          
            var project = await context.Projects.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
            
            
            if (project == null)
            {
                return NotFound(); // status code: 404
            }

            // Only delete if this project belongs to logged in user
            if (project.ApplicationUserId != (await GetStudent()).Id)
            {
                return BadRequest();
            }


            context.Remove(new Project() { Id = Id });
            await context.SaveChangesAsync();

            return NoContent(); // status code: 204
        }

        private async Task<ApplicationUser> GetStudent()
        {
            var email = User.FindFirst(ClaimTypes.Email).Value;
            var user = await context.Students.FirstOrDefaultAsync(x => x.Email == email);

            return user;
        }
    }
}