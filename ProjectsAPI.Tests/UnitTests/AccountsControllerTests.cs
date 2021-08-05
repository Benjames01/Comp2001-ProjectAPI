using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProjectsAPI.Controllers;
using ProjectsAPI.DTOs;
using ProjectsAPI.Entities;
using ProjectsAPI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


// Credits: MOQ - mocking framework
// Credits: Microsoft - EntityFrameworkCore
namespace ProjectsAPI.Tests.UnitTests
{
    [TestClass]
    public class AccountsControllerTests : BaseTest
    {

        [TestMethod]
        public async Task GetStudentProjectsByIdDoesNotExist()
        {
            // Preparation
            var databaseName = Guid.NewGuid().ToString();
            var controller = BuildTestPrep(databaseName).Controller;

            // Testing
            var response = await controller.Get(1);

            // Verification
            var result = response.Result as StatusCodeResult;
            Assert.AreEqual(404, result.StatusCode);

        }

        [TestMethod]
        public async Task GetStudentProjectsByIdExists()
        {
            // Preparation
            var databaseName = Guid.NewGuid().ToString();
            var prep = BuildTestPrep(databaseName);

            prep.Context.Students.Add(new ApplicationUser { UserName = "test@test.com", Email = "test@test.com", ProgrammeId = 1, Id = 1 });
            prep.Context.Projects.Add(new Project { ApplicationUserId = 1, Id = 1, Description = "string", Title = "string", Year = "string" });

            prep.Context.SaveChanges();
            var context = BuildContext(databaseName);

            // Testing
            var controller = BuildTestPrep(databaseName).Controller;

            var id = 1;
            var response = await controller.Get(1);
            var result = response.Value;

            // Verification
            Assert.AreEqual(id, result[0].ApplicationUserId);

        }

        private TestPrep BuildTestPrep(string databaseName)
        {
            var context = BuildContext(databaseName);
            var userManager = MockUserManager<ApplicationUser>();
            var mapper = BuildMap();

            var signinManager = MockSigninManager<ApplicationUser>(userManager.Object);

            var configSettings = new Dictionary<string, string>
            {
                {"JWT:Key", "THISISASUPERSECRETKEYFORENCRYPTIONDONTTELLANYONE" }
            };

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(configSettings)
                .Build();

            var programme = new Programme() { Id = 1, Name = "First year" };
            var programme1 = new Programme() { Id = 2, Name = "Second year" };
            var programme2 = new Programme() { Id = 3, Name = "Third year" };

            context.Add(programme);
            context.Add(programme1);
            context.Add(programme2);

            return new TestPrep() { Controller = new AccountsController(userManager.Object, signinManager.Object,
                config, context, mapper), Context = context};
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
            public ApplicationDbContext Context { get; set; }
            public AccountsController Controller { get; set; }
        }

    }
}