namespace Infrastructure.Mapper;

using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;

public class SubjectMapper : IMapper<Subject, SubjectDataModel>
{
    public Subject ToDomain(SubjectDataModel subjectDM)
    {
        Subject subject = new Subject(subjectDM.Id, subjectDM.Title, subjectDM.Description);

        subject.SetId(subjectDM.Id);

        return subject;
    }

    public IEnumerable<Subject> ToDomain(IEnumerable<SubjectDataModel> subjectDM)
    {
        return subjectDM.Select(ToDomain);
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