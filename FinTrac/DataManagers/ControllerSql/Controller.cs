using Azure.Identity;
using BusinessLogic.Dto_Components;
using BusinessLogic.User_Components;
using DataManagers.IControllers;

namespace DataManagers;

public class Controller : IUserController
{
     public UserRepositorySql UserRepo { get; set; }
     
     public Controller() 
     {
          
     }
     
     public User ToUser(UserDTO userDto)
     {
          try
          {
               User userConverted = new User(userDto.FirstName, userDto.LastName, userDto.Email, userDto.Password,
                    userDto.Address);
               return userConverted;
          }
          catch (Exception GenericException)
          {
               throw new ExceptionController(GenericException.Message);
          }
          
     }

     public UserDTO ToDtoUser(User userToConvert)
     {
          UserDTO dtoOfUser = new UserDTO(userToConvert.FirstName, userToConvert.LastName,
               userToConvert.Email, userToConvert.Password,userToConvert.Address);
          
          return dtoOfUser;
     }

     public void CreateUser(UserDTO userDtoToCreate)
     {
          throw new NotImplementedException();
     }
     
     public void UpdateUser(UserDTO userDto)
     {
          throw new NotImplementedException();
     }

     public void Login(UserDTO userToLog)
     {
          throw new NotImplementedException();
     }

     public void Register(UserDTO userToRegister)
     {
          throw new NotImplementedException();
     }
}