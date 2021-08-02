using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

        public ProjectsController( ApplicationDbContext context, ILogger<ProjectsController> logger) //repository is injected into constructor
        {
            this.context = context;
            this.logger = logger;
        }

        [HttpPost] // Create a new project
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post([FromBody] Project project)
        {
            context.Add(project);

            await context.SaveChangesAsync();

            return new CreatedAtRouteResult("getProject", new { Id = project.Id }, project);
        }

        [HttpGet("{Id:int}", Name = "getProject")] // [api/projects/{Id}] - get project with {Id}
        public async Task<ActionResult<Project>> Get(int Id)
        {
            var project = await context.Projects.FirstOrDefaultAsync(x => x.Id == Id);


            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        [HttpGet] // [api/projects] - Provides view of all projects for a programme
        //[ResponseCache(Duration = 60)] would be used to cache results to prevent spam
        public async Task<ActionResult<List<Project>>> Get()
        {
            return await context.Projects.AsNoTracking().ToListAsync();
        }

        [HttpPut("{Id:int}")] // [api/projects/{Id}] - Update an existing project with {Id}
        public async Task<ActionResult> Put()
        { 
            await Task.Delay(1);
            return NoContent();
        }

        [HttpDelete("{Id:int}")] // [api/projects/{Id}] - Delete project with {Id}
        public async Task<ActionResult> Delete()
        {
            await Task.Delay(1);
            return NoContent();
        }
    }
}