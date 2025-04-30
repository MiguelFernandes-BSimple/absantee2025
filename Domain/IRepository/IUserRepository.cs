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
        Task<IEnumerable<User>> GetByNamesAsync(string names);
        Task<IEnumerable<User>> GetBySurnamesAsync(string names);
        Task<IEnumerable<User>> GetByNamesAndSurnamesAsync(string names, string surnames);
        Task<User?> GetByEmailAsync(string email);
        User? GetById(Guid id);
        Task<User?> GetByIdAsync(Guid id);
        Task<IUser?> ActivationUser(Guid Id, DateTime FinalDate);
        Task<bool> Exists(Guid ID);

    }
}