using BusinessLogic.Dto_Components;
using BusinessLogic.User_Components;

namespace DataManagers.IControllers;

public interface IUserController
{
     public User ToUser(UserDTO userDto);
     public UserDTO ToDtoUser(User userToConvert);
     public void RegisterUser(UserDTO userDtoToCreate);
     public bool LoginUser(UserDTO userToLog);
     public void UpdateUser(UserDTO userDto);
     public User FindUser(string emailAK);




}

