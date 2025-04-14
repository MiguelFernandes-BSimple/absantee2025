using System.Net.Mail;
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

    public async Task<User> Create(string names, string surnames, string email, DateTime? deactivationDate)
    {
        Regex nameRegex = new Regex(@"^[A-Za-zÀ-ÖØ-öø-ÿ\s]{1,50}$");

        if (!nameRegex.IsMatch(names) || !nameRegex.IsMatch(surnames))
            throw new ArgumentException("Names or surnames are invalid.");

        try
        {
            var emailValidator = new MailAddress(email);
        }
        catch (Exception)
        {
            throw new ArgumentException("Email is invalid.");
        }

        deactivationDate ??= DateTime.MaxValue;

        if (DateTime.Now >= deactivationDate)
            throw new ArgumentException("Deactivaton date can't be in the past.");

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