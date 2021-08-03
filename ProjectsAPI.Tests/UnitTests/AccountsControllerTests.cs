using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProjectsAPI.Controllers;
using ProjectsAPI.Entities;
using ProjectsAPI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ProjectsAPI.Tests.UnitTests
{
    [TestClass]
    public class AccountsControllerTests : BaseTest
    {
        [TestMethod]
        public async Task GetAllStudents()
        {
            // Preparation
            TestPrep prep = new TestPrep();
            prep.Context.Programmes.Add(new Programme { Name = "Programme 1" });
            prep.Context.Programmes.Add(new Programme { Name = "Programme 2" });
           
            prep.Context.Students.Add(new ApplicationUser { UserName = "test@test.com", Email = "test@test.com", ProgrammeId = 1, Id = 1 });
            prep.Context.Students.Add(new ApplicationUser { UserName = "test1@test.com", Email = "test1@test.com", ProgrammeId = 2, Id = 2 });
            prep.Context.Students.Add(new ApplicationUser { UserName = "test2@test.com", Email = "test2@test.com", ProgrammeId = 2, Id = 3 });
            prep.Context.Students.Add(new ApplicationUser { UserName = "test3@test.com", Email = "test3@test.com", ProgrammeId = 3, Id = 4 });
            prep.Context.Students.Add(new ApplicationUser { UserName = "test4@test.com", Email = "test4@test.com", ProgrammeId = 3, Id = 5 });
            
            prep.Context.SaveChanges();

            var context = BuildContext(prep.DatabaseName); // Check from new context

            // Testing
            var controller = new AccountsController(prep.UserManager.Object, prep.SigninManager.Object,
               context, prep.Mapper);
            var response = await controller.Get();

            // Verification
            var students = response.Value;
            Assert.AreEqual(5, students.Count);
        }

        [TestMethod]
        public async Task GetStudentProjectsByIdDoesNotExist()
        {
            // Preparation
            TestPrep prep = new TestPrep();

            // Testing
            var controller = new AccountsController(prep.UserManager.Object, prep.SigninManager.Object,
                prep.Context, prep.Mapper);
            var response = await controller.Get(1);

            // Verification
            var result = response.Result as StatusCodeResult;
            Assert.AreEqual(404, result.StatusCode);
            
        }

        [TestMethod]
        public async Task GetStudentProjectsByIdExists()
        {
            // Preparation
            TestPrep prep = new TestPrep();

            prep.Context.Programmes.Add(new Programme { Name = "Programme 1" });
            prep.Context.Students.Add(new ApplicationUser { UserName = "test@test.com", Email = "test@test.com", ProgrammeId = 1, Id = 1 });
            prep.Context.Projects.Add(new Project { ApplicationUserId = 1, Id = 1, Description = "string", Title = "string", Year = "string" });

            prep.Context.SaveChanges();

            var context = BuildContext(prep.DatabaseName);

            // Testing
            var controller = new AccountsController(prep.UserManager.Object, prep.SigninManager.Object,
                context, prep.Mapper);

            var id = 1;
            var response = await controller.Get(1);
            var result = response.Value;

            // Verification
            Assert.AreEqual(id, result[0].ApplicationUserId);

        }

        [TestMethod]
        public async Task CreateAccount()
        {
            // Preparation
            TestPrep prep = new TestPrep();
            List<ApplicationUser> ls = new List<ApplicationUser>();
            var newAccount = new ApplicationUser { UserName = "test@test.com", Email = "test@test.com", ProgrammeId = 1, Id = 1 };
            var password = "Test123!";
            prep.UserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<ApplicationUser, string>((x, y) => ls.Add(x));

            // Testing
            var controller = new AccountsController(prep.UserManager.Object, prep.SigninManager.Object,
                prep.Context, prep.Mapper);
            var result = await prep.UserManager.Object.CreateAsync(newAccount, password);

            // Verification
            Assert.AreEqual(1, ls.Count);
        }


        private static Mock<UserManager<ApplicationUser>> MockUserManager<ApplicationUser>() where ApplicationUser : class
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            var userManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            userManager.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
            userManager.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());
                 
            return userManager;
        }

        private static Mock<SignInManager<ApplicationUser>> MockSigninManager<ApplicationUser>(UserManager<ApplicationUser> userManager) where ApplicationUser : class
        {
            var httpContext = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
            Mock<SignInManager<ApplicationUser>> signInManager = new Mock<SignInManager<ApplicationUser>>(userManager,
                           httpContext.Object, userPrincipalFactory.Object, null, null, null, null);
            return signInManager;
        }


        private class TestPrep
        {
            public string DatabaseName { get; set; }
            public ApplicationDbContext Context { get; set; }

            public Mock<UserManager<ApplicationUser>> UserManager { get; set; }
            public Mock<SignInManager<ApplicationUser>> SigninManager { get; set; }
            public IMapper Mapper { get; set; }

            public TestPrep()
            {
                DatabaseName = Guid.NewGuid().ToString();
                Context = BuildContext(DatabaseName);
                Mapper = BuildMap();
                UserManager = MockUserManager<ApplicationUser>();
                SigninManager = MockSigninManager(UserManager.Object);
            }
        }
    }
}
