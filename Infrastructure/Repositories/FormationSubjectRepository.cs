using Infrastructure.DataModel;
using Domain.IRepository;
using Infrastructure.Mapper;
using Domain.Visitor;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FormationSubjectRepository : GenericRepository<IFormationSubject, IFormationSubjectVisitor>, IFormationSubjectRepository
{
    private readonly IMapper<IFormationSubject, IFormationSubjectVisitor> _mapper;
    public FormationSubjectRepository(AbsanteeContext context, IMapper<IFormationSubject, IFormationSubjectVisitor> mapper) : base(context, mapper)
    {
        _mapper = mapper;
    }
    public override IFormationSubject? GetById(long id)
    {
        var subjectDM = _context.Set<FormationSubjectDataModel>().FirstOrDefault(s => s.Id == id);

        if (subjectDM == null)
        {
            return null;
        }

        var subject = _mapper.ToDomain(subjectDM);
        return subject;
    }

    public override async Task<IFormationSubject?> GetByIdAsync(long id)
    {
        var subjectDM = await _context.Set<FormationSubjectDataModel>().FirstOrDefaultAsync(s => s.Id == id);

        if (subjectDM == null)
        {
            return null;
        }

        var subject = _mapper.ToDomain(subjectDM);
        return subject;
    }

    public async Task<IFormationSubject?> GetByTitleAsync(string title)
    {
        var formationSubjectDM = await _context.Set<FormationSubjectDataModel>().FirstOrDefaultAsync(f => f.Title == title);

        if (formationSubjectDM == null)
        {
            return null;
        }

        var formationSubject = _mapper.ToDomain(formationSubjectDM);
        return formationSubject;
    }
}