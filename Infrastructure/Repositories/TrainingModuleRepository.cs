using Domain.Interfaces;
using Domain.IRepository;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TrainingModuleRepository : GenericRepository<ITrainingModule, ITrainingModuleVisitor>, ITrainingModuleRepository {
    private readonly IMapper<ITrainingModule, ITrainingModuleVisitor> _mapper;
    public TrainingModuleRepository(AbsanteeContext context, IMapper<ITrainingModule, ITrainingModuleVisitor> mapper) : base(context, mapper) {
        _mapper = mapper;
    }

    public override ITrainingModule? GetById(long id)
    {
        var moduleDM = _context.Set<TrainingModuleDataModel>()
            .FirstOrDefault(ts => ts.Id == id);

        if(moduleDM == null)
            return null;
        
        var module = _mapper.ToDomain(moduleDM);
        return module;
    }

    public override async Task<ITrainingModule?> GetByIdAsync(long id)
    {
        var moduleDM = await _context.Set<TrainingModuleDataModel>()
            .FirstOrDefaultAsync(ts => ts.Id == id);

        if(moduleDM == null)
            return null;

        var module = _mapper.ToDomain(moduleDM);
        return module;
    }
}
