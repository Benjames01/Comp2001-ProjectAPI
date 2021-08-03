namespace ProjectsAPI.DTOs
{
    public class ProjectDTO
    {
        public int Id { get; set; } // Project ID
        public int ApplicationUserId { get; set; } // Student ID

        public string Title { get; set; } // Project Title

        public string Description { get; set; } // Project description

        public string Year { get; set; }
    }
}