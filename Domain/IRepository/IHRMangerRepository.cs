using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.IRepository;
public interface IHRMangerRepository : IGenericRepositoryEF<IHRManager, HRManager, IHRManagerVisitor>
{
}
