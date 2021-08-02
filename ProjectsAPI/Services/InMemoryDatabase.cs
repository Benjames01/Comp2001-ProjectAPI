using Microsoft.Extensions.Logging;
using ProjectsAPI.Entities;
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
        private List<Programme> _programmes;
        private List<User> _users;
        private List<Project> _projects;

        public InMemoryDatabase(ILogger<InMemoryDatabase> logger)
        {
            _programmes = new List<Programme>()
            {
                new Programme(){Id = 1, Name = "Student"},
                new Programme(){Id = 2, Name = "Lecturer"}
            };

            _users = new List<User>()
            {
                new User{Id = 1, Name = "Student 0", ProgrammeId = 1},
                new User{Id = 2, Name = "Student 1", ProgrammeId = 1},
                new User{Id = 3, Name = "Student 2", ProgrammeId = 1},
                new User{Id = 4, Name = "Lecturer 0", ProgrammeId = 2}
            };

            _projects = new List<Project>()
            {
                new Project{Id = 1, UserId = 1, Title = "title 0", Description = "Description here", Year="2020"},
                new Project{Id = 2, UserId = 1, Title = "title 1", Description = "this is my 2nd project", Year="2021"},
                new Project{Id = 3, UserId = 2, Title = "First year!", Description = "this is my first project", Year="2020"},
                new Project{Id = 4, UserId = 2, Title = "Second year", Description = "this is my second year project", Year="2021"},
                new Project{Id = 5, UserId = 3, Title = "My only project", Description = "I have 1 project", Year="2020"},
            };
        }

        public async Task<List<Project>> GetAllProjects()
        {
            await Task.Delay(1); // simulated delay for a DB connection
            return _projects;
        }

        public async Task<List<Programme>> GetAllProgrammes()
        {
            await Task.Delay(1); // simulated delay for a DB connection
            return _programmes;
        }

        public async Task<List<User>> GetAllUsers()
        {
            await Task.Delay(1); // simulated delay for a DB connection
            return _users;
        }

        public async Task<Programme> GetProgrammeById(int Id)
        {
            await Task.Delay(1); // simulated delay for a DB connection
            return _programmes.FirstOrDefault(x => x.Id == Id);
        }

        public async Task<Project> GetProjectById(int Id)
        {
            await Task.Delay(1); // simulated delay for a DB connection
            return _projects.FirstOrDefault(x => x.Id == Id);
        }

        public async Task<bool> IsProgrammeIdValid(int Id)
        {
            await Task.Delay(1);
            return GetProgrammeById(Id) != null;
        }

        public async Task<bool> IsUserIdValid(int Id)
        {
            await Task.Delay(1);
            return _users.FirstOrDefault(x => x.Id == Id) != null;
        }

        public async Task AddProject(Project project)
        {
            project.Id = _projects.Max(x => x.Id) + 1;
            _projects.Add(project);
            return;
        }
    }
}