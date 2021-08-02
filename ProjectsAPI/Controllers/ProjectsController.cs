using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectsAPI.DTOs;
using ProjectsAPI.Entities;
using ProjectsAPI.Services;
using System.Collections.Generic;
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

        public ProjectsController( ApplicationDbContext context,
            ILogger<ProjectsController> logger,
            IMapper mapper) //repository is injected into constructor
        {
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpPost] // Create a new project
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post([FromBody] ProjectCreationDTO projectCreation)
        {
            var project = mapper.Map<Project>(projectCreation);

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
            var projects = await context.Projects.AsNoTracking().ToListAsync();
            var projectsDTOs = mapper.Map<List<ProjectDTO>>(projects);

            return projectsDTOs;
        }

        [HttpPut("{Id:int}")] // [api/projects/{Id}] - Update an existing project with {Id}
        public async Task<ActionResult> Put(int Id, [FromBody] ProjectUpdateDTO projectUpdate)
        {

            Project original = await context.Projects.AsNoTracking().FirstAsync(x => x.Id == Id);
            if (original == null)
            {
                return NotFound();  // status code: 404
            }

            var project = mapper.Map<Project>(projectUpdate);
            project.Id = Id;
            project.UserId = original.UserId;

            // partial updates until patch
            if (project.Title == null)
            {
                project.Title = original.Title;
            }

            if (project.Description == null)
            {
                project.Description = original.Description;
            }

            if (project.Year == null)
            {
                project.Year = original.Year;
            }

            context.Entry(project).State = EntityState.Modified;

            await context.SaveChangesAsync();

            return NoContent(); // status code: 204
        }

        [HttpDelete("{Id:int}")] // [api/projects/{Id}] - Delete project with {Id}
        public async Task<ActionResult> Delete(int Id)
        {
            var exists = await context.Projects.AnyAsync(x => x.Id == Id);
            if(!exists)
            {
                return NotFound(); // status code: 404
            }

            context.Remove(new Project() { Id = Id });
            await context.SaveChangesAsync();

            return NoContent(); // status code: 204
        }
    }
}