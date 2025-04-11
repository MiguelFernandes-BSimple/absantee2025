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

        public bool HasNames(long userId, string names)
        {
            if (string.IsNullOrWhiteSpace(names))
                return false;

            return this._context.Set<UserDataModel>()
                        .Any(u => u.Id == userId 
                        && u.Names.Contains(names, StringComparison.OrdinalIgnoreCase));
        }

        public bool HasSurnames(long userId, string surnames)
        {
            if (string.IsNullOrWhiteSpace(surnames))
                return false;

            return this._context.Set<UserDataModel>()
                        .Any(u => u.Id == userId
                        && u.Surnames.Contains(surnames, StringComparison.OrdinalIgnoreCase));
        }
    }
}
