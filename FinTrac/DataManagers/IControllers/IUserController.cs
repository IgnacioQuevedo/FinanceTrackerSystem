using BusinessLogic.Dto_Components;
using BusinessLogic.User_Components;

namespace DataManagers.IControllers;

public interface IUserController
{
     public User ToUser(UserDTO userDto);
     public UserDTO ToDtoUser(User userToConvert);
     public void CreateUser(UserDTO userDtoToCreate);
     public void UpdateUser(UserDTO userDto);
     public bool LoginUser(UserDTO userToLog);
     public void RegisterUser(UserDTO userToRegister);
     
    
     

}

