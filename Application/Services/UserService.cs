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
        return user;
    }
}
