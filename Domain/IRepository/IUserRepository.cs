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
    public interface IUserRepository : IGenericRepositoryEF<IUser, User, IUserVisitor>
    {
        Task<IEnumerable<IUser>> GetByNamesAsync(string names);
        Task<IEnumerable<IUser>> GetBySurnamesAsync(string names);
        Task<IEnumerable<IUser>> GetByNamesAndSurnamesAsync(string names, string surnames);
        Task<User?> GetByEmailAsync(string email);
        Task<IUser?> ActivationUser(Guid Id, DateTime FinalDate);
        Task<bool> Exists(Guid ID);

        Task<IEnumerable<User>> GetByIdsAsync(List<Guid> userIdsOfCollab);

        Task<User?> UpdateUser(IUser user_);
    }
}