using AutoMapper;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepositoryEF : GenericRepository<User, UserDataModel>, IUserRepository
{
    private readonly IMapper _mapper;
    public UserRepositoryEF(AbsanteeContext context, IMapper mapper) : base(context, mapper)
    {
        _mapper = mapper;
    }

    public async Task<IEnumerable<User>> GetByNamesAsync(string names)
    {
        if (string.IsNullOrWhiteSpace(names))
            return new List<User>();

        var usersDM = await this._context.Set<UserDataModel>()
                    .Where(u => u.Names.Contains(names))
                    .ToListAsync();

        var users = usersDM.Select(u => _mapper.Map<UserDataModel, User>(u));

        return users;
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

    public async Task<IEnumerable<User>> GetBySurnamesAsync(string surnames)
    {
        if (string.IsNullOrWhiteSpace(surnames))
            return new List<User>();

        var usersDM = await this._context.Set<UserDataModel>()
                    .Where(u => u.Surnames.Contains(surnames)).ToListAsync();

        var users = usersDM.Select(u => _mapper.Map<UserDataModel, User>(u));

        return users;
    }

    public async Task<IEnumerable<User>> GetByNamesAndSurnamesAsync(string names, string surnames)
    {
        if (string.IsNullOrWhiteSpace(names) && string.IsNullOrWhiteSpace(surnames))
            return new List<User>();

        var usersDM = await this._context.Set<UserDataModel>()
                    .Where(u => u.Names.Contains(names, StringComparison.OrdinalIgnoreCase)
                             && u.Surnames.Contains(surnames)).ToListAsync();

        var users = usersDM.Select(u => _mapper.Map<UserDataModel, User>(u));

        return users;
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
        var userDM = await _context.Set<UserDataModel>().FirstOrDefaultAsync(c => c.Id == id);

        if (userDM == null)
            return null;

        var user = _mapper.Map<UserDataModel, User>(userDM);
        return user;
    }
}
