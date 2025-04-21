using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public class SubjectFactory : ISubjectFactory
{
    public async Task<Subject> Create(long id, string title, string description)
    {
        return await Create(id, title, description);
    }

    public Subject Create(ISubjectVisitor subjectVisitor)
    {
        return new Subject(subjectVisitor.Id, subjectVisitor.Title, subjectVisitor.Description);
    }

}