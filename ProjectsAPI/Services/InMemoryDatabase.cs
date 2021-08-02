using Microsoft.Extensions.Logging;
using ProjectsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsAPI.Services
{
    /**
     * Used for testing before connected to database
     */
    public class InMemoryDatabase : IRepository
    {
        private List<Role> _roles;
        private List<User> _users;
        private List<Project> _projects;

        public InMemoryDatabase(ILogger<InMemoryDatabase> logger)
        {
            _roles = new List<Role>()
            {
                new Role(){Id = 1, Name = "Student"},
                new Role(){Id = 2, Name = "Lecturer"}
            };

            _users = new List<User>()
            {
                new User{Id = 0, Name = "Student 0", RoleID = 1},
                new User{Id = 1, Name = "Student 1", RoleID = 1},
                new User{Id = 2, Name = "Student 2", RoleID = 1},
                new User{Id = 3, Name = "Lecturer 0", RoleID = 2}
            };

            _projects = new List<Project>()
            {
                new Project{Id = 0, UserId = 0, Title = "title 0", Description = "Description here", Year="2020"},
                new Project{Id = 1, UserId = 0, Title = "title 1", Description = "this is my 2nd project", Year="2021"},
                new Project{Id = 2, UserId = 1, Title = "First year!", Description = "this is my first project", Year="2020"},
                new Project{Id = 3, UserId = 1, Title = "Second year", Description = "this is my second year project", Year="2021"},
                new Project{Id = 4, UserId = 2, Title = "My only project", Description = "I have 1 project", Year="2020"},
            };
        }

        public async Task<List<Project>> GetAllProjects()
        {
            await Task.Delay(1); // simulated delay for a DB connection
            return _projects;
        }

        public async Task<List<Role>> GetAllRoles()
        {
            await Task.Delay(1); // simulated delay for a DB connection
            return _roles;
        }

        public async Task<List<User>> GetAllUsers()
        {
            await Task.Delay(1); // simulated delay for a DB connection
            return _users;
        }

        public async Task<Role> GetRoleById(int Id)
        {
            await Task.Delay(1); // simulated delay for a DB connection
            return _roles.FirstOrDefault(x => x.Id == Id);
        }

        public async Task<bool> IsRoleIdValid(int Id)
        {
            await Task.Delay(1);
            return GetRoleById(Id) != null;
        }

        public async Task<bool> IsUserIdValid(int Id)
        {
            await Task.Delay(1);
            if (Id > 10)
            {
                return true;
            }

            return false;
        }
    }
}
