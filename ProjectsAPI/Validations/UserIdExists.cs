using ProjectsAPI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsAPI.Validations
{
    public class UserIdExists : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            // Hook into IServiceProvide to retrieve service
            var repository = (IRepository) validationContext.GetService(typeof(IRepository));

            Task<bool> task = Task.Run<bool>(async () => await repository.IsUserIdValid((int) value));
            bool result = task.Result;

            if (!result)
            {
                return new ValidationResult("User Id doesn't exist in repository.");
            } else
            
             return ValidationResult.Success;
        }
    }
}
