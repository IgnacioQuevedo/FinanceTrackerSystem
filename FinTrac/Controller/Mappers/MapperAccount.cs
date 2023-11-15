using BusinessLogic.User_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Exceptions;
using Mappers;

namespace Controller.Mappers;

public abstract class MapperAccount
{
	#region ToUser

	public static User ToAccount(UserDTO userDto)
	{
		try
		{
			User userConverted = new User(userDto.FirstName, userDto.LastName, userDto.Email, userDto.Password,
				userDto.Address);

			userConverted.UserId = userDto.UserId;
			FormatUserProperties(userConverted);
			return userConverted;
		}
		catch (ExceptionValidateUser Exception)
		{
			throw new ExceptionMapper(Exception.Message);
		}
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