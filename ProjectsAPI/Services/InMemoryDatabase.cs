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

        public InMemoryDatabase()
        {
            _roles = new List<Role>()
            {
                new Role(){Id = 1, Name = "Student"},
                new Role(){Id = 2, Name = "Lecturer"}
            };
        }

        public async Task<List<Role>> GetAllRoles()
        {
            await Task.Delay(1); // simulated delay for a DB connection
            return _roles;
        }

        public async Task<Role> GetRoleById(int Id)
        {
            await Task.Delay(1); // simulated delay for a DB connection
            return _roles.FirstOrDefault(x => x.Id == Id);
        }
    }
}
