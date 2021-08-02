using AutoMapper;
using ProjectsAPI.DTOs;
using ProjectsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Project, ProjectDTO>().ReverseMap();
            CreateMap<ProjectCreationDTO, Project>();
            CreateMap<ProjectUpdateDTO, Project>();
        }
    }
}
