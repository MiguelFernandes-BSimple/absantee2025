using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Factory;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper;

public class SubjectMapper : IMapper<Subject, SubjectDataModel>
{
    private ISubjectFactory _subjectFactory;

    public SubjectMapper(ISubjectFactory subjectFactory)
    {
        _subjectFactory = subjectFactory;
    }

    public Subject ToDomain(SubjectDataModel subjectDM)
    {
        var subjectDomain = _subjectFactory.Create(subjectDM);
        return subjectDomain;
    }

    public IEnumerable<Subject> ToDomain(IEnumerable<SubjectDataModel> subjectsDM){
        return subjectsDM.Select(ToDomain);
    }
    public SubjectDataModel ToDataModel(Subject subject)
    {
        return new SubjectDataModel(subject);
    }
    
    public IEnumerable<SubjectDataModel> ToDataModel(IEnumerable<Subject> subjects)
    {
        return subjects.Select(ToDataModel);
    }
}