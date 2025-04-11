using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain.IRepository
{
    public interface IUserRepository : IGenericRepository<IUser>
    {
        Task<bool> HasNames(long userId, string names);
        Task<bool> HasSurnames(long userId, string names);
        Task<bool> HasNamesAndSurnames(long userId, string names, string surnames);
    }
}
