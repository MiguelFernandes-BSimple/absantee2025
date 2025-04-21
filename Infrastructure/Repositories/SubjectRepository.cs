using Domain.Interfaces;
using Domain.IRepository;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;


public class SubjectRepository : GenericRepository<ISubject, ISubjectVisitor>, ISubjectRepository
{

    private readonly IMapper<ISubject, ISubjectVisitor> _mapper;

    public SubjectRepository(AbsanteeContext context, IMapper<ISubject, ISubjectVisitor> mapper) : base(context, mapper)
    {
        _mapper = mapper;
    }

    public override ISubject? GetById(long id)
    {
        var subjectDM = _context.Set<SubjectDataModel>().FirstOrDefault(s => s.Id == id);

        if(subjectDM == null)
            return null;

        var subject = _mapper.ToDomain(subjectDM);

        return subject;
    }

     public override async Task<ISubject?> GetByIdAsync(long id)
    {
        var subjectDM = await _context.Set<SubjectDataModel>().FirstOrDefaultAsync(c => c.Id == id);

        if (subjectDM == null)
            return null;

        var subject = _mapper.ToDomain(subjectDM);
        return subject;
    }

    public async Task<ISubject> GetByTituloAsync(string titulo)
    {
        var subjectDM = await _context.Set<SubjectDataModel>().FirstOrDefaultAsync(c => c.Titulo == titulo);

        if(subjectDM == null)
            return null;

        var subject = _mapper.ToDomain(subjectDM);
        
        return subject;
    }
}
