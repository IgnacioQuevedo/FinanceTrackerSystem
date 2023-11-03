using Azure.Identity;
using BusinessLogic.Dto_Components;
using BusinessLogic.User_Components;
using DataManagers.IControllers;

namespace DataManagers;

public class Controller : IUserController
{
    private UserRepositorySql _userRepo { get; set; }

    public Controller(SqlContext database)
    {
        _userRepo = new UserRepositorySql(database);
    }

    #region ToUser

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

    #endregion

    #region ToUserDTO

    public UserDTO ToDtoUser(User userToConvert)
    {
        UserDTO dtoOfUser = new UserDTO(userToConvert.FirstName, userToConvert.LastName,
            userToConvert.Email, userToConvert.Password, userToConvert.Address);

        return dtoOfUser;
    }

    #endregion

    #region FindUser
    public User FindUser(string emailAK)
    {
        return _userRepo.Find(emailAK);
    }
    #endregion
    public void CreateUser(UserDTO userDtoToCreate)
    {
        try
        {
            User userToAdd = ToUser(userDtoToCreate);
            _userRepo.EmailUsed(userToAdd.Email);
            _userRepo.Create(userToAdd);
        }
        catch (ExceptionUserRepository Exception)
        {
            throw new ExceptionController(Exception.Message);
        }
    }

    public void UpdateUser(UserDTO userDto)
    {
        throw new NotImplementedException();
    }

    public void LoginUser(UserDTO userToLog)
    {
        throw new NotImplementedException();
    }

    public void RegisterUser(UserDTO userToRegister)
    {
        throw new NotImplementedException();
    }

   
}