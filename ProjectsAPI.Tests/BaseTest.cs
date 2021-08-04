using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectsAPI.Helpers;

namespace ProjectsAPI.Tests
{
    public class BaseTest
    {
        protected static ApplicationDbContext BuildContext(string databaseName)
        {
            // Use an in memory database rather than the live database
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName).Options;

            var dbContext = new ApplicationDbContext(options);
            return dbContext;
        }

        protected static IMapper BuildMap()
        {
            var config = new MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapperProfiles());
            });

            return config.CreateMapper();
        }
    }
}