using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectsAPI.Services;


namespace ProjectsAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IRepository repository;

        public UsersController(IRepository repository)
        {
            this.repository = repository;
        }

        public JsonResult IsUserIdValid(int UserId)
        {
            Task<bool> task = Task.Run<bool>(async () => await repository.IsUserIdValid(UserId));
            //bool isValid = await repository.IsUserIdValid(UserId);
            if (!task.Result)
            {
                // User Id doesn't exist in repository
                return Json(false, System.Web.Mvc.JsonRequestBehavior.AllowGet);
            } else
            {
                // User Id exists
                return Json(true, System.Web.Mvc.JsonRequestBehavior.AllowGet);
            }
        }
    }
}