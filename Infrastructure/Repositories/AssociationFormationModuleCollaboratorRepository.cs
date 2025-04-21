using Domain.Interfaces;
using Domain.IRepository;
using Domain.Visitor;
using Infrastructure;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

public class AssociationFormationModuleCollaboratorRepository : GenericRepository<IAssociationFormationModuleCollaborator, IAssociationFormationModuleCollaboratorVisitor>, IAssociationFormationModuleCollaboratorRepository
{
    private readonly IMapper<IAssociationFormationModuleCollaborator, IAssociationFormationModuleCollaboratorVisitor> _associationFormationModuleCollaboratorMapper;

    public AssociationFormationModuleCollaboratorRepository(AbsanteeContext context, IMapper<IAssociationFormationModuleCollaborator, IAssociationFormationModuleCollaboratorVisitor> associationFormationModuleCollaboratorMapper) : base(context, associationFormationModuleCollaboratorMapper)
    {
        _associationFormationModuleCollaboratorMapper = associationFormationModuleCollaboratorMapper;
    }

    public override IAssociationFormationModuleCollaborator? GetById(long id)
    {
        var assocDM = _context.Set<AssociationFormationModuleCollaboratorDataModel>()
                                          .FirstOrDefault(a => a.Id == id);

        if (assocDM == null)
            return null;

        var assoc = _associationFormationModuleCollaboratorMapper.ToDomain(assocDM);
        return assoc;
    }

    public override async Task<IAssociationFormationModuleCollaborator?> GetByIdAsync(long id)
    {
        var assocDM = await _context.Set<AssociationFormationModuleCollaboratorDataModel>()
                                          .FirstOrDefaultAsync(a => a.Id == id);

        if (assocDM == null)
            return null;

        var assoc = _associationFormationModuleCollaboratorMapper.ToDomain(assocDM);
        return assoc;
    }


    public async Task<IEnumerable<IAssociationFormationModuleCollaborator>> FindAllByFormationModuleAsync(long formationModuleId)
    {
        IEnumerable<AssociationFormationModuleCollaboratorDataModel> assocDM = await _context.Set<AssociationFormationModuleCollaboratorDataModel>().Where(a => a.FormationModuleId == formationModuleId).ToListAsync();

        IEnumerable<IAssociationFormationModuleCollaborator> assocs = _associationFormationModuleCollaboratorMapper.ToDomain(assocDM);

        return assocs;
    }
}