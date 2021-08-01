using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectsAPI.Services;

namespace ProjectsAPI.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IRepository repository; // Database access

        public ProjectsController(IRepository repository)
        {
            this.repository = repository;
        }

    }
}