﻿using ProjectsAPI.Services;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ProjectsAPI.Validations
{
    public class UserIdExists : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Hook into IServiceProvide to retrieve service
            var repository = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));

            Task<bool> task = Task.Run<bool>(async () => await repository.IsUserIdValid((int)value));
            bool result = task.Result;

            if (!result)
            {
                return new ValidationResult("User Id doesn't exist in repository.");
            }
            else

                return ValidationResult.Success;
        }
    }
}