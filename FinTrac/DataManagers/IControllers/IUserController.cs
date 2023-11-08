using BusinessLogic.Dto_Components;
using BusinessLogic.User_Components;

namespace DataManagers.IControllers;

public interface IUserController
{
     public User ToUser(UserDTO userDto);
     public UserDTO ToUserDTO(User userToConvert);
     public void RegisterUser(UserDTO userDtoToCreate);
     public bool LoginUser(UserDTO userToLogin);
     public void UpdateUser(UserDTO userDto);
     public UserDTO FindUser(int userId);
     




}

