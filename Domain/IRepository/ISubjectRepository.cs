using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Visitor;

namespace Domain.IRepository;

public interface ISubjectRepository : IGenericRepository<ISubject, ISubjectVisitor>
{

}
