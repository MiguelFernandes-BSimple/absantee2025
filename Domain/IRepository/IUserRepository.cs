using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.IRepository
{
    public interface IUserRepository : IGenericRepository<User, IUserVisitor>
    {
        Task<IEnumerable<IUser>> GetByNamesAsync(string names);
        Task<IEnumerable<IUser>> GetBySurnamesAsync(string names);
        Task<IEnumerable<IUser>> GetByNamesAndSurnamesAsync(string names, string surnames);
        Task<IUser?> GetByEmailAsync(string email);
        User? GetById(long id);
        Task<User?> GetByIdAsync(long id);
    }
}