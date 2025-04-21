using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public class SubjectFactory : ISubjectFactory
{
    private readonly ISubjectRepository _subjectRepository;
    public SubjectFactory(ISubjectRepository subjectRepository){
        _subjectRepository = subjectRepository;
    }

    public async Task<Subject> Create(string titulo, string descricao)
    {
        var existingSubject = await _subjectRepository.GetByTituloAsync(titulo);

        if(existingSubject != null)
        {
            throw new ArgumentException("An Subject with this titulo already exists");
        }

        return new Subject(titulo,descricao);
    }

    public Subject Create(ISubjectVisitor subjectVisitor)
    {
        return new Subject(subjectVisitor.Id, subjectVisitor.Titulo, subjectVisitor.Descricao);
    }
}