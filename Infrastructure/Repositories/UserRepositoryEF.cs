using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepositoryEF : GenericRepository<IUser>, IUserRepository
    {
        public UserRepositoryEF(DbContext context) : base(context)
        {
        }
    }
}
