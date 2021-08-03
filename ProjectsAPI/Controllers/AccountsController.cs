using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectsAPI.DTOs;
using ProjectsAPI.Entities;
using ProjectsAPI.Infrastructure;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsAPI.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AccountsController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signinManager,
            IConfiguration configuration,
            ApplicationDbContext context,
            IMapper mapper)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _configuration = configuration;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet] // [api/accounts] - get all students
        public async Task<ActionResult<List<StudentDTO>>> Get()
        {
            var students = await _context.Students.AsNoTracking().ToListAsync();
            var studentsDTO = _mapper.Map<List<StudentDTO>>(students);

            return studentsDTO;
        }

        [HttpGet("{studentId:int}/projects")] // [api/students/projects/{studentID}] - get all student's projects
        public async Task<ActionResult<List<ProjectDTO>>> Get(int studentId)
        {

            if (!await _context.IsStudentIdValid(studentId))
            {
                return NotFound(); // student id doesnt exist
            }

            var projects = await _context.Projects.AsNoTracking().Where(x => x.ApplicationUserId == studentId).ToListAsync();
            var projectsDTO = _mapper.Map<List<ProjectDTO>>(projects);

            if (projectsDTO.Count == 0)
            {
                return NotFound(); // student has no projects
            }

            return projectsDTO;
        }


        [HttpPost("Create")]
        public async Task<ActionResult<AccountToken>> CreateStudent([FromBody] AccountData model)
        {    
            var user = new ApplicationUser { UserName = model.EmailAddress, Email = model.EmailAddress, ProgrammeId = model.ProgrammeId};
            var result = await _userManager.CreateAsync(user, model.Password);        

            if (result.Succeeded)
            {
                return BuildToken(model);
            }
            else
            {
                return BadRequest(result.Errors); // Why account creation failed
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AccountToken>> Login([FromBody] AccountLoginDTO model)
        {
            var accountData = _mapper.Map<AccountData>(model);


            var result = await _signinManager.PasswordSignInAsync(accountData.EmailAddress,
                accountData.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return BuildToken(accountData);
            }
            else
            {
                return BadRequest("Invalid Login attempt. Please try again.");
            }
        }

        [HttpPost("RenewToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<AccountToken>> Renew()
        {
            var accountData = new AccountData
            {
                EmailAddress = HttpContext.User.Identity.Name
            };

            return BuildToken(accountData);
        }


        private AccountToken BuildToken(AccountData accountData)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, accountData.EmailAddress),
                new Claim(ClaimTypes.Email, accountData.EmailAddress)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddDays(7);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            return new AccountToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
