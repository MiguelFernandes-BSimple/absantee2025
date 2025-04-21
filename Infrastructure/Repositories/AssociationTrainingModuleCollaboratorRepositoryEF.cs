using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.Repositories;

public class AssociationTrainingModuleCollaboratorRepositoryEF : GenericRepository<IAssociationTrainingModuleCollaborator, IAssociationTrainingModuleCollaboratorVisitor>, IAssociationTrainingModuleCollaboratorRepository
{
    private readonly IMapper<IAssociationTrainingModuleCollaborator, IAssociationTrainingModuleCollaboratorVisitor> _mapper;
    public TrainingModuleCollaboratorsRepository(DbContext context, IMapper<ITrainingModuleCollaborators, ITrainingModuleCollaboratorsVisitor> mapper) : base(context, mapper)
    {
        _mapper = mapper;
    }

    public override IAssociationTrainingModuleCollaborator? GetById(long id)
    {
        throw new NotImplementedException();
    }

    public override Task<IAssociationTrainingModuleCollaborator?> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<IAssociationTrainingModuleCollaborator>> GetByTrainingModuleIds(IEnumerable<long> trainingModuleIds)
    {
        var trainingModuleCollaboratorsDMs = await _context.Set<TrainingModuleCollaboratorDataModel>()
                                            .Where(t => trainingModuleIds.Contains(t.TrainingModuleId))
                                            .ToListAsync();

        var trainingModuleCollaborators = _mapper.ToDomain(trainingModuleCollaboratorsDMs);

        return trainingModuleCollaborators;
    }
}
