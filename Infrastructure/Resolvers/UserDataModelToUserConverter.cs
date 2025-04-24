using AutoMapper;
using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Resolvers;
public class UserDataModelToUserConverter : ITypeConverter<UserDataModel, User>
{
    private readonly IUserFactory _UserFactory;

    public UserDataModelToUserConverter(IUserFactory UserFactory)
    {
        _UserFactory = UserFactory;
    }

    public User Convert(UserDataModel source, User destination, ResolutionContext context)
    {
        return _UserFactory.Create(source);
    }
}