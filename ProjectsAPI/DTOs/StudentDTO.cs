using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsAPI.DTOs
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public int ProgrammeId { get; set; }

        public string UserName { get; set; }
    }
}
