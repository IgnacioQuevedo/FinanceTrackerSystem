using BusinessLogic.Dto_Components;
using BusinessLogic.User_Components;
using Controller.Mappers;
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

    public void SetUserConnected(int userIdToConnect)
    {
        if (_userConnected == null)
        {
            _userConnected = _userRepo.FindUserInDb(userIdToConnect);
            _userRepo.InstanceLists(_userConnected);
        }
    }

    #region User Repo

    #region FindUser

    public UserDTO FindUser(int userId)
    {
        User userFound = _userRepo.FindUserInDb(userId);

        if (userFound != null)
        {
            return MapperUser.ToUserDTO(userFound);
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
            User userToAdd = MapperUser.ToUser(userDtoToCreate);

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
        SetUserConnected(userDtoUpdated.UserId);

        User userWithUpdates = MapperUser.ToUser(userDtoUpdated);

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

    #endregion
}