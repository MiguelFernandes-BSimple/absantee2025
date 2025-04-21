using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;
public interface ISubjectFactory
{
    Task<Subject> Create(long Id, string Title, string Description);
    public Subject Create(ISubjectVisitor subjectVisitorVisitor);
}
