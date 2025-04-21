using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AssociationTrainingModuleCollaboratorRepository : GenericRepository<IAssociationTrainingModuleCollaborator, IAssociationTrainingModuleCollaboratorVisitor>, IAssociationTrainingModuleCollaboratorRepository {
    private readonly IMapper<IAssociationTrainingModuleCollaborator, IAssociationTrainingModuleCollaboratorVisitor> _mapper;
    public AssociationTrainingModuleCollaboratorRepository(AbsanteeContext context, IMapper<IAssociationTrainingModuleCollaborator, IAssociationTrainingModuleCollaboratorVisitor> mapper) : base(context, mapper) {
        _mapper = mapper;
    }

    public override IAssociationTrainingModuleCollaborator? GetById(long id)
    {
        var amcDM = _context.Set<AssociationTrainingModuleCollaboratorDataModel>()
            .FirstOrDefault(ts => ts.Id == id);

        if(amcDM == null)
            return null;
        
        var amc = _mapper.ToDomain(amcDM);
        return amc;
    }

    public override async Task<IAssociationTrainingModuleCollaborator?> GetByIdAsync(long id)
    {
        var amcDM = await _context.Set<AssociationTrainingModuleCollaboratorDataModel>()
            .FirstOrDefaultAsync(ts => ts.Id == id);

        if(amcDM == null)
            return null;

        var amc = _mapper.ToDomain(amcDM);
        return amc;
    }

    public async Task<IEnumerable<IAssociationTrainingModuleCollaborator>> FindAllByModuleIdAsync(long moduleId)
    {
        try
        {
            IEnumerable<AssociationTrainingModuleCollaboratorDataModel> assocDM =
                await _context.Set<AssociationTrainingModuleCollaboratorDataModel>()
                              .Where(a => a.TrainingModuleId == moduleId)
                              .ToListAsync();

            IEnumerable<IAssociationTrainingModuleCollaborator> assocs =
                _mapper.ToDomain(assocDM);

            return assocs;
        }
        catch
        {
            throw;
        }
    }
}
