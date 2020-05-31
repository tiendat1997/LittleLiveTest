using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LittleLive.Core;
using LittleLive.Core.Models;
using LittleLive.Core.Service;
using Entities = LittleLive.Core.Entities;

namespace LittleLive.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<User> AuthenticateUser(LoginCredential login)
        {
            Entities.User efUser = await _unitOfWork.Users.GetUserInformation(login.UserName, login.Password);
            User user = _mapper.Map<Entities.User, User>(efUser);

            return user;
        }

        public async Task<IEnumerable<User>> GetUsersByCountryCode(string countryCode)
        {
            IEnumerable<Entities.User> efUsers = await _unitOfWork.Users.GetUsersByCountryCode(countryCode);
            IEnumerable<User> users = _mapper.Map<IEnumerable<Entities.User>, IEnumerable<User>>(efUsers);
            return users;
        }
    }
}
