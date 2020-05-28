using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleLive.WebApi.Models
{
    public class Policies
    {
        public const string Admin = "Admin";
        public const string User = "User";

        public static AuthorizationPolicy AdminPolicy()
        {
            var adminPolicyBuilder = new AuthorizationPolicyBuilder()
                                        .RequireAuthenticatedUser()
                                        .RequireRole(Admin);                                        
            return adminPolicyBuilder.Build();
        }

        public static AuthorizationPolicy UserPolicy()
        {
            var userPolicyBuilder = new AuthorizationPolicyBuilder()
                                            .RequireAuthenticatedUser()
                                            .RequireRole(User);
            return userPolicyBuilder.Build();
        }
    }
}
