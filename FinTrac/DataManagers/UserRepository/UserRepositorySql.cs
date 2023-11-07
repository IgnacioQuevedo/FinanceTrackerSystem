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
            .Include(u=>u.MyAccounts).ThenInclude(a => a.MyTransactions)
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

    public void EmailUsed(string UserEmail)
    {
        if (FindUserInDb(UserEmail) != null)
        {
            throw new ExceptionUserRepository("Email already registered, impossible to create another account.");
        }
    }
    
    public void Update(User updatedUser)
    {
        var existingUser = FindUserInDb(updatedUser.Email);

        updatedUser.UserId = existingUser.UserId;
        _database.Entry(existingUser).CurrentValues.SetValues(updatedUser);
        _database.SaveChanges();
    }

    public User FindUserInDb(string emailAK)
    {
        return _database.Users.FirstOrDefault(u => u.Email == emailAK);
    }

    public bool Login(UserDTO userLogin)
    {
        User userFound = FindUserInDb(userLogin.Email);

        if (userFound != null && userLogin.Password.Equals(userFound.Password))
        {
            return true;
        }
        return false;
    }
}