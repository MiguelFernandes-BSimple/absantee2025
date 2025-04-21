using Domain.Visitor;

public interface IFormationSubjectFactory
{
    public Task<FormationSubject> Create(string title, string description);
    public FormationSubject Create(IFormationSubjectVisitor formationSubjectVisitor);
}