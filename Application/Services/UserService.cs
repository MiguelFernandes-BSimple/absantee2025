using Domain.IRepository;
using Domain.Interfaces;
using Domain.Models;
using Domain.Factory;

public class UserService
{
    private readonly IUserRepository _repo;
    private readonly IUserFactory _userFactory;

    public UserService(IUserRepository repo, IUserFactory factory)
    {
        _repo = repo;
        _userFactory = factory;
    }
    public async Task<IEnumerable<IUser>> GetAllAsync()
    {
        return await _repo.GetAllUsersAsync();
    }

    public async Task<IUser?> GetByIdAsync(long id)
    {
        return await _repo.GetByIdAsync(id);
    }

    public async Task<IUser?> GetByEmailAsync(string email)
    {
        return await _repo.GetByEmailAsync(email);
    }

    public async Task<IUser> CreateAsync(UserDTO dto)
    {
        var user = await _userFactory.Create(dto.Names, dto.Surnames, dto.Email, dto.DeactivationDate ?? DateTime.MaxValue);
        await _repo.AddAsync(user);
        await _repo.SaveChangesAsync();

        var userWithId = await _repo.GetByEmailAsync(user.Email);
        return userWithId!;
    }

    public async Task<IEnumerable<IUser>> GetByNamesAsync(string names)
    {
        return await _repo.GetByNamesAsync(names);
    }

    public async Task<bool> UpdateAsync(UserDTO dto, long id)
    {
        var user = await GetByIdAsync(id);

        if (user != null)
        {
            user = UserDTO.UpdateToDomain(user, dto);

            await _repo.UpdateAsync(user);

            return true;
        }
        else
        {
            return false;
        }
    }


    public async Task<IEnumerable<IUser>> GetActiveUsersAsync()
    {
        return await _repo.GetActiveUsersAsync();
    }
}
