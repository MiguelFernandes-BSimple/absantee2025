using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using System.Text.RegularExpressions;

namespace Domain.Factory;

public class SubjectFactory : ISubjectFactory
{
    public async Task<Subject> Create(long id, string title, string description)
    {
        Regex titleRegex = new Regex(@"^[A-Za-z0-9\s]{1,20}$");
        Regex descRegex = new Regex(@"^[A-Za-z0-9\s]{1,100}$");

        if (!titleRegex.IsMatch(title))
            throw new ArgumentException("Invalid title.");
        if (!descRegex.IsMatch(description))
            throw new ArgumentException("Invalid description.");
        return await Create(id, title, description);
    }

    public Subject Create(ISubjectVisitor subjectVisitor)
    {
        return new Subject(subjectVisitor.Id, subjectVisitor.Title, subjectVisitor.Description);
    }

}