using AutoMapper;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Resolvers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepositoryEF : GenericRepositoryEF<User, UserDataModel>, IUserRepository
{
    private readonly IMapper _mapper;
    public UserRepositoryEF(AbsanteeContext context, IMapper mapper) : base(context, mapper)
    {
        _mapper = mapper;
    }

    // método adicionado com o professor, pode ser util mais tarde
    /*     public async Task<IEnumerable<Guid>> GetUserIdByNamesAsync(string names)
        {
            if (string.IsNullOrWhiteSpace(names))
                return new List<Guid>();

            var usersIds = await this._context.Set<UserDataModel>()
                        .Where(u => u.Names.Contains(names)).Select(u => u.Id)
                        .ToListAsync();

            return usersIds;
        } */

    public async Task<IEnumerable<User>> GetByNamesAsync(string names)
    {
        if (string.IsNullOrWhiteSpace(names))
            return new List<User>();

        var usersDM = await _context.Set<UserDataModel>()
            .Where(u => EF.Functions.Like(u.Names, $"%{names}%"))
            .ToListAsync();

        var users = usersDM.Select(u => _mapper.Map<User>(u));

        return users;
    }

    public async Task<IEnumerable<User>> GetBySurnamesAsync(string surnames)
    {
        if (string.IsNullOrWhiteSpace(surnames))
            return new List<User>();

        var usersDM = await _context.Set<UserDataModel>()
            .Where(u => EF.Functions.Like(u.Surnames, $"%{surnames}%"))
            .ToListAsync();

        return usersDM.Select(u => _mapper.Map<User>(u));
    }

    public async Task<IEnumerable<User>> GetByNamesAndSurnamesAsync(string names, string surnames)
    {
        if (string.IsNullOrWhiteSpace(names) && string.IsNullOrWhiteSpace(surnames))
            return new List<User>();

        var usersDM = await _context.Set<UserDataModel>()
            .Where(u =>
                (string.IsNullOrWhiteSpace(names) || EF.Functions.Like(u.Names, $"%{names}%")) &&
                (string.IsNullOrWhiteSpace(surnames) || EF.Functions.Like(u.Surnames, $"%{surnames}%"))
            )
            .ToListAsync();

        return usersDM.Select(u => _mapper.Map<User>(u));
    }


    public async Task<User?> GetByEmailAsync(string email)
    {
        var userDM = await _context.Set<UserDataModel>().FirstOrDefaultAsync(c => c.Email == email);

        if (userDM == null)
        {
            return null;
        }

        var user = _mapper.Map<UserDataModel, User>(userDM);
        return user;
    }

    public override User? GetById(Guid id)
    {
        var userDM = _context.Set<UserDataModel>().FirstOrDefault(c => c.Id == id);

        if (userDM == null)
            return null;

        var user = _mapper.Map<UserDataModel, User>(userDM);
        return user;
    }

    public override async Task<User?> GetByIdAsync(Guid id)
    {
        var userDM = await _context.Set<UserDataModel>().FirstOrDefaultAsync(u => u.Id == id);

        if (userDM == null)
            return null;

        var user = _mapper.Map<UserDataModel, User>(userDM);
        return user;
    }

    public async Task<IEnumerable<User>> GetByIdsAsync(List<Guid> userIdsOfCollab)
    {
        var usersDM = await _context.Set<UserDataModel>()
            .Where(u => userIdsOfCollab.Contains(u.Id))
            .ToListAsync();

        return usersDM.Select(u => _mapper.Map<User>(u));
    }
    
    public async Task<bool> Exists(Guid ID)
    {
        var userDM = await _context.Set<UserDataModel>().FirstOrDefaultAsync(u => u.Id == ID);
        if (userDM == null)
            return false;
        return true;
    }
    public async Task<IUser?> ActivationUser(Guid Id, DateTime FinalDate)
    {

        var userDataModel = await _context.Set<UserDataModel>()
                .FirstAsync(u => u.Id == Id);

        userDataModel.PeriodDateTime = new PeriodDateTime(userDataModel.PeriodDateTime._initDate, FinalDate.ToUniversalTime());
        _context.Entry(userDataModel).State = EntityState.Modified;

        await _context.SaveChangesAsync();
        var user = _mapper.Map<UserDataModel, User>(userDataModel);
        return user;

    }
}
