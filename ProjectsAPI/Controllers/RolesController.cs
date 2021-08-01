using Microsoft.AspNetCore.Mvc;
using ProjectsAPI.Entities;
using ProjectsAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsAPI.Controllers
{
    [Route("api/roles")]
    public class RolesController : ControllerBase
    {

        private readonly IRepository repository;

        public RolesController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet] // api/roles
        [HttpGet("/allroles")]
        public async Task<ActionResult<List<Role>>> Get()
        {
            return await repository.GetAllRoles();
        }

        [HttpGet("{Id:int}")] // api/roles/{id} -  can pass value in {id}
        public async Task<ActionResult<Role>> Get(int Id)
        {
            var role = repository.GetRoleById(Id);

            if (role == null)
            {
                return NotFound();
            }

            return await role; 
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
