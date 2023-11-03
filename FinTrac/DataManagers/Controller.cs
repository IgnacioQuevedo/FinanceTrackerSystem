using BusinessLogic.Dto_Components;
using BusinessLogic.User_Components;

namespace DataManagers;

public class Controller
{
     public UserRepositorySql UserRepo { get; set; }


     public Controller()
     {
          
     }


     public User toUser(UserDTO userDto)
     {
          throw new NotImplementedException();
     }
}