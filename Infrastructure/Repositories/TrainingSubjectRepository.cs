using Domain.Interfaces;
using Domain.IRepository;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TrainingSubjectRepository : GenericRepository<ITrainingSubject, ITrainingSubjectVisitor>, ITrainingSubjectRepository
{
    private readonly IMapper<ITrainingSubject, ITrainingSubjectVisitor> _mapper;
    public TrainingSubjectRepository(AbsanteeContext context, IMapper<ITrainingSubject, ITrainingSubjectVisitor> mapper) : base(context, mapper) {
        _mapper = mapper;
    }

    public override ITrainingSubject? GetById(long id)
    {
        throw new NotImplementedException();
    }

    public override Task<ITrainingSubject?> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }
}
