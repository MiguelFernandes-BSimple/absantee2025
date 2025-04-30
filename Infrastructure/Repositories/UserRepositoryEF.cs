using AutoMapper;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Resolvers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepositoryEF : GenericRepositoryEF<IUser, IUserVisitor>, IUserRepository
{
    private readonly IMapper _mapper;
    public UserRepositoryEF(AbsanteeContext context, IMapper mapper) : base(context, mapper)
    {
        _mapper = mapper;
    }

    public async Task<IEnumerable<IUser>> GetByNamesAsync(string names)
    {
        if (string.IsNullOrWhiteSpace(names))
            return new List<IUser>();

        var usersDM = await this._context.Set<UserDataModel>()
                    .Where(u => u.Names.Contains(names, StringComparison.OrdinalIgnoreCase)).ToListAsync();

        var users = usersDM.Select(u => _mapper.Map<UserDataModel, User>(u));

        return users;
    }

    public async Task<IEnumerable<IUser>> GetBySurnamesAsync(string surnames)
    {
        if (string.IsNullOrWhiteSpace(surnames))
            return new List<IUser>();

        var usersDM = await this._context.Set<UserDataModel>()
                    .Where(u => u.Surnames.Contains(surnames, StringComparison.OrdinalIgnoreCase)).ToListAsync();

        var users = usersDM.Select(u => _mapper.Map<UserDataModel, User>(u));

        return users;
    }

    public async Task<IEnumerable<IUser>> GetByNamesAndSurnamesAsync(string names, string surnames)
    {
        if (string.IsNullOrWhiteSpace(names) && string.IsNullOrWhiteSpace(surnames))
            return new List<IUser>();

        var usersDM = await this._context.Set<UserDataModel>()
                    .Where(u => u.Names.Contains(names, StringComparison.OrdinalIgnoreCase)
                             && u.Surnames.Contains(surnames, StringComparison.OrdinalIgnoreCase)).ToListAsync();

        var users = usersDM.Select(u => _mapper.Map<UserDataModel, User>(u));

        return users;
    }

    public async Task<IUser?> GetByEmailAsync(string email)
    {
        var userDM = await _context.Set<UserDataModel>().FirstOrDefaultAsync(c => c.Email == email);

        if (userDM == null)
        {
            return null;
        }

        var user = _mapper.Map<UserDataModel, User>(userDM);
        return user;
    }

    public override IUser? GetById(Guid id)
    {
        var userDM = _context.Set<UserDataModel>().FirstOrDefault(c => c.Id == id);

        if (userDM == null)
            return null;

        var user = _mapper.Map<UserDataModel, User>(userDM);
        return user;
    }

    public override async Task<IUser?> GetByIdAsync(Guid id)
    {
        var userDM = await _context.Set<UserDataModel>().FirstOrDefaultAsync(u => u.Id == id);

        if (userDM == null)
            return null;

        var user = _mapper.Map<UserDataModel, User>(userDM);
        return user;
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

        userDataModel.PeriodDateTime._finalDate = FinalDate;
        _context.Entry(userDataModel).State = EntityState.Modified;

        await _context.SaveChangesAsync();
        var user = _mapper.Map<UserDataModel, User>(userDataModel);
        return user;

    }
}
