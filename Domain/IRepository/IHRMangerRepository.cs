using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Visitor;

namespace Domain.IRepository;
public interface IHRMangerRepository : IGenericRepository<HRManager, IHRManagerVisitor>
{
    HRManager? GetById(Guid id);
    Task<HRManager?> GetByIdAsync(Guid id);
}
