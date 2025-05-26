using Application.DTO;
using AutoMapper;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Infrastructure;
namespace Application.Services;
public class RHManagerService
{
    private IHRManagerFactory _HRManagerFactory;
    private IUserFactory _userFactory;
    private IHRMangerRepository _RHManagerRepository;
    private IUserRepository _userRepository;
    private AbsanteeContext _context;
    private readonly IMapper _mapper;
    public RHManagerService(IHRMangerRepository hRMangerRepository, IHRManagerFactory hRManagerFactory, IUserFactory userFactory, IUserRepository userRepository, AbsanteeContext context)
    {
        _HRManagerFactory = hRManagerFactory;
        _RHManagerRepository = hRMangerRepository;
        _userFactory = userFactory;
        _userRepository = userRepository;
        _context = context;
    }
    public RHManagerService(IHRMangerRepository hRMangerRepository, IHRManagerFactory hRManagerFactory, IUserFactory userFactory, IUserRepository userRepository, AbsanteeContext context, IMapper mapper) : this(hRMangerRepository, hRManagerFactory, userFactory, userRepository, context)
    {
        _mapper = mapper;
    }

    public async Task<RHManagerDTO> Add(CreateRHManagerDTO rHManagerDTO)
    {

        var user = await _userFactory.Create(rHManagerDTO.Names, rHManagerDTO.Surnames, rHManagerDTO.Email, rHManagerDTO.deactivationDate);

        if (user == null) return null;

        // corrigir o savechanges - deve ser uma transação
        var createdUser = _userRepository.Add(user);
        if (createdUser == null) return null;

        var rhmanager = _HRManagerFactory.Create(createdUser, rHManagerDTO.PeriodDateTime);
        if (rhmanager == null) return null;

        var rhmanagercreated = _RHManagerRepository.Add(rhmanager);
        if (rhmanagercreated == null) return null;

        _context.SaveChanges();

        return new RHManagerDTO(rhmanagercreated.Id, rhmanager.UserId, rhmanager.PeriodDateTime);
    }
}
