using BusinessLogic.Dto_Components;
using BusinessLogic.User_Components;
using DataManagers.IControllers;
using DataManagers;

namespace Controller;

public class GenericController : IUserController
{
    private UserRepositorySql _userRepo;
    private User _userConnected { get; set; }

    public GenericController(UserRepositorySql userRepo)
    {
        _userRepo = userRepo;
    }
    
    public void SetUserConnected(UserDTO userToConnect)
    {
        if (userToConnect != null)
        {
            _userConnected = ToUser(userToConnect);
            _userRepo.InstanceLists(_userConnected);
        }
    }

    #region User Repo

    #region ToUser

    public User ToUser(UserDTO userDto)
    {
        try
        {
            User userConverted = new User(userDto.FirstName, userDto.LastName, userDto.Email, userDto.Password,
                userDto.Address);
            userConverted.UserId = userDto.UserId;
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

    public UserDTO ToUserDTO(User userToConvert)
    {
        UserDTO dtoOfUser = new UserDTO(userToConvert.FirstName, userToConvert.LastName,
            userToConvert.Email, userToConvert.Password, userToConvert.Address);
        dtoOfUser.UserId = userToConvert.UserId;

        return dtoOfUser;
    }

    #endregion

    #region FindUser

    public UserDTO FindUser(int userId)
    {
        User userFound = _userRepo.FindUserInDb(userId);

        if (userFound != null)
        {
            return ToUserDTO(userFound);
        }
        else
        {
            throw new ExceptionController("User not found.");
        }
    }

    #endregion

    #region Register

    public void RegisterUser(UserDTO userDtoToCreate)
    {
        try
        {
            _userRepo.EmailUsed(userDtoToCreate.Email);
            User userToAdd = ToUser(userDtoToCreate);

            _userRepo.Create(userToAdd);
        }
        catch (ExceptionUserRepository Exception)
        {
            throw new ExceptionController(Exception.Message);
        }
    }

    public bool PasswordMatch(string password, string passwordRepeated)
    {
        bool passwordMatch = Helper.AreTheSameObject(password, passwordRepeated);

        if (!passwordMatch)
        {
            throw new ExceptionController("Passwords are not the same, try again.");
        }

        return passwordMatch;
    }

    #endregion

    #region UpdateUser

    public void UpdateUser(UserDTO userDtoUpdated)
    {
        User userWithUpdates = ToUser(userDtoUpdated);
        
        if (Helper.AreTheSameObject(userWithUpdates, _userConnected))
        {
            throw new ExceptionController("You need to change at least one value.");
        }

        _userRepo.Update(userWithUpdates);
    }

    #endregion

    #region LoginUser

    public bool LoginUser(UserDTO userToLogin)
    {
        userToLogin.Email = userToLogin.Email.ToLower();
        bool logged = _userRepo.Login(userToLogin);

        if (!logged)
        {
            throw new ExceptionController("User not exists, maybe you have an error on the email or password?");
        }

        return true;
    }

    #endregion

    #region AuxiliaryMethods

    private void FormatUserProperties(User user)
    {
        user.Email = user.Email.ToLower();
        user.FirstName = char.ToUpper(user.FirstName[0]) + user.FirstName.Substring(1).ToLower();
        user.LastName = char.ToUpper(user.LastName[0]) + user.LastName.Substring(1).ToLower();
    }

    #endregion

    #endregion
}