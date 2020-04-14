using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    public static class IdentityHelper
    {
        public static void SetIdentityOptions(IdentityOptions options)
        {
            //Set sign in options
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;

            //Set password strength
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = false; //Special characters

            //Set the lockout time
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);

            //Set how many passwords the user can try before they get locked out
            options.Lockout.MaxFailedAccessAttempts = 3;
        }
    }
}
