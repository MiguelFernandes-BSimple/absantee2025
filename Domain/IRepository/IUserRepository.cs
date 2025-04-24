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
        Task<IEnumerable<IUser>> GetByNamesAsync(string names);
        Task<IEnumerable<IUser>> GetBySurnamesAsync(string names);
        Task<IEnumerable<IUser>> GetByNamesAndSurnamesAsync(string names, string surnames);
        Task<IUser?> GetByEmailAsync(string email);
        IUser? GetById(Guid id);
        Task<IUser?> GetByIdAsync(Guid id);
    }
}