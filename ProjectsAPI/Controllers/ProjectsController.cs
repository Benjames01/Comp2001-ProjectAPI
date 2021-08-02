using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectsAPI.Entities;
using ProjectsAPI.Services;

namespace ProjectsAPI.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IRepository repository; // Database access

        public ProjectsController(IRepository repository) //repository is injected into constructor
        {
            this.repository = repository;
        }


        [HttpPost] // Create a new project
        public async Task<ActionResult> Post([FromBody] Project project)
        {
            await Task.Delay(1);
            return NoContent();
        }

        [HttpGet] // Provides view of all projects for a programme
        public async Task<ActionResult> Get()
        {
            await Task.Delay(1);
            return NoContent();
        }

        [HttpPut("{Id:int}")] // api/projects/{id} - Update an existing project
        public async Task<ActionResult> Put()
        {
            await Task.Delay(1);
            return NoContent();
        }

        [HttpDelete("{Id:int}")] //api/projects/{id} - Delete project
        public async Task<ActionResult> Delete()
        {
            await Task.Delay(1);
            return NoContent();
        }


    }
}