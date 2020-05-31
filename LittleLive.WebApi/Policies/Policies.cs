using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleLive.WebApi
{
    public static class Policies
    {
        public const string HQOwner = "HQOwner";
        public const string SchoolOwner = "SchoolOwner";
        public const string Teacher = "Teacher";
        public const string Administrator = "Administrator";

        public static AuthorizationPolicy HQOwnerPolicy()
        {
            var adminPolicyBuilder = new AuthorizationPolicyBuilder()
                                        .RequireAuthenticatedUser()
                                        .RequireRole(Core.Entities.Role.HQOwner.ToString());                                        
            return adminPolicyBuilder.Build();
        }

        public static AuthorizationPolicy SchoolOwnerPolicy()
        {
            var adminPolicyBuilder = new AuthorizationPolicyBuilder()
                                        .RequireAuthenticatedUser()
                                        .RequireRole(Core.Entities.Role.SchoolOwner.ToString());
            return adminPolicyBuilder.Build();
        }

        public static AuthorizationPolicy TeacherPolicy()
        {
            var adminPolicyBuilder = new AuthorizationPolicyBuilder()
                                        .RequireAuthenticatedUser()
                                        .RequireRole(Core.Entities.Role.Teacher.ToString());
            return adminPolicyBuilder.Build();
        }

        public static AuthorizationPolicy AdministratorPolicy()
        {
            var adminPolicyBuilder = new AuthorizationPolicyBuilder()
                                        .RequireAuthenticatedUser()
                                        .RequireRole(Core.Entities.Role.Administrator.ToString());
            return adminPolicyBuilder.Build();
        }
    }
}
