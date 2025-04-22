using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper
{
    public class UserMapper : IMapper<User, UserDataModel>
    {

        private IUserFactory _userFactory;

        public UserMapper(IUserFactory userFactory)
        {
            _userFactory = userFactory;
        }

        public User ToDomain(UserDataModel userDM)
        {
            var userDomain = _userFactory.Create(userDM);
            return userDomain;
        }

        public IEnumerable<User> ToDomain(IEnumerable<UserDataModel> usersDM)
        {
            return usersDM.Select(ToDomain);
        }

        public UserDataModel ToDataModel(User user)
        {
            return new UserDataModel(user);
        }

        public IEnumerable<UserDataModel> ToDataModel(IEnumerable<User> users)
        {
            return users.Select(ToDataModel);
        }
    }
}
