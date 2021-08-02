using ProjectsAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectsAPI.Services
{
    public interface IRepository
    {
        Task<List<Project>> GetAllProjects();

        Task<List<User>> GetAllUsers();

        Task<List<Programme>> GetAllProgrammes();

        Task<Programme> GetProgrammeById(int Id);

        Task<bool> IsUserIdValid(int Id);

        Task<bool> IsProgrammeIdValid(int Id);

        Task AddProject(Project project);

        Task<Project> GetProjectById(int Id);
    }
}