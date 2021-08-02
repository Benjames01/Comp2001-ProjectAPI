using Microsoft.AspNetCore.Mvc;
using ProjectsAPI.Entities;
using ProjectsAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectsAPI.Controllers
{
    [Route("api/programmes")]
    public class ProgrammesController : ControllerBase
    {
        private readonly IRepository repository;

        public ProgrammesController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet] // api/programmes
        [HttpGet("/allprogrammes")]
        public async Task<ActionResult<List<Programme>>> Get()
        {
            return await repository.GetAllProgrammes();
        }

        [HttpGet("{Id:int}")] // api/programmes/{id} -  can pass value in {id}
        public async Task<ActionResult<Programme>> Get(int Id)
        {
            var programme = repository.GetProgrammeById(Id);

            if (programme == null)
            {
                return NotFound();
            }

            return await programme;
        }

        [HttpPost]
        public ActionResult Post()
        {
            return NoContent();
        }

        [HttpPut]
        public ActionResult Put()
        {
            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete()
        {
            return NoContent();
        }
    }
}