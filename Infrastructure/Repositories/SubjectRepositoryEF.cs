using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SubjectRepositoryEF : GenericRepository<ISubject, SubjectDataModel>, ISubjectRepository
{
    private readonly IMapper<ISubject, SubjectDataModel> _mapper;
    public SubjectRepositoryEF(AbsanteeContext context, IMapper<ISubject, SubjectDataModel> mapper) : base(context, mapper)
    {
        _mapper = mapper;
    }
    public override ISubject? GetById(long id)
    {
        var subjectDM = this._context.Set<SubjectDataModel>()
                            .FirstOrDefault(s => s.Id == id);

        if (subjectDM == null)
            return null;

        var subject = _mapper.ToDomain(subjectDM);
        return subject;
    }

    public override async Task<ISubject?> GetByIdAsync(long id)
    {
        var subjectDM = await this._context.Set<SubjectDataModel>()
                            .FirstOrDefaultAsync(s => s.Id == id);

        if (subjectDM == null)
            return null;

        var subject = _mapper.ToDomain(subjectDM);
        return subject;
    }
}
