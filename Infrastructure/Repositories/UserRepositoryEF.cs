using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.IRepository;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepositoryEF : GenericRepository<IUser>, IUserRepository
    {
        public UserRepositoryEF(DbContext context) : base(context)
        {
        }

        public async Task<bool> HasNames(long userId, string names)
        {
            if (string.IsNullOrWhiteSpace(names))
                return false;

            return  await this._context.Set<UserDataModel>()
                        .AnyAsync(u => u.Id == userId 
                        && u.Names.Contains(names, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<bool> HasSurnames(long userId, string surnames)
        {
            if (string.IsNullOrWhiteSpace(surnames))
                return false;

            return await this._context.Set<UserDataModel>()
                        .AnyAsync(u => u.Id == userId
                        && u.Surnames.Contains(surnames, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<bool> HasNamesAndSurnames(long userId, string names, string surnames)
        {
            if (string.IsNullOrWhiteSpace(names) && string.IsNullOrWhiteSpace(surnames))
                return false;

            return await this._context.Set<UserDataModel>()
                        .AnyAsync(u => u.Id == userId
                        && u.Names.Contains(names, StringComparison.OrdinalIgnoreCase)
                        && u.Surnames.Contains(surnames, StringComparison.OrdinalIgnoreCase));
        }

    }
}
