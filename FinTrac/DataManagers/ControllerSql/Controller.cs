using BusinessLogic.Dto_Components;
using BusinessLogic.User_Components;

namespace DataManagers;

public class Controller
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
}