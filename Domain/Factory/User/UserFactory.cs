using System.Text.RegularExpressions;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public class UserFactory : IUserFactory
{

    private readonly IUserRepository _userRepository;

    public UserFactory(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Create(string names, string surnames, string email, DateTime deactivationDate)
    {
        var existingUser = await _userRepository.GetByEmailAsync(email);

        if (existingUser != null)
        {
            throw new ArgumentException("An user with this email already exists.");
        }

        return new User(names, surnames, email, deactivationDate);
    }

    public User Create(IUserVisitor userVisitor)
    {
        return new User(userVisitor.Id, userVisitor.Names, userVisitor.Surnames, userVisitor.Email, userVisitor.PeriodDateTime);
    }
}