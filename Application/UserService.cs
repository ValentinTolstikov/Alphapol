using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;

namespace Application
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository) 
        {
            _userRepository = userRepository;
        }

        public async Task<User?> LoginAsync(string login, string password)
        {
            var users = await _userRepository.GetAsync(p => p.Login == login && p.Password == password);
            if (users.Count == 0)
            {
                return null;
            }
            else
            {
                return users.FirstOrDefault();
            }
        }
    }
}
