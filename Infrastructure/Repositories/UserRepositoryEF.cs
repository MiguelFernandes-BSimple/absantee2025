using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.IRepository;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepositoryEF : GenericRepository<IUser>, IUserRepository
    {
        private UserMapper _mapper;
        public UserRepositoryEF(DbContext context, UserMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<IUser>> GetByNames(string names)
        {
            if (string.IsNullOrWhiteSpace(names))
                return new List<IUser>();

            var usersDM = await this._context.Set<UserDataModel>()
                        .Where(u => u.Names.Contains(names, StringComparison.OrdinalIgnoreCase)).ToListAsync();

            var users = _mapper.ToDomain(usersDM);

            return users;
        }

        public async Task<IEnumerable<IUser>> GetBySurnames(string surnames)
        {
            if (string.IsNullOrWhiteSpace(surnames))
                return new List<IUser>();

            var usersDM = await this._context.Set<UserDataModel>()
                        .Where(u => u.Surnames.Contains(surnames, StringComparison.OrdinalIgnoreCase)).ToListAsync();

            var users = _mapper.ToDomain(usersDM);

            return users;
        }

        public async Task<IEnumerable<IUser>> GetByNamesAndSurnames(string names, string surnames)
        {
            if (string.IsNullOrWhiteSpace(names) && string.IsNullOrWhiteSpace(surnames))
                return new List<IUser>();

            var usersDM = await this._context.Set<UserDataModel>()
                        .Where(u => u.Names.Contains(names, StringComparison.OrdinalIgnoreCase)
                                 && u.Surnames.Contains(surnames, StringComparison.OrdinalIgnoreCase)).ToListAsync();

            var users = _mapper.ToDomain(usersDM);

            return users;
        }

    }
}
