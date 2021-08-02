using ProjectsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsAPI.Services
{
    public interface IRepository
    {
        Task<List<Project>> GetAllProjects();
        Task<List<User>> GetAllUsers();
        Task<List<Role>> GetAllRoles();
        Task<Role> GetRoleById(int Id);

        Task<bool> IsUserIdValid(int Id);
        Task<bool> IsRoleIdValid(int Id);
    }
}
