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
            FormatUserProperties(userConverted);

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
        return _userRepo.FindUserInDb(emailAK);
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
    
    private void FormatUserProperties(User user)
    {
        user.Email = user.Email.ToLower();
        user.FirstName = char.ToUpper(user.FirstName[0]) + user.FirstName.Substring(1).ToLower();
        user.LastName = char.ToUpper(user.LastName[0]) + user.LastName.Substring(1).ToLower();
    }
    
    public void UpdateUser(UserDTO userDtoUpdated)
    {
        User userWithUpdates = ToUser(userDtoUpdated);
        User userToUpdate = FindUser(userWithUpdates.Email);
        userWithUpdates.UserId = userToUpdate.UserId;
        try
        {
            Helper.AreTheSameObject(userWithUpdates, userToUpdate);
        }
        catch (ExceptionHelper Exception)
        {
            throw new ExceptionController(Exception.Message);               
        }
        
        _userRepo.Update(userWithUpdates);
    }

    public bool LoginUser(UserDTO userToLog)
    {
        bool logged =_userRepo.Login(ToUser(userToLog));
        
        if (!logged)
        {
            throw new ExceptionController("User not exists, maybe you have an error on the email or password?");
        }

        return true;
    }

    public void RegisterUser(UserDTO userToRegister)
    {
        throw new NotImplementedException();
    }

   
}