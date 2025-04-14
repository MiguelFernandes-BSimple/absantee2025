using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public interface IUserFactory
{
    public Task<User> Create(string names, string surnames, string email, DateTime? deactivationDate);
    public User Create(IUserVisitor userVisitor);
}