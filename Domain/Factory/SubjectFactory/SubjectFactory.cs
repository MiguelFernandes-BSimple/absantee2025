using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory.SubjectFactory
{
    public class SubjectFactory : ISubjectFactory
    {
        private readonly ISubjectRepository _subjectRepo;

        public SubjectFactory(ISubjectRepository subjectRepo)
        {
            _subjectRepo = subjectRepo;
        }

        public Subject Create(string title, string description)
        {
            var tileExists = _subjectRepo.TitleExists(title);
            if (tileExists)
            {
                throw new ArgumentException("Title already exists");
            }
            return new Subject(title, description);
        }

        public Subject Create(ISubjectVisitor visitor)
        {
            return new Subject(visitor._id, visitor._title, visitor._description);
        }
    }
}