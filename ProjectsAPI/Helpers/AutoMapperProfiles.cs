using AutoMapper;
using ProjectsAPI.DTOs;
using ProjectsAPI.Entities;
using ProjectsAPI.Infrastructure;

namespace ProjectsAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Project, ProjectDTO>().ReverseMap();
            CreateMap<ProjectCreationDTO, Project>();
            CreateMap<ProjectUpdateDTO, Project>();

            CreateMap<ApplicationUser, StudentDTO>().ReverseMap();

            CreateMap<AccountLoginDTO, AccountData>();
        }
    }
}