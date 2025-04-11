using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Visitor;

namespace Domain.IRepository
{
    public interface IUserRepository : IGenericRepository<IUser, IUserVisitor>
    {
        Task<IEnumerable<IUser>> GetByNames(string names);
        Task<IEnumerable<IUser>> GetBySurnames(string names);
        Task<IEnumerable<IUser>> GetByNamesAndSurnames(string names, string surnames);
        IUser? GetById(long id);
        Task<IUser?> GetByIdAsync(long id);
    }
}
