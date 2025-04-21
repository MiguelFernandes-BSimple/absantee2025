using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Factory.SubjectFactory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper
{
    public class SubjectMapper : IMapper<Subject, SubjectDataModel>
    {
        private ISubjectFactory _subjectFactory;

        public SubjectMapper(ISubjectFactory subjectFactory)
        {
            _subjectFactory = subjectFactory;
        }

        public Subject ToDomain(SubjectDataModel subjectDM)
        {
            return _subjectFactory.Create(subjectDM);
        }

        public IEnumerable<Subject> ToDomain(IEnumerable<SubjectDataModel> subjectDMs)
        {
            return subjectDMs.Select(s => ToDomain(s));
        }

        public SubjectDataModel ToDataModel(Subject subjectDomain)
        {
            return new SubjectDataModel(subjectDomain);
        }

        public IEnumerable<SubjectDataModel> ToDataModel(IEnumerable<Subject> subjectDomains)
        {
            return subjectDomains.Select(s => ToDataModel(s));
        }


    }
}