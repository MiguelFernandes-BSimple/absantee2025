using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;

namespace Application.Services
{
    public class UserService
    {
        private IUserRepository _userRepository;
        private IUserFactory _userFactory;

        public UserService(IUserRepository userRepository, IUserFactory userFactory)
        {
            _userRepository = userRepository;
            _userFactory = userFactory;
        }

        public async Task<bool> Add(UserDTO userDTO)
        {
            var user = await _userFactory.Create(userDTO.Names, userDTO.Surnames, userDTO.Email, userDTO.FinalDate);
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<User>> GetAll()
        {
            var User = await _userRepository.GetAllAsync();
            return User;
        }
    }
}