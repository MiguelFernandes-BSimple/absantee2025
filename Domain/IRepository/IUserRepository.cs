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
        Task<IEnumerable<IUser>> GetByNames(string names);
        Task<IEnumerable<IUser>> GetBySurnames(string names);
        Task<IEnumerable<IUser>> GetByNamesAndSurnames(string names, string surnames);
    }
}
