using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsAPI.Entities
{
    public class Role
    {
        public int Id { get; set; } // Level of authority
        public string Name { get; set; } // Name of role i.e 'student', 'lecturer'
    }
}
