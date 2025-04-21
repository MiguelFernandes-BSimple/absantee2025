using Domain.Interfaces;
using Domain.IRepository;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories;

public class FormationModuleRepository : GenericRepository<IFormationModule, IFormationModuleVisitor>, IFormationModuleRepository
{
    private readonly IMapper<IFormationModule, IFormationModuleVisitor> _mapper;
    public FormationModuleRepository(AbsanteeContext context, IMapper<IFormationModule, IFormationModuleVisitor> mapper) : base(context, mapper)
    {
        _mapper = mapper;
    }

    public override IFormationModule? GetById(long id)
    {
        var moduleDM = _context.Set<FormationModuleDataModel>().FirstOrDefault(m => m.Id == id);

        if (moduleDM == null)
        {
            return null;
        }

        var module = _mapper.ToDomain(moduleDM);
        return module;
    }

    public override async Task<IFormationModule?> GetByIdAsync(long id)
    {
        var moduleDM = await _context.Set<FormationModuleDataModel>().FirstOrDefaultAsync(m => m.Id == id);

        if (moduleDM == null)
        {
            return null;
        }

        var module = _mapper.ToDomain(moduleDM);
        return module;
    }

    public async Task<IFormationModule?> GetBySubjectId(long subjectId)
    {
        var moduleDM = await _context.Set<FormationModuleDataModel>().FirstOrDefaultAsync(m => m.FormationSubjectId == subjectId);

        if (moduleDM == null)
        {
            return null;
        }

        var module = _mapper.ToDomain(moduleDM);
        return module;
    }
}