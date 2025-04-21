using Domain.Interfaces;
using Domain.IRepository;
using Domain.Visitor;
using Infrastructure;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

public class AssociationTrainingModuleCollaboratorRepositoryEF : GenericRepository<IAssociationTrainingModuleCollaborator, IAssociationTrainingModuleCollaboratorVisitor>, IAssociationTrainingModuleCollaboratorRepository
{
    private readonly IMapper<IAssociationTrainingModuleCollaborator, IAssociationTrainingModuleCollaboratorVisitor> _mapper;
    public AssociationTrainingModuleCollaboratorRepositoryEF(AbsanteeContext context, IMapper<IAssociationTrainingModuleCollaborator, IAssociationTrainingModuleCollaboratorVisitor> mapper) : base(context, mapper)
    {
        _mapper = mapper;
    }

    public async Task<IAssociationTrainingModuleCollaborator?> FindByCollaborator(long collabId)
    {
        var result = await _context.Set<AssociationTrainingModuleCollaboratorDataModel>()
                                   .FirstOrDefaultAsync(a => a.CollaboratorId == collabId);

        if (result == null) return null;

        var toDomain = _mapper.ToDomain(result);

        return toDomain;
    }

    public async Task<IEnumerable<IAssociationTrainingModuleCollaborator>> FindAllCollaboratorsByTrainingModule(long tmId)
    {
        var assocDMs =
            await _context.Set<AssociationTrainingModuleCollaboratorDataModel>()
                          .Where(a => a.TrainingModuleId == tmId)
                          .DistinctBy(a => a.CollaboratorId)
                          .ToListAsync();

        var assocsDomain = _mapper.ToDomain(assocDMs);

        return assocsDomain;
    }

    public override IAssociationTrainingModuleCollaborator? GetById(long id)
    {
        var assocDM = _context.Set<AssociationTrainingModuleCollaboratorDataModel>()
                           .FirstOrDefault(t => t.Id == id);

        if (assocDM == null)
            return null;

        var assoc = _mapper.ToDomain(assocDM);
        return assoc;
    }

    public override async Task<IAssociationTrainingModuleCollaborator?> GetByIdAsync(long id)
    {
        var assocDM = await _context.Set<AssociationTrainingModuleCollaboratorDataModel>()
                           .FirstOrDefaultAsync(t => t.Id == id);

        if (assocDM == null)
            return null;

        var assoc = _mapper.ToDomain(assocDM);
        return assoc;
    }
}