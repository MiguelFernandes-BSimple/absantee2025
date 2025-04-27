using AutoMapper;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepositoryEF : GenericRepository<IUser, UserDataModel>, IUserRepository
{
    private readonly IMapper _mapper;
    public UserRepositoryEF(AbsanteeContext context, IMapper mapper) : base(context, mapper)
    {
        _mapper = mapper;
    }

    public async Task<IEnumerable<IUser>> GetAllUsersAsync()
    {
        var usersDM = await _context.Set<UserDataModel>().ToListAsync();
        var users = usersDM.Select(u => _mapper.Map<UserDataModel, User>(u));
        return users;
    }

    public async Task UpdateAsync(IUser user)
    {
        var existing = await _context.Set<UserDataModel>().FirstOrDefaultAsync(u => u.Id == user.GetId());
        if (existing == null)
            throw new ArgumentException("User not found.");

        existing.Names = user.GetNames();
        existing.Surnames = user.GetSurnames();
        existing.Email = user.GetEmail();
        existing.PeriodDateTime._finalDate = user.GetPeriodDateTime()._finalDate;

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<IUser>> GetActiveUsersAsync()
    {
        var now = DateTime.UtcNow;

        var usersDM = await _context.Set<UserDataModel>()
            .Where(u => u.PeriodDateTime._finalDate > now)
            .ToListAsync();

        return usersDM.Select(u => _mapper.Map<UserDataModel, User>(u));
    }


    public async Task<IEnumerable<IUser>> GetByNamesAsync(string names)
    {
        if (string.IsNullOrWhiteSpace(names))
            return new List<IUser>();

        var usersDM = await _context.Set<UserDataModel>()
            .Where(u => EF.Functions.ILike(u.Names, $"%{names}%"))
            .ToListAsync();

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

    public override IUser? GetById(long id)
    {
        var userDM = _context.Set<UserDataModel>().FirstOrDefault(c => c.Id == id);

        if (userDM == null)
            return null;

        var user = _mapper.Map<UserDataModel, User>(userDM);
        return user;
    }

    public override async Task<IUser?> GetByIdAsync(long id)
    {
        var userDM = await _context.Set<UserDataModel>().FirstOrDefaultAsync(c => c.Id == id);

        if (userDM == null)
            return null;

        var user = _mapper.Map<UserDataModel, User>(userDM);
        return user;
    }
}
