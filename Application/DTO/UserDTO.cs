
using Domain.Models;
using Domain.Interfaces;

public class UserDTO
{
    public string Names { get; set; }
    public string Surnames { get; set; }
    public string Email { get; set; }
    public DateTime? DeactivationDate { get; set; }

    public UserDTO() { }

    public UserDTO(string names, string surnames, string email, DateTime? deactivationDate)
    {
        Names = names;
        Surnames = surnames;
        Email = email;
        DeactivationDate = deactivationDate;
    }

    public static UserDTO ToDTO(IUser user)
    {
        return new UserDTO(
            user.GetNames(),
            user.GetSurnames(),
            user.GetEmail(),
            user.GetPeriodDateTime()._finalDate == DateTime.MaxValue
                ? null
                : user.GetPeriodDateTime()._finalDate
        );
    }

    public static IEnumerable<UserDTO> ToDTO(IEnumerable<IUser> users)
    {
        var userDTOs = new List<UserDTO>();

        foreach (var user in users)
        {
            userDTOs.Add(ToDTO(user));
        }

        return userDTOs;
    }

    public static IUser ToDomain(UserDTO userDTO)
    {
        return new User(
            userDTO.Names,
            userDTO.Surnames,
            userDTO.Email,
            userDTO.DeactivationDate
        );
    }

    public static IUser UpdateToDomain(IUser user, UserDTO userDTO)
    {

        user = new User(
            user.GetId(),
            userDTO.Names,
            userDTO.Surnames,
            user.GetEmail(),
            new PeriodDateTime(
                user.GetPeriodDateTime()._initDate,
                userDTO.DeactivationDate ?? DateTime.MaxValue
            )
        );

        return user;
    }

}

