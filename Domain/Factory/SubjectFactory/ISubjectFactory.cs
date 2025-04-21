using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory.SubjectFactory
{
    public interface ISubjectFactory
    {
        Subject Create(string title, string description);
        Subject Create(ISubjectVisitor visitor);
    }
}