using LittleLive.Core.Entities;
using LittleLive.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LittleLive.Core.Service
{
    public interface IUserService
    {
        Task<Models.User> AuthenticateUser(LoginCredential login);
    }
}
