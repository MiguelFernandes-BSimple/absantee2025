using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper
{
    public class UserMapper : IMapper<User, UserDataModel>
    {
        public User ToDomain(UserDataModel userDM)
        {
            User user = new User(userDM.Names, userDM.Surnames, userDM.Email, userDM.PeriodDateTime.GetFinalDate());

            user.SetId(userDM.Id);

            return user;
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
