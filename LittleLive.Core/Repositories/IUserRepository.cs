using LittleLive.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LittleLive.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserInformation(string userName, string password);
        Task<IEnumerable<User>> GetUsersByCountryCode(string countryCode);
    }
}
