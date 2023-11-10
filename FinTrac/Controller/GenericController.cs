using BusinessLogic.Category_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.User_Components;
using Controller.IControllers;
using Controller.Mappers;
using DataManagers;
using Mappers;

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
            throw new Exception("User not found.");
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
        catch (Exception ExceptionType) when (
            ExceptionType is ExceptionUserRepository ||
            ExceptionType is ExceptionMapper
        )
        {
            throw new Exception(ExceptionType.Message);
        }
    }

    public void PasswordMatch(string password, string passwordRepeated)
    {
        bool passwordMatch = Helper.AreTheSameObject(password, passwordRepeated);

        if (!passwordMatch)
        {
            throw new Exception("Passwords are not the same, try again.");
        }
    }

    #endregion

    #region UpdateUser

    public void UpdateUser(UserDTO userDtoUpdated)
    {
        int userConnectedId = _userRepo.GetUserViaEmail(userDtoUpdated.Email).UserId;
        userDtoUpdated.UserId = userConnectedId;

        SetUserConnected(userConnectedId);
        try
        {
            User userWithUpdates = MapperUser.ToUser(userDtoUpdated);

            if (Helper.AreTheSameObject(userWithUpdates, _userConnected))
            {
                throw new Exception("You need to change at least one value.");
            }

            _userRepo.Update(userWithUpdates);
        }
        catch (ExceptionMapper Exception)
        {
            throw new Exception(Exception.Message);
        }
    }

    #endregion

    #region LoginUser

    public bool LoginUser(UserLoginDTO userToLogin)
    {
        userToLogin.Email = userToLogin.Email.ToLower();
        bool logged = _userRepo.Login(userToLogin);

        if (!logged)
        {
            throw new Exception("User not exists, maybe you have an error on the email or password?");
        }

        return true;
    }

    #endregion

    #endregion

    #region Category Section
    public void CreateCategory(CategoryDTO dtoToAdd)
    {
        try
        {
            SetUserConnected(dtoToAdd.UserId);
            Category categoryToAdd = MapperCategory.ToCategory(dtoToAdd);


            _userConnected.AddCategory(categoryToAdd);
            _userRepo.Update(_userConnected);
        }
        catch (ExceptionMapper Exception)
        {
            throw new Exception(Exception.Message);
        }
    }

    public Category FindCategory(int idOfCategoryToFind)
    {
        try
        {
            SetUserConnected(idOfCategoryToFind);
            return _userConnected.MyCategories[idOfCategoryToFind - 1];
        }
        catch (Exception Exception)
        {
            throw new Exception("Category was not found, an error on index must be somewhere.");
        }
    }

    public void UpdateCategory(CategoryDTO categoryDtoWithUpdates)
    {
        SetUserConnected(categoryDtoWithUpdates.UserId);
        Category categoryToUpd = MapperCategory.ToCategory(categoryDtoWithUpdates);
        Category categoryWithoutUpd = FindCategory(categoryDtoWithUpdates.CategoryId);
        
        categoryToUpd.CategoryUser = _userConnected;
        if (Helper.AreTheSameObject(categoryToUpd, categoryWithoutUpd))
        {
            throw new Exception("There are non existential changes, change at least one please.");
        }
        else
        {
            _userConnected.ModifyCategory(categoryToUpd);
            _userRepo.Update(_userConnected);
        }
    }
    
    #endregion
    
    public void DeleteCategory(CategoryDTO categoryDtoCategoryId)
    {
        SetUserConnected(categoryDtoCategoryId.UserId);
        _userConnected.DeleteCategory(FindCategory(categoryDtoCategoryId.UserId));
        _userRepo.Update(_userConnected);
    }
}