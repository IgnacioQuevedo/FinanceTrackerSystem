using BusinessLogic.Dto_Components;
using BusinessLogic.Exceptions;
using BusinessLogic.User_Components;
using Microsoft.EntityFrameworkCore;


namespace DataManagers;

public class UserRepositorySql
{
    private SqlContext _database;


    public UserRepositorySql(SqlContext database)
    {
        _database = database;
    }

    public User InstanceLists(User userConnected)
    {
        var userWithInitializedLists = _database.Users
            .Include(u => u.MyCategories)
            .Include(u => u.MyAccounts)
            .Include(u => u.MyAccounts).ThenInclude(a => a.MyTransactions)
            .Include(u => u.MyGoals)
            .Include(u => u.MyExchangesHistory)
            .FirstOrDefault(u => u.UserId == userConnected.UserId);

        return userWithInitializedLists;
    }

    public void Create(User userToAdd)
    {
        _database.Users.Add(userToAdd);
        _database.SaveChanges();
    }

    public void EmailUsed(string userWithEmailToCheck)
    {
        userWithEmailToCheck = userWithEmailToCheck.ToLower();
        if (GetUserViaEmail(userWithEmailToCheck) != null)
        {
            throw new ExceptionUserRepository("Email already registered, impossible to create another account.");
        }
    }

    public void Update(User updatedUser)
    {
        var existingUser = FindUserInDb(updatedUser.UserId);
        
        _database.Entry(existingUser).CurrentValues.SetValues(updatedUser);
        _database.SaveChanges();
    }

    public User FindUserInDb(int userId)
    {
        return _database.Users.FirstOrDefault(u => u.UserId == userId);
    }
    
    public bool Login(UserDTO userLogin)
    {
        var userInDb = GetUserViaEmail(userLogin.Email); 
        
        if (userInDb != null && userLogin.Password.Equals(userInDb.Password))
        {
            return true;
        }
        
        return false;
    }

    private User? GetUserViaEmail(string emailUser)
    {
        User userInDb = _database.Users.FirstOrDefault(u => u.Email == emailUser);
        return userInDb;
    }
    
}