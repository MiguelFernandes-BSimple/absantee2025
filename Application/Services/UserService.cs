using Domain.IRepository;
using Domain.Interfaces;
using Domain.Models;

public class UserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo)
    {
        _repo = repo;
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

    public async Task<IUser> CreateAsync(User user)
    {
        await _repo.AddAsync(user);
        await _repo.SaveChangesAsync();

        var userWithId = await _repo.GetByEmailAsync(user.Email);
        return userWithId!;
    }

    public async Task<IEnumerable<IUser>> GetByNamesAsync(string names)
    {
        return await _repo.GetByNamesAsync(names);
    }

    public async Task UpdateAsync(User user)
    {
        await _repo.UpdateAsync(user);
    }
    public async Task<IEnumerable<IUser>> GetActiveUsersAsync()
    {
        return await _repo.GetActiveUsersAsync();
    }
}
