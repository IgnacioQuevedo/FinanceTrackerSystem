using System.Security.Cryptography;
using BusinessLogic.Account_Components;
using BusinessLogic.Category_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Exceptions;
using BusinessLogic.ExchangeHistory_Components;
using BusinessLogic.Goal_Components;
using BusinessLogic.User_Components;
using Controller.IControllers;
using Controller.Mappers;
using DataManagers;
using Mappers;

namespace Controller;

public class GenericController : IUserController, ICategoryController, IGoalController, IExchangeHistoryController
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
            categoryToAdd.CategoryId = 0;

            _userConnected.AddCategory(categoryToAdd);

            _userRepo.Update(_userConnected);
        }
        catch (ExceptionMapper Exception)
        {
            throw new Exception(Exception.Message);
        }
    }

    public Category FindCategoryInDb(CategoryDTO categoryToFind)
    {
        SetUserConnected(categoryToFind.UserId);

        return SearchCategoryInDb(categoryToFind.CategoryId);
    }

    private Category SearchCategoryInDb(int idCategoryToFind)
    {
        foreach (var category in _userConnected.MyCategories)
        {
            if (category.CategoryId == idCategoryToFind)
            {
                return category;
            }
        }

        throw new Exception("Category was not found, an error on index must be somewhere.");
    }

    public CategoryDTO FindCategory(int idCategoryToFind, int userId)
    {
        Category categoryFound = SearchCategoryInDb(idCategoryToFind);
        CategoryDTO categoryFoundDTO = MapperCategory.ToCategoryDTO(categoryFound);

        return categoryFoundDTO;
    }


    public void UpdateCategory(CategoryDTO categoryDtoWithUpdates)
    {
        SetUserConnected(categoryDtoWithUpdates.UserId);
        Category categoryToUpd = MapperCategory.ToCategory(categoryDtoWithUpdates);
        Category categoryWithoutUpd = FindCategoryInDb(categoryDtoWithUpdates);

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

    public void DeleteCategory(CategoryDTO categoryDtoToDelete)
    {
        try
        {
            SetUserConnected(categoryDtoToDelete.UserId);
            _userConnected.DeleteCategory(FindCategoryInDb(categoryDtoToDelete));
            _userRepo.Update(_userConnected);
        }
        catch (ExceptionCategoryManagement Exception)
        {
            throw new Exception(Exception.Message);
        }
    }

    public List<CategoryDTO> GetAllCategories(int userConnectedId)
    {
        SetUserConnected(userConnectedId);
        List<CategoryDTO> listCategoryDTO = new List<CategoryDTO>();

        listCategoryDTO = MapperCategory.ToListOfCategoryDTO(_userConnected.MyCategories);

        return listCategoryDTO;
    }

    public List<Category> ReceiveCategoryListFromUser(int userConnectedId)
    {
        SetUserConnected(userConnectedId);
        return _userConnected.MyCategories;
    }

    #endregion

    #region Goal Section

    public void CreateGoal(GoalDTO goalDtoToCreate)
    {
        SetUserConnected((int)goalDtoToCreate.UserId);

        try
        {
            List<Category> categoriesOfGoal = SetListOfCategories(goalDtoToCreate);
            Goal goalToAdd = MapperGoal.ToGoal(goalDtoToCreate, categoriesOfGoal);
            goalToAdd.GoalId = 0;

            _userConnected.AddGoal(goalToAdd);
            _userRepo.Update(_userConnected);
        }
        catch (Exception ExceptionType) when (
            ExceptionType is ExceptionUserRepository ||
            ExceptionType is ExceptionMapper
        )
        {
            throw new Exception(ExceptionType.Message);
        }
    }

    public List<GoalDTO> GetAllGoalsDTO(int userConnectedId)
    {
        SetUserConnected(userConnectedId);
        List<GoalDTO> listGoalDTO = new List<GoalDTO>();

        listGoalDTO = MapperGoal.ToListOfGoalDTO(_userConnected.MyGoals);

        return listGoalDTO;
    }

    public List<Goal> ReceiveGoalListFromUser(int userConnectedId)
    {
        SetUserConnected(userConnectedId);
        return _userConnected.MyGoals;
    }

    private List<Category> SetListOfCategories(GoalDTO goalDtoToCreate)
    {
        List<Category> result = new List<Category>();
        foreach (CategoryDTO categoryDTO in goalDtoToCreate.CategoriesOfGoalDTO)
        {
            result.Add(FindCategoryInDb(categoryDTO));
        }

        return result;
    }

    #endregion

    #region Exchange History Section

    public void CreateExchangeHistory(ExchangeHistoryDTO exchangeDTO)
    {
        try
        {
            SetUserConnected((int)exchangeDTO.UserId);

            ExchangeHistory exchangeHistoryToCreate = MapperExchangeHistory.ToExchangeHistory(exchangeDTO);
            exchangeHistoryToCreate.ExchangeHistoryId = 0;
            _userConnected.AddExchangeHistory(exchangeHistoryToCreate);
            _userRepo.Update(_userConnected);
        }
        catch (ExceptionMapper Exception)
        {
            throw new Exception(Exception.Message);
        }
    }

    public ExchangeHistoryDTO FindExchangeHistory(int IdOfExchangeToFound, int idUserConnected)
    {
        SetUserConnected(idUserConnected);

        ExchangeHistoryDTO exchangeHistoryDTOFound =
            MapperExchangeHistory.ToExchangeHistoryDTO(searchInDbForAnExchange(IdOfExchangeToFound));

        return exchangeHistoryDTOFound;
    }


    #region ExchangeHistory Find method specifically for controller section.

    //This method will only be used in the controller section. Is necessary for some methods like update,delete,etc
    public ExchangeHistory FindExchangeHistoryInDB(ExchangeHistoryDTO exchangeToFound)
    {
        SetUserConnected((int)exchangeToFound.UserId);
        return searchInDbForAnExchange(exchangeToFound.ExchangeHistoryId);
    }

    private ExchangeHistory searchInDbForAnExchange(int idOfExchangeToSearch)
    {
        foreach (var exchangeHistory in _userConnected.MyExchangesHistory)
        {
            if (exchangeHistory.ExchangeHistoryId == idOfExchangeToSearch)
            {
                {
                    return exchangeHistory;
                }
            }
        }

        throw new Exception("Exchange History was not found, an error on index must be somewhere.");
    }

    #endregion


    public void UpdateExchangeHistory(ExchangeHistoryDTO dtoWithUpdates)
    {
        try
        {
            SetUserConnected((int)dtoWithUpdates.UserId);

            ExchangeHistory exchangeHistoryToUpdate = FindExchangeHistoryInDB(dtoWithUpdates);
            exchangeHistoryToUpdate.ValidateApplianceExchangeOnTransaction();

            ExchangeHistory exchangeHistoryWithUpdates = MapperExchangeHistory.ToExchangeHistory(dtoWithUpdates);
            _userConnected.ModifyExchangeHistory(exchangeHistoryWithUpdates);
            _userRepo.Update(_userConnected);
        }

        catch (Exception ExceptionType)
            when (
                ExceptionType is ExceptionExchangeHistoryManagement or ExceptionMapper
            )
        {
            throw new Exception(ExceptionType.Message);
        }
    }

    public void DeleteExchangeHistory(ExchangeHistoryDTO dtoToDelete)
    {
        try
        {
            SetUserConnected((int)dtoToDelete.UserId);
            ExchangeHistory exchangeHistoryToDelete = FindExchangeHistoryInDB(dtoToDelete);
            exchangeHistoryToDelete.ValidateApplianceExchangeOnTransaction();
            _userConnected.DeleteExchangeHistory(exchangeHistoryToDelete);

            _userRepo.UpdateDbWhenDeleting(_userConnected, exchangeHistoryToDelete);
        }
        catch (Exception ExceptionType)
            when (
                ExceptionType is ExceptionExchangeHistoryManagement or ExceptionMapper
            )
        {
            throw new Exception(ExceptionType.Message);
        }
    }

    public List<ExchangeHistoryDTO> GetAllExchangeHistories(int userConnectedId)
    {
        SetUserConnected(userConnectedId);
        return MapperExchangeHistory.ToListOfExchangeHistoryDTO(_userConnected.MyExchangesHistory);
    }

    #endregion


    #region Monetary Account section

    public void CreateMonetaryAccount(MonetaryAccountDTO monetAccountDTOToAdd)
    {
        try
        {
            SetUserConnected((int)monetAccountDTOToAdd.UserId);
            MonetaryAccount monetAccountToAdd = MapperMonetaryAccount.ToMonetaryAccount(monetAccountDTOToAdd);
            monetAccountToAdd.AccountId = 0;

            _userConnected.AddMonetaryAccount(monetAccountToAdd);

            _userRepo.Update(_userConnected);
        }
        catch (ExceptionMapper Exception)
        {
            throw new Exception(Exception.Message);
        }
    }

    #endregion

    public void CreateCreditAccount(CreditCardAccountDTO creditAccountDTOToAdd)
    {
        throw new NotImplementedException();
    }



}