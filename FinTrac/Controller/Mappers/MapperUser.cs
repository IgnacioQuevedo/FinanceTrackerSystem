using BusinessLogic.User_Components;
using BusinessLogic.Dto_Components;
using BusinessLogic.Exceptions;

namespace Controller.Mappers;

public abstract class MapperUser
{
    #region ToUser

    public static User ToUser(UserDTO userDto)
    {
        User userConverted = new User(userDto.FirstName, userDto.LastName, userDto.Email, userDto.Password,
            userDto.Address);
        userConverted.UserId = userDto.UserId;
        FormatUserProperties(userConverted);

        return userConverted;
    }

    #endregion

    #region ToUserDTO

    public static UserDTO ToUserDTO(User userToConvert)
    {
        UserDTO dtoOfUser = new UserDTO(userToConvert.FirstName, userToConvert.LastName,
            userToConvert.Email, userToConvert.Password, userToConvert.Address);
        dtoOfUser.UserId = userToConvert.UserId;

        return dtoOfUser;
    }

    #endregion


    #region AuxiliaryMethods

    private static void FormatUserProperties(User user)
    {
        user.Email = user.Email.ToLower();
        user.FirstName = char.ToUpper(user.FirstName[0]) + user.FirstName.Substring(1).ToLower();
        user.LastName = char.ToUpper(user.LastName[0]) + user.LastName.Substring(1).ToLower();
    }

    #endregion
}