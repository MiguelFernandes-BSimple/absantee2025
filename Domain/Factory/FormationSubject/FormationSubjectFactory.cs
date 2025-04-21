
using System.Threading.Tasks;
using Domain.Visitor;
using Domain.IRepository;

namespace Domain.Factory;

public class FormationSubjectFactory : IFormationSubjectFactory
{
    private readonly IFormationSubjectRepository _formationSubjectRepository;

    public FormationSubjectFactory(IFormationSubjectRepository formationSubjectRepository)
    {
        _formationSubjectRepository = formationSubjectRepository;
    }

    public async Task<FormationSubject> Create(string title, string description)
    {
        var existingFormationSubject = await _formationSubjectRepository.GetByTitleAsync(title);

        if (existingFormationSubject != null)
        {
            throw new ArgumentException("A subject with this title already exists.");
        }

        return new FormationSubject(title, description);
    }

    public FormationSubject Create(IFormationSubjectVisitor formationSubjectVisitor)
    {
        return new FormationSubject(formationSubjectVisitor.Id, formationSubjectVisitor.Title, formationSubjectVisitor.Description);
    }
}