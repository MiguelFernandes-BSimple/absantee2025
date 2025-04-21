using Domain.IRepository;
using Domain.Interfaces;
using Domain.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Infrastructure.DataModel;
using System.Threading.Tasks;
using Infrastructure.Mapper;
using Domain.Visitor;

namespace Infrastructure.Repositories;

public class AssociationTrainingModuleCollaboratorRepositoryEF : GenericRepository<IAssociationTrainingModuleCollaborator, IAssociationTrainingModuleCollaboratorVisitor>, IAssociationTrainingModuleCollaboratorRepository
{
    private readonly IMapper<IAssociationTrainingModuleCollaborator, IAssociationTrainingModuleCollaboratorVisitor> _mapper;
    public AssociationTrainingModuleCollaboratorRepositoryEF(AbsanteeContext context, IMapper<IAssociationTrainingModuleCollaborator, IAssociationTrainingModuleCollaboratorVisitor> mapper) : base(context, mapper)
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
        var associationTrainingModuleCollaboratorDataModels = await _context.Set<AssociationTrainingModuleCollaboratorDataModel>()
                                            .Where(t => trainingModuleIds.Contains(t.TrainingModuleId))
                                            .ToListAsync();

        var associationTrainingModuleCollaborators = _mapper.ToDomain(associationTrainingModuleCollaboratorDataModels);

        return associationTrainingModuleCollaborators;
    }
}
