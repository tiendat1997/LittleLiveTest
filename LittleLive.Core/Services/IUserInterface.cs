using LittleLive.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LittleLive.Core.Service
{
    public interface IUserInterface
    {
        Task<IEnumerable<User>> GetAllUsers();
    }
}
