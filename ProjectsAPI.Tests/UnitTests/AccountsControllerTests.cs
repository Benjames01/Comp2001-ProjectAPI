using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProjectsAPI.Controllers;
using ProjectsAPI.DTOs;
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


       


        public static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());
            return mgr;
        }

        public static Mock<RoleManager<TRole>> MockRoleManager<TRole>(IRoleStore<TRole> store = null) where TRole : class
        {
            store = store ?? new Mock<IRoleStore<TRole>>().Object;
            var roles = new List<IRoleValidator<TRole>>();
            roles.Add(new RoleValidator<TRole>());
            return new Mock<RoleManager<TRole>>(store, roles, new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(), null);
        }

        public static UserManager<TUser> TestUserManager<TUser>(IUserStore<TUser> store = null) where TUser : class
        {
            store = store ?? new Mock<IUserStore<TUser>>().Object;
            var options = new Mock<IOptions<IdentityOptions>>();
            var idOptions = new IdentityOptions();
            idOptions.Lockout.AllowedForNewUsers = false;
            options.Setup(o => o.Value).Returns(idOptions);
            var userValidators = new List<IUserValidator<TUser>>();
            var validator = new Mock<IUserValidator<TUser>>();
            userValidators.Add(validator.Object);
            var pwdValidators = new List<PasswordValidator<TUser>>();
            pwdValidators.Add(new PasswordValidator<TUser>());
            var userManager = new UserManager<TUser>(store, options.Object, new PasswordHasher<TUser>(),
                userValidators, pwdValidators, new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(), null,
                new Mock<ILogger<UserManager<TUser>>>().Object);
            validator.Setup(v => v.ValidateAsync(userManager, It.IsAny<TUser>()))
                .Returns(Task.FromResult(IdentityResult.Success)).Verifiable();
            return userManager;
        }

        public static RoleManager<TRole> TestRoleManager<TRole>(IRoleStore<TRole> store = null) where TRole : class
        {
            store = store ?? new Mock<IRoleStore<TRole>>().Object;
            var roles = new List<IRoleValidator<TRole>>();
            roles.Add(new RoleValidator<TRole>());
            return new RoleManager<TRole>(store, roles,
                new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(),
                null);
        }


    }
}


//private async Task CreateAccount(TestPrep prep)
//{
//    var controller = new AccountsController(prep.UserManager.Object, prep.SigninManager.Object,
//       prep.Context, prep.Mapper);
//    var accountData = new AccountData { EmailAddress = "test@test.com", Password = "Test123!", ProgrammeId = 1 };
//    await controller.CreateStudent(accountData);
//}

//[TestMethod]
//public async Task GetAllStudents()
//{
//    // Preparation
//    TestPrep prep = new TestPrep();
//    prep.Context.Programmes.Add(new Programme { Name = "Programme 1" });
//    prep.Context.Programmes.Add(new Programme { Name = "Programme 2" });

//    await CreateAccount(prep);
//    await CreateAccount(prep);
//    await CreateAccount(prep);
//    await CreateAccount(prep);

//    //prep.Context.Students.Add(new ApplicationUser { UserName = "test@test.com", Email = "test@test.com", ProgrammeId = 1, Id = 1 });
//    //prep.Context.Students.Add(new ApplicationUser { UserName = "test1@test.com", Email = "test1@test.com", ProgrammeId = 2, Id = 2 });
//    //prep.Context.Students.Add(new ApplicationUser { UserName = "test2@test.com", Email = "test2@test.com", ProgrammeId = 2, Id = 3 });
//    //prep.Context.Students.Add(new ApplicationUser { UserName = "test3@test.com", Email = "test3@test.com", ProgrammeId = 3, Id = 4 });
//    //prep.Context.Students.Add(new ApplicationUser { UserName = "test4@test.com", Email = "test4@test.com", ProgrammeId = 3, Id = 5 });

//    prep.Context.SaveChanges();

//    var context = BuildContext(prep.DatabaseName); // Check from new context

//    // Testing
//    var controller = new AccountsController(prep.UserManager.Object, prep.SigninManager.Object,
//       context, prep.Mapper);
//    var response = await controller.Get();

//    // Verification
//    var students = response.Value;
//    Assert.AreEqual(4, students.Count);
//}

//[TestMethod]
//public async Task GetStudentProjectsByIdDoesNotExist()
//{
//    // Preparation
//    TestPrep prep = new TestPrep();

//    // Testing
//    var controller = new AccountsController(prep.UserManager.Object, prep.SigninManager.Object,
//        prep.Context, prep.Mapper);
//    var response = await controller.Get(1);

//    // Verification
//    var result = response.Result as StatusCodeResult;
//    Assert.AreEqual(404, result.StatusCode);

//}

//[TestMethod]
//public async Task GetStudentProjectsByIdExists()
//{
//    // Preparation
//    TestPrep prep = new TestPrep();

//    prep.Context.Programmes.Add(new Programme { Name = "Programme 1" });
//    prep.Context.Students.Add(new ApplicationUser { UserName = "test@test.com", Email = "test@test.com", ProgrammeId = 1, Id = 1 });
//    prep.Context.Projects.Add(new Project { ApplicationUserId = 1, Id = 1, Description = "string", Title = "string", Year = "string" });

//    prep.Context.SaveChanges();

//    var context = BuildContext(prep.DatabaseName);

//    // Testing
//    var controller = new AccountsController(prep.UserManager.Object, prep.SigninManager.Object,
//        context, prep.Mapper);

//    var id = 1;
//    var response = await controller.Get(1);
//    var result = response.Value;

//    // Verification
//    Assert.AreEqual(id, result[0].ApplicationUserId);

//}

//[TestMethod]
//public async Task CreateAccount()
//{
//    // Preparation
//    TestPrep prep = new TestPrep();
//    List<ApplicationUser> ls = new List<ApplicationUser>();
//    var newAccount = new ApplicationUser { UserName = "test@test.com", Email = "test@test.com", ProgrammeId = 1, Id = 1 };
//    var password = "Test123!";

//    // Testing
//    var controller = new AccountsController(prep.UserManager.Object, prep.SigninManager.Object,
//        prep.Context, prep.Mapper);
//    var result = await prep.UserManager.Object.CreateAsync(newAccount, password);

//    // Verification
//    Assert.AreEqual(1, ls.Count);
//}