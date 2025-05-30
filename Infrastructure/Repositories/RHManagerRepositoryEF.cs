using AutoMapper;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Resolvers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RHManagerRepositoryEF : GenericRepositoryEF<IHRManager, HRManager, HRManagerDataModel>, IHRMangerRepository
{
    private readonly IMapper _mapper;
    public RHManagerRepositoryEF(AbsanteeContext context, IMapper mapper) : base(context, mapper)
    {
        _mapper = mapper;
    }

    public override IHRManager? GetById(Guid id)
    {
        var hrmanagerDM = _context.Set<HRManagerDataModel>().FirstOrDefault(hr => hr.Id == id);

        if (hrmanagerDM == null)
            return null;

        var hrManager = _mapper.Map<HRManagerDataModel, HRManager>(hrmanagerDM);
        return hrManager;
    }

    public override async Task<IHRManager?> GetByIdAsync(Guid id)
    {
        var hrmanagerDM = await _context.Set<HRManagerDataModel>().FirstOrDefaultAsync(hr => hr.Id == id);

        if (hrmanagerDM == null)
            return null;

        var hrManager = _mapper.Map<HRManagerDataModel, HRManager>(hrmanagerDM);
        return hrManager;
    }
}
